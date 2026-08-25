imports Common.Public.dll;
imports Common.Core.dll;
imports Common.DAO.Impl.dll;
imports Common.DAO.Reports.Impl.dll;

#region Includes
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.DAO.Impl.DataSync;
using TechnoPro.Common.DAO.Impl.Reports;
using TechnoPro.Common.DynamicCompiler.CompilerArgs.Reports;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using LegacyReportFunction = TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction;
#endregion

/* FastBatchDataSync - drop into an "Execute c# code" function step in the Report Editor,
 * in place of the stock "Batch data sync" (Data_Sync_Update_All_Students) step, as one step
 * within the same report that currently produces the active-student table (i.e. this reads
 * the same InputTable that step already feeds the stock step today - no other report changes
 * needed).
 *
 * WHY: the stock engine (DataSyncManager.DataSyncStudent) runs one full Report execution per
 * student for data, and one more per student for courses - each one opening a fresh connection
 * to the external Oracle reporting server (Common.OracleDatabase.OracleQuery, loaded via
 * Assembly.LoadFile + reflection on every single call). For N students that's 2N remote round
 * trips plus 2N passes through the generic report engine, capped at 4 parallel threads. That's
 * what turns a few thousand students into a multi-hour job.
 *
 * WHAT THIS DOES INSTEAD: fetches every student's external row(s) in a handful of chunked
 * IN-list queries against the same Oracle connection your ImportStudentDataReportId /
 * ImportStudentCoursesReportId reports already use, then hands the results to the SAME proven
 * diff/write logic the stock engine already uses:
 *   - LegacyReportFunction.ImportStudents (the real engine behind the "Data Sync (Data)" /
 *     Import_User_Data report step) already loops over every row of the DataView you give it,
 *     so it's called once per chunk of many students instead of once per student - unchanged.
 *   - DataSyncCourseManager.DataSyncCourses is genuinely per-student, so it's still called once
 *     per student, but now purely against the local ClockWork DB (no remote query gating it
 *     any more), so it runs with far more than 4-way parallelism.
 *
 * This does NOT reimplement field-mapping, encryption, or course-matching logic - all of that
 * (where the real correctness risk lives) is reused unchanged from the compiled application.
 * It only replaces how the external rows are acquired.
 *
 * SETUP REQUIRED (one-time, in the Report Editor): the Oracle Query step inside your
 * ImportStudentDataReportId and ImportStudentCoursesReportId reports currently filters by a
 * single student number (e.g. "WHERE student_no = :studentno"). Change that clause to use the
 * token this script looks for:
 *     WHERE student_no IN ({{STUDENT_NO_LIST}})
 * That's the one manual edit needed elsewhere - everything else reads your existing report
 * configuration automatically.
 */
namespace ClockWorkDynamicCSharp {
	public class CSharp {
		private const string StudentNoListToken = "{{STUDENT_NO_LIST}}";

		// How many student numbers to pack into a single external IN-list query.
		// Oracle caps a literal IN-list at 1000 items - stay comfortably under that.
		private const int ChunkSize = 500;

		// How many students' course-sync calls to run concurrently once their external course
		// rows are already sitting in memory. The stock code hardcodes 4 because each unit of
		// work used to include a remote Oracle round trip; here it doesn't, so this can be much
		// higher. Tune against your ClockWork DB's connection pool size.
		private const int CourseSyncDegreeOfParallelism = 16;

		private DataTable InputTable;
		private DataSyncOperationContext OpContext;

		public DataTable MainScript() {
			RequireColumn("student_no");

			List<string> studentNumbers = InputTable.Rows.Cast<DataRow>()
				.Select(r => (r["student_no"] == DBNull.Value ? "" : r["student_no"].ToString()).Trim().ToUpperInvariant())
				.Where(s => s.Length > 0)
				.Distinct()
				.ToList();

			var dataSyncInfoManager = new DataSyncInfoManager(OpContext);
			DataSyncInfo dataSyncInfo = dataSyncInfoManager.LoadDataSyncInfo();
			int dataReportId = (dataSyncInfo != null) ? dataSyncInfo.ImportStudentDataReportId : 0;
			int coursesReportId = (dataSyncInfo != null) ? dataSyncInfo.ImportStudentCoursesReportId : 0;

			var reportManager = new ReportManager(OpContext);
			Report dataReport = (dataReportId > 0) ? reportManager.LoadReport(dataReportId) : null;
			Report coursesReport = (coursesReportId > 0) ? reportManager.LoadReport(coursesReportId) : null;

			BulkOracleStep dataOracleStep = (dataReport != null) ? ExtractOracleQueryStep(dataReport) : null;
			string importUserDataParams = (dataReport != null) ? ExtractFunctionParameter(dataReport, eFunctionType.Import_User_Data) : null;
			BulkOracleStep coursesOracleStep = (coursesReport != null) ? ExtractOracleQueryStep(coursesReport) : null;

			if (dataOracleStep == null || importUserDataParams == null) {
				CWLogger.Logger.Warn("FastBatchDataSync:MissingDataReportOracleQueryOrImportUserDataStep:reportId={0}", dataReportId.ToString());
			}
			if (coursesReportId > 0 && coursesOracleStep == null) {
				CWLogger.Logger.Warn("FastBatchDataSync:MissingCoursesReportOracleQueryStep:reportId={0}", coursesReportId.ToString());
			}

			int batchDataSyncLogId = 0;
			try {
				var dataSyncDAO = new DataSyncDAO(OpContext);
				batchDataSyncLogId = dataSyncDAO.GetNewBatchDataSyncLogId(studentNumbers.Count);
			} catch (Exception ex) {
				CWLogger.Logger.Warn("FastBatchDataSync:CouldNotCreateBatchLogRow:err={0}", ex.ToString());
			}

			DataTable resultTable = new DataTable("results");
			resultTable.Columns.Add("chunk_number", typeof(int));
			resultTable.Columns.Add("student_count", typeof(int));
			resultTable.Columns.Add("status");
			resultTable.Columns.Add("error");

			int chunkNumber = 0;
			int attemptedCount = 0;
			string firstError = null;

			foreach (List<string> chunk in Chunk(studentNumbers, ChunkSize)) {
				chunkNumber++;
				string chunkError = null;
				try {
					if (dataOracleStep != null && importUserDataParams != null) {
						DataTable externalData = FetchBulk(dataOracleStep, chunk);
						if (externalData != null && externalData.Rows.Count > 0) {
							// ImportStudents already loops over every row of the DataView it's given -
							// this is the exact same call the stock code makes once PER STUDENT, made
							// here once per chunk of up to ChunkSize students instead.
							LegacyReportFunction.ImportStudents(externalData.DefaultView, importUserDataParams, true, OpContext);
						}
					}

					if (coursesOracleStep != null) {
						DataTable externalCourses = FetchBulk(coursesOracleStep, chunk);
						if (externalCourses != null && externalCourses.Rows.Count > 0) {
							RunCourseSyncForChunk(externalCourses, chunk);
						}
					}

					attemptedCount += chunk.Count;
				} catch (Exception ex) {
					chunkError = ex.ToString();
					firstError = firstError ?? chunkError;
					CWLogger.Logger.Error("FastBatchDataSync:ChunkFailed:chunk={0}:err={1}", chunkNumber.ToString(), chunkError);
				}

				resultTable.Rows.Add(new object[] {
					chunkNumber,
					chunk.Count,
					(chunkError == null) ? "OK" : "FAILED",
					(object) chunkError ?? DBNull.Value
				});
			}

			if (batchDataSyncLogId > 0) {
				try {
					new DataSyncDAO(OpContext).UpdateBatchSync(batchDataSyncLogId, attemptedCount, firstError);
				} catch (Exception ex) {
					CWLogger.Logger.Warn("FastBatchDataSync:CouldNotUpdateBatchLogRow:err={0}", ex.ToString());
				}
			}

			return resultTable;
		}

		private void RunCourseSyncForChunk(DataTable externalCourses, List<string> chunkStudentNumbers) {
			if (!externalCourses.Columns.Contains("student_no")) {
				CWLogger.Logger.Warn("FastBatchDataSync:RunCourseSyncForChunk:MissingStudentNoColumn");
				return;
			}

			Dictionary<string, List<DataRow>> byStudent = externalCourses.Rows.Cast<DataRow>()
				.GroupBy(r => (r["student_no"] == DBNull.Value ? "" : r["student_no"].ToString()).Trim().ToUpperInvariant())
				.ToDictionary(g => g.Key, g => g.ToList());

			// OpContext is per-invocation state built once in CustomEntry, safe to read (not write)
			// from multiple threads here - each iteration only reads it to construct its own manager.
			DataSyncOperationContext opContext = OpContext;

			Parallel.ForEach(chunkStudentNumbers, new ParallelOptions { MaxDegreeOfParallelism = CourseSyncDegreeOfParallelism }, studentNo => {
				List<DataRow> rows;
				if (!byStudent.TryGetValue(studentNo, out rows) || rows.Count < 1) {
					return;
				}

				try {
					DataTable studentTable = externalCourses.Clone();
					foreach (DataRow row in rows) {
						studentTable.ImportRow(row);
					}

					// Same call the stock DataSyncCourses2 report function makes for one student -
					// reused unchanged so course-matching / registration logic doesn't need re-verifying.
					var courseManager = new DataSyncCourseManager(opContext);
					var rowParts = courseManager.GetRowPartsFromDataTable(studentTable);
					var externalCoursesForStudent = courseManager.ParseExternalCourseRowParts(rowParts);
					courseManager.DataSyncCourses(studentNo, externalCoursesForStudent);
				} catch (Exception ex) {
					CWLogger.Logger.Error("FastBatchDataSync:RunCourseSyncForChunk:snum={0}:error={1}", studentNo, ex.ToString());
				}
			});
		}

		private class BulkOracleStep {
			public string ConnectionString;
			public string Sql;
		}

		private static BulkOracleStep ExtractOracleQueryStep(Report report) {
			ReportFunction step = (report.Functions != null)
				? report.Functions.FirstOrDefault(f => f.FunctionCode == eFunctionType.Execute_Basic_Oracle_Query)
				: null;
			if (step == null) {
				return null;
			}

			OracleQueryParameters oracleParams = step.GetDefaultFunctionParameter().OracleQueryParametersFromXml();
			if (oracleParams == null || oracleParams.Query == null || string.IsNullOrEmpty(oracleParams.Query.Sql)) {
				return null;
			}

			return new BulkOracleStep {
				ConnectionString = oracleParams.ConnectionString,
				Sql = oracleParams.Query.Sql
			};
		}

		private static string ExtractFunctionParameter(Report report, eFunctionType functionType) {
			ReportFunction step = (report.Functions != null)
				? report.Functions.FirstOrDefault(f => f.FunctionCode == functionType)
				: null;
			return (step != null) ? step.GetDefaultFunctionParameter() : null;
		}

		private DataTable FetchBulk(BulkOracleStep step, IList<string> studentNumbers) {
			if (step.Sql.IndexOf(StudentNoListToken, StringComparison.OrdinalIgnoreCase) < 0) {
				throw new InvalidOperationException(string.Format(
					"Oracle Query step's SQL doesn't contain the {0} token this fast path needs. " +
					"In the Report Editor, change the WHERE clause (e.g. from 'student_no = :studentno') to " +
					"'student_no IN ({0})' for the report used as ImportStudentDataReportId / " +
					"ImportStudentCoursesReportId, then re-run.", StudentNoListToken));
			}

			string placeholders = string.Join(",", studentNumbers.Select((_, i) => ":sn" + i));
			string sql = step.Sql.Replace(StudentNoListToken, placeholders);

			var request = new OracleQueryRequest {
				QueryType = eOracleQueryType.Query,
				Sql = sql,
				Parameters = studentNumbers.Select((sn, i) => new OracleParameter {
					Name = "sn" + i,
					OracleDbType = "Varchar2",
					Value = sn
				}).ToList()
			};

			// One connection (opened via the existing OracleQueryDAO plumbing) per CHUNK instead
			// of one per student - this is the change that removes most of the multi-hour runtime.
			var oracleQueryDAO = new OracleQueryDAO(OpContext);
			return oracleQueryDAO.ExecuteOracleQuery(step.ConnectionString, request);
		}

		private static IEnumerable<List<string>> Chunk(IList<string> source, int size) {
			for (int i = 0; i < source.Count; i += size) {
				yield return source.Skip(i).Take(size).ToList();
			}
		}

		private void RequireColumn(string columnName) {
			if (InputTable == null) {
				throw new Exception("Script did not receive an input DataTable");
			}
			if (!InputTable.Columns.Contains(columnName)) {
				throw new ArgumentException("Required input table column missing: " + columnName);
			}
		}

		#region Backend for scripts
		// Program entry point. ClockWork looks up ClockWorkDynamicCSharp.CSharp.CustomEntry(ReportParameters)
		// by name via reflection - do not rename this method, its class, or its namespace.
		public ReportReturnValue CustomEntry(ReportParameters args) {
			InputTable = args.Table;
			OpContext = new DataSyncOperationContext {
				WhoAmI = args.WhoAmI,
				AppContext = new ApplicationContext {
					ExecutingPath = (args.Context != null) ? args.Context.BinPath : null
				}
			};

			DataTable result;
			try {
				result = MainScript();
			} catch (Exception ex) {
				CWLogger.Logger.Error("FastBatchDataSync:Failed:err={0}", ex.ToString());
				result = new DataTable();
				result.Columns.Add("error");
				result.Rows.Add(ex.Message);
				result.Rows.Add(ex.ToString());
			}

			return new ReportReturnValue(result, args.Variables);
		}
		#endregion
	}
}

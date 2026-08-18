imports Databases.dll;


imports Common.Core.dll;
imports Common.ICore.dll;
imports Common.Public.dll;
imports Common.DAO.dll;
imports Common.DAO.Impl.dll;
/**************************************
 * Boilerplate for all report scripts */
using TechnoPro.Common.DynamicCompiler.CompilerArgs.Reports;
using TechnoPro.Common.DynamicCompiler.CompilerArgs;
using ClockWorkLogger;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Databases;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.DAO.DataSync;
using TechnoPro.Common.DAO.Impl.DataSync;
namespace ClockWorkDynamicCSharp {
	public class CSharp {
		public static readonly OperationContext Context = new OperationContext();
		public static readonly DatabaseLayer Db = DatabaseLayerFactory.GetDatabaseLayer(
			eDatabaseConnectionStringName.ClockWork,
			Context.TenantId
		);
		private Dictionary<string, object> ReportVariables;
		private DataTable InputTable;
/***************************************
 * Code specific to this report script */
		public DataTable MainScript() {
			RequireColumn("student_no");
			int overrideStudentDataId = LoadOptionalIDVariable("OverrideImportStudentDataReportId");
			int overrideCoursesDataId = LoadOptionalIDVariable("OverrideImportStudentCoursesReportId");
			// Read for parity with the real step's configuration surface, but - same as the
			// real Data_Sync_Update_All_Students - never actually consulted below. The real
			// implementation always runs sequentially regardless of this value; that is
			// replicated here, not fixed.
			bool useSingleThread = LoadOptionalCheckboxVariable("UseSingleThread");
			int lastDataSyncControlId = LoadOptionalIDVariable("LastDataSyncControlId");
			double allowedMinutesToRun = (double) LoadOptionalIDVariable("AllowedMinutesToRun");
			// This replicates a system/batch process, not a personalized per-user report - so,
			// like the real scheduled job (ClockWorkServerBatchDataSyncJob), it runs under
			// Context's WhoAmI (0/system by default) rather than whichever user happens to
			// trigger this particular report.
			DataSyncOperationContext opContext = new DataSyncOperationContext { WhoAmI = Context.WhoAmI };
			IDataSyncInfoManager dataSyncInfoManager = new DataSyncInfoManager(opContext);
			DataSyncInfo dataSyncInfo = dataSyncInfoManager.LoadDataSyncInfo();
			int reportIdStudentData = overrideStudentDataId >= 1 ? overrideStudentDataId : (dataSyncInfo != null ? dataSyncInfo.ImportStudentDataReportId : 0);
			int reportIdCoursesData = overrideCoursesDataId >= 1 ? overrideCoursesDataId : (dataSyncInfo != null ? dataSyncInfo.ImportStudentCoursesReportId : 0);
			List<string> studentNumbers = new List<string>();
			int successfulStudentCount = 0;
			string errorMessage = null;
			int batchDataSyncLogId = 0;
			try {
				// Deliberately NOT deduplicated - the real core loop selects student_no from
				// every row as-is, with no Distinct(). A table with repeat rows per student
				// will sync that student more than once, same as the real step.
				studentNumbers = InputTable.Rows.Cast<DataRow>()
					.Select(dr => dr["student_no"].ToString().Trim().ToUpper())
					.ToList();
				if (lastDataSyncControlId > 0) {
					IDynamicDataForReportsManager dynamicDataManager = new DynamicDataForReportsManager(opContext);
					DataTable crossReferenced = dynamicDataManager.CrossReferencePerStudentData(InputTable, new List<int> { lastDataSyncControlId });
					if (crossReferenced != null) {
						DataView sorted = new DataView {
							Table = crossReferenced,
							Sort = crossReferenced.Columns[crossReferenced.Columns.Count - 1].ColumnName
						};
						studentNumbers = sorted.Cast<DataRowView>()
							.Select(drv => drv.Row["student_no"].ToString().Trim().ToUpper())
							.ToList();
					}
				}
				IDataSyncDAO dataSyncDao = new DataSyncDAO(opContext);
				batchDataSyncLogId = dataSyncDao.GetNewBatchDataSyncLogId(studentNumbers.Count);
				opContext.BatchDataSyncLogId = batchDataSyncLogId;
				IReportManager reportManager = new ReportManager(opContext);
				Report reportStudentData = reportManager.LoadReport(reportIdStudentData);
				Report reportCoursesData = reportManager.LoadReport(reportIdCoursesData);
				DateTime startTime = DateTime.Now;
				bool checkTimeLimit = allowedMinutesToRun > 0.0;
				TimeSpan allowedTimeSpan = TimeSpan.FromMinutes(allowedMinutesToRun);
				foreach (string studentNumber in studentNumbers) {
					if (checkTimeLimit && (DateTime.Now - startTime) >= allowedTimeSpan) {
						break;
					}
					if (SyncOneStudent(reportManager, reportStudentData, reportCoursesData, reportIdStudentData, reportIdCoursesData, batchDataSyncLogId, studentNumber)) {
						successfulStudentCount++;
					}
				}
			} catch (Exception ex) {
				errorMessage = ex.ToString();
				ClockWorkLogger.CWLogger.Logger.Error("CustomBatchDataSync:Failed:context={0}:err={1}", batchDataSyncLogId, errorMessage);
			}
			if (batchDataSyncLogId > 0) {
				IDataSyncDAO dataSyncDao = new DataSyncDAO(opContext);
				dataSyncDao.UpdateBatchSync(batchDataSyncLogId, successfulStudentCount, errorMessage);
			}
			// Unlike the real report-engine step (which returns no table at all), MainScript()
			// has to return something - so this returns a small summary row instead.
			DataTable result = new DataTable();
			result.Columns.Add("BatchDataSyncLogId", typeof(int));
			result.Columns.Add("AttemptedStudentCount", typeof(int));
			result.Columns.Add("SuccessfulStudentCount", typeof(int));
			result.Columns.Add("ErrorMessage", typeof(string));
			result.Rows.Add(batchDataSyncLogId, studentNumbers.Count, successfulStudentCount, errorMessage ?? "");
			return result;
		}
		private static bool SyncOneStudent(IReportManager reportManager, Report reportStudentData, Report reportCoursesData, int reportIdStudentData, int reportIdCoursesData, int batchDataSyncLogId, string studentNumber) {
			if (string.IsNullOrEmpty(studentNumber)) {
				return false;
			}
			ReportParameter pStudentNo1 = new ReportParameter { Name = "studentno", Value = studentNumber };
			ReportParameter pStudentNo2 = new ReportParameter { Name = "student_no", Value = studentNumber };
			ReportParameter pLogId = new ReportParameter { Name = "BatchDataSyncLogId", Value = batchDataSyncLogId.ToString() };
			bool? studentDataSucceeded = null;
			if (reportIdStudentData > 0) {
				try {
					RunReportResult result = reportStudentData != null
						? reportManager.ExecuteReport2(reportStudentData, null, null, pStudentNo1, pStudentNo2, pLogId)
						: reportManager.ExecuteReport2(reportIdStudentData, pStudentNo1, pStudentNo2, pLogId);
					studentDataSucceeded = result != null && result.ReportStatus != null && result.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully;
				} catch (Exception ex) {
					ClockWorkLogger.CWLogger.Logger.Error("CustomBatchDataSync:StudentData:snum={0}:error={1}", studentNumber, ex.ToString());
				}
			}
			if (reportIdCoursesData < 1) {
				return studentDataSucceeded == true;
			}
			try {
				RunReportResult result2 = reportCoursesData != null
					? reportManager.ExecuteReport2(reportCoursesData, null, null, pStudentNo1, pStudentNo2, pLogId)
					: reportManager.ExecuteReport2(reportIdCoursesData, pStudentNo1, pStudentNo2, pLogId);
				bool coursesSucceeded = result2 != null && result2.ReportStatus != null && result2.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully;
				return coursesSucceeded && (studentDataSucceeded == null || studentDataSucceeded == true);
			} catch (Exception ex2) {
				ClockWorkLogger.CWLogger.Logger.Error("CustomBatchDataSync:Courses:snum={0}:error={1}", studentNumber, ex2.ToString());
				return false;
			}
		}
/*******************************************
 * More boilerplate for all report scripts */
		public ReportReturnValue CustomEntry(ReportParameters args) {
			SetupVariables(args.Variables);
			if (args.Table != null) {
				InputTable = args.Table;
			}
			DataTable result;
			try {
				result = MainScript();
			} catch (Exception ex) {
				ClockWorkLogger.CWLogger.Logger.Error(ex.Message);
				result = new DataTable();
				result.Columns.Add("Error");
				result.Rows.Add(ex.Message);
				result.Rows.Add(ex.ToString());
			}
			return new ReportReturnValue(
				result,
				ReportVariables.Select(kvp => new ReportVariable(kvp.Key, kvp.Value)).ToList<ReportVariable>()
			);
		}
		public bool LoadCheckboxVariable(string name) {
			return LoadVariable<int?, bool>(name, x => x == 1);
		}
		public int LoadListVariable(string name) {
			return LoadVariable<int>(name);
		}
		public string LoadTextVariable(string name) {
			return LoadVariable<string, string>(name, s => (s == null) ? "" : s);
		}
		public int LoadIDVariable(string name) {
			return LoadVariable<string, int>(name, Int32.Parse);
		}
		// Optional variants - default instead of throwing when the variable isn't set,
		// matching how the real DataSyncBatchParameters treats an unconfigured value as zero/false.
		public int LoadOptionalIDVariable(string name, int defaultValue = 0) {
			if (!ReportVariables.ContainsKey(name) || ReportVariables[name] == null) {
				return defaultValue;
			}
			object value = ReportVariables[name];
			if (value is int) {
				return (int) value;
			}
			int parsed;
			return Int32.TryParse(value.ToString(), out parsed) ? parsed : defaultValue;
		}
		public bool LoadOptionalCheckboxVariable(string name, bool defaultValue = false) {
			if (!ReportVariables.ContainsKey(name) || ReportVariables[name] == null) {
				return defaultValue;
			}
			object value = ReportVariables[name];
			if (value is int) {
				return (int) value == 1;
			}
			if (value is bool) {
				return (bool) value;
			}
			bool parsed;
			return bool.TryParse(value.ToString(), out parsed) ? parsed : defaultValue;
}
		public T LoadVariable<T>(string name) {
			if (!ReportVariables.ContainsKey(name)) {
				throw new KeyNotFoundException(String.Format(
					"Report parameter '{0}' is missing",
					name
				));
			}
			try {
				return (T) ReportVariables[name];
			} catch (InvalidCastException) {
				throw new ArgumentException(String.Format(
					"Report parameter '{0}' expected type {1} but received {2}",
					name,
					typeof(T).Name,
					ReportVariables[name].GetType().Name
				));
			}
		}
		public TTo LoadVariable<TFrom, TTo>(string name, Func<TFrom, TTo> convert) {
			try {
				return convert(LoadVariable<TFrom>(name));
			} catch (FormatException ex) {
				throw new FormatException(String.Format(
					"Error converting report parameter '{0}' to {1}: {2}",
					name,
					typeof(TTo).Name,
					ex.Message
				));
			}
		}
		public void RequireColumn(string name) {
			if (InputTable == null) {
				throw new Exception("Script did not receive an input DataTable");
			}
			if (!InputTable.Columns.Contains(name)) {
				throw new ArgumentException(String.Format(
					"Required input table column missing: {0}",
					name
				));
			}
		}
		public void RequireColumn<T>(string name) {
			RequireColumn(name);
			Type received = InputTable.Columns[name].DataType;
			if (received != typeof(T)) {
				throw new ArgumentException(String.Format(
					"Input table column '{0}' must have type {1}  but received {2}",
					name,
					typeof(T).Name,
					received.Name
				));
			}
		}
		private void SetupVariables(IList<ReportVariable> variables) {
			ReportVariables = variables.ToDictionary(
				variable => variable.Name.Trim(),
				variable => variable.Value,
				StringComparer.InvariantCultureIgnoreCase
			);
		}
	}
}
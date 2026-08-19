using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using TechnoPro.Common.DynamicCompiler.CompilerArgs.Reports;
using TechnoPro.Common.DynamicCompiler.CompilerArgs;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Intake;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.DAO.DataSync;
using TechnoPro.Common.DAO.Impl.DataSync;
using Databases;
using ClockWorkLogger;

/****************************************************************************************
 * Rewrite of the daily "Batch Data Sync" job (report #5) for one school-year population.
 *
 * The stock job runs two sub-reports (#103 "Import student data", #104 "Import student
 * courses") once PER STUDENT, and each of those reports opens its own connection to the
 * RPT/Banner Oracle database and queries it for just that one student. For a few thousand
 * students that's a few thousand round trips to a different server, twice over, and it's
 * the reason the job takes hours.
 *
 * This script does the same two things - sync student info, sync course enrollments - but
 * pulls RPT data for EVERYONE in one or two bulk Oracle queries, then writes it all to ClockWork in as
 * few passes as the underlying write methods allow. It calls the exact same ClockWork
 * methods the real reports call - it does not reimplement the sync logic itself.
 *
 * It still needs to run AFTER two setup steps in the report definition, same as reports
 * #103/#104 do, because it relies on them handing it an already-decrypted RPT connection
 * string in its input table:
 *   Step 1 (Sql_Query):   select settingstringvalue AS connectionstring from websettings2 where settingcode=10000000
 *   Step 2 (Decrypt_Data): parameter = connectionstring
 *   Step 3 (Execute_CSharp): this script
 ****************************************************************************************/

namespace ClockWorkDynamicCSharp
{
	public class CSharp
	{
		// --- Boilerplate every "Execute C#" report step needs ---
		// _t is whatever the PREVIOUS report step (step 2, Decrypt_Data) produced - here,
		// a one-row table with the RPT connection string already decrypted in it.
		private ReportParameters _args;
		private DataTable _t { get { return _args == null ? null : _args.Table; } }
		private IList<ReportVariable> _variables { get { return _args == null || _args.Variables == null ? new List<ReportVariable>() : _args.Variables; } }

		// A second connection, to ClockWork's OWN database (not the RPT/Oracle one), used
		// for looking up which students are active and for decrypting their student numbers.
		private static readonly OperationContext Context = new OperationContext();
		private static readonly DatabaseLayer Db = DatabaseLayerFactory.GetDatabaseLayer(
			eDatabaseConnectionStringName.ClockWork, Context.TenantId
			);

		// Decodes the single-letter meeting-day codes RPT sends (m/t/w/r/f/s/u) into the
		// day names ClockWork's timetable expects.
		private static readonly Dictionary<char, string> DayCodes = new Dictionary<char, string> {
				{ 'm', "mon" }, { 't', "tue" }, { 'w', "wed" }, { 'r', "thu" },
				{ 'f', "fri" }, { 's', "sat" }, { 'u', "sun" }
				};

		// This is the SAME list you'd see in report #103's "Import student data" step -
		// it maps a ClockWork control ID (the numeric ID of a field on a student's screen,
		// e.g. a text box or date field) to the column name it should come from in the RPT
		// data. Copy this straight from that report if it's ever changed there.
		private static readonly Dictionary<string, string> ControlIdVariableToRptColumn = new Dictionary<string, string> {
				{ "NetlinkIdControlId", "NETLINK_ID" },
				{ "BirthdateControlId", "BIRTHDATE" },
				{ "PhoneNumberControlId", "PHONE_NUMBER" },
				{ "CellNumberControlId", "CELL_NUMBER" },
				{ "UvicEmailControlId", "UVIC_EMAIL" },
				{ "TermCodeControlId", "TERM_CODE" },
				{ "ProgramControlId", "PROGRAM" },
				{ "FacultyControlId", "FACULTY" },
				{ "LevelCodeControlId", "LEVEL_CODE" },
				{ "FullPartTimeCodeControlId", "FULL_PART_TIME_CODE" },
				{ "AcadStdgTermCodeControlId", "ACAD_STDG_TERM_CODE" },
				{ "AcadStdgLevlCodeControlId", "ACAD_STDG_LEVL_CODE" },
				{ "AcadStdgDescControlId", "ACAD_STDG_DESC" },
				{ "AcadStdgDateControlId", "ACAD_STDG_DATE" },
				{ "AcadStdgOvrDescControlId", "ACAD_STDG_OVR_DESC" },
				{ "AcadStdgOvrDateControlId", "ACAD_STDG_OVR_DATE" },
				{ "TimeSlotDateControlId", "TIME_SLOT_DATE" },
				{ "TimeSlotTimeControlId", "TIME_SLOT_TIME" },
				{ "LastDataSyncControlId", "LastDataSync" },
				{ "StudentStatusControlId", "STUDENT_STATUS" },
				{ "LegalFirstNameControlId", "LEGAL_FIRST_NAME" }
				};

		public ReportReturnValue CustomEntry(ReportParameters args)
		{
			_args = args;

			// The output table this script hands back when the report finishes. One row,
			// giving you a summary of what happened - not the individual student records.
			DataTable result = new DataTable();
			result.Columns.Add("StudentCount", typeof(int));       // how many active students it attempted
			result.Columns.Add("CourseRowCount", typeof(int));     // how many raw course rows came back from Oracle
			result.Columns.Add("SuccessCount", typeof(int));       // students that made it through with no errors
			result.Columns.Add("FailedStudentCount", typeof(int)); // students that hit an error somewhere
			result.Columns.Add("DryRun", typeof(bool));
			result.Columns.Add("OraclePullMs", typeof(long));      // time spent talking to Oracle
			result.Columns.Add("ImportStudentsMs", typeof(long));  // time spent syncing student info
			result.Columns.Add("CourseSyncMs", typeof(long));      // time spent syncing courses
			result.Columns.Add("TotalMs", typeof(long));
			result.Columns.Add("BatchDataSyncLogId", typeof(int)); // the ID this run gets in ClockWork's own sync history
			result.Columns.Add("ErrorMessage", typeof(string));

			// Two report variables control safe testing:
			//   DryRun (checkbox, default ON)     - wraps writes in a transaction that's
			//                                        never committed, so nothing actually
			//                                        sticks. Turn this off to do a real run.
			//   MaxStudents (number, default 25)  - only process this many students. Set
			//                                        to 0 to run against everyone.
			bool dryRun = GetBoolVariable("DryRun", true);
			int maxStudents = GetIntVariable("MaxStudents", 25);

			string errorMessage = null;
			int studentCount = 0, courseRowCount = 0, batchDataSyncLogId = 0;
			long pullMs = 0, importMs = 0, courseMs = 0;

			// Every student number that hit an error anywhere in the run (intake, data, or
			// courses) lands in here. Doesn't matter WHERE it failed - if it's in this set,
			// that student didn't fully sync this run.
			HashSet<string> failedStudents = new HashSet<string>();

			try
			{
				// Sanity check on the input from steps 1+2. If someone runs this script by
				// itself, or those steps get reordered/removed, this fails clearly instead
				// of with a confusing null-reference error.
				if (_t == null || _t.Rows.Count < 1 || !_t.Columns.Contains("connectionstring"))
					throw new Exception("InputTable is empty or missing 'connectionstring' - confirm Sql_Query + Decrypt_Data steps ran before this one and produced that column.");

				string connectionString = _t.Rows[0]["connectionstring"].ToString();
				
				string controlMapping = BuildControlMappingFromVariables();
				if (string.IsNullOrEmpty(controlMapping))
					throw new Exception("No *ControlId report variables are set - add at least one in the Set_Variables step.");
                /*--------------------------------------------------------------------
                 * BLOCK 1: Who are we syncing?
                 * This is the exact same query the real "Batch Data Sync" report (#5)
                 * uses to build its student list: everyone in the "active students"
                 * group (peoplegroups.groupid=1) who's flagged active. Student numbers
                 * are stored encrypted in ClockWork's people table, so each one gets
                 * decrypted here before we can use it to query Oracle.
                 *------------------------------------------------------------------*/
				DataTable activePeople = Db.ExecuteQuery(
					"SELECT p.student_no FROM people AS p " +
					"INNER JOIN peoplegroups AS pg ON pg.personid = p.personid " +
					"WHERE pg.groupid = 1 AND p.isactive = 1"
					);

				List<string> studentNumbers = activePeople.Rows.Cast<DataRow>()
					.Select(dr => Decrypt((byte[]) dr["student_no"]))
					.Where(s => !string.IsNullOrEmpty(s))
					.Select(s => s.Trim().ToUpper())
					.ToList();

				if (maxStudents > 0)
					studentNumbers = studentNumbers.Take(maxStudents).ToList();

				studentCount = studentNumbers.Count;

                /*--------------------------------------------------------------------
                 * BLOCK 2: Open a batch log entry.
                 * Every real sync run gets a row in ClockWork's batch sync log, which
                 * is how staff can see "when did this last run, how many students,
                 * did it error." We create one here too, and fill in the final count
                 * at the very end - same as a real run would.
                 *------------------------------------------------------------------*/
				DataSyncOperationContext opContext = new DataSyncOperationContext { WhoAmI = Context.WhoAmI };
				IDataSyncDAO dataSyncDao = new DataSyncDAO(opContext);
				batchDataSyncLogId = dataSyncDao.GetNewBatchDataSyncLogId(studentCount);
				opContext.BatchDataSyncLogId = batchDataSyncLogId;

               /*--------------------------------------------------------------------
                * BLOCK 3: Pull RPT data for everyone, in bulk.
                * These two views are built around a single-student WHERE clause (that's
                * how reports #103/#104 always call them) - pulling them unconditionally
                * is dramatically slower, likely because it scans far more than just the
                * active population. So instead of one connection per student, we open
                * ONE Oracle connection per batch of ~500 students and pull BOTH views
                * through it, filtered by student_number, before moving to the next
                * batch. 500 is comfortably under Oracle's ~1000-item IN-list limit.
                *------------------------------------------------------------------*/
                Stopwatch pullTimer = Stopwatch.StartNew();
                DataTable rawStudentData = new DataTable("rawStudentData");
                DataTable rawCourseData = new DataTable("rawCourseData");
                const int ChunkSize = 500;
                for (int i = 0; i < studentNumbers.Count; i += ChunkSize)
                {
                    List<string> chunk = studentNumbers.Skip(i).Take(ChunkSize).ToList();
                    using (Oracle.DataAccess.Client.OracleConnection connection = new Oracle.DataAccess.Client.OracleConnection(connectionString))
                    {
                        connection.Open();

                        DataTable studentChunk = QueryOracle(connection, "v_stu_clockwork_pers_prog_inf", chunk);
                        if (rawStudentData.Columns.Count == 0 && studentChunk.Columns.Count > 0)
                            rawStudentData = studentChunk.Clone();
                        foreach (DataRow dr in studentChunk.Rows)
                            rawStudentData.ImportRow(dr);

                        DataTable courseChunk = QueryOracle(connection, "v_stu_clockwork_crse_schd_inf", chunk);
                        if (rawCourseData.Columns.Count == 0 && courseChunk.Columns.Count > 0)
                            rawCourseData = courseChunk.Clone();
                        foreach (DataRow dr in courseChunk.Rows)
                            rawCourseData.ImportRow(dr);
                    }
                }
                pullTimer.Stop();
                pullMs = pullTimer.ElapsedMilliseconds;
                courseRowCount = rawCourseData.Rows.Count;


                /*--------------------------------------------------------------------
                 * BLOCK 4: Sync student info (report #103's job).
                 * First we reshape the raw Oracle columns into what ClockWork expects
                 * - same column renames, preferred-name handling, etc. that report
                 * #103's own steps do. Then we hand the WHOLE table to ClockWork's
                 * ImportStudents method in one call, which is what actually writes
                 * checkbox/dropdown/textbox/date values onto each student's record,
                 * only touching fields that have actually changed.
                 *------------------------------------------------------------------*/
				DataTable transformedStudentData = TransformRawStudentRows(rawStudentData);
				Stopwatch importTimer = Stopwatch.StartNew();
				RunImportStudentsWithFallback(transformedStudentData, controlMapping, dryRun, failedStudents);
				importTimer.Stop();
				importMs = importTimer.ElapsedMilliseconds;

                /*--------------------------------------------------------------------
                 * BLOCK 5: Sync courses (report #104's job).
                 * Same idea as above - reshape the raw Oracle course rows (this is
                 * where instructor names get assembled, meeting times get formatted,
                 * and each course gets split into one row per meeting day). Course
                 * syncing has to run one student at a time (ClockWork doesn't have a
                 * bulk version of this one), so we group the rows back by student and
                 * loop - but each student is wrapped in its own try/catch, so one
                 * student with bad data can't stop everyone else's courses from syncing.
                 *------------------------------------------------------------------*/
				DataTable transformedCourseData = TransformRawCourseRows(rawCourseData);
				List<IGrouping<string, DataRow>> studentGroups = transformedCourseData.Rows.Cast<DataRow>()
					.GroupBy(dr => dr["student_no"].ToString().Trim().ToUpper())
					.ToList();

				IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(opContext);

				Stopwatch courseTimer = Stopwatch.StartNew();
				foreach (IGrouping<string, DataRow> group in studentGroups)
				{
					DataTable studentTable = transformedCourseData.Clone();
					foreach (DataRow dr in group)
						studentTable.ImportRow(dr);

					try
					{
						if (dryRun)
						{
							using (TransactionScope scope = new TransactionScope())
							{
								RunOneStudentCourses(dataSyncCourseManager, group.Key, studentTable);
								// scope.Complete() is never called - everything this student's
								// course sync wrote gets rolled back automatically.
							}
						}
						else
						{
							RunOneStudentCourses(dataSyncCourseManager, group.Key, studentTable);
						}
					}
					catch (Exception ex)
					{
						// This one student's courses failed - log it and move on to the
						// next student instead of aborting the whole run.
						failedStudents.Add(group.Key);
						CWLogger.Logger.Error("TimeCombinedSync:Courses:snum={0}:error={1}", group.Key, ex.ToString());
					}
				}
				courseTimer.Stop();
				courseMs = courseTimer.ElapsedMilliseconds;
			}
			catch (Exception ex)
			{
				// Anything that escapes all of the above (a bad Oracle connection string,
				// the active-student query failing, etc.) lands here. Whatever numbers we
				// did manage to collect before the failure still get reported below.
				errorMessage = ex.ToString();
				CWLogger.Logger.Error("TimeCombinedSync:Failed:err={0}", errorMessage);
			}

			int successCount = studentCount - failedStudents.Count;

            /*--------------------------------------------------------------------
             * BLOCK 6: Close out the batch log entry.
             * Only does this on a real (non-dry-run) run - logging a "successful"
             * batch sync entry for a run that got rolled back would be misleading
             * in ClockWork's own sync history.
             *------------------------------------------------------------------*/
			if (batchDataSyncLogId > 0 && !dryRun)
			{
				try
				{
					DataSyncOperationContext logContext = new DataSyncOperationContext { WhoAmI = Context.WhoAmI };
					IDataSyncDAO logDao = new DataSyncDAO(logContext);
					logDao.UpdateBatchSync(batchDataSyncLogId, successCount, errorMessage);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("TimeCombinedSync:UpdateBatchSync:Failed:err={0}", ex.ToString());
				}
			}

			result.Rows.Add(studentCount, courseRowCount, successCount, failedStudents.Count, dryRun,
				pullMs, importMs, courseMs, pullMs + importMs + courseMs, batchDataSyncLogId, errorMessage ?? "");
			return new ReportReturnValue { Table = result, VariablesOut = _variables };
		}

		// Handles the intake-data side effect described in Block 4 above. Runs once per
		// student, each isolated in its own try/catch - a failure here only affects that
		// one student, never blocks their actual data/course sync, and never stops the loop.
		private void RunIntakeSyncForAllStudents(List<string> studentNumbers, DataSyncOperationContext opContext, bool dryRun, HashSet<string> failedStudents)
		{
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(opContext);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(opContext.WhoAmI, eSettingCode.SETTING_Intake_MultiDepartmentIntakeSettings);
			MultiDepartmentIntakeSettings multiDepartmentIntakeSettings = (settingValue_String ?? "").DeserializeMultiDepartmentIntakeSettings();
			bool multiDeptEnabled = multiDepartmentIntakeSettings != null && multiDepartmentIntakeSettings.IsEnabled;
			if (multiDeptEnabled)
				return; // Multi-department intake handles this itself - skip auto-sync entirely.

			bool autoSyncDisabled = oldUserSettingManager.GetSettingValue_Bool(opContext.WhoAmI, eSettingCode.SETTING_Intake_DisableAutoIntakeDataSync);
			if (autoSyncDisabled)
				return; // Admin has explicitly turned this off.

			IDataSyncDataManager dataSyncDataManager = new DataSyncDataManager(opContext);
			foreach (string studentNumber in studentNumbers)
			{
				try
				{
					if (dryRun)
					{
						using (TransactionScope scope = new TransactionScope())
						{
							dataSyncDataManager.DataSyncIntakeData(studentNumber, true);
						}
					}
					else
					{
						dataSyncDataManager.DataSyncIntakeData(studentNumber, true);
					}
				}
				catch (Exception ex)
				{
					failedStudents.Add(studentNumber);
					CWLogger.Logger.Error("TimeCombinedSync:Intake:snum={0}:error={1}", studentNumber, ex.ToString());
				}
			}
		}

		// Syncs student info for everyone in one call when possible (fast path - avoids
		// re-checking the control-mapping list's field types for every single student).
		// If that one call fails for ANY reason, we don't want to lose everyone's sync
		// over one bad row, so we fall back to doing it one student at a time instead,
		// each isolated - matching how the real per-student report would behave.
		private void RunImportStudentsWithFallback(DataTable transformedStudentData, string controlMapping, bool dryRun, HashSet<string> failedStudents)
		{
			try
			{
				if (dryRun)
				{
					using (TransactionScope scope = new TransactionScope())
					{
						TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.ImportStudents(
							transformedStudentData.DefaultView, controlMapping, true, Context
							);
					}
				}
				else
				{
					TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.ImportStudents(
						transformedStudentData.DefaultView, controlMapping, true, Context
						);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("TimeCombinedSync:ImportStudents:BulkFailed:falling back to per-student:err={0}", ex.ToString());

				var byStudent = transformedStudentData.Rows.Cast<DataRow>()
					.GroupBy(dr => dr["student_no"].ToString().Trim().ToUpper());

				foreach (var group in byStudent)
				{
					DataTable studentTable = transformedStudentData.Clone();
					foreach (DataRow dr in group)
						studentTable.ImportRow(dr);

					try
					{
						if (dryRun)
						{
							using (TransactionScope scope = new TransactionScope())
							{
								TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.ImportStudents(
									studentTable.DefaultView, controlMapping, true, Context
									);
							}
						}
						else
						{
							TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.ImportStudents(
								studentTable.DefaultView, controlMapping, true, Context
								);
						}
					}
					catch (Exception ex2)
					{
						failedStudents.Add(group.Key);
						CWLogger.Logger.Error("TimeCombinedSync:ImportStudents:snum={0}:error={1}", group.Key, ex2.ToString());
					}
				}
			}
		
		}

		// The three-step call that actually syncs one student's courses: turn the raw
		// rows into course objects, match them up against ClockWork's course catalogue,
		// then write whatever's changed (new enrollments, dropped courses, instructor or
		// timetable changes). This is the exact same sequence report #104's course-sync
		// step runs.
		private static int RunOneStudentCourses(IDataSyncCourseManager dataSyncCourseManager, string studentNumber, DataTable studentTable)
		{
			List<DataSyncExternalCourseRowPart> rowParts = dataSyncCourseManager.GetRowPartsFromDataTable(studentTable);
			List<DataSyncExternalCourse> externalCourses = dataSyncCourseManager.ParseExternalCourseRowParts(rowParts);
			List<DataSyncExternalCourseSyncResult> results = dataSyncCourseManager.DataSyncCourses(studentNumber, externalCourses);
			return results != null ? results.Count : 0;
		}

		// Reshapes the raw student-info columns from Oracle into what ClockWork's control
		// mapping expects: renames a few columns to ClockWork's naming convention, stamps
		// today's date as the "last synced" value, and swaps in a student's preferred
		// first name (if they have one set) while keeping their legal name in a separate
		// column. Matches report #103's own transform step exactly.
		private DataTable TransformRawStudentRows(DataTable raw)
		{
			DataTable t2 = raw.Copy();
			if (!t2.Columns.Contains("LEGAL_FIRST_NAME")) t2.Columns.Add("LEGAL_FIRST_NAME", typeof(string));
			if (!t2.Columns.Contains("lastdatasync")) t2.Columns.Add("lastdatasync", typeof(DateTime));
			DateTime now = DateTime.Now;
			foreach (DataRow dr in t2.Rows)
			{
				dr["lastdatasync"] = now;
				dr["LEGAL_FIRST_NAME"] = dr["FIRST_NAME"];
				if (!DBNull.Value.Equals(dr["SPBPERS_PREF_FIRST_NAME"]))
					dr["FIRST_NAME"] = dr["SPBPERS_PREF_FIRST_NAME"];
			}
			foreach (string r in "student_number=student_no,first_name=firstname,middle_name=middlename,last_name=lastname".Split(','))
			{
				int ind = r.IndexOf("=");
				string oldName = r.Substring(0, ind), newName = r.Substring(ind + 1);
				if (t2.Columns.Contains(oldName)) t2.Columns[oldName].ColumnName = newName;
			}
			return t2;
		}

		// Reshapes the raw course-schedule columns from Oracle: renames columns to
		// ClockWork's naming, builds a display-ready instructor name and room string,
		// reformats meeting times from "1330" to "13:30", flags the primary instructor,
		// and - this is the important part - turns one Oracle row covering multiple
		// meeting days (e.g. "MWF") into several rows, one per day, since that's the
		// shape ClockWork's timetable data needs. Matches report #104's own transform.
		private DataTable TransformRawCourseRows(DataTable raw)
		{
			DataTable tt1 = raw.Copy();
			foreach (string mapping in new[] {
					"student_number=student_no", "course_start_date=startdate", "course_end_date=enddate",
					"term_code=term", "course_number=course", "section_number=section", "class_type=timeofday",
					"instructor_netlink_id=instructorusername", "instructor_phone_number=instructorphone",
					"instructor_email=instructoremail"
					})
			{
				int ind = mapping.IndexOf("=");
				string oldName = mapping.Substring(0, ind), newName = mapping.Substring(ind + 1);
				if (tt1.Columns.Contains(oldName)) tt1.Columns[oldName].ColumnName = newName;
			}
			tt1.Columns.Add("instructorname");
			tt1.Columns.Add("timetableroom");
			tt1.Columns.Add("starttime");
			tt1.Columns.Add("endtime");
			tt1.Columns.Add("dayofweek");
			tt1.Columns.Add("originalemail");
			tt1.Columns.Add("instructorisprimary");

			DataTable q = tt1.Clone();

			foreach (DataRow dr in tt1.Rows)
			{
				// Non-uvic.ca instructor emails get swapped for a generated uvic.ca one,
				// with the original kept aside - same substitution the real report does.
				if (dr["instructoremail"] is string && !((string) dr["instructoremail"]).ToLower().EndsWith("uvic.ca"))
				{
					dr["originalemail"] = dr["instructoremail"];
					dr["instructoremail"] = dr["instructorusername"].ToString() + "@uvic.ca";
				}

				string ifirst = dr["instructor_first_name"].ToString().Trim();
				string imiddle = dr["instructor_middle_name"].ToString().Trim();
				string ilast = dr["instructor_last_name"].ToString().Trim();
				if (imiddle.Length > 0) ifirst = string.Concat(ifirst, " ", imiddle);
				dr["instructorname"] = ifirst.Length > 0 ? string.Concat(ifirst, " ", ilast).Trim() : ilast;

				string bldg = dr["bldg_name"].ToString().Trim();
				string room = dr["meet_room"].ToString().Trim();
				dr["timetableroom"] = bldg.Length > 0 ? string.Concat(bldg, " ", room).Trim() : room;

				string sts = dr["meet_begin_time"].ToString().Trim();
				string ets = dr["meet_end_time"].ToString().Trim();
				if (sts.Length == 4) sts = string.Concat(sts.Substring(0, 2), ":", sts.Substring(2));
				if (ets.Length == 4) ets = string.Concat(ets.Substring(0, 2), ":", ets.Substring(2));
				dr["starttime"] = sts;
				dr["endtime"] = ets;

				dr["instructorisprimary"] = dr["PRIMARY_INSTRUCTOR"].ToString().ToUpper().Equals("PRIMARY");

				// One row per meeting day - a course that meets Mon/Wed/Fri becomes three
				// rows here, each identical except for "dayofweek". If a course somehow
				// has no recognized meeting days, keep it as a single row anyway so it
				// doesn't just vanish.
				bool addedAtLeastOne = false;
				string tods = dr["meet_days"].ToString().Trim().ToLower();
				foreach (char c in tods)
				{
					if (DayCodes.ContainsKey(c))
					{
						dr["dayofweek"] = DayCodes[c];
						q.ImportRow(dr);
						addedAtLeastOne = true;
					}
				}
				if (!addedAtLeastOne) q.ImportRow(dr);
			}

			return q;
		}

		private static DataTable QueryOracle(Oracle.DataAccess.Client.OracleConnection connection, string viewName, List<string> studentNumbers)
        {
            DataTable result = new DataTable();
            if (studentNumbers.Count == 0) return result;

            List<string> paramNames = studentNumbers.Select((s, idx) => "p_id_" + idx).ToList();
            string inClause = string.Join(",", paramNames.Select(p => ":" + p));
            string sql = "SELECT * FROM uvicrpt." + viewName + " WHERE student_number IN (" + inClause + ")";

            using (Oracle.DataAccess.Client.OracleCommand command = new Oracle.DataAccess.Client.OracleCommand(sql, connection))
            {
                for (int i = 0; i < studentNumbers.Count; i++)
                    command.Parameters.Add(paramNames[i], Oracle.DataAccess.Client.OracleDbType.Varchar2, studentNumbers[i], System.Data.ParameterDirection.Input);
                using (var reader = command.ExecuteReader())
                    result.Load(reader);
            }
            return result;
        }

		// Keeps only the rows whose student number column is in the active-student set -
		// this is where "everyone from RPT" gets narrowed down to "just our active
		// students," now that the Oracle side isn't filtering for us.
		private static DataTable FilterToActiveStudents(DataTable raw, string studentNumberColumn, HashSet<string> activeSet)
		{
			DataTable result = raw.Clone();
			foreach (DataRow dr in raw.Rows)
			{
				string snum = dr[studentNumberColumn].ToString().Trim().ToUpper();
				if (activeSet.Contains(snum))
					result.ImportRow(dr);
			}
			return result;
		}
		
		// Rebuilds the ImportStudents control-mapping string from whichever *ControlId
		// variables are actually set right now - add, remove, or repoint any of them in the
		// Set_Variables step and this just reflects it, no code change needed.
		private string BuildControlMappingFromVariables()
		{
			List<string> entries = new List<string>();
			foreach (KeyValuePair<string, string> kvp in ControlIdVariableToRptColumn)
			{
				int? controlId = GetOptionalIntVariable(kvp.Key);
				if (controlId.HasValue)
					entries.Add(controlId.Value + "=" + kvp.Value);
			}
			return string.Join("\r\n", entries);
		}

		private int? GetOptionalIntVariable(string name)
		{
			var v = _variables.FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
			if (v == null || v.Value == null) return null;
			int parsed;
			return int.TryParse(v.Value.ToString(), out parsed) ? (int?) parsed : null;
		}
		
		// Decrypts a value that ClockWork stored encrypted (student numbers, in this
		// script's case) using ClockWork's own standard encryption key.
		private static string Decrypt(byte[] encrypted)
		{
			return encrypted == null ? null : Db.Encryption.Decrypt(encrypted);
		}

		// Reads a checkbox-style report variable (DryRun), tolerant of however ClockWork
		// happens to hand it back (as a 1/0 int, a real bool, or missing entirely).
		private bool GetBoolVariable(string name, bool defaultValue)
		{
			var v = _variables.FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
			if (v == null || v.Value == null) return defaultValue;
			if (v.Value is int) return (int) v.Value == 1;
			if (v.Value is bool) return (bool) v.Value;
			bool parsed;
			return bool.TryParse(v.Value.ToString(), out parsed) ? parsed : defaultValue;
		}

		// Reads a numeric report variable (MaxStudents), same tolerance for missing values.
		private int GetIntVariable(string name, int defaultValue)
		{
			var v = _variables.FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
			if (v == null || v.Value == null) return defaultValue;
			int parsed;
			return int.TryParse(v.Value.ToString(), out parsed) ? parsed : defaultValue;
		}
	}
}
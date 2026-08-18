using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers
{
	// Token: 0x02000007 RID: 7
	public class DataSyncReports : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002050 File Offset: 0x00000250
		public DataSyncReports()
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000030C4 File Offset: 0x000012C4
		public DataSyncReports(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000030D6 File Offset: 0x000012D6
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000030DE File Offset: 0x000012DE
		public OperationContext OpContext { get; set; }

		// Token: 0x06000024 RID: 36 RVA: 0x000030E8 File Offset: 0x000012E8
		public AuditResult ExecuteAudit()
		{
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_DataSync_BatchImportReportId);
			int settingValue_Int2 = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_ReportNumberToRunForPreviewingStudentsFromExternalDatabase);
			int settingValue_Int3 = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_ReportNumberToRunForImportingStudentsFromExternalDatabase);
			int settingValue_Int4 = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_ReportNumberToRunForImportingStudentCourses);
			int settingValue = SettingManager.CurrentInstance.GetSettingValue<int>(Setting.NOTETAKINGB_ReportIdToRetreiveNotetakerStudentNumberFromUsername);
			return new AuditResult
			{
				Checks = new List<AuditCheck>
				{
					this.CheckDataSyncReport("Preview data", settingValue_Int2, new ReportParameter[]
					{
						new ReportParameter
						{
							Name = "studentno",
							Value = "1234567"
						}
					}, new string[]
					{
						"student_no",
						"firstname",
						"lastname",
						"paddress",
						"address",
						"email",
						"phone1",
						"phone2"
					}, new string[]
					{
						"middlename"
					}),
					this.CheckDataSyncReport("Import data", settingValue_Int3, new ReportParameter[]
					{
						new ReportParameter
						{
							Name = "studentno",
							Value = "1234567"
						}
					}, new string[]
					{
						"student_no",
						"firstname",
						"lastname"
					}, new string[]
					{
						"lastdatasync",
						"middlename"
					}),
					this.CheckDataSyncReport("Import courses", settingValue_Int4, new ReportParameter[]
					{
						new ReportParameter
						{
							Name = "studentno",
							Value = "1234567"
						}
					}, new string[]
					{
						"student_no",
						"term",
						"subject",
						"course",
						"section",
						"timeofday",
						"startdate",
						"enddate"
					}, new string[]
					{
						"instructorname",
						"instructoremail",
						"instructorusername",
						"starttime",
						"endtime",
						"dayofweek",
						"duration"
					}),
					this.CheckDataSyncReport("Get student number from username report", settingValue, new ReportParameter[]
					{
						new ReportParameter
						{
							Name = "username",
							Value = "technopro"
						}
					}, new string[]
					{
						"student_no"
					}, null),
					this.CheckDataSyncReport("Batch data sync", settingValue_Int, new ReportParameter[0], new string[]
					{
						"student_no",
						"firstname",
						"lastname"
					}, new string[]
					{
						"middlename"
					})
				}
			};
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000033DC File Offset: 0x000015DC
		private AuditCheck CheckDataSyncReport(string title, int rid, ReportParameter[] parameters, string[] failIfMissingColumns, string[] warningIfMissingColumns)
		{
			bool flag = rid < 1;
			AuditCheck result;
			if (flag)
			{
				result = new AuditCheck(title, eAuditStatus.Failed, new string[]
				{
					"Setting is missing"
				});
			}
			else
			{
				IReportManager reportManager = new ReportManager(this.OpContext);
				RunReportResult runReportResult = reportManager.ExecuteReport2(rid, new List<eFunctionType>
				{
					eFunctionType.Data_Sync_Update_All_Students,
					eFunctionType.Data_Sync_Courses_2,
					eFunctionType.Import_User_Data
				}, parameters);
				DataTable t = (runReportResult == null || runReportResult.PrimaryData == null) ? null : runReportResult.PrimaryData.Table;
				List<string> list = (t != null && failIfMissingColumns != null) ? (from g in failIfMissingColumns
				where !t.Columns.Contains(g)
				select g).ToList<string>() : new List<string>();
				List<string> list2 = (t != null && warningIfMissingColumns != null) ? (from g in warningIfMissingColumns
				where !t.Columns.Contains(g)
				select g).ToList<string>() : new List<string>();
				bool flag2 = t == null || t.Columns.Count < 1 || list.Count > 0;
				if (flag2)
				{
					result = new AuditCheck(title, eAuditStatus.Failed, new string[]
					{
						"Report failed:status={0}:missingColumns={1}:msg={2}",
						(runReportResult == null || runReportResult.ReportStatus == null) ? "NULL" : runReportResult.ReportStatus.LastStatusStep.ToString(),
						string.Join(",", list.ToArray()),
						(runReportResult == null || runReportResult.ReportStatus == null) ? "NULL" : (runReportResult.ReportStatus.ErrorMessage ?? "")
					});
				}
				else
				{
					bool flag3 = list2.Count > 0;
					if (flag3)
					{
						result = new AuditCheck(title, eAuditStatus.CompletedSuccessfulWithWarnings, new string[]
						{
							"Report successful with warning(s):warningMissingColumns={0}",
							string.Join(",", list2.ToArray())
						});
					}
					else
					{
						eAuditStatus status = eAuditStatus.CompletedSuccessful;
						string[] array = new string[2];
						array[0] = "Success:Columns={0}";
						array[1] = string.Join(",", (from DataColumn dc in t.Columns
						select dc.ColumnName).ToArray<string>());
						result = new AuditCheck(title, status, array);
					}
				}
			}
			return result;
		}
	}
}

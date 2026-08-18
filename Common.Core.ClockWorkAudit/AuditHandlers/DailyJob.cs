using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.ClockWorkDailyJob;
using TechnoPro.Common.Core.Jobs;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.ICore.ClockWorkDailyJob;
using TechnoPro.Common.ICore.ClockWorkServerJob;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Public.Entities.ClockWorkDailyJob;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers
{
	// Token: 0x02000005 RID: 5
	public class DailyJob : IClockWorkAuditHandler, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002050 File Offset: 0x00000250
		public DailyJob()
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000273F File Offset: 0x0000093F
		public DailyJob(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002751 File Offset: 0x00000951
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002759 File Offset: 0x00000959
		public OperationContext OpContext { get; set; }

		// Token: 0x06000017 RID: 23 RVA: 0x00002764 File Offset: 0x00000964
		public AuditResult ExecuteAudit()
		{
			IMiscTableSettingsManager miscTableSettingsManager = new MiscTableSettingsManager(this.OpContext);
			string text = (miscTableSettingsManager.LoadMiscSettingValue(1340) ?? "").Trim().ToLower();
			bool flag = text.Length > 0 && "1yestrue".IndexOf(text) >= 0;
			AuditResult auditResult = new AuditResult(eClockWorkAuditType.DailyJob);
			bool flag2 = flag;
			if (flag2)
			{
				auditResult.Checks.Add(new AuditCheck("Check daily job version", eAuditStatus.CompletedSuccessful, new string[]
				{
					"Using new server job system"
				}));
				IClockWorkServerJobManager clockWorkServerJobManager = new ClockWorkServerJobManager(new ClockWorkServerOperationContext
				{
					ClockWorkServerInstanceName = eClockWorkServerInstanceName.ClockWorkServer,
					ClockWorkServerVirtualDirectory = "ClockWorkServer",
					AppContext = this.OpContext.AppContext,
					WhoAmI = this.OpContext.WhoAmI
				});
				IList<ClockWorkServerJobInfo> activeClockWorkServerJobs = clockWorkServerJobManager.GetActiveClockWorkServerJobs();
				IEnumerable<ClockWorkServerJobStep> source = from g in activeClockWorkServerJobs
				select (g.JobSteps ?? new List<ClockWorkServerJobStep>()).FirstOrDefault((ClockWorkServerJobStep h) => h.JobType == "ClockWorkServerBatchDataSyncJob");
				ClockWorkServerJobStep clockWorkServerJobStep = source.FirstOrDefault<ClockWorkServerJobStep>();
				auditResult.Checks.Add((clockWorkServerJobStep == null) ? new AuditCheck("Check batch data sync job", eAuditStatus.Failed, new string[]
				{
					"No batch data sync job present."
				}) : this.CheckServerJobRanRecently(clockWorkServerJobManager, clockWorkServerJobStep, "Check batch data sync job"));
				List<ClockWorkServerJobStep> list = (from g in activeClockWorkServerJobs
				select (g.JobSteps ?? new List<ClockWorkServerJobStep>()).FirstOrDefault((ClockWorkServerJobStep h) => h.JobType == "ClockWorkServerEmailReminderJob")).ToList<ClockWorkServerJobStep>();
				int count = list.Count;
				auditResult.Checks.Add((count < 3) ? new AuditCheck("Check for 3 email reminder reports (should be test/midterm/appt reminders)", eAuditStatus.Failed, new string[]
				{
					"Too few email reminder jobs present: count present={0}",
					count.ToString()
				}) : new AuditCheck("Check for 3 email reminder reports (should be test/midterm/appt reminders)", eAuditStatus.CompletedSuccessful, new string[]
				{
					"Email reminder jobs present: count={0}",
					count.ToString()
				}));
				foreach (ClockWorkServerJobStep clockWorkServerJobStep2 in list)
				{
					auditResult.Checks.Add(this.CheckServerJobRanRecently(clockWorkServerJobManager, clockWorkServerJobStep2, string.Format("Check email reminder job: {0}", clockWorkServerJobStep2.Title ?? "")));
				}
			}
			else
			{
				auditResult.Checks.Add(new AuditCheck("Check daily job version", eAuditStatus.Failed, new string[]
				{
					"Using old Windows Scheduled Task job system"
				}));
				IDailyJobManager dailyJobManager = new DailyJobManager(this.OpContext);
				List<DailyJobTask> source2 = (from g in dailyJobManager.LoadDailyJobTasks()
				where g.IsActive
				select g).ToList<DailyJobTask>();
				IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				int batchImportReportId = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_DataSync_BatchImportReportId);
				bool flag3 = batchImportReportId > 0;
				if (flag3)
				{
					auditResult.Checks.Add(DailyJob.CheckOldWindowsTaskJobRanRecently(source2.FirstOrDefault((DailyJobTask g) => g.ReportBase.ReportId == batchImportReportId), "Check old Windows Scheduled Task Batch Data Sync Present"));
				}
				IReportManager reportManager = new ReportManager(this.OpContext);
				IReportManager reportManager2 = reportManager;
				ReportContext reportContext = new ReportContext();
				reportContext.ReportIds = (from g in source2
				select g.ReportBase.ReportId into h
				where h != batchImportReportId
				select h).ToList<int>();
				ReportCollection reportCollection = reportManager2.LoadReports(reportContext);
				List<int> emailReports = (from g in reportCollection.Reports
				where g.Functions.Any((ReportFunction h) => h.FunctionCode == eFunctionType.Batch_Email_with_Mail_Merge_3)
				select g into m
				select m.ReportId).ToList<int>();
				List<DailyJobTask> list2 = (from g in source2
				where emailReports.Any((int h) => g.ReportBase.ReportId == h)
				select g).ToList<DailyJobTask>();
				int count2 = list2.Count;
				auditResult.Checks.Add((count2 < 3) ? new AuditCheck("Check old Windows Scheduled Task Email Reminders count", eAuditStatus.Failed, new string[]
				{
					"Too few email reminders present (<3):Count={0}",
					count2.ToString()
				}) : new AuditCheck("Check old Windows Scheduled Task Email Reminders count", eAuditStatus.CompletedSuccessful, new string[]
				{
					"Email reminders present count={0}",
					count2.ToString()
				}));
				using (List<DailyJobTask>.Enumerator enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						DailyJobTask emailTaskJob = enumerator2.Current;
						Report report = reportCollection.Reports.FirstOrDefault((Report h) => h.ReportId == emailTaskJob.ReportBase.ReportId);
						auditResult.Checks.Add(DailyJob.CheckOldWindowsTaskJobRanRecently(emailTaskJob, "Check email task job " + ((report == null) ? "???" : (report.Title ?? "?"))));
					}
				}
			}
			return auditResult;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002C60 File Offset: 0x00000E60
		private static AuditCheck CheckOldWindowsTaskJobRanRecently(DailyJobTask jobTask, string title)
		{
			bool flag = jobTask == null;
			AuditCheck result;
			if (flag)
			{
				result = new AuditCheck(title, eAuditStatus.Failed, new string[]
				{
					"Task job not present."
				});
			}
			else
			{
				bool flag2 = jobTask.LastRunStartDate == null;
				if (flag2)
				{
					result = new AuditCheck(title, eAuditStatus.Failed, new string[]
					{
						"Task job present but has never run - maybe Windows Scheduled Task has not been created or is failing?."
					});
				}
				else
				{
					bool flag3 = (DateTime.Now.Date - jobTask.LastRunStartDate.Value.Date).TotalDays > 2.0;
					if (flag3)
					{
						result = new AuditCheck(title, eAuditStatus.Failed, new string[]
						{
							"Task job present and has run, but has not run lately.  Last run start={0}",
							jobTask.LastRunStartDate.Value.ToString("yyyy-MM-dd h:mm tt")
						});
					}
					else
					{
						bool flag4 = jobTask.LastRunEndDate == null;
						if (flag4)
						{
							result = new AuditCheck(title, eAuditStatus.Failed, new string[]
							{
								"Task job present and has run recently, but has not finished."
							});
						}
						else
						{
							double totalMinutes = (jobTask.LastRunEndDate.Value - jobTask.LastRunStartDate.Value).TotalMinutes;
							result = ((totalMinutes < 1.0) ? new AuditCheck(title, eAuditStatus.Failed, new string[]
							{
								"Task job present and has run and finished recently, but run duration seems too low.  Last run duration in minutes={0}",
								totalMinutes.ToString()
							}) : new AuditCheck(title, eAuditStatus.CompletedSuccessful, new string[]
							{
								"Last run start={0}; last run duration in minutes={1}",
								jobTask.LastRunStartDate.Value.ToString("yyyy-MM-dd h:mm tt"),
								totalMinutes.ToString()
							}));
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002E14 File Offset: 0x00001014
		private AuditCheck CheckServerJobRanRecently(IClockWorkServerJobManager sjm, ClockWorkServerJobStep jobStep, string title)
		{
			List<ClockWorkServerJobExecutionLog> list = sjm.GetClockWorkServerExecutingLogsByJob(jobStep.JobId, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1)).ToList<ClockWorkServerJobExecutionLog>();
			bool flag = list.Count < 1;
			AuditCheck result;
			if (flag)
			{
				result = new AuditCheck(title, eAuditStatus.Failed, new string[]
				{
					"Job exists but has never run - no logs available."
				});
			}
			else
			{
				list.Sort((ClockWorkServerJobExecutionLog g1, ClockWorkServerJobExecutionLog g2) => g2.StartTime.CompareTo(g1.StartTime));
				ClockWorkServerJobExecutionLog clockWorkServerJobExecutionLog = list[0];
				result = (((DateTime.Now.Date - clockWorkServerJobExecutionLog.StartTime.Date).TotalDays > 2.0) ? new AuditCheck(title, eAuditStatus.Failed, new string[]
				{
					"Job exists but last run date is: {0}",
					clockWorkServerJobExecutionLog.StartTime.ToString("yyyy-MM-dd h:mm tt")
				}) : new AuditCheck(title, eAuditStatus.CompletedSuccessful, new string[]
				{
					"Job last run successfully on {0}",
					clockWorkServerJobExecutionLog.StartTime.ToString("yyyy-MM-dd h:mm tt")
				}));
			}
			return result;
		}
	}
}

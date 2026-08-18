using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.MailMerging;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Jobs.ExecutingJobs
{
	// Token: 0x02000008 RID: 8
	[ClockWorkServerJobExecuting("Send automatic email reminders", ControlParametersType = "CtrlClockWorkServerJobEmailReminderParameters")]
	public class ClockWorkServerEmailReminderJob : IClockWorkServerExecutingJob, IDisposable
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000364E File Offset: 0x0000184E
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00003656 File Offset: 0x00001856
		protected ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002C4D File Offset: 0x00000E4D
		public string JobName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000365F File Offset: 0x0000185F
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00003667 File Offset: 0x00001867
		private int ReportId { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003670 File Offset: 0x00001870
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00003678 File Offset: 0x00001878
		private IList<ReportParameter> ReportParameters { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003681 File Offset: 0x00001881
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003689 File Offset: 0x00001889
		private int EmailTemplateId { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003692 File Offset: 0x00001892
		// (set) Token: 0x0600004D RID: 77 RVA: 0x0000369A File Offset: 0x0000189A
		private bool SendReport { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000036A3 File Offset: 0x000018A3
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000036AB File Offset: 0x000018AB
		private bool TestMode { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000036B4 File Offset: 0x000018B4
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000036BC File Offset: 0x000018BC
		private string EmailTypeCode { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000036C5 File Offset: 0x000018C5
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000036CD File Offset: 0x000018CD
		private string AdminEmail { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000036D6 File Offset: 0x000018D6
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000036DE File Offset: 0x000018DE
		private int EmailDelay { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000036E7 File Offset: 0x000018E7
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000036EF File Offset: 0x000018EF
		private int AppIconEmailSent { get; set; }

		// Token: 0x06000058 RID: 88 RVA: 0x00002C5A File Offset: 0x00000E5A
		public void Dispose()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000036F8 File Offset: 0x000018F8
		public void Init(ServerInstanceInfo serverInstance, string parameters)
		{
			this.ServerInstance = serverInstance;
			this.ReportParameters = new List<ReportParameter>();
			this.ParseParameters(parameters);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003714 File Offset: 0x00001914
		public ClockWorkServerJobRunningResult Run()
		{
			string executingPath = Path.Combine(this.ServerInstance.InstallationPath, "bin");
			OperationContext opContext = new OperationContext
			{
				WhoAmI = 0,
				AppContext = new ApplicationContext
				{
					ExecutingPath = executingPath
				}
			};
			IReportManager reportManager = new ReportManager(opContext);
			RunReportResult runReportResult = reportManager.ExecuteReport2(this.ReportId, this.ReportParameters.ToArray<ReportParameter>());
			eRunStatusStep? eRunStatusStep;
			if (runReportResult == null)
			{
				eRunStatusStep = null;
			}
			else
			{
				RunStatus reportStatus = runReportResult.ReportStatus;
				eRunStatusStep = ((reportStatus != null) ? new eRunStatusStep?(reportStatus.LastStatusStep) : null);
			}
			eRunStatusStep eRunStatusStep2 = eRunStatusStep ?? eRunStatusStep.Failed;
			if (eRunStatusStep2 != eRunStatusStep.CompletedSuccessfully)
			{
				string str = (((runReportResult != null) ? runReportResult.ReportStatus : null) != null) ? (runReportResult.ReportStatus.ErrorMessage ?? string.Empty) : string.Empty;
				return new ClockWorkServerJobRunningResult
				{
					JobName = this.JobName,
					Status = eClockWorkServerJobResult.Error,
					Message = eRunStatusStep2.ToString() + ": " + str
				};
			}
			Report report = reportManager.LoadReport(this.ReportId);
			if (((report != null) ? report.Functions : null) != null && report.Functions.Count > 0 && (report.Functions.Last<ReportFunction>().FunctionCode == eFunctionType.Batch_Email_with_Mail_Merge_3 || report.Functions.Last<ReportFunction>().FunctionCode == eFunctionType.Batch_Email_with_Mail_Merge))
			{
				if (this.EmailTemplateId > 0)
				{
					CWLogger.Logger.Warn("ClockWorkServer.Core.Jobs.ClockWOrkServerEmailReminderJob:Run:Report {0} was run successfully and had a batch email as the last step so the template with id={1} has been ignored.", this.ReportId.ToString(), this.EmailTemplateId.ToString());
				}
			}
			else
			{
				if (this.EmailTemplateId <= 0)
				{
					CWLogger.Logger.Warn("ClockWorkServer.Core.Jobs.ClockWorkServerEmailReminderJob:Run:The report with rid={0} has run successfully but emailtemplateid is 0 - no emails have been sent.", this.ReportId);
					return new ClockWorkServerJobRunningResult
					{
						JobName = this.JobName,
						Status = eClockWorkServerJobResult.SuccessWithWarnings,
						Message = string.Format("ClockWorkServer.Core.Jobs.ClockWorkServerEmailReminderJob:Run:The report with rid={0} has run successfully but emailtemplateid is 0 - no emails have been sent.", this.ReportId)
					};
				}
				IMailMergingManager mailMergingManager = new MailMergingManager(opContext);
				DataTable dataTable;
				if (runReportResult == null)
				{
					dataTable = null;
				}
				else
				{
					RunFunctionData primaryData = runReportResult.PrimaryData;
					dataTable = ((primaryData != null) ? primaryData.Table : null);
				}
				DataTable t = dataTable;
				IList<MailMergeContextWithCustomDictionary> contextsWithCustomDictionaries = mailMergingManager.ExtractMailMergeContextFromTable(t);
				IDictionary<MailMergeContext, TPMailMessage> messages = ((IMailMergingEmailManager)new MailMergingEmailManager(opContext)).MailMerge(contextsWithCustomDictionaries, this.EmailTemplateId);
				IList<TPMailResult> source = ((IEmailManager)new EmailManager(opContext)).SendEmails(messages, new BatchEmailSendParameters
				{
					AdminEmail = this.AdminEmail,
					AppIconEmailSent = this.AppIconEmailSent,
					EmailDelay = this.EmailDelay,
					SendReport = this.SendReport,
					TestMode = this.TestMode,
					Title = this.JobName,
					EmailTypeCode = this.EmailTypeCode,
					EmailTemplateId = this.EmailTemplateId
				});
				TPMailResult tpmailResult = source.FirstOrDefault((TPMailResult er) => er.Status == eTPMailResultStatus.Failed);
				if (tpmailResult != null)
				{
					return new ClockWorkServerJobRunningResult
					{
						JobName = this.JobName,
						Status = eClockWorkServerJobResult.Error,
						Message = tpmailResult.ErrorMessage
					};
				}
				tpmailResult = source.FirstOrDefault((TPMailResult er) => er.Status == eTPMailResultStatus.CompletedWithWarnings);
				if (tpmailResult != null)
				{
					return new ClockWorkServerJobRunningResult
					{
						JobName = this.JobName,
						Status = eClockWorkServerJobResult.SuccessWithWarnings,
						Message = tpmailResult.ErrorMessage
					};
				}
			}
			return new ClockWorkServerJobRunningResult
			{
				JobName = this.JobName,
				Status = eClockWorkServerJobResult.Success,
				Message = string.Empty
			};
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003A8C File Offset: 0x00001C8C
		private void ParseParameters(string parameters)
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			XElement xelement = XDocument.Load(new StringReader(parameters)).Element("ClockWorkServerJobReminderEmailParameters");
			XElement xelement2 = (xelement != null) ? xelement.Element("ReportId") : null;
			if (xelement2 == null)
			{
				return;
			}
			int reportId;
			if (!string.IsNullOrEmpty(xelement2.Value) && int.TryParse(xelement2.Value, out reportId))
			{
				this.ReportId = reportId;
			}
			XElement xelement3 = xelement.Element("OptionalReportParameters");
			if (xelement3 != null)
			{
				foreach (XElement xelement4 in from item in xelement3.Elements("Item")
				select item)
				{
					XAttribute xattribute = xelement4.Attribute("key");
					XAttribute xattribute2 = xelement4.Attribute("value");
					if (xattribute != null && xattribute2 != null)
					{
						dictionary.Add(xattribute.Value, xattribute2.Value);
					}
				}
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					this.ReportParameters.Add(new ReportParameter
					{
						Name = keyValuePair.Key,
						Value = keyValuePair.Value
					});
				}
			}
			XElement xelement5 = xelement.Element("EmailTemplateId");
			if (xelement5 == null)
			{
				return;
			}
			int emailTemplateId;
			if (!string.IsNullOrEmpty(xelement5.Value) && int.TryParse(xelement5.Value, out emailTemplateId))
			{
				this.EmailTemplateId = emailTemplateId;
			}
			XElement xelement6 = xelement.Element("SendReport");
			int num;
			this.SendReport = (xelement6 != null && !string.IsNullOrEmpty(xelement6.Value) && int.TryParse(xelement6.Value, out num) && num > 0);
			XElement xelement7 = xelement.Element("TestMode");
			int num2;
			this.TestMode = (xelement7 != null && !string.IsNullOrEmpty(xelement7.Value) && int.TryParse(xelement7.Value, out num2) && num2 > 0);
			XElement xelement8 = xelement.Element("EmailTypeCode");
			this.EmailTypeCode = ((xelement8 != null && !string.IsNullOrEmpty(xelement8.Value)) ? xelement8.Value : "UNKNOWN");
			XElement xelement9 = xelement.Element("AdminEmail");
			this.AdminEmail = ((xelement9 != null && !string.IsNullOrEmpty(xelement9.Value)) ? xelement9.Value : string.Empty);
			XElement xelement10 = xelement.Element("EmailDelay");
			int num3;
			this.EmailDelay = ((xelement10 != null && !string.IsNullOrEmpty(xelement10.Value) && int.TryParse(xelement10.Value, out num3)) ? num3 : 0);
			XElement xelement11 = xelement.Element("EmailSentAppIcon");
			int num4;
			this.AppIconEmailSent = ((xelement11 != null && !string.IsNullOrEmpty(xelement11.Value) && int.TryParse(xelement11.Value, out num4)) ? num4 : 0);
		}
	}
}

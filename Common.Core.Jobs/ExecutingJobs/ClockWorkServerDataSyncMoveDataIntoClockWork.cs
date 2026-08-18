using System;
using System.IO;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Jobs.ExecutingJobs
{
	// Token: 0x02000007 RID: 7
	[ClockWorkServerJobExecuting("Move flat file data into ClockWork lookup tables for data sync", ParametersDescription = "No parameters required")]
	public class ClockWorkServerDataSyncMoveDataIntoClockWork : IClockWorkServerExecutingJob, IDisposable
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000353F File Offset: 0x0000173F
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00003547 File Offset: 0x00001747
		protected ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002C4D File Offset: 0x00000E4D
		public string JobName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C5A File Offset: 0x00000E5A
		public void Dispose()
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003550 File Offset: 0x00001750
		public void Init(ServerInstanceInfo serverInstance, string parameters)
		{
			this.ServerInstance = serverInstance;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000355C File Offset: 0x0000175C
		public ClockWorkServerJobRunningResult Run()
		{
			string executingPath = Path.Combine(this.ServerInstance.InstallationPath, "bin");
			RunReportResult runReportResult = ((IDataSyncManager)new DataSyncManager(new OperationContext
			{
				WhoAmI = 0,
				AppContext = new ApplicationContext
				{
					ExecutingPath = executingPath
				}
			})).RunMoveDataIntoClockWork();
			eRunStatusStep eRunStatusStep = (runReportResult == null || runReportResult.ReportStatus == null) ? eRunStatusStep.Failed : runReportResult.ReportStatus.LastStatusStep;
			if (eRunStatusStep != eRunStatusStep.CompletedSuccessfully)
			{
				return new ClockWorkServerJobRunningResult
				{
					JobName = this.JobName,
					Status = eClockWorkServerJobResult.Error,
					Message = eRunStatusStep.ToString() + ": " + ((runReportResult == null || runReportResult.ReportStatus == null || string.IsNullOrEmpty(runReportResult.ReportStatus.ErrorMessage)) ? "NULL" : runReportResult.ReportStatus.ErrorMessage)
				};
			}
			return new ClockWorkServerJobRunningResult
			{
				JobName = this.JobName,
				Status = eClockWorkServerJobResult.Success,
				Message = string.Empty
			};
		}
	}
}

using System;
using System.IO;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.InstanceInfo;

namespace TechnoPro.Common.Core.Jobs.ExecutingJobs
{
	// Token: 0x02000004 RID: 4
	[ClockWorkServerJobExecuting("Data Sync all students", ParametersDescription = "No parameters required")]
	public class ClockWorkServerBatchDataSyncJob : IClockWorkServerExecutingJob, IDisposable
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002C3C File Offset: 0x00000E3C
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002C44 File Offset: 0x00000E44
		protected ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002C4D File Offset: 0x00000E4D
		public string JobName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C5A File Offset: 0x00000E5A
		public void Dispose()
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002C5C File Offset: 0x00000E5C
		public void Init(ServerInstanceInfo serverInstance, string parameters)
		{
			this.ServerInstance = serverInstance;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002C68 File Offset: 0x00000E68
		public ClockWorkServerJobRunningResult Run()
		{
			string executingPath = Path.Combine(this.ServerInstance.InstallationPath, "bin");
			DataSyncResult dataSyncResult = ((IDataSyncManager)new DataSyncManager(new OperationContext
			{
				WhoAmI = 0,
				AppContext = new ApplicationContext
				{
					ExecutingPath = executingPath
				}
			})).RunBatchDataSync();
			if (((dataSyncResult != null) ? dataSyncResult.Status : eDataSyncStatus.Failed) != eDataSyncStatus.CompletedSuccessfully)
			{
				return new ClockWorkServerJobRunningResult
				{
					JobName = this.JobName,
					Status = eClockWorkServerJobResult.Error,
					Message = ((dataSyncResult == null || dataSyncResult.SyncError == null) ? "" : (dataSyncResult.SyncError.ErrorMessage ?? ""))
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

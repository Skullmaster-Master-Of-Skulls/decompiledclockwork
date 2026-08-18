using System;
using System.Threading;
using ClockWorkLogger;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.InstanceInfo;

namespace TechnoPro.Common.Core.Jobs.ExecutingJobs
{
	// Token: 0x02000006 RID: 6
	[ClockWorkServerJobExecuting("Sleep for T minutes", ParametersDescription = "Integer for the time in minutes to sleep")]
	public class ClockWorkServerTminsJob : IClockWorkServerExecutingJob, IDisposable
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000344D File Offset: 0x0000164D
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00003455 File Offset: 0x00001655
		protected ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003460 File Offset: 0x00001660
		public string JobName
		{
			get
			{
				return base.GetType().Name.Replace("T", this.Minutes.ToString());
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003490 File Offset: 0x00001690
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00003498 File Offset: 0x00001698
		private int Minutes { get; set; }

		// Token: 0x06000038 RID: 56 RVA: 0x000034A4 File Offset: 0x000016A4
		public void Init(ServerInstanceInfo serverInstance, string parameters)
		{
			this.ServerInstance = serverInstance;
			int num;
			this.Minutes = ((!string.IsNullOrEmpty(parameters) && int.TryParse(parameters, out num)) ? num : 3);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C5A File Offset: 0x00000E5A
		public void Dispose()
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000034D4 File Offset: 0x000016D4
		public ClockWorkServerJobRunningResult Run()
		{
			CWLogger.Logger.Info("{0}:: ********* Job Run start *********", this.JobName);
			Thread.Sleep(TimeSpan.FromMinutes((double)this.Minutes));
			CWLogger.Logger.Info("{0}:: *********** Job Run end **********", this.JobName);
			return new ClockWorkServerJobRunningResult
			{
				JobName = this.JobName,
				Status = eClockWorkServerJobResult.Success,
				Message = string.Empty
			};
		}
	}
}

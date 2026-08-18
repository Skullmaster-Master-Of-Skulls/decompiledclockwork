using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.Common.ICore.ClockWorkServerJob
{
	// Token: 0x020000B2 RID: 178
	public interface IClockWorkServerJobManager : IBaseOperationContext<ClockWorkServerOperationContext>
	{
		// Token: 0x06000553 RID: 1363
		IList<ClockWorkServerJobInfo> GetClockWorkServerJobs();

		// Token: 0x06000554 RID: 1364
		IList<ClockWorkServerJobInfo> GetActiveClockWorkServerJobs();

		// Token: 0x06000555 RID: 1365
		ClockWorkServerJobInfo GetClockWorkServerJobById(int jobId);

		// Token: 0x06000556 RID: 1366
		int CreateClockWorkServerJob(ClockWorkServerJobInfo clockWorkServerJob);

		// Token: 0x06000557 RID: 1367
		void UpdateClockWorkServerJob(ClockWorkServerJobInfo clockWorkServerJob);

		// Token: 0x06000558 RID: 1368
		void UpdateClockWorkServerJobLastRun(int jobId, DateTime? lastRunStartDatetime, DateTime? lastRunEndDatetime, eClockWorkServerJobResult lastRunStatus, string lastRunMessage);

		// Token: 0x06000559 RID: 1369
		void RemoveClockWorkServerJob(int jobId);

		// Token: 0x0600055A RID: 1370
		IList<ClockWorkServerJobStep> GetClockWorkServerJobStepsByJobId(int clockworkServerJobId);

		// Token: 0x0600055B RID: 1371
		ClockWorkServerJobStep GetClockWorkServerJobStepById(int jobId, int stepId);

		// Token: 0x0600055C RID: 1372
		IList<ClockWorkServerJobExecutionLog> GetClockWorkServerExecutingLogsByJob(int jobId, DateTime startTime, DateTime endTime);

		// Token: 0x0600055D RID: 1373
		IList<ClockWorkServerJobExecutionLog> GetClockWorkServerExecutingLogs(DateTime startTime, DateTime endTime);

		// Token: 0x0600055E RID: 1374
		int AddClockWorkServerExecutingLog(ClockWorkServerJobExecutionLog clockWorkServerJobExecutionLog);

		// Token: 0x0600055F RID: 1375
		void EnableClockWorkServerJob(int jobId);

		// Token: 0x06000560 RID: 1376
		void DisableClockWorkServerJob(int jobId);

		// Token: 0x06000561 RID: 1377
		void RunClockWorkServerJobNow(int jobId);

		// Token: 0x06000562 RID: 1378
		void SynchronizeServerRecurringJobs();
	}
}

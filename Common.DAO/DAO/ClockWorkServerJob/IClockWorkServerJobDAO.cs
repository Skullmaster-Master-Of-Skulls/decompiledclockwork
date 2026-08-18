using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.Common.DAO.ClockWorkServerJob
{
	// Token: 0x02000099 RID: 153
	public interface IClockWorkServerJobDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003EF RID: 1007
		IList<ClockWorkServerJobInfo> GetClockWorkServerJobs();

		// Token: 0x060003F0 RID: 1008
		IList<ClockWorkServerJobInfo> GetActiveClockWorkServerJobs();

		// Token: 0x060003F1 RID: 1009
		ClockWorkServerJobInfo GetClockWorkServerJobById(int jobId);

		// Token: 0x060003F2 RID: 1010
		int CreateClockWorkServerJob(ClockWorkServerJobInfo clockWorkServerJob);

		// Token: 0x060003F3 RID: 1011
		void UpdateClockWorkServerJob(ClockWorkServerJobInfo clockWorkServerJob);

		// Token: 0x060003F4 RID: 1012
		void UpdateClockWorkServerJobLastRun(int jobId, DateTime? lastRunStartDatetime, DateTime? lastRunEndDatetime, eClockWorkServerJobResult lastRunStatus, string lastRunMessage);

		// Token: 0x060003F5 RID: 1013
		void RemoveClockWorkServerJob(int jobId);

		// Token: 0x060003F6 RID: 1014
		IList<ClockWorkServerJobStep> GetClockWorkServerJobStepsByJobId(int clockworkServerJobId);

		// Token: 0x060003F7 RID: 1015
		ClockWorkServerJobStep GetClockWorkServerJobStepById(int jobId, int stepId);

		// Token: 0x060003F8 RID: 1016
		IList<ClockWorkServerJobExecutionLog> GetClockWorkServerExecutingLogsByJob(int jobId, DateTime startTime, DateTime endTime);

		// Token: 0x060003F9 RID: 1017
		IList<ClockWorkServerJobExecutionLog> GetClockWorkServerExecutingLogs(DateTime startTime, DateTime endTime);

		// Token: 0x060003FA RID: 1018
		int AddClockWorkServerExecutingLog(ClockWorkServerJobExecutionLog clockWorkServerJobExecutionLog);

		// Token: 0x060003FB RID: 1019
		void EnableClockWorkServerJob(int jobId);

		// Token: 0x060003FC RID: 1020
		void DisableClockWorkServerJob(int jobId);

		// Token: 0x060003FD RID: 1021
		void UpdateClockWorkServerJobLastRunForBegin(int jobId, DateTime lastRunStartDatetime, eClockWorkServerJobResult lastRunStatus, string lastRunMessage);

		// Token: 0x060003FE RID: 1022
		void UpdateClockWorkServerJobLastRunForEnd(int jobId, DateTime lastRunEndDatetime, eClockWorkServerJobResult lastRunStatus, string lastRunMessage);
	}
}

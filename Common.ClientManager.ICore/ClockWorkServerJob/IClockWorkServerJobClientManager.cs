using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.ClockWorkServerJob
{
	// Token: 0x0200006D RID: 109
	public interface IClockWorkServerJobClientManager : IWebService
	{
		// Token: 0x0600033D RID: 829
		IList<ClockWorkServerJobInfoDTO> GetClockWorkServerJobs();

		// Token: 0x0600033E RID: 830
		ClockWorkServerJobInfoDTO GetClockWorkServerJobById(int jobId);

		// Token: 0x0600033F RID: 831
		int CreateClockWorkServerJob(ClockWorkServerJobInfoDTO clockWorkServerJob);

		// Token: 0x06000340 RID: 832
		void UpdateClockWorkServerJob(ClockWorkServerJobInfoDTO clockWorkServerJob);

		// Token: 0x06000341 RID: 833
		void RemoveClockWorkServerJob(int jobId);

		// Token: 0x06000342 RID: 834
		IList<ClockWorkServerJobExecutionLogDTO> GetClockWorkServerExecutingLogsByJob(int jobId, DateTime startTime, DateTime endTime);

		// Token: 0x06000343 RID: 835
		IList<ClockWorkServerJobExecutionLogDTO> GetClockWorkServerExecutingLogs(DateTime startTime, DateTime endTime);

		// Token: 0x06000344 RID: 836
		IList<ClockWorkServerJobExecutingTypeInfoDTO> GetClockWorkServerJobTypes();

		// Token: 0x06000345 RID: 837
		void RunClockWorkServerJobNow(int jobId);

		// Token: 0x06000346 RID: 838
		void EnableClockWorkServerJob(int jobId);

		// Token: 0x06000347 RID: 839
		void DisableClockWorkServerJob(int jobId);
	}
}

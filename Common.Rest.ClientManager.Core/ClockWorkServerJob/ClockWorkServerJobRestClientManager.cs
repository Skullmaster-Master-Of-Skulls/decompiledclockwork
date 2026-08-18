using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.Common.ClientManager.ICore.ClockWorkServerJob;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.ClockWorkServerJob
{
	// Token: 0x02000062 RID: 98
	public class ClockWorkServerJobRestClientManager : BearerTokenRestProxy<IClockWorkServerJobClientManager>, IClockWorkServerJobClientManager, IWebService
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public ClockWorkServerJobRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000B6FA File Offset: 0x000098FA
		public ClockWorkServerJobRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000B705 File Offset: 0x00009905
		public IList<ClockWorkServerJobInfoDTO> GetClockWorkServerJobs()
		{
			return base.GetMany<ClockWorkServerJobInfoDTO>("clockworkserverjob", true);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000B713 File Offset: 0x00009913
		public ClockWorkServerJobInfoDTO GetClockWorkServerJobById(int jobId)
		{
			return base.Get<ClockWorkServerJobInfoDTO>(string.Format("clockworkserverjob/jobid/{0}", jobId), true);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000B72C File Offset: 0x0000992C
		public int CreateClockWorkServerJob(ClockWorkServerJobInfoDTO clockWorkServerJob)
		{
			return base.Post<ClockWorkServerJobInfoDTO, int>(clockWorkServerJob, "clockworkserverjob");
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000B73A File Offset: 0x0000993A
		public void UpdateClockWorkServerJob(ClockWorkServerJobInfoDTO clockWorkServerJob)
		{
			base.Put<ClockWorkServerJobInfoDTO>(clockWorkServerJob, "clockworkserverjob");
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000B748 File Offset: 0x00009948
		public void RemoveClockWorkServerJob(int jobId)
		{
			base.Delete(string.Format("clockworkserverjob/jobid/{0}", jobId));
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000B760 File Offset: 0x00009960
		public IList<ClockWorkServerJobExecutionLogDTO> GetClockWorkServerExecutingLogsByJob(int jobId, DateTime startTime, DateTime endTime)
		{
			return base.GetMany<ClockWorkServerJobExecutionLogDTO>(string.Format("clockworkserverjob/executinglogs/jobid/{0}/range/{1}/{2}", jobId, startTime, endTime), true);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000B785 File Offset: 0x00009985
		public IList<ClockWorkServerJobExecutionLogDTO> GetClockWorkServerExecutingLogs(DateTime startTime, DateTime endTime)
		{
			return base.GetMany<ClockWorkServerJobExecutionLogDTO>(string.Format("clockworkserverjob/executinglogs/range/{0}/{1}", startTime, endTime), true);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000B7A4 File Offset: 0x000099A4
		public IList<ClockWorkServerJobExecutingTypeInfoDTO> GetClockWorkServerJobTypes()
		{
			return base.GetMany<ClockWorkServerJobExecutingTypeInfoDTO>("clockworkserverjob/jobtypes", true);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B7B2 File Offset: 0x000099B2
		public void RunClockWorkServerJobNow(int jobId)
		{
			base.Post(string.Format("clockworkserverjob/runjobnow/jobid/{0}", jobId));
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000B7CA File Offset: 0x000099CA
		public void EnableClockWorkServerJob(int jobId)
		{
			base.Post(string.Format("clockworkserverjob/enablejob/jobid/{0}", jobId));
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000B7E2 File Offset: 0x000099E2
		public void DisableClockWorkServerJob(int jobId)
		{
			base.Post(string.Format("clockworkserverjob/disablejob/jobid/{0}", jobId));
		}
	}
}

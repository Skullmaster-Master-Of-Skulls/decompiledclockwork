using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ClockWorkServerJob;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.ClockWorkServerJob
{
	// Token: 0x02000074 RID: 116
	public class ClockWorkServerJobClientManager : IClockWorkServerJobClientManager, IWebService
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x00012FA8 File Offset: 0x000111A8
		public IList<ClockWorkServerJobInfoDTO> GetClockWorkServerJobs()
		{
			GetClockWorkServerJobsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetClockWorkServerJobsReq>();
			return ClientServiceFactory.GetClientInstance<IClockWorkServerJob>().GetClockWorkServerJobs(request).ClockWorkServerJobInfoList;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00012FD8 File Offset: 0x000111D8
		public ClockWorkServerJobInfoDTO GetClockWorkServerJobById(int jobId)
		{
			GetClockWorkServerJobByIdReq getClockWorkServerJobByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetClockWorkServerJobByIdReq>();
			getClockWorkServerJobByIdReq.JobId = jobId;
			return ClientServiceFactory.GetClientInstance<IClockWorkServerJob>().GetClockWorkServerJobById(getClockWorkServerJobByIdReq).ClockWorkServerJob;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00013010 File Offset: 0x00011210
		public int CreateClockWorkServerJob(ClockWorkServerJobInfoDTO clockWorkServerJob)
		{
			CreateClockWorkServerJobReq createClockWorkServerJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateClockWorkServerJobReq>();
			createClockWorkServerJobReq.ClockWorkServerJob = clockWorkServerJob;
			return ClientServiceFactory.GetClientInstance<IClockWorkServerJob>().CreateClockWorkServerJob(createClockWorkServerJobReq).JobId;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00013048 File Offset: 0x00011248
		public void UpdateClockWorkServerJob(ClockWorkServerJobInfoDTO clockWorkServerJob)
		{
			UpdateClockWorkServerJobReq updateClockWorkServerJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateClockWorkServerJobReq>();
			updateClockWorkServerJobReq.ClockWorkServerJob = clockWorkServerJob;
			ClientServiceFactory.GetClientInstance<IClockWorkServerJob>().UpdateClockWorkServerJob(updateClockWorkServerJobReq);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00013078 File Offset: 0x00011278
		public void RemoveClockWorkServerJob(int jobId)
		{
			RemoveClockWorkServerJobReq removeClockWorkServerJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveClockWorkServerJobReq>();
			removeClockWorkServerJobReq.JobId = jobId;
			ClientServiceFactory.GetClientInstance<IClockWorkServerJob>().RemoveClockWorkServerJob(removeClockWorkServerJobReq);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000130A8 File Offset: 0x000112A8
		public IList<ClockWorkServerJobExecutionLogDTO> GetClockWorkServerExecutingLogsByJob(int jobId, DateTime startTime, DateTime endTime)
		{
			GetClockWorkServerExecutingLogsByJobReq getClockWorkServerExecutingLogsByJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetClockWorkServerExecutingLogsByJobReq>();
			getClockWorkServerExecutingLogsByJobReq.JobId = jobId;
			getClockWorkServerExecutingLogsByJobReq.StartTime = startTime;
			getClockWorkServerExecutingLogsByJobReq.EndTime = endTime;
			return ClientServiceFactory.GetClientInstance<IClockWorkServerJob>().GetClockWorkServerExecutingLogsByJob(getClockWorkServerExecutingLogsByJobReq).ClockWorkServerJobExecutionLogList;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000130F0 File Offset: 0x000112F0
		public IList<ClockWorkServerJobExecutionLogDTO> GetClockWorkServerExecutingLogs(DateTime startTime, DateTime endTime)
		{
			GetClockWorkServerExecutingLogsReq getClockWorkServerExecutingLogsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetClockWorkServerExecutingLogsReq>();
			getClockWorkServerExecutingLogsReq.StartTime = startTime;
			getClockWorkServerExecutingLogsReq.EndTime = endTime;
			return ClientServiceFactory.GetClientInstance<IClockWorkServerJob>().GetClockWorkServerExecutingLogs(getClockWorkServerExecutingLogsReq).ClockWorkServerJobExecutionLogList;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00013130 File Offset: 0x00011330
		public IList<ClockWorkServerJobExecutingTypeInfoDTO> GetClockWorkServerJobTypes()
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			object obj = clientCache["cClockWorkServerJobTypes"];
			bool flag = obj != null;
			IList<ClockWorkServerJobExecutingTypeInfoDTO> result;
			if (flag)
			{
				result = (IList<ClockWorkServerJobExecutingTypeInfoDTO>)obj;
			}
			else
			{
				GetClockWorkServerJobTypesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetClockWorkServerJobTypesReq>();
				GetClockWorkServerJobTypesResp clockWorkServerJobTypes = ClientServiceFactory.GetClientInstance<IClockWorkServerJob>().GetClockWorkServerJobTypes(request);
				clientCache["cClockWorkServerJobTypes"] = clockWorkServerJobTypes.ClockWorkServerJobTypeList;
				result = clockWorkServerJobTypes.ClockWorkServerJobTypeList;
			}
			return result;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001319C File Offset: 0x0001139C
		public void RunClockWorkServerJobNow(int jobId)
		{
			RunClockWorkServerJobNowReq runClockWorkServerJobNowReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RunClockWorkServerJobNowReq>();
			runClockWorkServerJobNowReq.JobId = jobId;
			ClientServiceFactory.GetClientInstance<IClockWorkServerJob>().RunClockWorkServerJobNow(runClockWorkServerJobNowReq);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000131CC File Offset: 0x000113CC
		public void EnableClockWorkServerJob(int jobId)
		{
			EnableClockWorkServerJobReq enableClockWorkServerJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<EnableClockWorkServerJobReq>();
			enableClockWorkServerJobReq.JobId = jobId;
			ClientServiceFactory.GetClientInstance<IClockWorkServerJob>().EnableClockWorkServerJob(enableClockWorkServerJobReq);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000131FC File Offset: 0x000113FC
		public void DisableClockWorkServerJob(int jobId)
		{
			DisableClockWorkServerJobReq disableClockWorkServerJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DisableClockWorkServerJobReq>();
			disableClockWorkServerJobReq.JobId = jobId;
			ClientServiceFactory.GetClientInstance<IClockWorkServerJob>().DisableClockWorkServerJob(disableClockWorkServerJobReq);
		}
	}
}

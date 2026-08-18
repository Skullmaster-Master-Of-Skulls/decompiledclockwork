using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.Common.Core.Jobs;
using TechnoPro.Common.Core.Mappers.ClockWorkServerJob;
using TechnoPro.Common.ICore.ClockWorkServerJob;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200002E RID: 46
	public class ClockWorkServerJobServiceManager : IClockWorkServerJob, IService
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00009520 File Offset: 0x00007720
		public GetClockWorkServerJobsResp GetClockWorkServerJobs(GetClockWorkServerJobsReq request)
		{
			IClockWorkServerJobManager clockWorkServerJobManager = new ClockWorkServerJobManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			return new GetClockWorkServerJobsResp
			{
				ClockWorkServerJobInfoList = clockWorkServerJobManager.GetClockWorkServerJobs().ToDTO()
			};
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009558 File Offset: 0x00007758
		public GetClockWorkServerJobByIdResp GetClockWorkServerJobById(GetClockWorkServerJobByIdReq request)
		{
			IClockWorkServerJobManager clockWorkServerJobManager = new ClockWorkServerJobManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			return new GetClockWorkServerJobByIdResp
			{
				ClockWorkServerJob = clockWorkServerJobManager.GetClockWorkServerJobById(request.JobId).ToDTO()
			};
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00009594 File Offset: 0x00007794
		public CreateClockWorkServerJobResp CreateClockWorkServerJob(CreateClockWorkServerJobReq request)
		{
			IClockWorkServerJobManager clockWorkServerJobManager = new ClockWorkServerJobManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			return new CreateClockWorkServerJobResp
			{
				JobId = clockWorkServerJobManager.CreateClockWorkServerJob(request.ClockWorkServerJob.ToDomainObject())
			};
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000095D0 File Offset: 0x000077D0
		public UpdateClockWorkServerJobResp UpdateClockWorkServerJob(UpdateClockWorkServerJobReq request)
		{
			IClockWorkServerJobManager clockWorkServerJobManager = new ClockWorkServerJobManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			clockWorkServerJobManager.UpdateClockWorkServerJob(request.ClockWorkServerJob.ToDomainObject());
			return new UpdateClockWorkServerJobResp();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009608 File Offset: 0x00007808
		public RemoveClockWorkServerJobResp RemoveClockWorkServerJob(RemoveClockWorkServerJobReq request)
		{
			IClockWorkServerJobManager clockWorkServerJobManager = new ClockWorkServerJobManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			clockWorkServerJobManager.RemoveClockWorkServerJob(request.JobId);
			return new RemoveClockWorkServerJobResp();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009638 File Offset: 0x00007838
		public GetClockWorkServerExecutingLogsByJobResp GetClockWorkServerExecutingLogsByJob(GetClockWorkServerExecutingLogsByJobReq request)
		{
			IClockWorkServerJobManager clockWorkServerJobManager = new ClockWorkServerJobManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			return new GetClockWorkServerExecutingLogsByJobResp
			{
				ClockWorkServerJobExecutionLogList = clockWorkServerJobManager.GetClockWorkServerExecutingLogsByJob(request.JobId, request.StartTime, request.EndTime).ToDTO()
			};
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009680 File Offset: 0x00007880
		public GetClockWorkServerExecutingLogsResp GetClockWorkServerExecutingLogs(GetClockWorkServerExecutingLogsReq request)
		{
			IClockWorkServerJobManager clockWorkServerJobManager = new ClockWorkServerJobManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			return new GetClockWorkServerExecutingLogsResp
			{
				ClockWorkServerJobExecutionLogList = clockWorkServerJobManager.GetClockWorkServerExecutingLogs(request.StartTime, request.EndTime).ToDTO()
			};
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000096C4 File Offset: 0x000078C4
		public GetClockWorkServerJobTypesResp GetClockWorkServerJobTypes(GetClockWorkServerJobTypesReq request)
		{
			Assembly assembly = Assembly.GetAssembly(typeof(IClockWorkServerExecutingJob));
			Type iType = typeof(IClockWorkServerExecutingJob);
			List<Type> list = (from t in assembly.GetTypes()
			where !t.IsInterface && !t.IsAbstract && iType.IsAssignableFrom(t)
			select t).ToList<Type>();
			List<ClockWorkServerJobExecutingTypeInfoDTO> list2 = new List<ClockWorkServerJobExecutingTypeInfoDTO>();
			bool flag = list.Count > 0;
			if (flag)
			{
				foreach (Type type in list)
				{
					try
					{
						ClockWorkServerJobExecutingAttribute[] customAttributes = type.GetCustomAttributes<ClockWorkServerJobExecutingAttribute>();
						ClockWorkServerJobExecutingTypeInfoDTO clockWorkServerJobExecutingTypeInfoDTO = new ClockWorkServerJobExecutingTypeInfoDTO();
						bool flag2 = customAttributes != null && customAttributes.Length != 0;
						if (flag2)
						{
							clockWorkServerJobExecutingTypeInfoDTO.Title = customAttributes[0].Title;
							clockWorkServerJobExecutingTypeInfoDTO.ParametersDescription = customAttributes[0].ParametersDescription;
							clockWorkServerJobExecutingTypeInfoDTO.ControlParametersType = customAttributes[0].ControlParametersType;
							clockWorkServerJobExecutingTypeInfoDTO.ExecutingType = type.Name;
						}
						else
						{
							clockWorkServerJobExecutingTypeInfoDTO.Title = (clockWorkServerJobExecutingTypeInfoDTO.ExecutingType = type.Name);
							clockWorkServerJobExecutingTypeInfoDTO.ControlParametersType = null;
							clockWorkServerJobExecutingTypeInfoDTO.ParametersDescription = string.Empty;
						}
						list2.Add(clockWorkServerJobExecutingTypeInfoDTO);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("ClockWorkServerJobServiceManager::GetClockWorkServerJobTypes: {0}", ex), ex);
					}
				}
			}
			return new GetClockWorkServerJobTypesResp
			{
				ClockWorkServerJobTypeList = list2
			};
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000985C File Offset: 0x00007A5C
		public RunClockWorkServerJobNowResp RunClockWorkServerJobNow(RunClockWorkServerJobNowReq request)
		{
			IClockWorkServerJobManager clockWorkServerJobManager = new ClockWorkServerJobManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			clockWorkServerJobManager.RunClockWorkServerJobNow(request.JobId);
			return new RunClockWorkServerJobNowResp();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000988C File Offset: 0x00007A8C
		public EnableClockWorkServerJobResp EnableClockWorkServerJob(EnableClockWorkServerJobReq request)
		{
			IClockWorkServerJobManager clockWorkServerJobManager = new ClockWorkServerJobManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			clockWorkServerJobManager.EnableClockWorkServerJob(request.JobId);
			return new EnableClockWorkServerJobResp();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000098BC File Offset: 0x00007ABC
		public DisableClockWorkServerJobResp DisableClockWorkServerJob(DisableClockWorkServerJobReq request)
		{
			IClockWorkServerJobManager clockWorkServerJobManager = new ClockWorkServerJobManager(request.GetOperationContext<ClockWorkServerOperationContext>());
			clockWorkServerJobManager.DisableClockWorkServerJob(request.JobId);
			return new DisableClockWorkServerJobResp();
		}
	}
}

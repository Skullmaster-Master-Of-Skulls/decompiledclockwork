using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob;
using TechnoPro.Common.Core.ClockWorkDailyJob;
using TechnoPro.Common.Core.Mappers.ClockWorkDailyJob;
using TechnoPro.Common.ICore.ClockWorkDailyJob;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkDailyJob;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200002C RID: 44
	public class DailyJobServiceManager : IDailyJob, IService
	{
		// Token: 0x060001CD RID: 461 RVA: 0x000091DC File Offset: 0x000073DC
		public RunDailyJobResp RunDailyJob(RunDailyJobReq Request)
		{
			IDailyJobManager dailyJobManager = new DailyJobManager(Request.GetOperationContext());
			IList<DailyJobTaskResult> list = dailyJobManager.RunDailyJob(Request.GroupId);
			RunDailyJobResp runDailyJobResp = new RunDailyJobResp();
			IList<DailyJobTaskResultDTO> dailyJobResults;
			if (list != null)
			{
				dailyJobResults = list.ToList<DailyJobTaskResult>().ConvertAll<DailyJobTaskResultDTO>((DailyJobTaskResult f) => f.ToDTO());
			}
			else
			{
				dailyJobResults = null;
			}
			runDailyJobResp.DailyJobResults = dailyJobResults;
			return runDailyJobResp;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009244 File Offset: 0x00007444
		public CreateDailyJobTaskResp CreateDailyJobTask(CreateDailyJobTaskReq Request)
		{
			IDailyJobManager dailyJobManager = new DailyJobManager(Request.GetOperationContext());
			int windowsJobTaskId = dailyJobManager.CreateDailyJobTask(Request.Task.ToDomainObject());
			return new CreateDailyJobTaskResp
			{
				WindowsJobTaskId = windowsJobTaskId
			};
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00009284 File Offset: 0x00007484
		public void UpdateDailyJobTask(UpdateDailyJobTaskReq Request)
		{
			IDailyJobManager dailyJobManager = new DailyJobManager(Request.GetOperationContext());
			dailyJobManager.UpdateDailyJobTask(Request.DailyJobTask.ToDomainObject());
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000092B0 File Offset: 0x000074B0
		public void ChangeTaskActiveStatus(ChangeTaskActiveStatusReq Request)
		{
			IDailyJobManager dailyJobManager = new DailyJobManager(Request.GetOperationContext());
			dailyJobManager.ChangeTaskActiveStatus(Request.WindowsTaskJobId, Request.NewIsActive);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000092E0 File Offset: 0x000074E0
		public LoadDailyJobTasksByGroupResp LoadDailyJobTasksByGroup(LoadDailyJobTasksByGroupReq Request)
		{
			IDailyJobManager dailyJobManager = new DailyJobManager(Request.GetOperationContext());
			IList<DailyJobTask> list = dailyJobManager.LoadDailyJobTasksByGroup(Request.TaskGroupId);
			LoadDailyJobTasksByGroupResp loadDailyJobTasksByGroupResp = new LoadDailyJobTasksByGroupResp();
			IList<DailyJobTaskDTO> dailyJobResults;
			if (list != null)
			{
				dailyJobResults = list.ToList<DailyJobTask>().ConvertAll<DailyJobTaskDTO>((DailyJobTask f) => f.ToDTO());
			}
			else
			{
				dailyJobResults = null;
			}
			loadDailyJobTasksByGroupResp.DailyJobResults = dailyJobResults;
			return loadDailyJobTasksByGroupResp;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009348 File Offset: 0x00007548
		public LoadDailyJobTaskByIdResp LoadDailyJobTaskById(LoadDailyJobTaskByIdReq Request)
		{
			IDailyJobManager dailyJobManager = new DailyJobManager(Request.GetOperationContext());
			DailyJobTask dailyJobTask = dailyJobManager.LoadDailyJobTaskById(Request.WindowsTaskJobId);
			return new LoadDailyJobTaskByIdResp
			{
				DailyJobResults = ((dailyJobTask == null) ? null : dailyJobTask.ToDTO())
			};
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000938C File Offset: 0x0000758C
		public void DeleteDailyJobTask(DeleteDailyJobTaskReq Request)
		{
			IDailyJobManager dailyJobManager = new DailyJobManager(Request.GetOperationContext());
			dailyJobManager.DeleteDailyJobTask(Request.WindowsTaskJobId);
		}
	}
}

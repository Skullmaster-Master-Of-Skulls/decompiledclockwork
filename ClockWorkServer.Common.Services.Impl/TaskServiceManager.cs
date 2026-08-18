using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Core.Mappers.Tasks;
using TechnoPro.Common.Core.Tasks;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000094 RID: 148
	public class TaskServiceManager : ITask, IService
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x00018744 File Offset: 0x00016944
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00018758 File Offset: 0x00016958
		public CreateTaskResp CreateTask(CreateTaskReq Request)
		{
			ITaskManager taskManager = new TaskManager(Request.GetOperationContext());
			return new CreateTaskResp
			{
				TaskId = taskManager.CreateTask(Request.Task.ToDomainObject())
			};
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00018794 File Offset: 0x00016994
		public void DeleteTask(DeleteTaskReq Request)
		{
			ITaskManager taskManager = new TaskManager(Request.GetOperationContext());
			taskManager.DeleteTask(Request.TaskId);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000187BC File Offset: 0x000169BC
		public void UpdateTask(UpdateTaskReq Request)
		{
			ITaskManager taskManager = new TaskManager(Request.GetOperationContext());
			taskManager.UpdateTask(Request.Task.ToDomainObject());
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000187E8 File Offset: 0x000169E8
		public void ChangeTaskCompletedStatus(ChangeTaskCompletedStatusReq Request)
		{
			ITaskManager taskManager = new TaskManager(Request.GetOperationContext());
			taskManager.ChangeTaskCompletedStatus(Request.TaskId, Request.NewCompletedStatus);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00018818 File Offset: 0x00016A18
		public LoadTasksResp LoadTasks(LoadTasksReq Request)
		{
			ITaskManager taskManager = new TaskManager(Request.GetOperationContext());
			List<Task> list = taskManager.LoadTasks(Request.IncludePrivateTasks, Request.IncludeSharedTasks, Request.IncludeAssignedTasks, (eTaskPart)Request.TaskParts);
			LoadTasksResp loadTasksResp = new LoadTasksResp();
			List<TaskDTO> tasks;
			if (list == null)
			{
				tasks = null;
			}
			else
			{
				tasks = list.ConvertAll<TaskDTO>((Task f) => f.ToDTO());
			}
			loadTasksResp.Tasks = tasks;
			return loadTasksResp;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001888C File Offset: 0x00016A8C
		public LoadCompletedTasksResp LoadCompletedTasks(LoadCompletedTasksReq Request)
		{
			ITaskManager taskManager = new TaskManager(Request.GetOperationContext());
			List<Task> list = taskManager.LoadCompletedTasks(Request.IncludePrivateTasks, Request.IncludeSharedTasks, Request.IncludeAssignedTasks, Request.StartDate, Request.EndDate);
			LoadCompletedTasksResp loadCompletedTasksResp = new LoadCompletedTasksResp();
			List<TaskDTO> tasks;
			if (list == null)
			{
				tasks = null;
			}
			else
			{
				tasks = list.ConvertAll<TaskDTO>((Task f) => f.ToDTO());
			}
			loadCompletedTasksResp.Tasks = tasks;
			return loadCompletedTasksResp;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00018908 File Offset: 0x00016B08
		public LoadTaskByIdResp LoadTaskById(LoadTaskByIdReq Request)
		{
			ITaskManager taskManager = new TaskManager(Request.GetOperationContext());
			Task task = taskManager.LoadTaskById(Request.TaskId);
			return new LoadTaskByIdResp
			{
				Task = ((task != null) ? task.ToDTO() : null)
			};
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001894C File Offset: 0x00016B4C
		public void ChangeRemoveFromListStatus(ChangeRemoveFromListStatusReq Request)
		{
			ITaskManager taskManager = new TaskManager(Request.GetOperationContext());
			taskManager.ChangeRemoveFromListStatus(Request.TaskId, Request.NewRemoveFromListStatus);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001897C File Offset: 0x00016B7C
		public LoadTasksAsTreeResp LoadTasksAsTree(LoadTasksAsTreeReq Request)
		{
			ITaskManager taskManager = new TaskManager(Request.GetOperationContext());
			List<Task> list;
			List<TaskGroup> list2;
			Forest<TaskOrGroup> item = taskManager.LoadTasksAsTree(Request.IncludePrivateTasks, Request.IncludeSharedTasks, Request.IncludeAssignedTasks, (eTaskPart)Request.PartsToLoad, out list, out list2);
			LoadTasksAsTreeResp loadTasksAsTreeResp = new LoadTasksAsTreeResp();
			List<TaskDTO> tasks;
			if (list != null)
			{
				tasks = list.ConvertAll<TaskDTO>((Task f) => f.ToDTO());
			}
			else
			{
				tasks = null;
			}
			loadTasksAsTreeResp.Tasks = tasks;
			loadTasksAsTreeResp.Tree = item.ToDTO();
			List<TaskGroupDTO> groups;
			if (list2 != null)
			{
				groups = list2.ConvertAll<TaskGroupDTO>((TaskGroup f) => f.ToDTO());
			}
			else
			{
				groups = null;
			}
			loadTasksAsTreeResp.Groups = groups;
			return loadTasksAsTreeResp;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00018A38 File Offset: 0x00016C38
		public LoadCompletedTasksAsTreeResp LoadCompletedTasksAsTree(LoadCompletedTasksAsTreeReq Request)
		{
			ITaskManager taskManager = new TaskManager(Request.GetOperationContext());
			List<Task> list;
			List<TaskGroup> list2;
			Forest<TaskOrGroup> item = taskManager.LoadCompletedTasksAsTree(Request.IncludePrivateTasks, Request.IncludeSharedTasks, Request.IncludeAssignedTasks, Request.StartDate, Request.EndDate, out list, out list2);
			LoadCompletedTasksAsTreeResp loadCompletedTasksAsTreeResp = new LoadCompletedTasksAsTreeResp();
			List<TaskDTO> tasks;
			if (list != null)
			{
				tasks = list.ConvertAll<TaskDTO>((Task f) => f.ToDTO());
			}
			else
			{
				tasks = null;
			}
			loadCompletedTasksAsTreeResp.Tasks = tasks;
			loadCompletedTasksAsTreeResp.Tree = item.ToDTO();
			List<TaskGroupDTO> groups;
			if (list2 != null)
			{
				groups = list2.ConvertAll<TaskGroupDTO>((TaskGroup f) => f.ToDTO());
			}
			else
			{
				groups = null;
			}
			loadCompletedTasksAsTreeResp.Groups = groups;
			return loadCompletedTasksAsTreeResp;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00018AF8 File Offset: 0x00016CF8
		public LoadTaskNotesByTaskIdResp LoadTaskNotesByTaskId(LoadTaskNotesByTaskIdReq Request)
		{
			ITaskManager taskManager = new TaskManager(Request.GetOperationContext());
			List<TaskNote> list = taskManager.LoadTaskNotesByTaskId(Request.TaskId);
			LoadTaskNotesByTaskIdResp loadTaskNotesByTaskIdResp = new LoadTaskNotesByTaskIdResp();
			List<TaskNoteDTO> taskNotes;
			if (list == null)
			{
				taskNotes = null;
			}
			else
			{
				taskNotes = list.ConvertAll<TaskNoteDTO>((TaskNote f) => f.ToDTO());
			}
			loadTaskNotesByTaskIdResp.TaskNotes = taskNotes;
			return loadTaskNotesByTaskIdResp;
		}
	}
}

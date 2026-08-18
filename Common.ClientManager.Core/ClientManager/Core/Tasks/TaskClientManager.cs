using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Tasks;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Tasks
{
	// Token: 0x02000011 RID: 17
	public class TaskClientManager : ITaskClientManager, IWebService
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000041EC File Offset: 0x000023EC
		public IList<TaskDTO> LoadTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPartDTO PartsToLoad)
		{
			LoadTasksReq loadTasksReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTasksReq>();
			loadTasksReq.IncludePrivateTasks = IncludePrivateTasks;
			loadTasksReq.IncludeSharedTasks = IncludeSharedTasks;
			loadTasksReq.IncludeAssignedTasks = IncludeAssignedTasks;
			loadTasksReq.TaskParts = PartsToLoad;
			return ClientServiceFactory.GetClientInstance<ITask>().LoadTasks(loadTasksReq).Tasks;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000423C File Offset: 0x0000243C
		public IList<TaskDTO> LoadCompletedTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate)
		{
			LoadCompletedTasksReq loadCompletedTasksReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCompletedTasksReq>();
			loadCompletedTasksReq.IncludePrivateTasks = IncludePrivateTasks;
			loadCompletedTasksReq.IncludeSharedTasks = IncludeSharedTasks;
			loadCompletedTasksReq.IncludeAssignedTasks = IncludeAssignedTasks;
			loadCompletedTasksReq.StartDate = StartDate;
			loadCompletedTasksReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<ITask>().LoadCompletedTasks(loadCompletedTasksReq).Tasks;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004294 File Offset: 0x00002494
		public Forest<TaskOrGroupDTO> LoadTasksAsTree(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPartDTO PartsToLoad, out List<TaskDTO> Tasks, out List<TaskGroupDTO> Groups)
		{
			LoadTasksAsTreeReq loadTasksAsTreeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTasksAsTreeReq>();
			loadTasksAsTreeReq.IncludePrivateTasks = IncludePrivateTasks;
			loadTasksAsTreeReq.IncludeSharedTasks = IncludeSharedTasks;
			loadTasksAsTreeReq.IncludeAssignedTasks = IncludeAssignedTasks;
			loadTasksAsTreeReq.PartsToLoad = PartsToLoad;
			LoadTasksAsTreeResp loadTasksAsTreeResp = ClientServiceFactory.GetClientInstance<ITask>().LoadTasksAsTree(loadTasksAsTreeReq);
			Tasks = loadTasksAsTreeResp.Tasks;
			Groups = loadTasksAsTreeResp.Groups;
			return loadTasksAsTreeResp.Tree;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000042F8 File Offset: 0x000024F8
		public Forest<TaskOrGroupDTO> LoadCompletedTasksAsTree(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate, out List<TaskDTO> Tasks, out List<TaskGroupDTO> Groups)
		{
			LoadCompletedTasksAsTreeReq loadCompletedTasksAsTreeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCompletedTasksAsTreeReq>();
			loadCompletedTasksAsTreeReq.IncludePrivateTasks = IncludePrivateTasks;
			loadCompletedTasksAsTreeReq.IncludeSharedTasks = IncludeSharedTasks;
			loadCompletedTasksAsTreeReq.IncludeAssignedTasks = IncludeAssignedTasks;
			loadCompletedTasksAsTreeReq.StartDate = StartDate;
			loadCompletedTasksAsTreeReq.EndDate = EndDate;
			LoadCompletedTasksAsTreeResp loadCompletedTasksAsTreeResp = ClientServiceFactory.GetClientInstance<ITask>().LoadCompletedTasksAsTree(loadCompletedTasksAsTreeReq);
			Tasks = loadCompletedTasksAsTreeResp.Tasks;
			Groups = loadCompletedTasksAsTreeResp.Groups;
			return loadCompletedTasksAsTreeResp.Tree;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004364 File Offset: 0x00002564
		public int CreateTask(TaskDTO Task)
		{
			CreateTaskReq createTaskReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateTaskReq>();
			createTaskReq.Task = Task;
			return ClientServiceFactory.GetClientInstance<ITask>().CreateTask(createTaskReq).TaskId;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000439C File Offset: 0x0000259C
		public void DeleteTask(int TaskId)
		{
			DeleteTaskReq deleteTaskReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteTaskReq>();
			deleteTaskReq.TaskId = TaskId;
			ClientServiceFactory.GetClientInstance<ITask>().DeleteTask(deleteTaskReq);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000043CC File Offset: 0x000025CC
		public void UpdateTask(TaskDTO Task)
		{
			UpdateTaskReq updateTaskReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateTaskReq>();
			updateTaskReq.Task = Task;
			ClientServiceFactory.GetClientInstance<ITask>().UpdateTask(updateTaskReq);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000043FC File Offset: 0x000025FC
		public void ChangeTaskCompletedStatus(int TaskId, bool NewCompletedStatus)
		{
			ChangeTaskCompletedStatusReq changeTaskCompletedStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeTaskCompletedStatusReq>();
			changeTaskCompletedStatusReq.TaskId = TaskId;
			changeTaskCompletedStatusReq.NewCompletedStatus = NewCompletedStatus;
			ClientServiceFactory.GetClientInstance<ITask>().ChangeTaskCompletedStatus(changeTaskCompletedStatusReq);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004434 File Offset: 0x00002634
		public TaskDTO LoadTaskById(int TaskId)
		{
			LoadTaskByIdReq loadTaskByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTaskByIdReq>();
			loadTaskByIdReq.TaskId = TaskId;
			return ClientServiceFactory.GetClientInstance<ITask>().LoadTaskById(loadTaskByIdReq).Task;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000446C File Offset: 0x0000266C
		public void ChangeRemoveFromListStatus(int TaskId, bool NewRemoveFromListStatus)
		{
			ChangeRemoveFromListStatusReq changeRemoveFromListStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeRemoveFromListStatusReq>();
			changeRemoveFromListStatusReq.TaskId = TaskId;
			changeRemoveFromListStatusReq.NewRemoveFromListStatus = NewRemoveFromListStatus;
			ClientServiceFactory.GetClientInstance<ITask>().ChangeRemoveFromListStatus(changeRemoveFromListStatusReq);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000044A4 File Offset: 0x000026A4
		public IList<TaskNoteDTO> LoadTaskNotesByTaskId(int TaskId)
		{
			LoadTaskNotesByTaskIdReq loadTaskNotesByTaskIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTaskNotesByTaskIdReq>();
			loadTaskNotesByTaskIdReq.TaskId = TaskId;
			return ClientServiceFactory.GetClientInstance<ITask>().LoadTaskNotesByTaskId(loadTaskNotesByTaskIdReq).TaskNotes;
		}
	}
}

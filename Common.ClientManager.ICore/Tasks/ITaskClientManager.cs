using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Tasks
{
	// Token: 0x0200000E RID: 14
	public interface ITaskClientManager : IWebService
	{
		// Token: 0x06000056 RID: 86
		IList<TaskDTO> LoadTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPartDTO PartsToLoad);

		// Token: 0x06000057 RID: 87
		IList<TaskDTO> LoadCompletedTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000058 RID: 88
		Forest<TaskOrGroupDTO> LoadTasksAsTree(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPartDTO PartsToLoad, out List<TaskDTO> Tasks, out List<TaskGroupDTO> Groups);

		// Token: 0x06000059 RID: 89
		Forest<TaskOrGroupDTO> LoadCompletedTasksAsTree(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate, out List<TaskDTO> Tasks, out List<TaskGroupDTO> Groups);

		// Token: 0x0600005A RID: 90
		int CreateTask(TaskDTO Task);

		// Token: 0x0600005B RID: 91
		void DeleteTask(int TaskId);

		// Token: 0x0600005C RID: 92
		void UpdateTask(TaskDTO Task);

		// Token: 0x0600005D RID: 93
		void ChangeTaskCompletedStatus(int TaskId, bool NewCompletedStatus);

		// Token: 0x0600005E RID: 94
		TaskDTO LoadTaskById(int TaskId);

		// Token: 0x0600005F RID: 95
		void ChangeRemoveFromListStatus(int TaskId, bool NewRemoveFromListStatus);

		// Token: 0x06000060 RID: 96
		IList<TaskNoteDTO> LoadTaskNotesByTaskId(int TaskId);
	}
}

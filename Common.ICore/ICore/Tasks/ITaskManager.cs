using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.ICore.Tasks
{
	// Token: 0x02000024 RID: 36
	public interface ITaskManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000EC RID: 236
		List<Task> LoadTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPart PartsToLoad);

		// Token: 0x060000ED RID: 237
		List<Task> LoadCompletedTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate);

		// Token: 0x060000EE RID: 238
		Forest<TaskOrGroup> LoadTasksAsTree(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPart PartsToLoad, out List<Task> Tasks, out List<TaskGroup> Groups);

		// Token: 0x060000EF RID: 239
		Forest<TaskOrGroup> LoadCompletedTasksAsTree(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate, out List<Task> Tasks, out List<TaskGroup> Groups);

		// Token: 0x060000F0 RID: 240
		int CreateTask(Task Task);

		// Token: 0x060000F1 RID: 241
		void DeleteTask(int TaskId);

		// Token: 0x060000F2 RID: 242
		void UpdateTask(Task Task);

		// Token: 0x060000F3 RID: 243
		void ChangeTaskCompletedStatus(int TaskId, bool NewCompletedStatus);

		// Token: 0x060000F4 RID: 244
		Task LoadTaskById(int TaskId);

		// Token: 0x060000F5 RID: 245
		void ChangeRemoveFromListStatus(int TaskId, bool NewRemoveFromListStatus);

		// Token: 0x060000F6 RID: 246
		List<TaskNote> LoadTaskNotesByTaskId(int TaskId);
	}
}

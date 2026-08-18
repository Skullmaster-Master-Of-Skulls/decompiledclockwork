using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.DAO.Tasks
{
	// Token: 0x02000024 RID: 36
	public interface ITaskDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000089 RID: 137
		List<Task> LoadTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPart PartsToLoad);

		// Token: 0x0600008A RID: 138
		List<Task> LoadCompletedTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate);

		// Token: 0x0600008B RID: 139
		int CreateTask(Task Task);

		// Token: 0x0600008C RID: 140
		void DeleteTask(int TaskId);

		// Token: 0x0600008D RID: 141
		void UpdateTask(Task Task);

		// Token: 0x0600008E RID: 142
		void ChangeTaskCompletedStatus(int TaskId, bool NewCompletedStatus);

		// Token: 0x0600008F RID: 143
		Task LoadTaskById(int TaskId);

		// Token: 0x06000090 RID: 144
		void ChangeRemoveFromListStatus(int TaskId, bool NewRemoveFromListStatus);

		// Token: 0x06000091 RID: 145
		List<TaskNote> LoadTaskNotesByTaskId(int TaskId);
	}
}

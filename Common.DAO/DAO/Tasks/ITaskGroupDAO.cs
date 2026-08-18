using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.DAO.Tasks
{
	// Token: 0x02000025 RID: 37
	public interface ITaskGroupDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000092 RID: 146
		int CreateNewTaskGroup(TaskGroup Group);

		// Token: 0x06000093 RID: 147
		void DeleteTaskGroup(int TaskGroupId);

		// Token: 0x06000094 RID: 148
		void UpdateTaskGroup(TaskGroup Group);

		// Token: 0x06000095 RID: 149
		List<TaskGroup> LoadGroups(bool IncludePrivate, bool IncludeShared);
	}
}

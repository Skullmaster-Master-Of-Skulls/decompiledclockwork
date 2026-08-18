using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.ICore.Tasks
{
	// Token: 0x02000023 RID: 35
	public interface ITaskGroupManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000E8 RID: 232
		int CreateNewTaskGroup(TaskGroup Group);

		// Token: 0x060000E9 RID: 233
		void DeleteTaskGroup(int TaskGroupId);

		// Token: 0x060000EA RID: 234
		void UpdateTaskGroup(TaskGroup Group);

		// Token: 0x060000EB RID: 235
		List<TaskGroup> LoadGroups(bool IncludePrivate, bool IncludeShared);
	}
}

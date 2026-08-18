using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Tasks
{
	// Token: 0x0200000F RID: 15
	public interface ITaskGroupClientManager : IWebService
	{
		// Token: 0x06000061 RID: 97
		int CreateNewTaskGroup(TaskGroupDTO Group);

		// Token: 0x06000062 RID: 98
		void DeleteTaskGroup(int TaskGroupId);

		// Token: 0x06000063 RID: 99
		void UpdateTaskGroup(TaskGroupDTO Group);

		// Token: 0x06000064 RID: 100
		IList<TaskGroupDTO> LoadGroups(bool IncludePrivate, bool IncludeShared);
	}
}

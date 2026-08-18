using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Tasks
{
	// Token: 0x02000012 RID: 18
	public class TaskGroupClientManager : ITaskGroupClientManager, IWebService
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000044DC File Offset: 0x000026DC
		public int CreateNewTaskGroup(TaskGroupDTO Group)
		{
			CreateNewTaskGroupReq createNewTaskGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateNewTaskGroupReq>();
			createNewTaskGroupReq.TaskGroup = Group;
			return ClientServiceFactory.GetClientInstance<ITaskGroup>().CreateNewTaskGroup(createNewTaskGroupReq).TaskGroupId;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004514 File Offset: 0x00002714
		public void DeleteTaskGroup(int TaskGroupId)
		{
			DeleteTaskGroupReq deleteTaskGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteTaskGroupReq>();
			deleteTaskGroupReq.TaskGroupId = TaskGroupId;
			ClientServiceFactory.GetClientInstance<ITaskGroup>().DeleteTaskGroup(deleteTaskGroupReq);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004544 File Offset: 0x00002744
		public void UpdateTaskGroup(TaskGroupDTO Group)
		{
			UpdateTaskGroupReq updateTaskGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateTaskGroupReq>();
			updateTaskGroupReq.TaskGroup = Group;
			ClientServiceFactory.GetClientInstance<ITaskGroup>().UpdateTaskGroup(updateTaskGroupReq);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004574 File Offset: 0x00002774
		public IList<TaskGroupDTO> LoadGroups(bool IncludePrivate, bool IncludeShared)
		{
			LoadGroupsReq loadGroupsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadGroupsReq>();
			loadGroupsReq.IncludePrivate = IncludePrivate;
			loadGroupsReq.IncludeShared = IncludeShared;
			return ClientServiceFactory.GetClientInstance<ITaskGroup>().LoadGroups(loadGroupsReq).TaskGroups;
		}
	}
}

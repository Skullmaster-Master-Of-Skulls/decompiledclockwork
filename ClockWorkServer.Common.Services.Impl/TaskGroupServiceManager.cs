using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Core.Mappers.Tasks;
using TechnoPro.Common.Core.Tasks;
using TechnoPro.Common.ICore.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000093 RID: 147
	public class TaskGroupServiceManager : ITaskGroup, IService
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x00018640 File Offset: 0x00016840
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00018654 File Offset: 0x00016854
		public CreateNewTaskGroupResp CreateNewTaskGroup(CreateNewTaskGroupReq Request)
		{
			ITaskGroupManager taskGroupManager = new TaskGroupManager(Request.GetOperationContext());
			return new CreateNewTaskGroupResp
			{
				TaskGroupId = taskGroupManager.CreateNewTaskGroup(Request.TaskGroup.ToDomainObject())
			};
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00018690 File Offset: 0x00016890
		public void DeleteTaskGroup(DeleteTaskGroupReq Request)
		{
			ITaskGroupManager taskGroupManager = new TaskGroupManager(Request.GetOperationContext());
			taskGroupManager.DeleteTaskGroup(Request.TaskGroupId);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000186B8 File Offset: 0x000168B8
		public void UpdateTaskGroup(UpdateTaskGroupReq Request)
		{
			ITaskGroupManager taskGroupManager = new TaskGroupManager(Request.GetOperationContext());
			taskGroupManager.UpdateTaskGroup(Request.TaskGroup.ToDomainObject());
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000186E4 File Offset: 0x000168E4
		public LoadGroupsResp LoadGroups(LoadGroupsReq Request)
		{
			ITaskGroupManager taskGroupManager = new TaskGroupManager(Request.GetOperationContext());
			LoadGroupsResp loadGroupsResp = new LoadGroupsResp();
			loadGroupsResp.TaskGroups = taskGroupManager.LoadGroups(Request.IncludePrivate, Request.IncludeShared).ConvertAll<TaskGroupDTO>((TaskGroup f) => f.ToDTO());
			return loadGroupsResp;
		}
	}
}

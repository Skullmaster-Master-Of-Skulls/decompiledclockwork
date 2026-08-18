using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200014E RID: 334
	internal class TaskGroupClientBaseProxy : ClientBase<ITaskGroup>, ITaskGroup, IService
	{
		// Token: 0x06000CC8 RID: 3272 RVA: 0x0001FCC5 File Offset: 0x0001DEC5
		public TaskGroupClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0001FCD0 File Offset: 0x0001DED0
		public TaskGroupClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0001FCDC File Offset: 0x0001DEDC
		public CreateNewTaskGroupResp CreateNewTaskGroup(CreateNewTaskGroupReq Request)
		{
			return base.Channel.CreateNewTaskGroup(Request);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0001FCFA File Offset: 0x0001DEFA
		public void DeleteTaskGroup(DeleteTaskGroupReq Request)
		{
			base.Channel.DeleteTaskGroup(Request);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0001FD0C File Offset: 0x0001DF0C
		public LoadGroupsResp LoadGroups(LoadGroupsReq Request)
		{
			return base.Channel.LoadGroups(Request);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0001FD2A File Offset: 0x0001DF2A
		public void UpdateTaskGroup(UpdateTaskGroupReq Request)
		{
			base.Channel.UpdateTaskGroup(Request);
		}
	}
}

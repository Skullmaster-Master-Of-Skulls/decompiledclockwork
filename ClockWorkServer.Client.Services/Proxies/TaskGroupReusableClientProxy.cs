using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200014D RID: 333
	public class TaskGroupReusableClientProxy : WCFTokenBasedReusableClientProxy<ITaskGroup>, ITaskGroup, IService
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x0001FBCE File Offset: 0x0001DDCE
		public TaskGroupReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0001FBD9 File Offset: 0x0001DDD9
		public TaskGroupReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0001FBE8 File Offset: 0x0001DDE8
		public CreateNewTaskGroupResp CreateNewTaskGroup(CreateNewTaskGroupReq Request)
		{
			return this.WrapServiceMethod<CreateNewTaskGroupResp>(() => this.Proxy.CreateNewTaskGroup(Request));
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0001FC20 File Offset: 0x0001DE20
		public void DeleteTaskGroup(DeleteTaskGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteTaskGroup(Request);
			});
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0001FC58 File Offset: 0x0001DE58
		public LoadGroupsResp LoadGroups(LoadGroupsReq Request)
		{
			return this.WrapServiceMethod<LoadGroupsResp>(() => this.Proxy.LoadGroups(Request));
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0001FC90 File Offset: 0x0001DE90
		public void UpdateTaskGroup(UpdateTaskGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateTaskGroup(Request);
			});
		}
	}
}

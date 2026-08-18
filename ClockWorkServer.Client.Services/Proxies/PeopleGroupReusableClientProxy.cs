using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200010C RID: 268
	public class PeopleGroupReusableClientProxy : WCFTokenBasedReusableClientProxy<IPeopleGroup>, IPeopleGroup, IService
	{
		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001B0A6 File Offset: 0x000192A6
		public PeopleGroupReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001B0B1 File Offset: 0x000192B1
		public PeopleGroupReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001B0C0 File Offset: 0x000192C0
		public LoadUsersByGroupTitleResp LoadUsersByGroupTitle(LoadUsersByGroupTitleReq Request)
		{
			return this.WrapServiceMethod<LoadUsersByGroupTitleResp>(() => this.Proxy.LoadUsersByGroupTitle(Request));
		}
	}
}

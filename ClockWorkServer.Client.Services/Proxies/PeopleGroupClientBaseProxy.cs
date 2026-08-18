using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200010D RID: 269
	internal class PeopleGroupClientBaseProxy : ClientBase<IPeopleGroup>, IPeopleGroup, IService
	{
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001B0F8 File Offset: 0x000192F8
		public PeopleGroupClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001B103 File Offset: 0x00019303
		public PeopleGroupClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001B110 File Offset: 0x00019310
		public LoadUsersByGroupTitleResp LoadUsersByGroupTitle(LoadUsersByGroupTitleReq Request)
		{
			return base.Channel.LoadUsersByGroupTitle(Request);
		}
	}
}

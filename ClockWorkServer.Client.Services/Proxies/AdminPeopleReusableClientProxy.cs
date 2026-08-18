using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000101 RID: 257
	public class AdminPeopleReusableClientProxy : WCFTokenBasedReusableClientProxy<IAdminPeople>, IAdminPeople, IService
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x000199D2 File Offset: 0x00017BD2
		public AdminPeopleReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000199DD File Offset: 0x00017BDD
		public AdminPeopleReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000199EC File Offset: 0x00017BEC
		public LoadPersonResp LoadPersonWithGroups(LoadPersonReq Request)
		{
			return this.WrapServiceMethod<LoadPersonResp>(() => this.Proxy.LoadPersonWithGroups(Request));
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00019A24 File Offset: 0x00017C24
		public LoadGroupsByIdResp LoadGroupsById(LoadGroupsByIdReq Request)
		{
			return this.WrapServiceMethod<LoadGroupsByIdResp>(() => this.Proxy.LoadGroupsById(Request));
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00019A5C File Offset: 0x00017C5C
		public LoadAllGroupsResp LoadAllGroups(LoadAllGroupsReq Request)
		{
			return this.WrapServiceMethod<LoadAllGroupsResp>(() => this.Proxy.LoadAllGroups(Request));
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00019A94 File Offset: 0x00017C94
		public LoadPersonsByUsernameResp LoadPersonsByUsername(LoadPersonsByUsernameReq Request)
		{
			return this.WrapServiceMethod<LoadPersonsByUsernameResp>(() => this.Proxy.LoadPersonsByUsername(Request));
		}
	}
}

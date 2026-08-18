using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000103 RID: 259
	public class GroupReusableClientProxy : WCFTokenBasedReusableClientProxy<IGroup>, IGroup, IService
	{
		// Token: 0x06000A15 RID: 2581 RVA: 0x00019B62 File Offset: 0x00017D62
		public GroupReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00019B6D File Offset: 0x00017D6D
		public GroupReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00019B7C File Offset: 0x00017D7C
		public CreateGroupByTitleResp CreateGroupByTitle(CreateGroupByTitleReq Request)
		{
			return this.WrapServiceMethod<CreateGroupByTitleResp>(() => this.Proxy.CreateGroupByTitle(Request));
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00019BB4 File Offset: 0x00017DB4
		public LoadGroupByTitleResp LoadGroupByTitle(LoadGroupByTitleReq Request)
		{
			return this.WrapServiceMethod<LoadGroupByTitleResp>(() => this.Proxy.LoadGroupByTitle(Request));
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00019BEC File Offset: 0x00017DEC
		public LoadGroupByIdResp LoadGroupById(LoadGroupByIdReq Request)
		{
			return this.WrapServiceMethod<LoadGroupByIdResp>(() => this.Proxy.LoadGroupById(Request));
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00019C24 File Offset: 0x00017E24
		public LoadAllowedGroupsResp LoadAllowedGroups(LoadAllowedGroupsReq Request)
		{
			return this.WrapServiceMethod<LoadAllowedGroupsResp>(() => this.Proxy.LoadAllowedGroups(Request));
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00019C5C File Offset: 0x00017E5C
		public LoadAllGroupContainersResp LoadAllGroupContainers(LoadAllGroupContainersReq Request)
		{
			return this.WrapServiceMethod<LoadAllGroupContainersResp>(() => this.Proxy.LoadAllGroupContainers(Request));
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00019C94 File Offset: 0x00017E94
		public LoadAllGroupForEditsResp LoadAllGroupForEdits(LoadAllGroupForEditsReq Request)
		{
			return this.WrapServiceMethod<LoadAllGroupForEditsResp>(() => this.Proxy.LoadAllGroupForEdits(Request));
		}
	}
}

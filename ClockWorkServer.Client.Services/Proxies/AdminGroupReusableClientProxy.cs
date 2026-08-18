using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000FF RID: 255
	public class AdminGroupReusableClientProxy : WCFTokenBasedReusableClientProxy<IAdminGroup>, IAdminGroup, IService
	{
		// Token: 0x060009EF RID: 2543 RVA: 0x000195DA File Offset: 0x000177DA
		public AdminGroupReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000195E5 File Offset: 0x000177E5
		public AdminGroupReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000195F4 File Offset: 0x000177F4
		public LoadAllGroupsAndContainersResp LoadAllGroupsAndContainers(LoadAllGroupsAndContainersReq Request)
		{
			return this.WrapServiceMethod<LoadAllGroupsAndContainersResp>(() => this.Proxy.LoadAllGroupsAndContainers(Request));
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0001962C File Offset: 0x0001782C
		public AdminCreateGroupResp AdminCreateGroup(AdminCreateGroupReq Request)
		{
			return this.WrapServiceMethod<AdminCreateGroupResp>(() => this.Proxy.AdminCreateGroup(Request));
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00019664 File Offset: 0x00017864
		public AdminUpdateGroupResp AdminUpdateGroup(AdminUpdateGroupReq Request)
		{
			return this.WrapServiceMethod<AdminUpdateGroupResp>(() => this.Proxy.AdminUpdateGroup(Request));
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001969C File Offset: 0x0001789C
		public AdminDeleteGroupResp AdminDeleteGroup(AdminDeleteGroupReq Request)
		{
			return this.WrapServiceMethod<AdminDeleteGroupResp>(() => this.Proxy.AdminDeleteGroup(Request));
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000196D4 File Offset: 0x000178D4
		public UpdateGroupOrderResp UpdateGroupOrder(UpdateGroupOrderReq Request)
		{
			return this.WrapServiceMethod<UpdateGroupOrderResp>(() => this.Proxy.UpdateGroupOrder(Request));
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001970C File Offset: 0x0001790C
		public UpdateGroupContainerTitleResp UpdateGroupContainerTitle(UpdateGroupContainerTitleReq Request)
		{
			return this.WrapServiceMethod<UpdateGroupContainerTitleResp>(() => this.Proxy.UpdateGroupContainerTitle(Request));
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00019744 File Offset: 0x00017944
		public UpdateGroupOrdersResp UpdateGroupOrders(UpdateGroupOrdersReq Request)
		{
			return this.WrapServiceMethod<UpdateGroupOrdersResp>(() => this.Proxy.UpdateGroupOrders(Request));
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001977C File Offset: 0x0001797C
		public AddMembersToGroupResp AddMembersToGroup(AddMembersToGroupReq Request)
		{
			return this.WrapServiceMethod<AddMembersToGroupResp>(() => this.Proxy.AddMembersToGroup(Request));
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000197B4 File Offset: 0x000179B4
		public RemoveMembersFromGroupResp RemoveMembersFromGroup(RemoveMembersFromGroupReq Request)
		{
			return this.WrapServiceMethod<RemoveMembersFromGroupResp>(() => this.Proxy.RemoveMembersFromGroup(Request));
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000197EC File Offset: 0x000179EC
		public UpdateGroupsOrdersResp UpdateGroupsOrders(UpdateGroupsOrdersReq Request)
		{
			return this.WrapServiceMethod<UpdateGroupsOrdersResp>(() => this.Proxy.UpdateGroupsOrders(Request));
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00019824 File Offset: 0x00017A24
		public LoadGroupMemberCountResp LoadGroupMemberCount(LoadGroupMemberCountReq Request)
		{
			return this.WrapServiceMethod<LoadGroupMemberCountResp>(() => this.Proxy.LoadGroupMemberCount(Request));
		}
	}
}

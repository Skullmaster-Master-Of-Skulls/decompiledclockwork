using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000100 RID: 256
	internal class AdminGroupClientBaseProxy : ClientBase<IAdminGroup>, IAdminGroup, IService
	{
		// Token: 0x060009FC RID: 2556 RVA: 0x0001985C File Offset: 0x00017A5C
		public AdminGroupClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00019867 File Offset: 0x00017A67
		public AdminGroupClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00019874 File Offset: 0x00017A74
		public LoadAllGroupsAndContainersResp LoadAllGroupsAndContainers(LoadAllGroupsAndContainersReq Request)
		{
			return base.Channel.LoadAllGroupsAndContainers(Request);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00019894 File Offset: 0x00017A94
		public AdminCreateGroupResp AdminCreateGroup(AdminCreateGroupReq request)
		{
			return base.Channel.AdminCreateGroup(request);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000198B4 File Offset: 0x00017AB4
		public AdminUpdateGroupResp AdminUpdateGroup(AdminUpdateGroupReq request)
		{
			return base.Channel.AdminUpdateGroup(request);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000198D4 File Offset: 0x00017AD4
		public AdminDeleteGroupResp AdminDeleteGroup(AdminDeleteGroupReq request)
		{
			return base.Channel.AdminDeleteGroup(request);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000198F4 File Offset: 0x00017AF4
		public UpdateGroupOrderResp UpdateGroupOrder(UpdateGroupOrderReq request)
		{
			return base.Channel.UpdateGroupOrder(request);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00019914 File Offset: 0x00017B14
		public UpdateGroupContainerTitleResp UpdateGroupContainerTitle(UpdateGroupContainerTitleReq request)
		{
			return base.Channel.UpdateGroupContainerTitle(request);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00019934 File Offset: 0x00017B34
		public UpdateGroupOrdersResp UpdateGroupOrders(UpdateGroupOrdersReq Request)
		{
			return base.Channel.UpdateGroupOrders(Request);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00019954 File Offset: 0x00017B54
		public AddMembersToGroupResp AddMembersToGroup(AddMembersToGroupReq Request)
		{
			return base.Channel.AddMembersToGroup(Request);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00019974 File Offset: 0x00017B74
		public RemoveMembersFromGroupResp RemoveMembersFromGroup(RemoveMembersFromGroupReq Request)
		{
			return base.Channel.RemoveMembersFromGroup(Request);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00019994 File Offset: 0x00017B94
		public UpdateGroupsOrdersResp UpdateGroupsOrders(UpdateGroupsOrdersReq Request)
		{
			return base.Channel.UpdateGroupsOrders(Request);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000199B4 File Offset: 0x00017BB4
		public LoadGroupMemberCountResp LoadGroupMemberCount(LoadGroupMemberCountReq Request)
		{
			return base.Channel.LoadGroupMemberCount(Request);
		}
	}
}

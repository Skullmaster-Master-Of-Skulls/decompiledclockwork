using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.People
{
	// Token: 0x0200002B RID: 43
	public class AdminGroupClientManager : IAdminGroupClientManager, IWebService
	{
		// Token: 0x06000162 RID: 354 RVA: 0x000078F4 File Offset: 0x00005AF4
		public LoadAllGroupsAndContainersResp LoadAllGroupsAndContainers()
		{
			LoadAllGroupsAndContainersReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllGroupsAndContainersReq>();
			LoadAllGroupsAndContainersResp loadAllGroupsAndContainersResp = ClientServiceFactory.GetClientInstance<IAdminGroup>().LoadAllGroupsAndContainers(request);
			return new LoadAllGroupsAndContainersResp
			{
				Groups = ((loadAllGroupsAndContainersResp != null) ? loadAllGroupsAndContainersResp.Groups : null),
				GroupContainers = ((loadAllGroupsAndContainersResp != null) ? loadAllGroupsAndContainersResp.GroupContainers : null)
			};
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007948 File Offset: 0x00005B48
		public int CreateGroup(GroupDTO group)
		{
			AdminCreateGroupReq adminCreateGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AdminCreateGroupReq>();
			adminCreateGroupReq.Group = group;
			AdminCreateGroupResp adminCreateGroupResp = ClientServiceFactory.GetClientInstance<IAdminGroup>().AdminCreateGroup(adminCreateGroupReq);
			return (adminCreateGroupResp != null) ? adminCreateGroupResp.GroupId : 0;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007988 File Offset: 0x00005B88
		public void UpdateGroup(GroupDTO group)
		{
			AdminUpdateGroupReq adminUpdateGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AdminUpdateGroupReq>();
			adminUpdateGroupReq.Group = group;
			ClientServiceFactory.GetClientInstance<IAdminGroup>().AdminUpdateGroup(adminUpdateGroupReq);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000079B8 File Offset: 0x00005BB8
		public void DeleteGroup(int groupId)
		{
			AdminDeleteGroupReq adminDeleteGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AdminDeleteGroupReq>();
			adminDeleteGroupReq.GroupId = groupId;
			ClientServiceFactory.GetClientInstance<IAdminGroup>().AdminDeleteGroup(adminDeleteGroupReq);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000079E8 File Offset: 0x00005BE8
		public void UpdateGroupOrder(int groupId, int newOrderNum)
		{
			UpdateGroupOrderReq updateGroupOrderReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateGroupOrderReq>();
			updateGroupOrderReq.GroupId = groupId;
			updateGroupOrderReq.NewOrderNum = newOrderNum;
			ClientServiceFactory.GetClientInstance<IAdminGroup>().UpdateGroupOrder(updateGroupOrderReq);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007A20 File Offset: 0x00005C20
		public void UpdateGroupContainerTitle(string oldContainerTitle, string newContainerTitle)
		{
			UpdateGroupContainerTitleReq updateGroupContainerTitleReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateGroupContainerTitleReq>();
			updateGroupContainerTitleReq.OldContainerTitle = oldContainerTitle;
			updateGroupContainerTitleReq.NewContainerTitle = newContainerTitle;
			ClientServiceFactory.GetClientInstance<IAdminGroup>().UpdateGroupContainerTitle(updateGroupContainerTitleReq);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007A58 File Offset: 0x00005C58
		public void AddMembersToGroup(int groupId, IEnumerable<int> pids)
		{
			AddMembersToGroupReq addMembersToGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddMembersToGroupReq>();
			addMembersToGroupReq.GroupId = groupId;
			addMembersToGroupReq.PersonIds = ((pids != null) ? pids.ToArray<int>() : null);
			ClientServiceFactory.GetClientInstance<IAdminGroup>().AddMembersToGroup(addMembersToGroupReq);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007A98 File Offset: 0x00005C98
		public void RemoveMembersFromGroup(int groupId, IEnumerable<int> pids)
		{
			RemoveMembersFromGroupReq removeMembersFromGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveMembersFromGroupReq>();
			removeMembersFromGroupReq.GroupId = groupId;
			removeMembersFromGroupReq.PersonIds = ((pids != null) ? pids.ToArray<int>() : null);
			ClientServiceFactory.GetClientInstance<IAdminGroup>().RemoveMembersFromGroup(removeMembersFromGroupReq);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public int LoadGroupMemberCount(int groupId)
		{
			LoadGroupMemberCountReq loadGroupMemberCountReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadGroupMemberCountReq>();
			loadGroupMemberCountReq.GroupId = groupId;
			LoadGroupMemberCountResp loadGroupMemberCountResp = ClientServiceFactory.GetClientInstance<IAdminGroup>().LoadGroupMemberCount(loadGroupMemberCountReq);
			return (loadGroupMemberCountResp != null) ? loadGroupMemberCountResp.GroupMemberCount : -1;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007B18 File Offset: 0x00005D18
		public void UpdateGroupsOrders(IDictionary<int, int> groupidsWithOrderNums)
		{
			UpdateGroupsOrdersReq updateGroupsOrdersReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateGroupsOrdersReq>();
			updateGroupsOrdersReq.GroupIdsWithOrderNums = groupidsWithOrderNums;
			ClientServiceFactory.GetClientInstance<IAdminGroup>().UpdateGroupsOrders(updateGroupsOrdersReq);
		}
	}
}

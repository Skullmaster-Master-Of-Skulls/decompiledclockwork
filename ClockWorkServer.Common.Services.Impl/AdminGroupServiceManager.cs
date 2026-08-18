using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000072 RID: 114
	public class AdminGroupServiceManager : IAdminGroup, IService
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x00014280 File Offset: 0x00012480
		public LoadAllGroupsAndContainersResp LoadAllGroupsAndContainers(LoadAllGroupsAndContainersReq request)
		{
			IAdminGroupManager adminGroupManager = new AdminGroupManager(request.GetOperationContext());
			GroupsAndContainers groupsAndContainers = adminGroupManager.LoadAllGroupsAndContainers();
			LoadAllGroupsAndContainersResp loadAllGroupsAndContainersResp = new LoadAllGroupsAndContainersResp();
			IList<GroupDTO> groups;
			if (groupsAndContainers == null)
			{
				groups = null;
			}
			else
			{
				IList<Group> groups2 = groupsAndContainers.Groups;
				if (groups2 == null)
				{
					groups = null;
				}
				else
				{
					groups = (from g in groups2
					select g.ToDTO()).ToList<GroupDTO>();
				}
			}
			loadAllGroupsAndContainersResp.Groups = groups;
			IList<GroupContainerDTO> groupContainers;
			if (groupsAndContainers == null)
			{
				groupContainers = null;
			}
			else
			{
				IList<GroupContainer> groupContainers2 = groupsAndContainers.GroupContainers;
				if (groupContainers2 == null)
				{
					groupContainers = null;
				}
				else
				{
					groupContainers = (from g in groupContainers2
					select g.ToDTO()).ToList<GroupContainerDTO>();
				}
			}
			loadAllGroupsAndContainersResp.GroupContainers = groupContainers;
			return loadAllGroupsAndContainersResp;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00014330 File Offset: 0x00012530
		public AdminCreateGroupResp AdminCreateGroup(AdminCreateGroupReq request)
		{
			IAdminGroupManager adminGroupManager = new AdminGroupManager(request.GetOperationContext());
			IAdminGroupManager adminGroupManager2 = adminGroupManager;
			GroupDTO group = request.Group;
			int groupId = adminGroupManager2.CreateGroup((group != null) ? group.ToDomainObject() : null);
			return new AdminCreateGroupResp
			{
				GroupId = groupId
			};
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00014374 File Offset: 0x00012574
		public AdminUpdateGroupResp AdminUpdateGroup(AdminUpdateGroupReq request)
		{
			IAdminGroupManager adminGroupManager = new AdminGroupManager(request.GetOperationContext());
			IAdminGroupManager adminGroupManager2 = adminGroupManager;
			GroupDTO group = request.Group;
			adminGroupManager2.UpdateGroup((group != null) ? group.ToDomainObject() : null);
			return new AdminUpdateGroupResp();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000143B0 File Offset: 0x000125B0
		public AdminDeleteGroupResp AdminDeleteGroup(AdminDeleteGroupReq request)
		{
			IAdminGroupManager adminGroupManager = new AdminGroupManager(request.GetOperationContext());
			adminGroupManager.DeleteGroup(request.GroupId);
			return new AdminDeleteGroupResp();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000143E0 File Offset: 0x000125E0
		public UpdateGroupOrderResp UpdateGroupOrder(UpdateGroupOrderReq request)
		{
			IAdminGroupManager adminGroupManager = new AdminGroupManager(request.GetOperationContext());
			adminGroupManager.UpdateGroupOrder(request.GroupId, request.NewOrderNum);
			return new UpdateGroupOrderResp();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00014418 File Offset: 0x00012618
		public UpdateGroupContainerTitleResp UpdateGroupContainerTitle(UpdateGroupContainerTitleReq request)
		{
			IAdminGroupManager adminGroupManager = new AdminGroupManager(request.GetOperationContext());
			adminGroupManager.UpdateGroupContainerTitle(request.OldContainerTitle, request.NewContainerTitle);
			return new UpdateGroupContainerTitleResp();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00014450 File Offset: 0x00012650
		public UpdateGroupOrdersResp UpdateGroupOrders(UpdateGroupOrdersReq request)
		{
			IAdminGroupManager adminGroupManager = new AdminGroupManager(request.GetOperationContext());
			foreach (KeyValuePair<int, int> keyValuePair in request.GroupIdsWithNewOrderNum)
			{
				adminGroupManager.UpdateGroupOrder(keyValuePair.Key, keyValuePair.Value);
			}
			return new UpdateGroupOrdersResp();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000144C4 File Offset: 0x000126C4
		public AddMembersToGroupResp AddMembersToGroup(AddMembersToGroupReq Request)
		{
			IAdminGroupManager adminGroupManager = new AdminGroupManager(Request.GetOperationContext());
			adminGroupManager.AddMembersToGroup(Request.GroupId, Request.PersonIds);
			return new AddMembersToGroupResp();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000144FC File Offset: 0x000126FC
		public RemoveMembersFromGroupResp RemoveMembersFromGroup(RemoveMembersFromGroupReq Request)
		{
			IAdminGroupManager adminGroupManager = new AdminGroupManager(Request.GetOperationContext());
			adminGroupManager.RemoveMembersFromGroup(Request.GroupId, Request.PersonIds);
			return new RemoveMembersFromGroupResp();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00014534 File Offset: 0x00012734
		public UpdateGroupsOrdersResp UpdateGroupsOrders(UpdateGroupsOrdersReq Request)
		{
			IAdminGroupManager adminGroupManager = new AdminGroupManager(Request.GetOperationContext());
			adminGroupManager.UpdateGroupsOrders(Request.GroupIdsWithOrderNums);
			return new UpdateGroupsOrdersResp();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00014564 File Offset: 0x00012764
		public LoadGroupMemberCountResp LoadGroupMemberCount(LoadGroupMemberCountReq Request)
		{
			IAdminGroupManager adminGroupManager = new AdminGroupManager(Request.GetOperationContext());
			int groupMemberCount = adminGroupManager.LoadGroupMemberCount(Request.GroupId);
			return new LoadGroupMemberCountResp
			{
				GroupMemberCount = groupMemberCount
			};
		}
	}
}

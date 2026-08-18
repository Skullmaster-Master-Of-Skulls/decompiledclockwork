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
	// Token: 0x02000075 RID: 117
	public class GroupServiceManager : IGroup, IService
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x0001478C File Offset: 0x0001298C
		public LoadGroupByTitleResp LoadGroupByTitle(LoadGroupByTitleReq Request)
		{
			IGroupManager groupManager = new GroupManager(Request.GetOperationContext());
			Group group = groupManager.LoadGroupByTitle(Request.GroupTitle);
			bool flag = group == null && !string.IsNullOrEmpty(Request.AlternateGroupTitle);
			if (flag)
			{
				group = groupManager.LoadGroupByTitle(Request.AlternateGroupTitle);
			}
			return new LoadGroupByTitleResp
			{
				Group = ((group == null) ? null : group.ToDTO())
			};
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000147F4 File Offset: 0x000129F4
		public CreateGroupByTitleResp CreateGroupByTitle(CreateGroupByTitleReq Request)
		{
			IGroupManager groupManager = new GroupManager(Request.GetOperationContext());
			int groupId = groupManager.CreateGroupByTitle(Request.GroupTitle);
			return new CreateGroupByTitleResp
			{
				GroupId = groupId
			};
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001482C File Offset: 0x00012A2C
		public LoadGroupByIdResp LoadGroupById(LoadGroupByIdReq Request)
		{
			IGroupManager groupManager = new GroupManager(Request.GetOperationContext());
			Group group = groupManager.LoadGroupById(Request.GroupId);
			return new LoadGroupByIdResp
			{
				Group = ((group == null) ? null : group.ToDTO())
			};
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00014870 File Offset: 0x00012A70
		public LoadAllowedGroupsResp LoadAllowedGroups(LoadAllowedGroupsReq Request)
		{
			IGroupManager groupManager = new GroupManager(Request.GetOperationContext());
			IList<Group> list = groupManager.LoadAllowedGroups(Request.OnlyReturnVisibleInCalendarGroups);
			LoadAllowedGroupsResp loadAllowedGroupsResp = new LoadAllowedGroupsResp();
			IList<GroupDTO> groups;
			if (list == null)
			{
				groups = null;
			}
			else
			{
				groups = (from h in list
				select h.ToDTO()).ToList<GroupDTO>();
			}
			loadAllowedGroupsResp.Groups = groups;
			return loadAllowedGroupsResp;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000148D8 File Offset: 0x00012AD8
		public LoadAllGroupContainersResp LoadAllGroupContainers(LoadAllGroupContainersReq Request)
		{
			IGroupManager groupManager = new GroupManager(Request.GetOperationContext());
			IList<GroupContainer> list = groupManager.LoadAllGroupContainers();
			LoadAllGroupContainersResp loadAllGroupContainersResp = new LoadAllGroupContainersResp();
			IList<GroupContainerDTO> groupContainers;
			if (list == null)
			{
				groupContainers = null;
			}
			else
			{
				groupContainers = (from h in list
				select h.ToDTO()).ToList<GroupContainerDTO>();
			}
			loadAllGroupContainersResp.GroupContainers = groupContainers;
			return loadAllGroupContainersResp;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001493C File Offset: 0x00012B3C
		public LoadAllGroupForEditsResp LoadAllGroupForEdits(LoadAllGroupForEditsReq Request)
		{
			IGroupManager groupManager = new GroupManager(Request.GetOperationContext());
			IList<GroupForEdit> list = groupManager.LoadAllGroupForEdits();
			LoadAllGroupForEditsResp loadAllGroupForEditsResp = new LoadAllGroupForEditsResp();
			IList<GroupForEditDTO> allGroupForEdits;
			if (list == null)
			{
				allGroupForEdits = null;
			}
			else
			{
				allGroupForEdits = (from h in list
				select h.ToDTO()).ToList<GroupForEditDTO>();
			}
			loadAllGroupForEditsResp.AllGroupForEdits = allGroupForEdits;
			return loadAllGroupForEditsResp;
		}
	}
}

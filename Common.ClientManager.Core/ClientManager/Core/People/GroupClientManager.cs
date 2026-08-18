using System;
using System.Collections.Generic;
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
	// Token: 0x0200002D RID: 45
	public class GroupClientManager : IGroupClientManager, IWebService
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00007C28 File Offset: 0x00005E28
		public GroupDTO LoadGroupByTitle(string groupTitle, string altGroupTitle)
		{
			LoadGroupByTitleReq loadGroupByTitleReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadGroupByTitleReq>();
			loadGroupByTitleReq.GroupTitle = groupTitle;
			loadGroupByTitleReq.AlternateGroupTitle = altGroupTitle;
			return ClientServiceFactory.GetClientInstance<IGroup>().LoadGroupByTitle(loadGroupByTitleReq).Group;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007C68 File Offset: 0x00005E68
		public int CreateGroupByTitle(string groupTitle)
		{
			CreateGroupByTitleReq createGroupByTitleReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateGroupByTitleReq>();
			createGroupByTitleReq.GroupTitle = groupTitle;
			return ClientServiceFactory.GetClientInstance<IGroup>().CreateGroupByTitle(createGroupByTitleReq).GroupId;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public GroupDTO LoadGroupById(int GroupId)
		{
			LoadGroupByIdReq loadGroupByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadGroupByIdReq>();
			loadGroupByIdReq.GroupId = GroupId;
			return ClientServiceFactory.GetClientInstance<IGroup>().LoadGroupById(loadGroupByIdReq).Group;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007CD8 File Offset: 0x00005ED8
		public IList<GroupDTO> LoadAllowedGroups(bool OnlyReturnVisibleInCalendarGroups)
		{
			LoadAllowedGroupsReq loadAllowedGroupsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllowedGroupsReq>();
			loadAllowedGroupsReq.OnlyReturnVisibleInCalendarGroups = OnlyReturnVisibleInCalendarGroups;
			return ClientServiceFactory.GetClientInstance<IGroup>().LoadAllowedGroups(loadAllowedGroupsReq).Groups;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007D10 File Offset: 0x00005F10
		public IList<GroupContainerDTO> LoadAllGroupContainers()
		{
			LoadAllGroupContainersReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllGroupContainersReq>();
			return ClientServiceFactory.GetClientInstance<IGroup>().LoadAllGroupContainers(request).GroupContainers;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007D40 File Offset: 0x00005F40
		public IList<GroupForEditDTO> LoadAllGroupForEdits()
		{
			LoadAllGroupForEditsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllGroupForEditsReq>();
			return ClientServiceFactory.GetClientInstance<IGroup>().LoadAllGroupForEdits(request).AllGroupForEdits;
		}
	}
}

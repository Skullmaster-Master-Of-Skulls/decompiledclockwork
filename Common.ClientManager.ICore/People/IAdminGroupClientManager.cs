using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.People
{
	// Token: 0x02000027 RID: 39
	public interface IAdminGroupClientManager : IWebService
	{
		// Token: 0x060000FD RID: 253
		LoadAllGroupsAndContainersResp LoadAllGroupsAndContainers();

		// Token: 0x060000FE RID: 254
		int CreateGroup(GroupDTO group);

		// Token: 0x060000FF RID: 255
		void UpdateGroup(GroupDTO group);

		// Token: 0x06000100 RID: 256
		void DeleteGroup(int groupId);

		// Token: 0x06000101 RID: 257
		void UpdateGroupOrder(int groupId, int newOrderNum);

		// Token: 0x06000102 RID: 258
		void UpdateGroupContainerTitle(string oldContainerTitle, string newContainerTitle);

		// Token: 0x06000103 RID: 259
		void AddMembersToGroup(int groupId, IEnumerable<int> pids);

		// Token: 0x06000104 RID: 260
		void RemoveMembersFromGroup(int groupId, IEnumerable<int> pids);

		// Token: 0x06000105 RID: 261
		int LoadGroupMemberCount(int groupId);

		// Token: 0x06000106 RID: 262
		void UpdateGroupsOrders(IDictionary<int, int> groupidsWithOrderNums);
	}
}

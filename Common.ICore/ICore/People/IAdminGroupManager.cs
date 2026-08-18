using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.People
{
	// Token: 0x0200004E RID: 78
	public interface IAdminGroupManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001E8 RID: 488
		GroupsAndContainers LoadAllGroupsAndContainers();

		// Token: 0x060001E9 RID: 489
		int CreateGroup(Group group);

		// Token: 0x060001EA RID: 490
		void UpdateGroup(Group group);

		// Token: 0x060001EB RID: 491
		void DeleteGroup(int groupId);

		// Token: 0x060001EC RID: 492
		void UpdateGroupOrder(int groupId, int newOrderNum);

		// Token: 0x060001ED RID: 493
		void UpdateGroupContainerTitle(string oldContainerTitle, string newContainerTitle);

		// Token: 0x060001EE RID: 494
		void AddMembersToGroup(int groupId, int[] pids);

		// Token: 0x060001EF RID: 495
		void RemoveMembersFromGroup(int groupId, int[] pids);

		// Token: 0x060001F0 RID: 496
		int LoadGroupMemberCount(int groupId);

		// Token: 0x060001F1 RID: 497
		void UpdateGroupsOrders(IDictionary<int, int> groupIdsWithOrderNums);
	}
}

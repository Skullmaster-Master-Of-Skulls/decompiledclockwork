using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.People
{
	// Token: 0x0200003D RID: 61
	public interface IAdminGroupDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600011D RID: 285
		int CreateGroup(Group group);

		// Token: 0x0600011E RID: 286
		void UpdateGroup(Group group);

		// Token: 0x0600011F RID: 287
		void DeleteGroup(int groupId);

		// Token: 0x06000120 RID: 288
		void UpdateGroupOrder(int groupId, int newOrderNum);

		// Token: 0x06000121 RID: 289
		void UpdateGroupContainerTitle(string oldContainerTitle, string newContainerTitle);

		// Token: 0x06000122 RID: 290
		void AddMembersToGroup(int groupId, int[] pids);

		// Token: 0x06000123 RID: 291
		void RemoveMembersFromGroup(int groupId, int[] pids);

		// Token: 0x06000124 RID: 292
		IList<PersonBase> LoadGroupMembers(bool onlyShowDeleted, params int[] gids);
	}
}

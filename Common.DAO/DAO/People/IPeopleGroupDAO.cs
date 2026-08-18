using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.People
{
	// Token: 0x0200003F RID: 63
	public interface IPeopleGroupDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600012B RID: 299
		IList<int> GetGroupIdsByPersonId(int PersonId);

		// Token: 0x0600012C RID: 300
		Task<IList<int>> GetGroupIdsByPersonIdAsync(int PersonId);

		// Token: 0x0600012D RID: 301
		IList<Group> LoadGroupsById(params int[] GroupIds);

		// Token: 0x0600012E RID: 302
		IList<Group> LoadAllGroups();

		// Token: 0x0600012F RID: 303
		int LoadGroupMemberCount(int groupId);
	}
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.People
{
	// Token: 0x02000051 RID: 81
	public interface IPeopleGroupManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001FE RID: 510
		IList<int> GetGroupIdsByPersonId(int PersonId);

		// Token: 0x060001FF RID: 511
		Task<IList<int>> GetGroupIdsByPersonIdAsync(int PersonId);

		// Token: 0x06000200 RID: 512
		bool IsAdmin(int personId);

		// Token: 0x06000201 RID: 513
		bool HasManageUserRoomPermissions(int personId);

		// Token: 0x06000202 RID: 514
		IList<PersonBase> LoadUsersByGroupTitle(string GroupTitle, string AlternateGroupTitle);

		// Token: 0x06000203 RID: 515
		IList<PersonBase> LoadusersByGroupTitleAndPersonIdList(IList<int> PersonIds, string GroupTitle, string AlternateGroupTitle);

		// Token: 0x06000204 RID: 516
		int LoadGroupMemberCount(int groupId);
	}
}

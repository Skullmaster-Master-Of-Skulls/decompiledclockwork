using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.People
{
	// Token: 0x0200004F RID: 79
	public interface IAdminPeopleManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001F2 RID: 498
		PersonBase LoadPersonWithGroups(int PersonId);

		// Token: 0x060001F3 RID: 499
		IList<Group> LoadGroupsById(params int[] GroupIds);

		// Token: 0x060001F4 RID: 500
		IList<Group> LoadAllGroups();

		// Token: 0x060001F5 RID: 501
		IList<PersonBase> LoadPersonsByUsername(string Username, bool includeDeletedAccounts = false);

		// Token: 0x060001F6 RID: 502
		PersonBase LoadAnyNonDeletedAccountByStudentNumber(string studentNumber);
	}
}

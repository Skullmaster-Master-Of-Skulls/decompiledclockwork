using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.People
{
	// Token: 0x02000028 RID: 40
	public interface IAdminPeopleClientManager : IWebService
	{
		// Token: 0x06000107 RID: 263
		PersonBaseDTO LoadPersonWithGroups(int PersonId);

		// Token: 0x06000108 RID: 264
		IList<GroupDTO> LoadGroupsById(IList<int> GroupIds);

		// Token: 0x06000109 RID: 265
		IList<GroupDTO> LoadAllGroups();

		// Token: 0x0600010A RID: 266
		IList<PersonBaseDTO> LoadPersonsByUsername(string Username, bool includeDeletedAccounts = false);
	}
}

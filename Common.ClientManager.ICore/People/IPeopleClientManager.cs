using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ClientManager.ICore.People
{
	// Token: 0x0200002A RID: 42
	public interface IPeopleClientManager : IWebService
	{
		// Token: 0x06000111 RID: 273
		int CreateUser(PersonBaseDTO User, List<int> GroupIds);

		// Token: 0x06000112 RID: 274
		PersonBaseDTO LoadPerson(int PersonId);

		// Token: 0x06000113 RID: 275
		PersonBaseDTO LoadPersonByStudentNumber(string Student_No, bool checkIfWhoamiIsAllowToSeeThisStudent = false);

		// Token: 0x06000114 RID: 276
		bool IsStudentsAccommodationsExpired(int PersonId);

		// Token: 0x06000115 RID: 277
		IList<PersonBaseDTO> LoadGroupMembers(params int[] GroupIds);

		// Token: 0x06000116 RID: 278
		IList<GroupDTO> LoadGroups();

		// Token: 0x06000117 RID: 279
		PersonBaseDTO LoadPersonById(int PersonId);

		// Token: 0x06000118 RID: 280
		IList<PersonBaseDTO> LoadStaff();

		// Token: 0x06000119 RID: 281
		IList<PersonBaseDTO> FindStudentBySearchString(string SearchString);

		// Token: 0x0600011A RID: 282
		FindUserGroupObjectBySearchStringResp FindUserGroupObjectBySearchString(string searchString, int startIndex, int maxResulsCount, params eUserGroupObjectType[] userGroupObjectTypes);

		// Token: 0x0600011B RID: 283
		int CreateGroup(GroupDTO Group);

		// Token: 0x0600011C RID: 284
		void DeleteGroup(int GroupId);

		// Token: 0x0600011D RID: 285
		void DeleteUser(int PersonId, bool JustDeactivate);

		// Token: 0x0600011E RID: 286
		void UpdateGroup(GroupDTO Group);

		// Token: 0x0600011F RID: 287
		void UpdateUser(PersonBaseDTO User);
	}
}

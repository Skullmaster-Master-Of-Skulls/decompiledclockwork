using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.People
{
	// Token: 0x0200002C RID: 44
	public interface IPersonBaseClientManager : IWebService
	{
		// Token: 0x06000121 RID: 289
		PersonBaseDTO LoadPersonByStudentNumber(string Student_no, bool CheckIfWhoAmIIsAllowedToSeeThisStudent, out bool WhoAmIIsAllowedToSeeThisStudent);

		// Token: 0x06000122 RID: 290
		PersonBaseDTO LoadPersonByStudentNumber(string Student_no, bool checkIfWhoAmIIsAllowedToSeeThisStudent);

		// Token: 0x06000123 RID: 291
		bool IsStudentsAccommodationsExpired(int personId);

		// Token: 0x06000124 RID: 292
		IList<PersonBaseDTO> GetStudents();

		// Token: 0x06000125 RID: 293
		IList<PersonBaseDTO> GetStaff();

		// Token: 0x06000126 RID: 294
		IList<PersonBaseDTO> GetRooms();

		// Token: 0x06000127 RID: 295
		IList<PersonBaseDTO> GetResources();

		// Token: 0x06000128 RID: 296
		IList<GroupDTO> GetGroups();

		// Token: 0x06000129 RID: 297
		IList<GroupDTO> GetRoomGroups();

		// Token: 0x0600012A RID: 298
		IList<PersonBaseDTO> LoadGroupMembers(int GroupId);

		// Token: 0x0600012B RID: 299
		IList<PersonBaseDTO> LoadGroupMembers(int[] GroupIds);

		// Token: 0x0600012C RID: 300
		PersonBaseDTO LoadPerson(int PersonId);

		// Token: 0x0600012D RID: 301
		int CreateUser(PersonBaseDTO User, List<int> GroupIds);

		// Token: 0x0600012E RID: 302
		PersonBaseDTO LoadStudentByStudent_No(string student_no, out bool whoAmIIsAllowedToSeeThisStudent);

		// Token: 0x0600012F RID: 303
		void UpdateUser(PersonBaseDTO user, bool UpdateGroupMemberships = true);

		// Token: 0x06000130 RID: 304
		PersonBaseWithExtendedInfoDTO LoadPersonWithExtendedInfo(int Personid);

		// Token: 0x06000131 RID: 305
		IList<PersonBaseDTO> LoadPersonsByIds(IList<int> PersonIds);

		// Token: 0x06000132 RID: 306
		string GetTempStudentNumber(string Prefix, string PostFix);

		// Token: 0x06000133 RID: 307
		IList<PersonBaseDTO> LoadDeletedAccounts(params int[] GroupIds);
	}
}

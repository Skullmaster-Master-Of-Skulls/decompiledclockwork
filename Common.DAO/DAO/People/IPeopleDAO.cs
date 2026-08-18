using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.People
{
	// Token: 0x02000044 RID: 68
	public interface IPeopleDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600013C RID: 316
		List<PersonBase> LoadGroupMembers(int GroupId);

		// Token: 0x0600013D RID: 317
		List<PersonBase> LoadGroupMembers(int[] GroupIds);

		// Token: 0x0600013E RID: 318
		List<PersonBase> LoadGroupMembersByPersonIds(int[] GroupIds, IList<int> PersonIds);

		// Token: 0x0600013F RID: 319
		PersonBase LoadPerson(int PersonId);

		// Token: 0x06000140 RID: 320
		PersonBase LoadPersonByStudentNumber(string Student_No);

		// Token: 0x06000141 RID: 321
		List<PersonBase> LoadAllUserObjects(bool LoadIsActivatedStatusForStudents = false);

		// Token: 0x06000142 RID: 322
		List<PersonBase> LoadAllUserObjectsAndBiggestPid(out int BiggestPid, bool LoadIsActivatedStatusForStudents = false);

		// Token: 0x06000143 RID: 323
		DateTime? GetStudentAccommodationExpiryDate(int PersonId);

		// Token: 0x06000144 RID: 324
		List<int> LoadAllowedStudentPids();

		// Token: 0x06000145 RID: 325
		List<Group> LoadAllGroups();

		// Token: 0x06000146 RID: 326
		int CreateUser(PersonBase User, List<int> GroupIds);

		// Token: 0x06000147 RID: 327
		List<int> LoadAllowedStudentPids(string studentSpecificSql, List<int> gids, bool useRestrictive);

		// Token: 0x06000148 RID: 328
		List<int> LoadAllowedStaffPids(List<int> gids, bool useRestrictive);

		// Token: 0x06000149 RID: 329
		List<int> LoadAllowedRoomPids(List<int> gids, bool useRestrictive);

		// Token: 0x0600014A RID: 330
		List<int> LoadAllowedResourcePids(List<int> gids, bool useRestrictive);

		// Token: 0x0600014B RID: 331
		List<Group> LoadAllRoomGroups();

		// Token: 0x0600014C RID: 332
		DateTime GetPersonDateAdded(int PersonId);

		// Token: 0x0600014D RID: 333
		PersonBase LoadStudentByEmail(string Email, int ControlId, bool EmailIsEncrypted);

		// Token: 0x0600014E RID: 334
		void UpdateUser(PersonBase User);

		// Token: 0x0600014F RID: 335
		int CreateGroup(Group Group);

		// Token: 0x06000150 RID: 336
		void UpdateGroup(Group Group);

		// Token: 0x06000151 RID: 337
		bool DeleteGroup(int GroupId);

		// Token: 0x06000152 RID: 338
		bool DeleteUser(int PersonId, bool JustDeactivate);

		// Token: 0x06000153 RID: 339
		PersonBase UnDeleteUser(int PersonId);

		// Token: 0x06000154 RID: 340
		IList<Group> LoadUserGroupMemberships(int PersonId);

		// Token: 0x06000155 RID: 341
		void AddUserToGroups(int PersonId, IList<int> GroupIds);

		// Token: 0x06000156 RID: 342
		IList<PersonBase> LoadPersonsByIds(IList<int> PersonIds);

		// Token: 0x06000157 RID: 343
		int GetLastPersonIdAddedToClockWork();

		// Token: 0x06000158 RID: 344
		IList<int> GetPidsGreaterThan(int pid);

		// Token: 0x06000159 RID: 345
		IList<int> LoadPersonIdsByStudentNumbers(IList<string> StudentNumbers);

		// Token: 0x0600015A RID: 346
		IList<PersonBase> LoadPersonsByStudentNumber(string Student_No);

		// Token: 0x0600015B RID: 347
		void RemoveUserFromGroups(int PersonId, IList<int> GroupIds);

		// Token: 0x0600015C RID: 348
		PersonBaseWithExtendedInfo LoadPersonWithExtendedInfo(int PersonId);

		// Token: 0x0600015D RID: 349
		bool IsUserInGroup(int PersonId, int GroupId);

		// Token: 0x0600015E RID: 350
		string GetTempStudentNumber();

		// Token: 0x0600015F RID: 351
		IDictionary<string, int> LoadPersonIdsByStudentNumbers2(IList<string> StudentNumbers);

		// Token: 0x06000160 RID: 352
		IList<PersonBase> LoadDeletedAccounts(params int[] GroupIds);

		// Token: 0x06000161 RID: 353
		PersonBase LoadAnyNonDeletedAccountByStudentNumber(string snum);
	}
}

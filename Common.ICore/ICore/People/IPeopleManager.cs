using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.People
{
	// Token: 0x02000056 RID: 86
	public interface IPeopleManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600021E RID: 542
		bool IsPersonInCoreGroup(PersonBase Person, eCoreGroup CoreGroups);

		// Token: 0x0600021F RID: 543
		void AddPersonToCoreGroup(PersonBase Person, eCoreGroup CoreGroup);

		// Token: 0x06000220 RID: 544
		string GetStudentName(PersonBase Person);

		// Token: 0x06000221 RID: 545
		List<PersonBase> LoadGroupMembers(int[] GroupIds);

		// Token: 0x06000222 RID: 546
		List<PersonBase> LoadGroupMembers(int GroupId);

		// Token: 0x06000223 RID: 547
		List<PersonBase> LoadGroupMembersByPersonIds(int GroupId, IList<int> PersonIds);

		// Token: 0x06000224 RID: 548
		List<PersonBase> LoadAllUserObjects(bool CheckForNewStudents = true);

		// Token: 0x06000225 RID: 549
		PersonBase LoadPerson(int PersonId);

		// Token: 0x06000226 RID: 550
		PersonBase LoadPersonByStudentNumber(string Student_No, out bool WhoAmIIsAllowedToSeeThisStudent, bool CheckIfWhoAmIIsAllowedToSeeThisStudent = false);

		// Token: 0x06000227 RID: 551
		PersonBase LoadPersonByStudentNumber(string Student_No);

		// Token: 0x06000228 RID: 552
		DateTime? GetStudentAccommodationExpiryDate(int PersonId);

		// Token: 0x06000229 RID: 553
		bool IsStudentsAccommodationsExpired(int PersonId);

		// Token: 0x0600022A RID: 554
		List<PersonBase> LoadStudents();

		// Token: 0x0600022B RID: 555
		List<PersonBase> LoadStaff();

		// Token: 0x0600022C RID: 556
		List<PersonBase> LoadRooms();

		// Token: 0x0600022D RID: 557
		List<PersonBase> LoadResources();

		// Token: 0x0600022E RID: 558
		List<Group> LoadGroups();

		// Token: 0x0600022F RID: 559
		List<Group> LoadRoomGroups();

		// Token: 0x06000230 RID: 560
		int CreateUser(PersonBase User, List<int> GroupIds);

		// Token: 0x06000231 RID: 561
		DateTime GetPersonDateAdded(int PersonId);

		// Token: 0x06000232 RID: 562
		IList<PersonBase> FindStudentBySearchString(string searchString);

		// Token: 0x06000233 RID: 563
		IList<UserGroupObject> FindUserGroupObjectBySearchString(string SearchString, eUserGroupObjectType[] ObjectTypesToExclude, int StartIndex, int MaxResultsCount, out int TotalResultsCount);

		// Token: 0x06000234 RID: 564
		void UpdateUser(PersonBase User, bool UpdateGroupMemberships);

		// Token: 0x06000235 RID: 565
		int CreateGroup(Group Group);

		// Token: 0x06000236 RID: 566
		void UpdateGroup(Group Group);

		// Token: 0x06000237 RID: 567
		void DeleteGroup(int GroupId);

		// Token: 0x06000238 RID: 568
		void DeleteUser(int PersonId, bool JustDeactivate);

		// Token: 0x06000239 RID: 569
		PersonBase UnDeleteUser(int PersonId);

		// Token: 0x0600023A RID: 570
		IList<Group> LoadUserGroupMemberships(int PersonId);

		// Token: 0x0600023B RID: 571
		void AddUserToGroups(int PersonId, IList<int> GroupIds);

		// Token: 0x0600023C RID: 572
		IList<PersonBase> LoadPersonsByIds(IList<int> PersonIds);

		// Token: 0x0600023D RID: 573
		IList<int> LoadPersonIdsByStudentNumbers(IList<string> StudentNumbers);

		// Token: 0x0600023E RID: 574
		void RemoveUserFromGroups(int PersonId, IList<int> GroupIds);

		// Token: 0x0600023F RID: 575
		PersonBaseWithExtendedInfo LoadPersonWithExtendedInfo(int PersonId);

		// Token: 0x06000240 RID: 576
		bool IsUserInGroup(int PersonId, int GroupId);

		// Token: 0x06000241 RID: 577
		bool IsPersonInAtLeastOneCoreGroup(int personId, params eCoreGroup[] coreGroups);

		// Token: 0x06000242 RID: 578
		bool IsPersonInAtLeastOneCoreGroup(int[] personGroupIds, params eCoreGroup[] coreGroups);

		// Token: 0x06000243 RID: 579
		string GetTempStudentNumber(string prefix, string postfix);

		// Token: 0x06000244 RID: 580
		IDictionary<string, int> LoadPersonIdsByStudentNumbers2(IList<string> StudentNumbers);

		// Token: 0x06000245 RID: 581
		IList<PersonBase> LoadDeletedAccounts(params int[] GroupIds);

		// Token: 0x06000246 RID: 582
		List<int> LoadAllowedStudentPids();
	}
}

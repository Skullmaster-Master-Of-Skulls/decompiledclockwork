using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000077 RID: 119
	public class PeopleServiceManager : IPeople, IService
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x00014A10 File Offset: 0x00012C10
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00014A24 File Offset: 0x00012C24
		public LoadPersonByStudentNumberResp LoadPersonByStudentNumber(LoadPersonByStudentNumberReq request)
		{
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			bool whoAmIIsAllowedToSeeThisStudent;
			PersonBase personBase = peopleManager.LoadPersonByStudentNumber(request.Student_no, out whoAmIIsAllowedToSeeThisStudent, request.CheckIfWhoAmIIsAllowedToSeeThisStudent);
			return new LoadPersonByStudentNumberResp
			{
				Person = personBase.ToDTO(),
				WhoAmIIsAllowedToSeeThisStudent = whoAmIIsAllowedToSeeThisStudent
			};
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00014A74 File Offset: 0x00012C74
		public LoadPersonResp LoadPerson(LoadPersonReq request)
		{
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			PersonBase personBase = peopleManager.LoadPerson(request.PersonId);
			return new LoadPersonResp
			{
				Person = personBase.ToDTO()
			};
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00014AB4 File Offset: 0x00012CB4
		public IsStudentsAccommodationsExpiredResp IsStudentsAccommodationsExpired(IsStudentsAccommodationsExpiredReq request)
		{
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			bool isExpired = peopleManager.IsStudentsAccommodationsExpired(request.PersonId);
			return new IsStudentsAccommodationsExpiredResp
			{
				IsExpired = isExpired
			};
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00014AEC File Offset: 0x00012CEC
		public LoadGroupsResp LoadGroups(LoadGroupsReq request)
		{
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			List<Group> list = peopleManager.LoadGroups();
			LoadGroupsResp loadGroupsResp = new LoadGroupsResp();
			loadGroupsResp.Groups = list.ConvertAll<GroupDTO>((Group g) => g.ToDTO());
			return loadGroupsResp;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00014B44 File Offset: 0x00012D44
		public AddPersonResp AddPersonToCoreGroup(AddPersonReq request)
		{
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			peopleManager.AddPersonToCoreGroup(request.Person.ToDomainObject(), (eCoreGroup)request.CoreGroup);
			return new AddPersonResp();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00014B80 File Offset: 0x00012D80
		public StudentNameResp GetStudentName(StudentNameReq request)
		{
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			string studentName = peopleManager.GetStudentName(request.Person.ToDomainObject());
			return new StudentNameResp
			{
				StudentName = studentName
			};
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00014BC0 File Offset: 0x00012DC0
		public LoadGroupMembersResp LoadGroupMembers(LoadGroupMembersReq request)
		{
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			List<PersonBase> list = peopleManager.LoadGroupMembers(request.GroupId);
			List<PersonBaseDTO> groupMembers = list.ConvertAll<PersonBaseDTO>((PersonBase m) => m.ToDTO());
			return new LoadGroupMembersResp
			{
				GroupMembers = groupMembers
			};
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00014C20 File Offset: 0x00012E20
		public LoadGroupMembersResp LoadMultipleGroupMembers(LoadMultipleGroupMembersReq request)
		{
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			List<PersonBase> list = peopleManager.LoadGroupMembers(request.GroupIds);
			List<PersonBaseDTO> groupMembers = list.ConvertAll<PersonBaseDTO>((PersonBase m) => m.ToDTO());
			return new LoadGroupMembersResp
			{
				GroupMembers = groupMembers
			};
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00014C80 File Offset: 0x00012E80
		public CreateUserResp CreateUser(CreateUserReq request)
		{
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			int personId = peopleManager.CreateUser(request.User.ToDomainObject(), request.GroupIds);
			return new CreateUserResp
			{
				PersonId = personId
			};
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00014CC4 File Offset: 0x00012EC4
		public LoadAllRoomGroupsResp LoadAllRoomGroups(LoadAllRoomGroupsReq request)
		{
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			List<Group> list = peopleManager.LoadRoomGroups();
			LoadAllRoomGroupsResp loadAllRoomGroupsResp = new LoadAllRoomGroupsResp();
			loadAllRoomGroupsResp.Groups = list.ConvertAll<GroupDTO>((Group g) => g.ToDTO());
			return loadAllRoomGroupsResp;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00014D1C File Offset: 0x00012F1C
		public LoadStaffResp LoadStaff(LoadStaffReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			List<PersonBase> list = peopleManager.LoadStaff();
			LoadStaffResp loadStaffResp = new LoadStaffResp();
			loadStaffResp.People = list.ConvertAll<PersonBaseDTO>((PersonBase f) => f.ToDTO());
			return loadStaffResp;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00014D74 File Offset: 0x00012F74
		public LoadStudentsResp LoadStudents(LoadStudentsReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			List<PersonBase> list = peopleManager.LoadStudents();
			LoadStudentsResp loadStudentsResp = new LoadStudentsResp();
			loadStudentsResp.People = list.ConvertAll<PersonBaseDTO>((PersonBase f) => f.ToDTO());
			return loadStudentsResp;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00014DCC File Offset: 0x00012FCC
		public LoadRoomsResp LoadRooms(LoadRoomsReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			List<PersonBase> list = peopleManager.LoadRooms();
			LoadRoomsResp loadRoomsResp = new LoadRoomsResp();
			loadRoomsResp.People = list.ConvertAll<PersonBaseDTO>((PersonBase f) => f.ToDTO());
			return loadRoomsResp;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00014E24 File Offset: 0x00013024
		public LoadResourcesResp LoadResources(LoadResourcesReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			List<PersonBase> list = peopleManager.LoadResources();
			LoadResourcesResp loadResourcesResp = new LoadResourcesResp();
			loadResourcesResp.People = list.ConvertAll<PersonBaseDTO>((PersonBase f) => f.ToDTO());
			return loadResourcesResp;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00014E7C File Offset: 0x0001307C
		public FindStudentBySearchStringResp FindStudentBySearchString(FindStudentBySearchStringReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			IList<PersonBase> list = peopleManager.FindStudentBySearchString(Request.SearchString);
			FindStudentBySearchStringResp findStudentBySearchStringResp = new FindStudentBySearchStringResp();
			IList<PersonBaseDTO> students;
			if (list != null)
			{
				students = list.ToList<PersonBase>().ConvertAll<PersonBaseDTO>((PersonBase f) => f.ToDTO());
			}
			else
			{
				students = null;
			}
			findStudentBySearchStringResp.Students = students;
			return findStudentBySearchStringResp;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00014EE4 File Offset: 0x000130E4
		public FindUserGroupObjectBySearchStringResp FindUserGroupObjectBySearchString(FindUserGroupObjectBySearchStringReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			bool flag = Request.MaxResultsCount <= 0;
			if (flag)
			{
				Request.MaxResultsCount = 120;
			}
			int totalMatchesCount;
			IList<UserGroupObject> list = peopleManager.FindUserGroupObjectBySearchString(Request.SearchString, Request.ObjectTypesToExclude, Request.StartIndex, Request.MaxResultsCount, out totalMatchesCount);
			FindUserGroupObjectBySearchStringResp findUserGroupObjectBySearchStringResp = new FindUserGroupObjectBySearchStringResp();
			List<UserGroupObjectDTO> matches;
			if (list != null)
			{
				matches = list.ToList<UserGroupObject>().ConvertAll<UserGroupObjectDTO>((UserGroupObject f) => f.ToDTO());
			}
			else
			{
				matches = null;
			}
			findUserGroupObjectBySearchStringResp.Matches = matches;
			findUserGroupObjectBySearchStringResp.TotalMatchesCount = totalMatchesCount;
			return findUserGroupObjectBySearchStringResp;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00014F84 File Offset: 0x00013184
		public void UpdateUser(UpdateUserReq request)
		{
			IPeopleManager peopleManager = new PeopleManager(request.GetOperationContext());
			PersonBase personBase = request.User.ToDomainObject();
			bool flag = request.User.Groups == null;
			if (flag)
			{
				personBase.Groups = null;
			}
			peopleManager.UpdateUser(personBase, request.UpdateGroupMemberships);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00014FD4 File Offset: 0x000131D4
		public CreateGroupResp CreateGroup(CreateGroupReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			int groupId = peopleManager.CreateGroup(Request.Group.ToDomainObject());
			return new CreateGroupResp
			{
				GroupId = groupId
			};
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00015014 File Offset: 0x00013214
		public void UpdateGroup(UpdateGroupReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			peopleManager.UpdateGroup(Request.Group.ToDomainObject());
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00015040 File Offset: 0x00013240
		public void DeleteGroup(DeleteGroupReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			peopleManager.DeleteGroup(Request.GroupId);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00015068 File Offset: 0x00013268
		public void DeleteUser(DeleteUserReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			peopleManager.DeleteUser(Request.PersonId, Request.JustDeactivate);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00015098 File Offset: 0x00013298
		public UnDeleteUserResp UnDeleteUser(UnDeleteUserReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			PersonBase personBase = peopleManager.UnDeleteUser(Request.PersonId);
			return new UnDeleteUserResp
			{
				User = ((personBase == null) ? null : personBase.ToDTO())
			};
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000150DC File Offset: 0x000132DC
		public LoadPersonWithExtendedInfoResp LoadPersonWithExtendedInfo(LoadPersonWithExtendedInfoReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			PersonBaseWithExtendedInfo personBaseWithExtendedInfo = peopleManager.LoadPersonWithExtendedInfo(Request.PersonId);
			return new LoadPersonWithExtendedInfoResp
			{
				PersonWithExtendedInfo = ((personBaseWithExtendedInfo == null) ? null : personBaseWithExtendedInfo.ToDTO())
			};
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00015120 File Offset: 0x00013320
		public LoadPersonsByIdsResp LoadPersonsByIds(LoadPersonsByIdsReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			IList<PersonBase> list = peopleManager.LoadPersonsByIds((Request.PersonIds == null) ? null : Request.PersonIds.ToList<int>());
			LoadPersonsByIdsResp loadPersonsByIdsResp = new LoadPersonsByIdsResp();
			IList<PersonBaseDTO> persons;
			if (list != null)
			{
				persons = list.ToList<PersonBase>().ConvertAll<PersonBaseDTO>((PersonBase g) => g.ToDTO());
			}
			else
			{
				persons = null;
			}
			loadPersonsByIdsResp.Persons = persons;
			return loadPersonsByIdsResp;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00015198 File Offset: 0x00013398
		public GetTempStudentNumberResp GetTempStudentNumber(GetTempStudentNumberReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			string tempStudentNumber = peopleManager.GetTempStudentNumber(Request.Prefix, Request.Postfix);
			return new GetTempStudentNumberResp
			{
				TempStudentNumber = tempStudentNumber
			};
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000151D8 File Offset: 0x000133D8
		public LoadDeletedAccountsResp LoadDeletedAccounts(LoadDeletedAccountsReq Request)
		{
			IPeopleManager peopleManager = new PeopleManager(Request.GetOperationContext());
			IList<PersonBase> list = peopleManager.LoadDeletedAccounts(Request.GroupIds);
			LoadDeletedAccountsResp loadDeletedAccountsResp = new LoadDeletedAccountsResp();
			IList<PersonBaseDTO> userAccounts;
			if (list != null)
			{
				userAccounts = (from g in list
				select g.ToDTO()).ToList<PersonBaseDTO>();
			}
			else
			{
				userAccounts = null;
			}
			loadDeletedAccountsResp.UserAccounts = userAccounts;
			return loadDeletedAccountsResp;
		}
	}
}

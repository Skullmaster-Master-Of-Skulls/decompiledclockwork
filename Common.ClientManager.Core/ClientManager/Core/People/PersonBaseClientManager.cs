using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.People
{
	// Token: 0x02000030 RID: 48
	public class PersonBaseClientManager : IPersonBaseClientManager, IWebService
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000866C File Offset: 0x0000686C
		public void UpdateUser(PersonBaseDTO user, bool UpdateGroupMemberships = true)
		{
			UpdateUserReq updateUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateUserReq>();
			updateUserReq.UpdateGroupMemberships = UpdateGroupMemberships;
			updateUserReq.User = user;
			ClientServiceFactory.GetClientInstance<IPeople>().UpdateUser(updateUserReq);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000086A4 File Offset: 0x000068A4
		public PersonBaseDTO LoadPersonByStudentNumber(string Student_no, bool CheckIfWhoAmIIsAllowedToSeeThisStudent, out bool WhoAmIIsAllowedToSeeThisStudent)
		{
			LoadPersonByStudentNumberReq loadPersonByStudentNumberReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPersonByStudentNumberReq>();
			loadPersonByStudentNumberReq.Student_no = Student_no;
			loadPersonByStudentNumberReq.CheckIfWhoAmIIsAllowedToSeeThisStudent = CheckIfWhoAmIIsAllowedToSeeThisStudent;
			LoadPersonByStudentNumberResp loadPersonByStudentNumberResp = ClientServiceFactory.GetClientInstance<IPeople>().LoadPersonByStudentNumber(loadPersonByStudentNumberReq);
			WhoAmIIsAllowedToSeeThisStudent = loadPersonByStudentNumberResp.WhoAmIIsAllowedToSeeThisStudent;
			return loadPersonByStudentNumberResp.Person;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000086EC File Offset: 0x000068EC
		public PersonBaseDTO LoadPersonByStudentNumber(string Student_no, bool checkIfWhoAmIIsAllowedToSeeThisStudent)
		{
			bool flag;
			return this.LoadPersonByStudentNumber(Student_no, checkIfWhoAmIIsAllowedToSeeThisStudent, out flag);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008708 File Offset: 0x00006908
		public int CreateUser(PersonBaseDTO User, List<int> GroupIds)
		{
			CreateUserReq createUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateUserReq>();
			createUserReq.User = User;
			createUserReq.GroupIds = GroupIds;
			return ClientServiceFactory.GetClientInstance<IPeople>().CreateUser(createUserReq).PersonId;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008748 File Offset: 0x00006948
		public PersonBaseDTO LoadPerson(int personId)
		{
			LoadPersonReq loadPersonReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPersonReq>();
			loadPersonReq.PersonId = personId;
			return ClientServiceFactory.GetClientInstance<IPeople>().LoadPerson(loadPersonReq).Person;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008780 File Offset: 0x00006980
		public IList<PersonBaseDTO> LoadGroupMembers(int groupId)
		{
			LoadGroupMembersReq loadGroupMembersReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadGroupMembersReq>();
			loadGroupMembersReq.GroupId = groupId;
			return ClientServiceFactory.GetClientInstance<IPeople>().LoadGroupMembers(loadGroupMembersReq).GroupMembers;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000087B8 File Offset: 0x000069B8
		public IList<PersonBaseDTO> LoadGroupMembers(int[] GroupIds)
		{
			LoadMultipleGroupMembersReq loadMultipleGroupMembersReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMultipleGroupMembersReq>();
			loadMultipleGroupMembersReq.GroupIds = GroupIds;
			return ClientServiceFactory.GetClientInstance<IPeople>().LoadMultipleGroupMembers(loadMultipleGroupMembersReq).GroupMembers;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000087F0 File Offset: 0x000069F0
		public bool IsStudentsAccommodationsExpired(int personId)
		{
			IsStudentsAccommodationsExpiredReq isStudentsAccommodationsExpiredReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsStudentsAccommodationsExpiredReq>();
			isStudentsAccommodationsExpiredReq.PersonId = personId;
			return ClientServiceFactory.GetClientInstance<IPeople>().IsStudentsAccommodationsExpired(isStudentsAccommodationsExpiredReq).IsExpired;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008828 File Offset: 0x00006A28
		public IList<GroupDTO> GetGroups()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			List<GroupDTO> list = (List<GroupDTO>)cacheStorageManager["cGroups"];
			bool flag = list != null;
			IList<GroupDTO> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				LoadGroupsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadGroupsReq>();
				LoadGroupsResp loadGroupsResp = ClientServiceFactory.GetClientInstance<IPeople>().LoadGroups(request);
				list = loadGroupsResp.Groups;
				cacheStorageManager["cGroups"] = list;
				result = list;
			}
			return result;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008890 File Offset: 0x00006A90
		public IList<GroupDTO> GetRoomGroups()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			List<GroupDTO> list = (List<GroupDTO>)cacheStorageManager["cRoomGroups"];
			bool flag = list != null;
			IList<GroupDTO> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				LoadAllRoomGroupsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllRoomGroupsReq>();
				LoadAllRoomGroupsResp loadAllRoomGroupsResp = ClientServiceFactory.GetClientInstance<IPeople>().LoadAllRoomGroups(request);
				list = loadAllRoomGroupsResp.Groups;
				cacheStorageManager["cGroups"] = list;
				result = list;
			}
			return result;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000088F8 File Offset: 0x00006AF8
		public IList<PersonBaseDTO> GetStudents()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager["cStudents"];
			List<PersonBaseDTO> list = (List<PersonBaseDTO>)obj;
			bool flag = list != null;
			IList<PersonBaseDTO> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				LoadStudentsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentsReq>();
				LoadStudentsResp loadStudentsResp = ClientServiceFactory.GetClientInstance<IPeople>().LoadStudents(request);
				list = loadStudentsResp.People;
				cacheStorageManager.Insert("cStudents", list);
				result = list;
			}
			return result;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008964 File Offset: 0x00006B64
		public IList<PersonBaseDTO> GetStaff()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager["cStaff"];
			List<PersonBaseDTO> list = (List<PersonBaseDTO>)obj;
			bool flag = list != null;
			IList<PersonBaseDTO> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				LoadStaffReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStaffReq>();
				LoadStaffResp loadStaffResp = ClientServiceFactory.GetClientInstance<IPeople>().LoadStaff(request);
				list = loadStaffResp.People;
				cacheStorageManager.Insert("cStaff", list);
				result = list;
			}
			return result;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000089D0 File Offset: 0x00006BD0
		public IList<PersonBaseDTO> GetRooms()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager["cRooms"];
			List<PersonBaseDTO> list = (List<PersonBaseDTO>)obj;
			bool flag = list != null;
			IList<PersonBaseDTO> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				LoadRoomsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadRoomsReq>();
				LoadRoomsResp loadRoomsResp = ClientServiceFactory.GetClientInstance<IPeople>().LoadRooms(request);
				list = loadRoomsResp.People;
				cacheStorageManager.Insert("cRooms", list);
				result = list;
			}
			return result;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008A3C File Offset: 0x00006C3C
		public IList<PersonBaseDTO> GetResources()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager["cResources"];
			List<PersonBaseDTO> list = (List<PersonBaseDTO>)obj;
			bool flag = list != null;
			IList<PersonBaseDTO> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				LoadResourcesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadResourcesReq>();
				LoadResourcesResp loadResourcesResp = ClientServiceFactory.GetClientInstance<IPeople>().LoadResources(request);
				list = loadResourcesResp.People;
				cacheStorageManager.Insert("cResources", list);
				result = list;
			}
			return result;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008AA8 File Offset: 0x00006CA8
		public PersonBaseDTO LoadStudentByStudent_No(string student_no, out bool whoAmIIsAllowedToSeeThisStudent)
		{
			LoadPersonByStudentNumberReq loadPersonByStudentNumberReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPersonByStudentNumberReq>();
			loadPersonByStudentNumberReq.Student_no = student_no;
			loadPersonByStudentNumberReq.CheckIfWhoAmIIsAllowedToSeeThisStudent = true;
			LoadPersonByStudentNumberResp loadPersonByStudentNumberResp = ClientServiceFactory.GetClientInstance<IPeople>().LoadPersonByStudentNumber(loadPersonByStudentNumberReq);
			whoAmIIsAllowedToSeeThisStudent = loadPersonByStudentNumberResp.WhoAmIIsAllowedToSeeThisStudent;
			return loadPersonByStudentNumberResp.Person;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008AF0 File Offset: 0x00006CF0
		public PersonBaseWithExtendedInfoDTO LoadPersonWithExtendedInfo(int Personid)
		{
			LoadPersonWithExtendedInfoReq loadPersonWithExtendedInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPersonWithExtendedInfoReq>();
			loadPersonWithExtendedInfoReq.PersonId = Personid;
			return ClientServiceFactory.GetClientInstance<IPeople>().LoadPersonWithExtendedInfo(loadPersonWithExtendedInfoReq).PersonWithExtendedInfo;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008B28 File Offset: 0x00006D28
		public IList<PersonBaseDTO> LoadPersonsByIds(IList<int> PersonIds)
		{
			LoadPersonsByIdsReq loadPersonsByIdsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPersonsByIdsReq>();
			loadPersonsByIdsReq.PersonIds = PersonIds.ToArray<int>();
			return ClientServiceFactory.GetClientInstance<IPeople>().LoadPersonsByIds(loadPersonsByIdsReq).Persons;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008B64 File Offset: 0x00006D64
		public string GetTempStudentNumber(string Prefix, string PostFix)
		{
			GetTempStudentNumberReq getTempStudentNumberReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetTempStudentNumberReq>();
			getTempStudentNumberReq.Prefix = Prefix;
			getTempStudentNumberReq.Postfix = PostFix;
			return ClientServiceFactory.GetClientInstance<IPeople>().GetTempStudentNumber(getTempStudentNumberReq).TempStudentNumber;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008BA4 File Offset: 0x00006DA4
		public IList<PersonBaseDTO> LoadDeletedAccounts(params int[] GroupIds)
		{
			LoadDeletedAccountsReq loadDeletedAccountsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDeletedAccountsReq>();
			loadDeletedAccountsReq.GroupIds = GroupIds;
			return ClientServiceFactory.GetClientInstance<IPeople>().LoadDeletedAccounts(loadDeletedAccountsReq).UserAccounts;
		}
	}
}

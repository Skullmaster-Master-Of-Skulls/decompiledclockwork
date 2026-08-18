using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.People
{
	// Token: 0x02000028 RID: 40
	public class PersonBaseRestClientManager : BearerTokenRestProxy<IPersonBaseClientManager>, IPersonBaseClientManager, IWebService
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00005614 File Offset: 0x00003814
		public PersonBaseRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000561E File Offset: 0x0000381E
		public PersonBaseRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000562C File Offset: 0x0000382C
		public PersonBaseDTO LoadPersonByStudentNumber(string Student_no, bool CheckIfWhoAmIIsAllowedToSeeThisStudent, out bool WhoAmIIsAllowedToSeeThisStudent)
		{
			LoadPersonByStudentNumberResp loadPersonByStudentNumberResp = base.Get<LoadPersonByStudentNumberResp>(string.Format("people/person/studentnumber/{0}?checkifwhoamiisallowtoseethisstudent={1}", Student_no, CheckIfWhoAmIIsAllowedToSeeThisStudent), true);
			WhoAmIIsAllowedToSeeThisStudent = loadPersonByStudentNumberResp.WhoAmIIsAllowedToSeeThisStudent;
			return loadPersonByStudentNumberResp.Person;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005660 File Offset: 0x00003860
		public PersonBaseDTO LoadPersonByStudentNumber(string Student_no, bool checkIfWhoAmIIsAllowedToSeeThisStudent)
		{
			return base.Get<LoadPersonByStudentNumberResp>(string.Format("people/person/studentnumber/{0}?checkifwhoamiisallowtoseethisstudent={1}", Student_no, checkIfWhoAmIIsAllowedToSeeThisStudent), true).Person;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000567F File Offset: 0x0000387F
		public bool IsStudentsAccommodationsExpired(int personId)
		{
			return base.Get<bool>(string.Format("people/isstudentaccommodationsexpired/personid/{0}", personId), true);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005698 File Offset: 0x00003898
		public IList<PersonBaseDTO> GetStudents()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<PersonBaseDTO> list = (IList<PersonBaseDTO>)cacheStorageManager["cStudents"];
			if (list != null)
			{
				return list;
			}
			list = base.GetMany<PersonBaseDTO>("people/students", true);
			cacheStorageManager.Insert("cStudents", list);
			return list;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000056DC File Offset: 0x000038DC
		public IList<PersonBaseDTO> GetStaff()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<PersonBaseDTO> list = (IList<PersonBaseDTO>)cacheStorageManager["cStaff"];
			if (list != null)
			{
				return list;
			}
			list = base.GetMany<PersonBaseDTO>("people/staff", true);
			cacheStorageManager.Insert("cStaff", list);
			return list;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005720 File Offset: 0x00003920
		public IList<PersonBaseDTO> GetRooms()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<PersonBaseDTO> list = (IList<PersonBaseDTO>)cacheStorageManager["cRooms"];
			if (list != null)
			{
				return list;
			}
			list = base.GetMany<PersonBaseDTO>("people/rooms", true);
			cacheStorageManager.Insert("cRooms", list);
			return list;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005764 File Offset: 0x00003964
		public IList<PersonBaseDTO> GetResources()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<PersonBaseDTO> list = (IList<PersonBaseDTO>)cacheStorageManager["cResources"];
			if (list != null)
			{
				return list;
			}
			list = base.GetMany<PersonBaseDTO>("people/resources", true);
			cacheStorageManager.Insert("cResources", list);
			return list;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000057A8 File Offset: 0x000039A8
		public IList<GroupDTO> GetGroups()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<GroupDTO> list = (IList<GroupDTO>)cacheStorageManager["cGroups"];
			if (list != null)
			{
				return list;
			}
			list = base.GetMany<GroupDTO>("people/groups", true);
			cacheStorageManager["cGroups"] = list;
			return list;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000057EC File Offset: 0x000039EC
		public IList<GroupDTO> GetRoomGroups()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<GroupDTO> list = (IList<GroupDTO>)cacheStorageManager["cRoomGroups"];
			if (list != null)
			{
				return list;
			}
			list = base.GetMany<GroupDTO>("people/allroomgroups", true);
			cacheStorageManager["cGroups"] = list;
			return list;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000582F File Offset: 0x00003A2F
		public IList<PersonBaseDTO> LoadGroupMembers(int GroupId)
		{
			return base.GetMany<PersonBaseDTO>(string.Format("people/groupmembers/groupid/{0}", GroupId), true);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005848 File Offset: 0x00003A48
		public IList<PersonBaseDTO> LoadGroupMembers(int[] GroupIds)
		{
			return base.GetMany<PersonBaseDTO>(string.Format("people/multiplegroupmembers/groupids/{0}", GroupIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005861 File Offset: 0x00003A61
		public PersonBaseDTO LoadPerson(int PersonId)
		{
			return base.Get<PersonBaseDTO>(string.Format("people/person/id/{0}", PersonId), true);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000587C File Offset: 0x00003A7C
		public int CreateUser(PersonBaseDTO User, List<int> GroupIds)
		{
			CreateUserReq createUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateUserReq>();
			createUserReq.User = User;
			createUserReq.GroupIds = GroupIds;
			return base.Post<CreateUserReq, int>(createUserReq, "people/user");
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000058B0 File Offset: 0x00003AB0
		public PersonBaseDTO LoadStudentByStudent_No(string student_no, out bool whoAmIIsAllowedToSeeThisStudent)
		{
			LoadPersonByStudentNumberResp loadPersonByStudentNumberResp = base.Get<LoadPersonByStudentNumberResp>(string.Format("people/person/studentnumber/{0}", student_no), true);
			whoAmIIsAllowedToSeeThisStudent = loadPersonByStudentNumberResp.WhoAmIIsAllowedToSeeThisStudent;
			return loadPersonByStudentNumberResp.Person;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000058E0 File Offset: 0x00003AE0
		public void UpdateUser(PersonBaseDTO user, bool UpdateGroupMemberships = true)
		{
			UpdateUserReq updateUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateUserReq>();
			updateUserReq.UpdateGroupMemberships = UpdateGroupMemberships;
			updateUserReq.User = user;
			base.Put<UpdateUserReq>(updateUserReq, "people/user");
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005912 File Offset: 0x00003B12
		public PersonBaseWithExtendedInfoDTO LoadPersonWithExtendedInfo(int Personid)
		{
			return base.Get<PersonBaseWithExtendedInfoDTO>(string.Format("people/personwithextendedinfo/personid/{0}", Personid), true);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000592B File Offset: 0x00003B2B
		public IList<PersonBaseDTO> LoadPersonsByIds(IList<int> PersonIds)
		{
			return base.GetMany<PersonBaseDTO>(string.Format("people/persons/personids/{0}", PersonIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005944 File Offset: 0x00003B44
		public string GetTempStudentNumber(string Prefix, string PostFix)
		{
			return base.Get<string>(string.Format("people/tempstudentnumber/prefix/{0}/postfix/{1}", Prefix, PostFix), true);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005959 File Offset: 0x00003B59
		public IList<PersonBaseDTO> LoadDeletedAccounts(params int[] GroupIds)
		{
			return base.GetMany<PersonBaseDTO>(string.Format("people/deletedaccounts/groupids/{0}", GroupIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}
	}
}

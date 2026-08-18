using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.People
{
	// Token: 0x02000027 RID: 39
	public class PeopleRestClientManager : BearerTokenRestProxy<IPeopleClientManager>, IPeopleClientManager, IWebService
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00005488 File Offset: 0x00003688
		public PeopleRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005492 File Offset: 0x00003692
		public PeopleRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000054A0 File Offset: 0x000036A0
		public int CreateUser(PersonBaseDTO User, List<int> GroupIds)
		{
			CreateUserReq createUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateUserReq>();
			createUserReq.User = User;
			createUserReq.GroupIds = GroupIds;
			return base.Post<CreateUserReq, int>(createUserReq, "people/user");
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000054D2 File Offset: 0x000036D2
		public PersonBaseDTO LoadPerson(int PersonId)
		{
			return base.Get<PersonBaseDTO>(string.Format("people/person/id/{0}", PersonId), true);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000054EB File Offset: 0x000036EB
		public PersonBaseDTO LoadPersonByStudentNumber(string Student_No, bool checkIfWhoamiIsAllowToSeeThisStudent = false)
		{
			return base.Get<PersonBaseDTO>(string.Format("people/person/studentnumber/{0}?checkifwhoamiisallowtoseethisstudent={1}", Student_No, checkIfWhoamiIsAllowToSeeThisStudent), true);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005505 File Offset: 0x00003705
		public bool IsStudentsAccommodationsExpired(int PersonId)
		{
			return base.Get<bool>(string.Format("people/isstudentaccommodationsexpired/personid/{0}", PersonId), true);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000551E File Offset: 0x0000371E
		public IList<PersonBaseDTO> LoadGroupMembers(params int[] GroupIds)
		{
			return base.GetMany<PersonBaseDTO>(string.Format("people/groupmembers/groupid/{0}", GroupIds), true);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005532 File Offset: 0x00003732
		public IList<GroupDTO> LoadGroups()
		{
			return base.GetMany<GroupDTO>("people/groups", true);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005540 File Offset: 0x00003740
		public PersonBaseDTO LoadPersonById(int PersonId)
		{
			return base.Get<PersonBaseDTO>(string.Format("people/person/id/{0}", PersonId), true);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005559 File Offset: 0x00003759
		public IList<PersonBaseDTO> LoadStaff()
		{
			return base.GetMany<PersonBaseDTO>("people/staff", true);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005567 File Offset: 0x00003767
		public IList<PersonBaseDTO> FindStudentBySearchString(string SearchString)
		{
			return base.GetMany<PersonBaseDTO>(string.Format("people/student/matching?searchstring={0}", SearchString), true);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000557B File Offset: 0x0000377B
		public FindUserGroupObjectBySearchStringResp FindUserGroupObjectBySearchString(string searchString, int startIndex, int maxResultsCount, params eUserGroupObjectType[] userGroupObjectTypes)
		{
			return base.Get<FindUserGroupObjectBySearchStringResp>(string.Format("people/usergroupobject/matching?searchstring={0}&startindex={1}&maxresultscount={2}&objecttypestoexclude={3}", new object[]
			{
				searchString,
				startIndex,
				maxResultsCount,
				userGroupObjectTypes.CommaSeparatedValuesWithoutSpace<eUserGroupObjectType>()
			}), true);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000055B4 File Offset: 0x000037B4
		public int CreateGroup(GroupDTO Group)
		{
			return base.Post<GroupDTO, int>(Group, "people/group");
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000055C2 File Offset: 0x000037C2
		public void DeleteGroup(int GroupId)
		{
			base.Delete(string.Format("people/group/groupid/{0}", GroupId));
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000055DA File Offset: 0x000037DA
		public void DeleteUser(int PersonId, bool JustDeactivate)
		{
			base.Delete(string.Format("people/user/personid/{0}?justdeactivate={1}", PersonId, JustDeactivate));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000055F8 File Offset: 0x000037F8
		public void UpdateGroup(GroupDTO Group)
		{
			base.Put<GroupDTO>(Group, "people/group");
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005606 File Offset: 0x00003806
		public void UpdateUser(PersonBaseDTO User)
		{
			base.Put<PersonBaseDTO>(User, "people/user");
		}
	}
}

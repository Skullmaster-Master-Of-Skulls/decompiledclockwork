using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000073 RID: 115
	public class AdminPeopleServiceManager : IAdminPeople, IService
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x0001459C File Offset: 0x0001279C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000145B0 File Offset: 0x000127B0
		public LoadPersonResp LoadPersonWithGroups(LoadPersonReq request)
		{
			IAdminPeopleManager adminPeopleManager = new AdminPeopleManager(request.GetOperationContext());
			PersonBase personBase = adminPeopleManager.LoadPersonWithGroups(request.PersonId);
			return new LoadPersonResp
			{
				Person = personBase.ToDTO()
			};
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000145F0 File Offset: 0x000127F0
		public LoadGroupsByIdResp LoadGroupsById(LoadGroupsByIdReq Request)
		{
			IAdminPeopleManager adminPeopleManager = new AdminPeopleManager(Request.GetOperationContext());
			IList<Group> list = adminPeopleManager.LoadGroupsById(Request.GroupIds.ToArray<int>());
			LoadGroupsByIdResp loadGroupsByIdResp = new LoadGroupsByIdResp();
			IList<GroupDTO> groups;
			if (list != null)
			{
				groups = list.ToList<Group>().ConvertAll<GroupDTO>((Group g) => g.ToDTO());
			}
			else
			{
				groups = null;
			}
			loadGroupsByIdResp.Groups = groups;
			return loadGroupsByIdResp;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001465C File Offset: 0x0001285C
		public LoadAllGroupsResp LoadAllGroups(LoadAllGroupsReq Request)
		{
			IAdminPeopleManager adminPeopleManager = new AdminPeopleManager(Request.GetOperationContext());
			IList<Group> list = adminPeopleManager.LoadAllGroups();
			LoadAllGroupsResp loadAllGroupsResp = new LoadAllGroupsResp();
			IList<GroupDTO> groups;
			if (list == null)
			{
				groups = null;
			}
			else
			{
				groups = list.ToList<Group>().ConvertAll<GroupDTO>((Group g) => g.ToDTO());
			}
			loadAllGroupsResp.Groups = groups;
			return loadAllGroupsResp;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000146C0 File Offset: 0x000128C0
		public LoadPersonsByUsernameResp LoadPersonsByUsername(LoadPersonsByUsernameReq Request)
		{
			IAdminPeopleManager adminPeopleManager = new AdminPeopleManager(Request.GetOperationContext());
			IList<PersonBase> list = adminPeopleManager.LoadPersonsByUsername(Request.Username, Request.IncludeDeletedAccounts);
			LoadPersonsByUsernameResp loadPersonsByUsernameResp = new LoadPersonsByUsernameResp();
			IList<PersonBaseDTO> people;
			if (list == null)
			{
				people = null;
			}
			else
			{
				people = list.ToList<PersonBase>().ConvertAll<PersonBaseDTO>((PersonBase g) => g.ToDTO());
			}
			loadPersonsByUsernameResp.People = people;
			return loadPersonsByUsernameResp;
		}
	}
}

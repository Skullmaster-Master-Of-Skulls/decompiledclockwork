using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.People
{
	// Token: 0x0200002C RID: 44
	public class AdminPeopleClientManager : IAdminPeopleClientManager, IWebService
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00007B48 File Offset: 0x00005D48
		public PersonBaseDTO LoadPersonWithGroups(int PersonId)
		{
			LoadPersonReq loadPersonReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPersonReq>();
			loadPersonReq.PersonId = PersonId;
			return ClientServiceFactory.GetClientInstance<IAdminPeople>().LoadPersonWithGroups(loadPersonReq).Person;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007B80 File Offset: 0x00005D80
		public IList<GroupDTO> LoadGroupsById(IList<int> GroupIds)
		{
			LoadGroupsByIdReq loadGroupsByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadGroupsByIdReq>();
			loadGroupsByIdReq.GroupIds = GroupIds;
			return ClientServiceFactory.GetClientInstance<IAdminPeople>().LoadGroupsById(loadGroupsByIdReq).Groups;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007BB8 File Offset: 0x00005DB8
		public IList<GroupDTO> LoadAllGroups()
		{
			LoadAllGroupsReq req = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllGroupsReq>();
			return ClientServiceFactory.GetClientInstance<IAdminPeople>().LoadAllGroups(req).Groups;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007BE8 File Offset: 0x00005DE8
		public IList<PersonBaseDTO> LoadPersonsByUsername(string Username, bool includeDeletedAccounts = false)
		{
			LoadPersonsByUsernameReq loadPersonsByUsernameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPersonsByUsernameReq>();
			loadPersonsByUsernameReq.Username = Username;
			loadPersonsByUsernameReq.IncludeDeletedAccounts = includeDeletedAccounts;
			return ClientServiceFactory.GetClientInstance<IAdminPeople>().LoadPersonsByUsername(loadPersonsByUsernameReq).People;
		}
	}
}

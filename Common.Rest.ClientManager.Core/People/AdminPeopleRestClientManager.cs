using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.People
{
	// Token: 0x02000024 RID: 36
	public class AdminPeopleRestClientManager : BearerTokenRestProxy<IAdminPeopleClientManager>, IAdminPeopleClientManager, IWebService
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000536F File Offset: 0x0000356F
		public AdminPeopleRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005379 File Offset: 0x00003579
		public AdminPeopleRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005384 File Offset: 0x00003584
		public PersonBaseDTO LoadPersonWithGroups(int PersonId)
		{
			return base.Get<PersonBaseDTO>(string.Format("adminpeople/personwithgroups/personid/{0}", PersonId), true);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000539D File Offset: 0x0000359D
		public IList<GroupDTO> LoadGroupsById(IList<int> GroupIds)
		{
			return base.GetMany<GroupDTO>(string.Format("adminpeople/groups/groupids/{0}", GroupIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000053B6 File Offset: 0x000035B6
		public IList<GroupDTO> LoadAllGroups()
		{
			return base.GetMany<GroupDTO>("adminpeople/groups", true);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000053C4 File Offset: 0x000035C4
		public IList<PersonBaseDTO> LoadPersonsByUsername(string Username)
		{
			return base.GetMany<PersonBaseDTO>(string.Format("adminpeople/persons/username/{0}", Username), true);
		}
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.People
{
	// Token: 0x02000026 RID: 38
	public class PeopleGroupRestClientManager : BearerTokenRestProxy<IPeopleGroupClientManager>, IPeopleGroupClientManager, IWebService
	{
		// Token: 0x0600014D RID: 333 RVA: 0x0000545E File Offset: 0x0000365E
		public PeopleGroupRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005468 File Offset: 0x00003668
		public PeopleGroupRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005473 File Offset: 0x00003673
		public IList<PersonBaseDTO> LoadUsersByGroupTitle(string GroupTitle, string AlternateGroupTitle = null)
		{
			return base.GetMany<PersonBaseDTO>(string.Format("peoplegroup/users/grouptitle/{0}?alternategrouptitle={1}", GroupTitle, AlternateGroupTitle), true);
		}
	}
}

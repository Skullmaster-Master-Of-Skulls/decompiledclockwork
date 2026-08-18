using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000102 RID: 258
	internal class AdminPeopleClientBaseProxy : ClientBase<IAdminPeople>, IAdminPeople, IService
	{
		// Token: 0x06000A0F RID: 2575 RVA: 0x00019ACC File Offset: 0x00017CCC
		public AdminPeopleClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00019AD7 File Offset: 0x00017CD7
		public AdminPeopleClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00019AE4 File Offset: 0x00017CE4
		public LoadPersonResp LoadPersonWithGroups(LoadPersonReq request)
		{
			return base.Channel.LoadPersonWithGroups(request);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00019B04 File Offset: 0x00017D04
		public LoadGroupsByIdResp LoadGroupsById(LoadGroupsByIdReq Request)
		{
			return base.Channel.LoadGroupsById(Request);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00019B24 File Offset: 0x00017D24
		public LoadAllGroupsResp LoadAllGroups(LoadAllGroupsReq Request)
		{
			return base.Channel.LoadAllGroups(Request);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00019B44 File Offset: 0x00017D44
		public LoadPersonsByUsernameResp LoadPersonsByUsername(LoadPersonsByUsernameReq Request)
		{
			return base.Channel.LoadPersonsByUsername(Request);
		}
	}
}

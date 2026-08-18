using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000A6 RID: 166
	internal class LdapClientBaseProxy : ClientBase<ILdap>, ILdap, IService
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x00011E28 File Offset: 0x00010028
		public LdapClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00011E33 File Offset: 0x00010033
		public LdapClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00011E40 File Offset: 0x00010040
		public LdapLoginResp LdapLogin(LdapLoginReq Request)
		{
			return base.Channel.LdapLogin(Request);
		}
	}
}

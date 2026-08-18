using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Authentication
{
	// Token: 0x0200006B RID: 107
	public class LdapRestClientManager : BearerTokenRestProxy<ILdapClientManager>, ILdapClientManager, IWebService
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0000C158 File Offset: 0x0000A358
		public LdapRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000C162 File Offset: 0x0000A362
		public LdapRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000C170 File Offset: 0x0000A370
		public LdapAuthenticationResultDTO LdapLogin(LdapConnectionInfoDTO ConnectionInfo, string UserName, string Password)
		{
			LdapLoginReq ldapLoginReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LdapLoginReq>();
			ldapLoginReq.ConnectionInfo = ConnectionInfo;
			ldapLoginReq.UserName = UserName;
			ldapLoginReq.PassWord = Password;
			return base.Post<LdapLoginReq, LdapAuthenticationResultDTO>(ldapLoginReq, "ldap/login");
		}
	}
}

using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Authentication
{
	// Token: 0x02000081 RID: 129
	public class LdapClientManager : ILdapClientManager, IWebService
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x000153A4 File Offset: 0x000135A4
		public LdapAuthenticationResultDTO LdapLogin(LdapConnectionInfoDTO ConnectionInfo, string UserName, string Password)
		{
			LdapLoginReq ldapLoginReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LdapLoginReq>();
			ldapLoginReq.ConnectionInfo = ConnectionInfo;
			ldapLoginReq.UserName = UserName;
			ldapLoginReq.PassWord = Password;
			return ClientServiceFactory.GetClientInstance<ILdap>().LdapLogin(ldapLoginReq).LoginResult;
		}
	}
}

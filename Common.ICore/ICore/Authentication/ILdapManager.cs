using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication;

namespace TechnoPro.Common.ICore.Authentication
{
	// Token: 0x020000DD RID: 221
	public interface ILdapManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006E1 RID: 1761
		LdapAuthenticationResult LdapLogin(LdapConnectionInfo ConnectionInfo, string UserName, string Password);
	}
}

using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200037A RID: 890
	internal class GenericXmlSecurityTokenAuthenticator : SecurityTokenAuthenticator
	{
		// Token: 0x06002116 RID: 8470 RVA: 0x0007ADCC File Offset: 0x00078FCC
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is GenericXmlSecurityToken;
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0007ADD8 File Offset: 0x00078FD8
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			GenericXmlSecurityToken genericXmlSecurityToken = (GenericXmlSecurityToken)token;
			return genericXmlSecurityToken.AuthorizationPolicies;
		}
	}
}

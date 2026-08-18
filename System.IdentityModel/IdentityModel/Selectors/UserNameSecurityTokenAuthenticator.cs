using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001AD RID: 429
	public abstract class UserNameSecurityTokenAuthenticator : SecurityTokenAuthenticator
	{
		// Token: 0x06000E0C RID: 3596 RVA: 0x0003FE06 File Offset: 0x0003E006
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is UserNameSecurityToken;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003FE14 File Offset: 0x0003E014
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			UserNameSecurityToken userNameSecurityToken = (UserNameSecurityToken)token;
			return this.ValidateUserNamePasswordCore(userNameSecurityToken.UserName, userNameSecurityToken.Password);
		}

		// Token: 0x06000E0E RID: 3598
		protected abstract ReadOnlyCollection<IAuthorizationPolicy> ValidateUserNamePasswordCore(string userName, string password);
	}
}

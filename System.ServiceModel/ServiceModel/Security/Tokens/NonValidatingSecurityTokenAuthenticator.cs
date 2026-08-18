using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200037C RID: 892
	internal class NonValidatingSecurityTokenAuthenticator<TTokenType> : SecurityTokenAuthenticator
	{
		// Token: 0x0600211C RID: 8476 RVA: 0x0007AE76 File Offset: 0x00079076
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is TTokenType;
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0007AE81 File Offset: 0x00079081
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			return EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
		}
	}
}

using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000399 RID: 921
	public class SecurityContextSecurityTokenAuthenticator : SecurityTokenAuthenticator
	{
		// Token: 0x06002224 RID: 8740 RVA: 0x0007D1BC File Offset: 0x0007B3BC
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is SecurityContextSecurityToken;
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0007D1C8 File Offset: 0x0007B3C8
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			SecurityContextSecurityToken securityContextSecurityToken = (SecurityContextSecurityToken)token;
			if (!this.IsTimeValid(securityContextSecurityToken))
			{
				this.ThrowExpiredContextFaultException(securityContextSecurityToken.ContextId, securityContextSecurityToken);
			}
			return securityContextSecurityToken.AuthorizationPolicies;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0007D1F8 File Offset: 0x0007B3F8
		private void ThrowExpiredContextFaultException(UniqueId contextId, SecurityContextSecurityToken sct)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityContextTokenValidationException(SR.GetString("SecurityContextExpired", new object[]
			{
				contextId,
				(sct.KeyGeneration == null) ? "none" : sct.KeyGeneration.ToString()
			})));
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x0007D24C File Offset: 0x0007B44C
		private bool IsTimeValid(SecurityContextSecurityToken sct)
		{
			DateTime utcNow = DateTime.UtcNow;
			return sct.ValidFrom <= utcNow && sct.ValidTo >= utcNow && sct.KeyEffectiveTime <= utcNow;
		}
	}
}

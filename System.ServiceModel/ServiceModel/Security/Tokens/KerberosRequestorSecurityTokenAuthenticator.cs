using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200037B RID: 891
	internal class KerberosRequestorSecurityTokenAuthenticator : SecurityTokenAuthenticator
	{
		// Token: 0x06002119 RID: 8473 RVA: 0x0007ADFA File Offset: 0x00078FFA
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is KerberosRequestorSecurityToken;
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0007AE08 File Offset: 0x00079008
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			KerberosRequestorSecurityToken kerberosRequestorSecurityToken = (KerberosRequestorSecurityToken)token;
			List<IAuthorizationPolicy> list = new List<IAuthorizationPolicy>(1);
			ClaimSet issuance = new DefaultClaimSet(ClaimSet.System, new Claim[]
			{
				new Claim(ClaimTypes.Spn, kerberosRequestorSecurityToken.ServicePrincipalName, Rights.PossessProperty)
			});
			list.Add(new UnconditionalPolicy(SecurityUtils.CreateIdentity(kerberosRequestorSecurityToken.ServicePrincipalName, "Kerberos"), issuance));
			return list.AsReadOnly();
		}
	}
}

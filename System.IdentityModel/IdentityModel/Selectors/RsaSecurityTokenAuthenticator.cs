using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001A3 RID: 419
	public class RsaSecurityTokenAuthenticator : SecurityTokenAuthenticator
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x0003EDE7 File Offset: 0x0003CFE7
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is RsaSecurityToken;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0003EDF4 File Offset: 0x0003CFF4
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			RsaSecurityToken rsaSecurityToken = (RsaSecurityToken)token;
			List<Claim> list = new List<Claim>(2);
			list.Add(new Claim(ClaimTypes.Rsa, rsaSecurityToken.Rsa, Rights.Identity));
			list.Add(Claim.CreateRsaClaim(rsaSecurityToken.Rsa));
			DefaultClaimSet issuance = new DefaultClaimSet(ClaimSet.Anonymous, list);
			return new List<IAuthorizationPolicy>(1)
			{
				new UnconditionalPolicy(issuance, rsaSecurityToken.ValidTo)
			}.AsReadOnly();
		}
	}
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000396 RID: 918
	internal static class SecurityContextSecurityTokenHelper
	{
		// Token: 0x060021EC RID: 8684 RVA: 0x0007C803 File Offset: 0x0007AA03
		public static SessionSecurityToken ConvertSctToSessionToken(SecurityContextSecurityToken sct)
		{
			return SecurityContextSecurityTokenHelper.ConvertSctToSessionToken(sct, SecureConversationVersion.Default);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0007C810 File Offset: 0x0007AA10
		public static SessionSecurityToken ConvertSctToSessionToken(SecurityContextSecurityToken sct, SecureConversationVersion version)
		{
			string endpointId = string.Empty;
			for (int i = 0; i < sct.AuthorizationPolicies.Count; i++)
			{
				EndpointAuthorizationPolicy endpointAuthorizationPolicy = sct.AuthorizationPolicies[i] as EndpointAuthorizationPolicy;
				if (endpointAuthorizationPolicy != null)
				{
					endpointId = endpointAuthorizationPolicy.EndpointId;
					break;
				}
			}
			SctAuthorizationPolicy sctAuthorizationPolicy = null;
			for (int j = 0; j < sct.AuthorizationPolicies.Count; j++)
			{
				IAuthorizationPolicy authorizationPolicy = sct.AuthorizationPolicies[j];
				sctAuthorizationPolicy = (authorizationPolicy as SctAuthorizationPolicy);
				if (sctAuthorizationPolicy != null)
				{
					break;
				}
			}
			ClaimsPrincipal claimsPrincipal = null;
			if (sct.AuthorizationPolicies != null && sct.AuthorizationPolicies.Count > 0)
			{
				AuthorizationPolicy authorizationPolicy2 = null;
				for (int k = 0; k < sct.AuthorizationPolicies.Count; k++)
				{
					authorizationPolicy2 = (sct.AuthorizationPolicies[k] as AuthorizationPolicy);
					if (authorizationPolicy2 != null)
					{
						break;
					}
				}
				if (authorizationPolicy2 != null && authorizationPolicy2.IdentityCollection != null)
				{
					claimsPrincipal = new ClaimsPrincipal(authorizationPolicy2.IdentityCollection);
				}
			}
			if (claimsPrincipal == null)
			{
				claimsPrincipal = new ClaimsPrincipal();
			}
			return new SessionSecurityToken(claimsPrincipal, sct.ContextId, sct.Id, string.Empty, sct.GetKeyBytes(), endpointId, new DateTime?(sct.ValidFrom), new DateTime?(sct.ValidTo), sct.KeyGeneration, new DateTime?(sct.KeyEffectiveTime), new DateTime?(sct.KeyExpirationTime), sctAuthorizationPolicy, new Uri(version.Namespace.Value));
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0007C964 File Offset: 0x0007AB64
		public static SecurityContextSecurityToken ConvertSessionTokenToSecurityContextSecurityToken(SessionSecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			List<IAuthorizationPolicy> list = new List<IAuthorizationPolicy>();
			if (token.SctAuthorizationPolicy != null)
			{
				list.Add(token.SctAuthorizationPolicy);
			}
			if (token.ClaimsPrincipal != null && token.ClaimsPrincipal.Identities != null)
			{
				list.Add(new AuthorizationPolicy(token.ClaimsPrincipal.Identities));
			}
			byte[] key = null;
			SymmetricSecurityKey symmetricSecurityKey = token.SecurityKeys[0] as SymmetricSecurityKey;
			if (symmetricSecurityKey != null)
			{
				key = symmetricSecurityKey.GetSymmetricKey();
			}
			return new SecurityContextSecurityToken(token.ContextId, token.Id, key, token.ValidFrom, token.ValidTo, token.KeyGeneration, token.KeyEffectiveTime, token.KeyExpirationTime, list.AsReadOnly());
		}
	}
}

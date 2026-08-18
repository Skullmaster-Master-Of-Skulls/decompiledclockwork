using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x0200035C RID: 860
	internal class SctClaimsHandler
	{
		// Token: 0x06001F98 RID: 8088 RVA: 0x000765A8 File Offset: 0x000747A8
		public SctClaimsHandler(SecurityTokenHandlerCollection securityTokenHandlerCollection, string endpointId)
		{
			if (securityTokenHandlerCollection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenHandlerCollection");
			}
			if (endpointId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNullOrEmptyString("endpointId");
			}
			this._securityTokenHandlerCollection = securityTokenHandlerCollection;
			this._endpointId = endpointId;
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x000765E4 File Offset: 0x000747E4
		public string EndpointId
		{
			get
			{
				return this._endpointId;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x000765EC File Offset: 0x000747EC
		public SecurityTokenHandlerCollection SecurityTokenHandlerCollection
		{
			get
			{
				return this._securityTokenHandlerCollection;
			}
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000765F4 File Offset: 0x000747F4
		internal void SetPrincipalBootstrapTokensAndBindIdfxAuthPolicy(SecurityContextSecurityToken sct)
		{
			if (sct == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sct");
			}
			List<IAuthorizationPolicy> list = new List<IAuthorizationPolicy>();
			if (sct.AuthorizationPolicies != null && sct.AuthorizationPolicies.Count > 0 && this.ContainsEndpointAuthPolicy(sct.AuthorizationPolicies))
			{
				return;
			}
			if (sct.AuthorizationPolicies != null && sct.AuthorizationPolicies.Count > 0)
			{
				AuthorizationPolicy item = IdentityModelServiceAuthorizationManager.TransformAuthorizationPolicies(sct.AuthorizationPolicies, this._securityTokenHandlerCollection, false);
				list.Add(item);
				Claim primaryIdentityClaim = this.GetPrimaryIdentityClaim(AuthorizationContext.CreateDefaultAuthorizationContext(sct.AuthorizationPolicies));
				SctAuthorizationPolicy item2 = new SctAuthorizationPolicy(primaryIdentityClaim);
				list.Add(item2);
			}
			list.Add(new EndpointAuthorizationPolicy(this._endpointId));
			sct.AuthorizationPolicies = list.AsReadOnly();
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000766AC File Offset: 0x000748AC
		private bool ContainsEndpointAuthPolicy(ReadOnlyCollection<IAuthorizationPolicy> policies)
		{
			if (policies == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("policies");
			}
			for (int i = 0; i < policies.Count; i++)
			{
				if (policies[i] is EndpointAuthorizationPolicy)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000766F0 File Offset: 0x000748F0
		private Claim GetPrimaryIdentityClaim(AuthorizationContext authContext)
		{
			if (authContext != null)
			{
				for (int i = 0; i < authContext.ClaimSets.Count; i++)
				{
					ClaimSet claimSet = authContext.ClaimSets[i];
					using (IEnumerator<Claim> enumerator = claimSet.FindClaims(null, Rights.Identity).GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							return enumerator.Current;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x0007676C File Offset: 0x0007496C
		public void OnTokenIssued(SecurityToken issuedToken, EndpointAddress tokenRequestor)
		{
			this.SetPrincipalBootstrapTokensAndBindIdfxAuthPolicy(issuedToken as SecurityContextSecurityToken);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0007677A File Offset: 0x0007497A
		public void OnTokenRenewed(SecurityToken issuedToken, SecurityToken oldToken)
		{
			this.SetPrincipalBootstrapTokensAndBindIdfxAuthPolicy(issuedToken as SecurityContextSecurityToken);
		}

		// Token: 0x04001EEA RID: 7914
		private SecurityTokenHandlerCollection _securityTokenHandlerCollection;

		// Token: 0x04001EEB RID: 7915
		private string _endpointId;
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x02000333 RID: 819
	public class SimpleSecurityTokenProvider : SecurityTokenProvider
	{
		// Token: 0x06001DBC RID: 7612 RVA: 0x0006E554 File Offset: 0x0006C754
		public SimpleSecurityTokenProvider(SecurityToken token, SecurityTokenRequirement tokenRequirement)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			GenericXmlSecurityToken genericXmlSecurityToken = token as GenericXmlSecurityToken;
			if (genericXmlSecurityToken != null)
			{
				this._securityToken = SimpleSecurityTokenProvider.WrapWithAuthPolicy(genericXmlSecurityToken, tokenRequirement);
				return;
			}
			this._securityToken = token;
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0006E599 File Offset: 0x0006C799
		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			return this._securityToken;
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0006E5A4 File Offset: 0x0006C7A4
		private static GenericXmlSecurityToken WrapWithAuthPolicy(GenericXmlSecurityToken issuedToken, SecurityTokenRequirement tokenRequirement)
		{
			EndpointIdentity endpointIdentity = null;
			InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = tokenRequirement as InitiatorServiceModelSecurityTokenRequirement;
			if (initiatorServiceModelSecurityTokenRequirement != null)
			{
				EndpointAddress targetAddress = initiatorServiceModelSecurityTokenRequirement.TargetAddress;
				if (targetAddress.Uri.IsAbsoluteUri)
				{
					endpointIdentity = EndpointIdentity.CreateDnsIdentity(targetAddress.Uri.DnsSafeHost);
				}
			}
			ReadOnlyCollection<IAuthorizationPolicy> serviceAuthorizationPolicies = SimpleSecurityTokenProvider.GetServiceAuthorizationPolicies(endpointIdentity);
			return new GenericXmlSecurityToken(issuedToken.TokenXml, issuedToken.ProofToken, issuedToken.ValidFrom, issuedToken.ValidTo, issuedToken.InternalTokenReference, issuedToken.ExternalTokenReference, serviceAuthorizationPolicies);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0006E614 File Offset: 0x0006C814
		private static ReadOnlyCollection<IAuthorizationPolicy> GetServiceAuthorizationPolicies(EndpointIdentity endpointIdentity)
		{
			if (endpointIdentity != null)
			{
				List<Claim> list = new List<Claim>(1);
				list.Add(endpointIdentity.IdentityClaim);
				return new List<IAuthorizationPolicy>(1)
				{
					new UnconditionalPolicy(SecurityUtils.CreateIdentity(endpointIdentity.IdentityClaim.Resource.ToString()), new DefaultClaimSet(ClaimSet.System, list))
				}.AsReadOnly();
			}
			return EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
		}

		// Token: 0x04001E36 RID: 7734
		private SecurityToken _securityToken;
	}
}

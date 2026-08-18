using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Security.Principal;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001A4 RID: 420
	public class SamlSecurityTokenAuthenticator : SecurityTokenAuthenticator
	{
		// Token: 0x06000D9D RID: 3485 RVA: 0x0003EE66 File Offset: 0x0003D066
		public SamlSecurityTokenAuthenticator(IList<SecurityTokenAuthenticator> supportingAuthenticators) : this(supportingAuthenticators, TimeSpan.Zero)
		{
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0003EE74 File Offset: 0x0003D074
		public SamlSecurityTokenAuthenticator(IList<SecurityTokenAuthenticator> supportingAuthenticators, TimeSpan maxClockSkew)
		{
			if (supportingAuthenticators == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("supportingAuthenticators");
			}
			this.supportingAuthenticators = new List<SecurityTokenAuthenticator>(supportingAuthenticators.Count);
			for (int i = 0; i < supportingAuthenticators.Count; i++)
			{
				this.supportingAuthenticators.Add(supportingAuthenticators[i]);
			}
			this.maxClockSkew = maxClockSkew;
			this.audienceUriMode = AudienceUriMode.Always;
			this.allowedAudienceUris = new Collection<string>();
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0003EEE7 File Offset: 0x0003D0E7
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x0003EEEF File Offset: 0x0003D0EF
		public AudienceUriMode AudienceUriMode
		{
			get
			{
				return this.audienceUriMode;
			}
			set
			{
				AudienceUriModeValidationHelper.Validate(this.audienceUriMode);
				this.audienceUriMode = value;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0003EF03 File Offset: 0x0003D103
		public IList<string> AllowedAudienceUris
		{
			get
			{
				return this.allowedAudienceUris;
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003EF0B File Offset: 0x0003D10B
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is SamlSecurityToken;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003EF18 File Offset: 0x0003D118
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			SamlSecurityToken samlSecurityToken = token as SamlSecurityToken;
			if (samlSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SamlTokenAuthenticatorCanOnlyProcessSamlTokens", new object[]
				{
					token.GetType().ToString()
				})));
			}
			if (samlSecurityToken.Assertion.Signature == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SamlTokenMissingSignature")));
			}
			if (!this.IsCurrentlyTimeEffective(samlSecurityToken))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLTokenTimeInvalid", new object[]
				{
					DateTime.UtcNow.ToUniversalTime(),
					samlSecurityToken.ValidFrom.ToString(),
					samlSecurityToken.ValidTo.ToString()
				})));
			}
			if (samlSecurityToken.Assertion.SigningToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SamlSigningTokenMissing")));
			}
			bool flag = false;
			for (int i = 0; i < this.supportingAuthenticators.Count; i++)
			{
				flag = this.supportingAuthenticators[i].CanValidateToken(samlSecurityToken.Assertion.SigningToken);
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SamlInvalidSigningToken")));
			}
			ClaimSet issuer = this.ResolveClaimSet(samlSecurityToken.Assertion.SigningToken) ?? ClaimSet.Anonymous;
			List<IAuthorizationPolicy> list = new List<IAuthorizationPolicy>();
			for (int j = 0; j < samlSecurityToken.Assertion.Statements.Count; j++)
			{
				list.Add(samlSecurityToken.Assertion.Statements[j].CreatePolicy(issuer, this));
			}
			if (this.audienceUriMode == AudienceUriMode.Always || (this.audienceUriMode == AudienceUriMode.BearerKeyOnly && samlSecurityToken.SecurityKeys.Count < 1))
			{
				bool flag2 = false;
				if (this.allowedAudienceUris == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAudienceUrisNotFound")));
				}
				for (int k = 0; k < samlSecurityToken.Assertion.Conditions.Conditions.Count; k++)
				{
					SamlAudienceRestrictionCondition samlAudienceRestrictionCondition = samlSecurityToken.Assertion.Conditions.Conditions[k] as SamlAudienceRestrictionCondition;
					if (samlAudienceRestrictionCondition != null)
					{
						flag2 = true;
						if (!this.ValidateAudienceRestriction(samlAudienceRestrictionCondition))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAudienceUriValidationFailed")));
						}
					}
				}
				if (!flag2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAudienceUriValidationFailed")));
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003F1B8 File Offset: 0x0003D3B8
		protected virtual bool ValidateAudienceRestriction(SamlAudienceRestrictionCondition audienceRestrictionCondition)
		{
			for (int i = 0; i < audienceRestrictionCondition.Audiences.Count; i++)
			{
				if (!(audienceRestrictionCondition.Audiences[i] == null))
				{
					for (int j = 0; j < this.allowedAudienceUris.Count; j++)
					{
						if (StringComparer.Ordinal.Compare(audienceRestrictionCondition.Audiences[i].AbsoluteUri, this.allowedAudienceUris[j]) == 0)
						{
							return true;
						}
						if (Uri.IsWellFormedUriString(this.allowedAudienceUris[j], UriKind.Absolute))
						{
							Uri obj = new Uri(this.allowedAudienceUris[j]);
							if (audienceRestrictionCondition.Audiences[i].Equals(obj))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003F274 File Offset: 0x0003D474
		public virtual ClaimSet ResolveClaimSet(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			for (int i = 0; i < this.supportingAuthenticators.Count; i++)
			{
				if (this.supportingAuthenticators[i].CanValidateToken(token))
				{
					ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies = this.supportingAuthenticators[i].ValidateToken(token);
					AuthorizationContext authorizationContext = AuthorizationContext.CreateDefaultAuthorizationContext(authorizationPolicies);
					if (authorizationContext.ClaimSets.Count > 0)
					{
						return authorizationContext.ClaimSets[0];
					}
				}
			}
			return null;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0003F2F4 File Offset: 0x0003D4F4
		public virtual ClaimSet ResolveClaimSet(SecurityKeyIdentifier keyIdentifier)
		{
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
			}
			RsaKeyIdentifierClause rsaKeyIdentifierClause;
			if (keyIdentifier.TryFind<RsaKeyIdentifierClause>(out rsaKeyIdentifierClause))
			{
				return new DefaultClaimSet(new Claim[]
				{
					new Claim(ClaimTypes.Rsa, rsaKeyIdentifierClause.Rsa, Rights.PossessProperty)
				});
			}
			EncryptedKeyIdentifierClause encryptedKeyIdentifierClause;
			if (keyIdentifier.TryFind<EncryptedKeyIdentifierClause>(out encryptedKeyIdentifierClause))
			{
				return new DefaultClaimSet(new Claim[]
				{
					Claim.CreateHashClaim(encryptedKeyIdentifierClause.GetBuffer())
				});
			}
			return null;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003F368 File Offset: 0x0003D568
		public virtual IIdentity ResolveIdentity(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			for (int i = 0; i < this.supportingAuthenticators.Count; i++)
			{
				if (this.supportingAuthenticators[i].CanValidateToken(token))
				{
					ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = this.supportingAuthenticators[i].ValidateToken(token);
					if (readOnlyCollection != null && readOnlyCollection.Count != 0)
					{
						for (int j = 0; j < readOnlyCollection.Count; j++)
						{
							IAuthorizationPolicy authorizationPolicy = readOnlyCollection[j];
							if (authorizationPolicy is UnconditionalPolicy)
							{
								return ((UnconditionalPolicy)authorizationPolicy).PrimaryIdentity;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003F400 File Offset: 0x0003D600
		public virtual IIdentity ResolveIdentity(SecurityKeyIdentifier keyIdentifier)
		{
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
			}
			RsaKeyIdentifierClause rsaKeyIdentifierClause;
			if (keyIdentifier.TryFind<RsaKeyIdentifierClause>(out rsaKeyIdentifierClause))
			{
				return SecurityUtils.CreateIdentity(rsaKeyIdentifierClause.Rsa.ToXmlString(false), base.GetType().Name);
			}
			return null;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003F448 File Offset: 0x0003D648
		private bool IsCurrentlyTimeEffective(SamlSecurityToken token)
		{
			return token.Assertion.Conditions == null || SecurityUtils.IsCurrentlyTimeEffective(token.Assertion.Conditions.NotBefore, token.Assertion.Conditions.NotOnOrAfter, this.maxClockSkew);
		}

		// Token: 0x04000CD6 RID: 3286
		private List<SecurityTokenAuthenticator> supportingAuthenticators;

		// Token: 0x04000CD7 RID: 3287
		private Collection<string> allowedAudienceUris;

		// Token: 0x04000CD8 RID: 3288
		private AudienceUriMode audienceUriMode;

		// Token: 0x04000CD9 RID: 3289
		private TimeSpan maxClockSkew;
	}
}

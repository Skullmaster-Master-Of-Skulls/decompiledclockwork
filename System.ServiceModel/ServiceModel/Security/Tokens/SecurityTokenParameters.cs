using System;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Text;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x020003A3 RID: 931
	[__DynamicallyInvokable]
	public abstract class SecurityTokenParameters
	{
		// Token: 0x060022CE RID: 8910 RVA: 0x0007F6E4 File Offset: 0x0007D8E4
		protected SecurityTokenParameters(SecurityTokenParameters other)
		{
			if (other == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("other");
			}
			this.requireDerivedKeys = other.requireDerivedKeys;
			this.inclusionMode = other.inclusionMode;
			this.referenceStyle = other.referenceStyle;
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x0007F735 File Offset: 0x0007D935
		protected SecurityTokenParameters()
		{
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060022D0 RID: 8912
		protected internal abstract bool HasAsymmetricKey { get; }

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x0007F744 File Offset: 0x0007D944
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x0007F74C File Offset: 0x0007D94C
		public SecurityTokenInclusionMode InclusionMode
		{
			get
			{
				return this.inclusionMode;
			}
			set
			{
				SecurityTokenInclusionModeHelper.Validate(value);
				this.inclusionMode = value;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x0007F75B File Offset: 0x0007D95B
		// (set) Token: 0x060022D4 RID: 8916 RVA: 0x0007F763 File Offset: 0x0007D963
		public SecurityTokenReferenceStyle ReferenceStyle
		{
			get
			{
				return this.referenceStyle;
			}
			set
			{
				TokenReferenceStyleHelper.Validate(value);
				this.referenceStyle = value;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x0007F772 File Offset: 0x0007D972
		// (set) Token: 0x060022D6 RID: 8918 RVA: 0x0007F77A File Offset: 0x0007D97A
		public bool RequireDerivedKeys
		{
			get
			{
				return this.requireDerivedKeys;
			}
			set
			{
				this.requireDerivedKeys = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060022D7 RID: 8919
		protected internal abstract bool SupportsClientAuthentication { get; }

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060022D8 RID: 8920
		protected internal abstract bool SupportsServerAuthentication { get; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060022D9 RID: 8921
		protected internal abstract bool SupportsClientWindowsIdentity { get; }

		// Token: 0x060022DA RID: 8922 RVA: 0x0007F784 File Offset: 0x0007D984
		[__DynamicallyInvokable]
		public SecurityTokenParameters Clone()
		{
			SecurityTokenParameters securityTokenParameters = this.CloneCore();
			if (securityTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityTokenParametersCloneInvalidResult", new object[]
				{
					base.GetType().ToString()
				})));
			}
			return securityTokenParameters;
		}

		// Token: 0x060022DB RID: 8923
		protected abstract SecurityTokenParameters CloneCore();

		// Token: 0x060022DC RID: 8924
		protected internal abstract SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle);

		// Token: 0x060022DD RID: 8925
		protected internal abstract void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement);

		// Token: 0x060022DE RID: 8926 RVA: 0x0007F7CC File Offset: 0x0007D9CC
		internal SecurityKeyIdentifierClause CreateKeyIdentifierClause<TExternalClause, TInternalClause>(SecurityToken token, SecurityTokenReferenceStyle referenceStyle) where TExternalClause : SecurityKeyIdentifierClause where TInternalClause : SecurityKeyIdentifierClause
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			SecurityKeyIdentifierClause result;
			if (referenceStyle != SecurityTokenReferenceStyle.Internal)
			{
				if (referenceStyle != SecurityTokenReferenceStyle.External)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("TokenDoesNotSupportKeyIdentifierClauseCreation", new object[]
					{
						token.GetType().Name,
						referenceStyle
					})));
				}
				result = token.CreateKeyIdentifierClause<TExternalClause>();
			}
			else
			{
				result = token.CreateKeyIdentifierClause<TInternalClause>();
			}
			return result;
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x0007F848 File Offset: 0x0007DA48
		internal SecurityKeyIdentifierClause CreateGenericXmlTokenKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			GenericXmlSecurityToken genericXmlSecurityToken = token as GenericXmlSecurityToken;
			if (genericXmlSecurityToken != null)
			{
				if (referenceStyle == SecurityTokenReferenceStyle.Internal && genericXmlSecurityToken.InternalTokenReference != null)
				{
					return genericXmlSecurityToken.InternalTokenReference;
				}
				if (referenceStyle == SecurityTokenReferenceStyle.External && genericXmlSecurityToken.ExternalTokenReference != null)
				{
					return genericXmlSecurityToken.ExternalTokenReference;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToCreateTokenReference")));
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x0007F8A0 File Offset: 0x0007DAA0
		protected internal virtual bool MatchesKeyIdentifierClause(SecurityToken token, SecurityKeyIdentifierClause keyIdentifierClause, SecurityTokenReferenceStyle referenceStyle)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (token is GenericXmlSecurityToken)
			{
				return this.MatchesGenericXmlTokenKeyIdentifierClause(token, keyIdentifierClause, referenceStyle);
			}
			bool result;
			if (referenceStyle != SecurityTokenReferenceStyle.Internal)
			{
				if (referenceStyle != SecurityTokenReferenceStyle.External)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("TokenDoesNotSupportKeyIdentifierClauseCreation", new object[]
					{
						token.GetType().Name,
						referenceStyle
					})));
				}
				result = (!(keyIdentifierClause is LocalIdKeyIdentifierClause) && token.MatchesKeyIdentifierClause(keyIdentifierClause));
			}
			else
			{
				result = token.MatchesKeyIdentifierClause(keyIdentifierClause);
			}
			return result;
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x0007F930 File Offset: 0x0007DB30
		internal bool MatchesGenericXmlTokenKeyIdentifierClause(SecurityToken token, SecurityKeyIdentifierClause keyIdentifierClause, SecurityTokenReferenceStyle referenceStyle)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			GenericXmlSecurityToken genericXmlSecurityToken = token as GenericXmlSecurityToken;
			bool result;
			if (genericXmlSecurityToken == null)
			{
				result = false;
			}
			else if (referenceStyle == SecurityTokenReferenceStyle.External && genericXmlSecurityToken.ExternalTokenReference != null)
			{
				result = genericXmlSecurityToken.ExternalTokenReference.Matches(keyIdentifierClause);
			}
			else
			{
				result = (referenceStyle == SecurityTokenReferenceStyle.Internal && genericXmlSecurityToken.MatchesKeyIdentifierClause(keyIdentifierClause));
			}
			return result;
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0007F98C File Offset: 0x0007DB8C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}:", new object[]
			{
				base.GetType().ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "InclusionMode: {0}", new object[]
			{
				this.inclusionMode.ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "ReferenceStyle: {0}", new object[]
			{
				this.referenceStyle.ToString()
			}));
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "RequireDerivedKeys: {0}", new object[]
			{
				this.requireDerivedKeys.ToString()
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x04001FC4 RID: 8132
		internal const SecurityTokenInclusionMode defaultInclusionMode = SecurityTokenInclusionMode.AlwaysToRecipient;

		// Token: 0x04001FC5 RID: 8133
		internal const SecurityTokenReferenceStyle defaultReferenceStyle = SecurityTokenReferenceStyle.Internal;

		// Token: 0x04001FC6 RID: 8134
		internal const bool defaultRequireDerivedKeys = true;

		// Token: 0x04001FC7 RID: 8135
		private SecurityTokenInclusionMode inclusionMode;

		// Token: 0x04001FC8 RID: 8136
		private SecurityTokenReferenceStyle referenceStyle;

		// Token: 0x04001FC9 RID: 8137
		private bool requireDerivedKeys = true;
	}
}

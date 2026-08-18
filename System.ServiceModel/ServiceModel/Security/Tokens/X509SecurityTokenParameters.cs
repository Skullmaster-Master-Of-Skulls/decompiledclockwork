using System;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Text;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x020003A9 RID: 937
	public class X509SecurityTokenParameters : SecurityTokenParameters
	{
		// Token: 0x06002322 RID: 8994 RVA: 0x0008046F File Offset: 0x0007E66F
		protected X509SecurityTokenParameters(X509SecurityTokenParameters other) : base(other)
		{
			this.x509ReferenceStyle = other.x509ReferenceStyle;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x00080484 File Offset: 0x0007E684
		public X509SecurityTokenParameters() : this(X509KeyIdentifierClauseType.Any, SecurityTokenInclusionMode.AlwaysToRecipient)
		{
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x0008048E File Offset: 0x0007E68E
		public X509SecurityTokenParameters(X509KeyIdentifierClauseType x509ReferenceStyle) : this(x509ReferenceStyle, SecurityTokenInclusionMode.AlwaysToRecipient)
		{
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00080498 File Offset: 0x0007E698
		public X509SecurityTokenParameters(X509KeyIdentifierClauseType x509ReferenceStyle, SecurityTokenInclusionMode inclusionMode) : this(x509ReferenceStyle, inclusionMode, true)
		{
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000804A3 File Offset: 0x0007E6A3
		internal X509SecurityTokenParameters(X509KeyIdentifierClauseType x509ReferenceStyle, SecurityTokenInclusionMode inclusionMode, bool requireDerivedKeys)
		{
			this.X509ReferenceStyle = x509ReferenceStyle;
			base.InclusionMode = inclusionMode;
			base.RequireDerivedKeys = requireDerivedKeys;
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x000804C0 File Offset: 0x0007E6C0
		protected internal override bool HasAsymmetricKey
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x000804C3 File Offset: 0x0007E6C3
		// (set) Token: 0x06002329 RID: 9001 RVA: 0x000804CB File Offset: 0x0007E6CB
		public X509KeyIdentifierClauseType X509ReferenceStyle
		{
			get
			{
				return this.x509ReferenceStyle;
			}
			set
			{
				X509SecurityTokenReferenceStyleHelper.Validate(value);
				this.x509ReferenceStyle = value;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x000804DA File Offset: 0x0007E6DA
		protected internal override bool SupportsClientAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x000804DD File Offset: 0x0007E6DD
		protected internal override bool SupportsServerAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x000804E0 File Offset: 0x0007E6E0
		protected internal override bool SupportsClientWindowsIdentity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000804E3 File Offset: 0x0007E6E3
		protected override SecurityTokenParameters CloneCore()
		{
			return new X509SecurityTokenParameters(this);
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000804EC File Offset: 0x0007E6EC
		protected internal override SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			SecurityKeyIdentifierClause securityKeyIdentifierClause = null;
			switch (this.x509ReferenceStyle)
			{
			default:
				if (referenceStyle == SecurityTokenReferenceStyle.External)
				{
					X509SecurityToken x509SecurityToken = token as X509SecurityToken;
					if (x509SecurityToken != null)
					{
						X509SubjectKeyIdentifierClause x509SubjectKeyIdentifierClause;
						if (X509SubjectKeyIdentifierClause.TryCreateFrom(x509SecurityToken.Certificate, out x509SubjectKeyIdentifierClause))
						{
							securityKeyIdentifierClause = x509SubjectKeyIdentifierClause;
						}
					}
					else
					{
						X509WindowsSecurityToken x509WindowsSecurityToken = token as X509WindowsSecurityToken;
						X509SubjectKeyIdentifierClause x509SubjectKeyIdentifierClause2;
						if (x509WindowsSecurityToken != null && X509SubjectKeyIdentifierClause.TryCreateFrom(x509WindowsSecurityToken.Certificate, out x509SubjectKeyIdentifierClause2))
						{
							securityKeyIdentifierClause = x509SubjectKeyIdentifierClause2;
						}
					}
					if (securityKeyIdentifierClause == null)
					{
						securityKeyIdentifierClause = token.CreateKeyIdentifierClause<X509IssuerSerialKeyIdentifierClause>();
					}
					if (securityKeyIdentifierClause == null)
					{
						securityKeyIdentifierClause = token.CreateKeyIdentifierClause<X509ThumbprintKeyIdentifierClause>();
					}
				}
				else
				{
					securityKeyIdentifierClause = token.CreateKeyIdentifierClause<LocalIdKeyIdentifierClause>();
				}
				break;
			case X509KeyIdentifierClauseType.Thumbprint:
				securityKeyIdentifierClause = base.CreateKeyIdentifierClause<X509ThumbprintKeyIdentifierClause, LocalIdKeyIdentifierClause>(token, referenceStyle);
				break;
			case X509KeyIdentifierClauseType.IssuerSerial:
				securityKeyIdentifierClause = base.CreateKeyIdentifierClause<X509IssuerSerialKeyIdentifierClause, LocalIdKeyIdentifierClause>(token, referenceStyle);
				break;
			case X509KeyIdentifierClauseType.SubjectKeyIdentifier:
				securityKeyIdentifierClause = base.CreateKeyIdentifierClause<X509SubjectKeyIdentifierClause, LocalIdKeyIdentifierClause>(token, referenceStyle);
				break;
			case X509KeyIdentifierClauseType.RawDataKeyIdentifier:
				securityKeyIdentifierClause = base.CreateKeyIdentifierClause<X509RawDataKeyIdentifierClause, LocalIdKeyIdentifierClause>(token, referenceStyle);
				break;
			}
			return securityKeyIdentifierClause;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000805A6 File Offset: 0x0007E7A6
		protected internal override void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			requirement.TokenType = SecurityTokenTypes.X509Certificate;
			requirement.RequireCryptographicToken = true;
			requirement.KeyType = SecurityKeyType.AsymmetricKey;
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000805C4 File Offset: 0x0007E7C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "X509ReferenceStyle: {0}", new object[]
			{
				this.x509ReferenceStyle.ToString()
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x04001FD6 RID: 8150
		internal const X509KeyIdentifierClauseType defaultX509ReferenceStyle = X509KeyIdentifierClauseType.Any;

		// Token: 0x04001FD7 RID: 8151
		private X509KeyIdentifierClauseType x509ReferenceStyle;
	}
}

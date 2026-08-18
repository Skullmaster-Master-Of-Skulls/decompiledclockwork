using System;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x020003A5 RID: 933
	public class SspiSecurityTokenParameters : SecurityTokenParameters
	{
		// Token: 0x060022F5 RID: 8949 RVA: 0x0007FC40 File Offset: 0x0007DE40
		protected SspiSecurityTokenParameters(SspiSecurityTokenParameters other) : base(other)
		{
			this.requireCancellation = other.requireCancellation;
			if (other.issuerBindingContext != null)
			{
				this.issuerBindingContext = other.issuerBindingContext.Clone();
			}
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x0007FC6E File Offset: 0x0007DE6E
		public SspiSecurityTokenParameters() : this(false)
		{
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x0007FC77 File Offset: 0x0007DE77
		public SspiSecurityTokenParameters(bool requireCancellation)
		{
			this.requireCancellation = requireCancellation;
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060022F8 RID: 8952 RVA: 0x0007FC86 File Offset: 0x0007DE86
		protected internal override bool HasAsymmetricKey
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x0007FC89 File Offset: 0x0007DE89
		// (set) Token: 0x060022FA RID: 8954 RVA: 0x0007FC91 File Offset: 0x0007DE91
		public bool RequireCancellation
		{
			get
			{
				return this.requireCancellation;
			}
			set
			{
				this.requireCancellation = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x0007FC9A File Offset: 0x0007DE9A
		// (set) Token: 0x060022FC RID: 8956 RVA: 0x0007FCA2 File Offset: 0x0007DEA2
		internal BindingContext IssuerBindingContext
		{
			get
			{
				return this.issuerBindingContext;
			}
			set
			{
				if (value != null)
				{
					value = value.Clone();
				}
				this.issuerBindingContext = value;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x0007FCB6 File Offset: 0x0007DEB6
		protected internal override bool SupportsClientAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x0007FCB9 File Offset: 0x0007DEB9
		protected internal override bool SupportsServerAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x0007FCBC File Offset: 0x0007DEBC
		protected internal override bool SupportsClientWindowsIdentity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x0007FCBF File Offset: 0x0007DEBF
		protected override SecurityTokenParameters CloneCore()
		{
			return new SspiSecurityTokenParameters(this);
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x0007FCC7 File Offset: 0x0007DEC7
		protected internal override SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			if (token is GenericXmlSecurityToken)
			{
				return base.CreateGenericXmlTokenKeyIdentifierClause(token, referenceStyle);
			}
			return base.CreateKeyIdentifierClause<SecurityContextKeyIdentifierClause, LocalIdKeyIdentifierClause>(token, referenceStyle);
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x0007FCE4 File Offset: 0x0007DEE4
		protected internal override void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			requirement.TokenType = ServiceModelSecurityTokenTypes.Spnego;
			requirement.RequireCryptographicToken = true;
			requirement.KeyType = SecurityKeyType.SymmetricKey;
			requirement.Properties[ServiceModelSecurityTokenRequirement.SupportSecurityContextCancellationProperty] = this.RequireCancellation;
			if (this.IssuerBindingContext != null)
			{
				requirement.Properties[ServiceModelSecurityTokenRequirement.IssuerBindingContextProperty] = this.IssuerBindingContext.Clone();
			}
			requirement.Properties[ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty] = base.Clone();
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0007FD60 File Offset: 0x0007DF60
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "RequireCancellation: {0}", new object[]
			{
				this.RequireCancellation.ToString()
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x04001FCF RID: 8143
		internal const bool defaultRequireCancellation = false;

		// Token: 0x04001FD0 RID: 8144
		private bool requireCancellation;

		// Token: 0x04001FD1 RID: 8145
		private BindingContext issuerBindingContext;
	}
}

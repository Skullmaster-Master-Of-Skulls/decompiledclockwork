using System;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x020003A4 RID: 932
	public class SslSecurityTokenParameters : SecurityTokenParameters
	{
		// Token: 0x060022E3 RID: 8931 RVA: 0x0007FA59 File Offset: 0x0007DC59
		protected SslSecurityTokenParameters(SslSecurityTokenParameters other) : base(other)
		{
			this.requireClientCertificate = other.requireClientCertificate;
			this.requireCancellation = other.requireCancellation;
			if (other.issuerBindingContext != null)
			{
				this.issuerBindingContext = other.issuerBindingContext.Clone();
			}
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0007FA93 File Offset: 0x0007DC93
		public SslSecurityTokenParameters() : this(false)
		{
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0007FA9C File Offset: 0x0007DC9C
		public SslSecurityTokenParameters(bool requireClientCertificate) : this(requireClientCertificate, false)
		{
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0007FAA6 File Offset: 0x0007DCA6
		public SslSecurityTokenParameters(bool requireClientCertificate, bool requireCancellation)
		{
			this.requireClientCertificate = requireClientCertificate;
			this.requireCancellation = requireCancellation;
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x0007FABC File Offset: 0x0007DCBC
		protected internal override bool HasAsymmetricKey
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x0007FABF File Offset: 0x0007DCBF
		// (set) Token: 0x060022E9 RID: 8937 RVA: 0x0007FAC7 File Offset: 0x0007DCC7
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

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x0007FAD0 File Offset: 0x0007DCD0
		// (set) Token: 0x060022EB RID: 8939 RVA: 0x0007FAD8 File Offset: 0x0007DCD8
		public bool RequireClientCertificate
		{
			get
			{
				return this.requireClientCertificate;
			}
			set
			{
				this.requireClientCertificate = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x0007FAE1 File Offset: 0x0007DCE1
		// (set) Token: 0x060022ED RID: 8941 RVA: 0x0007FAE9 File Offset: 0x0007DCE9
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

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x0007FAFD File Offset: 0x0007DCFD
		protected internal override bool SupportsClientAuthentication
		{
			get
			{
				return this.requireClientCertificate;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x0007FB05 File Offset: 0x0007DD05
		protected internal override bool SupportsServerAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x0007FB08 File Offset: 0x0007DD08
		protected internal override bool SupportsClientWindowsIdentity
		{
			get
			{
				return this.requireClientCertificate;
			}
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0007FB10 File Offset: 0x0007DD10
		protected override SecurityTokenParameters CloneCore()
		{
			return new SslSecurityTokenParameters(this);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0007FB18 File Offset: 0x0007DD18
		protected internal override SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			if (token is GenericXmlSecurityToken)
			{
				return base.CreateGenericXmlTokenKeyIdentifierClause(token, referenceStyle);
			}
			return base.CreateKeyIdentifierClause<SecurityContextKeyIdentifierClause, LocalIdKeyIdentifierClause>(token, referenceStyle);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x0007FB34 File Offset: 0x0007DD34
		protected internal override void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			requirement.TokenType = (this.RequireClientCertificate ? ServiceModelSecurityTokenTypes.MutualSslnego : ServiceModelSecurityTokenTypes.AnonymousSslnego);
			requirement.RequireCryptographicToken = true;
			requirement.KeyType = SecurityKeyType.SymmetricKey;
			requirement.Properties[ServiceModelSecurityTokenRequirement.SupportSecurityContextCancellationProperty] = this.RequireCancellation;
			if (this.IssuerBindingContext != null)
			{
				requirement.Properties[ServiceModelSecurityTokenRequirement.IssuerBindingContextProperty] = this.IssuerBindingContext.Clone();
			}
			requirement.Properties[ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty] = base.Clone();
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x0007FBC0 File Offset: 0x0007DDC0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "RequireCancellation: {0}", new object[]
			{
				this.RequireCancellation.ToString()
			}));
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "RequireClientCertificate: {0}", new object[]
			{
				this.RequireClientCertificate.ToString()
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x04001FCA RID: 8138
		internal const bool defaultRequireClientCertificate = false;

		// Token: 0x04001FCB RID: 8139
		internal const bool defaultRequireCancellation = false;

		// Token: 0x04001FCC RID: 8140
		private bool requireCancellation;

		// Token: 0x04001FCD RID: 8141
		private bool requireClientCertificate;

		// Token: 0x04001FCE RID: 8142
		private BindingContext issuerBindingContext;
	}
}

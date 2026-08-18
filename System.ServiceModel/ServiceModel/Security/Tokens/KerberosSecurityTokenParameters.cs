using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200039D RID: 925
	public class KerberosSecurityTokenParameters : SecurityTokenParameters
	{
		// Token: 0x06002298 RID: 8856 RVA: 0x0007F0B9 File Offset: 0x0007D2B9
		protected KerberosSecurityTokenParameters(KerberosSecurityTokenParameters other) : base(other)
		{
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0007F0C2 File Offset: 0x0007D2C2
		public KerberosSecurityTokenParameters()
		{
			base.InclusionMode = SecurityTokenInclusionMode.Once;
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x0007F0D1 File Offset: 0x0007D2D1
		protected internal override bool HasAsymmetricKey
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x0007F0D4 File Offset: 0x0007D2D4
		protected internal override bool SupportsClientAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x0007F0D7 File Offset: 0x0007D2D7
		protected internal override bool SupportsServerAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x0007F0DA File Offset: 0x0007D2DA
		protected internal override bool SupportsClientWindowsIdentity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0007F0DD File Offset: 0x0007D2DD
		protected override SecurityTokenParameters CloneCore()
		{
			return new KerberosSecurityTokenParameters(this);
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0007F0E5 File Offset: 0x0007D2E5
		protected internal override SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			return base.CreateKeyIdentifierClause<KerberosTicketHashKeyIdentifierClause, LocalIdKeyIdentifierClause>(token, referenceStyle);
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0007F0EF File Offset: 0x0007D2EF
		protected internal override void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			requirement.TokenType = SecurityTokenTypes.Kerberos;
			requirement.KeyType = SecurityKeyType.SymmetricKey;
			requirement.RequireCryptographicToken = true;
		}
	}
}

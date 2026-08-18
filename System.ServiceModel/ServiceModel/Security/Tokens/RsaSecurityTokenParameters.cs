using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000387 RID: 903
	public class RsaSecurityTokenParameters : SecurityTokenParameters
	{
		// Token: 0x0600216B RID: 8555 RVA: 0x0007B9E6 File Offset: 0x00079BE6
		protected RsaSecurityTokenParameters(RsaSecurityTokenParameters other) : base(other)
		{
			base.InclusionMode = SecurityTokenInclusionMode.Never;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0007B9F6 File Offset: 0x00079BF6
		public RsaSecurityTokenParameters()
		{
			base.InclusionMode = SecurityTokenInclusionMode.Never;
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x0007BA05 File Offset: 0x00079C05
		protected internal override bool HasAsymmetricKey
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x0007BA08 File Offset: 0x00079C08
		protected internal override bool SupportsClientAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x0007BA0B File Offset: 0x00079C0B
		protected internal override bool SupportsServerAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x0007BA0E File Offset: 0x00079C0E
		protected internal override bool SupportsClientWindowsIdentity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0007BA11 File Offset: 0x00079C11
		protected override SecurityTokenParameters CloneCore()
		{
			return new RsaSecurityTokenParameters(this);
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0007BA19 File Offset: 0x00079C19
		protected internal override SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			return base.CreateKeyIdentifierClause<RsaKeyIdentifierClause, RsaKeyIdentifierClause>(token, referenceStyle);
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0007BA23 File Offset: 0x00079C23
		protected internal override void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			requirement.TokenType = SecurityTokenTypes.Rsa;
			requirement.RequireCryptographicToken = true;
			requirement.KeyType = SecurityKeyType.AsymmetricKey;
		}
	}
}

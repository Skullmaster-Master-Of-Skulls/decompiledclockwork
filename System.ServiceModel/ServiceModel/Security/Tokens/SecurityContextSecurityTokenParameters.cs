using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x020003A2 RID: 930
	internal class SecurityContextSecurityTokenParameters : SecurityTokenParameters
	{
		// Token: 0x060022C5 RID: 8901 RVA: 0x0007F693 File Offset: 0x0007D893
		protected SecurityContextSecurityTokenParameters(SecurityContextSecurityTokenParameters other) : base(other)
		{
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0007F69C File Offset: 0x0007D89C
		public SecurityContextSecurityTokenParameters()
		{
			base.InclusionMode = SecurityTokenInclusionMode.AlwaysToRecipient;
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x0007F6AB File Offset: 0x0007D8AB
		protected internal override bool SupportsClientAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x0007F6AE File Offset: 0x0007D8AE
		protected internal override bool SupportsServerAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x0007F6B1 File Offset: 0x0007D8B1
		protected internal override bool SupportsClientWindowsIdentity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x0007F6B4 File Offset: 0x0007D8B4
		protected internal override bool HasAsymmetricKey
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0007F6B7 File Offset: 0x0007D8B7
		protected override SecurityTokenParameters CloneCore()
		{
			return new SecurityContextSecurityTokenParameters(this);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0007F6BF File Offset: 0x0007D8BF
		protected internal override SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			return base.CreateKeyIdentifierClause<SecurityContextKeyIdentifierClause, LocalIdKeyIdentifierClause>(token, referenceStyle);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0007F6C9 File Offset: 0x0007D8C9
		protected internal override void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			requirement.TokenType = ServiceModelSecurityTokenTypes.SecurityContext;
			requirement.KeyType = SecurityKeyType.SymmetricKey;
			requirement.RequireCryptographicToken = true;
		}
	}
}

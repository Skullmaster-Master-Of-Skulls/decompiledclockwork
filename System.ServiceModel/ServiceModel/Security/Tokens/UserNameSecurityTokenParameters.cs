using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x020003A7 RID: 935
	[__DynamicallyInvokable]
	public class UserNameSecurityTokenParameters : SecurityTokenParameters
	{
		// Token: 0x06002310 RID: 8976 RVA: 0x000803D7 File Offset: 0x0007E5D7
		protected UserNameSecurityTokenParameters(UserNameSecurityTokenParameters other) : base(other)
		{
			base.RequireDerivedKeys = false;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000803E7 File Offset: 0x0007E5E7
		[__DynamicallyInvokable]
		public UserNameSecurityTokenParameters()
		{
			base.RequireDerivedKeys = false;
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x000803F6 File Offset: 0x0007E5F6
		protected internal override bool HasAsymmetricKey
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x000803F9 File Offset: 0x0007E5F9
		protected internal override bool SupportsClientAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x000803FC File Offset: 0x0007E5FC
		protected internal override bool SupportsServerAuthentication
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x000803FF File Offset: 0x0007E5FF
		protected internal override bool SupportsClientWindowsIdentity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00080402 File Offset: 0x0007E602
		protected override SecurityTokenParameters CloneCore()
		{
			return new UserNameSecurityTokenParameters(this);
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x0008040A File Offset: 0x0007E60A
		protected internal override SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			return base.CreateKeyIdentifierClause<SecurityKeyIdentifierClause, LocalIdKeyIdentifierClause>(token, referenceStyle);
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x00080414 File Offset: 0x0007E614
		protected internal override void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			requirement.TokenType = SecurityTokenTypes.UserName;
			requirement.RequireCryptographicToken = false;
		}
	}
}

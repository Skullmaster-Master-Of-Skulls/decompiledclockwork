using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x02000322 RID: 802
	internal class SessionDerivedKeySecurityTokenParameters : SecurityTokenParameters
	{
		// Token: 0x06001C2A RID: 7210 RVA: 0x0006A2EC File Offset: 0x000684EC
		protected SessionDerivedKeySecurityTokenParameters(SessionDerivedKeySecurityTokenParameters other) : base(other)
		{
			this.actAsInitiator = other.actAsInitiator;
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0006A301 File Offset: 0x00068501
		public SessionDerivedKeySecurityTokenParameters(bool actAsInitiator)
		{
			this.actAsInitiator = actAsInitiator;
			base.InclusionMode = (actAsInitiator ? SecurityTokenInclusionMode.AlwaysToRecipient : SecurityTokenInclusionMode.AlwaysToInitiator);
			base.RequireDerivedKeys = false;
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x0006A324 File Offset: 0x00068524
		protected internal override bool SupportsClientAuthentication
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0006A327 File Offset: 0x00068527
		protected internal override bool SupportsServerAuthentication
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x0006A32A File Offset: 0x0006852A
		protected internal override bool SupportsClientWindowsIdentity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0006A32D File Offset: 0x0006852D
		protected internal override bool HasAsymmetricKey
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0006A330 File Offset: 0x00068530
		protected override SecurityTokenParameters CloneCore()
		{
			return new SessionDerivedKeySecurityTokenParameters(this);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0006A338 File Offset: 0x00068538
		protected internal override SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			if (referenceStyle == SecurityTokenReferenceStyle.Internal)
			{
				return token.CreateKeyIdentifierClause<LocalIdKeyIdentifierClause>();
			}
			return null;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0006A348 File Offset: 0x00068548
		protected internal override bool MatchesKeyIdentifierClause(SecurityToken token, SecurityKeyIdentifierClause keyIdentifierClause, SecurityTokenReferenceStyle referenceStyle)
		{
			if (referenceStyle == SecurityTokenReferenceStyle.Internal)
			{
				LocalIdKeyIdentifierClause localIdKeyIdentifierClause = keyIdentifierClause as LocalIdKeyIdentifierClause;
				return localIdKeyIdentifierClause != null && localIdKeyIdentifierClause.LocalId == token.Id;
			}
			return false;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0006A377 File Offset: 0x00068577
		protected internal override void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x04001DC7 RID: 7623
		private bool actAsInitiator;
	}
}

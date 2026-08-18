using System;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x0200027B RID: 635
	internal class InfoCardChannelParameter
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x000432B0 File Offset: 0x000414B0
		public SecurityToken Token
		{
			get
			{
				return this.m_token;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x000432B8 File Offset: 0x000414B8
		public Uri RelyingPartyIssuer
		{
			get
			{
				return this.m_relyingPartyIssuer;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x000432C0 File Offset: 0x000414C0
		public bool RequiresInfoCard
		{
			get
			{
				return this.m_requiresInfocard;
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000432C8 File Offset: 0x000414C8
		public InfoCardChannelParameter(SecurityToken token, Uri relyingIssuer, bool requiresInfoCard)
		{
			this.m_token = token;
			this.m_relyingPartyIssuer = relyingIssuer;
			this.m_requiresInfocard = requiresInfoCard;
		}

		// Token: 0x040019D0 RID: 6608
		private SecurityToken m_token;

		// Token: 0x040019D1 RID: 6609
		private Uri m_relyingPartyIssuer;

		// Token: 0x040019D2 RID: 6610
		private bool m_requiresInfocard;
	}
}

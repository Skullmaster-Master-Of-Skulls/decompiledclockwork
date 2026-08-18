using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x0200036A RID: 874
	internal class WSSecurityXXX2005 : WSSecurityJan2004
	{
		// Token: 0x06001FF9 RID: 8185 RVA: 0x00077A26 File Offset: 0x00075C26
		public WSSecurityXXX2005(WSSecurityTokenSerializer tokenSerializer, SamlSerializer samlSerializer) : base(tokenSerializer, samlSerializer)
		{
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00077A30 File Offset: 0x00075C30
		public override void PopulateTokenEntries(IList<WSSecurityTokenSerializer.TokenEntry> tokenEntryList)
		{
			base.PopulateJan2004TokenEntries(tokenEntryList);
			tokenEntryList.Add(new WSSecurityXXX2005.WrappedKeyTokenEntry(base.WSSecurityTokenSerializer));
			tokenEntryList.Add(new WSSecurityXXX2005.SamlTokenEntry(base.WSSecurityTokenSerializer, base.SamlSerializer));
		}

		// Token: 0x02000B91 RID: 2961
		private new class SamlTokenEntry : WSSecurityJan2004.SamlTokenEntry
		{
			// Token: 0x0600734A RID: 29514 RVA: 0x001AE1C6 File Offset: 0x001AC3C6
			public SamlTokenEntry(WSSecurityTokenSerializer tokenSerializer, SamlSerializer samlSerializer) : base(tokenSerializer, samlSerializer)
			{
			}

			// Token: 0x17001AB5 RID: 6837
			// (get) Token: 0x0600734B RID: 29515 RVA: 0x001AE1D0 File Offset: 0x001AC3D0
			public override string TokenTypeUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1";
				}
			}
		}

		// Token: 0x02000B92 RID: 2962
		private new class WrappedKeyTokenEntry : WSSecurityJan2004.WrappedKeyTokenEntry
		{
			// Token: 0x0600734C RID: 29516 RVA: 0x001AE1D7 File Offset: 0x001AC3D7
			public WrappedKeyTokenEntry(WSSecurityTokenSerializer tokenSerializer) : base(tokenSerializer)
			{
			}

			// Token: 0x17001AB6 RID: 6838
			// (get) Token: 0x0600734D RID: 29517 RVA: 0x001AE1E0 File Offset: 0x001AC3E0
			public override string TokenTypeUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey";
				}
			}
		}
	}
}

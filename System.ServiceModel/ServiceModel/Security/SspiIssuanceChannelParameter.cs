using System;
using System.IdentityModel;

namespace System.ServiceModel.Security
{
	// Token: 0x0200030A RID: 778
	internal class SspiIssuanceChannelParameter
	{
		// Token: 0x06001AB8 RID: 6840 RVA: 0x00064230 File Offset: 0x00062430
		public SspiIssuanceChannelParameter(bool getTokenOnOpen, SafeFreeCredentials credentialsHandle)
		{
			this.getTokenOnOpen = getTokenOnOpen;
			this.credentialsHandle = credentialsHandle;
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x00064246 File Offset: 0x00062446
		public bool GetTokenOnOpen
		{
			get
			{
				return this.getTokenOnOpen;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0006424E File Offset: 0x0006244E
		public SafeFreeCredentials CredentialsHandle
		{
			get
			{
				return this.credentialsHandle;
			}
		}

		// Token: 0x04001D31 RID: 7473
		private bool getTokenOnOpen;

		// Token: 0x04001D32 RID: 7474
		private SafeFreeCredentials credentialsHandle;
	}
}

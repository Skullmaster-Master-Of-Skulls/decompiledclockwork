using System;

namespace System.ServiceModel.Security
{
	// Token: 0x02000314 RID: 788
	internal interface IChannelSecureConversationSessionSettings
	{
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001B2E RID: 6958
		// (set) Token: 0x06001B2F RID: 6959
		TimeSpan KeyRenewalInterval { get; set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001B30 RID: 6960
		// (set) Token: 0x06001B31 RID: 6961
		TimeSpan KeyRolloverInterval { get; set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001B32 RID: 6962
		// (set) Token: 0x06001B33 RID: 6963
		bool TolerateTransportFailures { get; set; }
	}
}

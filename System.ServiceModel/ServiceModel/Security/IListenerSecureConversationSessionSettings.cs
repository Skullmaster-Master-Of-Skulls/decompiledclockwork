using System;

namespace System.ServiceModel.Security
{
	// Token: 0x02000315 RID: 789
	internal interface IListenerSecureConversationSessionSettings
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001B34 RID: 6964
		// (set) Token: 0x06001B35 RID: 6965
		bool TolerateTransportFailures { get; set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001B36 RID: 6966
		// (set) Token: 0x06001B37 RID: 6967
		int MaximumPendingSessions { get; set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001B38 RID: 6968
		// (set) Token: 0x06001B39 RID: 6969
		TimeSpan InactivityTimeout { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001B3A RID: 6970
		// (set) Token: 0x06001B3B RID: 6971
		TimeSpan MaximumKeyRenewalInterval { get; set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001B3C RID: 6972
		// (set) Token: 0x06001B3D RID: 6973
		TimeSpan KeyRolloverInterval { get; set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001B3E RID: 6974
		// (set) Token: 0x06001B3F RID: 6975
		int MaximumPendingKeysPerSession { get; set; }
	}
}

using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000913 RID: 2323
	internal interface IReliableFactorySettings
	{
		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x060058AA RID: 22698
		TimeSpan AcknowledgementInterval { get; }

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x060058AB RID: 22699
		bool FlowControlEnabled { get; }

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x060058AC RID: 22700
		TimeSpan InactivityTimeout { get; }

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x060058AD RID: 22701
		int MaxPendingChannels { get; }

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x060058AE RID: 22702
		int MaxRetryCount { get; }

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x060058AF RID: 22703
		int MaxTransferWindowSize { get; }

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x060058B0 RID: 22704
		MessageVersion MessageVersion { get; }

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x060058B1 RID: 22705
		bool Ordered { get; }

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x060058B2 RID: 22706
		ReliableMessagingVersion ReliableMessagingVersion { get; }

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x060058B3 RID: 22707
		TimeSpan SendTimeout { get; }
	}
}

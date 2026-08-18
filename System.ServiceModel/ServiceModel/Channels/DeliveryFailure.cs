using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008D1 RID: 2257
	public enum DeliveryFailure
	{
		// Token: 0x0400351E RID: 13598
		Unknown,
		// Token: 0x0400351F RID: 13599
		AccessDenied = 32772,
		// Token: 0x04003520 RID: 13600
		BadDestinationQueue = 32768,
		// Token: 0x04003521 RID: 13601
		BadEncryption = 32775,
		// Token: 0x04003522 RID: 13602
		BadSignature = 32774,
		// Token: 0x04003523 RID: 13603
		CouldNotEncrypt = 32776,
		// Token: 0x04003524 RID: 13604
		HopCountExceeded = 32773,
		// Token: 0x04003525 RID: 13605
		NotTransactionalQueue = 32777,
		// Token: 0x04003526 RID: 13606
		NotTransactionalMessage,
		// Token: 0x04003527 RID: 13607
		Purged = 32769,
		// Token: 0x04003528 RID: 13608
		QueueDeleted = 49152,
		// Token: 0x04003529 RID: 13609
		QueueExceedMaximumSize = 32771,
		// Token: 0x0400352A RID: 13610
		QueuePurged = 49153,
		// Token: 0x0400352B RID: 13611
		ReachQueueTimeout = 32770,
		// Token: 0x0400352C RID: 13612
		ReceiveTimeout = 49154
	}
}

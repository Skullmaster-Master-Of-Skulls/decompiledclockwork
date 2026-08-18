using System;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A7F RID: 2687
	[Flags]
	internal enum MessageLoggingSource
	{
		// Token: 0x04003C64 RID: 15460
		None = 0,
		// Token: 0x04003C65 RID: 15461
		TransportReceive = 2,
		// Token: 0x04003C66 RID: 15462
		TransportSend = 4,
		// Token: 0x04003C67 RID: 15463
		Transport = 6,
		// Token: 0x04003C68 RID: 15464
		ServiceLevelReceiveDatagram = 16,
		// Token: 0x04003C69 RID: 15465
		ServiceLevelSendDatagram = 32,
		// Token: 0x04003C6A RID: 15466
		ServiceLevelReceiveRequest = 64,
		// Token: 0x04003C6B RID: 15467
		ServiceLevelSendRequest = 128,
		// Token: 0x04003C6C RID: 15468
		ServiceLevelReceiveReply = 256,
		// Token: 0x04003C6D RID: 15469
		ServiceLevelSendReply = 512,
		// Token: 0x04003C6E RID: 15470
		ServiceLevelReceive = 336,
		// Token: 0x04003C6F RID: 15471
		ServiceLevelSend = 672,
		// Token: 0x04003C70 RID: 15472
		ServiceLevelService = 592,
		// Token: 0x04003C71 RID: 15473
		ServiceLevelProxy = 416,
		// Token: 0x04003C72 RID: 15474
		ServiceLevel = 1008,
		// Token: 0x04003C73 RID: 15475
		Malformed = 1024,
		// Token: 0x04003C74 RID: 15476
		LastChance = 2048,
		// Token: 0x04003C75 RID: 15477
		All = 2147483647
	}
}

using System;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A71 RID: 2673
	internal enum ActivityType
	{
		// Token: 0x04003C37 RID: 15415
		Unknown,
		// Token: 0x04003C38 RID: 15416
		Close,
		// Token: 0x04003C39 RID: 15417
		Construct,
		// Token: 0x04003C3A RID: 15418
		ExecuteUserCode,
		// Token: 0x04003C3B RID: 15419
		ListenAt,
		// Token: 0x04003C3C RID: 15420
		Open,
		// Token: 0x04003C3D RID: 15421
		OpenClient,
		// Token: 0x04003C3E RID: 15422
		ProcessMessage,
		// Token: 0x04003C3F RID: 15423
		ProcessAction,
		// Token: 0x04003C40 RID: 15424
		ReceiveBytes,
		// Token: 0x04003C41 RID: 15425
		SecuritySetup,
		// Token: 0x04003C42 RID: 15426
		TransferToComPlus,
		// Token: 0x04003C43 RID: 15427
		WmiGetObject,
		// Token: 0x04003C44 RID: 15428
		WmiPutInstance,
		// Token: 0x04003C45 RID: 15429
		NumItems
	}
}

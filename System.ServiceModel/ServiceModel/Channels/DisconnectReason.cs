using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A38 RID: 2616
	internal enum DisconnectReason
	{
		// Token: 0x04003B91 RID: 15249
		LeavingMesh = 2,
		// Token: 0x04003B92 RID: 15250
		NotUsefulNeighbor,
		// Token: 0x04003B93 RID: 15251
		DuplicateNeighbor,
		// Token: 0x04003B94 RID: 15252
		DuplicateNodeId,
		// Token: 0x04003B95 RID: 15253
		NodeBusy,
		// Token: 0x04003B96 RID: 15254
		InternalFailure = 10
	}
}

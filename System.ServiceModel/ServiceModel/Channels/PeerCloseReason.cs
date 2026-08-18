using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A07 RID: 2567
	internal enum PeerCloseReason
	{
		// Token: 0x04003A9B RID: 15003
		None,
		// Token: 0x04003A9C RID: 15004
		InvalidNeighbor,
		// Token: 0x04003A9D RID: 15005
		LeavingMesh,
		// Token: 0x04003A9E RID: 15006
		NotUsefulNeighbor,
		// Token: 0x04003A9F RID: 15007
		DuplicateNeighbor,
		// Token: 0x04003AA0 RID: 15008
		DuplicateNodeId,
		// Token: 0x04003AA1 RID: 15009
		NodeBusy,
		// Token: 0x04003AA2 RID: 15010
		ConnectTimedOut,
		// Token: 0x04003AA3 RID: 15011
		Faulted,
		// Token: 0x04003AA4 RID: 15012
		Closed,
		// Token: 0x04003AA5 RID: 15013
		InternalFailure,
		// Token: 0x04003AA6 RID: 15014
		AuthenticationFailure,
		// Token: 0x04003AA7 RID: 15015
		NodeTooSlow
	}
}

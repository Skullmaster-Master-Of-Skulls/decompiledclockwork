using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A3C RID: 2620
	internal static class PeerConnectorHelper
	{
		// Token: 0x060067E0 RID: 26592 RVA: 0x00183F56 File Offset: 0x00182156
		public static bool IsDefined(DisconnectReason value)
		{
			return value == DisconnectReason.LeavingMesh || value == DisconnectReason.NotUsefulNeighbor || value == DisconnectReason.DuplicateNeighbor || value == DisconnectReason.DuplicateNodeId || value == DisconnectReason.NodeBusy || value == DisconnectReason.InternalFailure;
		}

		// Token: 0x060067E1 RID: 26593 RVA: 0x00183F73 File Offset: 0x00182173
		public static bool IsDefined(RefuseReason value)
		{
			return value == RefuseReason.DuplicateNodeId || value == RefuseReason.DuplicateNeighbor || value == RefuseReason.NodeBusy;
		}
	}
}

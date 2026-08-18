using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009EF RID: 2543
	internal static class PeerNeighborStateHelper
	{
		// Token: 0x060064B1 RID: 25777 RVA: 0x00177F13 File Offset: 0x00176113
		public static bool IsSettable(PeerNeighborState state)
		{
			return state == PeerNeighborState.Authenticated || state == PeerNeighborState.Connecting || state == PeerNeighborState.Connected || state == PeerNeighborState.Disconnecting || state == PeerNeighborState.Disconnected;
		}

		// Token: 0x060064B2 RID: 25778 RVA: 0x00177F2B File Offset: 0x0017612B
		public static bool IsConnected(PeerNeighborState state)
		{
			return state == PeerNeighborState.Connected;
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x00177F31 File Offset: 0x00176131
		public static bool IsAuthenticatedOrClosed(PeerNeighborState state)
		{
			return state == PeerNeighborState.Authenticated || state == PeerNeighborState.Faulted || state == PeerNeighborState.Closed;
		}
	}
}

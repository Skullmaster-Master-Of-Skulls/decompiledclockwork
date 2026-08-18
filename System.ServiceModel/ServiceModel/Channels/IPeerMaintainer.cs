using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A1A RID: 2586
	internal interface IPeerMaintainer
	{
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06006641 RID: 26177
		// (remove) Token: 0x06006642 RID: 26178
		event NeighborClosedHandler NeighborClosed;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06006643 RID: 26179
		// (remove) Token: 0x06006644 RID: 26180
		event NeighborConnectedHandler NeighborConnected;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06006645 RID: 26181
		// (remove) Token: 0x06006646 RID: 26182
		event MaintainerClosedHandler MaintainerClosed;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06006647 RID: 26183
		// (remove) Token: 0x06006648 RID: 26184
		event ReferralsAddedHandler ReferralsAdded;

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06006649 RID: 26185
		int ConnectedNeighborCount { get; }

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x0600664A RID: 26186
		int NonClosingNeighborCount { get; }

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x0600664B RID: 26187
		bool IsOpen { get; }

		// Token: 0x0600664C RID: 26188
		IAsyncResult BeginOpenNeighbor(PeerNodeAddress to, TimeSpan timeout, AsyncCallback callback, object asyncState);

		// Token: 0x0600664D RID: 26189
		IPeerNeighbor EndOpenNeighbor(IAsyncResult result);

		// Token: 0x0600664E RID: 26190
		void CloseNeighbor(IPeerNeighbor neighbor, PeerCloseReason closeReason);

		// Token: 0x0600664F RID: 26191
		IPeerNeighbor FindDuplicateNeighbor(PeerNodeAddress address);

		// Token: 0x06006650 RID: 26192
		PeerNodeAddress GetListenAddress();

		// Token: 0x06006651 RID: 26193
		IPeerNeighbor GetLeastUsefulNeighbor();
	}
}

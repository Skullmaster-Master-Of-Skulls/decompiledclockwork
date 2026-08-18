using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A1D RID: 2589
	internal class PeerMaintainer : PeerMaintainerBase<ConnectAlgorithms>
	{
		// Token: 0x06006679 RID: 26233 RVA: 0x0017DB67 File Offset: 0x0017BD67
		public PeerMaintainer(PeerNodeConfig config, PeerNeighborManager neighborManager, PeerFlooder flooder) : base(config, neighborManager, flooder)
		{
		}
	}
}

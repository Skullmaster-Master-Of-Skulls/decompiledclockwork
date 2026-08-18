using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009F9 RID: 2553
	internal class PeerFlooder : PeerFlooderSimple
	{
		// Token: 0x0600654A RID: 25930 RVA: 0x00179AEA File Offset: 0x00177CEA
		private PeerFlooder(PeerNodeConfig config, PeerNeighborManager neighborManager) : base(config, neighborManager)
		{
		}

		// Token: 0x0600654B RID: 25931 RVA: 0x00179AF4 File Offset: 0x00177CF4
		public static PeerFlooder CreateFlooder(PeerNodeConfig config, PeerNeighborManager neighborManager, IPeerNodeMessageHandling messageHandler)
		{
			return new PeerFlooder(config, neighborManager)
			{
				messageHandler = messageHandler
			};
		}
	}
}

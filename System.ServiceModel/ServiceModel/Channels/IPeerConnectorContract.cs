using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A33 RID: 2611
	internal interface IPeerConnectorContract
	{
		// Token: 0x060067AF RID: 26543
		void Connect(IPeerNeighbor neighbor, ConnectInfo connectInfo);

		// Token: 0x060067B0 RID: 26544
		void Disconnect(IPeerNeighbor neighbor, DisconnectInfo disconnectInfo);

		// Token: 0x060067B1 RID: 26545
		void Refuse(IPeerNeighbor neighbor, RefuseInfo refuseInfo);

		// Token: 0x060067B2 RID: 26546
		void Welcome(IPeerNeighbor neighbor, WelcomeInfo welcomeInfo);
	}
}

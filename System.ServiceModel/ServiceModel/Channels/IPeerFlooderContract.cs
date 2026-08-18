using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A34 RID: 2612
	internal interface IPeerFlooderContract<TFloodContract, TLinkContract>
	{
		// Token: 0x060067B3 RID: 26547
		IAsyncResult OnFloodedMessage(IPeerNeighbor neighbor, TFloodContract floodedInfo, AsyncCallback callback, object state);

		// Token: 0x060067B4 RID: 26548
		void EndFloodMessage(IAsyncResult result);

		// Token: 0x060067B5 RID: 26549
		void ProcessLinkUtility(IPeerNeighbor neighbor, TLinkContract utilityInfo);
	}
}

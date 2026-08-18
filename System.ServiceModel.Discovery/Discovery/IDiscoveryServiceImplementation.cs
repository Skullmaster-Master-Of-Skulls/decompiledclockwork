using System;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200003A RID: 58
	internal interface IDiscoveryServiceImplementation
	{
		// Token: 0x060002E5 RID: 741
		bool IsDuplicate(UniqueId messageId);

		// Token: 0x060002E6 RID: 742
		DiscoveryMessageSequence GetNextMessageSequence();

		// Token: 0x060002E7 RID: 743
		IAsyncResult BeginFind(FindRequestContext findRequestContext, AsyncCallback callback, object state);

		// Token: 0x060002E8 RID: 744
		void EndFind(IAsyncResult result);

		// Token: 0x060002E9 RID: 745
		IAsyncResult BeginResolve(ResolveCriteria resolveCriteria, AsyncCallback callback, object state);

		// Token: 0x060002EA RID: 746
		EndpointDiscoveryMetadata EndResolve(IAsyncResult result);
	}
}

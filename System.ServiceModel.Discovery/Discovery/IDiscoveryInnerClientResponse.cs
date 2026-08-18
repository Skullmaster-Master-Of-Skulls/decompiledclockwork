using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000038 RID: 56
	internal interface IDiscoveryInnerClientResponse
	{
		// Token: 0x060002D8 RID: 728
		void HelloOperation(UniqueId relatesTo, DiscoveryMessageSequence proxyMessageSequence, EndpointDiscoveryMetadata proxyEndpointMetadata);

		// Token: 0x060002D9 RID: 729
		void ProbeMatchOperation(UniqueId relatesTo, DiscoveryMessageSequence discoveryMessageSequence, Collection<EndpointDiscoveryMetadata> endpointDiscoveryMetadataCollection, bool findCompleted);

		// Token: 0x060002DA RID: 730
		void ResolveMatchOperation(UniqueId relatesTo, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata);

		// Token: 0x060002DB RID: 731
		void PostFindCompletedAndRemove(UniqueId operationId, bool cancelled, Exception error);

		// Token: 0x060002DC RID: 732
		void PostResolveCompletedAndRemove(UniqueId operationId, bool cancelled, Exception error);
	}
}

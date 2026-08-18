using System;
using System.Collections.ObjectModel;
using System.Runtime;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000039 RID: 57
	internal interface IDiscoveryRequestContext
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002DD RID: 733
		TimeoutHelper TimeoutHelper { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002DE RID: 734
		ServiceDiscoveryMode DiscoveryMode { get; }

		// Token: 0x060002DF RID: 735
		IAsyncResult BeginSendFindResponse(Collection<EndpointDiscoveryMetadata> matchingEndpoints, AsyncCallback callback, object state);

		// Token: 0x060002E0 RID: 736
		void EndSendFindResponse(IAsyncResult result);

		// Token: 0x060002E1 RID: 737
		IAsyncResult BeginSendResolveResponse(EndpointDiscoveryMetadata matchingEndpoint, AsyncCallback callback, object state);

		// Token: 0x060002E2 RID: 738
		void EndSendResolveResponse(IAsyncResult result);

		// Token: 0x060002E3 RID: 739
		IAsyncResult BeginSendProxyAnnouncements(Collection<EndpointDiscoveryMetadata> proxyAnnouncementEndpoints, AsyncCallback callback, object state);

		// Token: 0x060002E4 RID: 740
		void EndSendProxyAnnouncements(IAsyncResult result);
	}
}

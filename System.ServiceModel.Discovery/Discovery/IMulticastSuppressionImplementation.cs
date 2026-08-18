using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200003C RID: 60
	internal interface IMulticastSuppressionImplementation
	{
		// Token: 0x060002F6 RID: 758
		IAsyncResult BeginShouldRedirectFind(FindCriteria findCriteria, AsyncCallback callback, object state);

		// Token: 0x060002F7 RID: 759
		bool EndShouldRedirectFind(IAsyncResult result, out Collection<EndpointDiscoveryMetadata> redirectionEndpoints);

		// Token: 0x060002F8 RID: 760
		IAsyncResult BeginShouldRedirectResolve(ResolveCriteria resolveCriteria, AsyncCallback callback, object state);

		// Token: 0x060002F9 RID: 761
		bool EndShouldRedirectResolve(IAsyncResult result, out Collection<EndpointDiscoveryMetadata> redirectionEndpoints);
	}
}

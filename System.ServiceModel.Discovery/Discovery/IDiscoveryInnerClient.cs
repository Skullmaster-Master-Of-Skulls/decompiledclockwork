using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000037 RID: 55
	internal interface IDiscoveryInnerClient
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002CC RID: 716
		ClientCredentials ClientCredentials { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002CD RID: 717
		ChannelFactory ChannelFactory { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002CE RID: 718
		IClientChannel InnerChannel { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002CF RID: 719
		ServiceEndpoint Endpoint { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002D0 RID: 720
		ICommunicationObject InnerCommunicationObject { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002D1 RID: 721
		bool IsRequestResponse { get; }

		// Token: 0x060002D2 RID: 722
		void ProbeOperation(FindCriteria findCriteria);

		// Token: 0x060002D3 RID: 723
		void ResolveOperation(ResolveCriteria resolveCriteria);

		// Token: 0x060002D4 RID: 724
		IAsyncResult BeginProbeOperation(FindCriteria findCriteria, AsyncCallback callback, object state);

		// Token: 0x060002D5 RID: 725
		IAsyncResult BeginResolveOperation(ResolveCriteria resolveCriteria, AsyncCallback callback, object state);

		// Token: 0x060002D6 RID: 726
		void EndProbeOperation(IAsyncResult result);

		// Token: 0x060002D7 RID: 727
		void EndResolveOperation(IAsyncResult result);
	}
}

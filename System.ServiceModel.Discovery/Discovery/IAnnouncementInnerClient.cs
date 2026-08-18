using System;
using System.ComponentModel;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000035 RID: 53
	internal interface IAnnouncementInnerClient
	{
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060002B4 RID: 692
		// (remove) Token: 0x060002B5 RID: 693
		event EventHandler<AsyncCompletedEventArgs> HelloOperationCompleted;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060002B6 RID: 694
		// (remove) Token: 0x060002B7 RID: 695
		event EventHandler<AsyncCompletedEventArgs> ByeOperationCompleted;

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002B8 RID: 696
		// (set) Token: 0x060002B9 RID: 697
		DiscoveryMessageSequenceGenerator DiscoveryMessageSequenceGenerator { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002BA RID: 698
		ClientCredentials ClientCredentials { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002BB RID: 699
		ChannelFactory ChannelFactory { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002BC RID: 700
		IClientChannel InnerChannel { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002BD RID: 701
		ServiceEndpoint Endpoint { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002BE RID: 702
		ICommunicationObject InnerCommunicationObject { get; }

		// Token: 0x060002BF RID: 703
		IAsyncResult BeginHelloOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state);

		// Token: 0x060002C0 RID: 704
		void EndHelloOperation(IAsyncResult result);

		// Token: 0x060002C1 RID: 705
		IAsyncResult BeginByeOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state);

		// Token: 0x060002C2 RID: 706
		void EndByeOperation(IAsyncResult result);

		// Token: 0x060002C3 RID: 707
		void HelloOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata);

		// Token: 0x060002C4 RID: 708
		void ByeOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata);

		// Token: 0x060002C5 RID: 709
		void HelloOperationAsync(EndpointDiscoveryMetadata endpointDiscoveryMetadata, object userState);

		// Token: 0x060002C6 RID: 710
		void ByeOperationAsync(EndpointDiscoveryMetadata endpointDiscoveryMetadata, object userState);
	}
}

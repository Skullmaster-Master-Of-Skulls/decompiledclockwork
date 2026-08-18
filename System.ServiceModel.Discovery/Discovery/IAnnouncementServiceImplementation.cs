using System;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000036 RID: 54
	internal interface IAnnouncementServiceImplementation
	{
		// Token: 0x060002C7 RID: 711
		bool IsDuplicate(UniqueId messageId);

		// Token: 0x060002C8 RID: 712
		IAsyncResult OnBeginOnlineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state);

		// Token: 0x060002C9 RID: 713
		void OnEndOnlineAnnouncement(IAsyncResult result);

		// Token: 0x060002CA RID: 714
		IAsyncResult OnBeginOfflineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state);

		// Token: 0x060002CB RID: 715
		void OnEndOfflineAnnouncement(IAsyncResult result);
	}
}

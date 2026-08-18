using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000720 RID: 1824
	internal interface IChannelDemuxer
	{
		// Token: 0x06004535 RID: 17717
		void OnOuterListenerOpen(ChannelDemuxerFilter filter, IChannelListener listener, TimeSpan timeout);

		// Token: 0x06004536 RID: 17718
		IAsyncResult OnBeginOuterListenerOpen(ChannelDemuxerFilter filter, IChannelListener listener, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06004537 RID: 17719
		void OnEndOuterListenerOpen(IAsyncResult result);

		// Token: 0x06004538 RID: 17720
		void OnOuterListenerAbort(ChannelDemuxerFilter filter);

		// Token: 0x06004539 RID: 17721
		void OnOuterListenerClose(ChannelDemuxerFilter filter, TimeSpan timeout);

		// Token: 0x0600453A RID: 17722
		IAsyncResult OnBeginOuterListenerClose(ChannelDemuxerFilter filter, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x0600453B RID: 17723
		void OnEndOuterListenerClose(IAsyncResult result);
	}
}

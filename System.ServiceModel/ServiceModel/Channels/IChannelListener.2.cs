using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000705 RID: 1797
	public interface IChannelListener<TChannel> : IChannelListener, ICommunicationObject where TChannel : class, IChannel
	{
		// Token: 0x060044B1 RID: 17585
		TChannel AcceptChannel();

		// Token: 0x060044B2 RID: 17586
		TChannel AcceptChannel(TimeSpan timeout);

		// Token: 0x060044B3 RID: 17587
		IAsyncResult BeginAcceptChannel(AsyncCallback callback, object state);

		// Token: 0x060044B4 RID: 17588
		IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060044B5 RID: 17589
		TChannel EndAcceptChannel(IAsyncResult result);
	}
}

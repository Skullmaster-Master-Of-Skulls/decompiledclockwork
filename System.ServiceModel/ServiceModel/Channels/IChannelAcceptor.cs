using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000744 RID: 1860
	internal interface IChannelAcceptor<TChannel> : ICommunicationObject where TChannel : class, IChannel
	{
		// Token: 0x0600470C RID: 18188
		TChannel AcceptChannel(TimeSpan timeout);

		// Token: 0x0600470D RID: 18189
		IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x0600470E RID: 18190
		TChannel EndAcceptChannel(IAsyncResult result);

		// Token: 0x0600470F RID: 18191
		bool WaitForChannel(TimeSpan timeout);

		// Token: 0x06004710 RID: 18192
		IAsyncResult BeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06004711 RID: 18193
		bool EndWaitForChannel(IAsyncResult result);
	}
}

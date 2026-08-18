using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000912 RID: 2322
	internal interface IServerReliableChannelBinder : IReliableChannelBinder
	{
		// Token: 0x060058A5 RID: 22693
		bool AddressResponse(Message request, Message response);

		// Token: 0x060058A6 RID: 22694
		bool UseNewChannel(IChannel channel);

		// Token: 0x060058A7 RID: 22695
		IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060058A8 RID: 22696
		bool EndWaitForRequest(IAsyncResult result);

		// Token: 0x060058A9 RID: 22697
		bool WaitForRequest(TimeSpan timeout);
	}
}

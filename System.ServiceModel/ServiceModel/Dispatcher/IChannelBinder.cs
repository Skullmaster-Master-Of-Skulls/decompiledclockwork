using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000569 RID: 1385
	internal interface IChannelBinder
	{
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x060035EB RID: 13803
		IChannel Channel { get; }

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x060035EC RID: 13804
		bool HasSession { get; }

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x060035ED RID: 13805
		Uri ListenUri { get; }

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x060035EE RID: 13806
		EndpointAddress LocalAddress { get; }

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060035EF RID: 13807
		EndpointAddress RemoteAddress { get; }

		// Token: 0x060035F0 RID: 13808
		void Abort();

		// Token: 0x060035F1 RID: 13809
		void CloseAfterFault(TimeSpan timeout);

		// Token: 0x060035F2 RID: 13810
		bool TryReceive(TimeSpan timeout, out RequestContext requestContext);

		// Token: 0x060035F3 RID: 13811
		IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060035F4 RID: 13812
		bool EndTryReceive(IAsyncResult result, out RequestContext requestContext);

		// Token: 0x060035F5 RID: 13813
		void Send(Message message, TimeSpan timeout);

		// Token: 0x060035F6 RID: 13814
		IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060035F7 RID: 13815
		void EndSend(IAsyncResult result);

		// Token: 0x060035F8 RID: 13816
		Message Request(Message message, TimeSpan timeout);

		// Token: 0x060035F9 RID: 13817
		IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060035FA RID: 13818
		Message EndRequest(IAsyncResult result);

		// Token: 0x060035FB RID: 13819
		bool WaitForMessage(TimeSpan timeout);

		// Token: 0x060035FC RID: 13820
		IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060035FD RID: 13821
		bool EndWaitForMessage(IAsyncResult result);

		// Token: 0x060035FE RID: 13822
		RequestContext CreateRequestContext(Message message);
	}
}

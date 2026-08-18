using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000710 RID: 1808
	public interface IReplyChannel : IChannel, ICommunicationObject
	{
		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x060044D0 RID: 17616
		EndpointAddress LocalAddress { get; }

		// Token: 0x060044D1 RID: 17617
		RequestContext ReceiveRequest();

		// Token: 0x060044D2 RID: 17618
		RequestContext ReceiveRequest(TimeSpan timeout);

		// Token: 0x060044D3 RID: 17619
		IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state);

		// Token: 0x060044D4 RID: 17620
		IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060044D5 RID: 17621
		RequestContext EndReceiveRequest(IAsyncResult result);

		// Token: 0x060044D6 RID: 17622
		bool TryReceiveRequest(TimeSpan timeout, out RequestContext context);

		// Token: 0x060044D7 RID: 17623
		IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060044D8 RID: 17624
		bool EndTryReceiveRequest(IAsyncResult result, out RequestContext context);

		// Token: 0x060044D9 RID: 17625
		bool WaitForRequest(TimeSpan timeout);

		// Token: 0x060044DA RID: 17626
		IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060044DB RID: 17627
		bool EndWaitForRequest(IAsyncResult result);
	}
}

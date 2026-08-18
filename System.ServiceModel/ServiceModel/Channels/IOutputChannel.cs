using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200070D RID: 1805
	[__DynamicallyInvokable]
	public interface IOutputChannel : IChannel, ICommunicationObject
	{
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x060044C9 RID: 17609
		[__DynamicallyInvokable]
		EndpointAddress RemoteAddress { [__DynamicallyInvokable] get; }

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x060044CA RID: 17610
		[__DynamicallyInvokable]
		Uri Via { [__DynamicallyInvokable] get; }

		// Token: 0x060044CB RID: 17611
		[__DynamicallyInvokable]
		void Send(Message message);

		// Token: 0x060044CC RID: 17612
		[__DynamicallyInvokable]
		void Send(Message message, TimeSpan timeout);

		// Token: 0x060044CD RID: 17613
		[__DynamicallyInvokable]
		IAsyncResult BeginSend(Message message, AsyncCallback callback, object state);

		// Token: 0x060044CE RID: 17614
		[__DynamicallyInvokable]
		IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060044CF RID: 17615
		[__DynamicallyInvokable]
		void EndSend(IAsyncResult result);
	}
}

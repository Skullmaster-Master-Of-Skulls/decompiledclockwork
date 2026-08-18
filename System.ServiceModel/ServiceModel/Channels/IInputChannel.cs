using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000709 RID: 1801
	[__DynamicallyInvokable]
	public interface IInputChannel : IChannel, ICommunicationObject
	{
		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x060044BB RID: 17595
		[__DynamicallyInvokable]
		EndpointAddress LocalAddress { [__DynamicallyInvokable] get; }

		// Token: 0x060044BC RID: 17596
		[__DynamicallyInvokable]
		Message Receive();

		// Token: 0x060044BD RID: 17597
		[__DynamicallyInvokable]
		Message Receive(TimeSpan timeout);

		// Token: 0x060044BE RID: 17598
		[__DynamicallyInvokable]
		IAsyncResult BeginReceive(AsyncCallback callback, object state);

		// Token: 0x060044BF RID: 17599
		[__DynamicallyInvokable]
		IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060044C0 RID: 17600
		[__DynamicallyInvokable]
		Message EndReceive(IAsyncResult result);

		// Token: 0x060044C1 RID: 17601
		[__DynamicallyInvokable]
		bool TryReceive(TimeSpan timeout, out Message message);

		// Token: 0x060044C2 RID: 17602
		[__DynamicallyInvokable]
		IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060044C3 RID: 17603
		[__DynamicallyInvokable]
		bool EndTryReceive(IAsyncResult result, out Message message);

		// Token: 0x060044C4 RID: 17604
		[__DynamicallyInvokable]
		bool WaitForMessage(TimeSpan timeout);

		// Token: 0x060044C5 RID: 17605
		[__DynamicallyInvokable]
		IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060044C6 RID: 17606
		[__DynamicallyInvokable]
		bool EndWaitForMessage(IAsyncResult result);
	}
}

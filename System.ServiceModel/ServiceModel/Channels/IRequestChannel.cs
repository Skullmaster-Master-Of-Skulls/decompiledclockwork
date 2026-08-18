using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000712 RID: 1810
	[__DynamicallyInvokable]
	public interface IRequestChannel : IChannel, ICommunicationObject
	{
		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x060044DC RID: 17628
		[__DynamicallyInvokable]
		EndpointAddress RemoteAddress { [__DynamicallyInvokable] get; }

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x060044DD RID: 17629
		[__DynamicallyInvokable]
		Uri Via { [__DynamicallyInvokable] get; }

		// Token: 0x060044DE RID: 17630
		[__DynamicallyInvokable]
		Message Request(Message message);

		// Token: 0x060044DF RID: 17631
		[__DynamicallyInvokable]
		Message Request(Message message, TimeSpan timeout);

		// Token: 0x060044E0 RID: 17632
		[__DynamicallyInvokable]
		IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state);

		// Token: 0x060044E1 RID: 17633
		[__DynamicallyInvokable]
		IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060044E2 RID: 17634
		[__DynamicallyInvokable]
		Message EndRequest(IAsyncResult result);
	}
}

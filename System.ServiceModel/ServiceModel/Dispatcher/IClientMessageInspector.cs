using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200057D RID: 1405
	[__DynamicallyInvokable]
	public interface IClientMessageInspector
	{
		// Token: 0x06003655 RID: 13909
		[__DynamicallyInvokable]
		object BeforeSendRequest(ref Message request, IClientChannel channel);

		// Token: 0x06003656 RID: 13910
		[__DynamicallyInvokable]
		void AfterReceiveReply(ref Message reply, object correlationState);
	}
}

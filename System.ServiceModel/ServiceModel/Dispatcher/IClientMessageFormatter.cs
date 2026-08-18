using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200057C RID: 1404
	[__DynamicallyInvokable]
	public interface IClientMessageFormatter
	{
		// Token: 0x06003653 RID: 13907
		[__DynamicallyInvokable]
		Message SerializeRequest(MessageVersion messageVersion, object[] parameters);

		// Token: 0x06003654 RID: 13908
		[__DynamicallyInvokable]
		object DeserializeReply(Message message, object[] parameters);
	}
}

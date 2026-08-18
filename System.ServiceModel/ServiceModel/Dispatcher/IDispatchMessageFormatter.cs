using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000584 RID: 1412
	[__DynamicallyInvokable]
	public interface IDispatchMessageFormatter
	{
		// Token: 0x06003670 RID: 13936
		[__DynamicallyInvokable]
		void DeserializeRequest(Message message, object[] parameters);

		// Token: 0x06003671 RID: 13937
		[__DynamicallyInvokable]
		Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result);
	}
}

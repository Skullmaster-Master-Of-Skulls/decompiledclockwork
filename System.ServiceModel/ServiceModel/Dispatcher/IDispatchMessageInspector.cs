using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000585 RID: 1413
	public interface IDispatchMessageInspector
	{
		// Token: 0x06003672 RID: 13938
		object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext);

		// Token: 0x06003673 RID: 13939
		void BeforeSendReply(ref Message reply, object correlationState);
	}
}

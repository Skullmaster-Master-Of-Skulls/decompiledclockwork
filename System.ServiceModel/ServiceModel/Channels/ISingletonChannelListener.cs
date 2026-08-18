using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000819 RID: 2073
	internal interface ISingletonChannelListener
	{
		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06004D71 RID: 19825
		TimeSpan ReceiveTimeout { get; }

		// Token: 0x06004D72 RID: 19826
		void ReceiveRequest(RequestContext requestContext, Action callback, bool canDispatchOnThisThread);
	}
}

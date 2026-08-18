using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000570 RID: 1392
	internal interface IListenerBinder
	{
		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06003611 RID: 13841
		IChannelListener Listener { get; }

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06003612 RID: 13842
		MessageVersion MessageVersion { get; }

		// Token: 0x06003613 RID: 13843
		IChannelBinder Accept(TimeSpan timeout);

		// Token: 0x06003614 RID: 13844
		IAsyncResult BeginAccept(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06003615 RID: 13845
		IChannelBinder EndAccept(IAsyncResult result);
	}
}

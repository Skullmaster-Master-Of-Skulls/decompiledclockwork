using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200057A RID: 1402
	internal interface IInvokeReceivedNotification
	{
		// Token: 0x0600364F RID: 13903
		void NotifyInvokeReceived();

		// Token: 0x06003650 RID: 13904
		void NotifyInvokeReceived(RequestContext request);
	}
}

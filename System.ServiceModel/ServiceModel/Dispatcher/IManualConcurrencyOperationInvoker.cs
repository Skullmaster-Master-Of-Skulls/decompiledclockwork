using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000579 RID: 1401
	internal interface IManualConcurrencyOperationInvoker : IOperationInvoker
	{
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x0600364C RID: 13900
		bool OwnsFormatter { get; }

		// Token: 0x0600364D RID: 13901
		object Invoke(object instance, object[] inputs, IInvokeReceivedNotification notification, out object[] outputs);

		// Token: 0x0600364E RID: 13902
		IAsyncResult InvokeBegin(object instance, object[] inputs, IInvokeReceivedNotification notification, AsyncCallback callback, object state);
	}
}

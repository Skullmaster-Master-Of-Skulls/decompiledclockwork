using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000578 RID: 1400
	public interface IOperationInvoker
	{
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06003647 RID: 13895
		bool IsSynchronous { get; }

		// Token: 0x06003648 RID: 13896
		object[] AllocateInputs();

		// Token: 0x06003649 RID: 13897
		object Invoke(object instance, object[] inputs, out object[] outputs);

		// Token: 0x0600364A RID: 13898
		IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state);

		// Token: 0x0600364B RID: 13899
		object InvokeEnd(object instance, out object[] outputs, IAsyncResult result);
	}
}

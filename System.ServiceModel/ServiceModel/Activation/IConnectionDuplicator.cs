using System;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005C5 RID: 1477
	internal interface IConnectionDuplicator
	{
		// Token: 0x06003996 RID: 14742
		[OperationContract(IsOneWay = false, AsyncPattern = true)]
		IAsyncResult BeginDuplicate(DuplicateContext duplicateContext, AsyncCallback callback, object state);

		// Token: 0x06003997 RID: 14743
		void EndDuplicate(IAsyncResult result);
	}
}

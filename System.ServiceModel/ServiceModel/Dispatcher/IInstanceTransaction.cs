using System;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000583 RID: 1411
	internal interface IInstanceTransaction
	{
		// Token: 0x0600366F RID: 13935
		Transaction GetTransactionForInstance(OperationContext operationContext);
	}
}

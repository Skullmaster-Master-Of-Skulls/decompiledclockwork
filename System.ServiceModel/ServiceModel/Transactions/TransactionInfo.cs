using System;
using System.Transactions;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001B5 RID: 437
	internal abstract class TransactionInfo
	{
		// Token: 0x06000E51 RID: 3665
		public abstract Transaction UnmarshalTransaction();
	}
}

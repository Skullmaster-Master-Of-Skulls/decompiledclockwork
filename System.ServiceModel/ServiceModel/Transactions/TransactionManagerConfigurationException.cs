using System;
using System.Transactions;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001B7 RID: 439
	internal class TransactionManagerConfigurationException : TransactionException
	{
		// Token: 0x06000E5B RID: 3675 RVA: 0x000336FF File Offset: 0x000318FF
		public TransactionManagerConfigurationException(string error, Exception e) : base(error, e)
		{
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00033709 File Offset: 0x00031909
		public TransactionManagerConfigurationException(string error) : base(error)
		{
		}
	}
}

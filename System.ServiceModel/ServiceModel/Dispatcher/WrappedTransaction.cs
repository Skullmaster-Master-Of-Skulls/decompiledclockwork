using System;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200058A RID: 1418
	internal class WrappedTransaction
	{
		// Token: 0x060036A7 RID: 13991 RVA: 0x000D284F File Offset: 0x000D0A4F
		internal WrappedTransaction(Transaction transaction)
		{
			this.transaction = transaction;
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x000D285E File Offset: 0x000D0A5E
		internal Transaction Transaction
		{
			get
			{
				return this.transaction;
			}
		}

		// Token: 0x040028BF RID: 10431
		private Transaction transaction;
	}
}

using System;
using System.Transactions;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001B2 RID: 434
	internal class WsatIncomingTransactionCache : TransactionCache<string, Transaction>
	{
		// Token: 0x06000E40 RID: 3648 RVA: 0x00033230 File Offset: 0x00031430
		public static void Cache(string identifier, Transaction tx)
		{
			WsatIncomingTransactionCache wsatIncomingTransactionCache = new WsatIncomingTransactionCache();
			wsatIncomingTransactionCache.AddEntry(tx, identifier, tx);
		}
	}
}

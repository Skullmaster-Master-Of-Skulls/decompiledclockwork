using System;
using System.Transactions;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001B1 RID: 433
	internal class WsatExtendedInformationCache : TransactionCache<Transaction, WsatExtendedInformation>
	{
		// Token: 0x06000E3E RID: 3646 RVA: 0x0003320C File Offset: 0x0003140C
		public static void Cache(Transaction tx, WsatExtendedInformation info)
		{
			WsatExtendedInformationCache wsatExtendedInformationCache = new WsatExtendedInformationCache();
			wsatExtendedInformationCache.AddEntry(tx, tx, info);
		}
	}
}

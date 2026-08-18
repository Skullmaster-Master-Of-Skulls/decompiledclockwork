using System;
using System.ServiceModel.Channels;
using System.Transactions;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001B4 RID: 436
	internal abstract class TransactionFormatter
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x000333DA File Offset: 0x000315DA
		public static TransactionFormatter OleTxFormatter
		{
			get
			{
				return TransactionFormatter.oleTxFormatter;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x000333E4 File Offset: 0x000315E4
		public static TransactionFormatter WsatFormatter10
		{
			get
			{
				if (TransactionFormatter.wsatFormatter10 == null)
				{
					object obj = TransactionFormatter.syncRoot;
					lock (obj)
					{
						if (TransactionFormatter.wsatFormatter10 == null)
						{
							TransactionFormatter.wsatFormatter10 = new WsatTransactionFormatter10();
						}
					}
				}
				return TransactionFormatter.wsatFormatter10;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00033444 File Offset: 0x00031644
		public static TransactionFormatter WsatFormatter11
		{
			get
			{
				if (TransactionFormatter.wsatFormatter11 == null)
				{
					object obj = TransactionFormatter.syncRoot;
					lock (obj)
					{
						if (TransactionFormatter.wsatFormatter11 == null)
						{
							TransactionFormatter.wsatFormatter11 = new WsatTransactionFormatter11();
						}
					}
				}
				return TransactionFormatter.wsatFormatter11;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E4C RID: 3660
		public abstract MessageHeader EmptyTransactionHeader { get; }

		// Token: 0x06000E4D RID: 3661
		public abstract void WriteTransaction(Transaction transaction, Message message);

		// Token: 0x06000E4E RID: 3662
		public abstract TransactionInfo ReadTransaction(Message message);

		// Token: 0x04001744 RID: 5956
		private static TransactionFormatter oleTxFormatter = new OleTxTransactionFormatter();

		// Token: 0x04001745 RID: 5957
		private static object syncRoot = new object();

		// Token: 0x04001746 RID: 5958
		private static volatile TransactionFormatter wsatFormatter10;

		// Token: 0x04001747 RID: 5959
		private static volatile TransactionFormatter wsatFormatter11;
	}
}

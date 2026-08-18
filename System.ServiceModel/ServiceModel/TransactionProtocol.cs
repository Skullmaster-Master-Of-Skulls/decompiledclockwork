using System;
using System.ComponentModel;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x02000178 RID: 376
	[TypeConverter(typeof(TransactionProtocolConverter))]
	public abstract class TransactionProtocol
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00028C5A File Offset: 0x00026E5A
		public static TransactionProtocol Default
		{
			get
			{
				return TransactionProtocol.OleTransactions;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00028C61 File Offset: 0x00026E61
		public static TransactionProtocol OleTransactions
		{
			get
			{
				return OleTransactionsProtocol.Instance;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00028C68 File Offset: 0x00026E68
		public static TransactionProtocol WSAtomicTransactionOctober2004
		{
			get
			{
				return WSAtomicTransactionOctober2004Protocol.Instance;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00028C6F File Offset: 0x00026E6F
		public static TransactionProtocol WSAtomicTransaction11
		{
			get
			{
				return WSAtomicTransaction11Protocol.Instance;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B0B RID: 2827
		internal abstract string Name { get; }

		// Token: 0x06000B0C RID: 2828 RVA: 0x00028C76 File Offset: 0x00026E76
		internal static bool IsDefined(TransactionProtocol transactionProtocol)
		{
			return transactionProtocol == TransactionProtocol.OleTransactions || transactionProtocol == TransactionProtocol.WSAtomicTransactionOctober2004 || transactionProtocol == TransactionProtocol.WSAtomicTransaction11;
		}
	}
}

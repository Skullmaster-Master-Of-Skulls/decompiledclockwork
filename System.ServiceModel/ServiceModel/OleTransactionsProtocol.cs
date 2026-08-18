using System;

namespace System.ServiceModel
{
	// Token: 0x02000179 RID: 377
	internal class OleTransactionsProtocol : TransactionProtocol
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00028C9A File Offset: 0x00026E9A
		internal static TransactionProtocol Instance
		{
			get
			{
				return OleTransactionsProtocol.instance;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00028CA1 File Offset: 0x00026EA1
		internal override string Name
		{
			get
			{
				return "OleTransactions";
			}
		}

		// Token: 0x04000BF2 RID: 3058
		private static TransactionProtocol instance = new OleTransactionsProtocol();
	}
}

using System;

namespace System.ServiceModel
{
	// Token: 0x0200017A RID: 378
	internal class WSAtomicTransactionOctober2004Protocol : TransactionProtocol
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00028CBC File Offset: 0x00026EBC
		internal static TransactionProtocol Instance
		{
			get
			{
				return WSAtomicTransactionOctober2004Protocol.instance;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00028CC3 File Offset: 0x00026EC3
		internal override string Name
		{
			get
			{
				return "WSAtomicTransactionOctober2004";
			}
		}

		// Token: 0x04000BF3 RID: 3059
		private static TransactionProtocol instance = new WSAtomicTransactionOctober2004Protocol();
	}
}

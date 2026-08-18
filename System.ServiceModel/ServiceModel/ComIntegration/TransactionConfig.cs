using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000264 RID: 612
	[ComVisible(false)]
	[Serializable]
	internal enum TransactionConfig
	{
		// Token: 0x0400199E RID: 6558
		NoTransaction,
		// Token: 0x0400199F RID: 6559
		IfContainerIsTransactional,
		// Token: 0x040019A0 RID: 6560
		CreateTransactionIfNecessary,
		// Token: 0x040019A1 RID: 6561
		NewTransaction
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200005D RID: 93
	internal struct StoreTransactionOperation
	{
		// Token: 0x0400019A RID: 410
		[MarshalAs(UnmanagedType.U4)]
		public StoreTransactionOperationType Operation;

		// Token: 0x0400019B RID: 411
		public StoreTransactionData Data;
	}
}

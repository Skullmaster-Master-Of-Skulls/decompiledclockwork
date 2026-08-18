using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200026A RID: 618
	[Guid("59f4c2a3-d3d7-4a31-b6e4-6ab3177c50b9")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IServiceTransactionConfig
	{
		// Token: 0x060011A6 RID: 4518
		void ConfigureTransaction(TransactionConfig transactionConfig);

		// Token: 0x060011A7 RID: 4519
		void IsolationLevel(int option);

		// Token: 0x060011A8 RID: 4520
		void TransactionTimeout(uint ulTimeoutSec);

		// Token: 0x060011A9 RID: 4521
		void BringYourOwnTransaction([MarshalAs(UnmanagedType.LPWStr)] string szTipURL);

		// Token: 0x060011AA RID: 4522
		void NewTransactionDescription([MarshalAs(UnmanagedType.LPWStr)] string szTxDesc);

		// Token: 0x060011AB RID: 4523
		void ConfigureBYOT(IntPtr pITxByot);
	}
}

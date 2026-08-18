using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000268 RID: 616
	[Guid("33CAF1A1-FCB8-472b-B45E-967448DED6D8")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IServiceSysTxnConfig
	{
		// Token: 0x0600119C RID: 4508
		void ConfigureTransaction(TransactionConfig transactionConfig);

		// Token: 0x0600119D RID: 4509
		void IsolationLevel(int option);

		// Token: 0x0600119E RID: 4510
		void TransactionTimeout(uint ulTimeoutSec);

		// Token: 0x0600119F RID: 4511
		void BringYourOwnTransaction([MarshalAs(UnmanagedType.LPWStr)] string szTipURL);

		// Token: 0x060011A0 RID: 4512
		void NewTransactionDescription([MarshalAs(UnmanagedType.LPWStr)] string szTxDesc);

		// Token: 0x060011A1 RID: 4513
		void ConfigureBYOT(IntPtr pITxByot);

		// Token: 0x060011A2 RID: 4514
		void ConfigureBYOTSysTxn(IntPtr pITxByot);
	}
}

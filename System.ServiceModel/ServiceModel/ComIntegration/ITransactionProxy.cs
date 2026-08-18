using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Transactions;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200022F RID: 559
	[SuppressUnmanagedCodeSecurity]
	[Guid("02558374-DF2E-4dae-BD6B-1D5C994F9BDC")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ITransactionProxy
	{
		// Token: 0x060010C0 RID: 4288
		void Commit(Guid guid);

		// Token: 0x060010C1 RID: 4289
		void Abort();

		// Token: 0x060010C2 RID: 4290
		[return: MarshalAs(UnmanagedType.Interface)]
		IDtcTransaction Promote();

		// Token: 0x060010C3 RID: 4291
		void CreateVoter([MarshalAs(UnmanagedType.Interface)] ITransactionVoterNotifyAsync2 voterNotification, IntPtr voterBallot);

		// Token: 0x060010C4 RID: 4292
		DtcIsolationLevel GetIsolationLevel();

		// Token: 0x060010C5 RID: 4293
		Guid GetIdentifier();

		// Token: 0x060010C6 RID: 4294
		bool IsReusable();
	}
}

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000231 RID: 561
	[SuppressUnmanagedCodeSecurity]
	[Guid("3A6AD9E2-23B9-11cf-AD60-00AA00A74CCD")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ITransactionOutcomeEvents
	{
		// Token: 0x060010C8 RID: 4296
		void Committed([MarshalAs(UnmanagedType.Bool)] bool retaining, int newUow, int hr);

		// Token: 0x060010C9 RID: 4297
		void Aborted(int reason, [MarshalAs(UnmanagedType.Bool)] bool retaining, int newUow, int hr);

		// Token: 0x060010CA RID: 4298
		void HeuristicDecision(int decision, int reason, int hr);

		// Token: 0x060010CB RID: 4299
		void InDoubt();
	}
}

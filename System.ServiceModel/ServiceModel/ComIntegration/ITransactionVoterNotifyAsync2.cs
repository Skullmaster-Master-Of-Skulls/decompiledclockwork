using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000232 RID: 562
	[SuppressUnmanagedCodeSecurity]
	[Guid("5433376B-414D-11d3-B206-00C04FC2F3EF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ITransactionVoterNotifyAsync2
	{
		// Token: 0x060010CC RID: 4300
		void Committed([MarshalAs(UnmanagedType.Bool)] bool retaining, int newUow, int hr);

		// Token: 0x060010CD RID: 4301
		void Aborted(int reason, [MarshalAs(UnmanagedType.Bool)] bool retaining, int newUow, int hr);

		// Token: 0x060010CE RID: 4302
		void HeuristicDecision(int decision, int reason, int hr);

		// Token: 0x060010CF RID: 4303
		void InDoubt();

		// Token: 0x060010D0 RID: 4304
		void VoteRequest();
	}
}

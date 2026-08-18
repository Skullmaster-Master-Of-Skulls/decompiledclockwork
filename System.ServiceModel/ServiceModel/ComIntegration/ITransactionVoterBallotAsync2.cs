using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000230 RID: 560
	[SuppressUnmanagedCodeSecurity]
	[Guid("5433376C-414D-11d3-B206-00C04FC2F3EF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ITransactionVoterBallotAsync2
	{
		// Token: 0x060010C7 RID: 4295
		void VoteRequestDone(int hr, int reason);
	}
}

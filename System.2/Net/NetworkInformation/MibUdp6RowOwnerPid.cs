using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002D3 RID: 723
	internal struct MibUdp6RowOwnerPid
	{
		// Token: 0x04001A32 RID: 6706
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] localAddr;

		// Token: 0x04001A33 RID: 6707
		internal uint localScopeId;

		// Token: 0x04001A34 RID: 6708
		internal byte localPort1;

		// Token: 0x04001A35 RID: 6709
		internal byte localPort2;

		// Token: 0x04001A36 RID: 6710
		internal byte ignoreLocalPort3;

		// Token: 0x04001A37 RID: 6711
		internal byte ignoreLocalPort4;

		// Token: 0x04001A38 RID: 6712
		internal uint owningPid;
	}
}

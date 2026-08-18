using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002CD RID: 717
	internal struct MibTcp6RowOwnerPid
	{
		// Token: 0x04001A0F RID: 6671
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] localAddr;

		// Token: 0x04001A10 RID: 6672
		internal uint localScopeId;

		// Token: 0x04001A11 RID: 6673
		internal byte localPort1;

		// Token: 0x04001A12 RID: 6674
		internal byte localPort2;

		// Token: 0x04001A13 RID: 6675
		internal byte ignoreLocalPort3;

		// Token: 0x04001A14 RID: 6676
		internal byte ignoreLocalPort4;

		// Token: 0x04001A15 RID: 6677
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] remoteAddr;

		// Token: 0x04001A16 RID: 6678
		internal uint remoteScopeId;

		// Token: 0x04001A17 RID: 6679
		internal byte remotePort1;

		// Token: 0x04001A18 RID: 6680
		internal byte remotePort2;

		// Token: 0x04001A19 RID: 6681
		internal byte ignoreRemotePort3;

		// Token: 0x04001A1A RID: 6682
		internal byte ignoreRemotePort4;

		// Token: 0x04001A1B RID: 6683
		internal TcpState state;

		// Token: 0x04001A1C RID: 6684
		internal uint owningPid;
	}
}

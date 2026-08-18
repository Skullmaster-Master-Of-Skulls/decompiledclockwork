using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002CB RID: 715
	internal struct MibTcpRow
	{
		// Token: 0x04001A03 RID: 6659
		internal TcpState state;

		// Token: 0x04001A04 RID: 6660
		internal uint localAddr;

		// Token: 0x04001A05 RID: 6661
		internal byte localPort1;

		// Token: 0x04001A06 RID: 6662
		internal byte localPort2;

		// Token: 0x04001A07 RID: 6663
		internal byte ignoreLocalPort3;

		// Token: 0x04001A08 RID: 6664
		internal byte ignoreLocalPort4;

		// Token: 0x04001A09 RID: 6665
		internal uint remoteAddr;

		// Token: 0x04001A0A RID: 6666
		internal byte remotePort1;

		// Token: 0x04001A0B RID: 6667
		internal byte remotePort2;

		// Token: 0x04001A0C RID: 6668
		internal byte ignoreRemotePort3;

		// Token: 0x04001A0D RID: 6669
		internal byte ignoreRemotePort4;
	}
}

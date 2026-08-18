using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000607 RID: 1543
	internal struct MibTcpRow
	{
		// Token: 0x04002DA5 RID: 11685
		internal TcpState state;

		// Token: 0x04002DA6 RID: 11686
		internal uint localAddr;

		// Token: 0x04002DA7 RID: 11687
		internal byte localPort1;

		// Token: 0x04002DA8 RID: 11688
		internal byte localPort2;

		// Token: 0x04002DA9 RID: 11689
		internal byte localPort3;

		// Token: 0x04002DAA RID: 11690
		internal byte localPort4;

		// Token: 0x04002DAB RID: 11691
		internal uint remoteAddr;

		// Token: 0x04002DAC RID: 11692
		internal byte remotePort1;

		// Token: 0x04002DAD RID: 11693
		internal byte remotePort2;

		// Token: 0x04002DAE RID: 11694
		internal byte remotePort3;

		// Token: 0x04002DAF RID: 11695
		internal byte remotePort4;
	}
}

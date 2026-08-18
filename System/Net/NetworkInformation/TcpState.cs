using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000640 RID: 1600
	public enum TcpState
	{
		// Token: 0x04002E92 RID: 11922
		Unknown,
		// Token: 0x04002E93 RID: 11923
		Closed,
		// Token: 0x04002E94 RID: 11924
		Listen,
		// Token: 0x04002E95 RID: 11925
		SynSent,
		// Token: 0x04002E96 RID: 11926
		SynReceived,
		// Token: 0x04002E97 RID: 11927
		Established,
		// Token: 0x04002E98 RID: 11928
		FinWait1,
		// Token: 0x04002E99 RID: 11929
		FinWait2,
		// Token: 0x04002E9A RID: 11930
		CloseWait,
		// Token: 0x04002E9B RID: 11931
		Closing,
		// Token: 0x04002E9C RID: 11932
		LastAck,
		// Token: 0x04002E9D RID: 11933
		TimeWait,
		// Token: 0x04002E9E RID: 11934
		DeleteTcb
	}
}

using System;

namespace System.Net.Sockets
{
	// Token: 0x02000379 RID: 889
	public enum SocketAsyncOperation
	{
		// Token: 0x04001E6A RID: 7786
		None,
		// Token: 0x04001E6B RID: 7787
		Accept,
		// Token: 0x04001E6C RID: 7788
		Connect,
		// Token: 0x04001E6D RID: 7789
		Disconnect,
		// Token: 0x04001E6E RID: 7790
		Receive,
		// Token: 0x04001E6F RID: 7791
		ReceiveFrom,
		// Token: 0x04001E70 RID: 7792
		ReceiveMessageFrom,
		// Token: 0x04001E71 RID: 7793
		Send,
		// Token: 0x04001E72 RID: 7794
		SendPackets,
		// Token: 0x04001E73 RID: 7795
		SendTo
	}
}

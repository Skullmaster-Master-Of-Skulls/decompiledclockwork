using System;

namespace System.Net.Sockets
{
	// Token: 0x020005BF RID: 1471
	public enum SocketAsyncOperation
	{
		// Token: 0x04002B5B RID: 11099
		None,
		// Token: 0x04002B5C RID: 11100
		Accept,
		// Token: 0x04002B5D RID: 11101
		Connect,
		// Token: 0x04002B5E RID: 11102
		Disconnect,
		// Token: 0x04002B5F RID: 11103
		Receive,
		// Token: 0x04002B60 RID: 11104
		ReceiveFrom,
		// Token: 0x04002B61 RID: 11105
		ReceiveMessageFrom,
		// Token: 0x04002B62 RID: 11106
		Send,
		// Token: 0x04002B63 RID: 11107
		SendPackets,
		// Token: 0x04002B64 RID: 11108
		SendTo
	}
}

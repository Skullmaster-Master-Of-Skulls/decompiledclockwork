using System;

namespace System.Net.Sockets
{
	// Token: 0x020005C8 RID: 1480
	public enum SocketType
	{
		// Token: 0x04002C27 RID: 11303
		Stream = 1,
		// Token: 0x04002C28 RID: 11304
		Dgram,
		// Token: 0x04002C29 RID: 11305
		Raw,
		// Token: 0x04002C2A RID: 11306
		Rdm,
		// Token: 0x04002C2B RID: 11307
		Seqpacket,
		// Token: 0x04002C2C RID: 11308
		Unknown = -1
	}
}

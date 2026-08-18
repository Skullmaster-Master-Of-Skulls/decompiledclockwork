using System;

namespace System.Net.Sockets
{
	// Token: 0x02000385 RID: 901
	public enum SocketType
	{
		// Token: 0x04001F3F RID: 7999
		Stream = 1,
		// Token: 0x04001F40 RID: 8000
		Dgram,
		// Token: 0x04001F41 RID: 8001
		Raw,
		// Token: 0x04001F42 RID: 8002
		Rdm,
		// Token: 0x04001F43 RID: 8003
		Seqpacket,
		// Token: 0x04001F44 RID: 8004
		Unknown = -1
	}
}

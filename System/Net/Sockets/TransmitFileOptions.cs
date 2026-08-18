using System;

namespace System.Net.Sockets
{
	// Token: 0x020005CB RID: 1483
	[Flags]
	public enum TransmitFileOptions
	{
		// Token: 0x04002C37 RID: 11319
		UseDefaultWorkerThread = 0,
		// Token: 0x04002C38 RID: 11320
		Disconnect = 1,
		// Token: 0x04002C39 RID: 11321
		ReuseSocket = 2,
		// Token: 0x04002C3A RID: 11322
		WriteBehind = 4,
		// Token: 0x04002C3B RID: 11323
		UseSystemThread = 16,
		// Token: 0x04002C3C RID: 11324
		UseKernelApc = 32
	}
}

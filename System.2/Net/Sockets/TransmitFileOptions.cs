using System;

namespace System.Net.Sockets
{
	// Token: 0x02000388 RID: 904
	[Flags]
	public enum TransmitFileOptions
	{
		// Token: 0x04001F4F RID: 8015
		UseDefaultWorkerThread = 0,
		// Token: 0x04001F50 RID: 8016
		Disconnect = 1,
		// Token: 0x04001F51 RID: 8017
		ReuseSocket = 2,
		// Token: 0x04001F52 RID: 8018
		WriteBehind = 4,
		// Token: 0x04001F53 RID: 8019
		UseSystemThread = 16,
		// Token: 0x04001F54 RID: 8020
		UseKernelApc = 32
	}
}

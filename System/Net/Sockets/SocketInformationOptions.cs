using System;

namespace System.Net.Sockets
{
	// Token: 0x020005B1 RID: 1457
	[Flags]
	public enum SocketInformationOptions
	{
		// Token: 0x04002B1B RID: 11035
		NonBlocking = 1,
		// Token: 0x04002B1C RID: 11036
		Connected = 2,
		// Token: 0x04002B1D RID: 11037
		Listening = 4,
		// Token: 0x04002B1E RID: 11038
		UseOnlyOverlappedIO = 8
	}
}

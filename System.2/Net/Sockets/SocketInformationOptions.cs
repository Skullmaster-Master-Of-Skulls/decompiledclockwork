using System;

namespace System.Net.Sockets
{
	// Token: 0x02000374 RID: 884
	[Flags]
	public enum SocketInformationOptions
	{
		// Token: 0x04001E34 RID: 7732
		NonBlocking = 1,
		// Token: 0x04001E35 RID: 7733
		Connected = 2,
		// Token: 0x04001E36 RID: 7734
		Listening = 4,
		// Token: 0x04001E37 RID: 7735
		UseOnlyOverlappedIO = 8
	}
}

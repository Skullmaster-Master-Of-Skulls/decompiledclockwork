using System;

namespace System.Net.Sockets
{
	// Token: 0x0200037E RID: 894
	[Flags]
	public enum SocketFlags
	{
		// Token: 0x04001EF5 RID: 7925
		None = 0,
		// Token: 0x04001EF6 RID: 7926
		OutOfBand = 1,
		// Token: 0x04001EF7 RID: 7927
		Peek = 2,
		// Token: 0x04001EF8 RID: 7928
		DontRoute = 4,
		// Token: 0x04001EF9 RID: 7929
		MaxIOVectorLength = 16,
		// Token: 0x04001EFA RID: 7930
		Truncated = 256,
		// Token: 0x04001EFB RID: 7931
		ControlDataTruncated = 512,
		// Token: 0x04001EFC RID: 7932
		Broadcast = 1024,
		// Token: 0x04001EFD RID: 7933
		Multicast = 2048,
		// Token: 0x04001EFE RID: 7934
		Partial = 32768
	}
}

using System;

namespace System.Net.Sockets
{
	// Token: 0x020005A6 RID: 1446
	[Flags]
	internal enum AsyncEventBits
	{
		// Token: 0x04002A96 RID: 10902
		FdNone = 0,
		// Token: 0x04002A97 RID: 10903
		FdRead = 1,
		// Token: 0x04002A98 RID: 10904
		FdWrite = 2,
		// Token: 0x04002A99 RID: 10905
		FdOob = 4,
		// Token: 0x04002A9A RID: 10906
		FdAccept = 8,
		// Token: 0x04002A9B RID: 10907
		FdConnect = 16,
		// Token: 0x04002A9C RID: 10908
		FdClose = 32,
		// Token: 0x04002A9D RID: 10909
		FdQos = 64,
		// Token: 0x04002A9E RID: 10910
		FdGroupQos = 128,
		// Token: 0x04002A9F RID: 10911
		FdRoutingInterfaceChange = 256,
		// Token: 0x04002AA0 RID: 10912
		FdAddressListChange = 512,
		// Token: 0x04002AA1 RID: 10913
		FdAllEvents = 1023
	}
}

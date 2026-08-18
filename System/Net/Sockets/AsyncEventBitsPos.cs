using System;

namespace System.Net.Sockets
{
	// Token: 0x020005A7 RID: 1447
	internal enum AsyncEventBitsPos
	{
		// Token: 0x04002AA3 RID: 10915
		FdReadBit,
		// Token: 0x04002AA4 RID: 10916
		FdWriteBit,
		// Token: 0x04002AA5 RID: 10917
		FdOobBit,
		// Token: 0x04002AA6 RID: 10918
		FdAcceptBit,
		// Token: 0x04002AA7 RID: 10919
		FdConnectBit,
		// Token: 0x04002AA8 RID: 10920
		FdCloseBit,
		// Token: 0x04002AA9 RID: 10921
		FdQosBit,
		// Token: 0x04002AAA RID: 10922
		FdGroupQosBit,
		// Token: 0x04002AAB RID: 10923
		FdRoutingInterfaceChangeBit,
		// Token: 0x04002AAC RID: 10924
		FdAddressListChangeBit,
		// Token: 0x04002AAD RID: 10925
		FdMaxEvents
	}
}

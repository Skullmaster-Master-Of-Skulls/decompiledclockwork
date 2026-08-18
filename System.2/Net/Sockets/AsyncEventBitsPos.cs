using System;

namespace System.Net.Sockets
{
	// Token: 0x02000368 RID: 872
	internal enum AsyncEventBitsPos
	{
		// Token: 0x04001DAF RID: 7599
		FdReadBit,
		// Token: 0x04001DB0 RID: 7600
		FdWriteBit,
		// Token: 0x04001DB1 RID: 7601
		FdOobBit,
		// Token: 0x04001DB2 RID: 7602
		FdAcceptBit,
		// Token: 0x04001DB3 RID: 7603
		FdConnectBit,
		// Token: 0x04001DB4 RID: 7604
		FdCloseBit,
		// Token: 0x04001DB5 RID: 7605
		FdQosBit,
		// Token: 0x04001DB6 RID: 7606
		FdGroupQosBit,
		// Token: 0x04001DB7 RID: 7607
		FdRoutingInterfaceChangeBit,
		// Token: 0x04001DB8 RID: 7608
		FdAddressListChangeBit,
		// Token: 0x04001DB9 RID: 7609
		FdMaxEvents
	}
}

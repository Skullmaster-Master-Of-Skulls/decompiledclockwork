using System;

namespace System.Net.Sockets
{
	// Token: 0x02000367 RID: 871
	[Flags]
	internal enum AsyncEventBits
	{
		// Token: 0x04001DA2 RID: 7586
		FdNone = 0,
		// Token: 0x04001DA3 RID: 7587
		FdRead = 1,
		// Token: 0x04001DA4 RID: 7588
		FdWrite = 2,
		// Token: 0x04001DA5 RID: 7589
		FdOob = 4,
		// Token: 0x04001DA6 RID: 7590
		FdAccept = 8,
		// Token: 0x04001DA7 RID: 7591
		FdConnect = 16,
		// Token: 0x04001DA8 RID: 7592
		FdClose = 32,
		// Token: 0x04001DA9 RID: 7593
		FdQos = 64,
		// Token: 0x04001DAA RID: 7594
		FdGroupQos = 128,
		// Token: 0x04001DAB RID: 7595
		FdRoutingInterfaceChange = 256,
		// Token: 0x04001DAC RID: 7596
		FdAddressListChange = 512,
		// Token: 0x04001DAD RID: 7597
		FdAllEvents = 1023
	}
}

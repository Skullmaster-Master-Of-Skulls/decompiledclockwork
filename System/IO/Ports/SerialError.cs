using System;

namespace System.IO.Ports
{
	// Token: 0x020007AC RID: 1964
	public enum SerialError
	{
		// Token: 0x04003532 RID: 13618
		TXFull = 256,
		// Token: 0x04003533 RID: 13619
		RXOver = 1,
		// Token: 0x04003534 RID: 13620
		Overrun,
		// Token: 0x04003535 RID: 13621
		RXParity = 4,
		// Token: 0x04003536 RID: 13622
		Frame = 8
	}
}

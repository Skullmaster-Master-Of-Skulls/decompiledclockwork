using System;

namespace System.IO.Ports
{
	// Token: 0x0200040B RID: 1035
	public enum SerialError
	{
		// Token: 0x04002103 RID: 8451
		TXFull = 256,
		// Token: 0x04002104 RID: 8452
		RXOver = 1,
		// Token: 0x04002105 RID: 8453
		Overrun,
		// Token: 0x04002106 RID: 8454
		RXParity = 4,
		// Token: 0x04002107 RID: 8455
		Frame = 8
	}
}

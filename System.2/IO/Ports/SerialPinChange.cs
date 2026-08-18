using System;

namespace System.IO.Ports
{
	// Token: 0x0200040E RID: 1038
	public enum SerialPinChange
	{
		// Token: 0x0400210A RID: 8458
		CtsChanged = 8,
		// Token: 0x0400210B RID: 8459
		DsrChanged = 16,
		// Token: 0x0400210C RID: 8460
		CDChanged = 32,
		// Token: 0x0400210D RID: 8461
		Ring = 256,
		// Token: 0x0400210E RID: 8462
		Break = 64
	}
}

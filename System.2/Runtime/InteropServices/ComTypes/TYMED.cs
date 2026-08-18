using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020003E7 RID: 999
	[Flags]
	[__DynamicallyInvokable]
	public enum TYMED
	{
		// Token: 0x040020A6 RID: 8358
		[__DynamicallyInvokable]
		TYMED_HGLOBAL = 1,
		// Token: 0x040020A7 RID: 8359
		[__DynamicallyInvokable]
		TYMED_FILE = 2,
		// Token: 0x040020A8 RID: 8360
		[__DynamicallyInvokable]
		TYMED_ISTREAM = 4,
		// Token: 0x040020A9 RID: 8361
		[__DynamicallyInvokable]
		TYMED_ISTORAGE = 8,
		// Token: 0x040020AA RID: 8362
		[__DynamicallyInvokable]
		TYMED_GDI = 16,
		// Token: 0x040020AB RID: 8363
		[__DynamicallyInvokable]
		TYMED_MFPICT = 32,
		// Token: 0x040020AC RID: 8364
		[__DynamicallyInvokable]
		TYMED_ENHMF = 64,
		// Token: 0x040020AD RID: 8365
		[__DynamicallyInvokable]
		TYMED_NULL = 0
	}
}

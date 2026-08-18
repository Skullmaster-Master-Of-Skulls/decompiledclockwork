using System;

namespace Spire.CompoundFile.XLS.Native
{
	// Token: 0x02000650 RID: 1616
	[Flags]
	internal enum TYMED
	{
		// Token: 0x04002FC5 RID: 12229
		TYMED_NULL = 0,
		// Token: 0x04002FC6 RID: 12230
		TYMED_HGLOBAL = 1,
		// Token: 0x04002FC7 RID: 12231
		TYMED_FILE = 2,
		// Token: 0x04002FC8 RID: 12232
		TYMED_ISTREAM = 4,
		// Token: 0x04002FC9 RID: 12233
		TYMED_ISTORAGE = 8,
		// Token: 0x04002FCA RID: 12234
		TYMED_GDI = 16,
		// Token: 0x04002FCB RID: 12235
		TYMED_MFPICT = 32,
		// Token: 0x04002FCC RID: 12236
		TYMED_ENHMF = 64
	}
}

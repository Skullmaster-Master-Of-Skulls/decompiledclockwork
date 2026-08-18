using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200010E RID: 270
	[Flags]
	internal enum TypeDefTreatment : byte
	{
		// Token: 0x040007F0 RID: 2032
		None = 0,
		// Token: 0x040007F1 RID: 2033
		KindMask = 15,
		// Token: 0x040007F2 RID: 2034
		NormalNonAttribute = 1,
		// Token: 0x040007F3 RID: 2035
		NormalAttribute = 2,
		// Token: 0x040007F4 RID: 2036
		UnmangleWinRTName = 3,
		// Token: 0x040007F5 RID: 2037
		PrefixWinRTName = 4,
		// Token: 0x040007F6 RID: 2038
		RedirectedToClrType = 5,
		// Token: 0x040007F7 RID: 2039
		RedirectedToClrAttribute = 6,
		// Token: 0x040007F8 RID: 2040
		MarkAbstractFlag = 16,
		// Token: 0x040007F9 RID: 2041
		MarkInternalFlag = 32
	}
}

using System;

namespace System.Xml.Schema
{
	// Token: 0x020001A9 RID: 425
	[Flags]
	internal enum RestrictionFlags
	{
		// Token: 0x04000CF6 RID: 3318
		Length = 1,
		// Token: 0x04000CF7 RID: 3319
		MinLength = 2,
		// Token: 0x04000CF8 RID: 3320
		MaxLength = 4,
		// Token: 0x04000CF9 RID: 3321
		Pattern = 8,
		// Token: 0x04000CFA RID: 3322
		Enumeration = 16,
		// Token: 0x04000CFB RID: 3323
		WhiteSpace = 32,
		// Token: 0x04000CFC RID: 3324
		MaxInclusive = 64,
		// Token: 0x04000CFD RID: 3325
		MaxExclusive = 128,
		// Token: 0x04000CFE RID: 3326
		MinInclusive = 256,
		// Token: 0x04000CFF RID: 3327
		MinExclusive = 512,
		// Token: 0x04000D00 RID: 3328
		TotalDigits = 1024,
		// Token: 0x04000D01 RID: 3329
		FractionDigits = 2048
	}
}

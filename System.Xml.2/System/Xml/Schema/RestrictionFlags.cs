using System;

namespace System.Xml.Schema
{
	// Token: 0x02000201 RID: 513
	[Flags]
	internal enum RestrictionFlags
	{
		// Token: 0x04000DEB RID: 3563
		Length = 1,
		// Token: 0x04000DEC RID: 3564
		MinLength = 2,
		// Token: 0x04000DED RID: 3565
		MaxLength = 4,
		// Token: 0x04000DEE RID: 3566
		Pattern = 8,
		// Token: 0x04000DEF RID: 3567
		Enumeration = 16,
		// Token: 0x04000DF0 RID: 3568
		WhiteSpace = 32,
		// Token: 0x04000DF1 RID: 3569
		MaxInclusive = 64,
		// Token: 0x04000DF2 RID: 3570
		MaxExclusive = 128,
		// Token: 0x04000DF3 RID: 3571
		MinInclusive = 256,
		// Token: 0x04000DF4 RID: 3572
		MinExclusive = 512,
		// Token: 0x04000DF5 RID: 3573
		TotalDigits = 1024,
		// Token: 0x04000DF6 RID: 3574
		FractionDigits = 2048
	}
}

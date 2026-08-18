using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000203 RID: 515
	internal class RestrictionFacets
	{
		// Token: 0x04000DFB RID: 3579
		internal int Length;

		// Token: 0x04000DFC RID: 3580
		internal int MinLength;

		// Token: 0x04000DFD RID: 3581
		internal int MaxLength;

		// Token: 0x04000DFE RID: 3582
		internal ArrayList Patterns;

		// Token: 0x04000DFF RID: 3583
		internal ArrayList Enumeration;

		// Token: 0x04000E00 RID: 3584
		internal XmlSchemaWhiteSpace WhiteSpace;

		// Token: 0x04000E01 RID: 3585
		internal object MaxInclusive;

		// Token: 0x04000E02 RID: 3586
		internal object MaxExclusive;

		// Token: 0x04000E03 RID: 3587
		internal object MinInclusive;

		// Token: 0x04000E04 RID: 3588
		internal object MinExclusive;

		// Token: 0x04000E05 RID: 3589
		internal int TotalDigits;

		// Token: 0x04000E06 RID: 3590
		internal int FractionDigits;

		// Token: 0x04000E07 RID: 3591
		internal RestrictionFlags Flags;

		// Token: 0x04000E08 RID: 3592
		internal RestrictionFlags FixedFlags;
	}
}

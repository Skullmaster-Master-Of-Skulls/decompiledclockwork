using System;

namespace Ionic.Zlib
{
	// Token: 0x02000017 RID: 23
	internal static class InternalConstants
	{
		// Token: 0x04000111 RID: 273
		internal static readonly int MAX_BITS = 15;

		// Token: 0x04000112 RID: 274
		internal static readonly int BL_CODES = 19;

		// Token: 0x04000113 RID: 275
		internal static readonly int D_CODES = 30;

		// Token: 0x04000114 RID: 276
		internal static readonly int LITERALS = 256;

		// Token: 0x04000115 RID: 277
		internal static readonly int LENGTH_CODES = 29;

		// Token: 0x04000116 RID: 278
		internal static readonly int L_CODES = InternalConstants.LITERALS + 1 + InternalConstants.LENGTH_CODES;

		// Token: 0x04000117 RID: 279
		internal static readonly int MAX_BL_BITS = 7;

		// Token: 0x04000118 RID: 280
		internal static readonly int REP_3_6 = 16;

		// Token: 0x04000119 RID: 281
		internal static readonly int REPZ_3_10 = 17;

		// Token: 0x0400011A RID: 282
		internal static readonly int REPZ_11_138 = 18;
	}
}

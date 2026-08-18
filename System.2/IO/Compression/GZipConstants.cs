using System;

namespace System.IO.Compression
{
	// Token: 0x0200042F RID: 1071
	internal static class GZipConstants
	{
		// Token: 0x040021E1 RID: 8673
		internal const int CompressionLevel_3 = 3;

		// Token: 0x040021E2 RID: 8674
		internal const int CompressionLevel_10 = 10;

		// Token: 0x040021E3 RID: 8675
		internal const long FileLengthModulo = 4294967296L;

		// Token: 0x040021E4 RID: 8676
		internal const byte ID1 = 31;

		// Token: 0x040021E5 RID: 8677
		internal const byte ID2 = 139;

		// Token: 0x040021E6 RID: 8678
		internal const byte Deflate = 8;

		// Token: 0x040021E7 RID: 8679
		internal const int Xfl_HeaderPos = 8;

		// Token: 0x040021E8 RID: 8680
		internal const byte Xfl_FastestAlgorithm = 4;

		// Token: 0x040021E9 RID: 8681
		internal const byte Xfl_MaxCompressionSlowestAlgorithm = 2;
	}
}

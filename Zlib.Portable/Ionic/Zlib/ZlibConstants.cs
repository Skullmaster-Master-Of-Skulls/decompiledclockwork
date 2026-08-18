using System;

namespace Ionic.Zlib
{
	// Token: 0x0200001D RID: 29
	public static class ZlibConstants
	{
		// Token: 0x0400014C RID: 332
		public const int WindowBitsMax = 15;

		// Token: 0x0400014D RID: 333
		public const int WindowBitsDefault = 15;

		// Token: 0x0400014E RID: 334
		public const int Z_OK = 0;

		// Token: 0x0400014F RID: 335
		public const int Z_STREAM_END = 1;

		// Token: 0x04000150 RID: 336
		public const int Z_NEED_DICT = 2;

		// Token: 0x04000151 RID: 337
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x04000152 RID: 338
		public const int Z_DATA_ERROR = -3;

		// Token: 0x04000153 RID: 339
		public const int Z_BUF_ERROR = -5;

		// Token: 0x04000154 RID: 340
		public const int WorkingBufferSizeDefault = 16384;

		// Token: 0x04000155 RID: 341
		public const int WorkingBufferSizeMin = 1024;
	}
}

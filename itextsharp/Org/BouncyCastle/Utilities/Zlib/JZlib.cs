using System;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x02000295 RID: 661
	public sealed class JZlib
	{
		// Token: 0x060018F4 RID: 6388 RVA: 0x00092DAA File Offset: 0x00091DAA
		public static string version()
		{
			return "1.0.2";
		}

		// Token: 0x040010CE RID: 4302
		private const string _version = "1.0.2";

		// Token: 0x040010CF RID: 4303
		public const int Z_NO_COMPRESSION = 0;

		// Token: 0x040010D0 RID: 4304
		public const int Z_BEST_SPEED = 1;

		// Token: 0x040010D1 RID: 4305
		public const int Z_BEST_COMPRESSION = 9;

		// Token: 0x040010D2 RID: 4306
		public const int Z_DEFAULT_COMPRESSION = -1;

		// Token: 0x040010D3 RID: 4307
		public const int Z_FILTERED = 1;

		// Token: 0x040010D4 RID: 4308
		public const int Z_HUFFMAN_ONLY = 2;

		// Token: 0x040010D5 RID: 4309
		public const int Z_DEFAULT_STRATEGY = 0;

		// Token: 0x040010D6 RID: 4310
		public const int Z_NO_FLUSH = 0;

		// Token: 0x040010D7 RID: 4311
		public const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x040010D8 RID: 4312
		public const int Z_SYNC_FLUSH = 2;

		// Token: 0x040010D9 RID: 4313
		public const int Z_FULL_FLUSH = 3;

		// Token: 0x040010DA RID: 4314
		public const int Z_FINISH = 4;

		// Token: 0x040010DB RID: 4315
		public const int Z_OK = 0;

		// Token: 0x040010DC RID: 4316
		public const int Z_STREAM_END = 1;

		// Token: 0x040010DD RID: 4317
		public const int Z_NEED_DICT = 2;

		// Token: 0x040010DE RID: 4318
		public const int Z_ERRNO = -1;

		// Token: 0x040010DF RID: 4319
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x040010E0 RID: 4320
		public const int Z_DATA_ERROR = -3;

		// Token: 0x040010E1 RID: 4321
		public const int Z_MEM_ERROR = -4;

		// Token: 0x040010E2 RID: 4322
		public const int Z_BUF_ERROR = -5;

		// Token: 0x040010E3 RID: 4323
		public const int Z_VERSION_ERROR = -6;
	}
}

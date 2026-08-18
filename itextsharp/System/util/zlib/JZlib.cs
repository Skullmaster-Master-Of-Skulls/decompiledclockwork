using System;

namespace System.util.zlib
{
	// Token: 0x020002DF RID: 735
	public sealed class JZlib
	{
		// Token: 0x06001B4F RID: 6991 RVA: 0x000A4C6B File Offset: 0x000A3C6B
		public static string version()
		{
			return "1.0.2";
		}

		// Token: 0x040012D9 RID: 4825
		private const string version_ = "1.0.2";

		// Token: 0x040012DA RID: 4826
		public const int Z_NO_COMPRESSION = 0;

		// Token: 0x040012DB RID: 4827
		public const int Z_BEST_SPEED = 1;

		// Token: 0x040012DC RID: 4828
		public const int Z_BEST_COMPRESSION = 9;

		// Token: 0x040012DD RID: 4829
		public const int Z_DEFAULT_COMPRESSION = -1;

		// Token: 0x040012DE RID: 4830
		public const int Z_FILTERED = 1;

		// Token: 0x040012DF RID: 4831
		public const int Z_HUFFMAN_ONLY = 2;

		// Token: 0x040012E0 RID: 4832
		public const int Z_DEFAULT_STRATEGY = 0;

		// Token: 0x040012E1 RID: 4833
		public const int Z_NO_FLUSH = 0;

		// Token: 0x040012E2 RID: 4834
		public const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x040012E3 RID: 4835
		public const int Z_SYNC_FLUSH = 2;

		// Token: 0x040012E4 RID: 4836
		public const int Z_FULL_FLUSH = 3;

		// Token: 0x040012E5 RID: 4837
		public const int Z_FINISH = 4;

		// Token: 0x040012E6 RID: 4838
		public const int Z_OK = 0;

		// Token: 0x040012E7 RID: 4839
		public const int Z_STREAM_END = 1;

		// Token: 0x040012E8 RID: 4840
		public const int Z_NEED_DICT = 2;

		// Token: 0x040012E9 RID: 4841
		public const int Z_ERRNO = -1;

		// Token: 0x040012EA RID: 4842
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x040012EB RID: 4843
		public const int Z_DATA_ERROR = -3;

		// Token: 0x040012EC RID: 4844
		public const int Z_MEM_ERROR = -4;

		// Token: 0x040012ED RID: 4845
		public const int Z_BUF_ERROR = -5;

		// Token: 0x040012EE RID: 4846
		public const int Z_VERSION_ERROR = -6;
	}
}

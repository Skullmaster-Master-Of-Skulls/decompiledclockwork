using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x0200004D RID: 77
	public class DeflaterConstants
	{
		// Token: 0x0400022C RID: 556
		public const bool DEBUGGING = false;

		// Token: 0x0400022D RID: 557
		public const int STORED_BLOCK = 0;

		// Token: 0x0400022E RID: 558
		public const int STATIC_TREES = 1;

		// Token: 0x0400022F RID: 559
		public const int DYN_TREES = 2;

		// Token: 0x04000230 RID: 560
		public const int PRESET_DICT = 32;

		// Token: 0x04000231 RID: 561
		public const int DEFAULT_MEM_LEVEL = 8;

		// Token: 0x04000232 RID: 562
		public const int MAX_MATCH = 258;

		// Token: 0x04000233 RID: 563
		public const int MIN_MATCH = 3;

		// Token: 0x04000234 RID: 564
		public const int MAX_WBITS = 15;

		// Token: 0x04000235 RID: 565
		public const int WSIZE = 32768;

		// Token: 0x04000236 RID: 566
		public const int WMASK = 32767;

		// Token: 0x04000237 RID: 567
		public const int HASH_BITS = 15;

		// Token: 0x04000238 RID: 568
		public const int HASH_SIZE = 32768;

		// Token: 0x04000239 RID: 569
		public const int HASH_MASK = 32767;

		// Token: 0x0400023A RID: 570
		public const int HASH_SHIFT = 5;

		// Token: 0x0400023B RID: 571
		public const int MIN_LOOKAHEAD = 262;

		// Token: 0x0400023C RID: 572
		public const int MAX_DIST = 32506;

		// Token: 0x0400023D RID: 573
		public const int PENDING_BUF_SIZE = 65536;

		// Token: 0x0400023E RID: 574
		public const int DEFLATE_STORED = 0;

		// Token: 0x0400023F RID: 575
		public const int DEFLATE_FAST = 1;

		// Token: 0x04000240 RID: 576
		public const int DEFLATE_SLOW = 2;

		// Token: 0x04000241 RID: 577
		public static int MAX_BLOCK_SIZE = Math.Min(65535, 65531);

		// Token: 0x04000242 RID: 578
		public static int[] GOOD_LENGTH = new int[]
		{
			0,
			4,
			4,
			4,
			4,
			8,
			8,
			8,
			32,
			32
		};

		// Token: 0x04000243 RID: 579
		public static int[] MAX_LAZY = new int[]
		{
			0,
			4,
			5,
			6,
			4,
			16,
			16,
			32,
			128,
			258
		};

		// Token: 0x04000244 RID: 580
		public static int[] NICE_LENGTH = new int[]
		{
			0,
			8,
			16,
			32,
			16,
			32,
			128,
			128,
			258,
			258
		};

		// Token: 0x04000245 RID: 581
		public static int[] MAX_CHAIN = new int[]
		{
			0,
			4,
			8,
			32,
			16,
			32,
			128,
			256,
			1024,
			4096
		};

		// Token: 0x04000246 RID: 582
		public static int[] COMPR_FUNC = new int[]
		{
			0,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			2
		};
	}
}

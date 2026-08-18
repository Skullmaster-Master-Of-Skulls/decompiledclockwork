using System;

namespace Spire.Compression.Zip
{
	// Token: 0x02000226 RID: 550
	internal enum CompressionMethod
	{
		// Token: 0x040011AC RID: 4524
		Stored,
		// Token: 0x040011AD RID: 4525
		Shrunk,
		// Token: 0x040011AE RID: 4526
		ReducedFactor1,
		// Token: 0x040011AF RID: 4527
		ReducedFactor2,
		// Token: 0x040011B0 RID: 4528
		ReducedFactor3,
		// Token: 0x040011B1 RID: 4529
		ReducedFactor4,
		// Token: 0x040011B2 RID: 4530
		Imploded,
		// Token: 0x040011B3 RID: 4531
		Tokenizing,
		// Token: 0x040011B4 RID: 4532
		Deflated,
		// Token: 0x040011B5 RID: 4533
		Defalte64,
		// Token: 0x040011B6 RID: 4534
		PRWARE,
		// Token: 0x040011B7 RID: 4535
		BZIP2 = 12,
		// Token: 0x040011B8 RID: 4536
		LZMA = 14,
		// Token: 0x040011B9 RID: 4537
		IBMTerse = 18,
		// Token: 0x040011BA RID: 4538
		LZ77,
		// Token: 0x040011BB RID: 4539
		PPMd = 98
	}
}

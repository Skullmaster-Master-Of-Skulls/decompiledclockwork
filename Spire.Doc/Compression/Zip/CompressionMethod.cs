using System;

namespace Spire.Compression.Zip
{
	// Token: 0x0200012F RID: 303
	internal enum CompressionMethod
	{
		// Token: 0x0400111B RID: 4379
		Stored,
		// Token: 0x0400111C RID: 4380
		Shrunk,
		// Token: 0x0400111D RID: 4381
		ReducedFactor1,
		// Token: 0x0400111E RID: 4382
		ReducedFactor2,
		// Token: 0x0400111F RID: 4383
		ReducedFactor3,
		// Token: 0x04001120 RID: 4384
		ReducedFactor4,
		// Token: 0x04001121 RID: 4385
		Imploded,
		// Token: 0x04001122 RID: 4386
		Tokenizing,
		// Token: 0x04001123 RID: 4387
		Deflated,
		// Token: 0x04001124 RID: 4388
		Defalte64,
		// Token: 0x04001125 RID: 4389
		PRWARE,
		// Token: 0x04001126 RID: 4390
		BZIP2 = 12,
		// Token: 0x04001127 RID: 4391
		LZMA = 14,
		// Token: 0x04001128 RID: 4392
		IBMTerse = 18,
		// Token: 0x04001129 RID: 4393
		LZ77,
		// Token: 0x0400112A RID: 4394
		PPMd = 98
	}
}

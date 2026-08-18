using System;

namespace System.IO.Compression
{
	// Token: 0x02000433 RID: 1075
	internal enum InflaterState
	{
		// Token: 0x04002216 RID: 8726
		ReadingHeader,
		// Token: 0x04002217 RID: 8727
		ReadingBFinal = 2,
		// Token: 0x04002218 RID: 8728
		ReadingBType,
		// Token: 0x04002219 RID: 8729
		ReadingNumLitCodes,
		// Token: 0x0400221A RID: 8730
		ReadingNumDistCodes,
		// Token: 0x0400221B RID: 8731
		ReadingNumCodeLengthCodes,
		// Token: 0x0400221C RID: 8732
		ReadingCodeLengthCodes,
		// Token: 0x0400221D RID: 8733
		ReadingTreeCodesBefore,
		// Token: 0x0400221E RID: 8734
		ReadingTreeCodesAfter,
		// Token: 0x0400221F RID: 8735
		DecodeTop,
		// Token: 0x04002220 RID: 8736
		HaveInitialLength,
		// Token: 0x04002221 RID: 8737
		HaveFullLength,
		// Token: 0x04002222 RID: 8738
		HaveDistCode,
		// Token: 0x04002223 RID: 8739
		UncompressedAligning = 15,
		// Token: 0x04002224 RID: 8740
		UncompressedByte1,
		// Token: 0x04002225 RID: 8741
		UncompressedByte2,
		// Token: 0x04002226 RID: 8742
		UncompressedByte3,
		// Token: 0x04002227 RID: 8743
		UncompressedByte4,
		// Token: 0x04002228 RID: 8744
		DecodingUncompressed,
		// Token: 0x04002229 RID: 8745
		StartReadingFooter,
		// Token: 0x0400222A RID: 8746
		ReadingFooter,
		// Token: 0x0400222B RID: 8747
		VerifyingFooter,
		// Token: 0x0400222C RID: 8748
		Done
	}
}

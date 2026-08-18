using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007F6 RID: 2038
	internal enum ClientFramingDecoderState
	{
		// Token: 0x04002FE6 RID: 12262
		ReadingUpgradeRecord,
		// Token: 0x04002FE7 RID: 12263
		ReadingUpgradeMode,
		// Token: 0x04002FE8 RID: 12264
		UpgradeResponse,
		// Token: 0x04002FE9 RID: 12265
		ReadingAckRecord,
		// Token: 0x04002FEA RID: 12266
		Start,
		// Token: 0x04002FEB RID: 12267
		ReadingFault,
		// Token: 0x04002FEC RID: 12268
		ReadingFaultString,
		// Token: 0x04002FED RID: 12269
		Fault,
		// Token: 0x04002FEE RID: 12270
		ReadingEnvelopeRecord,
		// Token: 0x04002FEF RID: 12271
		ReadingEnvelopeSize,
		// Token: 0x04002FF0 RID: 12272
		EnvelopeStart,
		// Token: 0x04002FF1 RID: 12273
		ReadingEnvelopeBytes,
		// Token: 0x04002FF2 RID: 12274
		EnvelopeEnd,
		// Token: 0x04002FF3 RID: 12275
		ReadingEndRecord,
		// Token: 0x04002FF4 RID: 12276
		End
	}
}

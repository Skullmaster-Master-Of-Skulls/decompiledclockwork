using System;

namespace OracleInternal.BinXml
{
	// Token: 0x0200000E RID: 14
	[Flags]
	internal enum DecodeStates
	{
		// Token: 0x04000071 RID: 113
		None = 0,
		// Token: 0x04000072 RID: 114
		InstructionStart = 1,
		// Token: 0x04000073 RID: 115
		ElementStart = 2,
		// Token: 0x04000074 RID: 116
		ElementStartNoClosingBracket = 4,
		// Token: 0x04000075 RID: 117
		ElementStartTagBegin = 8,
		// Token: 0x04000076 RID: 118
		ElementStartTagEnd = 16,
		// Token: 0x04000077 RID: 119
		ElementStartAttribute = 32,
		// Token: 0x04000078 RID: 120
		ElementStartAttributeStart = 64,
		// Token: 0x04000079 RID: 121
		ElementStartAttributeDone = 128,
		// Token: 0x0400007A RID: 122
		ElementEndStart = 256,
		// Token: 0x0400007B RID: 123
		ElementEndPending = 512,
		// Token: 0x0400007C RID: 124
		ElementData = 1024,
		// Token: 0x0400007D RID: 125
		ElementDataStart = 2048,
		// Token: 0x0400007E RID: 126
		ElementDataStartElementOpt = 4096,
		// Token: 0x0400007F RID: 127
		ElementDataStartCharsOpt = 16384,
		// Token: 0x04000080 RID: 128
		ElementDataStartCharsLoc = 32768,
		// Token: 0x04000081 RID: 129
		ElementDataStartPartial = 65536,
		// Token: 0x04000082 RID: 130
		ElementDataDone = 131072,
		// Token: 0x04000083 RID: 131
		SectionStart = 262144,
		// Token: 0x04000084 RID: 132
		SectionEnd = 524288
	}
}

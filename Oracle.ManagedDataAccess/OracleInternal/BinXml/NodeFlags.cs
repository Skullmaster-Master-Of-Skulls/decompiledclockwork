using System;

namespace OracleInternal.BinXml
{
	// Token: 0x0200001F RID: 31
	[Flags]
	internal enum NodeFlags : short
	{
		// Token: 0x0400013D RID: 317
		SELFMODE = 0,
		// Token: 0x0400013E RID: 318
		ARRMODE = 1,
		// Token: 0x0400013F RID: 319
		SEQMODE = 2,
		// Token: 0x04000140 RID: 320
		SELFSTRMMODE = 4,
		// Token: 0x04000141 RID: 321
		STRMMODE = 4,
		// Token: 0x04000142 RID: 322
		OPTMODE = 8,
		// Token: 0x04000143 RID: 323
		KIDMODE = 16,
		// Token: 0x04000144 RID: 324
		ARRNEXTMODE = 32,
		// Token: 0x04000145 RID: 325
		TYPEID = 256,
		// Token: 0x04000146 RID: 326
		ARRSEQMODE = 3,
		// Token: 0x04000147 RID: 327
		OPTSEQMODE = 10,
		// Token: 0x04000148 RID: 328
		ARRNEXTSEQMODE = 34
	}
}

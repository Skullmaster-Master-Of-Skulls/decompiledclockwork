using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200035C RID: 860
	internal enum LiteralKind
	{
		// Token: 0x040015A5 RID: 5541
		Number,
		// Token: 0x040015A6 RID: 5542
		String,
		// Token: 0x040015A7 RID: 5543
		UnicodeString,
		// Token: 0x040015A8 RID: 5544
		Boolean,
		// Token: 0x040015A9 RID: 5545
		Binary,
		// Token: 0x040015AA RID: 5546
		DateTime,
		// Token: 0x040015AB RID: 5547
		Time,
		// Token: 0x040015AC RID: 5548
		DateTimeOffset,
		// Token: 0x040015AD RID: 5549
		Guid,
		// Token: 0x040015AE RID: 5550
		Null
	}
}

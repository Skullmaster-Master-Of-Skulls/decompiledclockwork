using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200022C RID: 556
	internal enum LiteralKind
	{
		// Token: 0x04000610 RID: 1552
		Number,
		// Token: 0x04000611 RID: 1553
		String,
		// Token: 0x04000612 RID: 1554
		UnicodeString,
		// Token: 0x04000613 RID: 1555
		Boolean,
		// Token: 0x04000614 RID: 1556
		Binary,
		// Token: 0x04000615 RID: 1557
		DateTime,
		// Token: 0x04000616 RID: 1558
		Time,
		// Token: 0x04000617 RID: 1559
		DateTimeOffset,
		// Token: 0x04000618 RID: 1560
		Guid,
		// Token: 0x04000619 RID: 1561
		Null
	}
}

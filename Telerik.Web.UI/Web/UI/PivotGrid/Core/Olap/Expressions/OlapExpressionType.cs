using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x02000700 RID: 1792
	internal enum OlapExpressionType
	{
		// Token: 0x040010E0 RID: 4320
		Constant,
		// Token: 0x040010E1 RID: 4321
		Identifier,
		// Token: 0x040010E2 RID: 4322
		Tuple,
		// Token: 0x040010E3 RID: 4323
		Set,
		// Token: 0x040010E4 RID: 4324
		Binary,
		// Token: 0x040010E5 RID: 4325
		SelectQueryAxisClause,
		// Token: 0x040010E6 RID: 4326
		SelectClause,
		// Token: 0x040010E7 RID: 4327
		MemberFunction,
		// Token: 0x040010E8 RID: 4328
		Function,
		// Token: 0x040010E9 RID: 4329
		Wrapper
	}
}

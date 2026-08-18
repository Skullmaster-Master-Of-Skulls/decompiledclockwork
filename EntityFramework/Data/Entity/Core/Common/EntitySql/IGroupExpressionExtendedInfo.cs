using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000255 RID: 597
	internal interface IGroupExpressionExtendedInfo
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060014D4 RID: 5332
		DbExpression GroupVarBasedExpression { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060014D5 RID: 5333
		DbExpression GroupAggBasedExpression { get; }
	}
}

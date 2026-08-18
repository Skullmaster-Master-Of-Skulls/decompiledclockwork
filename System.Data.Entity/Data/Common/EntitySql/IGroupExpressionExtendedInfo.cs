using System;
using System.Data.Common.CommandTrees;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000346 RID: 838
	internal interface IGroupExpressionExtendedInfo
	{
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06003169 RID: 12649
		DbExpression GroupVarBasedExpression { get; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600316A RID: 12650
		DbExpression GroupAggBasedExpression { get; }
	}
}

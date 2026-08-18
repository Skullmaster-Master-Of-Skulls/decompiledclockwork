using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200037E RID: 894
	internal sealed class GroupByClause : Node
	{
		// Token: 0x06003249 RID: 12873 RVA: 0x000C5088 File Offset: 0x000C3288
		internal GroupByClause(NodeList<AliasedExpr> groupItems)
		{
			this._groupItems = groupItems;
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x000C5097 File Offset: 0x000C3297
		internal NodeList<AliasedExpr> GroupItems
		{
			get
			{
				return this._groupItems;
			}
		}

		// Token: 0x04001636 RID: 5686
		private readonly NodeList<AliasedExpr> _groupItems;
	}
}

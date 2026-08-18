using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000225 RID: 549
	internal sealed class GroupByClause : Node
	{
		// Token: 0x06001386 RID: 4998 RVA: 0x00050743 File Offset: 0x0004E943
		internal GroupByClause(NodeList<AliasedExpr> groupItems)
		{
			this._groupItems = groupItems;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x00050752 File Offset: 0x0004E952
		internal NodeList<AliasedExpr> GroupItems
		{
			get
			{
				return this._groupItems;
			}
		}

		// Token: 0x040005F5 RID: 1525
		private readonly NodeList<AliasedExpr> _groupItems;
	}
}

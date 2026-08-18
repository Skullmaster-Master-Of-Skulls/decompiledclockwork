using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000220 RID: 544
	internal sealed class FromClause : Node
	{
		// Token: 0x06001378 RID: 4984 RVA: 0x00050671 File Offset: 0x0004E871
		internal FromClause(NodeList<FromClauseItem> fromClauseItems)
		{
			this._fromClauseItems = fromClauseItems;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x00050680 File Offset: 0x0004E880
		internal NodeList<FromClauseItem> FromClauseItems
		{
			get
			{
				return this._fromClauseItems;
			}
		}

		// Token: 0x040005E7 RID: 1511
		private readonly NodeList<FromClauseItem> _fromClauseItems;
	}
}

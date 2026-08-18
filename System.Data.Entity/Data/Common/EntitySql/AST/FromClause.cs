using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200037B RID: 891
	internal sealed class FromClause : Node
	{
		// Token: 0x06003242 RID: 12866 RVA: 0x000C501F File Offset: 0x000C321F
		internal FromClause(NodeList<FromClauseItem> fromClauseItems)
		{
			this._fromClauseItems = fromClauseItems;
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000C502E File Offset: 0x000C322E
		internal NodeList<FromClauseItem> FromClauseItems
		{
			get
			{
				return this._fromClauseItems;
			}
		}

		// Token: 0x0400162F RID: 5679
		private readonly NodeList<FromClauseItem> _fromClauseItems;
	}
}

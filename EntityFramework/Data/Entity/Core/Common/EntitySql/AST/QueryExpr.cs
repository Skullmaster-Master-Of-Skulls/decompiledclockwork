using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000234 RID: 564
	internal sealed class QueryExpr : Node
	{
		// Token: 0x060013CD RID: 5069 RVA: 0x000514E3 File Offset: 0x0004F6E3
		internal QueryExpr(SelectClause selectClause, FromClause fromClause, Node whereClause, GroupByClause groupByClause, HavingClause havingClause, OrderByClause orderByClause)
		{
			this._selectClause = selectClause;
			this._fromClause = fromClause;
			this._whereClause = whereClause;
			this._groupByClause = groupByClause;
			this._havingClause = havingClause;
			this._orderByClause = orderByClause;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x00051518 File Offset: 0x0004F718
		internal SelectClause SelectClause
		{
			get
			{
				return this._selectClause;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x00051520 File Offset: 0x0004F720
		internal FromClause FromClause
		{
			get
			{
				return this._fromClause;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00051528 File Offset: 0x0004F728
		internal Node WhereClause
		{
			get
			{
				return this._whereClause;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00051530 File Offset: 0x0004F730
		internal GroupByClause GroupByClause
		{
			get
			{
				return this._groupByClause;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00051538 File Offset: 0x0004F738
		internal HavingClause HavingClause
		{
			get
			{
				return this._havingClause;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00051540 File Offset: 0x0004F740
		internal OrderByClause OrderByClause
		{
			get
			{
				return this._orderByClause;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00051548 File Offset: 0x0004F748
		internal bool HasMethodCall
		{
			get
			{
				return this._selectClause.HasMethodCall || (this._havingClause != null && this._havingClause.HasMethodCall) || (this._orderByClause != null && this._orderByClause.HasMethodCall);
			}
		}

		// Token: 0x0400062F RID: 1583
		private readonly SelectClause _selectClause;

		// Token: 0x04000630 RID: 1584
		private readonly FromClause _fromClause;

		// Token: 0x04000631 RID: 1585
		private readonly Node _whereClause;

		// Token: 0x04000632 RID: 1586
		private readonly GroupByClause _groupByClause;

		// Token: 0x04000633 RID: 1587
		private readonly HavingClause _havingClause;

		// Token: 0x04000634 RID: 1588
		private readonly OrderByClause _orderByClause;
	}
}

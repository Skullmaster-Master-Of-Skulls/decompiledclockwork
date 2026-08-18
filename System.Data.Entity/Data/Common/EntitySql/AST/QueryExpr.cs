using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000379 RID: 889
	internal sealed class QueryExpr : Node
	{
		// Token: 0x06003234 RID: 12852 RVA: 0x000C4F27 File Offset: 0x000C3127
		internal QueryExpr(SelectClause selectClause, FromClause fromClause, Node whereClause, GroupByClause groupByClause, HavingClause havingClause, OrderByClause orderByClause)
		{
			this._selectClause = selectClause;
			this._fromClause = fromClause;
			this._whereClause = whereClause;
			this._groupByClause = groupByClause;
			this._havingClause = havingClause;
			this._orderByClause = orderByClause;
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x000C4F5C File Offset: 0x000C315C
		internal SelectClause SelectClause
		{
			get
			{
				return this._selectClause;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000C4F64 File Offset: 0x000C3164
		internal FromClause FromClause
		{
			get
			{
				return this._fromClause;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x000C4F6C File Offset: 0x000C316C
		internal Node WhereClause
		{
			get
			{
				return this._whereClause;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x000C4F74 File Offset: 0x000C3174
		internal GroupByClause GroupByClause
		{
			get
			{
				return this._groupByClause;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000C4F7C File Offset: 0x000C317C
		internal HavingClause HavingClause
		{
			get
			{
				return this._havingClause;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x000C4F84 File Offset: 0x000C3184
		internal OrderByClause OrderByClause
		{
			get
			{
				return this._orderByClause;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000C4F8C File Offset: 0x000C318C
		internal bool HasMethodCall
		{
			get
			{
				return this._selectClause.HasMethodCall || (this._havingClause != null && this._havingClause.HasMethodCall) || (this._orderByClause != null && this._orderByClause.HasMethodCall);
			}
		}

		// Token: 0x04001624 RID: 5668
		private readonly SelectClause _selectClause;

		// Token: 0x04001625 RID: 5669
		private readonly FromClause _fromClause;

		// Token: 0x04001626 RID: 5670
		private readonly Node _whereClause;

		// Token: 0x04001627 RID: 5671
		private readonly GroupByClause _groupByClause;

		// Token: 0x04001628 RID: 5672
		private readonly HavingClause _havingClause;

		// Token: 0x04001629 RID: 5673
		private readonly OrderByClause _orderByClause;
	}
}

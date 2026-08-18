using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000381 RID: 897
	internal sealed class OrderByClauseItem : Node
	{
		// Token: 0x06003253 RID: 12883 RVA: 0x000C5110 File Offset: 0x000C3310
		internal OrderByClauseItem(Node orderExpr, OrderKind orderKind) : this(orderExpr, orderKind, null)
		{
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000C511B File Offset: 0x000C331B
		internal OrderByClauseItem(Node orderExpr, OrderKind orderKind, Identifier optCollationIdentifier)
		{
			this._orderExpr = orderExpr;
			this._orderKind = orderKind;
			this._optCollationIdentifier = optCollationIdentifier;
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x000C5138 File Offset: 0x000C3338
		internal Node OrderExpr
		{
			get
			{
				return this._orderExpr;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06003256 RID: 12886 RVA: 0x000C5140 File Offset: 0x000C3340
		internal OrderKind OrderKind
		{
			get
			{
				return this._orderKind;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000C5148 File Offset: 0x000C3348
		internal Identifier Collation
		{
			get
			{
				return this._optCollationIdentifier;
			}
		}

		// Token: 0x0400163D RID: 5693
		private readonly Node _orderExpr;

		// Token: 0x0400163E RID: 5694
		private readonly OrderKind _orderKind;

		// Token: 0x0400163F RID: 5695
		private readonly Identifier _optCollationIdentifier;
	}
}

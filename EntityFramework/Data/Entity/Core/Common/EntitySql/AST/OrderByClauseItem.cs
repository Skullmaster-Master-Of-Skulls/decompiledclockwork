using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000231 RID: 561
	internal sealed class OrderByClauseItem : Node
	{
		// Token: 0x060013C6 RID: 5062 RVA: 0x0005148C File Offset: 0x0004F68C
		internal OrderByClauseItem(Node orderExpr, OrderKind orderKind) : this(orderExpr, orderKind, null)
		{
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00051497 File Offset: 0x0004F697
		internal OrderByClauseItem(Node orderExpr, OrderKind orderKind, Identifier optCollationIdentifier)
		{
			this._orderExpr = orderExpr;
			this._orderKind = orderKind;
			this._optCollationIdentifier = optCollationIdentifier;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x000514B4 File Offset: 0x0004F6B4
		internal Node OrderExpr
		{
			get
			{
				return this._orderExpr;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x000514BC File Offset: 0x0004F6BC
		internal OrderKind OrderKind
		{
			get
			{
				return this._orderKind;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x000514C4 File Offset: 0x0004F6C4
		internal Identifier Collation
		{
			get
			{
				return this._optCollationIdentifier;
			}
		}

		// Token: 0x04000627 RID: 1575
		private readonly Node _orderExpr;

		// Token: 0x04000628 RID: 1576
		private readonly OrderKind _orderKind;

		// Token: 0x04000629 RID: 1577
		private readonly Identifier _optCollationIdentifier;
	}
}

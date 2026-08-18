using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003B9 RID: 953
	internal abstract class NormalFormNode<T_Identifier>
	{
		// Token: 0x060033E7 RID: 13287 RVA: 0x000C8E77 File Offset: 0x000C7077
		protected NormalFormNode(BoolExpr<T_Identifier> expr)
		{
			this._expr = expr.Simplify();
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x000C8E8B File Offset: 0x000C708B
		internal BoolExpr<T_Identifier> Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000C8E93 File Offset: 0x000C7093
		protected static BoolExpr<T_Identifier> ExprSelector<T_NormalFormNode>(T_NormalFormNode node) where T_NormalFormNode : NormalFormNode<T_Identifier>
		{
			return node._expr;
		}

		// Token: 0x040016A8 RID: 5800
		private readonly BoolExpr<T_Identifier> _expr;
	}
}

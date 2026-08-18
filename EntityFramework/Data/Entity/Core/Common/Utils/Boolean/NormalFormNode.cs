using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000300 RID: 768
	internal abstract class NormalFormNode<T_Identifier>
	{
		// Token: 0x06001AF5 RID: 6901 RVA: 0x000868D0 File Offset: 0x00084AD0
		protected NormalFormNode(BoolExpr<T_Identifier> expr)
		{
			this._expr = expr.Simplify();
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x000868E4 File Offset: 0x00084AE4
		internal BoolExpr<T_Identifier> Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000868EC File Offset: 0x00084AEC
		protected static BoolExpr<T_Identifier> ExprSelector<T_NormalFormNode>(T_NormalFormNode node) where T_NormalFormNode : NormalFormNode<T_Identifier>
		{
			return node._expr;
		}

		// Token: 0x04000977 RID: 2423
		private readonly BoolExpr<T_Identifier> _expr;
	}
}

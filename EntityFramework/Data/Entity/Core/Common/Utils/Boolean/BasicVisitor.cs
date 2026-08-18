using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x020001EA RID: 490
	internal abstract class BasicVisitor<T_Identifier> : Visitor<T_Identifier, BoolExpr<T_Identifier>>
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x000493CA File Offset: 0x000475CA
		internal override BoolExpr<T_Identifier> VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return expression;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000493CD File Offset: 0x000475CD
		internal override BoolExpr<T_Identifier> VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return expression;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000493D0 File Offset: 0x000475D0
		internal override BoolExpr<T_Identifier> VisitTerm(TermExpr<T_Identifier> expression)
		{
			return expression;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000493D3 File Offset: 0x000475D3
		internal override BoolExpr<T_Identifier> VisitNot(NotExpr<T_Identifier> expression)
		{
			return new NotExpr<T_Identifier>(expression.Child.Accept<BoolExpr<T_Identifier>>(this));
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000493E6 File Offset: 0x000475E6
		internal override BoolExpr<T_Identifier> VisitAnd(AndExpr<T_Identifier> expression)
		{
			return new AndExpr<T_Identifier>(this.AcceptChildren(expression.Children));
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000493F9 File Offset: 0x000475F9
		internal override BoolExpr<T_Identifier> VisitOr(OrExpr<T_Identifier> expression)
		{
			return new OrExpr<T_Identifier>(this.AcceptChildren(expression.Children));
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000495A8 File Offset: 0x000477A8
		private IEnumerable<BoolExpr<T_Identifier>> AcceptChildren(IEnumerable<BoolExpr<T_Identifier>> children)
		{
			foreach (BoolExpr<T_Identifier> child in children)
			{
				yield return child.Accept<BoolExpr<T_Identifier>>(this);
			}
			yield break;
		}
	}
}

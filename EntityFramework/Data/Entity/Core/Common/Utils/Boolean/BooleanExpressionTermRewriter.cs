using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x020002FF RID: 767
	internal class BooleanExpressionTermRewriter<T_From, T_To> : Visitor<T_From, BoolExpr<T_To>>
	{
		// Token: 0x06001AED RID: 6893 RVA: 0x000866B3 File Offset: 0x000848B3
		internal BooleanExpressionTermRewriter(Func<TermExpr<T_From>, BoolExpr<T_To>> translator)
		{
			this._translator = translator;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000866C2 File Offset: 0x000848C2
		internal override BoolExpr<T_To> VisitFalse(FalseExpr<T_From> expression)
		{
			return FalseExpr<T_To>.Value;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000866C9 File Offset: 0x000848C9
		internal override BoolExpr<T_To> VisitTrue(TrueExpr<T_From> expression)
		{
			return TrueExpr<T_To>.Value;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000866D0 File Offset: 0x000848D0
		internal override BoolExpr<T_To> VisitNot(NotExpr<T_From> expression)
		{
			return new NotExpr<T_To>(expression.Child.Accept<BoolExpr<T_To>>(this));
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000866E3 File Offset: 0x000848E3
		internal override BoolExpr<T_To> VisitTerm(TermExpr<T_From> expression)
		{
			return this._translator(expression);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000866F1 File Offset: 0x000848F1
		internal override BoolExpr<T_To> VisitAnd(AndExpr<T_From> expression)
		{
			return new AndExpr<T_To>(this.VisitChildren(expression));
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000866FF File Offset: 0x000848FF
		internal override BoolExpr<T_To> VisitOr(OrExpr<T_From> expression)
		{
			return new OrExpr<T_To>(this.VisitChildren(expression));
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000868AC File Offset: 0x00084AAC
		private IEnumerable<BoolExpr<T_To>> VisitChildren(TreeExpr<T_From> expression)
		{
			foreach (BoolExpr<T_From> child in expression.Children)
			{
				yield return child.Accept<BoolExpr<T_To>>(this);
			}
			yield break;
		}

		// Token: 0x04000976 RID: 2422
		private readonly Func<TermExpr<T_From>, BoolExpr<T_To>> _translator;
	}
}

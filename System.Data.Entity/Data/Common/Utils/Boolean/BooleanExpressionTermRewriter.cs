using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003C2 RID: 962
	internal class BooleanExpressionTermRewriter<T_From, T_To> : Visitor<T_From, BoolExpr<T_To>>
	{
		// Token: 0x06003418 RID: 13336 RVA: 0x000C93E8 File Offset: 0x000C75E8
		internal BooleanExpressionTermRewriter(Func<TermExpr<T_From>, BoolExpr<T_To>> translator)
		{
			this._translator = translator;
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000C93F7 File Offset: 0x000C75F7
		internal override BoolExpr<T_To> VisitFalse(FalseExpr<T_From> expression)
		{
			return FalseExpr<T_To>.Value;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000C93FE File Offset: 0x000C75FE
		internal override BoolExpr<T_To> VisitTrue(TrueExpr<T_From> expression)
		{
			return TrueExpr<T_To>.Value;
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000C9405 File Offset: 0x000C7605
		internal override BoolExpr<T_To> VisitNot(NotExpr<T_From> expression)
		{
			return new NotExpr<T_To>(expression.Child.Accept<BoolExpr<T_To>>(this));
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000C9418 File Offset: 0x000C7618
		internal override BoolExpr<T_To> VisitTerm(TermExpr<T_From> expression)
		{
			return this._translator(expression);
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000C9426 File Offset: 0x000C7626
		internal override BoolExpr<T_To> VisitAnd(AndExpr<T_From> expression)
		{
			return new AndExpr<T_To>(this.VisitChildren(expression));
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000C9434 File Offset: 0x000C7634
		internal override BoolExpr<T_To> VisitOr(OrExpr<T_From> expression)
		{
			return new OrExpr<T_To>(this.VisitChildren(expression));
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000C9442 File Offset: 0x000C7642
		private IEnumerable<BoolExpr<T_To>> VisitChildren(TreeExpr<T_From> expression)
		{
			foreach (BoolExpr<T_From> boolExpr in expression.Children)
			{
				yield return boolExpr.Accept<BoolExpr<T_To>>(this);
			}
			HashSet<BoolExpr<T_From>>.Enumerator enumerator = default(HashSet<BoolExpr<T_From>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x040016AD RID: 5805
		private readonly Func<TermExpr<T_From>, BoolExpr<T_To>> _translator;
	}
}

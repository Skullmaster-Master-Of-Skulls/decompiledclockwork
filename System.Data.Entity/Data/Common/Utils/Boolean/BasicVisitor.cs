using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003BF RID: 959
	internal abstract class BasicVisitor<T_Identifier> : Visitor<T_Identifier, BoolExpr<T_Identifier>>
	{
		// Token: 0x060033FC RID: 13308 RVA: 0x00002391 File Offset: 0x00000591
		internal override BoolExpr<T_Identifier> VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return expression;
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x00002391 File Offset: 0x00000591
		internal override BoolExpr<T_Identifier> VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return expression;
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x00002391 File Offset: 0x00000591
		internal override BoolExpr<T_Identifier> VisitTerm(TermExpr<T_Identifier> expression)
		{
			return expression;
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000C921C File Offset: 0x000C741C
		internal override BoolExpr<T_Identifier> VisitNot(NotExpr<T_Identifier> expression)
		{
			return new NotExpr<T_Identifier>(expression.Child.Accept<BoolExpr<T_Identifier>>(this));
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000C922F File Offset: 0x000C742F
		internal override BoolExpr<T_Identifier> VisitAnd(AndExpr<T_Identifier> expression)
		{
			return new AndExpr<T_Identifier>(this.AcceptChildren(expression.Children));
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000C9242 File Offset: 0x000C7442
		internal override BoolExpr<T_Identifier> VisitOr(OrExpr<T_Identifier> expression)
		{
			return new OrExpr<T_Identifier>(this.AcceptChildren(expression.Children));
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000C9255 File Offset: 0x000C7455
		private IEnumerable<BoolExpr<T_Identifier>> AcceptChildren(IEnumerable<BoolExpr<T_Identifier>> children)
		{
			foreach (BoolExpr<T_Identifier> boolExpr in children)
			{
				yield return boolExpr.Accept<BoolExpr<T_Identifier>>(this);
			}
			IEnumerator<BoolExpr<T_Identifier>> enumerator = null;
			yield break;
			yield break;
		}
	}
}

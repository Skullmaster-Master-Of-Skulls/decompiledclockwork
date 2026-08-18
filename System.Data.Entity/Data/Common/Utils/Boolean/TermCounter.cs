using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003C0 RID: 960
	internal class TermCounter<T_Identifier> : Visitor<T_Identifier, int>
	{
		// Token: 0x06003404 RID: 13316 RVA: 0x000C9274 File Offset: 0x000C7474
		internal static int CountTerms(BoolExpr<T_Identifier> expression)
		{
			return expression.Accept<int>(TermCounter<T_Identifier>.s_instance);
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override int VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return 0;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override int VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return 0;
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int VisitTerm(TermExpr<T_Identifier> expression)
		{
			return 1;
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x000C9281 File Offset: 0x000C7481
		internal override int VisitNot(NotExpr<T_Identifier> expression)
		{
			return expression.Child.Accept<int>(this);
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000C928F File Offset: 0x000C748F
		internal override int VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000C928F File Offset: 0x000C748F
		internal override int VisitOr(OrExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000C9298 File Offset: 0x000C7498
		private int VisitTree(TreeExpr<T_Identifier> expression)
		{
			int num = 0;
			foreach (BoolExpr<T_Identifier> boolExpr in expression.Children)
			{
				num += boolExpr.Accept<int>(this);
			}
			return num;
		}

		// Token: 0x040016AB RID: 5803
		private static readonly TermCounter<T_Identifier> s_instance = new TermCounter<T_Identifier>();
	}
}

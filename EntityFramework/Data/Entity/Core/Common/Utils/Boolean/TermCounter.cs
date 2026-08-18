using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x020001ED RID: 493
	internal class TermCounter<T_Identifier> : Visitor<T_Identifier, int>
	{
		// Token: 0x06001148 RID: 4424 RVA: 0x000497EC File Offset: 0x000479EC
		internal static int CountTerms(BoolExpr<T_Identifier> expression)
		{
			return expression.Accept<int>(TermCounter<T_Identifier>._instance);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000497F9 File Offset: 0x000479F9
		internal override int VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return 0;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000497FC File Offset: 0x000479FC
		internal override int VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return 0;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000497FF File Offset: 0x000479FF
		internal override int VisitTerm(TermExpr<T_Identifier> expression)
		{
			return 1;
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00049802 File Offset: 0x00047A02
		internal override int VisitNot(NotExpr<T_Identifier> expression)
		{
			return expression.Child.Accept<int>(this);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00049810 File Offset: 0x00047A10
		internal override int VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00049819 File Offset: 0x00047A19
		internal override int VisitOr(OrExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00049824 File Offset: 0x00047A24
		private int VisitTree(TreeExpr<T_Identifier> expression)
		{
			int num = 0;
			foreach (BoolExpr<T_Identifier> boolExpr in expression.Children)
			{
				num += boolExpr.Accept<int>(this);
			}
			return num;
		}

		// Token: 0x04000523 RID: 1315
		private static readonly TermCounter<T_Identifier> _instance = new TermCounter<T_Identifier>();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003C1 RID: 961
	internal class LeafVisitor<T_Identifier> : Visitor<T_Identifier, bool>
	{
		// Token: 0x0600340E RID: 13326 RVA: 0x000C9308 File Offset: 0x000C7508
		private LeafVisitor()
		{
			this._terms = new List<TermExpr<T_Identifier>>();
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000C931C File Offset: 0x000C751C
		internal static List<TermExpr<T_Identifier>> GetTerms(BoolExpr<T_Identifier> expression)
		{
			LeafVisitor<T_Identifier> leafVisitor = new LeafVisitor<T_Identifier>();
			expression.Accept<bool>(leafVisitor);
			return leafVisitor._terms;
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000C933D File Offset: 0x000C753D
		internal static IEnumerable<T_Identifier> GetLeaves(BoolExpr<T_Identifier> expression)
		{
			return from term in LeafVisitor<T_Identifier>.GetTerms(expression)
			select term.Identifier;
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x00017938 File Offset: 0x00015B38
		internal override bool VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return true;
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x00017938 File Offset: 0x00015B38
		internal override bool VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return true;
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000C9369 File Offset: 0x000C7569
		internal override bool VisitTerm(TermExpr<T_Identifier> expression)
		{
			this._terms.Add(expression);
			return true;
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000C9378 File Offset: 0x000C7578
		internal override bool VisitNot(NotExpr<T_Identifier> expression)
		{
			return expression.Child.Accept<bool>(this);
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000C9386 File Offset: 0x000C7586
		internal override bool VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000C9386 File Offset: 0x000C7586
		internal override bool VisitOr(OrExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000C9390 File Offset: 0x000C7590
		private bool VisitTree(TreeExpr<T_Identifier> expression)
		{
			foreach (BoolExpr<T_Identifier> boolExpr in expression.Children)
			{
				boolExpr.Accept<bool>(this);
			}
			return true;
		}

		// Token: 0x040016AC RID: 5804
		private readonly List<TermExpr<T_Identifier>> _terms;
	}
}

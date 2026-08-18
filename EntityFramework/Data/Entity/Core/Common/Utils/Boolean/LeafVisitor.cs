using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000312 RID: 786
	internal class LeafVisitor<T_Identifier> : Visitor<T_Identifier, bool>
	{
		// Token: 0x06001B42 RID: 6978 RVA: 0x00087637 File Offset: 0x00085837
		private LeafVisitor()
		{
			this._terms = new List<TermExpr<T_Identifier>>();
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0008764C File Offset: 0x0008584C
		internal static List<TermExpr<T_Identifier>> GetTerms(BoolExpr<T_Identifier> expression)
		{
			LeafVisitor<T_Identifier> leafVisitor = new LeafVisitor<T_Identifier>();
			expression.Accept<bool>(leafVisitor);
			return leafVisitor._terms;
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x00087675 File Offset: 0x00085875
		internal static IEnumerable<T_Identifier> GetLeaves(BoolExpr<T_Identifier> expression)
		{
			return from term in LeafVisitor<T_Identifier>.GetTerms(expression)
			select term.Identifier;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0008769F File Offset: 0x0008589F
		internal override bool VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return true;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x000876A2 File Offset: 0x000858A2
		internal override bool VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return true;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000876A5 File Offset: 0x000858A5
		internal override bool VisitTerm(TermExpr<T_Identifier> expression)
		{
			this._terms.Add(expression);
			return true;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000876B4 File Offset: 0x000858B4
		internal override bool VisitNot(NotExpr<T_Identifier> expression)
		{
			return expression.Child.Accept<bool>(this);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000876C2 File Offset: 0x000858C2
		internal override bool VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000876CB File Offset: 0x000858CB
		internal override bool VisitOr(OrExpr<T_Identifier> expression)
		{
			return this.VisitTree(expression);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000876D4 File Offset: 0x000858D4
		private bool VisitTree(TreeExpr<T_Identifier> expression)
		{
			foreach (BoolExpr<T_Identifier> boolExpr in expression.Children)
			{
				boolExpr.Accept<bool>(this);
			}
			return true;
		}

		// Token: 0x04000999 RID: 2457
		private readonly List<TermExpr<T_Identifier>> _terms;
	}
}

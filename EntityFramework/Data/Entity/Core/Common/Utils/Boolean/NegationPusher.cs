using System;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000315 RID: 789
	internal static class NegationPusher
	{
		// Token: 0x06001B56 RID: 6998 RVA: 0x00087802 File Offset: 0x00085A02
		internal static BoolExpr<DomainConstraint<T_Variable, T_Element>> EliminateNot<T_Variable, T_Element>(BoolExpr<DomainConstraint<T_Variable, T_Element>> expression)
		{
			return expression.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element>.Instance);
		}

		// Token: 0x02000316 RID: 790
		private class NonNegatedTreeVisitor<T_Identifier> : BasicVisitor<T_Identifier>
		{
			// Token: 0x06001B57 RID: 6999 RVA: 0x0008780F File Offset: 0x00085A0F
			protected NonNegatedTreeVisitor()
			{
			}

			// Token: 0x06001B58 RID: 7000 RVA: 0x00087817 File Offset: 0x00085A17
			internal override BoolExpr<T_Identifier> VisitNot(NotExpr<T_Identifier> expression)
			{
				return expression.Child.Accept<BoolExpr<T_Identifier>>(NegationPusher.NegatedTreeVisitor<T_Identifier>.Instance);
			}

			// Token: 0x0400099F RID: 2463
			internal static readonly NegationPusher.NonNegatedTreeVisitor<T_Identifier> Instance = new NegationPusher.NonNegatedTreeVisitor<T_Identifier>();
		}

		// Token: 0x02000317 RID: 791
		private class NegatedTreeVisitor<T_Identifier> : Visitor<T_Identifier, BoolExpr<T_Identifier>>
		{
			// Token: 0x06001B5A RID: 7002 RVA: 0x00087835 File Offset: 0x00085A35
			protected NegatedTreeVisitor()
			{
			}

			// Token: 0x06001B5B RID: 7003 RVA: 0x0008783D File Offset: 0x00085A3D
			internal override BoolExpr<T_Identifier> VisitTrue(TrueExpr<T_Identifier> expression)
			{
				return FalseExpr<T_Identifier>.Value;
			}

			// Token: 0x06001B5C RID: 7004 RVA: 0x00087844 File Offset: 0x00085A44
			internal override BoolExpr<T_Identifier> VisitFalse(FalseExpr<T_Identifier> expression)
			{
				return TrueExpr<T_Identifier>.Value;
			}

			// Token: 0x06001B5D RID: 7005 RVA: 0x0008784B File Offset: 0x00085A4B
			internal override BoolExpr<T_Identifier> VisitTerm(TermExpr<T_Identifier> expression)
			{
				return new NotExpr<T_Identifier>(expression);
			}

			// Token: 0x06001B5E RID: 7006 RVA: 0x00087853 File Offset: 0x00085A53
			internal override BoolExpr<T_Identifier> VisitNot(NotExpr<T_Identifier> expression)
			{
				return expression.Child.Accept<BoolExpr<T_Identifier>>(NegationPusher.NonNegatedTreeVisitor<T_Identifier>.Instance);
			}

			// Token: 0x06001B5F RID: 7007 RVA: 0x0008786E File Offset: 0x00085A6E
			internal override BoolExpr<T_Identifier> VisitAnd(AndExpr<T_Identifier> expression)
			{
				return new OrExpr<T_Identifier>(from child in expression.Children
				select child.Accept<BoolExpr<T_Identifier>>(this));
			}

			// Token: 0x06001B60 RID: 7008 RVA: 0x00087895 File Offset: 0x00085A95
			internal override BoolExpr<T_Identifier> VisitOr(OrExpr<T_Identifier> expression)
			{
				return new AndExpr<T_Identifier>(from child in expression.Children
				select child.Accept<BoolExpr<T_Identifier>>(this));
			}

			// Token: 0x040009A0 RID: 2464
			internal static readonly NegationPusher.NegatedTreeVisitor<T_Identifier> Instance = new NegationPusher.NegatedTreeVisitor<T_Identifier>();
		}

		// Token: 0x02000318 RID: 792
		private class NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element> : NegationPusher.NonNegatedTreeVisitor<DomainConstraint<T_Variable, T_Element>>
		{
			// Token: 0x06001B64 RID: 7012 RVA: 0x000878BF File Offset: 0x00085ABF
			private NonNegatedDomainConstraintTreeVisitor()
			{
			}

			// Token: 0x06001B65 RID: 7013 RVA: 0x000878C7 File Offset: 0x00085AC7
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> VisitNot(NotExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				return expression.Child.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(NegationPusher.NegatedDomainConstraintTreeVisitor<T_Variable, T_Element>.Instance);
			}

			// Token: 0x040009A1 RID: 2465
			internal new static readonly NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element> Instance = new NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element>();
		}

		// Token: 0x02000319 RID: 793
		private class NegatedDomainConstraintTreeVisitor<T_Variable, T_Element> : NegationPusher.NegatedTreeVisitor<DomainConstraint<T_Variable, T_Element>>
		{
			// Token: 0x06001B67 RID: 7015 RVA: 0x000878E5 File Offset: 0x00085AE5
			private NegatedDomainConstraintTreeVisitor()
			{
			}

			// Token: 0x06001B68 RID: 7016 RVA: 0x000878ED File Offset: 0x00085AED
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> VisitNot(NotExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				return expression.Child.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element>.Instance);
			}

			// Token: 0x06001B69 RID: 7017 RVA: 0x000878FF File Offset: 0x00085AFF
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> VisitTerm(TermExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				return new TermExpr<DomainConstraint<T_Variable, T_Element>>(expression.Identifier.InvertDomainConstraint());
			}

			// Token: 0x040009A2 RID: 2466
			internal new static readonly NegationPusher.NegatedDomainConstraintTreeVisitor<T_Variable, T_Element> Instance = new NegationPusher.NegatedDomainConstraintTreeVisitor<T_Variable, T_Element>();
		}
	}
}

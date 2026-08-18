using System;
using System.Linq;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003B8 RID: 952
	internal static class NegationPusher
	{
		// Token: 0x060033E6 RID: 13286 RVA: 0x000C8E6A File Offset: 0x000C706A
		internal static BoolExpr<DomainConstraint<T_Variable, T_Element>> EliminateNot<T_Variable, T_Element>(BoolExpr<DomainConstraint<T_Variable, T_Element>> expression)
		{
			return expression.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element>.Instance);
		}

		// Token: 0x0200068A RID: 1674
		private class NonNegatedTreeVisitor<T_Identifier> : BasicVisitor<T_Identifier>
		{
			// Token: 0x0600450B RID: 17675 RVA: 0x000F8E79 File Offset: 0x000F7079
			protected NonNegatedTreeVisitor()
			{
			}

			// Token: 0x0600450C RID: 17676 RVA: 0x000F8E81 File Offset: 0x000F7081
			internal override BoolExpr<T_Identifier> VisitNot(NotExpr<T_Identifier> expression)
			{
				return expression.Child.Accept<BoolExpr<T_Identifier>>(NegationPusher.NegatedTreeVisitor<T_Identifier>.Instance);
			}

			// Token: 0x04001FD8 RID: 8152
			internal static readonly NegationPusher.NonNegatedTreeVisitor<T_Identifier> Instance = new NegationPusher.NonNegatedTreeVisitor<T_Identifier>();
		}

		// Token: 0x0200068B RID: 1675
		private class NegatedTreeVisitor<T_Identifier> : Visitor<T_Identifier, BoolExpr<T_Identifier>>
		{
			// Token: 0x0600450E RID: 17678 RVA: 0x000F8E9F File Offset: 0x000F709F
			protected NegatedTreeVisitor()
			{
			}

			// Token: 0x0600450F RID: 17679 RVA: 0x000F8EA7 File Offset: 0x000F70A7
			internal override BoolExpr<T_Identifier> VisitTrue(TrueExpr<T_Identifier> expression)
			{
				return FalseExpr<T_Identifier>.Value;
			}

			// Token: 0x06004510 RID: 17680 RVA: 0x000F8EAE File Offset: 0x000F70AE
			internal override BoolExpr<T_Identifier> VisitFalse(FalseExpr<T_Identifier> expression)
			{
				return TrueExpr<T_Identifier>.Value;
			}

			// Token: 0x06004511 RID: 17681 RVA: 0x000F8EB5 File Offset: 0x000F70B5
			internal override BoolExpr<T_Identifier> VisitTerm(TermExpr<T_Identifier> expression)
			{
				return new NotExpr<T_Identifier>(expression);
			}

			// Token: 0x06004512 RID: 17682 RVA: 0x000F8EBD File Offset: 0x000F70BD
			internal override BoolExpr<T_Identifier> VisitNot(NotExpr<T_Identifier> expression)
			{
				return expression.Child.Accept<BoolExpr<T_Identifier>>(NegationPusher.NonNegatedTreeVisitor<T_Identifier>.Instance);
			}

			// Token: 0x06004513 RID: 17683 RVA: 0x000F8ECF File Offset: 0x000F70CF
			internal override BoolExpr<T_Identifier> VisitAnd(AndExpr<T_Identifier> expression)
			{
				return new OrExpr<T_Identifier>(from child in expression.Children
				select child.Accept<BoolExpr<T_Identifier>>(this));
			}

			// Token: 0x06004514 RID: 17684 RVA: 0x000F8EED File Offset: 0x000F70ED
			internal override BoolExpr<T_Identifier> VisitOr(OrExpr<T_Identifier> expression)
			{
				return new AndExpr<T_Identifier>(from child in expression.Children
				select child.Accept<BoolExpr<T_Identifier>>(this));
			}

			// Token: 0x04001FD9 RID: 8153
			internal static readonly NegationPusher.NegatedTreeVisitor<T_Identifier> Instance = new NegationPusher.NegatedTreeVisitor<T_Identifier>();
		}

		// Token: 0x0200068C RID: 1676
		private class NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element> : NegationPusher.NonNegatedTreeVisitor<DomainConstraint<T_Variable, T_Element>>
		{
			// Token: 0x06004518 RID: 17688 RVA: 0x000F8F20 File Offset: 0x000F7120
			private NonNegatedDomainConstraintTreeVisitor()
			{
			}

			// Token: 0x06004519 RID: 17689 RVA: 0x000F8F28 File Offset: 0x000F7128
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> VisitNot(NotExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				return expression.Child.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(NegationPusher.NegatedDomainConstraintTreeVisitor<T_Variable, T_Element>.Instance);
			}

			// Token: 0x04001FDA RID: 8154
			internal new static readonly NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element> Instance = new NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element>();
		}

		// Token: 0x0200068D RID: 1677
		private class NegatedDomainConstraintTreeVisitor<T_Variable, T_Element> : NegationPusher.NegatedTreeVisitor<DomainConstraint<T_Variable, T_Element>>
		{
			// Token: 0x0600451B RID: 17691 RVA: 0x000F8F46 File Offset: 0x000F7146
			private NegatedDomainConstraintTreeVisitor()
			{
			}

			// Token: 0x0600451C RID: 17692 RVA: 0x000F8F4E File Offset: 0x000F714E
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> VisitNot(NotExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				return expression.Child.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(NegationPusher.NonNegatedDomainConstraintTreeVisitor<T_Variable, T_Element>.Instance);
			}

			// Token: 0x0600451D RID: 17693 RVA: 0x000F8F60 File Offset: 0x000F7160
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> VisitTerm(TermExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				return new TermExpr<DomainConstraint<T_Variable, T_Element>>(expression.Identifier.InvertDomainConstraint());
			}

			// Token: 0x04001FDB RID: 8155
			internal new static readonly NegationPusher.NegatedDomainConstraintTreeVisitor<T_Variable, T_Element> Instance = new NegationPusher.NegatedDomainConstraintTreeVisitor<T_Variable, T_Element>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x0200010C RID: 268
	public abstract class BasicExpressionVisitor : DbExpressionVisitor
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x000261C9 File Offset: 0x000243C9
		protected virtual void VisitUnaryExpression(DbUnaryExpression expression)
		{
			Check.NotNull<DbUnaryExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000261E3 File Offset: 0x000243E3
		protected virtual void VisitBinaryExpression(DbBinaryExpression expression)
		{
			Check.NotNull<DbBinaryExpression>(expression, "expression");
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00026209 File Offset: 0x00024409
		protected virtual void VisitExpressionBindingPre(DbExpressionBinding binding)
		{
			Check.NotNull<DbExpressionBinding>(binding, "binding");
			this.VisitExpression(binding.Expression);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00026223 File Offset: 0x00024423
		protected virtual void VisitExpressionBindingPost(DbExpressionBinding binding)
		{
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00026225 File Offset: 0x00024425
		protected virtual void VisitGroupExpressionBindingPre(DbGroupExpressionBinding binding)
		{
			Check.NotNull<DbGroupExpressionBinding>(binding, "binding");
			this.VisitExpression(binding.Expression);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0002623F File Offset: 0x0002443F
		protected virtual void VisitGroupExpressionBindingMid(DbGroupExpressionBinding binding)
		{
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00026241 File Offset: 0x00024441
		protected virtual void VisitGroupExpressionBindingPost(DbGroupExpressionBinding binding)
		{
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00026243 File Offset: 0x00024443
		protected virtual void VisitLambdaPre(DbLambda lambda)
		{
			Check.NotNull<DbLambda>(lambda, "lambda");
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00026251 File Offset: 0x00024451
		protected virtual void VisitLambdaPost(DbLambda lambda)
		{
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00026253 File Offset: 0x00024453
		public virtual void VisitExpression(DbExpression expression)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			expression.Accept(this);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00026268 File Offset: 0x00024468
		public virtual void VisitExpressionList(IList<DbExpression> expressionList)
		{
			Check.NotNull<IList<DbExpression>>(expressionList, "expressionList");
			for (int i = 0; i < expressionList.Count; i++)
			{
				this.VisitExpression(expressionList[i]);
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000262A0 File Offset: 0x000244A0
		public virtual void VisitAggregateList(IList<DbAggregate> aggregates)
		{
			Check.NotNull<IList<DbAggregate>>(aggregates, "aggregates");
			for (int i = 0; i < aggregates.Count; i++)
			{
				this.VisitAggregate(aggregates[i]);
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000262D7 File Offset: 0x000244D7
		public virtual void VisitAggregate(DbAggregate aggregate)
		{
			Check.NotNull<DbAggregate>(aggregate, "aggregate");
			this.VisitExpressionList(aggregate.Arguments);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000262F4 File Offset: 0x000244F4
		internal virtual void VisitRelatedEntityReferenceList(IList<DbRelatedEntityRef> relatedEntityReferences)
		{
			for (int i = 0; i < relatedEntityReferences.Count; i++)
			{
				this.VisitRelatedEntityReference(relatedEntityReferences[i]);
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0002631F File Offset: 0x0002451F
		internal virtual void VisitRelatedEntityReference(DbRelatedEntityRef relatedEntityRef)
		{
			this.VisitExpression(relatedEntityRef.TargetEntityReference);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0002632D File Offset: 0x0002452D
		public override void Visit(DbExpression expression)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(expression.GetType().FullName));
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00026350 File Offset: 0x00024550
		public override void Visit(DbConstantExpression expression)
		{
			Check.NotNull<DbConstantExpression>(expression, "expression");
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0002635E File Offset: 0x0002455E
		public override void Visit(DbNullExpression expression)
		{
			Check.NotNull<DbNullExpression>(expression, "expression");
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0002636C File Offset: 0x0002456C
		public override void Visit(DbVariableReferenceExpression expression)
		{
			Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0002637A File Offset: 0x0002457A
		public override void Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00026388 File Offset: 0x00024588
		public override void Visit(DbFunctionExpression expression)
		{
			Check.NotNull<DbFunctionExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000263A4 File Offset: 0x000245A4
		public override void Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
			this.VisitLambdaPre(expression.Lambda);
			this.VisitExpression(expression.Lambda.Body);
			this.VisitLambdaPost(expression.Lambda);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000263F2 File Offset: 0x000245F2
		public override void Visit(DbPropertyExpression expression)
		{
			Check.NotNull<DbPropertyExpression>(expression, "expression");
			if (expression.Instance != null)
			{
				this.VisitExpression(expression.Instance);
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00026414 File Offset: 0x00024614
		public override void Visit(DbComparisonExpression expression)
		{
			Check.NotNull<DbComparisonExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00026429 File Offset: 0x00024629
		public override void Visit(DbLikeExpression expression)
		{
			Check.NotNull<DbLikeExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
			this.VisitExpression(expression.Pattern);
			this.VisitExpression(expression.Escape);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0002645B File Offset: 0x0002465B
		public override void Visit(DbLimitExpression expression)
		{
			Check.NotNull<DbLimitExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
			this.VisitExpression(expression.Limit);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00026481 File Offset: 0x00024681
		public override void Visit(DbIsNullExpression expression)
		{
			Check.NotNull<DbIsNullExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00026496 File Offset: 0x00024696
		public override void Visit(DbArithmeticExpression expression)
		{
			Check.NotNull<DbArithmeticExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000264B0 File Offset: 0x000246B0
		public override void Visit(DbAndExpression expression)
		{
			Check.NotNull<DbAndExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000264C5 File Offset: 0x000246C5
		public override void Visit(DbOrExpression expression)
		{
			Check.NotNull<DbOrExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000264DA File Offset: 0x000246DA
		public override void Visit(DbInExpression expression)
		{
			Check.NotNull<DbInExpression>(expression, "expression");
			this.VisitExpression(expression.Item);
			this.VisitExpressionList(expression.List);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00026500 File Offset: 0x00024700
		public override void Visit(DbNotExpression expression)
		{
			Check.NotNull<DbNotExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00026515 File Offset: 0x00024715
		public override void Visit(DbDistinctExpression expression)
		{
			Check.NotNull<DbDistinctExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0002652A File Offset: 0x0002472A
		public override void Visit(DbElementExpression expression)
		{
			Check.NotNull<DbElementExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0002653F File Offset: 0x0002473F
		public override void Visit(DbIsEmptyExpression expression)
		{
			Check.NotNull<DbIsEmptyExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00026554 File Offset: 0x00024754
		public override void Visit(DbUnionAllExpression expression)
		{
			Check.NotNull<DbUnionAllExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00026569 File Offset: 0x00024769
		public override void Visit(DbIntersectExpression expression)
		{
			Check.NotNull<DbIntersectExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0002657E File Offset: 0x0002477E
		public override void Visit(DbExceptExpression expression)
		{
			Check.NotNull<DbExceptExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00026593 File Offset: 0x00024793
		public override void Visit(DbOfTypeExpression expression)
		{
			Check.NotNull<DbOfTypeExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000265A8 File Offset: 0x000247A8
		public override void Visit(DbTreatExpression expression)
		{
			Check.NotNull<DbTreatExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000265BD File Offset: 0x000247BD
		public override void Visit(DbCastExpression expression)
		{
			Check.NotNull<DbCastExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000265D2 File Offset: 0x000247D2
		public override void Visit(DbIsOfExpression expression)
		{
			Check.NotNull<DbIsOfExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000265E7 File Offset: 0x000247E7
		public override void Visit(DbCaseExpression expression)
		{
			Check.NotNull<DbCaseExpression>(expression, "expression");
			this.VisitExpressionList(expression.When);
			this.VisitExpressionList(expression.Then);
			this.VisitExpression(expression.Else);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00026619 File Offset: 0x00024819
		public override void Visit(DbNewInstanceExpression expression)
		{
			Check.NotNull<DbNewInstanceExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
			if (expression.HasRelatedEntityReferences)
			{
				this.VisitRelatedEntityReferenceList(expression.RelatedEntityReferences);
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00026647 File Offset: 0x00024847
		public override void Visit(DbRefExpression expression)
		{
			Check.NotNull<DbRefExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0002665C File Offset: 0x0002485C
		public override void Visit(DbRelationshipNavigationExpression expression)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
			this.VisitExpression(expression.NavigationSource);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00026676 File Offset: 0x00024876
		public override void Visit(DbDerefExpression expression)
		{
			Check.NotNull<DbDerefExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0002668B File Offset: 0x0002488B
		public override void Visit(DbRefKeyExpression expression)
		{
			Check.NotNull<DbRefKeyExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000266A0 File Offset: 0x000248A0
		public override void Visit(DbEntityRefExpression expression)
		{
			Check.NotNull<DbEntityRefExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000266B5 File Offset: 0x000248B5
		public override void Visit(DbScanExpression expression)
		{
			Check.NotNull<DbScanExpression>(expression, "expression");
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000266C3 File Offset: 0x000248C3
		public override void Visit(DbFilterExpression expression)
		{
			Check.NotNull<DbFilterExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Predicate);
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000266F5 File Offset: 0x000248F5
		public override void Visit(DbProjectExpression expression)
		{
			Check.NotNull<DbProjectExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Projection);
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00026728 File Offset: 0x00024928
		public override void Visit(DbCrossJoinExpression expression)
		{
			Check.NotNull<DbCrossJoinExpression>(expression, "expression");
			foreach (DbExpressionBinding binding in expression.Inputs)
			{
				this.VisitExpressionBindingPre(binding);
			}
			foreach (DbExpressionBinding binding2 in expression.Inputs)
			{
				this.VisitExpressionBindingPost(binding2);
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x000267C0 File Offset: 0x000249C0
		public override void Visit(DbJoinExpression expression)
		{
			Check.NotNull<DbJoinExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Left);
			this.VisitExpressionBindingPre(expression.Right);
			this.VisitExpression(expression.JoinCondition);
			this.VisitExpressionBindingPost(expression.Left);
			this.VisitExpressionBindingPost(expression.Right);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00026815 File Offset: 0x00024A15
		public override void Visit(DbApplyExpression expression)
		{
			Check.NotNull<DbApplyExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			if (expression.Apply != null)
			{
				this.VisitExpression(expression.Apply.Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00026854 File Offset: 0x00024A54
		public override void Visit(DbGroupByExpression expression)
		{
			Check.NotNull<DbGroupByExpression>(expression, "expression");
			this.VisitGroupExpressionBindingPre(expression.Input);
			this.VisitExpressionList(expression.Keys);
			this.VisitGroupExpressionBindingMid(expression.Input);
			this.VisitAggregateList(expression.Aggregates);
			this.VisitGroupExpressionBindingPost(expression.Input);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000268AC File Offset: 0x00024AAC
		public override void Visit(DbSkipExpression expression)
		{
			Check.NotNull<DbSkipExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			foreach (DbSortClause dbSortClause in expression.SortOrder)
			{
				this.VisitExpression(dbSortClause.Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
			this.VisitExpression(expression.Count);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00026930 File Offset: 0x00024B30
		public override void Visit(DbSortExpression expression)
		{
			Check.NotNull<DbSortExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			for (int i = 0; i < expression.SortOrder.Count; i++)
			{
				this.VisitExpression(expression.SortOrder[i].Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0002698E File Offset: 0x00024B8E
		public override void Visit(DbQuantifierExpression expression)
		{
			Check.NotNull<DbQuantifierExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Predicate);
			this.VisitExpressionBindingPost(expression.Input);
		}
	}
}

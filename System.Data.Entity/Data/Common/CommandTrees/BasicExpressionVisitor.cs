using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003F5 RID: 1013
	internal abstract class BasicExpressionVisitor : DbExpressionVisitor
	{
		// Token: 0x06003618 RID: 13848 RVA: 0x000D0484 File Offset: 0x000CE684
		protected virtual void VisitUnaryExpression(DbUnaryExpression expression)
		{
			this.VisitExpression(EntityUtil.CheckArgumentNull<DbUnaryExpression>(expression, "expression").Argument);
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000D049C File Offset: 0x000CE69C
		protected virtual void VisitBinaryExpression(DbBinaryExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbBinaryExpression>(expression, "expression");
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000D04C2 File Offset: 0x000CE6C2
		protected virtual void VisitExpressionBindingPre(DbExpressionBinding binding)
		{
			EntityUtil.CheckArgumentNull<DbExpressionBinding>(binding, "binding");
			this.VisitExpression(binding.Expression);
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void VisitExpressionBindingPost(DbExpressionBinding binding)
		{
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000D04DC File Offset: 0x000CE6DC
		protected virtual void VisitGroupExpressionBindingPre(DbGroupExpressionBinding binding)
		{
			EntityUtil.CheckArgumentNull<DbGroupExpressionBinding>(binding, "binding");
			this.VisitExpression(binding.Expression);
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void VisitGroupExpressionBindingMid(DbGroupExpressionBinding binding)
		{
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void VisitGroupExpressionBindingPost(DbGroupExpressionBinding binding)
		{
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000D04F6 File Offset: 0x000CE6F6
		protected virtual void VisitLambdaPre(DbLambda lambda)
		{
			EntityUtil.CheckArgumentNull<DbLambda>(lambda, "lambda");
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void VisitLambdaPost(DbLambda lambda)
		{
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000D0504 File Offset: 0x000CE704
		public virtual void VisitExpression(DbExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(expression, "expression").Accept(this);
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000D0518 File Offset: 0x000CE718
		public virtual void VisitExpressionList(IList<DbExpression> expressionList)
		{
			EntityUtil.CheckArgumentNull<IList<DbExpression>>(expressionList, "expressionList");
			for (int i = 0; i < expressionList.Count; i++)
			{
				this.VisitExpression(expressionList[i]);
			}
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000D0550 File Offset: 0x000CE750
		public virtual void VisitAggregateList(IList<DbAggregate> aggregates)
		{
			EntityUtil.CheckArgumentNull<IList<DbAggregate>>(aggregates, "aggregates");
			for (int i = 0; i < aggregates.Count; i++)
			{
				this.VisitAggregate(aggregates[i]);
			}
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000D0587 File Offset: 0x000CE787
		public virtual void VisitAggregate(DbAggregate aggregate)
		{
			this.VisitExpressionList(EntityUtil.CheckArgumentNull<DbAggregate>(aggregate, "aggregate").Arguments);
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000D05A0 File Offset: 0x000CE7A0
		internal virtual void VisitRelatedEntityReferenceList(IList<DbRelatedEntityRef> relatedEntityReferences)
		{
			for (int i = 0; i < relatedEntityReferences.Count; i++)
			{
				this.VisitRelatedEntityReference(relatedEntityReferences[i]);
			}
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000D05CB File Offset: 0x000CE7CB
		internal virtual void VisitRelatedEntityReference(DbRelatedEntityRef relatedEntityRef)
		{
			this.VisitExpression(relatedEntityRef.TargetEntityReference);
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000D05D9 File Offset: 0x000CE7D9
		public override void Visit(DbExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(expression, "expression");
			throw EntityUtil.NotSupported(Strings.Cqt_General_UnsupportedExpression(expression.GetType().FullName));
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000D05FC File Offset: 0x000CE7FC
		public override void Visit(DbConstantExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbConstantExpression>(expression, "expression");
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000D060A File Offset: 0x000CE80A
		public override void Visit(DbNullExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbNullExpression>(expression, "expression");
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000D0618 File Offset: 0x000CE818
		public override void Visit(DbVariableReferenceExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbVariableReferenceExpression>(expression, "expression");
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000D0626 File Offset: 0x000CE826
		public override void Visit(DbParameterReferenceExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbParameterReferenceExpression>(expression, "expression");
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000D0634 File Offset: 0x000CE834
		public override void Visit(DbFunctionExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbFunctionExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000D0650 File Offset: 0x000CE850
		public override void Visit(DbLambdaExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbLambdaExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
			this.VisitLambdaPre(expression.Lambda);
			this.VisitExpression(expression.Lambda.Body);
			this.VisitLambdaPost(expression.Lambda);
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000D069E File Offset: 0x000CE89E
		public override void Visit(DbPropertyExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbPropertyExpression>(expression, "expression");
			if (expression.Instance != null)
			{
				this.VisitExpression(expression.Instance);
			}
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000D06C0 File Offset: 0x000CE8C0
		public override void Visit(DbComparisonExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000D06C9 File Offset: 0x000CE8C9
		public override void Visit(DbLikeExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbLikeExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
			this.VisitExpression(expression.Pattern);
			this.VisitExpression(expression.Escape);
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000D06FB File Offset: 0x000CE8FB
		public override void Visit(DbLimitExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbLimitExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
			this.VisitExpression(expression.Limit);
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbIsNullExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000D072A File Offset: 0x000CE92A
		public override void Visit(DbArithmeticExpression expression)
		{
			this.VisitExpressionList(EntityUtil.CheckArgumentNull<DbArithmeticExpression>(expression, "expression").Arguments);
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000D06C0 File Offset: 0x000CE8C0
		public override void Visit(DbAndExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000D06C0 File Offset: 0x000CE8C0
		public override void Visit(DbOrExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbNotExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbDistinctExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbElementExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbIsEmptyExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x000D06C0 File Offset: 0x000CE8C0
		public override void Visit(DbUnionAllExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x000D06C0 File Offset: 0x000CE8C0
		public override void Visit(DbIntersectExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x000D06C0 File Offset: 0x000CE8C0
		public override void Visit(DbExceptExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbOfTypeExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbTreatExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbCastExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbIsOfExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000D0742 File Offset: 0x000CE942
		public override void Visit(DbCaseExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbCaseExpression>(expression, "expression");
			this.VisitExpressionList(expression.When);
			this.VisitExpressionList(expression.Then);
			this.VisitExpression(expression.Else);
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000D0774 File Offset: 0x000CE974
		public override void Visit(DbNewInstanceExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbNewInstanceExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
			if (expression.HasRelatedEntityReferences)
			{
				this.VisitRelatedEntityReferenceList(expression.RelatedEntityReferences);
			}
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbRefExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000D07A2 File Offset: 0x000CE9A2
		public override void Visit(DbRelationshipNavigationExpression expression)
		{
			this.VisitExpression(EntityUtil.CheckArgumentNull<DbRelationshipNavigationExpression>(expression, "expression").NavigationSource);
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbDerefExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbRefKeyExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000D0721 File Offset: 0x000CE921
		public override void Visit(DbEntityRefExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000D07BA File Offset: 0x000CE9BA
		public override void Visit(DbScanExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbScanExpression>(expression, "expression");
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000D07C8 File Offset: 0x000CE9C8
		public override void Visit(DbFilterExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbFilterExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Predicate);
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000D07FA File Offset: 0x000CE9FA
		public override void Visit(DbProjectExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbProjectExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Projection);
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000D082C File Offset: 0x000CEA2C
		public override void Visit(DbCrossJoinExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbCrossJoinExpression>(expression, "expression");
			foreach (DbExpressionBinding binding in expression.Inputs)
			{
				this.VisitExpressionBindingPre(binding);
			}
			foreach (DbExpressionBinding binding2 in expression.Inputs)
			{
				this.VisitExpressionBindingPost(binding2);
			}
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000D08C4 File Offset: 0x000CEAC4
		public override void Visit(DbJoinExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbJoinExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Left);
			this.VisitExpressionBindingPre(expression.Right);
			this.VisitExpression(expression.JoinCondition);
			this.VisitExpressionBindingPost(expression.Left);
			this.VisitExpressionBindingPost(expression.Right);
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000D0919 File Offset: 0x000CEB19
		public override void Visit(DbApplyExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbApplyExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			if (expression.Apply != null)
			{
				this.VisitExpression(expression.Apply.Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000D0958 File Offset: 0x000CEB58
		public override void Visit(DbGroupByExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbGroupByExpression>(expression, "expression");
			this.VisitGroupExpressionBindingPre(expression.Input);
			this.VisitExpressionList(expression.Keys);
			this.VisitGroupExpressionBindingMid(expression.Input);
			this.VisitAggregateList(expression.Aggregates);
			this.VisitGroupExpressionBindingPost(expression.Input);
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000D09B0 File Offset: 0x000CEBB0
		public override void Visit(DbSkipExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbSkipExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			foreach (DbSortClause dbSortClause in expression.SortOrder)
			{
				this.VisitExpression(dbSortClause.Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
			this.VisitExpression(expression.Count);
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000D0A34 File Offset: 0x000CEC34
		public override void Visit(DbSortExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbSortExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			for (int i = 0; i < expression.SortOrder.Count; i++)
			{
				this.VisitExpression(expression.SortOrder[i].Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000D0A92 File Offset: 0x000CEC92
		public override void Visit(DbQuantifierExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbQuantifierExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Predicate);
			this.VisitExpressionBindingPost(expression.Input);
		}
	}
}

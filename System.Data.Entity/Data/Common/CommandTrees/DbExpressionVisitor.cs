using System;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003E6 RID: 998
	public abstract class DbExpressionVisitor
	{
		// Token: 0x06003569 RID: 13673
		public abstract void Visit(DbExpression expression);

		// Token: 0x0600356A RID: 13674
		public abstract void Visit(DbAndExpression expression);

		// Token: 0x0600356B RID: 13675
		public abstract void Visit(DbApplyExpression expression);

		// Token: 0x0600356C RID: 13676
		public abstract void Visit(DbArithmeticExpression expression);

		// Token: 0x0600356D RID: 13677
		public abstract void Visit(DbCaseExpression expression);

		// Token: 0x0600356E RID: 13678
		public abstract void Visit(DbCastExpression expression);

		// Token: 0x0600356F RID: 13679
		public abstract void Visit(DbComparisonExpression expression);

		// Token: 0x06003570 RID: 13680
		public abstract void Visit(DbConstantExpression expression);

		// Token: 0x06003571 RID: 13681
		public abstract void Visit(DbCrossJoinExpression expression);

		// Token: 0x06003572 RID: 13682
		public abstract void Visit(DbDerefExpression expression);

		// Token: 0x06003573 RID: 13683
		public abstract void Visit(DbDistinctExpression expression);

		// Token: 0x06003574 RID: 13684
		public abstract void Visit(DbElementExpression expression);

		// Token: 0x06003575 RID: 13685
		public abstract void Visit(DbExceptExpression expression);

		// Token: 0x06003576 RID: 13686
		public abstract void Visit(DbFilterExpression expression);

		// Token: 0x06003577 RID: 13687
		public abstract void Visit(DbFunctionExpression expression);

		// Token: 0x06003578 RID: 13688
		public abstract void Visit(DbEntityRefExpression expression);

		// Token: 0x06003579 RID: 13689
		public abstract void Visit(DbRefKeyExpression expression);

		// Token: 0x0600357A RID: 13690
		public abstract void Visit(DbGroupByExpression expression);

		// Token: 0x0600357B RID: 13691
		public abstract void Visit(DbIntersectExpression expression);

		// Token: 0x0600357C RID: 13692
		public abstract void Visit(DbIsEmptyExpression expression);

		// Token: 0x0600357D RID: 13693
		public abstract void Visit(DbIsNullExpression expression);

		// Token: 0x0600357E RID: 13694
		public abstract void Visit(DbIsOfExpression expression);

		// Token: 0x0600357F RID: 13695
		public abstract void Visit(DbJoinExpression expression);

		// Token: 0x06003580 RID: 13696 RVA: 0x00013A81 File Offset: 0x00011C81
		public virtual void Visit(DbLambdaExpression expression)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06003581 RID: 13697
		public abstract void Visit(DbLikeExpression expression);

		// Token: 0x06003582 RID: 13698
		public abstract void Visit(DbLimitExpression expression);

		// Token: 0x06003583 RID: 13699
		public abstract void Visit(DbNewInstanceExpression expression);

		// Token: 0x06003584 RID: 13700
		public abstract void Visit(DbNotExpression expression);

		// Token: 0x06003585 RID: 13701
		public abstract void Visit(DbNullExpression expression);

		// Token: 0x06003586 RID: 13702
		public abstract void Visit(DbOfTypeExpression expression);

		// Token: 0x06003587 RID: 13703
		public abstract void Visit(DbOrExpression expression);

		// Token: 0x06003588 RID: 13704
		public abstract void Visit(DbParameterReferenceExpression expression);

		// Token: 0x06003589 RID: 13705
		public abstract void Visit(DbProjectExpression expression);

		// Token: 0x0600358A RID: 13706
		public abstract void Visit(DbPropertyExpression expression);

		// Token: 0x0600358B RID: 13707
		public abstract void Visit(DbQuantifierExpression expression);

		// Token: 0x0600358C RID: 13708
		public abstract void Visit(DbRefExpression expression);

		// Token: 0x0600358D RID: 13709
		public abstract void Visit(DbRelationshipNavigationExpression expression);

		// Token: 0x0600358E RID: 13710
		public abstract void Visit(DbScanExpression expression);

		// Token: 0x0600358F RID: 13711
		public abstract void Visit(DbSkipExpression expression);

		// Token: 0x06003590 RID: 13712
		public abstract void Visit(DbSortExpression expression);

		// Token: 0x06003591 RID: 13713
		public abstract void Visit(DbTreatExpression expression);

		// Token: 0x06003592 RID: 13714
		public abstract void Visit(DbUnionAllExpression expression);

		// Token: 0x06003593 RID: 13715
		public abstract void Visit(DbVariableReferenceExpression expression);
	}
}

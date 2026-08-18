using System;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003E7 RID: 999
	public abstract class DbExpressionVisitor<TResultType>
	{
		// Token: 0x06003595 RID: 13717
		public abstract TResultType Visit(DbExpression expression);

		// Token: 0x06003596 RID: 13718
		public abstract TResultType Visit(DbAndExpression expression);

		// Token: 0x06003597 RID: 13719
		public abstract TResultType Visit(DbApplyExpression expression);

		// Token: 0x06003598 RID: 13720
		public abstract TResultType Visit(DbArithmeticExpression expression);

		// Token: 0x06003599 RID: 13721
		public abstract TResultType Visit(DbCaseExpression expression);

		// Token: 0x0600359A RID: 13722
		public abstract TResultType Visit(DbCastExpression expression);

		// Token: 0x0600359B RID: 13723
		public abstract TResultType Visit(DbComparisonExpression expression);

		// Token: 0x0600359C RID: 13724
		public abstract TResultType Visit(DbConstantExpression expression);

		// Token: 0x0600359D RID: 13725
		public abstract TResultType Visit(DbCrossJoinExpression expression);

		// Token: 0x0600359E RID: 13726
		public abstract TResultType Visit(DbDerefExpression expression);

		// Token: 0x0600359F RID: 13727
		public abstract TResultType Visit(DbDistinctExpression expression);

		// Token: 0x060035A0 RID: 13728
		public abstract TResultType Visit(DbElementExpression expression);

		// Token: 0x060035A1 RID: 13729
		public abstract TResultType Visit(DbExceptExpression expression);

		// Token: 0x060035A2 RID: 13730
		public abstract TResultType Visit(DbFilterExpression expression);

		// Token: 0x060035A3 RID: 13731
		public abstract TResultType Visit(DbFunctionExpression expression);

		// Token: 0x060035A4 RID: 13732
		public abstract TResultType Visit(DbEntityRefExpression expression);

		// Token: 0x060035A5 RID: 13733
		public abstract TResultType Visit(DbRefKeyExpression expression);

		// Token: 0x060035A6 RID: 13734
		public abstract TResultType Visit(DbGroupByExpression expression);

		// Token: 0x060035A7 RID: 13735
		public abstract TResultType Visit(DbIntersectExpression expression);

		// Token: 0x060035A8 RID: 13736
		public abstract TResultType Visit(DbIsEmptyExpression expression);

		// Token: 0x060035A9 RID: 13737
		public abstract TResultType Visit(DbIsNullExpression expression);

		// Token: 0x060035AA RID: 13738
		public abstract TResultType Visit(DbIsOfExpression expression);

		// Token: 0x060035AB RID: 13739
		public abstract TResultType Visit(DbJoinExpression expression);

		// Token: 0x060035AC RID: 13740 RVA: 0x00013A81 File Offset: 0x00011C81
		public virtual TResultType Visit(DbLambdaExpression expression)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x060035AD RID: 13741
		public abstract TResultType Visit(DbLikeExpression expression);

		// Token: 0x060035AE RID: 13742
		public abstract TResultType Visit(DbLimitExpression expression);

		// Token: 0x060035AF RID: 13743
		public abstract TResultType Visit(DbNewInstanceExpression expression);

		// Token: 0x060035B0 RID: 13744
		public abstract TResultType Visit(DbNotExpression expression);

		// Token: 0x060035B1 RID: 13745
		public abstract TResultType Visit(DbNullExpression expression);

		// Token: 0x060035B2 RID: 13746
		public abstract TResultType Visit(DbOfTypeExpression expression);

		// Token: 0x060035B3 RID: 13747
		public abstract TResultType Visit(DbOrExpression expression);

		// Token: 0x060035B4 RID: 13748
		public abstract TResultType Visit(DbParameterReferenceExpression expression);

		// Token: 0x060035B5 RID: 13749
		public abstract TResultType Visit(DbProjectExpression expression);

		// Token: 0x060035B6 RID: 13750
		public abstract TResultType Visit(DbPropertyExpression expression);

		// Token: 0x060035B7 RID: 13751
		public abstract TResultType Visit(DbQuantifierExpression expression);

		// Token: 0x060035B8 RID: 13752
		public abstract TResultType Visit(DbRefExpression expression);

		// Token: 0x060035B9 RID: 13753
		public abstract TResultType Visit(DbRelationshipNavigationExpression expression);

		// Token: 0x060035BA RID: 13754
		public abstract TResultType Visit(DbScanExpression expression);

		// Token: 0x060035BB RID: 13755
		public abstract TResultType Visit(DbSortExpression expression);

		// Token: 0x060035BC RID: 13756
		public abstract TResultType Visit(DbSkipExpression expression);

		// Token: 0x060035BD RID: 13757
		public abstract TResultType Visit(DbTreatExpression expression);

		// Token: 0x060035BE RID: 13758
		public abstract TResultType Visit(DbUnionAllExpression expression);

		// Token: 0x060035BF RID: 13759
		public abstract TResultType Visit(DbVariableReferenceExpression expression);
	}
}

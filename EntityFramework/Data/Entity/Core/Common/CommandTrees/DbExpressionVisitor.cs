using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000031 RID: 49
	public abstract class DbExpressionVisitor<TResultType>
	{
		// Token: 0x0600025A RID: 602
		public abstract TResultType Visit(DbExpression expression);

		// Token: 0x0600025B RID: 603
		public abstract TResultType Visit(DbAndExpression expression);

		// Token: 0x0600025C RID: 604
		public abstract TResultType Visit(DbApplyExpression expression);

		// Token: 0x0600025D RID: 605
		public abstract TResultType Visit(DbArithmeticExpression expression);

		// Token: 0x0600025E RID: 606
		public abstract TResultType Visit(DbCaseExpression expression);

		// Token: 0x0600025F RID: 607
		public abstract TResultType Visit(DbCastExpression expression);

		// Token: 0x06000260 RID: 608
		public abstract TResultType Visit(DbComparisonExpression expression);

		// Token: 0x06000261 RID: 609
		public abstract TResultType Visit(DbConstantExpression expression);

		// Token: 0x06000262 RID: 610
		public abstract TResultType Visit(DbCrossJoinExpression expression);

		// Token: 0x06000263 RID: 611
		public abstract TResultType Visit(DbDerefExpression expression);

		// Token: 0x06000264 RID: 612
		public abstract TResultType Visit(DbDistinctExpression expression);

		// Token: 0x06000265 RID: 613
		public abstract TResultType Visit(DbElementExpression expression);

		// Token: 0x06000266 RID: 614
		public abstract TResultType Visit(DbExceptExpression expression);

		// Token: 0x06000267 RID: 615
		public abstract TResultType Visit(DbFilterExpression expression);

		// Token: 0x06000268 RID: 616
		public abstract TResultType Visit(DbFunctionExpression expression);

		// Token: 0x06000269 RID: 617
		public abstract TResultType Visit(DbEntityRefExpression expression);

		// Token: 0x0600026A RID: 618
		public abstract TResultType Visit(DbRefKeyExpression expression);

		// Token: 0x0600026B RID: 619
		public abstract TResultType Visit(DbGroupByExpression expression);

		// Token: 0x0600026C RID: 620
		public abstract TResultType Visit(DbIntersectExpression expression);

		// Token: 0x0600026D RID: 621
		public abstract TResultType Visit(DbIsEmptyExpression expression);

		// Token: 0x0600026E RID: 622
		public abstract TResultType Visit(DbIsNullExpression expression);

		// Token: 0x0600026F RID: 623
		public abstract TResultType Visit(DbIsOfExpression expression);

		// Token: 0x06000270 RID: 624
		public abstract TResultType Visit(DbJoinExpression expression);

		// Token: 0x06000271 RID: 625 RVA: 0x0000E565 File Offset: 0x0000C765
		public virtual TResultType Visit(DbLambdaExpression expression)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000272 RID: 626
		public abstract TResultType Visit(DbLikeExpression expression);

		// Token: 0x06000273 RID: 627
		public abstract TResultType Visit(DbLimitExpression expression);

		// Token: 0x06000274 RID: 628
		public abstract TResultType Visit(DbNewInstanceExpression expression);

		// Token: 0x06000275 RID: 629
		public abstract TResultType Visit(DbNotExpression expression);

		// Token: 0x06000276 RID: 630
		public abstract TResultType Visit(DbNullExpression expression);

		// Token: 0x06000277 RID: 631
		public abstract TResultType Visit(DbOfTypeExpression expression);

		// Token: 0x06000278 RID: 632
		public abstract TResultType Visit(DbOrExpression expression);

		// Token: 0x06000279 RID: 633
		public abstract TResultType Visit(DbParameterReferenceExpression expression);

		// Token: 0x0600027A RID: 634
		public abstract TResultType Visit(DbProjectExpression expression);

		// Token: 0x0600027B RID: 635
		public abstract TResultType Visit(DbPropertyExpression expression);

		// Token: 0x0600027C RID: 636
		public abstract TResultType Visit(DbQuantifierExpression expression);

		// Token: 0x0600027D RID: 637
		public abstract TResultType Visit(DbRefExpression expression);

		// Token: 0x0600027E RID: 638
		public abstract TResultType Visit(DbRelationshipNavigationExpression expression);

		// Token: 0x0600027F RID: 639
		public abstract TResultType Visit(DbScanExpression expression);

		// Token: 0x06000280 RID: 640
		public abstract TResultType Visit(DbSortExpression expression);

		// Token: 0x06000281 RID: 641
		public abstract TResultType Visit(DbSkipExpression expression);

		// Token: 0x06000282 RID: 642
		public abstract TResultType Visit(DbTreatExpression expression);

		// Token: 0x06000283 RID: 643
		public abstract TResultType Visit(DbUnionAllExpression expression);

		// Token: 0x06000284 RID: 644
		public abstract TResultType Visit(DbVariableReferenceExpression expression);

		// Token: 0x06000285 RID: 645 RVA: 0x0000E56C File Offset: 0x0000C76C
		public virtual TResultType Visit(DbInExpression expression)
		{
			throw new NotImplementedException(Strings.VisitDbInExpressionNotImplemented);
		}
	}
}

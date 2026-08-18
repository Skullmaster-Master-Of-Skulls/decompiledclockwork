using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x0200010B RID: 267
	public abstract class DbExpressionVisitor
	{
		// Token: 0x06000699 RID: 1689
		public abstract void Visit(DbExpression expression);

		// Token: 0x0600069A RID: 1690
		public abstract void Visit(DbAndExpression expression);

		// Token: 0x0600069B RID: 1691
		public abstract void Visit(DbApplyExpression expression);

		// Token: 0x0600069C RID: 1692
		public abstract void Visit(DbArithmeticExpression expression);

		// Token: 0x0600069D RID: 1693
		public abstract void Visit(DbCaseExpression expression);

		// Token: 0x0600069E RID: 1694
		public abstract void Visit(DbCastExpression expression);

		// Token: 0x0600069F RID: 1695
		public abstract void Visit(DbComparisonExpression expression);

		// Token: 0x060006A0 RID: 1696
		public abstract void Visit(DbConstantExpression expression);

		// Token: 0x060006A1 RID: 1697
		public abstract void Visit(DbCrossJoinExpression expression);

		// Token: 0x060006A2 RID: 1698
		public abstract void Visit(DbDerefExpression expression);

		// Token: 0x060006A3 RID: 1699
		public abstract void Visit(DbDistinctExpression expression);

		// Token: 0x060006A4 RID: 1700
		public abstract void Visit(DbElementExpression expression);

		// Token: 0x060006A5 RID: 1701
		public abstract void Visit(DbExceptExpression expression);

		// Token: 0x060006A6 RID: 1702
		public abstract void Visit(DbFilterExpression expression);

		// Token: 0x060006A7 RID: 1703
		public abstract void Visit(DbFunctionExpression expression);

		// Token: 0x060006A8 RID: 1704
		public abstract void Visit(DbEntityRefExpression expression);

		// Token: 0x060006A9 RID: 1705
		public abstract void Visit(DbRefKeyExpression expression);

		// Token: 0x060006AA RID: 1706
		public abstract void Visit(DbGroupByExpression expression);

		// Token: 0x060006AB RID: 1707
		public abstract void Visit(DbIntersectExpression expression);

		// Token: 0x060006AC RID: 1708
		public abstract void Visit(DbIsEmptyExpression expression);

		// Token: 0x060006AD RID: 1709
		public abstract void Visit(DbIsNullExpression expression);

		// Token: 0x060006AE RID: 1710
		public abstract void Visit(DbIsOfExpression expression);

		// Token: 0x060006AF RID: 1711
		public abstract void Visit(DbJoinExpression expression);

		// Token: 0x060006B0 RID: 1712 RVA: 0x000261AE File Offset: 0x000243AE
		public virtual void Visit(DbLambdaExpression expression)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006B1 RID: 1713
		public abstract void Visit(DbLikeExpression expression);

		// Token: 0x060006B2 RID: 1714
		public abstract void Visit(DbLimitExpression expression);

		// Token: 0x060006B3 RID: 1715
		public abstract void Visit(DbNewInstanceExpression expression);

		// Token: 0x060006B4 RID: 1716
		public abstract void Visit(DbNotExpression expression);

		// Token: 0x060006B5 RID: 1717
		public abstract void Visit(DbNullExpression expression);

		// Token: 0x060006B6 RID: 1718
		public abstract void Visit(DbOfTypeExpression expression);

		// Token: 0x060006B7 RID: 1719
		public abstract void Visit(DbOrExpression expression);

		// Token: 0x060006B8 RID: 1720
		public abstract void Visit(DbParameterReferenceExpression expression);

		// Token: 0x060006B9 RID: 1721
		public abstract void Visit(DbProjectExpression expression);

		// Token: 0x060006BA RID: 1722
		public abstract void Visit(DbPropertyExpression expression);

		// Token: 0x060006BB RID: 1723
		public abstract void Visit(DbQuantifierExpression expression);

		// Token: 0x060006BC RID: 1724
		public abstract void Visit(DbRefExpression expression);

		// Token: 0x060006BD RID: 1725
		public abstract void Visit(DbRelationshipNavigationExpression expression);

		// Token: 0x060006BE RID: 1726
		public abstract void Visit(DbScanExpression expression);

		// Token: 0x060006BF RID: 1727
		public abstract void Visit(DbSkipExpression expression);

		// Token: 0x060006C0 RID: 1728
		public abstract void Visit(DbSortExpression expression);

		// Token: 0x060006C1 RID: 1729
		public abstract void Visit(DbTreatExpression expression);

		// Token: 0x060006C2 RID: 1730
		public abstract void Visit(DbUnionAllExpression expression);

		// Token: 0x060006C3 RID: 1731
		public abstract void Visit(DbVariableReferenceExpression expression);

		// Token: 0x060006C4 RID: 1732 RVA: 0x000261B5 File Offset: 0x000243B5
		public virtual void Visit(DbInExpression expression)
		{
			throw new NotImplementedException(Strings.VisitDbInExpressionNotImplemented);
		}
	}
}

using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003FD RID: 1021
	internal abstract class UpdateExpressionVisitor<TReturn> : DbExpressionVisitor<TReturn>
	{
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600258B RID: 9611
		protected abstract string VisitorName { get; }

		// Token: 0x0600258C RID: 9612 RVA: 0x000B34CC File Offset: 0x000B16CC
		protected NotSupportedException ConstructNotSupportedException(DbExpression node)
		{
			string p = (node == null) ? null : node.ExpressionKind.ToString();
			return new NotSupportedException(Strings.Update_UnsupportedExpressionKind(p, this.VisitorName));
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000B3501 File Offset: 0x000B1701
		public override TReturn Visit(DbExpression expression)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			if (expression != null)
			{
				return expression.Accept<TReturn>(this);
			}
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000B3521 File Offset: 0x000B1721
		public override TReturn Visit(DbAndExpression expression)
		{
			Check.NotNull<DbAndExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000B3536 File Offset: 0x000B1736
		public override TReturn Visit(DbApplyExpression expression)
		{
			Check.NotNull<DbApplyExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000B354B File Offset: 0x000B174B
		public override TReturn Visit(DbArithmeticExpression expression)
		{
			Check.NotNull<DbArithmeticExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000B3560 File Offset: 0x000B1760
		public override TReturn Visit(DbCaseExpression expression)
		{
			Check.NotNull<DbCaseExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000B3575 File Offset: 0x000B1775
		public override TReturn Visit(DbCastExpression expression)
		{
			Check.NotNull<DbCastExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000B358A File Offset: 0x000B178A
		public override TReturn Visit(DbComparisonExpression expression)
		{
			Check.NotNull<DbComparisonExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000B359F File Offset: 0x000B179F
		public override TReturn Visit(DbConstantExpression expression)
		{
			Check.NotNull<DbConstantExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000B35B4 File Offset: 0x000B17B4
		public override TReturn Visit(DbCrossJoinExpression expression)
		{
			Check.NotNull<DbCrossJoinExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000B35C9 File Offset: 0x000B17C9
		public override TReturn Visit(DbDerefExpression expression)
		{
			Check.NotNull<DbDerefExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000B35DE File Offset: 0x000B17DE
		public override TReturn Visit(DbDistinctExpression expression)
		{
			Check.NotNull<DbDistinctExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000B35F3 File Offset: 0x000B17F3
		public override TReturn Visit(DbElementExpression expression)
		{
			Check.NotNull<DbElementExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000B3608 File Offset: 0x000B1808
		public override TReturn Visit(DbExceptExpression expression)
		{
			Check.NotNull<DbExceptExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000B361D File Offset: 0x000B181D
		public override TReturn Visit(DbFilterExpression expression)
		{
			Check.NotNull<DbFilterExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000B3632 File Offset: 0x000B1832
		public override TReturn Visit(DbFunctionExpression expression)
		{
			Check.NotNull<DbFunctionExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000B3647 File Offset: 0x000B1847
		public override TReturn Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000B365C File Offset: 0x000B185C
		public override TReturn Visit(DbEntityRefExpression expression)
		{
			Check.NotNull<DbEntityRefExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000B3671 File Offset: 0x000B1871
		public override TReturn Visit(DbRefKeyExpression expression)
		{
			Check.NotNull<DbRefKeyExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000B3686 File Offset: 0x000B1886
		public override TReturn Visit(DbGroupByExpression expression)
		{
			Check.NotNull<DbGroupByExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000B369B File Offset: 0x000B189B
		public override TReturn Visit(DbIntersectExpression expression)
		{
			Check.NotNull<DbIntersectExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000B36B0 File Offset: 0x000B18B0
		public override TReturn Visit(DbIsEmptyExpression expression)
		{
			Check.NotNull<DbIsEmptyExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000B36C5 File Offset: 0x000B18C5
		public override TReturn Visit(DbIsNullExpression expression)
		{
			Check.NotNull<DbIsNullExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000B36DA File Offset: 0x000B18DA
		public override TReturn Visit(DbIsOfExpression expression)
		{
			Check.NotNull<DbIsOfExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000B36EF File Offset: 0x000B18EF
		public override TReturn Visit(DbJoinExpression expression)
		{
			Check.NotNull<DbJoinExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000B3704 File Offset: 0x000B1904
		public override TReturn Visit(DbLikeExpression expression)
		{
			Check.NotNull<DbLikeExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000B3719 File Offset: 0x000B1919
		public override TReturn Visit(DbLimitExpression expression)
		{
			Check.NotNull<DbLimitExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000B372E File Offset: 0x000B192E
		public override TReturn Visit(DbNewInstanceExpression expression)
		{
			Check.NotNull<DbNewInstanceExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000B3743 File Offset: 0x000B1943
		public override TReturn Visit(DbNotExpression expression)
		{
			Check.NotNull<DbNotExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000B3758 File Offset: 0x000B1958
		public override TReturn Visit(DbNullExpression expression)
		{
			Check.NotNull<DbNullExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000B376D File Offset: 0x000B196D
		public override TReturn Visit(DbOfTypeExpression expression)
		{
			Check.NotNull<DbOfTypeExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000B3782 File Offset: 0x000B1982
		public override TReturn Visit(DbOrExpression expression)
		{
			Check.NotNull<DbOrExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000B3797 File Offset: 0x000B1997
		public override TReturn Visit(DbInExpression expression)
		{
			Check.NotNull<DbInExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000B37AC File Offset: 0x000B19AC
		public override TReturn Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000B37C1 File Offset: 0x000B19C1
		public override TReturn Visit(DbProjectExpression expression)
		{
			Check.NotNull<DbProjectExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000B37D6 File Offset: 0x000B19D6
		public override TReturn Visit(DbPropertyExpression expression)
		{
			Check.NotNull<DbPropertyExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x000B37EB File Offset: 0x000B19EB
		public override TReturn Visit(DbQuantifierExpression expression)
		{
			Check.NotNull<DbQuantifierExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x000B3800 File Offset: 0x000B1A00
		public override TReturn Visit(DbRefExpression expression)
		{
			Check.NotNull<DbRefExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000B3815 File Offset: 0x000B1A15
		public override TReturn Visit(DbRelationshipNavigationExpression expression)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000B382A File Offset: 0x000B1A2A
		public override TReturn Visit(DbSkipExpression expression)
		{
			Check.NotNull<DbSkipExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000B383F File Offset: 0x000B1A3F
		public override TReturn Visit(DbSortExpression expression)
		{
			Check.NotNull<DbSortExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000B3854 File Offset: 0x000B1A54
		public override TReturn Visit(DbTreatExpression expression)
		{
			Check.NotNull<DbTreatExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000B3869 File Offset: 0x000B1A69
		public override TReturn Visit(DbUnionAllExpression expression)
		{
			Check.NotNull<DbUnionAllExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000B387E File Offset: 0x000B1A7E
		public override TReturn Visit(DbVariableReferenceExpression expression)
		{
			Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000B3893 File Offset: 0x000B1A93
		public override TReturn Visit(DbScanExpression expression)
		{
			Check.NotNull<DbScanExpression>(expression, "expression");
			throw this.ConstructNotSupportedException(expression);
		}
	}
}

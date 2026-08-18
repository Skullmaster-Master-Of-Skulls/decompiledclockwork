using System;
using System.Data.Common.CommandTrees;
using System.Data.Entity;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002D3 RID: 723
	internal abstract class UpdateExpressionVisitor<TReturn> : DbExpressionVisitor<TReturn>
	{
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002A72 RID: 10866
		protected abstract string VisitorName { get; }

		// Token: 0x06002A73 RID: 10867 RVA: 0x000A6EB4 File Offset: 0x000A50B4
		protected NotSupportedException ConstructNotSupportedException(DbExpression node)
		{
			string p = (node == null) ? null : node.ExpressionKind.ToString();
			return EntityUtil.NotSupported(Strings.Update_UnsupportedExpressionKind(p, this.VisitorName));
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000A6EED File Offset: 0x000A50ED
		public override TReturn Visit(DbExpression expression)
		{
			if (expression != null)
			{
				return expression.Accept<TReturn>(this);
			}
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbAndExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbApplyExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbArithmeticExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbCaseExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbCastExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbComparisonExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbConstantExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbCrossJoinExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbDerefExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbDistinctExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbElementExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbExceptExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbFilterExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbFunctionExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbLambdaExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbEntityRefExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbRefKeyExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbGroupByExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbIntersectExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbIsEmptyExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbIsNullExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbIsOfExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbJoinExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbLikeExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbLimitExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbNewInstanceExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbNotExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbNullExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbOfTypeExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbOrExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbParameterReferenceExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbProjectExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbPropertyExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbQuantifierExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbRefExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbRelationshipNavigationExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbSkipExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbSortExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbTreatExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbUnionAllExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbVariableReferenceExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000A6F01 File Offset: 0x000A5101
		public override TReturn Visit(DbScanExpression expression)
		{
			throw this.ConstructNotSupportedException(expression);
		}
	}
}

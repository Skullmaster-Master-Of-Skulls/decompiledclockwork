using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Metadata.Edm;

namespace Oracle.DataAccess.Client.SqlGen
{
	// Token: 0x02000052 RID: 82
	internal abstract class BasicExpressionVisitor : DbExpressionVisitor
	{
		// Token: 0x060003AB RID: 939 RVA: 0x00029EB4 File Offset: 0x00028EB4
		protected virtual void VisitUnaryExpression(DbUnaryExpression expression)
		{
			this.VisitExpression(EntityUtils.CheckArgumentNull<DbUnaryExpression>(expression, "expression").Argument);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00029ECC File Offset: 0x00028ECC
		protected virtual void VisitBinaryExpression(DbBinaryExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbBinaryExpression>(expression, "expression");
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00029EF2 File Offset: 0x00028EF2
		protected virtual void VisitExpressionBindingPre(DbExpressionBinding binding)
		{
			EntityUtils.CheckArgumentNull<DbExpressionBinding>(binding, "binding");
			this.VisitExpression(binding.Expression);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00029F0C File Offset: 0x00028F0C
		protected virtual void VisitExpressionBindingPost(DbExpressionBinding binding)
		{
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00029F0E File Offset: 0x00028F0E
		protected virtual void VisitGroupExpressionBindingPre(DbGroupExpressionBinding binding)
		{
			EntityUtils.CheckArgumentNull<DbGroupExpressionBinding>(binding, "binding");
			this.VisitExpression(binding.Expression);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00029F28 File Offset: 0x00028F28
		protected virtual void VisitGroupExpressionBindingMid(DbGroupExpressionBinding binding)
		{
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00029F2A File Offset: 0x00028F2A
		protected virtual void VisitGroupExpressionBindingPost(DbGroupExpressionBinding binding)
		{
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00029F2C File Offset: 0x00028F2C
		protected virtual void VisitLambdaFunctionPre(EdmFunction function, DbExpression body)
		{
			EntityUtils.CheckArgumentNull<EdmFunction>(function, "function");
			EntityUtils.CheckArgumentNull<DbExpression>(body, "body");
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00029F46 File Offset: 0x00028F46
		protected virtual void VisitLambdaFunctionPost(EdmFunction function, DbExpression body)
		{
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00029F48 File Offset: 0x00028F48
		public virtual void VisitExpression(DbExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbExpression>(expression, "expression").Accept(this);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00029F5C File Offset: 0x00028F5C
		public virtual void VisitExpressionList(IList<DbExpression> expressionList)
		{
			EntityUtils.CheckArgumentNull<IList<DbExpression>>(expressionList, "expressionList");
			for (int i = 0; i < expressionList.Count; i++)
			{
				this.VisitExpression(expressionList[i]);
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00029F94 File Offset: 0x00028F94
		public virtual void VisitAggregateList(IList<DbAggregate> aggregates)
		{
			EntityUtils.CheckArgumentNull<IList<DbAggregate>>(aggregates, "aggregates");
			for (int i = 0; i < aggregates.Count; i++)
			{
				this.VisitAggregate(aggregates[i]);
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00029FCB File Offset: 0x00028FCB
		public virtual void VisitAggregate(DbAggregate aggregate)
		{
			this.VisitExpressionList(EntityUtils.CheckArgumentNull<DbAggregate>(aggregate, "aggregate").Arguments);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00029FE4 File Offset: 0x00028FE4
		public override void Visit(DbExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbExpression>(expression, "expression");
			throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_NOT_SUPPORTED, new string[]
			{
				"Oracle Data Provider for .NET",
				expression.GetType().FullName
			}));
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0002A02A File Offset: 0x0002902A
		public override void Visit(DbConstantExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbConstantExpression>(expression, "expression");
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0002A038 File Offset: 0x00029038
		public override void Visit(DbNullExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbNullExpression>(expression, "expression");
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0002A046 File Offset: 0x00029046
		public override void Visit(DbVariableReferenceExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbVariableReferenceExpression>(expression, "expression");
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0002A054 File Offset: 0x00029054
		public override void Visit(DbParameterReferenceExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbParameterReferenceExpression>(expression, "expression");
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0002A062 File Offset: 0x00029062
		public override void Visit(DbFunctionExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbFunctionExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0002A07C File Offset: 0x0002907C
		public override void Visit(DbPropertyExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbPropertyExpression>(expression, "expression");
			if (expression.Instance != null)
			{
				this.VisitExpression(expression.Instance);
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0002A09E File Offset: 0x0002909E
		public override void Visit(DbComparisonExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0002A0A7 File Offset: 0x000290A7
		public override void Visit(DbLikeExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbLikeExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
			this.VisitExpression(expression.Pattern);
			this.VisitExpression(expression.Escape);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0002A0D9 File Offset: 0x000290D9
		public override void Visit(DbLimitExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbLimitExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
			this.VisitExpression(expression.Limit);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0002A0FF File Offset: 0x000290FF
		public override void Visit(DbIsNullExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0002A108 File Offset: 0x00029108
		public override void Visit(DbArithmeticExpression expression)
		{
			this.VisitExpressionList(EntityUtils.CheckArgumentNull<DbArithmeticExpression>(expression, "expression").Arguments);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0002A120 File Offset: 0x00029120
		public override void Visit(DbAndExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0002A129 File Offset: 0x00029129
		public override void Visit(DbOrExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0002A132 File Offset: 0x00029132
		public override void Visit(DbNotExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0002A13B File Offset: 0x0002913B
		public override void Visit(DbDistinctExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0002A144 File Offset: 0x00029144
		public override void Visit(DbElementExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0002A14D File Offset: 0x0002914D
		public override void Visit(DbIsEmptyExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0002A156 File Offset: 0x00029156
		public override void Visit(DbUnionAllExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0002A15F File Offset: 0x0002915F
		public override void Visit(DbIntersectExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0002A168 File Offset: 0x00029168
		public override void Visit(DbExceptExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0002A171 File Offset: 0x00029171
		public override void Visit(DbOfTypeExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0002A17A File Offset: 0x0002917A
		public override void Visit(DbTreatExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0002A183 File Offset: 0x00029183
		public override void Visit(DbCastExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0002A18C File Offset: 0x0002918C
		public override void Visit(DbIsOfExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0002A195 File Offset: 0x00029195
		public override void Visit(DbCaseExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbCaseExpression>(expression, "expression");
			this.VisitExpressionList(expression.When);
			this.VisitExpressionList(expression.Then);
			this.VisitExpression(expression.Else);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0002A1C7 File Offset: 0x000291C7
		public override void Visit(DbRefExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0002A1D0 File Offset: 0x000291D0
		public override void Visit(DbRelationshipNavigationExpression expression)
		{
			this.VisitExpression(EntityUtils.CheckArgumentNull<DbRelationshipNavigationExpression>(expression, "expression").NavigationSource);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0002A1E8 File Offset: 0x000291E8
		public override void Visit(DbDerefExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0002A1F1 File Offset: 0x000291F1
		public override void Visit(DbRefKeyExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0002A1FA File Offset: 0x000291FA
		public override void Visit(DbEntityRefExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0002A203 File Offset: 0x00029203
		public override void Visit(DbScanExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbScanExpression>(expression, "expression");
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0002A211 File Offset: 0x00029211
		public override void Visit(DbFilterExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbFilterExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Predicate);
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0002A243 File Offset: 0x00029243
		public override void Visit(DbProjectExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbProjectExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Projection);
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0002A278 File Offset: 0x00029278
		public override void Visit(DbCrossJoinExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbCrossJoinExpression>(expression, "expression");
			foreach (DbExpressionBinding binding in expression.Inputs)
			{
				this.VisitExpressionBindingPre(binding);
			}
			foreach (DbExpressionBinding binding2 in expression.Inputs)
			{
				this.VisitExpressionBindingPost(binding2);
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0002A310 File Offset: 0x00029310
		public override void Visit(DbJoinExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbJoinExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Left);
			this.VisitExpressionBindingPre(expression.Right);
			this.VisitExpression(expression.JoinCondition);
			this.VisitExpressionBindingPost(expression.Left);
			this.VisitExpressionBindingPost(expression.Right);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0002A365 File Offset: 0x00029365
		public override void Visit(DbApplyExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbApplyExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			if (expression.Apply != null)
			{
				this.VisitExpression(expression.Apply.Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0002A3A4 File Offset: 0x000293A4
		public override void Visit(DbGroupByExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbGroupByExpression>(expression, "expression");
			this.VisitGroupExpressionBindingPre(expression.Input);
			this.VisitExpressionList(expression.Keys);
			this.VisitGroupExpressionBindingMid(expression.Input);
			this.VisitAggregateList(expression.Aggregates);
			this.VisitGroupExpressionBindingPost(expression.Input);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0002A3FC File Offset: 0x000293FC
		public override void Visit(DbSkipExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbSkipExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			foreach (DbSortClause dbSortClause in expression.SortOrder)
			{
				this.VisitExpression(dbSortClause.Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
			this.VisitExpression(expression.Count);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0002A480 File Offset: 0x00029480
		public override void Visit(DbSortExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbSortExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			for (int i = 0; i < expression.SortOrder.Count; i++)
			{
				this.VisitExpression(expression.SortOrder[i].Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0002A4DE File Offset: 0x000294DE
		public override void Visit(DbQuantifierExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbQuantifierExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Predicate);
			this.VisitExpressionBindingPost(expression.Input);
		}
	}
}

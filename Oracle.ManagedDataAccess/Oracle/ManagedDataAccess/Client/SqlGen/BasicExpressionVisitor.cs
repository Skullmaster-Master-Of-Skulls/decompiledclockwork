using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Metadata.Edm;
using OracleInternal.EntityFramework;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000EB RID: 235
	internal abstract class BasicExpressionVisitor : DbExpressionVisitor
	{
		// Token: 0x06000946 RID: 2374 RVA: 0x0006CEB8 File Offset: 0x0006B0B8
		protected virtual void VisitUnaryExpression(DbUnaryExpression expression)
		{
			this.VisitExpression(EntityUtils.CheckArgumentNull<DbUnaryExpression>(expression, "expression").Argument);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0006CED0 File Offset: 0x0006B0D0
		protected virtual void VisitBinaryExpression(DbBinaryExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbBinaryExpression>(expression, "expression");
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0006CEF8 File Offset: 0x0006B0F8
		protected virtual void VisitExpressionBindingPre(DbExpressionBinding binding)
		{
			EntityUtils.CheckArgumentNull<DbExpressionBinding>(binding, "binding");
			this.VisitExpression(binding.Expression);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0006CF14 File Offset: 0x0006B114
		protected virtual void VisitExpressionBindingPost(DbExpressionBinding binding)
		{
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0006CF18 File Offset: 0x0006B118
		protected virtual void VisitGroupExpressionBindingPre(DbGroupExpressionBinding binding)
		{
			EntityUtils.CheckArgumentNull<DbGroupExpressionBinding>(binding, "binding");
			this.VisitExpression(binding.Expression);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0006CF34 File Offset: 0x0006B134
		protected virtual void VisitGroupExpressionBindingMid(DbGroupExpressionBinding binding)
		{
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0006CF38 File Offset: 0x0006B138
		protected virtual void VisitGroupExpressionBindingPost(DbGroupExpressionBinding binding)
		{
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0006CF3C File Offset: 0x0006B13C
		protected virtual void VisitLambdaFunctionPre(EdmFunction function, DbExpression body)
		{
			EntityUtils.CheckArgumentNull<EdmFunction>(function, "function");
			EntityUtils.CheckArgumentNull<DbExpression>(body, "body");
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0006CF58 File Offset: 0x0006B158
		protected virtual void VisitLambdaFunctionPost(EdmFunction function, DbExpression body)
		{
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0006CF5C File Offset: 0x0006B15C
		public virtual void VisitExpression(DbExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbExpression>(expression, "expression").Accept(this);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual void VisitExpressionList(IList<DbExpression> expressionList)
		{
			EntityUtils.CheckArgumentNull<IList<DbExpression>>(expressionList, "expressionList");
			for (int i = 0; i < expressionList.Count; i++)
			{
				this.VisitExpression(expressionList[i]);
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0006CFA8 File Offset: 0x0006B1A8
		public virtual void VisitAggregateList(IList<DbAggregate> aggregates)
		{
			EntityUtils.CheckArgumentNull<IList<DbAggregate>>(aggregates, "aggregates");
			for (int i = 0; i < aggregates.Count; i++)
			{
				this.VisitAggregate(aggregates[i]);
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0006CFE0 File Offset: 0x0006B1E0
		public virtual void VisitAggregate(DbAggregate aggregate)
		{
			this.VisitExpressionList(EntityUtils.CheckArgumentNull<DbAggregate>(aggregate, "aggregate").Arguments);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0006CFF8 File Offset: 0x0006B1F8
		public override void Visit(DbExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbExpression>(expression, "expression");
			throw new NotSupportedException(EFProviderSettings.Instance.GetErrorMessage(-1703, new string[]
			{
				"Oracle Data Provider for .NET",
				expression.GetType().FullName
			}));
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0006D044 File Offset: 0x0006B244
		public override void Visit(DbConstantExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbConstantExpression>(expression, "expression");
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0006D054 File Offset: 0x0006B254
		public override void Visit(DbNullExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbNullExpression>(expression, "expression");
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0006D064 File Offset: 0x0006B264
		public override void Visit(DbVariableReferenceExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbVariableReferenceExpression>(expression, "expression");
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0006D074 File Offset: 0x0006B274
		public override void Visit(DbParameterReferenceExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbParameterReferenceExpression>(expression, "expression");
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0006D084 File Offset: 0x0006B284
		public override void Visit(DbFunctionExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbFunctionExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0006D0A0 File Offset: 0x0006B2A0
		public override void Visit(DbPropertyExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbPropertyExpression>(expression, "expression");
			if (expression.Instance != null)
			{
				this.VisitExpression(expression.Instance);
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0006D0C4 File Offset: 0x0006B2C4
		public override void Visit(DbComparisonExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0006D0D0 File Offset: 0x0006B2D0
		public override void Visit(DbLikeExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbLikeExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
			this.VisitExpression(expression.Pattern);
			this.VisitExpression(expression.Escape);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0006D104 File Offset: 0x0006B304
		public override void Visit(DbLimitExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbLimitExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
			this.VisitExpression(expression.Limit);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0006D12C File Offset: 0x0006B32C
		public override void Visit(DbIsNullExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0006D138 File Offset: 0x0006B338
		public override void Visit(DbArithmeticExpression expression)
		{
			this.VisitExpressionList(EntityUtils.CheckArgumentNull<DbArithmeticExpression>(expression, "expression").Arguments);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0006D150 File Offset: 0x0006B350
		public override void Visit(DbAndExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0006D15C File Offset: 0x0006B35C
		public override void Visit(DbOrExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0006D168 File Offset: 0x0006B368
		public override void Visit(DbNotExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0006D174 File Offset: 0x0006B374
		public override void Visit(DbDistinctExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0006D180 File Offset: 0x0006B380
		public override void Visit(DbElementExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0006D18C File Offset: 0x0006B38C
		public override void Visit(DbIsEmptyExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0006D198 File Offset: 0x0006B398
		public override void Visit(DbUnionAllExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0006D1A4 File Offset: 0x0006B3A4
		public override void Visit(DbIntersectExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0006D1B0 File Offset: 0x0006B3B0
		public override void Visit(DbExceptExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0006D1BC File Offset: 0x0006B3BC
		public override void Visit(DbOfTypeExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0006D1C8 File Offset: 0x0006B3C8
		public override void Visit(DbTreatExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0006D1D4 File Offset: 0x0006B3D4
		public override void Visit(DbCastExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0006D1E0 File Offset: 0x0006B3E0
		public override void Visit(DbIsOfExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0006D1EC File Offset: 0x0006B3EC
		public override void Visit(DbCaseExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbCaseExpression>(expression, "expression");
			this.VisitExpressionList(expression.When);
			this.VisitExpressionList(expression.Then);
			this.VisitExpression(expression.Else);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0006D220 File Offset: 0x0006B420
		public override void Visit(DbRefExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0006D22C File Offset: 0x0006B42C
		public override void Visit(DbRelationshipNavigationExpression expression)
		{
			this.VisitExpression(EntityUtils.CheckArgumentNull<DbRelationshipNavigationExpression>(expression, "expression").NavigationSource);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0006D244 File Offset: 0x0006B444
		public override void Visit(DbDerefExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0006D250 File Offset: 0x0006B450
		public override void Visit(DbRefKeyExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0006D25C File Offset: 0x0006B45C
		public override void Visit(DbEntityRefExpression expression)
		{
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0006D268 File Offset: 0x0006B468
		public override void Visit(DbScanExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbScanExpression>(expression, "expression");
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0006D278 File Offset: 0x0006B478
		public override void Visit(DbFilterExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbFilterExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Predicate);
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0006D2AC File Offset: 0x0006B4AC
		public override void Visit(DbProjectExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbProjectExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Projection);
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0006D2E0 File Offset: 0x0006B4E0
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

		// Token: 0x06000976 RID: 2422 RVA: 0x0006D378 File Offset: 0x0006B578
		public override void Visit(DbJoinExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbJoinExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Left);
			this.VisitExpressionBindingPre(expression.Right);
			this.VisitExpression(expression.JoinCondition);
			this.VisitExpressionBindingPost(expression.Left);
			this.VisitExpressionBindingPost(expression.Right);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0006D3D0 File Offset: 0x0006B5D0
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

		// Token: 0x06000978 RID: 2424 RVA: 0x0006D410 File Offset: 0x0006B610
		public override void Visit(DbGroupByExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbGroupByExpression>(expression, "expression");
			this.VisitGroupExpressionBindingPre(expression.Input);
			this.VisitExpressionList(expression.Keys);
			this.VisitGroupExpressionBindingMid(expression.Input);
			this.VisitAggregateList(expression.Aggregates);
			this.VisitGroupExpressionBindingPost(expression.Input);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0006D468 File Offset: 0x0006B668
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

		// Token: 0x0600097A RID: 2426 RVA: 0x0006D4EC File Offset: 0x0006B6EC
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

		// Token: 0x0600097B RID: 2427 RVA: 0x0006D54C File Offset: 0x0006B74C
		public override void Visit(DbQuantifierExpression expression)
		{
			EntityUtils.CheckArgumentNull<DbQuantifierExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Predicate);
			this.VisitExpressionBindingPost(expression.Input);
		}
	}
}

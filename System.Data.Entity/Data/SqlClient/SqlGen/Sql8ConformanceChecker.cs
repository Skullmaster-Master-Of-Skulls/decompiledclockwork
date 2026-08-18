using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Entity;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x02000039 RID: 57
	internal class Sql8ConformanceChecker : DbExpressionVisitor<bool>
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00017230 File Offset: 0x00015430
		internal static bool NeedsRewrite(DbExpression expr)
		{
			Sql8ConformanceChecker visitor = new Sql8ConformanceChecker();
			return expr.Accept<bool>(visitor);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001724A File Offset: 0x0001544A
		private Sql8ConformanceChecker()
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00017252 File Offset: 0x00015452
		private bool VisitUnaryExpression(DbUnaryExpression expr)
		{
			return this.VisitExpression(expr.Argument);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00017260 File Offset: 0x00015460
		private bool VisitBinaryExpression(DbBinaryExpression expr)
		{
			bool flag = this.VisitExpression(expr.Left);
			bool flag2 = this.VisitExpression(expr.Right);
			return flag || flag2;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001728A File Offset: 0x0001548A
		private bool VisitAggregate(DbAggregate aggregate)
		{
			return this.VisitExpressionList(aggregate.Arguments);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00017298 File Offset: 0x00015498
		private bool VisitExpressionBinding(DbExpressionBinding expressionBinding)
		{
			return this.VisitExpression(expressionBinding.Expression);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000172A6 File Offset: 0x000154A6
		private bool VisitExpression(DbExpression expression)
		{
			return expression != null && expression.Accept<bool>(this);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000172B4 File Offset: 0x000154B4
		private bool VisitSortClause(DbSortClause sortClause)
		{
			return this.VisitExpression(sortClause.Expression);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000172C4 File Offset: 0x000154C4
		private static bool VisitList<TElementType>(Sql8ConformanceChecker.ListElementHandler<TElementType> handler, IList<TElementType> list)
		{
			bool flag = false;
			foreach (TElementType element in list)
			{
				bool flag2 = handler(element);
				flag = (flag || flag2);
			}
			return flag;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00017314 File Offset: 0x00015514
		private bool VisitAggregateList(IList<DbAggregate> list)
		{
			return Sql8ConformanceChecker.VisitList<DbAggregate>(new Sql8ConformanceChecker.ListElementHandler<DbAggregate>(this.VisitAggregate), list);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00017328 File Offset: 0x00015528
		private bool VisitExpressionBindingList(IList<DbExpressionBinding> list)
		{
			return Sql8ConformanceChecker.VisitList<DbExpressionBinding>(new Sql8ConformanceChecker.ListElementHandler<DbExpressionBinding>(this.VisitExpressionBinding), list);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001733C File Offset: 0x0001553C
		private bool VisitExpressionList(IList<DbExpression> list)
		{
			return Sql8ConformanceChecker.VisitList<DbExpression>(new Sql8ConformanceChecker.ListElementHandler<DbExpression>(this.VisitExpression), list);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00017350 File Offset: 0x00015550
		private bool VisitSortClauseList(IList<DbSortClause> list)
		{
			return Sql8ConformanceChecker.VisitList<DbSortClause>(new Sql8ConformanceChecker.ListElementHandler<DbSortClause>(this.VisitSortClause), list);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00017364 File Offset: 0x00015564
		public override bool Visit(DbExpression expression)
		{
			throw EntityUtil.NotSupported(Strings.Cqt_General_UnsupportedExpression(expression.GetType().FullName));
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001737B File Offset: 0x0001557B
		public override bool Visit(DbAndExpression expression)
		{
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00017384 File Offset: 0x00015584
		public override bool Visit(DbApplyExpression expression)
		{
			throw EntityUtil.NotSupported(Strings.SqlGen_ApplyNotSupportedOnSql8);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00017390 File Offset: 0x00015590
		public override bool Visit(DbArithmeticExpression expression)
		{
			return this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000173A0 File Offset: 0x000155A0
		public override bool Visit(DbCaseExpression expression)
		{
			bool flag = this.VisitExpressionList(expression.When);
			bool flag2 = this.VisitExpressionList(expression.Then);
			bool flag3 = this.VisitExpression(expression.Else);
			return flag || flag2 || flag3;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbCastExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001737B File Offset: 0x0001557B
		public override bool Visit(DbComparisonExpression expression)
		{
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool Visit(DbConstantExpression expression)
		{
			return false;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000173E5 File Offset: 0x000155E5
		public override bool Visit(DbCrossJoinExpression expression)
		{
			return this.VisitExpressionBindingList(expression.Inputs);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbDerefExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbDistinctExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbElementExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbEntityRefExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000173F3 File Offset: 0x000155F3
		public override bool Visit(DbExceptExpression expression)
		{
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
			return true;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00017410 File Offset: 0x00015610
		public override bool Visit(DbFilterExpression expression)
		{
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitExpression(expression.Predicate);
			return flag || flag2;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001743A File Offset: 0x0001563A
		public override bool Visit(DbFunctionExpression expression)
		{
			return this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00017448 File Offset: 0x00015648
		public override bool Visit(DbLambdaExpression expression)
		{
			bool flag = this.VisitExpressionList(expression.Arguments);
			bool flag2 = this.VisitExpression(expression.Lambda.Body);
			return flag || flag2;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00017478 File Offset: 0x00015678
		public override bool Visit(DbGroupByExpression expression)
		{
			bool flag = this.VisitExpression(expression.Input.Expression);
			bool flag2 = this.VisitExpressionList(expression.Keys);
			bool flag3 = this.VisitAggregateList(expression.Aggregates);
			return flag || flag2 || flag3;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000173F3 File Offset: 0x000155F3
		public override bool Visit(DbIntersectExpression expression)
		{
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
			return true;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbIsEmptyExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbIsNullExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbIsOfExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000174B8 File Offset: 0x000156B8
		public override bool Visit(DbJoinExpression expression)
		{
			bool flag = this.VisitExpressionBinding(expression.Left);
			bool flag2 = this.VisitExpressionBinding(expression.Right);
			bool flag3 = this.VisitExpression(expression.JoinCondition);
			return flag || flag2 || flag3;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000174F4 File Offset: 0x000156F4
		public override bool Visit(DbLikeExpression expression)
		{
			bool flag = this.VisitExpression(expression.Argument);
			bool flag2 = this.VisitExpression(expression.Pattern);
			bool flag3 = this.VisitExpression(expression.Escape);
			return flag || flag2 || flag3;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001752D File Offset: 0x0001572D
		public override bool Visit(DbLimitExpression expression)
		{
			if (expression.Limit is DbParameterReferenceExpression)
			{
				throw EntityUtil.NotSupported(Strings.SqlGen_ParameterForLimitNotSupportedOnSql8);
			}
			return this.VisitExpression(expression.Argument);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00017553 File Offset: 0x00015753
		public override bool Visit(DbNewInstanceExpression expression)
		{
			return this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbNotExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool Visit(DbNullExpression expression)
		{
			return false;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbOfTypeExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001737B File Offset: 0x0001557B
		public override bool Visit(DbOrExpression expression)
		{
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool Visit(DbParameterReferenceExpression expression)
		{
			return false;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00017564 File Offset: 0x00015764
		public override bool Visit(DbProjectExpression expression)
		{
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitExpression(expression.Projection);
			return flag || flag2;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001758E File Offset: 0x0001578E
		public override bool Visit(DbPropertyExpression expression)
		{
			return this.VisitExpression(expression.Instance);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001759C File Offset: 0x0001579C
		public override bool Visit(DbQuantifierExpression expression)
		{
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitExpression(expression.Predicate);
			return flag || flag2;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbRefExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbRefKeyExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000175C6 File Offset: 0x000157C6
		public override bool Visit(DbRelationshipNavigationExpression expression)
		{
			return this.VisitExpression(expression.NavigationSource);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool Visit(DbScanExpression expression)
		{
			return false;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000175D4 File Offset: 0x000157D4
		public override bool Visit(DbSkipExpression expression)
		{
			if (expression.Count is DbParameterReferenceExpression)
			{
				throw EntityUtil.NotSupported(Strings.SqlGen_ParameterForSkipNotSupportedOnSql8);
			}
			this.VisitExpressionBinding(expression.Input);
			this.VisitSortClauseList(expression.SortOrder);
			this.VisitExpression(expression.Count);
			return true;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00017624 File Offset: 0x00015824
		public override bool Visit(DbSortExpression expression)
		{
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitSortClauseList(expression.SortOrder);
			return flag || flag2;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000173D9 File Offset: 0x000155D9
		public override bool Visit(DbTreatExpression expression)
		{
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001737B File Offset: 0x0001557B
		public override bool Visit(DbUnionAllExpression expression)
		{
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool Visit(DbVariableReferenceExpression expression)
		{
			return false;
		}

		// Token: 0x0200045C RID: 1116
		// (Invoke) Token: 0x06003AC3 RID: 15043
		private delegate bool ListElementHandler<TElementType>(TElementType element);
	}
}

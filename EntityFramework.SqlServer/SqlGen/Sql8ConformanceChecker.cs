using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000033 RID: 51
	internal class Sql8ConformanceChecker : DbExpressionVisitor<bool>
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		internal static bool NeedsRewrite(DbExpression expr)
		{
			Sql8ConformanceChecker visitor = new Sql8ConformanceChecker();
			return expr.Accept<bool>(visitor);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000BBEA File Offset: 0x00009DEA
		private Sql8ConformanceChecker()
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000BBF2 File Offset: 0x00009DF2
		private bool VisitUnaryExpression(DbUnaryExpression expr)
		{
			return this.VisitExpression(expr.Argument);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000BC00 File Offset: 0x00009E00
		private bool VisitBinaryExpression(DbBinaryExpression expr)
		{
			bool flag = this.VisitExpression(expr.Left);
			bool flag2 = this.VisitExpression(expr.Right);
			return flag || flag2;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000BC2D File Offset: 0x00009E2D
		private bool VisitAggregate(DbAggregate aggregate)
		{
			return this.VisitExpressionList(aggregate.Arguments);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000BC3B File Offset: 0x00009E3B
		private bool VisitExpressionBinding(DbExpressionBinding expressionBinding)
		{
			return this.VisitExpression(expressionBinding.Expression);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000BC49 File Offset: 0x00009E49
		private bool VisitExpression(DbExpression expression)
		{
			return expression != null && expression.Accept<bool>(this);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000BC57 File Offset: 0x00009E57
		private bool VisitSortClause(DbSortClause sortClause)
		{
			return this.VisitExpression(sortClause.Expression);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000BC68 File Offset: 0x00009E68
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

		// Token: 0x060002CA RID: 714 RVA: 0x0000BCBC File Offset: 0x00009EBC
		private bool VisitAggregateList(IList<DbAggregate> list)
		{
			return Sql8ConformanceChecker.VisitList<DbAggregate>(new Sql8ConformanceChecker.ListElementHandler<DbAggregate>(this.VisitAggregate), list);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		private bool VisitExpressionBindingList(IList<DbExpressionBinding> list)
		{
			return Sql8ConformanceChecker.VisitList<DbExpressionBinding>(new Sql8ConformanceChecker.ListElementHandler<DbExpressionBinding>(this.VisitExpressionBinding), list);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		private bool VisitExpressionList(IList<DbExpression> list)
		{
			return Sql8ConformanceChecker.VisitList<DbExpression>(new Sql8ConformanceChecker.ListElementHandler<DbExpression>(this.VisitExpression), list);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		private bool VisitSortClauseList(IList<DbSortClause> list)
		{
			return Sql8ConformanceChecker.VisitList<DbSortClause>(new Sql8ConformanceChecker.ListElementHandler<DbSortClause>(this.VisitSortClause), list);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public override bool Visit(DbExpression expression)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(expression.GetType().FullName));
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000BD2F File Offset: 0x00009F2F
		public override bool Visit(DbAndExpression expression)
		{
			Check.NotNull<DbAndExpression>(expression, "expression");
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000BD44 File Offset: 0x00009F44
		public override bool Visit(DbApplyExpression expression)
		{
			Check.NotNull<DbApplyExpression>(expression, "expression");
			throw new NotSupportedException(Strings.SqlGen_ApplyNotSupportedOnSql8);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000BD5C File Offset: 0x00009F5C
		public override bool Visit(DbArithmeticExpression expression)
		{
			Check.NotNull<DbArithmeticExpression>(expression, "expression");
			return this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000BD78 File Offset: 0x00009F78
		public override bool Visit(DbCaseExpression expression)
		{
			Check.NotNull<DbCaseExpression>(expression, "expression");
			bool flag = this.VisitExpressionList(expression.When);
			bool flag2 = this.VisitExpressionList(expression.Then);
			bool flag3 = this.VisitExpression(expression.Else);
			return flag || flag2 || flag3;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000BDC1 File Offset: 0x00009FC1
		public override bool Visit(DbCastExpression expression)
		{
			Check.NotNull<DbCastExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000BDD6 File Offset: 0x00009FD6
		public override bool Visit(DbComparisonExpression expression)
		{
			Check.NotNull<DbComparisonExpression>(expression, "expression");
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000BDEB File Offset: 0x00009FEB
		public override bool Visit(DbConstantExpression expression)
		{
			Check.NotNull<DbConstantExpression>(expression, "expression");
			return false;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000BDFA File Offset: 0x00009FFA
		public override bool Visit(DbCrossJoinExpression expression)
		{
			Check.NotNull<DbCrossJoinExpression>(expression, "expression");
			return this.VisitExpressionBindingList(expression.Inputs);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000BE14 File Offset: 0x0000A014
		public override bool Visit(DbDerefExpression expression)
		{
			Check.NotNull<DbDerefExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000BE29 File Offset: 0x0000A029
		public override bool Visit(DbDistinctExpression expression)
		{
			Check.NotNull<DbDistinctExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000BE3E File Offset: 0x0000A03E
		public override bool Visit(DbElementExpression expression)
		{
			Check.NotNull<DbElementExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000BE53 File Offset: 0x0000A053
		public override bool Visit(DbEntityRefExpression expression)
		{
			Check.NotNull<DbEntityRefExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000BE68 File Offset: 0x0000A068
		public override bool Visit(DbExceptExpression expression)
		{
			Check.NotNull<DbExceptExpression>(expression, "expression");
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
			return true;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000BE94 File Offset: 0x0000A094
		public override bool Visit(DbFilterExpression expression)
		{
			Check.NotNull<DbFilterExpression>(expression, "expression");
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitExpression(expression.Predicate);
			return flag || flag2;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000BECD File Offset: 0x0000A0CD
		public override bool Visit(DbFunctionExpression expression)
		{
			Check.NotNull<DbFunctionExpression>(expression, "expression");
			return this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
		public override bool Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			bool flag = this.VisitExpressionList(expression.Arguments);
			bool flag2 = this.VisitExpression(expression.Lambda.Body);
			return flag || flag2;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000BF28 File Offset: 0x0000A128
		public override bool Visit(DbGroupByExpression expression)
		{
			Check.NotNull<DbGroupByExpression>(expression, "expression");
			bool flag = this.VisitExpression(expression.Input.Expression);
			bool flag2 = this.VisitExpressionList(expression.Keys);
			bool flag3 = this.VisitAggregateList(expression.Aggregates);
			return flag || flag2 || flag3;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000BF76 File Offset: 0x0000A176
		public override bool Visit(DbIntersectExpression expression)
		{
			Check.NotNull<DbIntersectExpression>(expression, "expression");
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
			return true;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000BF9F File Offset: 0x0000A19F
		public override bool Visit(DbIsEmptyExpression expression)
		{
			Check.NotNull<DbIsEmptyExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		public override bool Visit(DbIsNullExpression expression)
		{
			Check.NotNull<DbIsNullExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000BFC9 File Offset: 0x0000A1C9
		public override bool Visit(DbIsOfExpression expression)
		{
			Check.NotNull<DbIsOfExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		public override bool Visit(DbJoinExpression expression)
		{
			Check.NotNull<DbJoinExpression>(expression, "expression");
			bool flag = this.VisitExpressionBinding(expression.Left);
			bool flag2 = this.VisitExpressionBinding(expression.Right);
			bool flag3 = this.VisitExpression(expression.JoinCondition);
			return flag || flag2 || flag3;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C02C File Offset: 0x0000A22C
		public override bool Visit(DbLikeExpression expression)
		{
			Check.NotNull<DbLikeExpression>(expression, "expression");
			bool flag = this.VisitExpression(expression.Argument);
			bool flag2 = this.VisitExpression(expression.Pattern);
			bool flag3 = this.VisitExpression(expression.Escape);
			return flag || flag2 || flag3;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000C075 File Offset: 0x0000A275
		public override bool Visit(DbLimitExpression expression)
		{
			Check.NotNull<DbLimitExpression>(expression, "expression");
			if (expression.Limit is DbParameterReferenceExpression)
			{
				throw new NotSupportedException(Strings.SqlGen_ParameterForLimitNotSupportedOnSql8);
			}
			return this.VisitExpression(expression.Argument);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C0A7 File Offset: 0x0000A2A7
		public override bool Visit(DbNewInstanceExpression expression)
		{
			Check.NotNull<DbNewInstanceExpression>(expression, "expression");
			return this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C0C1 File Offset: 0x0000A2C1
		public override bool Visit(DbNotExpression expression)
		{
			Check.NotNull<DbNotExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000C0D6 File Offset: 0x0000A2D6
		public override bool Visit(DbNullExpression expression)
		{
			Check.NotNull<DbNullExpression>(expression, "expression");
			return false;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000C0E5 File Offset: 0x0000A2E5
		public override bool Visit(DbOfTypeExpression expression)
		{
			Check.NotNull<DbOfTypeExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C0FA File Offset: 0x0000A2FA
		public override bool Visit(DbOrExpression expression)
		{
			Check.NotNull<DbOrExpression>(expression, "expression");
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000C10F File Offset: 0x0000A30F
		public override bool Visit(DbInExpression expression)
		{
			Check.NotNull<DbInExpression>(expression, "expression");
			return this.VisitExpression(expression.Item) || this.VisitExpressionList(expression.List);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000C139 File Offset: 0x0000A339
		public override bool Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
			return false;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000C148 File Offset: 0x0000A348
		public override bool Visit(DbProjectExpression expression)
		{
			Check.NotNull<DbProjectExpression>(expression, "expression");
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitExpression(expression.Projection);
			return flag || flag2;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000C181 File Offset: 0x0000A381
		public override bool Visit(DbPropertyExpression expression)
		{
			Check.NotNull<DbPropertyExpression>(expression, "expression");
			return this.VisitExpression(expression.Instance);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000C19C File Offset: 0x0000A39C
		public override bool Visit(DbQuantifierExpression expression)
		{
			Check.NotNull<DbQuantifierExpression>(expression, "expression");
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitExpression(expression.Predicate);
			return flag || flag2;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000C1D5 File Offset: 0x0000A3D5
		public override bool Visit(DbRefExpression expression)
		{
			Check.NotNull<DbRefExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000C1EA File Offset: 0x0000A3EA
		public override bool Visit(DbRefKeyExpression expression)
		{
			Check.NotNull<DbRefKeyExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000C1FF File Offset: 0x0000A3FF
		public override bool Visit(DbRelationshipNavigationExpression expression)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
			return this.VisitExpression(expression.NavigationSource);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000C219 File Offset: 0x0000A419
		public override bool Visit(DbScanExpression expression)
		{
			Check.NotNull<DbScanExpression>(expression, "expression");
			return false;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000C228 File Offset: 0x0000A428
		public override bool Visit(DbSkipExpression expression)
		{
			Check.NotNull<DbSkipExpression>(expression, "expression");
			if (expression.Count is DbParameterReferenceExpression)
			{
				throw new NotSupportedException(Strings.SqlGen_ParameterForSkipNotSupportedOnSql8);
			}
			this.VisitExpressionBinding(expression.Input);
			this.VisitSortClauseList(expression.SortOrder);
			this.VisitExpression(expression.Count);
			return true;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C284 File Offset: 0x0000A484
		public override bool Visit(DbSortExpression expression)
		{
			Check.NotNull<DbSortExpression>(expression, "expression");
			bool flag = this.VisitExpressionBinding(expression.Input);
			bool flag2 = this.VisitSortClauseList(expression.SortOrder);
			return flag || flag2;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000C2BD File Offset: 0x0000A4BD
		public override bool Visit(DbTreatExpression expression)
		{
			Check.NotNull<DbTreatExpression>(expression, "expression");
			return this.VisitUnaryExpression(expression);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000C2D2 File Offset: 0x0000A4D2
		public override bool Visit(DbUnionAllExpression expression)
		{
			Check.NotNull<DbUnionAllExpression>(expression, "expression");
			return this.VisitBinaryExpression(expression);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000C2E7 File Offset: 0x0000A4E7
		public override bool Visit(DbVariableReferenceExpression expression)
		{
			Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
			return false;
		}

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x060002FB RID: 763
		private delegate bool ListElementHandler<TElementType>(TElementType element);
	}
}

using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000025 RID: 37
	internal class CommandTreeUtils
	{
		// Token: 0x0600017D RID: 381 RVA: 0x000145CC File Offset: 0x000135CC
		internal static IEnumerable<DbExpression> FlattenAssociativeExpression(DbExpression expression)
		{
			return CommandTreeUtils.FlattenAssociativeExpression(expression.ExpressionKind, new DbExpression[]
			{
				expression
			});
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000145F0 File Offset: 0x000135F0
		internal static IEnumerable<DbExpression> FlattenAssociativeExpression(DbExpressionKind expressionKind, params DbExpression[] arguments)
		{
			if (!CommandTreeUtils._associativeExpressionKinds.Contains(expressionKind))
			{
				return arguments;
			}
			List<DbExpression> list = new List<DbExpression>();
			foreach (DbExpression expression in arguments)
			{
				CommandTreeUtils.ExtractAssociativeArguments(expressionKind, list, expression);
			}
			return list;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00014630 File Offset: 0x00013630
		private static void ExtractAssociativeArguments(DbExpressionKind expressionKind, List<DbExpression> argumentList, DbExpression expression)
		{
			if (expression.ExpressionKind != expressionKind)
			{
				argumentList.Add(expression);
				return;
			}
			DbBinaryExpression dbBinaryExpression = expression as DbBinaryExpression;
			if (dbBinaryExpression != null)
			{
				CommandTreeUtils.ExtractAssociativeArguments(expressionKind, argumentList, dbBinaryExpression.Left);
				CommandTreeUtils.ExtractAssociativeArguments(expressionKind, argumentList, dbBinaryExpression.Right);
				return;
			}
			DbArithmeticExpression dbArithmeticExpression = (DbArithmeticExpression)expression;
			CommandTreeUtils.ExtractAssociativeArguments(expressionKind, argumentList, dbArithmeticExpression.Arguments[0]);
			CommandTreeUtils.ExtractAssociativeArguments(expressionKind, argumentList, dbArithmeticExpression.Arguments[1]);
		}

		// Token: 0x04000100 RID: 256
		private static readonly HashSet<DbExpressionKind> _associativeExpressionKinds = new HashSet<DbExpressionKind>(new DbExpressionKind[]
		{
			DbExpressionKind.Or,
			DbExpressionKind.And,
			DbExpressionKind.Plus,
			DbExpressionKind.Multiply
		});
	}
}

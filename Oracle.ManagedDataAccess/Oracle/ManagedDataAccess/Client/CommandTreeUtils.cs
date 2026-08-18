using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x020000E8 RID: 232
	internal class CommandTreeUtils
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x0006C934 File Offset: 0x0006AB34
		internal static IEnumerable<DbExpression> FlattenAssociativeExpression(DbExpression expression)
		{
			return CommandTreeUtils.FlattenAssociativeExpression(expression.ExpressionKind, new DbExpression[]
			{
				expression
			});
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0006C958 File Offset: 0x0006AB58
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

		// Token: 0x06000927 RID: 2343 RVA: 0x0006C998 File Offset: 0x0006AB98
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

		// Token: 0x04000C3C RID: 3132
		private static readonly HashSet<DbExpressionKind> _associativeExpressionKinds = new HashSet<DbExpressionKind>(new DbExpressionKind[]
		{
			DbExpressionKind.Or,
			DbExpressionKind.And,
			DbExpressionKind.Plus,
			DbExpressionKind.Multiply
		});
	}
}

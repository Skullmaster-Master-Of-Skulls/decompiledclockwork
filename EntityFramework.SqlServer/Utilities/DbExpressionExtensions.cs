using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200002B RID: 43
	internal static class DbExpressionExtensions
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000B788 File Offset: 0x00009988
		public static IEnumerable<DbExpression> GetLeafNodes(this DbExpression root, DbExpressionKind kind, Func<DbExpression, IEnumerable<DbExpression>> getChildNodes)
		{
			Stack<DbExpression> nodes = new Stack<DbExpression>();
			nodes.Push(root);
			while (nodes.Count > 0)
			{
				DbExpression current = nodes.Pop();
				if (current.ExpressionKind != kind)
				{
					yield return current;
				}
				else
				{
					foreach (DbExpression item in getChildNodes(current).Reverse<DbExpression>())
					{
						nodes.Push(item);
					}
				}
			}
			yield break;
		}
	}
}

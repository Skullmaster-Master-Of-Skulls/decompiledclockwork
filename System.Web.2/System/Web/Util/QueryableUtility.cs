using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Web.Util
{
	// Token: 0x020001DC RID: 476
	internal static class QueryableUtility
	{
		// Token: 0x060017A3 RID: 6051 RVA: 0x0004A268 File Offset: 0x00048468
		private static MethodInfo GetQueryableMethod(Expression expression)
		{
			if (expression.NodeType == ExpressionType.Call)
			{
				MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
				if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.DeclaringType == typeof(Queryable))
				{
					return methodCallExpression.Method.GetGenericMethodDefinition();
				}
			}
			return null;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0004A2BC File Offset: 0x000484BC
		public static bool IsQueryableMethod(Expression expression, string method)
		{
			return (from m in QueryableUtility._methods
			where m.Name == method
			select m).Contains(QueryableUtility.GetQueryableMethod(expression));
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0004A2F8 File Offset: 0x000484F8
		public static bool IsOrderingMethod(Expression expression)
		{
			return QueryableUtility._orderMethods.Any((string method) => QueryableUtility.IsQueryableMethod(expression, method));
		}

		// Token: 0x04001720 RID: 5920
		private static readonly string[] _orderMethods = new string[]
		{
			"OrderBy",
			"ThenBy",
			"OrderByDescending",
			"ThenByDescending"
		};

		// Token: 0x04001721 RID: 5921
		private static readonly MethodInfo[] _methods = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public);
	}
}

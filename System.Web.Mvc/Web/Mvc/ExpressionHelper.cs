using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc.ExpressionUtil;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000146 RID: 326
	public static class ExpressionHelper
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x0001748C File Offset: 0x0001568C
		public static string GetExpressionText(string expression)
		{
			if (!string.Equals(expression, "model", StringComparison.OrdinalIgnoreCase))
			{
				return expression;
			}
			return string.Empty;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000174AC File Offset: 0x000156AC
		public static string GetExpressionText(LambdaExpression expression)
		{
			Stack<string> stack = new Stack<string>();
			Expression expression2 = expression.Body;
			while (expression2 != null)
			{
				if (expression2.NodeType == ExpressionType.Call)
				{
					MethodCallExpression methodCallExpression = (MethodCallExpression)expression2;
					if (!ExpressionHelper.IsSingleArgumentIndexer(methodCallExpression))
					{
						break;
					}
					stack.Push(ExpressionHelper.GetIndexerInvocation(methodCallExpression.Arguments.Single<Expression>(), expression.Parameters.ToArray<ParameterExpression>()));
					expression2 = methodCallExpression.Object;
				}
				else if (expression2.NodeType == ExpressionType.ArrayIndex)
				{
					BinaryExpression binaryExpression = (BinaryExpression)expression2;
					stack.Push(ExpressionHelper.GetIndexerInvocation(binaryExpression.Right, expression.Parameters.ToArray<ParameterExpression>()));
					expression2 = binaryExpression.Left;
				}
				else if (expression2.NodeType == ExpressionType.MemberAccess)
				{
					MemberExpression memberExpression = (MemberExpression)expression2;
					stack.Push("." + memberExpression.Member.Name);
					expression2 = memberExpression.Expression;
				}
				else
				{
					if (expression2.NodeType != ExpressionType.Parameter)
					{
						break;
					}
					stack.Push(string.Empty);
					expression2 = null;
				}
			}
			if (stack.Count > 0 && string.Equals(stack.Peek(), ".model", StringComparison.OrdinalIgnoreCase))
			{
				stack.Pop();
			}
			if (stack.Count > 0)
			{
				return stack.Aggregate((string left, string right) => left + right).TrimStart(new char[]
				{
					'.'
				});
			}
			return string.Empty;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00017608 File Offset: 0x00015808
		private static string GetIndexerInvocation(Expression expression, ParameterExpression[] parameters)
		{
			Expression body = Expression.Convert(expression, typeof(object));
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
			Expression<Func<object, object>> lambdaExpression = Expression.Lambda<Func<object, object>>(body, new ParameterExpression[]
			{
				parameterExpression
			});
			Func<object, object> func;
			try
			{
				func = CachedExpressionCompiler.Process<object, object>(lambdaExpression);
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ExpressionHelper_InvalidIndexerExpression, new object[]
				{
					expression,
					parameters[0].Name
				}), innerException);
			}
			return "[" + Convert.ToString(func(null), CultureInfo.InvariantCulture) + "]";
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000176DC File Offset: 0x000158DC
		internal static bool IsSingleArgumentIndexer(Expression expression)
		{
			MethodCallExpression methodExpression = expression as MethodCallExpression;
			return methodExpression != null && methodExpression.Arguments.Count == 1 && methodExpression.Method.DeclaringType.GetDefaultMembers().OfType<PropertyInfo>().Any((PropertyInfo p) => p.GetGetMethod() == methodExpression.Method);
		}
	}
}

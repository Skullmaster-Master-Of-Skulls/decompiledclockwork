using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000CD RID: 205
	internal static class ExpressionHelper
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x00026218 File Offset: 0x00024418
		public static Expression GetValue(Expression exp)
		{
			Type underlyingType = ExpressionHelper.GetUnderlyingType(exp.Type);
			if (underlyingType == exp.Type)
			{
				return exp;
			}
			return Expression.Convert(exp, underlyingType);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00026248 File Offset: 0x00024448
		public static Type GetUnderlyingType(Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				return type.GetGenericArguments()[0];
			}
			return type;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00026273 File Offset: 0x00024473
		public static object BuildObjectValue(object value, Type type)
		{
			return DataSourceHelper.BuildObjectValue(value, type, string.Empty);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00026284 File Offset: 0x00024484
		public static Expression CreatePropertyExpression(Expression parameterExpression, string propertyName)
		{
			if (parameterExpression == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(propertyName))
			{
				return null;
			}
			Expression expression = null;
			string[] array = propertyName.Split(new char[]
			{
				'.'
			});
			foreach (string propertyOrFieldName in array)
			{
				if (expression == null)
				{
					expression = Expression.PropertyOrField(parameterExpression, propertyOrFieldName);
				}
				else
				{
					expression = Expression.PropertyOrField(expression, propertyOrFieldName);
				}
			}
			return expression;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x000262E1 File Offset: 0x000244E1
		public static IQueryable Where(this IQueryable source, LambdaExpression lambda)
		{
			return source.Call("Where", lambda, new Type[]
			{
				source.ElementType
			});
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000262FE File Offset: 0x000244FE
		public static IQueryable Call(this IQueryable source, string queryMethod, Type[] genericArgs, params Expression[] arguments)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), queryMethod, genericArgs, arguments));
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002632C File Offset: 0x0002452C
		public static IQueryable Call(this IQueryable source, string queryableMethod, LambdaExpression lambda, params Type[] genericArgs)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), queryableMethod, genericArgs, new Expression[]
			{
				source.Expression,
				Expression.Quote(lambda)
			}));
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002637C File Offset: 0x0002457C
		public static Expression Or(IEnumerable<Expression> expressions)
		{
			Expression expression = null;
			foreach (Expression expression2 in expressions)
			{
				if (expression2 != null)
				{
					if (expression == null)
					{
						expression = expression2;
					}
					else
					{
						expression = Expression.OrElse(expression, expression2);
					}
				}
			}
			return expression;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000263D4 File Offset: 0x000245D4
		public static Expression And(IEnumerable<Expression> expressions)
		{
			Expression expression = null;
			foreach (Expression expression2 in expressions)
			{
				if (expression2 != null)
				{
					if (expression == null)
					{
						expression = expression2;
					}
					else
					{
						expression = Expression.AndAlso(expression, expression2);
					}
				}
			}
			return expression;
		}
	}
}

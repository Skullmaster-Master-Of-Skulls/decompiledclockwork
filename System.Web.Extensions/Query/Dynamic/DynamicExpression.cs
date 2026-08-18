using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Web.Query.Dynamic
{
	// Token: 0x0200003A RID: 58
	internal static class DynamicExpression
	{
		// Token: 0x0600022D RID: 557 RVA: 0x0000D990 File Offset: 0x0000BB90
		public static Expression Parse(Type resultType, string expression, params object[] values)
		{
			ExpressionParser expressionParser = new ExpressionParser(null, expression, values);
			return expressionParser.Parse(resultType);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D9AD File Offset: 0x0000BBAD
		public static LambdaExpression ParseLambda(Type itType, Type resultType, string expression, params object[] values)
		{
			return DynamicExpression.ParseLambda(new ParameterExpression[]
			{
				Expression.Parameter(itType, "")
			}, resultType, expression, values);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D9CC File Offset: 0x0000BBCC
		public static LambdaExpression ParseLambda(ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
		{
			ExpressionParser expressionParser = new ExpressionParser(parameters, expression, values);
			return DynamicExpression.Lambda(expressionParser.Parse(resultType), parameters);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D9EF File Offset: 0x0000BBEF
		public static Expression<Func<T, S>> ParseLambda<T, S>(string expression, params object[] values)
		{
			return (Expression<Func<T, S>>)DynamicExpression.ParseLambda(typeof(T), typeof(S), expression, values);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000DA11 File Offset: 0x0000BC11
		public static Type CreateClass(params DynamicProperty[] properties)
		{
			return ClassFactory.Instance.GetDynamicClass(properties);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000DA11 File Offset: 0x0000BC11
		public static Type CreateClass(IEnumerable<DynamicProperty> properties)
		{
			return ClassFactory.Instance.GetDynamicClass(properties);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000DA20 File Offset: 0x0000BC20
		public static LambdaExpression Lambda(Expression body, params ParameterExpression[] parameters)
		{
			int num = (parameters == null) ? 0 : parameters.Length;
			Type[] array = new Type[num + 1];
			for (int i = 0; i < num; i++)
			{
				array[i] = parameters[i].Type;
			}
			array[num] = body.Type;
			return Expression.Lambda(DynamicExpression.GetFuncType(array), body, parameters);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000DA6D File Offset: 0x0000BC6D
		public static Type GetFuncType(params Type[] typeArgs)
		{
			if (typeArgs == null || typeArgs.Length < 1 || typeArgs.Length > 5)
			{
				throw new ArgumentException();
			}
			return DynamicExpression.funcTypes[typeArgs.Length - 1].MakeGenericType(typeArgs);
		}

		// Token: 0x040000DC RID: 220
		private static readonly Type[] funcTypes = new Type[]
		{
			typeof(Func<>),
			typeof(Func<, >),
			typeof(Func<, , >),
			typeof(Func<, , , >),
			typeof(Func<, , , , >)
		};
	}
}

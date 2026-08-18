using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Telerik.Web.UI
{
	// Token: 0x02000372 RID: 882
	internal static class DynamicExpression
	{
		// Token: 0x06001E3A RID: 7738 RVA: 0x0005E3BC File Offset: 0x0005C5BC
		public static Expression Parse(Type resultType, string expression, params object[] values)
		{
			ExpressionParser expressionParser = new ExpressionParser(null, expression, values);
			return expressionParser.Parse(resultType);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0005E3DC File Offset: 0x0005C5DC
		public static LambdaExpression ParseLambda(Type itType, Type resultType, string expression, params object[] values)
		{
			return DynamicExpression.ParseLambda(new ParameterExpression[]
			{
				Expression.Parameter(itType, "")
			}, resultType, expression, values);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0005E408 File Offset: 0x0005C608
		public static LambdaExpression ParseLambda(ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
		{
			ExpressionParser expressionParser = new ExpressionParser(parameters, expression, values);
			return Expression.Lambda(expressionParser.Parse(resultType), parameters);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0005E42B File Offset: 0x0005C62B
		public static Expression<Func<T, S>> ParseLambda<T, S>(string expression, params object[] values)
		{
			return (Expression<Func<T, S>>)DynamicExpression.ParseLambda(typeof(T), typeof(S), expression, values);
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x0005E44D File Offset: 0x0005C64D
		public static Type CreateClass(params DynamicProperty[] properties)
		{
			return ClassFactory.Instance.GetDynamicClass(properties);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0005E45A File Offset: 0x0005C65A
		public static Type CreateClass(IEnumerable<DynamicProperty> properties)
		{
			return ClassFactory.Instance.GetDynamicClass(properties);
		}
	}
}

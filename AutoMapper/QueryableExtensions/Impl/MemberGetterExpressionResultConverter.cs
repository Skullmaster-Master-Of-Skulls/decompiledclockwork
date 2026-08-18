using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x02000062 RID: 98
	public class MemberGetterExpressionResultConverter : IExpressionResultConverter
	{
		// Token: 0x06000383 RID: 899 RVA: 0x00008D3D File Offset: 0x00006F3D
		public ExpressionResolutionResult GetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, PropertyMap propertyMap, IValueResolver valueResolver)
		{
			return MemberGetterExpressionResultConverter.ExpressionResolutionResult(expressionResolutionResult, valueResolver);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00008D3D File Offset: 0x00006F3D
		public ExpressionResolutionResult GetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, ConstructorParameterMap propertyMap, IValueResolver valueResolver)
		{
			return MemberGetterExpressionResultConverter.ExpressionResolutionResult(expressionResolutionResult, valueResolver);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00008D48 File Offset: 0x00006F48
		private static ExpressionResolutionResult ExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, IValueResolver valueResolver)
		{
			Expression expression = expressionResolutionResult.ResolutionExpression;
			PropertyInfo propertyInfo = ((IMemberGetter)valueResolver).MemberInfo as PropertyInfo;
			Type type;
			if (propertyInfo != null)
			{
				expression = Expression.Property(expression, propertyInfo);
				type = propertyInfo.PropertyType;
			}
			else
			{
				type = expression.Type;
			}
			return new ExpressionResolutionResult(expression, type);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00008D95 File Offset: 0x00006F95
		public bool CanGetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, IValueResolver valueResolver)
		{
			return valueResolver is IMemberGetter;
		}
	}
}

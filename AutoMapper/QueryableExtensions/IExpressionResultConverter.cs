using System;

namespace AutoMapper.QueryableExtensions
{
	// Token: 0x0200005A RID: 90
	public interface IExpressionResultConverter
	{
		// Token: 0x06000359 RID: 857
		ExpressionResolutionResult GetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, PropertyMap propertyMap, IValueResolver valueResolver);

		// Token: 0x0600035A RID: 858
		ExpressionResolutionResult GetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, ConstructorParameterMap propertyMap, IValueResolver valueResolver);

		// Token: 0x0600035B RID: 859
		bool CanGetExpressionResolutionResult(ExpressionResolutionResult expressionResolutionResult, IValueResolver valueResolver);
	}
}

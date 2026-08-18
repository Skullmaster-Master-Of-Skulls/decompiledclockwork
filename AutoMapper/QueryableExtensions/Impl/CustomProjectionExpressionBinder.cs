using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x0200005E RID: 94
	public class CustomProjectionExpressionBinder : IExpressionBinder
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0000898F File Offset: 0x00006B8F
		public bool IsMatch(PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionResolutionResult result)
		{
			return ((propertyTypeMap != null) ? propertyTypeMap.CustomProjection : null) != null;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000089A0 File Offset: 0x00006BA0
		public MemberAssignment Build(IConfigurationProvider configuration, PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionRequest request, ExpressionResolutionResult result, ConcurrentDictionary<ExpressionRequest, int> typePairCount)
		{
			return CustomProjectionExpressionBinder.BindCustomProjectionExpression(propertyMap, propertyTypeMap, result);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000089AC File Offset: 0x00006BAC
		private static MemberAssignment BindCustomProjectionExpression(PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionResolutionResult result)
		{
			Expression expression = new ParameterReplacementVisitor(result.ResolutionExpression).Visit(propertyTypeMap.CustomProjection.Body);
			return Expression.Bind(propertyMap.DestinationProperty.MemberInfo, expression);
		}
	}
}

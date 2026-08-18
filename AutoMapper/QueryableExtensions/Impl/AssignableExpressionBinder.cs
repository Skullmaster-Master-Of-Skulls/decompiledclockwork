using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x0200005D RID: 93
	public class AssignableExpressionBinder : IExpressionBinder
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000895A File Offset: 0x00006B5A
		public bool IsMatch(PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionResolutionResult result)
		{
			return propertyMap.DestinationPropertyType.IsAssignableFrom(result.Type);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000896D File Offset: 0x00006B6D
		public MemberAssignment Build(IConfigurationProvider configuration, PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionRequest request, ExpressionResolutionResult result, ConcurrentDictionary<ExpressionRequest, int> typePairCount)
		{
			return AssignableExpressionBinder.BindAssignableExpression(propertyMap, result);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00008977 File Offset: 0x00006B77
		private static MemberAssignment BindAssignableExpression(PropertyMap propertyMap, ExpressionResolutionResult result)
		{
			return Expression.Bind(propertyMap.DestinationProperty.MemberInfo, result.ResolutionExpression);
		}
	}
}

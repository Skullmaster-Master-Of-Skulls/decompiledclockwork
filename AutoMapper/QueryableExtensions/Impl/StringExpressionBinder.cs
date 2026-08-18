using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x0200006E RID: 110
	public class StringExpressionBinder : IExpressionBinder
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0000984F File Offset: 0x00007A4F
		public bool IsMatch(PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionResolutionResult result)
		{
			return propertyMap.DestinationPropertyType == typeof(string);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00009866 File Offset: 0x00007A66
		public MemberAssignment Build(IConfigurationProvider configuration, PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionRequest request, ExpressionResolutionResult result, ConcurrentDictionary<ExpressionRequest, int> typePairCount)
		{
			return StringExpressionBinder.BindStringExpression(propertyMap, result);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00009870 File Offset: 0x00007A70
		private static MemberAssignment BindStringExpression(PropertyMap propertyMap, ExpressionResolutionResult result)
		{
			return Expression.Bind(propertyMap.DestinationProperty.MemberInfo, Expression.Call(result.ResolutionExpression, "ToString", null, null));
		}
	}
}

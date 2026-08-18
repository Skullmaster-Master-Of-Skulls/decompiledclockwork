using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x02000060 RID: 96
	public class MappedTypeExpressionBinder : IExpressionBinder
	{
		// Token: 0x0600037D RID: 893 RVA: 0x00008C54 File Offset: 0x00006E54
		public bool IsMatch(PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionResolutionResult result)
		{
			return propertyTypeMap != null && propertyTypeMap.CustomProjection == null;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00008C64 File Offset: 0x00006E64
		public MemberAssignment Build(IConfigurationProvider configuration, PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionRequest request, ExpressionResolutionResult result, ConcurrentDictionary<ExpressionRequest, int> typePairCount)
		{
			return MappedTypeExpressionBinder.BindMappedTypeExpression(configuration, propertyMap, request, result, typePairCount);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00008C74 File Offset: 0x00006E74
		private static MemberAssignment BindMappedTypeExpression(IConfigurationProvider configuration, PropertyMap propertyMap, ExpressionRequest request, ExpressionResolutionResult result, ConcurrentDictionary<ExpressionRequest, int> typePairCount)
		{
			Expression expression = configuration.ExpressionBuilder.CreateMapExpression(request, result.ResolutionExpression, typePairCount);
			if (expression == null)
			{
				return null;
			}
			if (configuration.AllowNullDestinationValues)
			{
				ConstantExpression ifFalse = Expression.Constant(null, propertyMap.DestinationPropertyType);
				expression = Expression.Condition(Expression.NotEqual(result.ResolutionExpression, Expression.Constant(null)), expression, ifFalse);
			}
			return Expression.Bind(propertyMap.DestinationProperty.MemberInfo, expression);
		}
	}
}

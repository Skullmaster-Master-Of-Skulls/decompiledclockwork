using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using AutoMapper.Internal;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x02000064 RID: 100
	public class NullableExpressionBinder : IExpressionBinder
	{
		// Token: 0x0600038D RID: 909 RVA: 0x00008E01 File Offset: 0x00007001
		public bool IsMatch(PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionResolutionResult result)
		{
			return propertyMap.DestinationPropertyType.IsNullableType() && !result.Type.IsNullableType();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00008E20 File Offset: 0x00007020
		public MemberAssignment Build(IConfigurationProvider configuration, PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionRequest request, ExpressionResolutionResult result, ConcurrentDictionary<ExpressionRequest, int> typePairCount)
		{
			return NullableExpressionBinder.BindNullableExpression(propertyMap, result);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00008E2C File Offset: 0x0000702C
		private static MemberAssignment BindNullableExpression(PropertyMap propertyMap, ExpressionResolutionResult result)
		{
			if (result.ResolutionExpression.NodeType == ExpressionType.MemberAccess)
			{
				MemberExpression memberExpression = (MemberExpression)result.ResolutionExpression;
				if (memberExpression.Expression != null && memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
				{
					Type destinationPropertyType = propertyMap.DestinationPropertyType;
					Expression expression = memberExpression.Expression;
					Expression expression2 = Expression.Convert(memberExpression, destinationPropertyType);
					UnaryExpression ifTrue = Expression.Convert(Expression.Constant(null), destinationPropertyType);
					while (expression.NodeType != ExpressionType.Parameter)
					{
						memberExpression = (MemberExpression)memberExpression.Expression;
						expression = memberExpression.Expression;
						expression2 = Expression.Condition(Expression.Equal(memberExpression, Expression.Constant(null)), ifTrue, expression2);
					}
					return Expression.Bind(propertyMap.DestinationProperty.MemberInfo, expression2);
				}
			}
			return Expression.Bind(propertyMap.DestinationProperty.MemberInfo, Expression.Convert(result.ResolutionExpression, propertyMap.DestinationPropertyType));
		}
	}
}

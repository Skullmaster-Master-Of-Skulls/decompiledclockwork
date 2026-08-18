using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x0200005F RID: 95
	public class EnumerableExpressionBinder : IExpressionBinder
	{
		// Token: 0x06000377 RID: 887 RVA: 0x000089E8 File Offset: 0x00006BE8
		public bool IsMatch(PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionResolutionResult result)
		{
			return propertyMap.DestinationPropertyType.GetTypeInfo().ImplementedInterfaces.Any((Type t) => t.Name == "IEnumerable") && propertyMap.DestinationPropertyType != typeof(string);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00008A42 File Offset: 0x00006C42
		public MemberAssignment Build(IConfigurationProvider configuration, PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionRequest request, ExpressionResolutionResult result, ConcurrentDictionary<ExpressionRequest, int> typePairCount)
		{
			return EnumerableExpressionBinder.BindEnumerableExpression(configuration, propertyMap, request, result, typePairCount);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00008A54 File Offset: 0x00006C54
		private static MemberAssignment BindEnumerableExpression(IConfigurationProvider configuration, PropertyMap propertyMap, ExpressionRequest request, ExpressionResolutionResult result, ConcurrentDictionary<ExpressionRequest, int> typePairCount)
		{
			Type destinationListTypeFor = EnumerableExpressionBinder.GetDestinationListTypeFor(propertyMap);
			Type type = result.Type.IsArray ? result.Type.GetElementType() : result.Type.GetTypeInfo().GenericTypeArguments.First<Type>();
			ExpressionRequest request2 = new ExpressionRequest(type, destinationListTypeFor, request.MembersToExpand);
			Expression expression = result.ResolutionExpression;
			if (type != destinationListTypeFor)
			{
				LambdaExpression lambdaExpression = configuration.ExpressionBuilder.CreateMapExpression(request2, typePairCount);
				if (lambdaExpression == null)
				{
					return null;
				}
				expression = Expression.Call(typeof(Enumerable), "Select", new Type[]
				{
					type,
					destinationListTypeFor
				}, new Expression[]
				{
					result.ResolutionExpression,
					lambdaExpression
				});
			}
			MemberAssignment result2;
			if (typeof(IList<>).MakeGenericType(new Type[]
			{
				destinationListTypeFor
			}).GetTypeInfo().IsAssignableFrom(propertyMap.DestinationPropertyType.GetTypeInfo()) || typeof(ICollection<>).MakeGenericType(new Type[]
			{
				destinationListTypeFor
			}).GetTypeInfo().IsAssignableFrom(propertyMap.DestinationPropertyType.GetTypeInfo()))
			{
				MethodCallExpression toListCallExpression = EnumerableExpressionBinder.GetToListCallExpression(propertyMap, destinationListTypeFor, expression);
				result2 = Expression.Bind(propertyMap.DestinationProperty.MemberInfo, toListCallExpression);
			}
			else if (propertyMap.DestinationPropertyType.IsArray)
			{
				MethodCallExpression expression2 = Expression.Call(typeof(Enumerable), "ToArray", new Type[]
				{
					destinationListTypeFor
				}, new Expression[]
				{
					expression
				});
				result2 = Expression.Bind(propertyMap.DestinationProperty.MemberInfo, expression2);
			}
			else
			{
				result2 = Expression.Bind(propertyMap.DestinationProperty.MemberInfo, expression);
			}
			return result2;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00008BE6 File Offset: 0x00006DE6
		private static Type GetDestinationListTypeFor(PropertyMap propertyMap)
		{
			if (!propertyMap.DestinationPropertyType.IsArray)
			{
				return propertyMap.DestinationPropertyType.GetTypeInfo().GenericTypeArguments.First<Type>();
			}
			return propertyMap.DestinationPropertyType.GetElementType();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00008C16 File Offset: 0x00006E16
		private static MethodCallExpression GetToListCallExpression(PropertyMap propertyMap, Type destinationListType, Expression selectExpression)
		{
			return Expression.Call(typeof(Enumerable), propertyMap.DestinationPropertyType.IsArray ? "ToArray" : "ToList", new Type[]
			{
				destinationListType
			}, new Expression[]
			{
				selectExpression
			});
		}
	}
}

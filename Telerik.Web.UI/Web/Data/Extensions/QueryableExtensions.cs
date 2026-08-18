using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Web.Data.Expressions;

namespace Telerik.Web.Data.Extensions
{
	// Token: 0x02001B8A RID: 7050
	public static class QueryableExtensions
	{
		// Token: 0x06011145 RID: 69957 RVA: 0x003C47C0 File Offset: 0x003C29C0
		private static IQueryable CallQueryableMethod(this IQueryable source, string methodName, LambdaExpression selector)
		{
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), methodName, new Type[]
			{
				source.ElementType,
				selector.Body.Type
			}, new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06011146 RID: 69958 RVA: 0x003C4824 File Offset: 0x003C2A24
		public static IQueryable Sort(this IQueryable source, IEnumerable<SortDescriptor> sortDescriptors)
		{
			SortDescriptorCollectionExpressionBuilder sortDescriptorCollectionExpressionBuilder = new SortDescriptorCollectionExpressionBuilder(source, sortDescriptors);
			return sortDescriptorCollectionExpressionBuilder.Sort();
		}

		// Token: 0x06011147 RID: 69959 RVA: 0x003C4840 File Offset: 0x003C2A40
		public static IQueryable Page(this IQueryable source, int startIndex, int pageSize)
		{
			IQueryable queryable = source;
			if (startIndex > 0)
			{
				queryable = queryable.Skip(startIndex);
			}
			if (pageSize > 0)
			{
				queryable = queryable.Take(pageSize);
			}
			return queryable;
		}

		// Token: 0x06011148 RID: 69960 RVA: 0x003C4868 File Offset: 0x003C2A68
		public static IQueryable Select(this IQueryable source, LambdaExpression selector)
		{
			return source.CallQueryableMethod("Select", selector);
		}

		// Token: 0x06011149 RID: 69961 RVA: 0x003C4876 File Offset: 0x003C2A76
		public static IQueryable GroupBy(this IQueryable source, LambdaExpression keySelector)
		{
			return source.CallQueryableMethod("GroupBy", keySelector);
		}

		// Token: 0x0601114A RID: 69962 RVA: 0x003C4884 File Offset: 0x003C2A84
		public static IQueryable OrderBy(this IQueryable source, LambdaExpression keySelector)
		{
			return source.CallQueryableMethod("OrderBy", keySelector);
		}

		// Token: 0x0601114B RID: 69963 RVA: 0x003C4892 File Offset: 0x003C2A92
		public static IQueryable OrderByDescending(this IQueryable source, LambdaExpression keySelector)
		{
			return source.CallQueryableMethod("OrderByDescending", keySelector);
		}

		// Token: 0x0601114C RID: 69964 RVA: 0x003C48A0 File Offset: 0x003C2AA0
		public static IQueryable OrderBy(this IQueryable source, LambdaExpression keySelector, ListSortDirection? sortDirection)
		{
			if (sortDirection == null)
			{
				return source;
			}
			if (sortDirection.Value == ListSortDirection.Ascending)
			{
				return source.OrderBy(keySelector);
			}
			return source.OrderByDescending(keySelector);
		}

		// Token: 0x0601114D RID: 69965 RVA: 0x003C48C8 File Offset: 0x003C2AC8
		public static IQueryable GroupBy(this IQueryable source, IEnumerable<IGroupDescriptor> groupDescriptors)
		{
			GroupDescriptorCollectionExpressionBuilder groupDescriptorCollectionExpressionBuilder = new GroupDescriptorCollectionExpressionBuilder(source, groupDescriptors);
			return groupDescriptorCollectionExpressionBuilder.CreateQuery();
		}

		// Token: 0x0601114E RID: 69966 RVA: 0x003C48E4 File Offset: 0x003C2AE4
		public static AggregateResultCollection Aggregate(this IQueryable source, IEnumerable<AggregateFunction> aggregateFunctions)
		{
			List<AggregateFunction> list = aggregateFunctions.ToList<AggregateFunction>();
			if (list.Count > 0)
			{
				QueryableAggregatesGroupDescriptor queryableAggregatesGroupDescriptor = new QueryableAggregatesGroupDescriptor(list);
				IQueryable queryable = source.GroupBy(new QueryableAggregatesGroupDescriptor[]
				{
					queryableAggregatesGroupDescriptor
				});
				using (IEnumerator enumerator = queryable.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						AggregateFunctionsGroup aggregateFunctionsGroup = (AggregateFunctionsGroup)enumerator.Current;
						return aggregateFunctionsGroup.GetAggregateResults(list);
					}
				}
			}
			return new AggregateResultCollection();
		}

		// Token: 0x0601114F RID: 69967 RVA: 0x003C4978 File Offset: 0x003C2B78
		public static AggregateResultCollection Aggregate(this IQueryable source, AggregateFunction aggregateFunction)
		{
			return source.Aggregate(new List<AggregateFunction>
			{
				aggregateFunction
			});
		}

		// Token: 0x06011150 RID: 69968 RVA: 0x003C499C File Offset: 0x003C2B9C
		public static IQueryable Where(this IQueryable source, Expression predicate)
		{
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Where", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06011151 RID: 69969 RVA: 0x003C49F4 File Offset: 0x003C2BF4
		public static IQueryable Where(this IQueryable source, IEnumerable<IFilterDescriptor> filterDescriptors)
		{
			if (filterDescriptors.Count() > 0)
			{
				ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, "item");
				FilterDescriptorCollectionExpressionBuilder filterDescriptorCollectionExpressionBuilder = new FilterDescriptorCollectionExpressionBuilder(parameterExpression, filterDescriptors);
				LambdaExpression predicate = filterDescriptorCollectionExpressionBuilder.CreateFilterExpression();
				return source.Where(predicate);
			}
			return source;
		}

		// Token: 0x06011152 RID: 69970 RVA: 0x003C4A34 File Offset: 0x003C2C34
		internal static IQueryable SelectDistinct(this IQueryable source, Type propertyType, string propertyName)
		{
			MemberAccessExpressionBuilderBase memberAccessExpressionBuilderBase = ExpressionBuilderFactory.MemberAccess(source, propertyType, propertyName);
			LambdaExpression lambdaExpression = memberAccessExpressionBuilderBase.CreateLambdaExpression();
			IQueryable queryable = source.Select(lambdaExpression);
			return queryable.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Distinct", new Type[]
			{
				lambdaExpression.Body.Type
			}, new Expression[]
			{
				queryable.Expression
			}));
		}

		// Token: 0x06011153 RID: 69971 RVA: 0x003C4AA4 File Offset: 0x003C2CA4
		internal static IQueryable Ordered(this IQueryable source)
		{
			IdentityExpressionBuilder identityExpressionBuilder = new IdentityExpressionBuilder(source.ElementType);
			LambdaExpression keySelector = identityExpressionBuilder.CreateLambdaExpression();
			return source.OrderBy(keySelector);
		}

		// Token: 0x06011154 RID: 69972 RVA: 0x003C4ACC File Offset: 0x003C2CCC
		public static IQueryable Take(this IQueryable source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Take", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression,
				Expression.Constant(count)
			}));
		}

		// Token: 0x06011155 RID: 69973 RVA: 0x003C4B38 File Offset: 0x003C2D38
		public static IQueryable Skip(this IQueryable source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Skip", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression,
				Expression.Constant(count)
			}));
		}

		// Token: 0x06011156 RID: 69974 RVA: 0x003C4BA4 File Offset: 0x003C2DA4
		public static int Count(this IQueryable source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return (int)source.Provider.Execute(Expression.Call(typeof(Queryable), "Count", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06011157 RID: 69975 RVA: 0x003C4C08 File Offset: 0x003C2E08
		public static object ElementAt(this IQueryable source, int index)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return source.Provider.Execute(Expression.Call(typeof(Queryable), "ElementAt", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression,
				Expression.Constant(index)
			}));
		}

		// Token: 0x06011158 RID: 69976 RVA: 0x003C4C84 File Offset: 0x003C2E84
		public static IList ToIList(this IQueryable source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
			{
				source.ElementType
			}));
			foreach (object value in source)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x06011159 RID: 69977 RVA: 0x003C4D14 File Offset: 0x003C2F14
		internal static bool IsBindableType(Type type)
		{
			return type.IsPrimitive || !(type != typeof(string)) || !(type != typeof(DateTime)) || !(type != typeof(TimeSpan)) || !(type != typeof(decimal)) || !(type != typeof(Guid)) || type.IsEnum || (type.IsValueType && type.IsGenericType && type.GetGenericArguments().Length == 1 && QueryableExtensions.IsBindableType(type.GetGenericArguments()[0]));
		}
	}
}

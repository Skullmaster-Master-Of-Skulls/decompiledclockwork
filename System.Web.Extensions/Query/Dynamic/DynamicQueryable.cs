using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.Web.Query.Dynamic
{
	// Token: 0x02000037 RID: 55
	internal static class DynamicQueryable
	{
		// Token: 0x0600021E RID: 542 RVA: 0x0000D420 File Offset: 0x0000B620
		public static IQueryable<T> Where<T>(this IQueryable<T> source, string predicate, params object[] values)
		{
			return (IQueryable<T>)source.Where(predicate, values);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000D430 File Offset: 0x0000B630
		public static IQueryable Where(this IQueryable source, string predicate, params object[] values)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			LambdaExpression expression = DynamicExpression.ParseLambda(source.ElementType, typeof(bool), predicate, values);
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Where", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression,
				Expression.Quote(expression)
			}));
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		public static IQueryable Select(this IQueryable source, string selector, params object[] values)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			LambdaExpression lambdaExpression = DynamicExpression.ParseLambda(source.ElementType, null, selector, values);
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Select", new Type[]
			{
				source.ElementType,
				lambdaExpression.Body.Type
			}, new Expression[]
			{
				source.Expression,
				Expression.Quote(lambdaExpression)
			}));
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000D544 File Offset: 0x0000B744
		public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, params object[] values)
		{
			return (IQueryable<T>)source.OrderBy(ordering, values);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000D554 File Offset: 0x0000B754
		public static IQueryable OrderBy(this IQueryable source, string ordering, params object[] values)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (ordering == null)
			{
				throw new ArgumentNullException("ordering");
			}
			ParameterExpression[] parameters = new ParameterExpression[]
			{
				Expression.Parameter(source.ElementType, "")
			};
			ExpressionParser expressionParser = new ExpressionParser(parameters, ordering, values);
			IEnumerable<DynamicOrdering> enumerable = expressionParser.ParseOrdering();
			Expression expression = source.Expression;
			string text = "OrderBy";
			string text2 = "OrderByDescending";
			foreach (DynamicOrdering dynamicOrdering in enumerable)
			{
				expression = Expression.Call(typeof(Queryable), dynamicOrdering.Ascending ? text : text2, new Type[]
				{
					source.ElementType,
					dynamicOrdering.Selector.Type
				}, new Expression[]
				{
					expression,
					Expression.Quote(DynamicExpression.Lambda(dynamicOrdering.Selector, parameters))
				});
				text = "ThenBy";
				text2 = "ThenByDescending";
			}
			return source.Provider.CreateQuery(expression);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000D66C File Offset: 0x0000B86C
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

		// Token: 0x06000224 RID: 548 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
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

		// Token: 0x06000225 RID: 549 RVA: 0x0000D73C File Offset: 0x0000B93C
		public static IQueryable GroupBy(this IQueryable source, string keySelector, string elementSelector, params object[] values)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			LambdaExpression lambdaExpression = DynamicExpression.ParseLambda(source.ElementType, null, keySelector, values);
			LambdaExpression lambdaExpression2 = DynamicExpression.ParseLambda(source.ElementType, null, elementSelector, values);
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "GroupBy", new Type[]
			{
				source.ElementType,
				lambdaExpression.Body.Type,
				lambdaExpression2.Body.Type
			}, new Expression[]
			{
				source.Expression,
				Expression.Quote(lambdaExpression),
				Expression.Quote(lambdaExpression2)
			}));
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000D7FC File Offset: 0x0000B9FC
		public static bool Any(this IQueryable source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return (bool)source.Provider.Execute(Expression.Call(typeof(Queryable), "Any", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000D85C File Offset: 0x0000BA5C
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
	}
}

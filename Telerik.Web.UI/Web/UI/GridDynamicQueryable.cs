using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x0200036E RID: 878
	internal static class GridDynamicQueryable
	{
		// Token: 0x06001E26 RID: 7718 RVA: 0x0005DC7B File Offset: 0x0005BE7B
		public static IQueryable<T> Where<T>(this IQueryable<T> source, string predicate, params object[] values)
		{
			return (IQueryable<T>)source.Where(predicate, values);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0005DC8C File Offset: 0x0005BE8C
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

		// Token: 0x06001E28 RID: 7720 RVA: 0x0005DD18 File Offset: 0x0005BF18
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

		// Token: 0x06001E29 RID: 7721 RVA: 0x0005DDA8 File Offset: 0x0005BFA8
		internal static bool ShouldApplyComparer(Type type)
		{
			return type == typeof(DataRow) || type == typeof(DataRowView) || type.GetInterface("IDataRecord") != null;
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0005DDE4 File Offset: 0x0005BFE4
		internal static IComparer<object> GetComparer(Type type)
		{
			Comparer<object> @default = Comparer<object>.Default;
			Type type2 = typeof(GridDynamicQueryable.NullValuesComparer<>).MakeGenericType(new Type[]
			{
				type
			});
			return (IComparer<object>)Activator.CreateInstance(type2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[]
			{
				@default
			}, null);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0005DE2E File Offset: 0x0005C02E
		public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, params object[] values)
		{
			return (IQueryable<T>)source.OrderBy(ordering, values);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0005DE40 File Offset: 0x0005C040
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
				if (GridDynamicQueryable.ShouldApplyComparer(source.ElementType))
				{
					Type elementType = source.ElementType;
					elementType.BaseType == typeof(DynamicClass);
					expression = Expression.Call(typeof(Queryable), dynamicOrdering.Ascending ? text : text2, new Type[]
					{
						elementType,
						typeof(object)
					}, new Expression[]
					{
						expression,
						Expression.Quote(Expression.Lambda(dynamicOrdering.Selector, parameters)),
						Expression.Constant(GridDynamicQueryable.GetComparer(typeof(object)))
					});
				}
				else
				{
					expression = Expression.Call(typeof(Queryable), dynamicOrdering.Ascending ? text : text2, new Type[]
					{
						source.ElementType,
						dynamicOrdering.Selector.Type
					}, new Expression[]
					{
						expression,
						Expression.Quote(Expression.Lambda(dynamicOrdering.Selector, parameters))
					});
				}
				text = "ThenBy";
				text2 = "ThenByDescending";
			}
			return source.Provider.CreateQuery(expression);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0005E028 File Offset: 0x0005C228
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

		// Token: 0x06001E2E RID: 7726 RVA: 0x0005E094 File Offset: 0x0005C294
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

		// Token: 0x06001E2F RID: 7727 RVA: 0x0005E100 File Offset: 0x0005C300
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

		// Token: 0x06001E30 RID: 7728 RVA: 0x0005E1C4 File Offset: 0x0005C3C4
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

		// Token: 0x06001E31 RID: 7729 RVA: 0x0005E228 File Offset: 0x0005C428
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

		// Token: 0x0200036F RID: 879
		internal class NullValuesComparer<TElement> : IComparer<object>, IComparer
		{
			// Token: 0x06001E32 RID: 7730 RVA: 0x0005E289 File Offset: 0x0005C489
			public NullValuesComparer(IComparer<object> nullIncapableComparer)
			{
				this.nonNullComparer = nullIncapableComparer;
			}

			// Token: 0x06001E33 RID: 7731 RVA: 0x0005E298 File Offset: 0x0005C498
			public int Compare(object x, object y)
			{
				if (GridDynamicQueryable.NullValuesComparer<TElement>.IsNull(x) && GridDynamicQueryable.NullValuesComparer<TElement>.IsNull(y))
				{
					return 0;
				}
				if (GridDynamicQueryable.NullValuesComparer<TElement>.IsNull(x))
				{
					return -1;
				}
				if (GridDynamicQueryable.NullValuesComparer<TElement>.IsNull(y))
				{
					return 1;
				}
				return this.nonNullComparer.Compare(x, y);
			}

			// Token: 0x06001E34 RID: 7732 RVA: 0x0005E2CD File Offset: 0x0005C4CD
			private static bool IsNull(object obj)
			{
				return obj == null || obj == DBNull.Value;
			}

			// Token: 0x0400077A RID: 1914
			private IComparer<object> nonNullComparer;
		}
	}
}

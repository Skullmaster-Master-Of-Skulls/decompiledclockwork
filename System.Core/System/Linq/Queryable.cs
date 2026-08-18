using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
	// Token: 0x0200014E RID: 334
	[__DynamicallyInvokable]
	public static class Queryable
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00026469 File Offset: 0x00024669
		private static MethodInfo GetMethodInfo<T1, T2>(Func<T1, T2> f, T1 unused1)
		{
			return f.Method;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00026471 File Offset: 0x00024671
		private static MethodInfo GetMethodInfo<T1, T2, T3>(Func<T1, T2, T3> f, T1 unused1, T2 unused2)
		{
			return f.Method;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00026479 File Offset: 0x00024679
		private static MethodInfo GetMethodInfo<T1, T2, T3, T4>(Func<T1, T2, T3, T4> f, T1 unused1, T2 unused2, T3 unused3)
		{
			return f.Method;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00026481 File Offset: 0x00024681
		private static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5> f, T1 unused1, T2 unused2, T3 unused3, T4 unused4)
		{
			return f.Method;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00026489 File Offset: 0x00024689
		private static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6> f, T1 unused1, T2 unused2, T3 unused3, T4 unused4, T5 unused5)
		{
			return f.Method;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00026491 File Offset: 0x00024691
		private static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7> f, T1 unused1, T2 unused2, T3 unused3, T4 unused4, T5 unused5, T6 unused6)
		{
			return f.Method;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00026499 File Offset: 0x00024699
		[__DynamicallyInvokable]
		public static IQueryable<TElement> AsQueryable<TElement>(this IEnumerable<TElement> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (source is IQueryable<TElement>)
			{
				return (IQueryable<TElement>)source;
			}
			return new EnumerableQuery<TElement>(source);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000264C0 File Offset: 0x000246C0
		[__DynamicallyInvokable]
		public static IQueryable AsQueryable(this IEnumerable source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (source is IQueryable)
			{
				return (IQueryable)source;
			}
			Type type = TypeHelper.FindGenericType(typeof(IEnumerable<>), source.GetType());
			if (type == null)
			{
				throw Error.ArgumentNotIEnumerableGeneric("source");
			}
			return EnumerableQuery.Create(type.GetGenericArguments()[0], source);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00026524 File Offset: 0x00024724
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, IQueryable<TSource>>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, IQueryable<TSource>>(Queryable.Where<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002658C File Offset: 0x0002478C
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, int, bool>>, IQueryable<TSource>>(new Func<IQueryable<TSource>, Expression<Func<TSource, int, bool>>, IQueryable<TSource>>(Queryable.Where<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000265F4 File Offset: 0x000247F4
		[__DynamicallyInvokable]
		public static IQueryable<TResult> OfType<TResult>(this IQueryable source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable, IQueryable<TResult>>(new Func<IQueryable, IQueryable<TResult>>(Queryable.OfType<TResult>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00026644 File Offset: 0x00024844
		[__DynamicallyInvokable]
		public static IQueryable<TResult> Cast<TResult>(this IQueryable source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable, IQueryable<TResult>>(new Func<IQueryable, IQueryable<TResult>>(Queryable.Cast<TResult>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00026694 File Offset: 0x00024894
		[__DynamicallyInvokable]
		public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TResult>>, IQueryable<TResult>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TResult>>, IQueryable<TResult>>(Queryable.Select<TSource, TResult>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000266FC File Offset: 0x000248FC
		[__DynamicallyInvokable]
		public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, int, TResult>>, IQueryable<TResult>>(new Func<IQueryable<TSource>, Expression<Func<TSource, int, TResult>>, IQueryable<TResult>>(Queryable.Select<TSource, TResult>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00026764 File Offset: 0x00024964
		[__DynamicallyInvokable]
		public static IQueryable<TResult> SelectMany<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, IEnumerable<TResult>>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, IEnumerable<TResult>>>, IQueryable<TResult>>(new Func<IQueryable<TSource>, Expression<Func<TSource, IEnumerable<TResult>>>, IQueryable<TResult>>(Queryable.SelectMany<TSource, TResult>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000267CC File Offset: 0x000249CC
		[__DynamicallyInvokable]
		public static IQueryable<TResult> SelectMany<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, IEnumerable<TResult>>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, int, IEnumerable<TResult>>>, IQueryable<TResult>>(new Func<IQueryable<TSource>, Expression<Func<TSource, int, IEnumerable<TResult>>>, IQueryable<TResult>>(Queryable.SelectMany<TSource, TResult>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00026834 File Offset: 0x00024A34
		[__DynamicallyInvokable]
		public static IQueryable<TResult> SelectMany<TSource, TCollection, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, IEnumerable<TCollection>>> collectionSelector, Expression<Func<TSource, TCollection, TResult>> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (collectionSelector == null)
			{
				throw Error.ArgumentNull("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, int, IEnumerable<TCollection>>>, Expression<Func<TSource, TCollection, TResult>>, IQueryable<TResult>>(new Func<IQueryable<TSource>, Expression<Func<TSource, int, IEnumerable<TCollection>>>, Expression<Func<TSource, TCollection, TResult>>, IQueryable<TResult>>(Queryable.SelectMany<TSource, TCollection, TResult>), source, collectionSelector, resultSelector), new Expression[]
			{
				source.Expression,
				Expression.Quote(collectionSelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000268B4 File Offset: 0x00024AB4
		[__DynamicallyInvokable]
		public static IQueryable<TResult> SelectMany<TSource, TCollection, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, IEnumerable<TCollection>>> collectionSelector, Expression<Func<TSource, TCollection, TResult>> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (collectionSelector == null)
			{
				throw Error.ArgumentNull("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, IEnumerable<TCollection>>>, Expression<Func<TSource, TCollection, TResult>>, IQueryable<TResult>>(new Func<IQueryable<TSource>, Expression<Func<TSource, IEnumerable<TCollection>>>, Expression<Func<TSource, TCollection, TResult>>, IQueryable<TResult>>(Queryable.SelectMany<TSource, TCollection, TResult>), source, collectionSelector, resultSelector), new Expression[]
			{
				source.Expression,
				Expression.Quote(collectionSelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00026934 File Offset: 0x00024B34
		private static Expression GetSourceExpression<TSource>(IEnumerable<TSource> source)
		{
			IQueryable<TSource> queryable = source as IQueryable<TSource>;
			if (queryable != null)
			{
				return queryable.Expression;
			}
			return Expression.Constant(source, typeof(IEnumerable<TSource>));
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00026964 File Offset: 0x00024B64
		[__DynamicallyInvokable]
		public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return outer.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter, TInner, TResult>>, IQueryable<TResult>>(new Func<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter, TInner, TResult>>, IQueryable<TResult>>(Queryable.Join<TOuter, TInner, TKey, TResult>), outer, inner, outerKeySelector, innerKeySelector, resultSelector), new Expression[]
			{
				outer.Expression,
				Queryable.GetSourceExpression<TInner>(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00026A14 File Offset: 0x00024C14
		[__DynamicallyInvokable]
		public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return outer.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter, TInner, TResult>>, IEqualityComparer<TKey>, IQueryable<TResult>>(new Func<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter, TInner, TResult>>, IEqualityComparer<TKey>, IQueryable<TResult>>(Queryable.Join<TOuter, TInner, TKey, TResult>), outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer), new Expression[]
			{
				outer.Expression,
				Queryable.GetSourceExpression<TInner>(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))
			}));
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00026ADC File Offset: 0x00024CDC
		[__DynamicallyInvokable]
		public static IQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return outer.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter, IEnumerable<TInner>, TResult>>, IQueryable<TResult>>(new Func<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter, IEnumerable<TInner>, TResult>>, IQueryable<TResult>>(Queryable.GroupJoin<TOuter, TInner, TKey, TResult>), outer, inner, outerKeySelector, innerKeySelector, resultSelector), new Expression[]
			{
				outer.Expression,
				Queryable.GetSourceExpression<TInner>(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00026B8C File Offset: 0x00024D8C
		[__DynamicallyInvokable]
		public static IQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return outer.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter, IEnumerable<TInner>, TResult>>, IEqualityComparer<TKey>, IQueryable<TResult>>(new Func<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter, IEnumerable<TInner>, TResult>>, IEqualityComparer<TKey>, IQueryable<TResult>>(Queryable.GroupJoin<TOuter, TInner, TKey, TResult>), outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer), new Expression[]
			{
				outer.Expression,
				Queryable.GetSourceExpression<TInner>(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))
			}));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00026C54 File Offset: 0x00024E54
		[__DynamicallyInvokable]
		public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IOrderedQueryable<TSource>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IOrderedQueryable<TSource>>(Queryable.OrderBy<TSource, TKey>), source, keySelector), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector)
			}));
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00026CC0 File Offset: 0x00024EC0
		[__DynamicallyInvokable]
		public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IComparer<TKey>, IOrderedQueryable<TSource>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IComparer<TKey>, IOrderedQueryable<TSource>>(Queryable.OrderBy<TSource, TKey>), source, keySelector, comparer), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Constant(comparer, typeof(IComparer<TKey>))
			}));
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00026D40 File Offset: 0x00024F40
		[__DynamicallyInvokable]
		public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IOrderedQueryable<TSource>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IOrderedQueryable<TSource>>(Queryable.OrderByDescending<TSource, TKey>), source, keySelector), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector)
			}));
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00026DAC File Offset: 0x00024FAC
		[__DynamicallyInvokable]
		public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IComparer<TKey>, IOrderedQueryable<TSource>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IComparer<TKey>, IOrderedQueryable<TSource>>(Queryable.OrderByDescending<TSource, TKey>), source, keySelector, comparer), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Constant(comparer, typeof(IComparer<TKey>))
			}));
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00026E2C File Offset: 0x0002502C
		[__DynamicallyInvokable]
		public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IOrderedQueryable<TSource>, Expression<Func<TSource, TKey>>, IOrderedQueryable<TSource>>(new Func<IOrderedQueryable<TSource>, Expression<Func<TSource, TKey>>, IOrderedQueryable<TSource>>(Queryable.ThenBy<TSource, TKey>), source, keySelector), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector)
			}));
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00026E98 File Offset: 0x00025098
		[__DynamicallyInvokable]
		public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IOrderedQueryable<TSource>, Expression<Func<TSource, TKey>>, IComparer<TKey>, IOrderedQueryable<TSource>>(new Func<IOrderedQueryable<TSource>, Expression<Func<TSource, TKey>>, IComparer<TKey>, IOrderedQueryable<TSource>>(Queryable.ThenBy<TSource, TKey>), source, keySelector, comparer), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Constant(comparer, typeof(IComparer<TKey>))
			}));
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00026F18 File Offset: 0x00025118
		[__DynamicallyInvokable]
		public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IOrderedQueryable<TSource>, Expression<Func<TSource, TKey>>, IOrderedQueryable<TSource>>(new Func<IOrderedQueryable<TSource>, Expression<Func<TSource, TKey>>, IOrderedQueryable<TSource>>(Queryable.ThenByDescending<TSource, TKey>), source, keySelector), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector)
			}));
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00026F84 File Offset: 0x00025184
		[__DynamicallyInvokable]
		public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IOrderedQueryable<TSource>, Expression<Func<TSource, TKey>>, IComparer<TKey>, IOrderedQueryable<TSource>>(new Func<IOrderedQueryable<TSource>, Expression<Func<TSource, TKey>>, IComparer<TKey>, IOrderedQueryable<TSource>>(Queryable.ThenByDescending<TSource, TKey>), source, keySelector, comparer), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Constant(comparer, typeof(IComparer<TKey>))
			}));
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00027004 File Offset: 0x00025204
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Take<TSource>(this IQueryable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, int, IQueryable<TSource>>(new Func<IQueryable<TSource>, int, IQueryable<TSource>>(Queryable.Take<TSource>), source, count), new Expression[]
			{
				source.Expression,
				Expression.Constant(count)
			}));
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00027060 File Offset: 0x00025260
		[__DynamicallyInvokable]
		public static IQueryable<TSource> TakeWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, IQueryable<TSource>>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, IQueryable<TSource>>(Queryable.TakeWhile<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000270C8 File Offset: 0x000252C8
		[__DynamicallyInvokable]
		public static IQueryable<TSource> TakeWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, int, bool>>, IQueryable<TSource>>(new Func<IQueryable<TSource>, Expression<Func<TSource, int, bool>>, IQueryable<TSource>>(Queryable.TakeWhile<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00027130 File Offset: 0x00025330
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Skip<TSource>(this IQueryable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, int, IQueryable<TSource>>(new Func<IQueryable<TSource>, int, IQueryable<TSource>>(Queryable.Skip<TSource>), source, count), new Expression[]
			{
				source.Expression,
				Expression.Constant(count)
			}));
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002718C File Offset: 0x0002538C
		[__DynamicallyInvokable]
		public static IQueryable<TSource> SkipWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, IQueryable<TSource>>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, IQueryable<TSource>>(Queryable.SkipWhile<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000271F4 File Offset: 0x000253F4
		[__DynamicallyInvokable]
		public static IQueryable<TSource> SkipWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, int, bool>>, IQueryable<TSource>>(new Func<IQueryable<TSource>, Expression<Func<TSource, int, bool>>, IQueryable<TSource>>(Queryable.SkipWhile<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002725C File Offset: 0x0002545C
		[__DynamicallyInvokable]
		public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return source.Provider.CreateQuery<IGrouping<TKey, TSource>>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IQueryable<IGrouping<TKey, TSource>>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IQueryable<IGrouping<TKey, TSource>>>(Queryable.GroupBy<TSource, TKey>), source, keySelector), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector)
			}));
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000272C4 File Offset: 0x000254C4
		[__DynamicallyInvokable]
		public static IQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			return source.Provider.CreateQuery<IGrouping<TKey, TElement>>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>, IQueryable<IGrouping<TKey, TElement>>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>, IQueryable<IGrouping<TKey, TElement>>>(Queryable.GroupBy<TSource, TKey, TElement>), source, keySelector, elementSelector), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(elementSelector)
			}));
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00027344 File Offset: 0x00025544
		[__DynamicallyInvokable]
		public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return source.Provider.CreateQuery<IGrouping<TKey, TSource>>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IEqualityComparer<TKey>, IQueryable<IGrouping<TKey, TSource>>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, IEqualityComparer<TKey>, IQueryable<IGrouping<TKey, TSource>>>(Queryable.GroupBy<TSource, TKey>), source, keySelector, comparer), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))
			}));
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000273C0 File Offset: 0x000255C0
		[__DynamicallyInvokable]
		public static IQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			return source.Provider.CreateQuery<IGrouping<TKey, TElement>>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>, IEqualityComparer<TKey>, IQueryable<IGrouping<TKey, TElement>>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>, IEqualityComparer<TKey>, IQueryable<IGrouping<TKey, TElement>>>(Queryable.GroupBy<TSource, TKey, TElement>), source, keySelector, elementSelector, comparer), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(elementSelector),
				Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))
			}));
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00027454 File Offset: 0x00025654
		[__DynamicallyInvokable]
		public static IQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>, Expression<Func<TKey, IEnumerable<TElement>, TResult>>, IQueryable<TResult>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>, Expression<Func<TKey, IEnumerable<TElement>, TResult>>, IQueryable<TResult>>(Queryable.GroupBy<TSource, TKey, TElement, TResult>), source, keySelector, elementSelector, resultSelector), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(elementSelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000274EC File Offset: 0x000256EC
		[__DynamicallyInvokable]
		public static IQueryable<TResult> GroupBy<TSource, TKey, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TKey, IEnumerable<TSource>, TResult>>, IQueryable<TResult>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TKey, IEnumerable<TSource>, TResult>>, IQueryable<TResult>>(Queryable.GroupBy<TSource, TKey, TResult>), source, keySelector, resultSelector), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002756C File Offset: 0x0002576C
		[__DynamicallyInvokable]
		public static IQueryable<TResult> GroupBy<TSource, TKey, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TKey, IEnumerable<TSource>, TResult>>, IEqualityComparer<TKey>, IQueryable<TResult>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TKey, IEnumerable<TSource>, TResult>>, IEqualityComparer<TKey>, IQueryable<TResult>>(Queryable.GroupBy<TSource, TKey, TResult>), source, keySelector, resultSelector, comparer), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))
			}));
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00027600 File Offset: 0x00025800
		[__DynamicallyInvokable]
		public static IQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>, Expression<Func<TKey, IEnumerable<TElement>, TResult>>, IEqualityComparer<TKey>, IQueryable<TResult>>(new Func<IQueryable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>, Expression<Func<TKey, IEnumerable<TElement>, TResult>>, IEqualityComparer<TKey>, IQueryable<TResult>>(Queryable.GroupBy<TSource, TKey, TElement, TResult>), source, keySelector, elementSelector, resultSelector, comparer), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(elementSelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))
			}));
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000276AC File Offset: 0x000258AC
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Distinct<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IQueryable<TSource>>(new Func<IQueryable<TSource>, IQueryable<TSource>>(Queryable.Distinct<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000276FC File Offset: 0x000258FC
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Distinct<TSource>(this IQueryable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IEqualityComparer<TSource>, IQueryable<TSource>>(new Func<IQueryable<TSource>, IEqualityComparer<TSource>, IQueryable<TSource>>(Queryable.Distinct<TSource>), source, comparer), new Expression[]
			{
				source.Expression,
				Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))
			}));
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00027760 File Offset: 0x00025960
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Concat<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>>(new Func<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>>(Queryable.Concat<TSource>), source1, source2), new Expression[]
			{
				source1.Expression,
				Queryable.GetSourceExpression<TSource>(source2)
			}));
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000277C8 File Offset: 0x000259C8
		[__DynamicallyInvokable]
		public static IQueryable<TResult> Zip<TFirst, TSecond, TResult>(this IQueryable<TFirst> source1, IEnumerable<TSecond> source2, Expression<Func<TFirst, TSecond, TResult>> resultSelector)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source1.Provider.CreateQuery<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TFirst>, IEnumerable<TSecond>, Expression<Func<TFirst, TSecond, TResult>>, IQueryable<TResult>>(new Func<IQueryable<TFirst>, IEnumerable<TSecond>, Expression<Func<TFirst, TSecond, TResult>>, IQueryable<TResult>>(Queryable.Zip<TFirst, TSecond, TResult>), source1, source2, resultSelector), new Expression[]
			{
				source1.Expression,
				Queryable.GetSourceExpression<TSecond>(source2),
				Expression.Quote(resultSelector)
			}));
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00027848 File Offset: 0x00025A48
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Union<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>>(new Func<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>>(Queryable.Union<TSource>), source1, source2), new Expression[]
			{
				source1.Expression,
				Queryable.GetSourceExpression<TSource>(source2)
			}));
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000278B0 File Offset: 0x00025AB0
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Union<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IEnumerable<TSource>, IEqualityComparer<TSource>, IQueryable<TSource>>(new Func<IQueryable<TSource>, IEnumerable<TSource>, IEqualityComparer<TSource>, IQueryable<TSource>>(Queryable.Union<TSource>), source1, source2, comparer), new Expression[]
			{
				source1.Expression,
				Queryable.GetSourceExpression<TSource>(source2),
				Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))
			}));
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002792C File Offset: 0x00025B2C
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Intersect<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>>(new Func<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>>(Queryable.Intersect<TSource>), source1, source2), new Expression[]
			{
				source1.Expression,
				Queryable.GetSourceExpression<TSource>(source2)
			}));
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00027994 File Offset: 0x00025B94
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Intersect<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IEnumerable<TSource>, IEqualityComparer<TSource>, IQueryable<TSource>>(new Func<IQueryable<TSource>, IEnumerable<TSource>, IEqualityComparer<TSource>, IQueryable<TSource>>(Queryable.Intersect<TSource>), source1, source2, comparer), new Expression[]
			{
				source1.Expression,
				Queryable.GetSourceExpression<TSource>(source2),
				Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))
			}));
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00027A10 File Offset: 0x00025C10
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Except<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>>(new Func<IQueryable<TSource>, IEnumerable<TSource>, IQueryable<TSource>>(Queryable.Except<TSource>), source1, source2), new Expression[]
			{
				source1.Expression,
				Queryable.GetSourceExpression<TSource>(source2)
			}));
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00027A78 File Offset: 0x00025C78
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Except<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IEnumerable<TSource>, IEqualityComparer<TSource>, IQueryable<TSource>>(new Func<IQueryable<TSource>, IEnumerable<TSource>, IEqualityComparer<TSource>, IQueryable<TSource>>(Queryable.Except<TSource>), source1, source2, comparer), new Expression[]
			{
				source1.Expression,
				Queryable.GetSourceExpression<TSource>(source2),
				Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))
			}));
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00027AF4 File Offset: 0x00025CF4
		[__DynamicallyInvokable]
		public static TSource First<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TSource>(new Func<IQueryable<TSource>, TSource>(Queryable.First<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00027B44 File Offset: 0x00025D44
		[__DynamicallyInvokable]
		public static TSource First<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(Queryable.First<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00027BAC File Offset: 0x00025DAC
		[__DynamicallyInvokable]
		public static TSource FirstOrDefault<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TSource>(new Func<IQueryable<TSource>, TSource>(Queryable.FirstOrDefault<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00027BFC File Offset: 0x00025DFC
		[__DynamicallyInvokable]
		public static TSource FirstOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(Queryable.FirstOrDefault<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00027C64 File Offset: 0x00025E64
		[__DynamicallyInvokable]
		public static TSource Last<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TSource>(new Func<IQueryable<TSource>, TSource>(Queryable.Last<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00027CB4 File Offset: 0x00025EB4
		[__DynamicallyInvokable]
		public static TSource Last<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(Queryable.Last<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00027D1C File Offset: 0x00025F1C
		[__DynamicallyInvokable]
		public static TSource LastOrDefault<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TSource>(new Func<IQueryable<TSource>, TSource>(Queryable.LastOrDefault<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00027D6C File Offset: 0x00025F6C
		[__DynamicallyInvokable]
		public static TSource LastOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(Queryable.LastOrDefault<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00027DD4 File Offset: 0x00025FD4
		[__DynamicallyInvokable]
		public static TSource Single<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TSource>(new Func<IQueryable<TSource>, TSource>(Queryable.Single<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00027E24 File Offset: 0x00026024
		[__DynamicallyInvokable]
		public static TSource Single<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(Queryable.Single<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00027E8C File Offset: 0x0002608C
		[__DynamicallyInvokable]
		public static TSource SingleOrDefault<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TSource>(new Func<IQueryable<TSource>, TSource>(Queryable.SingleOrDefault<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00027EDC File Offset: 0x000260DC
		[__DynamicallyInvokable]
		public static TSource SingleOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, TSource>(Queryable.SingleOrDefault<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00027F44 File Offset: 0x00026144
		[__DynamicallyInvokable]
		public static TSource ElementAt<TSource>(this IQueryable<TSource> source, int index)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (index < 0)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, int, TSource>(new Func<IQueryable<TSource>, int, TSource>(Queryable.ElementAt<TSource>), source, index), new Expression[]
			{
				source.Expression,
				Expression.Constant(index)
			}));
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00027FB0 File Offset: 0x000261B0
		[__DynamicallyInvokable]
		public static TSource ElementAtOrDefault<TSource>(this IQueryable<TSource> source, int index)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, int, TSource>(new Func<IQueryable<TSource>, int, TSource>(Queryable.ElementAtOrDefault<TSource>), source, index), new Expression[]
			{
				source.Expression,
				Expression.Constant(index)
			}));
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002800C File Offset: 0x0002620C
		[__DynamicallyInvokable]
		public static IQueryable<TSource> DefaultIfEmpty<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IQueryable<TSource>>(new Func<IQueryable<TSource>, IQueryable<TSource>>(Queryable.DefaultIfEmpty<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002805C File Offset: 0x0002625C
		[__DynamicallyInvokable]
		public static IQueryable<TSource> DefaultIfEmpty<TSource>(this IQueryable<TSource> source, TSource defaultValue)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TSource, IQueryable<TSource>>(new Func<IQueryable<TSource>, TSource, IQueryable<TSource>>(Queryable.DefaultIfEmpty<TSource>), source, defaultValue), new Expression[]
			{
				source.Expression,
				Expression.Constant(defaultValue, typeof(TSource))
			}));
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000280C4 File Offset: 0x000262C4
		[__DynamicallyInvokable]
		public static bool Contains<TSource>(this IQueryable<TSource> source, TSource item)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<bool>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TSource, bool>(new Func<IQueryable<TSource>, TSource, bool>(Queryable.Contains<TSource>), source, item), new Expression[]
			{
				source.Expression,
				Expression.Constant(item, typeof(TSource))
			}));
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002812C File Offset: 0x0002632C
		[__DynamicallyInvokable]
		public static bool Contains<TSource>(this IQueryable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<bool>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TSource, IEqualityComparer<TSource>, bool>(new Func<IQueryable<TSource>, TSource, IEqualityComparer<TSource>, bool>(Queryable.Contains<TSource>), source, item, comparer), new Expression[]
			{
				source.Expression,
				Expression.Constant(item, typeof(TSource)),
				Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))
			}));
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000281A8 File Offset: 0x000263A8
		[__DynamicallyInvokable]
		public static IQueryable<TSource> Reverse<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IQueryable<TSource>>(new Func<IQueryable<TSource>, IQueryable<TSource>>(Queryable.Reverse<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000281F8 File Offset: 0x000263F8
		[__DynamicallyInvokable]
		public static bool SequenceEqual<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.Execute<bool>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IEnumerable<TSource>, bool>(new Func<IQueryable<TSource>, IEnumerable<TSource>, bool>(Queryable.SequenceEqual<TSource>), source1, source2), new Expression[]
			{
				source1.Expression,
				Queryable.GetSourceExpression<TSource>(source2)
			}));
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00028260 File Offset: 0x00026460
		[__DynamicallyInvokable]
		public static bool SequenceEqual<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.Execute<bool>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, IEnumerable<TSource>, IEqualityComparer<TSource>, bool>(new Func<IQueryable<TSource>, IEnumerable<TSource>, IEqualityComparer<TSource>, bool>(Queryable.SequenceEqual<TSource>), source1, source2, comparer), new Expression[]
			{
				source1.Expression,
				Queryable.GetSourceExpression<TSource>(source2),
				Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))
			}));
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000282DC File Offset: 0x000264DC
		[__DynamicallyInvokable]
		public static bool Any<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<bool>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, bool>(new Func<IQueryable<TSource>, bool>(Queryable.Any<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002832C File Offset: 0x0002652C
		[__DynamicallyInvokable]
		public static bool Any<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<bool>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, bool>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, bool>(Queryable.Any<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00028394 File Offset: 0x00026594
		[__DynamicallyInvokable]
		public static bool All<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<bool>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, bool>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, bool>(Queryable.All<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000283FC File Offset: 0x000265FC
		[__DynamicallyInvokable]
		public static int Count<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<int>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, int>(new Func<IQueryable<TSource>, int>(Queryable.Count<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002844C File Offset: 0x0002664C
		[__DynamicallyInvokable]
		public static int Count<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<int>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, int>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, int>(Queryable.Count<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000284B4 File Offset: 0x000266B4
		[__DynamicallyInvokable]
		public static long LongCount<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<long>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, long>(new Func<IQueryable<TSource>, long>(Queryable.LongCount<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00028504 File Offset: 0x00026704
		[__DynamicallyInvokable]
		public static long LongCount<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<long>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, bool>>, long>(new Func<IQueryable<TSource>, Expression<Func<TSource, bool>>, long>(Queryable.LongCount<TSource>), source, predicate), new Expression[]
			{
				source.Expression,
				Expression.Quote(predicate)
			}));
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002856C File Offset: 0x0002676C
		[__DynamicallyInvokable]
		public static TSource Min<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TSource>(new Func<IQueryable<TSource>, TSource>(Queryable.Min<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000285BC File Offset: 0x000267BC
		[__DynamicallyInvokable]
		public static TResult Min<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TResult>>, TResult>(new Func<IQueryable<TSource>, Expression<Func<TSource, TResult>>, TResult>(Queryable.Min<TSource, TResult>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00028624 File Offset: 0x00026824
		[__DynamicallyInvokable]
		public static TSource Max<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TSource>(new Func<IQueryable<TSource>, TSource>(Queryable.Max<TSource>), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00028674 File Offset: 0x00026874
		[__DynamicallyInvokable]
		public static TResult Max<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TResult>>, TResult>(new Func<IQueryable<TSource>, Expression<Func<TSource, TResult>>, TResult>(Queryable.Max<TSource, TResult>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000286DC File Offset: 0x000268DC
		[__DynamicallyInvokable]
		public static int Sum(this IQueryable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<int>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<int>, int>(new Func<IQueryable<int>, int>(Queryable.Sum), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002872C File Offset: 0x0002692C
		[__DynamicallyInvokable]
		public static int? Sum(this IQueryable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<int?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<int?>, int?>(new Func<IQueryable<int?>, int?>(Queryable.Sum), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002877C File Offset: 0x0002697C
		[__DynamicallyInvokable]
		public static long Sum(this IQueryable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<long>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<long>, long>(new Func<IQueryable<long>, long>(Queryable.Sum), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000287CC File Offset: 0x000269CC
		[__DynamicallyInvokable]
		public static long? Sum(this IQueryable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<long?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<long?>, long?>(new Func<IQueryable<long?>, long?>(Queryable.Sum), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002881C File Offset: 0x00026A1C
		[__DynamicallyInvokable]
		public static float Sum(this IQueryable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<float>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<float>, float>(new Func<IQueryable<float>, float>(Queryable.Sum), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002886C File Offset: 0x00026A6C
		[__DynamicallyInvokable]
		public static float? Sum(this IQueryable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<float?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<float?>, float?>(new Func<IQueryable<float?>, float?>(Queryable.Sum), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000288BC File Offset: 0x00026ABC
		[__DynamicallyInvokable]
		public static double Sum(this IQueryable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<double>, double>(new Func<IQueryable<double>, double>(Queryable.Sum), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002890C File Offset: 0x00026B0C
		[__DynamicallyInvokable]
		public static double? Sum(this IQueryable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<double?>, double?>(new Func<IQueryable<double?>, double?>(Queryable.Sum), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002895C File Offset: 0x00026B5C
		[__DynamicallyInvokable]
		public static decimal Sum(this IQueryable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<decimal>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<decimal>, decimal>(new Func<IQueryable<decimal>, decimal>(Queryable.Sum), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000289AC File Offset: 0x00026BAC
		[__DynamicallyInvokable]
		public static decimal? Sum(this IQueryable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<decimal?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<decimal?>, decimal?>(new Func<IQueryable<decimal?>, decimal?>(Queryable.Sum), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000289FC File Offset: 0x00026BFC
		[__DynamicallyInvokable]
		public static int Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<int>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, int>>, int>(new Func<IQueryable<TSource>, Expression<Func<TSource, int>>, int>(Queryable.Sum<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00028A64 File Offset: 0x00026C64
		[__DynamicallyInvokable]
		public static int? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<int?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, int?>>, int?>(new Func<IQueryable<TSource>, Expression<Func<TSource, int?>>, int?>(Queryable.Sum<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00028ACC File Offset: 0x00026CCC
		[__DynamicallyInvokable]
		public static long Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<long>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, long>>, long>(new Func<IQueryable<TSource>, Expression<Func<TSource, long>>, long>(Queryable.Sum<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00028B34 File Offset: 0x00026D34
		[__DynamicallyInvokable]
		public static long? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<long?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, long?>>, long?>(new Func<IQueryable<TSource>, Expression<Func<TSource, long?>>, long?>(Queryable.Sum<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00028B9C File Offset: 0x00026D9C
		[__DynamicallyInvokable]
		public static float Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<float>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, float>>, float>(new Func<IQueryable<TSource>, Expression<Func<TSource, float>>, float>(Queryable.Sum<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00028C04 File Offset: 0x00026E04
		[__DynamicallyInvokable]
		public static float? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<float?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, float?>>, float?>(new Func<IQueryable<TSource>, Expression<Func<TSource, float?>>, float?>(Queryable.Sum<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00028C6C File Offset: 0x00026E6C
		[__DynamicallyInvokable]
		public static double Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, double>>, double>(new Func<IQueryable<TSource>, Expression<Func<TSource, double>>, double>(Queryable.Sum<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00028CD4 File Offset: 0x00026ED4
		[__DynamicallyInvokable]
		public static double? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, double?>>, double?>(new Func<IQueryable<TSource>, Expression<Func<TSource, double?>>, double?>(Queryable.Sum<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00028D3C File Offset: 0x00026F3C
		[__DynamicallyInvokable]
		public static decimal Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<decimal>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, decimal>>, decimal>(new Func<IQueryable<TSource>, Expression<Func<TSource, decimal>>, decimal>(Queryable.Sum<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00028DA4 File Offset: 0x00026FA4
		[__DynamicallyInvokable]
		public static decimal? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<decimal?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, decimal?>>, decimal?>(new Func<IQueryable<TSource>, Expression<Func<TSource, decimal?>>, decimal?>(Queryable.Sum<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00028E0C File Offset: 0x0002700C
		[__DynamicallyInvokable]
		public static double Average(this IQueryable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<int>, double>(new Func<IQueryable<int>, double>(Queryable.Average), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00028E5C File Offset: 0x0002705C
		[__DynamicallyInvokable]
		public static double? Average(this IQueryable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<int?>, double?>(new Func<IQueryable<int?>, double?>(Queryable.Average), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00028EAC File Offset: 0x000270AC
		[__DynamicallyInvokable]
		public static double Average(this IQueryable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<long>, double>(new Func<IQueryable<long>, double>(Queryable.Average), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00028EFC File Offset: 0x000270FC
		[__DynamicallyInvokable]
		public static double? Average(this IQueryable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<long?>, double?>(new Func<IQueryable<long?>, double?>(Queryable.Average), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00028F4C File Offset: 0x0002714C
		[__DynamicallyInvokable]
		public static float Average(this IQueryable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<float>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<float>, float>(new Func<IQueryable<float>, float>(Queryable.Average), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00028F9C File Offset: 0x0002719C
		[__DynamicallyInvokable]
		public static float? Average(this IQueryable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<float?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<float?>, float?>(new Func<IQueryable<float?>, float?>(Queryable.Average), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00028FEC File Offset: 0x000271EC
		[__DynamicallyInvokable]
		public static double Average(this IQueryable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<double>, double>(new Func<IQueryable<double>, double>(Queryable.Average), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002903C File Offset: 0x0002723C
		[__DynamicallyInvokable]
		public static double? Average(this IQueryable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<double?>, double?>(new Func<IQueryable<double?>, double?>(Queryable.Average), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002908C File Offset: 0x0002728C
		[__DynamicallyInvokable]
		public static decimal Average(this IQueryable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<decimal>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<decimal>, decimal>(new Func<IQueryable<decimal>, decimal>(Queryable.Average), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000290DC File Offset: 0x000272DC
		[__DynamicallyInvokable]
		public static decimal? Average(this IQueryable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<decimal?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<decimal?>, decimal?>(new Func<IQueryable<decimal?>, decimal?>(Queryable.Average), source), new Expression[]
			{
				source.Expression
			}));
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002912C File Offset: 0x0002732C
		[__DynamicallyInvokable]
		public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, int>>, double>(new Func<IQueryable<TSource>, Expression<Func<TSource, int>>, double>(Queryable.Average<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00029194 File Offset: 0x00027394
		[__DynamicallyInvokable]
		public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, int?>>, double?>(new Func<IQueryable<TSource>, Expression<Func<TSource, int?>>, double?>(Queryable.Average<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000291FC File Offset: 0x000273FC
		[__DynamicallyInvokable]
		public static float Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<float>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, float>>, float>(new Func<IQueryable<TSource>, Expression<Func<TSource, float>>, float>(Queryable.Average<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00029264 File Offset: 0x00027464
		[__DynamicallyInvokable]
		public static float? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<float?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, float?>>, float?>(new Func<IQueryable<TSource>, Expression<Func<TSource, float?>>, float?>(Queryable.Average<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000292CC File Offset: 0x000274CC
		[__DynamicallyInvokable]
		public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, long>>, double>(new Func<IQueryable<TSource>, Expression<Func<TSource, long>>, double>(Queryable.Average<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00029334 File Offset: 0x00027534
		[__DynamicallyInvokable]
		public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, long?>>, double?>(new Func<IQueryable<TSource>, Expression<Func<TSource, long?>>, double?>(Queryable.Average<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002939C File Offset: 0x0002759C
		[__DynamicallyInvokable]
		public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, double>>, double>(new Func<IQueryable<TSource>, Expression<Func<TSource, double>>, double>(Queryable.Average<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00029404 File Offset: 0x00027604
		[__DynamicallyInvokable]
		public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, double?>>, double?>(new Func<IQueryable<TSource>, Expression<Func<TSource, double?>>, double?>(Queryable.Average<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002946C File Offset: 0x0002766C
		[__DynamicallyInvokable]
		public static decimal Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<decimal>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, decimal>>, decimal>(new Func<IQueryable<TSource>, Expression<Func<TSource, decimal>>, decimal>(Queryable.Average<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000294D4 File Offset: 0x000276D4
		[__DynamicallyInvokable]
		public static decimal? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<decimal?>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, decimal?>>, decimal?>(new Func<IQueryable<TSource>, Expression<Func<TSource, decimal?>>, decimal?>(Queryable.Average<TSource>), source, selector), new Expression[]
			{
				source.Expression,
				Expression.Quote(selector)
			}));
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002953C File Offset: 0x0002773C
		[__DynamicallyInvokable]
		public static TSource Aggregate<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, TSource, TSource>> func)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, Expression<Func<TSource, TSource, TSource>>, TSource>(new Func<IQueryable<TSource>, Expression<Func<TSource, TSource, TSource>>, TSource>(Queryable.Aggregate<TSource>), source, func), new Expression[]
			{
				source.Expression,
				Expression.Quote(func)
			}));
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000295A4 File Offset: 0x000277A4
		[__DynamicallyInvokable]
		public static TAccumulate Aggregate<TSource, TAccumulate>(this IQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			return source.Provider.Execute<TAccumulate>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TAccumulate, Expression<Func<TAccumulate, TSource, TAccumulate>>, TAccumulate>(new Func<IQueryable<TSource>, TAccumulate, Expression<Func<TAccumulate, TSource, TAccumulate>>, TAccumulate>(Queryable.Aggregate<TSource, TAccumulate>), source, seed, func), new Expression[]
			{
				source.Expression,
				Expression.Constant(seed),
				Expression.Quote(func)
			}));
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00029618 File Offset: 0x00027818
		[__DynamicallyInvokable]
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this IQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func, Expression<Func<TAccumulate, TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<TResult>(Expression.Call(null, Queryable.GetMethodInfo<IQueryable<TSource>, TAccumulate, Expression<Func<TAccumulate, TSource, TAccumulate>>, Expression<Func<TAccumulate, TResult>>, TResult>(new Func<IQueryable<TSource>, TAccumulate, Expression<Func<TAccumulate, TSource, TAccumulate>>, Expression<Func<TAccumulate, TResult>>, TResult>(Queryable.Aggregate<TSource, TAccumulate, TResult>), source, seed, func, selector), new Expression[]
			{
				source.Expression,
				Expression.Constant(seed),
				Expression.Quote(func),
				Expression.Quote(selector)
			}));
		}
	}
}

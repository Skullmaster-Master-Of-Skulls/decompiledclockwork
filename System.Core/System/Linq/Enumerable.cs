using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq
{
	// Token: 0x0200014F RID: 335
	[__DynamicallyInvokable]
	public static class Enumerable
	{
		// Token: 0x06000B2A RID: 2858 RVA: 0x000296A4 File Offset: 0x000278A4
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			if (source is Enumerable.Iterator<TSource>)
			{
				return ((Enumerable.Iterator<TSource>)source).Where(predicate);
			}
			if (source is TSource[])
			{
				return new Enumerable.WhereArrayIterator<TSource>((TSource[])source, predicate);
			}
			if (source is List<TSource>)
			{
				return new Enumerable.WhereListIterator<TSource>((List<TSource>)source, predicate);
			}
			return new Enumerable.WhereEnumerableIterator<TSource>(source, predicate);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00029713 File Offset: 0x00027913
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return Enumerable.WhereIterator<TSource>(source, predicate);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00029738 File Offset: 0x00027938
		private static IEnumerable<TSource> WhereIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			int index = -1;
			foreach (TSource tsource in source)
			{
				int num = index;
				index = checked(num + 1);
				if (predicate(tsource, index))
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00029750 File Offset: 0x00027950
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			if (source is Enumerable.Iterator<TSource>)
			{
				return ((Enumerable.Iterator<TSource>)source).Select<TResult>(selector);
			}
			if (source is TSource[])
			{
				return new Enumerable.WhereSelectArrayIterator<TSource, TResult>((TSource[])source, null, selector);
			}
			if (source is List<TSource>)
			{
				return new Enumerable.WhereSelectListIterator<TSource, TResult>((List<TSource>)source, null, selector);
			}
			return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult>(source, null, selector);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000297C2 File Offset: 0x000279C2
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return Enumerable.SelectIterator<TSource, TResult>(source, selector);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000297E7 File Offset: 0x000279E7
		private static IEnumerable<TResult> SelectIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
		{
			int index = -1;
			foreach (TSource arg in source)
			{
				int num = index;
				index = checked(num + 1);
				yield return selector(arg, index);
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00029800 File Offset: 0x00027A00
		private static Func<TSource, bool> CombinePredicates<TSource>(Func<TSource, bool> predicate1, Func<TSource, bool> predicate2)
		{
			return (TSource x) => predicate1(x) && predicate2(x);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00029830 File Offset: 0x00027A30
		private static Func<TSource, TResult> CombineSelectors<TSource, TMiddle, TResult>(Func<TSource, TMiddle> selector1, Func<TMiddle, TResult> selector2)
		{
			return (TSource x) => selector2(selector1(x));
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002985D File Offset: 0x00027A5D
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return Enumerable.SelectManyIterator<TSource, TResult>(source, selector);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00029882 File Offset: 0x00027A82
		private static IEnumerable<TResult> SelectManyIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
		{
			foreach (TSource arg in source)
			{
				foreach (TResult tresult in selector(arg))
				{
					yield return tresult;
				}
				IEnumerator<TResult> enumerator2 = null;
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00029899 File Offset: 0x00027A99
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return Enumerable.SelectManyIterator<TSource, TResult>(source, selector);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000298BE File Offset: 0x00027ABE
		private static IEnumerable<TResult> SelectManyIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
		{
			int index = -1;
			foreach (TSource arg in source)
			{
				int num = index;
				index = checked(num + 1);
				foreach (TResult tresult in selector(arg, index))
				{
					yield return tresult;
				}
				IEnumerator<TResult> enumerator2 = null;
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000298D5 File Offset: 0x00027AD5
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
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
			return Enumerable.SelectManyIterator<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00029909 File Offset: 0x00027B09
		private static IEnumerable<TResult> SelectManyIterator<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			int index = -1;
			foreach (TSource element in source)
			{
				int num = index;
				index = checked(num + 1);
				foreach (TCollection arg in collectionSelector(element, index))
				{
					yield return resultSelector(element, arg);
				}
				IEnumerator<TCollection> enumerator2 = null;
				element = default(TSource);
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00029927 File Offset: 0x00027B27
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
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
			return Enumerable.SelectManyIterator<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002995B File Offset: 0x00027B5B
		private static IEnumerable<TResult> SelectManyIterator<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			foreach (TSource element in source)
			{
				foreach (TCollection arg in collectionSelector(element))
				{
					yield return resultSelector(element, arg);
				}
				IEnumerator<TCollection> enumerator2 = null;
				element = default(TSource);
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00029979 File Offset: 0x00027B79
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.TakeIterator<TSource>(source, count);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00029990 File Offset: 0x00027B90
		private static IEnumerable<TSource> TakeIterator<TSource>(IEnumerable<TSource> source, int count)
		{
			if (count > 0)
			{
				foreach (TSource tsource in source)
				{
					yield return tsource;
					int num = count - 1;
					count = num;
					if (num == 0)
					{
						break;
					}
				}
				IEnumerator<TSource> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000299A7 File Offset: 0x00027BA7
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return Enumerable.TakeWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000299CC File Offset: 0x00027BCC
		private static IEnumerable<TSource> TakeWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			foreach (TSource tsource in source)
			{
				if (!predicate(tsource))
				{
					break;
				}
				yield return tsource;
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000299E3 File Offset: 0x00027BE3
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return Enumerable.TakeWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00029A08 File Offset: 0x00027C08
		private static IEnumerable<TSource> TakeWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			int index = -1;
			foreach (TSource tsource in source)
			{
				int num = index;
				index = checked(num + 1);
				if (!predicate(tsource, index))
				{
					break;
				}
				yield return tsource;
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00029A1F File Offset: 0x00027C1F
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.SkipIterator<TSource>(source, count);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00029A36 File Offset: 0x00027C36
		private static IEnumerable<TSource> SkipIterator<TSource>(IEnumerable<TSource> source, int count)
		{
			using (IEnumerator<TSource> e = source.GetEnumerator())
			{
				while (count > 0 && e.MoveNext())
				{
					int num = count;
					count = num - 1;
				}
				if (count <= 0)
				{
					while (e.MoveNext())
					{
						TSource tsource = e.Current;
						yield return tsource;
					}
				}
			}
			IEnumerator<TSource> e = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00029A4D File Offset: 0x00027C4D
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return Enumerable.SkipWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00029A72 File Offset: 0x00027C72
		private static IEnumerable<TSource> SkipWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			bool yielding = false;
			foreach (TSource tsource in source)
			{
				if (!yielding && !predicate(tsource))
				{
					yielding = true;
				}
				if (yielding)
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00029A89 File Offset: 0x00027C89
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return Enumerable.SkipWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00029AAE File Offset: 0x00027CAE
		private static IEnumerable<TSource> SkipWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			int index = -1;
			bool yielding = false;
			foreach (TSource tsource in source)
			{
				int num = index;
				index = checked(num + 1);
				if (!yielding && !predicate(tsource, index))
				{
					yielding = true;
				}
				if (yielding)
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00029AC8 File Offset: 0x00027CC8
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
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
			return Enumerable.JoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00029B28 File Offset: 0x00027D28
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
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
			return Enumerable.JoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00029B89 File Offset: 0x00027D89
		private static IEnumerable<TResult> JoinIterator<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			Lookup<TKey, TInner> lookup = Lookup<TKey, TInner>.CreateForJoin(inner, innerKeySelector, comparer);
			foreach (TOuter item in outer)
			{
				Lookup<TKey, TInner>.Grouping g = lookup.GetGrouping(outerKeySelector(item), false);
				if (g != null)
				{
					int num;
					for (int i = 0; i < g.count; i = num + 1)
					{
						yield return resultSelector(item, g.elements[i]);
						num = i;
					}
				}
				g = null;
				item = default(TOuter);
			}
			IEnumerator<TOuter> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00029BC0 File Offset: 0x00027DC0
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
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
			return Enumerable.GroupJoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00029C20 File Offset: 0x00027E20
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
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
			return Enumerable.GroupJoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00029C81 File Offset: 0x00027E81
		private static IEnumerable<TResult> GroupJoinIterator<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			Lookup<TKey, TInner> lookup = Lookup<TKey, TInner>.CreateForJoin(inner, innerKeySelector, comparer);
			foreach (TOuter touter in outer)
			{
				yield return resultSelector(touter, lookup[outerKeySelector(touter)]);
			}
			IEnumerator<TOuter> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00029CB6 File Offset: 0x00027EB6
		[__DynamicallyInvokable]
		public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, null, false);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00029CC1 File Offset: 0x00027EC1
		[__DynamicallyInvokable]
		public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, false);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00029CCC File Offset: 0x00027ECC
		[__DynamicallyInvokable]
		public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, null, true);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00029CD7 File Offset: 0x00027ED7
		[__DynamicallyInvokable]
		public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, true);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00029CE2 File Offset: 0x00027EE2
		[__DynamicallyInvokable]
		public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.CreateOrderedEnumerable<TKey>(keySelector, null, false);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00029CFB File Offset: 0x00027EFB
		[__DynamicallyInvokable]
		public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, false);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00029D14 File Offset: 0x00027F14
		[__DynamicallyInvokable]
		public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.CreateOrderedEnumerable<TKey>(keySelector, null, true);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00029D2D File Offset: 0x00027F2D
		[__DynamicallyInvokable]
		public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, true);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00029D46 File Offset: 0x00027F46
		[__DynamicallyInvokable]
		public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return new GroupedEnumerable<TSource, TKey, TSource>(source, keySelector, IdentityFunction<TSource>.Instance, null);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00029D55 File Offset: 0x00027F55
		[__DynamicallyInvokable]
		public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			return new GroupedEnumerable<TSource, TKey, TSource>(source, keySelector, IdentityFunction<TSource>.Instance, comparer);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00029D64 File Offset: 0x00027F64
		[__DynamicallyInvokable]
		public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return new GroupedEnumerable<TSource, TKey, TElement>(source, keySelector, elementSelector, null);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00029D6F File Offset: 0x00027F6F
		[__DynamicallyInvokable]
		public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			return new GroupedEnumerable<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00029D7A File Offset: 0x00027F7A
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
		{
			return new GroupedEnumerable<TSource, TKey, TSource, TResult>(source, keySelector, IdentityFunction<TSource>.Instance, resultSelector, null);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00029D8A File Offset: 0x00027F8A
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			return new GroupedEnumerable<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, null);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00029D96 File Offset: 0x00027F96
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			return new GroupedEnumerable<TSource, TKey, TSource, TResult>(source, keySelector, IdentityFunction<TSource>.Instance, resultSelector, comparer);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00029DA6 File Offset: 0x00027FA6
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			return new GroupedEnumerable<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00029DB3 File Offset: 0x00027FB3
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.ConcatIterator<TSource>(first, second);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00029DD8 File Offset: 0x00027FD8
		private static IEnumerable<TSource> ConcatIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			foreach (TSource tsource in first)
			{
				yield return tsource;
			}
			IEnumerator<TSource> enumerator = null;
			foreach (TSource tsource2 in second)
			{
				yield return tsource2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00029DEF File Offset: 0x00027FEF
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return Enumerable.ZipIterator<TFirst, TSecond, TResult>(first, second, resultSelector);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00029E23 File Offset: 0x00028023
		private static IEnumerable<TResult> ZipIterator<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			using (IEnumerator<TFirst> e = first.GetEnumerator())
			{
				using (IEnumerator<TSecond> e2 = second.GetEnumerator())
				{
					while (e.MoveNext() && e2.MoveNext())
					{
						yield return resultSelector(e.Current, e2.Current);
					}
				}
				IEnumerator<TSecond> e2 = null;
			}
			IEnumerator<TFirst> e = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00029E41 File Offset: 0x00028041
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.DistinctIterator<TSource>(source, null);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00029E58 File Offset: 0x00028058
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.DistinctIterator<TSource>(source, comparer);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00029E6F File Offset: 0x0002806F
		private static IEnumerable<TSource> DistinctIterator<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			Set<TSource> set = new Set<TSource>(comparer);
			foreach (TSource tsource in source)
			{
				if (set.Add(tsource))
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00029E86 File Offset: 0x00028086
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.UnionIterator<TSource>(first, second, null);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00029EAC File Offset: 0x000280AC
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.UnionIterator<TSource>(first, second, comparer);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00029ED2 File Offset: 0x000280D2
		private static IEnumerable<TSource> UnionIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			Set<TSource> set = new Set<TSource>(comparer);
			foreach (TSource tsource in first)
			{
				if (set.Add(tsource))
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			foreach (TSource tsource2 in second)
			{
				if (set.Add(tsource2))
				{
					yield return tsource2;
				}
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00029EF0 File Offset: 0x000280F0
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.IntersectIterator<TSource>(first, second, null);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00029F16 File Offset: 0x00028116
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.IntersectIterator<TSource>(first, second, comparer);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00029F3C File Offset: 0x0002813C
		private static IEnumerable<TSource> IntersectIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			Set<TSource> set = new Set<TSource>(comparer);
			foreach (TSource value in second)
			{
				set.Add(value);
			}
			foreach (TSource tsource in first)
			{
				if (set.Remove(tsource))
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00029F5A File Offset: 0x0002815A
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.ExceptIterator<TSource>(first, second, null);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00029F80 File Offset: 0x00028180
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.ExceptIterator<TSource>(first, second, comparer);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00029FA6 File Offset: 0x000281A6
		private static IEnumerable<TSource> ExceptIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			Set<TSource> set = new Set<TSource>(comparer);
			foreach (TSource value in second)
			{
				set.Add(value);
			}
			foreach (TSource tsource in first)
			{
				if (set.Add(tsource))
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00029FC4 File Offset: 0x000281C4
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.ReverseIterator<TSource>(source);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00029FDA File Offset: 0x000281DA
		private static IEnumerable<TSource> ReverseIterator<TSource>(IEnumerable<TSource> source)
		{
			Buffer<TSource> buffer = new Buffer<TSource>(source);
			int num;
			for (int i = buffer.count - 1; i >= 0; i = num - 1)
			{
				yield return buffer.items[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00029FEA File Offset: 0x000281EA
		[__DynamicallyInvokable]
		public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return first.SequenceEqual(second, null);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00029FF4 File Offset: 0x000281F4
		[__DynamicallyInvokable]
		public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			using (IEnumerator<TSource> enumerator = first.GetEnumerator())
			{
				using (IEnumerator<TSource> enumerator2 = second.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator2.MoveNext() || !comparer.Equals(enumerator.Current, enumerator2.Current))
						{
							return false;
						}
					}
					if (enumerator2.MoveNext())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002A0A0 File Offset: 0x000282A0
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
		{
			return source;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002A0A4 File Offset: 0x000282A4
		[__DynamicallyInvokable]
		public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return new Buffer<TSource>(source).ToArray();
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002A0CD File Offset: 0x000282CD
		[__DynamicallyInvokable]
		public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return new List<TSource>(source);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002A0E3 File Offset: 0x000282E3
		[__DynamicallyInvokable]
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToDictionary(keySelector, IdentityFunction<TSource>.Instance, null);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002A0F2 File Offset: 0x000282F2
		[__DynamicallyInvokable]
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			return source.ToDictionary(keySelector, IdentityFunction<TSource>.Instance, comparer);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002A101 File Offset: 0x00028301
		[__DynamicallyInvokable]
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToDictionary(keySelector, elementSelector, null);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002A10C File Offset: 0x0002830C
		[__DynamicallyInvokable]
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
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
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(comparer);
			foreach (TSource arg in source)
			{
				dictionary.Add(keySelector(arg), elementSelector(arg));
			}
			return dictionary;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002A194 File Offset: 0x00028394
		[__DynamicallyInvokable]
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return Lookup<TKey, TSource>.Create<TSource>(source, keySelector, IdentityFunction<TSource>.Instance, null);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002A1A3 File Offset: 0x000283A3
		[__DynamicallyInvokable]
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			return Lookup<TKey, TSource>.Create<TSource>(source, keySelector, IdentityFunction<TSource>.Instance, comparer);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002A1B2 File Offset: 0x000283B2
		[__DynamicallyInvokable]
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return Lookup<TKey, TElement>.Create<TSource>(source, keySelector, elementSelector, null);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002A1BD File Offset: 0x000283BD
		[__DynamicallyInvokable]
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			return Lookup<TKey, TElement>.Create<TSource>(source, keySelector, elementSelector, comparer);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002A1C8 File Offset: 0x000283C8
		public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source)
		{
			return source.ToHashSet(null);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002A1D1 File Offset: 0x000283D1
		public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return new HashSet<TSource>(source, comparer);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002A1E8 File Offset: 0x000283E8
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return source.DefaultIfEmpty(default(TSource));
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002A204 File Offset: 0x00028404
		[__DynamicallyInvokable]
		public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.DefaultIfEmptyIterator<TSource>(source, defaultValue);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002A21B File Offset: 0x0002841B
		private static IEnumerable<TSource> DefaultIfEmptyIterator<TSource>(IEnumerable<TSource> source, TSource defaultValue)
		{
			using (IEnumerator<TSource> e = source.GetEnumerator())
			{
				if (e.MoveNext())
				{
					do
					{
						yield return e.Current;
					}
					while (e.MoveNext());
				}
				else
				{
					yield return defaultValue;
				}
			}
			IEnumerator<TSource> e = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002A232 File Offset: 0x00028432
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.OfTypeIterator<TResult>(source);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002A248 File Offset: 0x00028448
		private static IEnumerable<TResult> OfTypeIterator<TResult>(IEnumerable source)
		{
			foreach (object obj in source)
			{
				if (obj is TResult)
				{
					yield return (TResult)((object)obj);
				}
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002A258 File Offset: 0x00028458
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
		{
			IEnumerable<TResult> enumerable = source as IEnumerable<TResult>;
			if (enumerable != null)
			{
				return enumerable;
			}
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.CastIterator<TResult>(source);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002A285 File Offset: 0x00028485
		private static IEnumerable<TResult> CastIterator<TResult>(IEnumerable source)
		{
			foreach (object obj in source)
			{
				yield return (TResult)((object)obj);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002A298 File Offset: 0x00028498
		[__DynamicallyInvokable]
		public static TSource First<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				if (list.Count > 0)
				{
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return enumerator.Current;
					}
				}
			}
			throw Error.NoElements();
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002A30C File Offset: 0x0002850C
		[__DynamicallyInvokable]
		public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					return tsource;
				}
			}
			throw Error.NoMatch();
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002A380 File Offset: 0x00028580
		[__DynamicallyInvokable]
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				if (list.Count > 0)
				{
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return enumerator.Current;
					}
				}
			}
			return default(TSource);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002A3F8 File Offset: 0x000285F8
		[__DynamicallyInvokable]
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					return tsource;
				}
			}
			return default(TSource);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002A470 File Offset: 0x00028670
		[__DynamicallyInvokable]
		public static TSource Last<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				int count = list.Count;
				if (count > 0)
				{
					return list[count - 1];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						TSource result;
						do
						{
							result = enumerator.Current;
						}
						while (enumerator.MoveNext());
						return result;
					}
				}
			}
			throw Error.NoElements();
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002A4F4 File Offset: 0x000286F4
		[__DynamicallyInvokable]
		public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			TSource result = default(TSource);
			bool flag = false;
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					result = tsource;
					flag = true;
				}
			}
			if (flag)
			{
				return result;
			}
			throw Error.NoMatch();
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002A574 File Offset: 0x00028774
		[__DynamicallyInvokable]
		public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				int count = list.Count;
				if (count > 0)
				{
					return list[count - 1];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						TSource result;
						do
						{
							result = enumerator.Current;
						}
						while (enumerator.MoveNext());
						return result;
					}
				}
			}
			return default(TSource);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002A5FC File Offset: 0x000287FC
		[__DynamicallyInvokable]
		public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			TSource result = default(TSource);
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					result = tsource;
				}
			}
			return result;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002A670 File Offset: 0x00028870
		[__DynamicallyInvokable]
		public static TSource Single<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				int count = list.Count;
				if (count == 0)
				{
					throw Error.NoElements();
				}
				if (count == 1)
				{
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						throw Error.NoElements();
					}
					TSource result = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						return result;
					}
				}
			}
			throw Error.MoreThanOneElement();
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002A700 File Offset: 0x00028900
		[__DynamicallyInvokable]
		public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			TSource result = default(TSource);
			long num = 0L;
			checked
			{
				foreach (TSource tsource in source)
				{
					if (predicate(tsource))
					{
						result = tsource;
						num += 1L;
					}
				}
				if (num == 0L)
				{
					throw Error.NoMatch();
				}
				if (num != 1L)
				{
					throw Error.MoreThanOneMatch();
				}
				return result;
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002A790 File Offset: 0x00028990
		[__DynamicallyInvokable]
		public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				int count = list.Count;
				if (count == 0)
				{
					TSource result = default(TSource);
					return result;
				}
				if (count == 1)
				{
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						TSource result = default(TSource);
						return result;
					}
					TSource result2 = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						return result2;
					}
				}
			}
			throw Error.MoreThanOneElement();
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002A82C File Offset: 0x00028A2C
		[__DynamicallyInvokable]
		public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			TSource result = default(TSource);
			long num = 0L;
			checked
			{
				foreach (TSource tsource in source)
				{
					if (predicate(tsource))
					{
						result = tsource;
						num += 1L;
					}
				}
				if (num == 0L)
				{
					return default(TSource);
				}
				if (num != 1L)
				{
					throw Error.MoreThanOneMatch();
				}
				return result;
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002A8C0 File Offset: 0x00028AC0
		[__DynamicallyInvokable]
		public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return list[index];
			}
			if (index < 0)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (index == 0)
					{
						return enumerator.Current;
					}
					index--;
				}
				throw Error.ArgumentOutOfRange("index");
			}
			TSource result;
			return result;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002A944 File Offset: 0x00028B44
		[__DynamicallyInvokable]
		public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (index >= 0)
			{
				IList<TSource> list = source as IList<TSource>;
				if (list != null)
				{
					if (index < list.Count)
					{
						return list[index];
					}
				}
				else
				{
					using (IEnumerator<TSource> enumerator = source.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (index == 0)
							{
								return enumerator.Current;
							}
							index--;
						}
					}
				}
			}
			return default(TSource);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002A9C8 File Offset: 0x00028BC8
		[__DynamicallyInvokable]
		public static IEnumerable<int> Range(int start, int count)
		{
			long num = (long)start + (long)count - 1L;
			if (count < 0 || num > 2147483647L)
			{
				throw Error.ArgumentOutOfRange("count");
			}
			return Enumerable.RangeIterator(start, count);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002A9FD File Offset: 0x00028BFD
		private static IEnumerable<int> RangeIterator(int start, int count)
		{
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				yield return start + i;
				num = i;
			}
			yield break;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002AA14 File Offset: 0x00028C14
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
		{
			if (count < 0)
			{
				throw Error.ArgumentOutOfRange("count");
			}
			return Enumerable.RepeatIterator<TResult>(element, count);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002AA2C File Offset: 0x00028C2C
		private static IEnumerable<TResult> RepeatIterator<TResult>(TResult element, int count)
		{
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				yield return element;
				num = i;
			}
			yield break;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002AA43 File Offset: 0x00028C43
		[__DynamicallyInvokable]
		public static IEnumerable<TResult> Empty<TResult>()
		{
			return EmptyEnumerable<TResult>.Instance;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002AA4C File Offset: 0x00028C4C
		[__DynamicallyInvokable]
		public static bool Any<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002AA9C File Offset: 0x00028C9C
		[__DynamicallyInvokable]
		public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			foreach (TSource arg in source)
			{
				if (predicate(arg))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002AB0C File Offset: 0x00028D0C
		[__DynamicallyInvokable]
		public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			foreach (TSource arg in source)
			{
				if (!predicate(arg))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002AB7C File Offset: 0x00028D7C
		[__DynamicallyInvokable]
		public static int Count<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				return collection.Count;
			}
			ICollection collection2 = source as ICollection;
			if (collection2 != null)
			{
				return collection2.Count;
			}
			int num = 0;
			checked
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002ABF0 File Offset: 0x00028DF0
		[__DynamicallyInvokable]
		public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			int num = 0;
			checked
			{
				foreach (TSource arg in source)
				{
					if (predicate(arg))
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002AC60 File Offset: 0x00028E60
		[__DynamicallyInvokable]
		public static long LongCount<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			checked
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						num += 1L;
					}
				}
				return num;
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002ACB4 File Offset: 0x00028EB4
		[__DynamicallyInvokable]
		public static long LongCount<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			long num = 0L;
			checked
			{
				foreach (TSource arg in source)
				{
					if (predicate(arg))
					{
						num += 1L;
					}
				}
				return num;
			}
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002AD24 File Offset: 0x00028F24
		[__DynamicallyInvokable]
		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				return collection.Contains(value);
			}
			return source.Contains(value, null);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002AD4C File Offset: 0x00028F4C
		[__DynamicallyInvokable]
		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			foreach (TSource x in source)
			{
				if (comparer.Equals(x, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002ADB8 File Offset: 0x00028FB8
		[__DynamicallyInvokable]
		public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			TSource result;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				TSource tsource = enumerator.Current;
				while (enumerator.MoveNext())
				{
					TSource arg = enumerator.Current;
					tsource = func(tsource, arg);
				}
				result = tsource;
			}
			return result;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002AE34 File Offset: 0x00029034
		[__DynamicallyInvokable]
		public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			TAccumulate taccumulate = seed;
			foreach (TSource arg in source)
			{
				taccumulate = func(taccumulate, arg);
			}
			return taccumulate;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002AEA0 File Offset: 0x000290A0
		[__DynamicallyInvokable]
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			TAccumulate taccumulate = seed;
			foreach (TSource arg in source)
			{
				taccumulate = func(taccumulate, arg);
			}
			return resultSelector(taccumulate);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002AF20 File Offset: 0x00029120
		[__DynamicallyInvokable]
		public static int Sum(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int num = 0;
			checked
			{
				foreach (int num2 in source)
				{
					num += num2;
				}
				return num;
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002AF78 File Offset: 0x00029178
		[__DynamicallyInvokable]
		public static int? Sum(this IEnumerable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int num = 0;
			checked
			{
				foreach (int? num2 in source)
				{
					if (num2 != null)
					{
						num += num2.GetValueOrDefault();
					}
				}
				return new int?(num);
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002AFE4 File Offset: 0x000291E4
		[__DynamicallyInvokable]
		public static long Sum(this IEnumerable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			checked
			{
				foreach (long num2 in source)
				{
					num += num2;
				}
				return num;
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002B03C File Offset: 0x0002923C
		[__DynamicallyInvokable]
		public static long? Sum(this IEnumerable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			checked
			{
				foreach (long? num2 in source)
				{
					if (num2 != null)
					{
						num += num2.GetValueOrDefault();
					}
				}
				return new long?(num);
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002B0A8 File Offset: 0x000292A8
		[__DynamicallyInvokable]
		public static float Sum(this IEnumerable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			foreach (float num2 in source)
			{
				num += (double)num2;
			}
			return (float)num;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002B108 File Offset: 0x00029308
		[__DynamicallyInvokable]
		public static float? Sum(this IEnumerable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			foreach (float? num2 in source)
			{
				if (num2 != null)
				{
					num += (double)num2.GetValueOrDefault();
				}
			}
			return new float?((float)num);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002B17C File Offset: 0x0002937C
		[__DynamicallyInvokable]
		public static double Sum(this IEnumerable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			foreach (double num2 in source)
			{
				num += num2;
			}
			return num;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002B1DC File Offset: 0x000293DC
		[__DynamicallyInvokable]
		public static double? Sum(this IEnumerable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			foreach (double? num2 in source)
			{
				if (num2 != null)
				{
					num += num2.GetValueOrDefault();
				}
			}
			return new double?(num);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002B250 File Offset: 0x00029450
		[__DynamicallyInvokable]
		public static decimal Sum(this IEnumerable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal num = 0m;
			foreach (decimal d in source)
			{
				num += d;
			}
			return num;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002B2B0 File Offset: 0x000294B0
		[__DynamicallyInvokable]
		public static decimal? Sum(this IEnumerable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal num = 0m;
			foreach (decimal? num2 in source)
			{
				if (num2 != null)
				{
					num += num2.GetValueOrDefault();
				}
			}
			return new decimal?(num);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002B324 File Offset: 0x00029524
		[__DynamicallyInvokable]
		public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002B332 File Offset: 0x00029532
		[__DynamicallyInvokable]
		public static int? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002B340 File Offset: 0x00029540
		[__DynamicallyInvokable]
		public static long Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002B34E File Offset: 0x0002954E
		[__DynamicallyInvokable]
		public static long? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002B35C File Offset: 0x0002955C
		[__DynamicallyInvokable]
		public static float Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002B36A File Offset: 0x0002956A
		[__DynamicallyInvokable]
		public static float? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002B378 File Offset: 0x00029578
		[__DynamicallyInvokable]
		public static double Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002B386 File Offset: 0x00029586
		[__DynamicallyInvokable]
		public static double? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002B394 File Offset: 0x00029594
		[__DynamicallyInvokable]
		public static decimal Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002B3A2 File Offset: 0x000295A2
		[__DynamicallyInvokable]
		public static decimal? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002B3B0 File Offset: 0x000295B0
		[__DynamicallyInvokable]
		public static int Min(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int num = 0;
			bool flag = false;
			foreach (int num2 in source)
			{
				if (flag)
				{
					if (num2 < num)
					{
						num = num2;
					}
				}
				else
				{
					num = num2;
					flag = true;
				}
			}
			if (flag)
			{
				return num;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002B41C File Offset: 0x0002961C
		[__DynamicallyInvokable]
		public static int? Min(this IEnumerable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int? num = null;
			foreach (int? num2 in source)
			{
				if (num != null)
				{
					int? num3 = num2;
					int? num4 = num;
					if (!(num3.GetValueOrDefault() < num4.GetValueOrDefault() & (num3 != null & num4 != null)))
					{
						continue;
					}
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002B4A8 File Offset: 0x000296A8
		[__DynamicallyInvokable]
		public static long Min(this IEnumerable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			bool flag = false;
			foreach (long num2 in source)
			{
				if (flag)
				{
					if (num2 < num)
					{
						num = num2;
					}
				}
				else
				{
					num = num2;
					flag = true;
				}
			}
			if (flag)
			{
				return num;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002B518 File Offset: 0x00029718
		[__DynamicallyInvokable]
		public static long? Min(this IEnumerable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long? num = null;
			foreach (long? num2 in source)
			{
				if (num != null)
				{
					long? num3 = num2;
					long? num4 = num;
					if (!(num3.GetValueOrDefault() < num4.GetValueOrDefault() & (num3 != null & num4 != null)))
					{
						continue;
					}
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002B5A4 File Offset: 0x000297A4
		[__DynamicallyInvokable]
		public static float Min(this IEnumerable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			float num = 0f;
			bool flag = false;
			foreach (float num2 in source)
			{
				if (flag)
				{
					if (num2 < num || float.IsNaN(num2))
					{
						num = num2;
					}
				}
				else
				{
					num = num2;
					flag = true;
				}
			}
			if (flag)
			{
				return num;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002B61C File Offset: 0x0002981C
		[__DynamicallyInvokable]
		public static float? Min(this IEnumerable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			float? num = null;
			foreach (float? num2 in source)
			{
				if (num2 != null)
				{
					if (num != null)
					{
						float? num3 = num2;
						float? num4 = num;
						if (!(num3.GetValueOrDefault() < num4.GetValueOrDefault() & (num3 != null & num4 != null)) && !float.IsNaN(num2.Value))
						{
							continue;
						}
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002B6C0 File Offset: 0x000298C0
		[__DynamicallyInvokable]
		public static double Min(this IEnumerable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			bool flag = false;
			foreach (double num2 in source)
			{
				if (flag)
				{
					if (num2 < num || double.IsNaN(num2))
					{
						num = num2;
					}
				}
				else
				{
					num = num2;
					flag = true;
				}
			}
			if (flag)
			{
				return num;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002B73C File Offset: 0x0002993C
		[__DynamicallyInvokable]
		public static double? Min(this IEnumerable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double? num = null;
			foreach (double? num2 in source)
			{
				if (num2 != null)
				{
					if (num != null)
					{
						double? num3 = num2;
						double? num4 = num;
						if (!(num3.GetValueOrDefault() < num4.GetValueOrDefault() & (num3 != null & num4 != null)) && !double.IsNaN(num2.Value))
						{
							continue;
						}
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002B7E0 File Offset: 0x000299E0
		[__DynamicallyInvokable]
		public static decimal Min(this IEnumerable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal num = 0m;
			bool flag = false;
			foreach (decimal num2 in source)
			{
				if (flag)
				{
					if (num2 < num)
					{
						num = num2;
					}
				}
				else
				{
					num = num2;
					flag = true;
				}
			}
			if (flag)
			{
				return num;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002B858 File Offset: 0x00029A58
		[__DynamicallyInvokable]
		public static decimal? Min(this IEnumerable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal? num = null;
			foreach (decimal? num2 in source)
			{
				if (num != null)
				{
					decimal? num3 = num2;
					decimal? num4 = num;
					if (!(num3.GetValueOrDefault() < num4.GetValueOrDefault() & (num3 != null & num4 != null)))
					{
						continue;
					}
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002B8E8 File Offset: 0x00029AE8
		[__DynamicallyInvokable]
		public static TSource Min<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			Comparer<TSource> @default = Comparer<TSource>.Default;
			TSource tsource = default(TSource);
			if (tsource == null)
			{
				foreach (TSource tsource2 in source)
				{
					if (tsource2 != null && (tsource == null || @default.Compare(tsource2, tsource) < 0))
					{
						tsource = tsource2;
					}
				}
				return tsource;
			}
			bool flag = false;
			foreach (TSource tsource3 in source)
			{
				if (flag)
				{
					if (@default.Compare(tsource3, tsource) < 0)
					{
						tsource = tsource3;
					}
				}
				else
				{
					tsource = tsource3;
					flag = true;
				}
			}
			if (flag)
			{
				return tsource;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002B9CC File Offset: 0x00029BCC
		[__DynamicallyInvokable]
		public static int Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Min();
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002B9DA File Offset: 0x00029BDA
		[__DynamicallyInvokable]
		public static int? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Min();
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002B9E8 File Offset: 0x00029BE8
		[__DynamicallyInvokable]
		public static long Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Min();
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002B9F6 File Offset: 0x00029BF6
		[__DynamicallyInvokable]
		public static long? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Min();
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002BA04 File Offset: 0x00029C04
		[__DynamicallyInvokable]
		public static float Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Min();
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002BA12 File Offset: 0x00029C12
		[__DynamicallyInvokable]
		public static float? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Min();
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002BA20 File Offset: 0x00029C20
		[__DynamicallyInvokable]
		public static double Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Min();
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002BA2E File Offset: 0x00029C2E
		[__DynamicallyInvokable]
		public static double? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Min();
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002BA3C File Offset: 0x00029C3C
		[__DynamicallyInvokable]
		public static decimal Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Min();
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002BA4A File Offset: 0x00029C4A
		[__DynamicallyInvokable]
		public static decimal? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Min();
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002BA58 File Offset: 0x00029C58
		[__DynamicallyInvokable]
		public static TResult Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			return source.Select(selector).Min<TResult>();
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002BA68 File Offset: 0x00029C68
		[__DynamicallyInvokable]
		public static int Max(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int num = 0;
			bool flag = false;
			foreach (int num2 in source)
			{
				if (flag)
				{
					if (num2 > num)
					{
						num = num2;
					}
				}
				else
				{
					num = num2;
					flag = true;
				}
			}
			if (flag)
			{
				return num;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002BAD4 File Offset: 0x00029CD4
		[__DynamicallyInvokable]
		public static int? Max(this IEnumerable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int? num = null;
			foreach (int? num2 in source)
			{
				if (num != null)
				{
					int? num3 = num2;
					int? num4 = num;
					if (!(num3.GetValueOrDefault() > num4.GetValueOrDefault() & (num3 != null & num4 != null)))
					{
						continue;
					}
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002BB60 File Offset: 0x00029D60
		[__DynamicallyInvokable]
		public static long Max(this IEnumerable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			bool flag = false;
			foreach (long num2 in source)
			{
				if (flag)
				{
					if (num2 > num)
					{
						num = num2;
					}
				}
				else
				{
					num = num2;
					flag = true;
				}
			}
			if (flag)
			{
				return num;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002BBD0 File Offset: 0x00029DD0
		[__DynamicallyInvokable]
		public static long? Max(this IEnumerable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long? num = null;
			foreach (long? num2 in source)
			{
				if (num != null)
				{
					long? num3 = num2;
					long? num4 = num;
					if (!(num3.GetValueOrDefault() > num4.GetValueOrDefault() & (num3 != null & num4 != null)))
					{
						continue;
					}
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002BC5C File Offset: 0x00029E5C
		[__DynamicallyInvokable]
		public static double Max(this IEnumerable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			bool flag = false;
			foreach (double num2 in source)
			{
				if (flag)
				{
					if (num2 > num || double.IsNaN(num))
					{
						num = num2;
					}
				}
				else
				{
					num = num2;
					flag = true;
				}
			}
			if (flag)
			{
				return num;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002BCD8 File Offset: 0x00029ED8
		[__DynamicallyInvokable]
		public static double? Max(this IEnumerable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double? num = null;
			foreach (double? num2 in source)
			{
				if (num2 != null)
				{
					if (num != null)
					{
						double? num3 = num2;
						double? num4 = num;
						if (!(num3.GetValueOrDefault() > num4.GetValueOrDefault() & (num3 != null & num4 != null)) && !double.IsNaN(num.Value))
						{
							continue;
						}
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002BD7C File Offset: 0x00029F7C
		[__DynamicallyInvokable]
		public static float Max(this IEnumerable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			float num = 0f;
			bool flag = false;
			foreach (float num2 in source)
			{
				if (flag)
				{
					if (num2 > num || double.IsNaN((double)num))
					{
						num = num2;
					}
				}
				else
				{
					num = num2;
					flag = true;
				}
			}
			if (flag)
			{
				return num;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002BDF8 File Offset: 0x00029FF8
		[__DynamicallyInvokable]
		public static float? Max(this IEnumerable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			float? num = null;
			foreach (float? num2 in source)
			{
				if (num2 != null)
				{
					if (num != null)
					{
						float? num3 = num2;
						float? num4 = num;
						if (!(num3.GetValueOrDefault() > num4.GetValueOrDefault() & (num3 != null & num4 != null)) && !float.IsNaN(num.Value))
						{
							continue;
						}
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002BE9C File Offset: 0x0002A09C
		[__DynamicallyInvokable]
		public static decimal Max(this IEnumerable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal num = 0m;
			bool flag = false;
			foreach (decimal num2 in source)
			{
				if (flag)
				{
					if (num2 > num)
					{
						num = num2;
					}
				}
				else
				{
					num = num2;
					flag = true;
				}
			}
			if (flag)
			{
				return num;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002BF14 File Offset: 0x0002A114
		[__DynamicallyInvokable]
		public static decimal? Max(this IEnumerable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal? num = null;
			foreach (decimal? num2 in source)
			{
				if (num != null)
				{
					decimal? num3 = num2;
					decimal? num4 = num;
					if (!(num3.GetValueOrDefault() > num4.GetValueOrDefault() & (num3 != null & num4 != null)))
					{
						continue;
					}
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002BFA4 File Offset: 0x0002A1A4
		[__DynamicallyInvokable]
		public static TSource Max<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			Comparer<TSource> @default = Comparer<TSource>.Default;
			TSource tsource = default(TSource);
			if (tsource == null)
			{
				foreach (TSource tsource2 in source)
				{
					if (tsource2 != null && (tsource == null || @default.Compare(tsource2, tsource) > 0))
					{
						tsource = tsource2;
					}
				}
				return tsource;
			}
			bool flag = false;
			foreach (TSource tsource3 in source)
			{
				if (flag)
				{
					if (@default.Compare(tsource3, tsource) > 0)
					{
						tsource = tsource3;
					}
				}
				else
				{
					tsource = tsource3;
					flag = true;
				}
			}
			if (flag)
			{
				return tsource;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002C088 File Offset: 0x0002A288
		[__DynamicallyInvokable]
		public static int Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Max();
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002C096 File Offset: 0x0002A296
		[__DynamicallyInvokable]
		public static int? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Max();
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002C0A4 File Offset: 0x0002A2A4
		[__DynamicallyInvokable]
		public static long Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Max();
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002C0B2 File Offset: 0x0002A2B2
		[__DynamicallyInvokable]
		public static long? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Max();
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002C0C0 File Offset: 0x0002A2C0
		[__DynamicallyInvokable]
		public static float Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Max();
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002C0CE File Offset: 0x0002A2CE
		[__DynamicallyInvokable]
		public static float? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Max();
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002C0DC File Offset: 0x0002A2DC
		[__DynamicallyInvokable]
		public static double Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Max();
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002C0EA File Offset: 0x0002A2EA
		[__DynamicallyInvokable]
		public static double? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Max();
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002C0F8 File Offset: 0x0002A2F8
		[__DynamicallyInvokable]
		public static decimal Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Max();
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002C106 File Offset: 0x0002A306
		[__DynamicallyInvokable]
		public static decimal? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Max();
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002C114 File Offset: 0x0002A314
		[__DynamicallyInvokable]
		public static TResult Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			return source.Select(selector).Max<TResult>();
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002C124 File Offset: 0x0002A324
		[__DynamicallyInvokable]
		public static double Average(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			long num2 = 0L;
			checked
			{
				foreach (int num3 in source)
				{
					num += unchecked((long)num3);
					num2 += 1L;
				}
				if (num2 > 0L)
				{
					return (double)num / (double)num2;
				}
				throw Error.NoElements();
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002C194 File Offset: 0x0002A394
		[__DynamicallyInvokable]
		public static double? Average(this IEnumerable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			long num2 = 0L;
			checked
			{
				foreach (int? num3 in source)
				{
					if (num3 != null)
					{
						num += unchecked((long)num3.GetValueOrDefault());
						num2 += 1L;
					}
				}
				if (num2 > 0L)
				{
					return new double?((double)num / (double)num2);
				}
				return null;
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002C21C File Offset: 0x0002A41C
		[__DynamicallyInvokable]
		public static double Average(this IEnumerable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			long num2 = 0L;
			checked
			{
				foreach (long num3 in source)
				{
					num += num3;
					num2 += 1L;
				}
				if (num2 > 0L)
				{
					return (double)num / (double)num2;
				}
				throw Error.NoElements();
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002C28C File Offset: 0x0002A48C
		[__DynamicallyInvokable]
		public static double? Average(this IEnumerable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			long num2 = 0L;
			checked
			{
				foreach (long? num3 in source)
				{
					if (num3 != null)
					{
						num += num3.GetValueOrDefault();
						num2 += 1L;
					}
				}
				if (num2 > 0L)
				{
					return new double?((double)num / (double)num2);
				}
				return null;
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002C314 File Offset: 0x0002A514
		[__DynamicallyInvokable]
		public static float Average(this IEnumerable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			long num2 = 0L;
			foreach (float num3 in source)
			{
				num += (double)num3;
				checked
				{
					num2 += 1L;
				}
			}
			if (num2 > 0L)
			{
				return (float)(num / (double)num2);
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002C38C File Offset: 0x0002A58C
		[__DynamicallyInvokable]
		public static float? Average(this IEnumerable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			long num2 = 0L;
			foreach (float? num3 in source)
			{
				if (num3 != null)
				{
					num += (double)num3.GetValueOrDefault();
					checked
					{
						num2 += 1L;
					}
				}
			}
			if (num2 > 0L)
			{
				return new float?((float)(num / (double)num2));
			}
			return null;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002C41C File Offset: 0x0002A61C
		[__DynamicallyInvokable]
		public static double Average(this IEnumerable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			long num2 = 0L;
			foreach (double num3 in source)
			{
				num += num3;
				checked
				{
					num2 += 1L;
				}
			}
			if (num2 > 0L)
			{
				return num / (double)num2;
			}
			throw Error.NoElements();
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002C490 File Offset: 0x0002A690
		[__DynamicallyInvokable]
		public static double? Average(this IEnumerable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			long num2 = 0L;
			foreach (double? num3 in source)
			{
				if (num3 != null)
				{
					num += num3.GetValueOrDefault();
					checked
					{
						num2 += 1L;
					}
				}
			}
			if (num2 > 0L)
			{
				return new double?(num / (double)num2);
			}
			return null;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002C520 File Offset: 0x0002A720
		[__DynamicallyInvokable]
		public static decimal Average(this IEnumerable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal d = 0m;
			long num = 0L;
			checked
			{
				foreach (decimal d2 in source)
				{
					d += d2;
					num += 1L;
				}
				if (num > 0L)
				{
					return d / num;
				}
				throw Error.NoElements();
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002C5A0 File Offset: 0x0002A7A0
		[__DynamicallyInvokable]
		public static decimal? Average(this IEnumerable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal d = 0m;
			long num = 0L;
			checked
			{
				foreach (decimal? num2 in source)
				{
					if (num2 != null)
					{
						d += num2.GetValueOrDefault();
						num += 1L;
					}
				}
				if (num > 0L)
				{
					return new decimal?(d / num);
				}
				return null;
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002C638 File Offset: 0x0002A838
		[__DynamicallyInvokable]
		public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002C646 File Offset: 0x0002A846
		[__DynamicallyInvokable]
		public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002C654 File Offset: 0x0002A854
		[__DynamicallyInvokable]
		public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002C662 File Offset: 0x0002A862
		[__DynamicallyInvokable]
		public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002C670 File Offset: 0x0002A870
		[__DynamicallyInvokable]
		public static float Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002C67E File Offset: 0x0002A87E
		[__DynamicallyInvokable]
		public static float? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002C68C File Offset: 0x0002A88C
		[__DynamicallyInvokable]
		public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002C69A File Offset: 0x0002A89A
		[__DynamicallyInvokable]
		public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002C6A8 File Offset: 0x0002A8A8
		[__DynamicallyInvokable]
		public static decimal Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002C6B6 File Offset: 0x0002A8B6
		[__DynamicallyInvokable]
		public static decimal? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Average();
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002C6C4 File Offset: 0x0002A8C4
		public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource element)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			Enumerable.AppendPrependIterator<TSource> appendPrependIterator = source as Enumerable.AppendPrependIterator<TSource>;
			if (appendPrependIterator != null)
			{
				return appendPrependIterator.Append(element);
			}
			return new Enumerable.AppendPrepend1Iterator<TSource>(source, element, true);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002C6FC File Offset: 0x0002A8FC
		public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource element)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			Enumerable.AppendPrependIterator<TSource> appendPrependIterator = source as Enumerable.AppendPrependIterator<TSource>;
			if (appendPrependIterator != null)
			{
				return appendPrependIterator.Prepend(element);
			}
			return new Enumerable.AppendPrepend1Iterator<TSource>(source, element, false);
		}

		// Token: 0x02000370 RID: 880
		private abstract class Iterator<TSource> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06001BA8 RID: 7080 RVA: 0x00063A58 File Offset: 0x00061C58
			public Iterator()
			{
				this.threadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00063A70 File Offset: 0x00061C70
			public TSource Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06001BAA RID: 7082
			public abstract Enumerable.Iterator<TSource> Clone();

			// Token: 0x06001BAB RID: 7083 RVA: 0x00063A78 File Offset: 0x00061C78
			public virtual void Dispose()
			{
				this.current = default(TSource);
				this.state = -1;
			}

			// Token: 0x06001BAC RID: 7084 RVA: 0x00063A90 File Offset: 0x00061C90
			public IEnumerator<TSource> GetEnumerator()
			{
				if (this.threadId == Thread.CurrentThread.ManagedThreadId && this.state == 0)
				{
					this.state = 1;
					return this;
				}
				Enumerable.Iterator<TSource> iterator = this.Clone();
				iterator.state = 1;
				return iterator;
			}

			// Token: 0x06001BAD RID: 7085
			public abstract bool MoveNext();

			// Token: 0x06001BAE RID: 7086
			public abstract IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector);

			// Token: 0x06001BAF RID: 7087
			public abstract IEnumerable<TSource> Where(Func<TSource, bool> predicate);

			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x00063ACF File Offset: 0x00061CCF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001BB1 RID: 7089 RVA: 0x00063ADC File Offset: 0x00061CDC
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06001BB2 RID: 7090 RVA: 0x00063AE4 File Offset: 0x00061CE4
			void IEnumerator.Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000F9D RID: 3997
			private int threadId;

			// Token: 0x04000F9E RID: 3998
			internal int state;

			// Token: 0x04000F9F RID: 3999
			internal TSource current;
		}

		// Token: 0x02000371 RID: 881
		private class WhereEnumerableIterator<TSource> : Enumerable.Iterator<TSource>
		{
			// Token: 0x06001BB3 RID: 7091 RVA: 0x00063AEB File Offset: 0x00061CEB
			public WhereEnumerableIterator(IEnumerable<TSource> source, Func<TSource, bool> predicate)
			{
				this.source = source;
				this.predicate = predicate;
			}

			// Token: 0x06001BB4 RID: 7092 RVA: 0x00063B01 File Offset: 0x00061D01
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.WhereEnumerableIterator<TSource>(this.source, this.predicate);
			}

			// Token: 0x06001BB5 RID: 7093 RVA: 0x00063B14 File Offset: 0x00061D14
			public override void Dispose()
			{
				if (this.enumerator != null)
				{
					this.enumerator.Dispose();
				}
				this.enumerator = null;
				base.Dispose();
			}

			// Token: 0x06001BB6 RID: 7094 RVA: 0x00063B38 File Offset: 0x00061D38
			public override bool MoveNext()
			{
				int state = this.state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this.enumerator = this.source.GetEnumerator();
					this.state = 2;
				}
				while (this.enumerator.MoveNext())
				{
					TSource tsource = this.enumerator.Current;
					if (this.predicate(tsource))
					{
						this.current = tsource;
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06001BB7 RID: 7095 RVA: 0x00063BA7 File Offset: 0x00061DA7
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult>(this.source, this.predicate, selector);
			}

			// Token: 0x06001BB8 RID: 7096 RVA: 0x00063BBB File Offset: 0x00061DBB
			public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereEnumerableIterator<TSource>(this.source, Enumerable.CombinePredicates<TSource>(this.predicate, predicate));
			}

			// Token: 0x04000FA0 RID: 4000
			private IEnumerable<TSource> source;

			// Token: 0x04000FA1 RID: 4001
			private Func<TSource, bool> predicate;

			// Token: 0x04000FA2 RID: 4002
			private IEnumerator<TSource> enumerator;
		}

		// Token: 0x02000372 RID: 882
		private class WhereArrayIterator<TSource> : Enumerable.Iterator<TSource>
		{
			// Token: 0x06001BB9 RID: 7097 RVA: 0x00063BD4 File Offset: 0x00061DD4
			public WhereArrayIterator(TSource[] source, Func<TSource, bool> predicate)
			{
				this.source = source;
				this.predicate = predicate;
			}

			// Token: 0x06001BBA RID: 7098 RVA: 0x00063BEA File Offset: 0x00061DEA
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.WhereArrayIterator<TSource>(this.source, this.predicate);
			}

			// Token: 0x06001BBB RID: 7099 RVA: 0x00063C00 File Offset: 0x00061E00
			public override bool MoveNext()
			{
				if (this.state == 1)
				{
					while (this.index < this.source.Length)
					{
						TSource tsource = this.source[this.index];
						this.index++;
						if (this.predicate(tsource))
						{
							this.current = tsource;
							return true;
						}
					}
					this.Dispose();
				}
				return false;
			}

			// Token: 0x06001BBC RID: 7100 RVA: 0x00063C66 File Offset: 0x00061E66
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.WhereSelectArrayIterator<TSource, TResult>(this.source, this.predicate, selector);
			}

			// Token: 0x06001BBD RID: 7101 RVA: 0x00063C7A File Offset: 0x00061E7A
			public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereArrayIterator<TSource>(this.source, Enumerable.CombinePredicates<TSource>(this.predicate, predicate));
			}

			// Token: 0x04000FA3 RID: 4003
			private TSource[] source;

			// Token: 0x04000FA4 RID: 4004
			private Func<TSource, bool> predicate;

			// Token: 0x04000FA5 RID: 4005
			private int index;
		}

		// Token: 0x02000373 RID: 883
		private class WhereListIterator<TSource> : Enumerable.Iterator<TSource>
		{
			// Token: 0x06001BBE RID: 7102 RVA: 0x00063C93 File Offset: 0x00061E93
			public WhereListIterator(List<TSource> source, Func<TSource, bool> predicate)
			{
				this.source = source;
				this.predicate = predicate;
			}

			// Token: 0x06001BBF RID: 7103 RVA: 0x00063CA9 File Offset: 0x00061EA9
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.WhereListIterator<TSource>(this.source, this.predicate);
			}

			// Token: 0x06001BC0 RID: 7104 RVA: 0x00063CBC File Offset: 0x00061EBC
			public override bool MoveNext()
			{
				int state = this.state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this.enumerator = this.source.GetEnumerator();
					this.state = 2;
				}
				while (this.enumerator.MoveNext())
				{
					TSource tsource = this.enumerator.Current;
					if (this.predicate(tsource))
					{
						this.current = tsource;
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06001BC1 RID: 7105 RVA: 0x00063D2B File Offset: 0x00061F2B
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.WhereSelectListIterator<TSource, TResult>(this.source, this.predicate, selector);
			}

			// Token: 0x06001BC2 RID: 7106 RVA: 0x00063D3F File Offset: 0x00061F3F
			public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereListIterator<TSource>(this.source, Enumerable.CombinePredicates<TSource>(this.predicate, predicate));
			}

			// Token: 0x04000FA6 RID: 4006
			private List<TSource> source;

			// Token: 0x04000FA7 RID: 4007
			private Func<TSource, bool> predicate;

			// Token: 0x04000FA8 RID: 4008
			private List<TSource>.Enumerator enumerator;
		}

		// Token: 0x02000374 RID: 884
		private class SelectEnumerableIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IIListProvider<TResult>, IEnumerable<!1>, IEnumerable
		{
			// Token: 0x06001BC3 RID: 7107 RVA: 0x00063D58 File Offset: 0x00061F58
			public SelectEnumerableIterator(IEnumerable<TSource> source, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06001BC4 RID: 7108 RVA: 0x00063D6E File Offset: 0x00061F6E
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectEnumerableIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x06001BC5 RID: 7109 RVA: 0x00063D81 File Offset: 0x00061F81
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x06001BC6 RID: 7110 RVA: 0x00063DA4 File Offset: 0x00061FA4
			public override bool MoveNext()
			{
				int state = this.state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this.state = 2;
				}
				if (this._enumerator.MoveNext())
				{
					this.current = this._selector(this._enumerator.Current);
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06001BC7 RID: 7111 RVA: 0x00063E0C File Offset: 0x0006200C
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectEnumerableIterator<TSource, TResult2>(this._source, Enumerable.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x06001BC8 RID: 7112 RVA: 0x00063E25 File Offset: 0x00062025
			public override IEnumerable<TResult> Where(Func<TResult, bool> predicate)
			{
				return new Enumerable.WhereEnumerableIterator<TResult>(this, predicate);
			}

			// Token: 0x06001BC9 RID: 7113 RVA: 0x00063E30 File Offset: 0x00062030
			public TResult[] ToArray()
			{
				LargeArrayBuilder<TResult> largeArrayBuilder = new LargeArrayBuilder<TResult>(true);
				foreach (TSource arg in this._source)
				{
					largeArrayBuilder.Add(this._selector(arg));
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x06001BCA RID: 7114 RVA: 0x00063E98 File Offset: 0x00062098
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>();
				foreach (TSource arg in this._source)
				{
					list.Add(this._selector(arg));
				}
				return list;
			}

			// Token: 0x06001BCB RID: 7115 RVA: 0x00063EF8 File Offset: 0x000620F8
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource arg in this._source)
					{
						this._selector(arg);
						num++;
					}
					return num;
				}
			}

			// Token: 0x04000FA9 RID: 4009
			private readonly IEnumerable<TSource> _source;

			// Token: 0x04000FAA RID: 4010
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x04000FAB RID: 4011
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x02000375 RID: 885
		private class WhereSelectEnumerableIterator<TSource, TResult> : Enumerable.Iterator<TResult>
		{
			// Token: 0x06001BCC RID: 7116 RVA: 0x00063F58 File Offset: 0x00062158
			public WhereSelectEnumerableIterator(IEnumerable<TSource> source, Func<TSource, bool> predicate, Func<TSource, TResult> selector)
			{
				this.source = source;
				this.predicate = predicate;
				this.selector = selector;
			}

			// Token: 0x06001BCD RID: 7117 RVA: 0x00063F75 File Offset: 0x00062175
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult>(this.source, this.predicate, this.selector);
			}

			// Token: 0x06001BCE RID: 7118 RVA: 0x00063F8E File Offset: 0x0006218E
			public override void Dispose()
			{
				if (this.enumerator != null)
				{
					this.enumerator.Dispose();
				}
				this.enumerator = null;
				base.Dispose();
			}

			// Token: 0x06001BCF RID: 7119 RVA: 0x00063FB0 File Offset: 0x000621B0
			public override bool MoveNext()
			{
				int state = this.state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this.enumerator = this.source.GetEnumerator();
					this.state = 2;
				}
				while (this.enumerator.MoveNext())
				{
					TSource arg = this.enumerator.Current;
					if (this.predicate == null || this.predicate(arg))
					{
						this.current = this.selector(arg);
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06001BD0 RID: 7120 RVA: 0x00064032 File Offset: 0x00062232
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult2>(this.source, this.predicate, Enumerable.CombineSelectors<TSource, TResult, TResult2>(this.selector, selector));
			}

			// Token: 0x06001BD1 RID: 7121 RVA: 0x00064051 File Offset: 0x00062251
			public override IEnumerable<TResult> Where(Func<TResult, bool> predicate)
			{
				return new Enumerable.WhereEnumerableIterator<TResult>(this, predicate);
			}

			// Token: 0x04000FAC RID: 4012
			private IEnumerable<TSource> source;

			// Token: 0x04000FAD RID: 4013
			private Func<TSource, bool> predicate;

			// Token: 0x04000FAE RID: 4014
			private Func<TSource, TResult> selector;

			// Token: 0x04000FAF RID: 4015
			private IEnumerator<TSource> enumerator;
		}

		// Token: 0x02000376 RID: 886
		private class WhereSelectArrayIterator<TSource, TResult> : Enumerable.Iterator<TResult>
		{
			// Token: 0x06001BD2 RID: 7122 RVA: 0x0006405A File Offset: 0x0006225A
			public WhereSelectArrayIterator(TSource[] source, Func<TSource, bool> predicate, Func<TSource, TResult> selector)
			{
				this.source = source;
				this.predicate = predicate;
				this.selector = selector;
			}

			// Token: 0x06001BD3 RID: 7123 RVA: 0x00064077 File Offset: 0x00062277
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.WhereSelectArrayIterator<TSource, TResult>(this.source, this.predicate, this.selector);
			}

			// Token: 0x06001BD4 RID: 7124 RVA: 0x00064090 File Offset: 0x00062290
			public override bool MoveNext()
			{
				if (this.state == 1)
				{
					while (this.index < this.source.Length)
					{
						TSource arg = this.source[this.index];
						this.index++;
						if (this.predicate == null || this.predicate(arg))
						{
							this.current = this.selector(arg);
							return true;
						}
					}
					this.Dispose();
				}
				return false;
			}

			// Token: 0x06001BD5 RID: 7125 RVA: 0x00064109 File Offset: 0x00062309
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.WhereSelectArrayIterator<TSource, TResult2>(this.source, this.predicate, Enumerable.CombineSelectors<TSource, TResult, TResult2>(this.selector, selector));
			}

			// Token: 0x06001BD6 RID: 7126 RVA: 0x00064128 File Offset: 0x00062328
			public override IEnumerable<TResult> Where(Func<TResult, bool> predicate)
			{
				return new Enumerable.WhereEnumerableIterator<TResult>(this, predicate);
			}

			// Token: 0x04000FB0 RID: 4016
			private TSource[] source;

			// Token: 0x04000FB1 RID: 4017
			private Func<TSource, bool> predicate;

			// Token: 0x04000FB2 RID: 4018
			private Func<TSource, TResult> selector;

			// Token: 0x04000FB3 RID: 4019
			private int index;
		}

		// Token: 0x02000377 RID: 887
		private class WhereSelectListIterator<TSource, TResult> : Enumerable.Iterator<TResult>
		{
			// Token: 0x06001BD7 RID: 7127 RVA: 0x00064131 File Offset: 0x00062331
			public WhereSelectListIterator(List<TSource> source, Func<TSource, bool> predicate, Func<TSource, TResult> selector)
			{
				this.source = source;
				this.predicate = predicate;
				this.selector = selector;
			}

			// Token: 0x06001BD8 RID: 7128 RVA: 0x0006414E File Offset: 0x0006234E
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.WhereSelectListIterator<TSource, TResult>(this.source, this.predicate, this.selector);
			}

			// Token: 0x06001BD9 RID: 7129 RVA: 0x00064168 File Offset: 0x00062368
			public override bool MoveNext()
			{
				int state = this.state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this.enumerator = this.source.GetEnumerator();
					this.state = 2;
				}
				while (this.enumerator.MoveNext())
				{
					TSource arg = this.enumerator.Current;
					if (this.predicate == null || this.predicate(arg))
					{
						this.current = this.selector(arg);
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06001BDA RID: 7130 RVA: 0x000641EA File Offset: 0x000623EA
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.WhereSelectListIterator<TSource, TResult2>(this.source, this.predicate, Enumerable.CombineSelectors<TSource, TResult, TResult2>(this.selector, selector));
			}

			// Token: 0x06001BDB RID: 7131 RVA: 0x00064209 File Offset: 0x00062409
			public override IEnumerable<TResult> Where(Func<TResult, bool> predicate)
			{
				return new Enumerable.WhereEnumerableIterator<TResult>(this, predicate);
			}

			// Token: 0x04000FB4 RID: 4020
			private List<TSource> source;

			// Token: 0x04000FB5 RID: 4021
			private Func<TSource, bool> predicate;

			// Token: 0x04000FB6 RID: 4022
			private Func<TSource, TResult> selector;

			// Token: 0x04000FB7 RID: 4023
			private List<TSource>.Enumerator enumerator;
		}

		// Token: 0x02000378 RID: 888
		private abstract class AppendPrependIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06001BDC RID: 7132 RVA: 0x00064212 File Offset: 0x00062412
			protected AppendPrependIterator(IEnumerable<TSource> source)
			{
				this._source = source;
			}

			// Token: 0x06001BDD RID: 7133 RVA: 0x00064221 File Offset: 0x00062421
			protected void GetSourceEnumerator()
			{
				this.enumerator = this._source.GetEnumerator();
			}

			// Token: 0x06001BDE RID: 7134
			public abstract Enumerable.AppendPrependIterator<TSource> Append(TSource item);

			// Token: 0x06001BDF RID: 7135
			public abstract Enumerable.AppendPrependIterator<TSource> Prepend(TSource item);

			// Token: 0x06001BE0 RID: 7136 RVA: 0x00064234 File Offset: 0x00062434
			protected bool LoadFromEnumerator()
			{
				if (this.enumerator.MoveNext())
				{
					this.current = this.enumerator.Current;
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06001BE1 RID: 7137 RVA: 0x0006425D File Offset: 0x0006245D
			public override void Dispose()
			{
				if (this.enumerator != null)
				{
					this.enumerator.Dispose();
					this.enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x06001BE2 RID: 7138 RVA: 0x0006427F File Offset: 0x0006247F
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.SelectEnumerableIterator<TSource, TResult>(this, selector);
			}

			// Token: 0x06001BE3 RID: 7139 RVA: 0x00064288 File Offset: 0x00062488
			public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereEnumerableIterator<TSource>(this, predicate);
			}

			// Token: 0x06001BE4 RID: 7140
			public abstract TSource[] ToArray();

			// Token: 0x06001BE5 RID: 7141
			public abstract List<TSource> ToList();

			// Token: 0x06001BE6 RID: 7142
			public abstract int GetCount(bool onlyIfCheap);

			// Token: 0x04000FB8 RID: 4024
			protected readonly IEnumerable<TSource> _source;

			// Token: 0x04000FB9 RID: 4025
			protected IEnumerator<TSource> enumerator;
		}

		// Token: 0x02000379 RID: 889
		private class AppendPrepend1Iterator<TSource> : Enumerable.AppendPrependIterator<TSource>
		{
			// Token: 0x06001BE7 RID: 7143 RVA: 0x00064291 File Offset: 0x00062491
			public AppendPrepend1Iterator(IEnumerable<TSource> source, TSource item, bool appending) : base(source)
			{
				this._item = item;
				this._appending = appending;
			}

			// Token: 0x06001BE8 RID: 7144 RVA: 0x000642A8 File Offset: 0x000624A8
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.AppendPrepend1Iterator<TSource>(this._source, this._item, this._appending);
			}

			// Token: 0x06001BE9 RID: 7145 RVA: 0x000642C4 File Offset: 0x000624C4
			public override bool MoveNext()
			{
				switch (this.state)
				{
				case 1:
					this.state = 2;
					if (!this._appending)
					{
						this.current = this._item;
						return true;
					}
					break;
				case 2:
					break;
				case 3:
					goto IL_47;
				default:
					goto IL_67;
				}
				base.GetSourceEnumerator();
				this.state = 3;
				IL_47:
				if (base.LoadFromEnumerator())
				{
					return true;
				}
				if (this._appending)
				{
					this.current = this._item;
					return true;
				}
				IL_67:
				this.Dispose();
				return false;
			}

			// Token: 0x06001BEA RID: 7146 RVA: 0x00064340 File Offset: 0x00062540
			public override Enumerable.AppendPrependIterator<TSource> Append(TSource item)
			{
				if (this._appending)
				{
					return new Enumerable.AppendPrependN<TSource>(this._source, null, new SingleLinkedNode<TSource>(this._item).Add(item), 0, 2);
				}
				return new Enumerable.AppendPrependN<TSource>(this._source, new SingleLinkedNode<TSource>(this._item), new SingleLinkedNode<TSource>(item), 1, 1);
			}

			// Token: 0x06001BEB RID: 7147 RVA: 0x00064394 File Offset: 0x00062594
			public override Enumerable.AppendPrependIterator<TSource> Prepend(TSource item)
			{
				if (this._appending)
				{
					return new Enumerable.AppendPrependN<TSource>(this._source, new SingleLinkedNode<TSource>(item), new SingleLinkedNode<TSource>(this._item), 1, 1);
				}
				return new Enumerable.AppendPrependN<TSource>(this._source, new SingleLinkedNode<TSource>(this._item).Add(item), null, 2, 0);
			}

			// Token: 0x06001BEC RID: 7148 RVA: 0x000643E8 File Offset: 0x000625E8
			private TSource[] LazyToArray()
			{
				LargeArrayBuilder<TSource> largeArrayBuilder = new LargeArrayBuilder<TSource>(true);
				if (!this._appending)
				{
					largeArrayBuilder.SlowAdd(this._item);
				}
				largeArrayBuilder.AddRange(this._source);
				if (this._appending)
				{
					largeArrayBuilder.SlowAdd(this._item);
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x06001BED RID: 7149 RVA: 0x0006443C File Offset: 0x0006263C
			public override TSource[] ToArray()
			{
				int count = this.GetCount(true);
				if (count == -1)
				{
					return this.LazyToArray();
				}
				TSource[] array = new TSource[count];
				int arrayIndex;
				if (this._appending)
				{
					arrayIndex = 0;
				}
				else
				{
					array[0] = this._item;
					arrayIndex = 1;
				}
				EnumerableHelpers.Copy<TSource>(this._source, array, arrayIndex, count - 1);
				if (this._appending)
				{
					array[array.Length - 1] = this._item;
				}
				return array;
			}

			// Token: 0x06001BEE RID: 7150 RVA: 0x000644A8 File Offset: 0x000626A8
			public override List<TSource> ToList()
			{
				int count = this.GetCount(true);
				List<TSource> list = (count == -1) ? new List<TSource>() : new List<TSource>(count);
				if (!this._appending)
				{
					list.Add(this._item);
				}
				list.AddRange(this._source);
				if (this._appending)
				{
					list.Add(this._item);
				}
				return list;
			}

			// Token: 0x06001BEF RID: 7151 RVA: 0x00064504 File Offset: 0x00062704
			public override int GetCount(bool onlyIfCheap)
			{
				IIListProvider<TSource> iilistProvider = this._source as IIListProvider<TSource>;
				if (iilistProvider != null)
				{
					int count = iilistProvider.GetCount(onlyIfCheap);
					if (count != -1)
					{
						return count + 1;
					}
					return -1;
				}
				else
				{
					if (onlyIfCheap && !(this._source is ICollection<TSource>))
					{
						return -1;
					}
					return this._source.Count<TSource>() + 1;
				}
			}

			// Token: 0x04000FBA RID: 4026
			private readonly TSource _item;

			// Token: 0x04000FBB RID: 4027
			private readonly bool _appending;
		}

		// Token: 0x0200037A RID: 890
		private class AppendPrependN<TSource> : Enumerable.AppendPrependIterator<TSource>
		{
			// Token: 0x06001BF0 RID: 7152 RVA: 0x00064551 File Offset: 0x00062751
			public AppendPrependN(IEnumerable<TSource> source, SingleLinkedNode<TSource> prepended, SingleLinkedNode<TSource> appended, int prependCount, int appendCount) : base(source)
			{
				this._prepended = prepended;
				this._appended = appended;
				this._prependCount = prependCount;
				this._appendCount = appendCount;
			}

			// Token: 0x06001BF1 RID: 7153 RVA: 0x00064578 File Offset: 0x00062778
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.AppendPrependN<TSource>(this._source, this._prepended, this._appended, this._prependCount, this._appendCount);
			}

			// Token: 0x06001BF2 RID: 7154 RVA: 0x000645A0 File Offset: 0x000627A0
			public override bool MoveNext()
			{
				switch (this.state)
				{
				case 1:
					this._node = this._prepended;
					this.state = 2;
					break;
				case 2:
					break;
				case 3:
					goto IL_70;
				case 4:
					goto IL_A2;
				default:
					this.Dispose();
					return false;
				}
				if (this._node != null)
				{
					this.current = this._node.Item;
					this._node = this._node.Linked;
					return true;
				}
				base.GetSourceEnumerator();
				this.state = 3;
				IL_70:
				if (base.LoadFromEnumerator())
				{
					return true;
				}
				if (this._appended == null)
				{
					return false;
				}
				this.enumerator = this._appended.GetEnumerator(this._appendCount);
				this.state = 4;
				IL_A2:
				return base.LoadFromEnumerator();
			}

			// Token: 0x06001BF3 RID: 7155 RVA: 0x00064660 File Offset: 0x00062860
			public override Enumerable.AppendPrependIterator<TSource> Append(TSource item)
			{
				SingleLinkedNode<TSource> appended = (this._appended != null) ? this._appended.Add(item) : new SingleLinkedNode<TSource>(item);
				return new Enumerable.AppendPrependN<TSource>(this._source, this._prepended, appended, this._prependCount, this._appendCount + 1);
			}

			// Token: 0x06001BF4 RID: 7156 RVA: 0x000646AC File Offset: 0x000628AC
			public override Enumerable.AppendPrependIterator<TSource> Prepend(TSource item)
			{
				SingleLinkedNode<TSource> prepended = (this._prepended != null) ? this._prepended.Add(item) : new SingleLinkedNode<TSource>(item);
				return new Enumerable.AppendPrependN<TSource>(this._source, prepended, this._appended, this._prependCount + 1, this._appendCount);
			}

			// Token: 0x06001BF5 RID: 7157 RVA: 0x000646F8 File Offset: 0x000628F8
			private TSource[] LazyToArray()
			{
				SparseArrayBuilder<TSource> sparseArrayBuilder = new SparseArrayBuilder<TSource>(true);
				if (this._prepended != null)
				{
					sparseArrayBuilder.Reserve(this._prependCount);
				}
				sparseArrayBuilder.AddRange(this._source);
				if (this._appended != null)
				{
					sparseArrayBuilder.Reserve(this._appendCount);
				}
				TSource[] array = sparseArrayBuilder.ToArray();
				int num = 0;
				for (SingleLinkedNode<TSource> singleLinkedNode = this._prepended; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
				{
					array[num++] = singleLinkedNode.Item;
				}
				num = array.Length - 1;
				for (SingleLinkedNode<TSource> singleLinkedNode2 = this._appended; singleLinkedNode2 != null; singleLinkedNode2 = singleLinkedNode2.Linked)
				{
					array[num--] = singleLinkedNode2.Item;
				}
				return array;
			}

			// Token: 0x06001BF6 RID: 7158 RVA: 0x000647A4 File Offset: 0x000629A4
			public override TSource[] ToArray()
			{
				int count = this.GetCount(true);
				if (count == -1)
				{
					return this.LazyToArray();
				}
				TSource[] array = new TSource[count];
				int num = 0;
				for (SingleLinkedNode<TSource> singleLinkedNode = this._prepended; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
				{
					array[num] = singleLinkedNode.Item;
					num++;
				}
				ICollection<TSource> collection = this._source as ICollection<TSource>;
				if (collection != null)
				{
					collection.CopyTo(array, num);
				}
				else
				{
					foreach (TSource tsource in this._source)
					{
						array[num] = tsource;
						num++;
					}
				}
				num = array.Length;
				for (SingleLinkedNode<TSource> singleLinkedNode2 = this._appended; singleLinkedNode2 != null; singleLinkedNode2 = singleLinkedNode2.Linked)
				{
					num--;
					array[num] = singleLinkedNode2.Item;
				}
				return array;
			}

			// Token: 0x06001BF7 RID: 7159 RVA: 0x0006488C File Offset: 0x00062A8C
			public override List<TSource> ToList()
			{
				int count = this.GetCount(true);
				List<TSource> list = (count == -1) ? new List<TSource>() : new List<TSource>(count);
				for (SingleLinkedNode<TSource> singleLinkedNode = this._prepended; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
				{
					list.Add(singleLinkedNode.Item);
				}
				list.AddRange(this._source);
				if (this._appended != null)
				{
					IEnumerator<TSource> enumerator = this._appended.GetEnumerator(this._appendCount);
					while (enumerator.MoveNext())
					{
						TSource item = enumerator.Current;
						list.Add(item);
					}
				}
				return list;
			}

			// Token: 0x06001BF8 RID: 7160 RVA: 0x00064910 File Offset: 0x00062B10
			public override int GetCount(bool onlyIfCheap)
			{
				IIListProvider<TSource> iilistProvider = this._source as IIListProvider<TSource>;
				if (iilistProvider != null)
				{
					int count = iilistProvider.GetCount(onlyIfCheap);
					if (count != -1)
					{
						return count + this._appendCount + this._prependCount;
					}
					return -1;
				}
				else
				{
					if (onlyIfCheap && !(this._source is ICollection<TSource>))
					{
						return -1;
					}
					return this._source.Count<TSource>() + this._appendCount + this._prependCount;
				}
			}

			// Token: 0x04000FBC RID: 4028
			private readonly SingleLinkedNode<TSource> _prepended;

			// Token: 0x04000FBD RID: 4029
			private readonly SingleLinkedNode<TSource> _appended;

			// Token: 0x04000FBE RID: 4030
			private readonly int _prependCount;

			// Token: 0x04000FBF RID: 4031
			private readonly int _appendCount;

			// Token: 0x04000FC0 RID: 4032
			private SingleLinkedNode<TSource> _node;
		}
	}
}

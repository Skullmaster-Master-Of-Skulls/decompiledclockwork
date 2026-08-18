using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Validation;

namespace System.Linq
{
	// Token: 0x02000006 RID: 6
	public static class ImmutableArrayExtensions
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000022A2 File Offset: 0x000004A2
		public static IEnumerable<TResult> Select<T, TResult>(this ImmutableArray<T> immutableArray, Func<T, TResult> selector)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Select(selector);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022B7 File Offset: 0x000004B7
		public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this ImmutableArray<TSource> immutableArray, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			if (collectionSelector == null || resultSelector == null)
			{
				return immutableArray.SelectMany(collectionSelector, resultSelector);
			}
			if (immutableArray.Length != 0)
			{
				return immutableArray.SelectManyIterator(collectionSelector, resultSelector);
			}
			return Enumerable.Empty<TResult>();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022EB File Offset: 0x000004EB
		public static IEnumerable<T> Where<T>(this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Where(predicate);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002300 File Offset: 0x00000500
		public static bool Any<T>(this ImmutableArray<T> immutableArray)
		{
			return immutableArray.Length > 0;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000230C File Offset: 0x0000050C
		public static bool Any<T>(this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T arg in immutableArray.array)
			{
				if (predicate(arg))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002354 File Offset: 0x00000554
		public static bool All<T>(this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T arg in immutableArray.array)
			{
				if (!predicate(arg))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000239C File Offset: 0x0000059C
		public static bool SequenceEqual<TDerived, TBase>(this ImmutableArray<TBase> immutableArray, ImmutableArray<TDerived> items, IEqualityComparer<TBase> comparer = null) where TDerived : TBase
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			if (immutableArray.array == items.array)
			{
				return true;
			}
			if (immutableArray.Length != items.Length)
			{
				return false;
			}
			if (comparer == null)
			{
				comparer = EqualityComparer<TBase>.Default;
			}
			for (int i = 0; i < immutableArray.Length; i++)
			{
				if (!comparer.Equals(immutableArray.array[i], (TBase)((object)items.array[i])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002424 File Offset: 0x00000624
		public static bool SequenceEqual<TDerived, TBase>(this ImmutableArray<TBase> immutableArray, IEnumerable<TDerived> items, IEqualityComparer<TBase> comparer = null) where TDerived : TBase
		{
			Requires.NotNull<IEnumerable<TDerived>>(items, "items");
			if (comparer == null)
			{
				comparer = EqualityComparer<TBase>.Default;
			}
			int num = 0;
			int length = immutableArray.Length;
			foreach (TDerived tderived in items)
			{
				if (num == length)
				{
					return false;
				}
				if (!comparer.Equals(immutableArray[num], (TBase)((object)tderived)))
				{
					return false;
				}
				num++;
			}
			return num == length;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024B8 File Offset: 0x000006B8
		public static bool SequenceEqual<TDerived, TBase>(this ImmutableArray<TBase> immutableArray, ImmutableArray<TDerived> items, Func<TBase, TBase, bool> predicate) where TDerived : TBase
		{
			Requires.NotNull<Func<TBase, TBase, bool>>(predicate, "predicate");
			immutableArray.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			if (immutableArray.array == items.array)
			{
				return true;
			}
			if (immutableArray.Length != items.Length)
			{
				return false;
			}
			int i = 0;
			int length = immutableArray.Length;
			while (i < length)
			{
				if (!predicate(immutableArray[i], (TBase)((object)items[i])))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000253C File Offset: 0x0000073C
		public static T Aggregate<T>(this ImmutableArray<T> immutableArray, Func<T, T, T> func)
		{
			Requires.NotNull<Func<T, T, T>>(func, "func");
			if (immutableArray.Length == 0)
			{
				return default(T);
			}
			T t = immutableArray[0];
			int i = 1;
			int length = immutableArray.Length;
			while (i < length)
			{
				t = func(t, immutableArray[i]);
				i++;
			}
			return t;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002598 File Offset: 0x00000798
		public static TAccumulate Aggregate<TAccumulate, T>(this ImmutableArray<T> immutableArray, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func)
		{
			Requires.NotNull<Func<TAccumulate, T, TAccumulate>>(func, "func");
			TAccumulate taccumulate = seed;
			foreach (T arg in immutableArray.array)
			{
				taccumulate = func(taccumulate, arg);
			}
			return taccumulate;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025D9 File Offset: 0x000007D9
		public static TResult Aggregate<TAccumulate, TResult, T>(this ImmutableArray<T> immutableArray, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			Requires.NotNull<Func<TAccumulate, TResult>>(resultSelector, "resultSelector");
			return resultSelector(immutableArray.Aggregate(seed, func));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025F4 File Offset: 0x000007F4
		public static T ElementAt<T>(this ImmutableArray<T> immutableArray, int index)
		{
			return immutableArray[index];
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002600 File Offset: 0x00000800
		public static T ElementAtOrDefault<T>(this ImmutableArray<T> immutableArray, int index)
		{
			if (index < 0 || index >= immutableArray.Length)
			{
				return default(T);
			}
			return immutableArray[index];
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002630 File Offset: 0x00000830
		public static T First<T>(this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (predicate(t))
				{
					return t;
				}
			}
			return Enumerable.Empty<T>().First<T>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000267A File Offset: 0x0000087A
		public static T First<T>(this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.Length <= 0)
			{
				return immutableArray.array.First<T>();
			}
			return immutableArray[0];
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000269C File Offset: 0x0000089C
		public static T FirstOrDefault<T>(this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.array.Length == 0)
			{
				return default(T);
			}
			return immutableArray.array[0];
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026C8 File Offset: 0x000008C8
		public static T FirstOrDefault<T>(this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (predicate(t))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002711 File Offset: 0x00000911
		public static T Last<T>(this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.Length <= 0)
			{
				return immutableArray.array.Last<T>();
			}
			return immutableArray[immutableArray.Length - 1];
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002738 File Offset: 0x00000938
		public static T Last<T>(this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			for (int i = immutableArray.Length - 1; i >= 0; i--)
			{
				if (predicate(immutableArray[i]))
				{
					return immutableArray[i];
				}
			}
			return Enumerable.Empty<T>().Last<T>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002787 File Offset: 0x00000987
		public static T LastOrDefault<T>(this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.LastOrDefault<T>();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000279C File Offset: 0x0000099C
		public static T LastOrDefault<T>(this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			for (int i = immutableArray.Length - 1; i >= 0; i--)
			{
				if (predicate(immutableArray[i]))
				{
					return immutableArray[i];
				}
			}
			return default(T);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027EA File Offset: 0x000009EA
		public static T Single<T>(this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Single<T>();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002800 File Offset: 0x00000A00
		public static T Single<T>(this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			bool flag = true;
			T result = default(T);
			foreach (T t in immutableArray.array)
			{
				if (predicate(t))
				{
					if (!flag)
					{
						ImmutableArray.TwoElementArray.Single<byte>();
					}
					flag = false;
					result = t;
				}
			}
			if (flag)
			{
				Enumerable.Empty<T>().Single<T>();
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000286C File Offset: 0x00000A6C
		public static T SingleOrDefault<T>(this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.SingleOrDefault<T>();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002880 File Offset: 0x00000A80
		public static T SingleOrDefault<T>(this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			bool flag = true;
			T result = default(T);
			foreach (T t in immutableArray.array)
			{
				if (predicate(t))
				{
					if (!flag)
					{
						ImmutableArray.TwoElementArray.Single<byte>();
					}
					flag = false;
					result = t;
				}
			}
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028DE File Offset: 0x00000ADE
		public static Dictionary<TKey, T> ToDictionary<TKey, T>(this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector)
		{
			return immutableArray.ToDictionary(keySelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028EC File Offset: 0x00000AEC
		public static Dictionary<TKey, TElement> ToDictionary<TKey, TElement, T>(this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, Func<T, TElement> elementSelector)
		{
			return immutableArray.ToDictionary(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028FC File Offset: 0x00000AFC
		public static Dictionary<TKey, T> ToDictionary<TKey, T>(this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Requires.NotNull<Func<T, TKey>>(keySelector, "keySelector");
			Dictionary<TKey, T> dictionary = new Dictionary<TKey, T>(comparer);
			foreach (T t in immutableArray)
			{
				dictionary.Add(keySelector(t), t);
			}
			return dictionary;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002948 File Offset: 0x00000B48
		public static Dictionary<TKey, TElement> ToDictionary<TKey, TElement, T>(this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, Func<T, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Requires.NotNull<Func<T, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<T, TElement>>(elementSelector, "elementSelector");
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(immutableArray.Length, comparer);
			foreach (T arg in immutableArray.array)
			{
				dictionary.Add(keySelector(arg), elementSelector(arg));
			}
			return dictionary;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029AB File Offset: 0x00000BAB
		public static T[] ToArray<T>(this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			if (immutableArray.array.Length == 0)
			{
				return ImmutableArray<T>.Empty.array;
			}
			return (T[])immutableArray.array.Clone();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029D8 File Offset: 0x00000BD8
		public static T First<T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				throw new InvalidOperationException();
			}
			return builder[0];
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029FC File Offset: 0x00000BFC
		public static T FirstOrDefault<T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				return default(T);
			}
			return builder[0];
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A2D File Offset: 0x00000C2D
		public static T Last<T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				throw new InvalidOperationException();
			}
			return builder[builder.Count - 1];
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A58 File Offset: 0x00000C58
		public static T LastOrDefault<T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				return default(T);
			}
			return builder[builder.Count - 1];
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A90 File Offset: 0x00000C90
		public static bool Any<T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			return builder.Count > 0;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AA6 File Offset: 0x00000CA6
		private static IEnumerable<TResult> SelectManyIterator<TSource, TCollection, TResult>(this ImmutableArray<TSource> immutableArray, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			foreach (TSource item in immutableArray.array)
			{
				foreach (TCollection arg in collectionSelector(item))
				{
					yield return resultSelector(item, arg);
				}
				IEnumerator<TCollection> enumerator = null;
				item = default(TSource);
			}
			TSource[] array = null;
			yield break;
			yield break;
		}
	}
}

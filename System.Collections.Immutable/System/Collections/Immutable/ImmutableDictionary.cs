using System;
using System.Collections.Generic;
using System.Linq;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200001C RID: 28
	public static class ImmutableDictionary
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00004A87 File Offset: 0x00002C87
		public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>()
		{
			return ImmutableDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004A8E File Offset: 0x00002C8E
		public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>(IEqualityComparer<TKey> keyComparer)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004A9B File Offset: 0x00002C9B
		public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004AA9 File Offset: 0x00002CA9
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.AddRange(items);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004AB6 File Offset: 0x00002CB6
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEqualityComparer<TKey> keyComparer, IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004AC9 File Offset: 0x00002CC9
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004ADD File Offset: 0x00002CDD
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>()
		{
			return ImmutableDictionary.Create<TKey, TValue>().ToBuilder();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004AE9 File Offset: 0x00002CE9
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IEqualityComparer<TKey> keyComparer)
		{
			return ImmutableDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004AF6 File Offset: 0x00002CF6
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004B04 File Offset: 0x00002D04
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<TSource>>(source, "source");
			Requires.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<TSource, TValue>>(elementSelector, "elementSelector");
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(from element in source
			select new KeyValuePair<TKey, TValue>(keySelector(element), elementSelector(element)));
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004B74 File Offset: 0x00002D74
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary(keySelector, elementSelector, keyComparer, null);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004B80 File Offset: 0x00002D80
		public static ImmutableDictionary<TKey, TSource> ToImmutableDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToImmutableDictionary(keySelector, (TSource v) => v, null, null);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004BAA File Offset: 0x00002DAA
		public static ImmutableDictionary<TKey, TSource> ToImmutableDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary(keySelector, (TSource v) => v, keyComparer, null);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004BD4 File Offset: 0x00002DD4
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
		{
			return source.ToImmutableDictionary(keySelector, elementSelector, null, null);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004BE0 File Offset: 0x00002DE0
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(source, "source");
			ImmutableDictionary<TKey, TValue> immutableDictionary = source as ImmutableDictionary<TKey, TValue>;
			if (immutableDictionary != null)
			{
				return immutableDictionary.WithComparers(keyComparer, valueComparer);
			}
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004C1D File Offset: 0x00002E1D
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary(keyComparer, null);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004C27 File Offset: 0x00002E27
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return source.ToImmutableDictionary(null, null);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004C31 File Offset: 0x00002E31
		public static bool Contains<TKey, TValue>(this IImmutableDictionary<TKey, TValue> map, TKey key, TValue value)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(map, "map");
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return map.Contains(new KeyValuePair<TKey, TValue>(key, value));
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004C58 File Offset: 0x00002E58
		public static TValue GetValueOrDefault<TKey, TValue>(this IImmutableDictionary<TKey, TValue> dictionary, TKey key)
		{
			return dictionary.GetValueOrDefault(key, default(TValue));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004C78 File Offset: 0x00002E78
		public static TValue GetValueOrDefault<TKey, TValue>(this IImmutableDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, "dictionary");
			Requires.NotNullAllowStructs<TKey>(key, "key");
			TValue result;
			if (dictionary.TryGetValue(key, out result))
			{
				return result;
			}
			return defaultValue;
		}
	}
}

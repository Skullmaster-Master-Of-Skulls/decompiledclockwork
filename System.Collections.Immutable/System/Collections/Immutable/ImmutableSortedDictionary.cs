using System;
using System.Collections.Generic;
using System.Linq;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200002C RID: 44
	public static class ImmutableSortedDictionary
	{
		// Token: 0x0600029A RID: 666 RVA: 0x00007B66 File Offset: 0x00005D66
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, TValue>()
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00007B6D File Offset: 0x00005D6D
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, TValue>(IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00007B7A File Offset: 0x00005D7A
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, TValue>(IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007B88 File Offset: 0x00005D88
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.AddRange(items);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00007B95 File Offset: 0x00005D95
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(IComparer<TKey> keyComparer, IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007BA8 File Offset: 0x00005DA8
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007BBC File Offset: 0x00005DBC
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>()
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>().ToBuilder();
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00007BD5 File Offset: 0x00005DD5
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007BE4 File Offset: 0x00005DE4
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<TSource>>(source, "source");
			Requires.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<TSource, TValue>>(elementSelector, "elementSelector");
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(from element in source
			select new KeyValuePair<TKey, TValue>(keySelector(element), elementSelector(element)));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00007C54 File Offset: 0x00005E54
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary(keySelector, elementSelector, keyComparer, null);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00007C60 File Offset: 0x00005E60
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
		{
			return source.ToImmutableSortedDictionary(keySelector, elementSelector, null, null);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00007C6C File Offset: 0x00005E6C
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(source, "source");
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = source as ImmutableSortedDictionary<TKey, TValue>;
			if (immutableSortedDictionary != null)
			{
				return immutableSortedDictionary.WithComparers(keyComparer, valueComparer);
			}
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00007CA9 File Offset: 0x00005EA9
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary(keyComparer, null);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00007CB3 File Offset: 0x00005EB3
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return source.ToImmutableSortedDictionary(null, null);
		}
	}
}

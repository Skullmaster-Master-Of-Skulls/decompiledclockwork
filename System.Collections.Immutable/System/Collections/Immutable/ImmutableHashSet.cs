using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000021 RID: 33
	public static class ImmutableHashSet
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00005941 File Offset: 0x00003B41
		public static ImmutableHashSet<T> Create<T>()
		{
			return ImmutableHashSet<T>.Empty;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005948 File Offset: 0x00003B48
		public static ImmutableHashSet<T> Create<T>(IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005955 File Offset: 0x00003B55
		public static ImmutableHashSet<T> Create<T>(T item)
		{
			return ImmutableHashSet<T>.Empty.Add(item);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005962 File Offset: 0x00003B62
		public static ImmutableHashSet<T> Create<T>(IEqualityComparer<T> equalityComparer, T item)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Add(item);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005975 File Offset: 0x00003B75
		public static ImmutableHashSet<T> CreateRange<T>(IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005982 File Offset: 0x00003B82
		public static ImmutableHashSet<T> CreateRange<T>(IEqualityComparer<T> equalityComparer, IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00005975 File Offset: 0x00003B75
		public static ImmutableHashSet<T> Create<T>(params T[] items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005982 File Offset: 0x00003B82
		public static ImmutableHashSet<T> Create<T>(IEqualityComparer<T> equalityComparer, params T[] items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000059B5 File Offset: 0x00003BB5
		public static ImmutableHashSet<T>.Builder CreateBuilder<T>()
		{
			return ImmutableHashSet.Create<T>().ToBuilder();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000059C1 File Offset: 0x00003BC1
		public static ImmutableHashSet<T>.Builder CreateBuilder<T>(IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet.Create<T>(equalityComparer).ToBuilder();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000059D0 File Offset: 0x00003BD0
		public static ImmutableHashSet<TSource> ToImmutableHashSet<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> equalityComparer)
		{
			ImmutableHashSet<TSource> immutableHashSet = source as ImmutableHashSet<TSource>;
			if (immutableHashSet != null)
			{
				return immutableHashSet.WithComparer(equalityComparer);
			}
			return ImmutableHashSet<TSource>.Empty.WithComparer(equalityComparer).Union(source);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005A00 File Offset: 0x00003C00
		public static ImmutableHashSet<TSource> ToImmutableHashSet<TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableHashSet(null);
		}
	}
}

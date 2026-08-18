using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000030 RID: 48
	public static class ImmutableSortedSet
	{
		// Token: 0x060002EF RID: 751 RVA: 0x00008566 File Offset: 0x00006766
		public static ImmutableSortedSet<T> Create<T>()
		{
			return ImmutableSortedSet<T>.Empty;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000856D File Offset: 0x0000676D
		public static ImmutableSortedSet<T> Create<T>(IComparer<T> comparer)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000857A File Offset: 0x0000677A
		public static ImmutableSortedSet<T> Create<T>(T item)
		{
			return ImmutableSortedSet<T>.Empty.Add(item);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00008587 File Offset: 0x00006787
		public static ImmutableSortedSet<T> Create<T>(IComparer<T> comparer, T item)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Add(item);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000859A File Offset: 0x0000679A
		public static ImmutableSortedSet<T> CreateRange<T>(IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000085A7 File Offset: 0x000067A7
		public static ImmutableSortedSet<T> CreateRange<T>(IComparer<T> comparer, IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000859A File Offset: 0x0000679A
		public static ImmutableSortedSet<T> Create<T>(params T[] items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000085A7 File Offset: 0x000067A7
		public static ImmutableSortedSet<T> Create<T>(IComparer<T> comparer, params T[] items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000085DA File Offset: 0x000067DA
		public static ImmutableSortedSet<T>.Builder CreateBuilder<T>()
		{
			return ImmutableSortedSet.Create<T>().ToBuilder();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000085E6 File Offset: 0x000067E6
		public static ImmutableSortedSet<T>.Builder CreateBuilder<T>(IComparer<T> comparer)
		{
			return ImmutableSortedSet.Create<T>(comparer).ToBuilder();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000085F4 File Offset: 0x000067F4
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
		{
			ImmutableSortedSet<TSource> immutableSortedSet = source as ImmutableSortedSet<TSource>;
			if (immutableSortedSet != null)
			{
				return immutableSortedSet.WithComparer(comparer);
			}
			return ImmutableSortedSet<TSource>.Empty.WithComparer(comparer).Union(source);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00008624 File Offset: 0x00006824
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableSortedSet(null);
		}
	}
}

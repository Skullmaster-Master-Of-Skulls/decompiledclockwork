using System;
using System.Collections.Generic;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000025 RID: 37
	public static class ImmutableList
	{
		// Token: 0x0600020F RID: 527 RVA: 0x00006C3A File Offset: 0x00004E3A
		public static ImmutableList<T> Create<T>()
		{
			return ImmutableList<T>.Empty;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006C41 File Offset: 0x00004E41
		public static ImmutableList<T> Create<T>(T item)
		{
			return ImmutableList<T>.Empty.Add(item);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006C4E File Offset: 0x00004E4E
		public static ImmutableList<T> CreateRange<T>(IEnumerable<T> items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006C4E File Offset: 0x00004E4E
		public static ImmutableList<T> Create<T>(params T[] items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006C68 File Offset: 0x00004E68
		public static ImmutableList<T>.Builder CreateBuilder<T>()
		{
			return ImmutableList.Create<T>().ToBuilder();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006C74 File Offset: 0x00004E74
		public static ImmutableList<TSource> ToImmutableList<TSource>(this IEnumerable<TSource> source)
		{
			ImmutableList<TSource> immutableList = source as ImmutableList<TSource>;
			if (immutableList != null)
			{
				return immutableList;
			}
			return ImmutableList<TSource>.Empty.AddRange(source);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00006C98 File Offset: 0x00004E98
		public static IImmutableList<T> Replace<T>(this IImmutableList<T> list, T oldValue, T newValue)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006CB2 File Offset: 0x00004EB2
		public static IImmutableList<T> Remove<T>(this IImmutableList<T> list, T value)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Remove(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006CCB File Offset: 0x00004ECB
		public static IImmutableList<T> RemoveRange<T>(this IImmutableList<T> list, IEnumerable<T> items)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006CE4 File Offset: 0x00004EE4
		public static int IndexOf<T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, EqualityComparer<T>.Default);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006D04 File Offset: 0x00004F04
		public static int IndexOf<T>(this IImmutableList<T> list, T item, IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, equalityComparer);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00006D20 File Offset: 0x00004F20
		public static int IndexOf<T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, list.Count - startIndex, EqualityComparer<T>.Default);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006D42 File Offset: 0x00004F42
		public static int IndexOf<T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006D5D File Offset: 0x00004F5D
		public static int LastIndexOf<T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, EqualityComparer<T>.Default);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00006D8E File Offset: 0x00004F8E
		public static int LastIndexOf<T>(this IImmutableList<T> list, T item, IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, equalityComparer);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006DBB File Offset: 0x00004FBB
		public static int LastIndexOf<T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0 && startIndex == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006DE5 File Offset: 0x00004FE5
		public static int LastIndexOf<T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}
	}
}

using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000010 RID: 16
	public interface IImmutableList<T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000065 RID: 101
		IImmutableList<T> Clear();

		// Token: 0x06000066 RID: 102
		int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer);

		// Token: 0x06000067 RID: 103
		int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer);

		// Token: 0x06000068 RID: 104
		IImmutableList<T> Add(T value);

		// Token: 0x06000069 RID: 105
		IImmutableList<T> AddRange(IEnumerable<T> items);

		// Token: 0x0600006A RID: 106
		IImmutableList<T> Insert(int index, T element);

		// Token: 0x0600006B RID: 107
		IImmutableList<T> InsertRange(int index, IEnumerable<T> items);

		// Token: 0x0600006C RID: 108
		IImmutableList<T> Remove(T value, IEqualityComparer<T> equalityComparer);

		// Token: 0x0600006D RID: 109
		IImmutableList<T> RemoveAll(Predicate<T> match);

		// Token: 0x0600006E RID: 110
		IImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer);

		// Token: 0x0600006F RID: 111
		IImmutableList<T> RemoveRange(int index, int count);

		// Token: 0x06000070 RID: 112
		IImmutableList<T> RemoveAt(int index);

		// Token: 0x06000071 RID: 113
		IImmutableList<T> SetItem(int index, T value);

		// Token: 0x06000072 RID: 114
		IImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer);
	}
}

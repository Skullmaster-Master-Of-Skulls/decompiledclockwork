using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000011 RID: 17
	internal interface IImmutableListQueries<T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000073 RID: 115
		ImmutableList<TOutput> ConvertAll<TOutput>(Func<T, TOutput> converter);

		// Token: 0x06000074 RID: 116
		void ForEach(Action<T> action);

		// Token: 0x06000075 RID: 117
		ImmutableList<T> GetRange(int index, int count);

		// Token: 0x06000076 RID: 118
		void CopyTo(T[] array);

		// Token: 0x06000077 RID: 119
		void CopyTo(T[] array, int arrayIndex);

		// Token: 0x06000078 RID: 120
		void CopyTo(int index, T[] array, int arrayIndex, int count);

		// Token: 0x06000079 RID: 121
		bool Exists(Predicate<T> match);

		// Token: 0x0600007A RID: 122
		T Find(Predicate<T> match);

		// Token: 0x0600007B RID: 123
		ImmutableList<T> FindAll(Predicate<T> match);

		// Token: 0x0600007C RID: 124
		int FindIndex(Predicate<T> match);

		// Token: 0x0600007D RID: 125
		int FindIndex(int startIndex, Predicate<T> match);

		// Token: 0x0600007E RID: 126
		int FindIndex(int startIndex, int count, Predicate<T> match);

		// Token: 0x0600007F RID: 127
		T FindLast(Predicate<T> match);

		// Token: 0x06000080 RID: 128
		int FindLastIndex(Predicate<T> match);

		// Token: 0x06000081 RID: 129
		int FindLastIndex(int startIndex, Predicate<T> match);

		// Token: 0x06000082 RID: 130
		int FindLastIndex(int startIndex, int count, Predicate<T> match);

		// Token: 0x06000083 RID: 131
		bool TrueForAll(Predicate<T> match);

		// Token: 0x06000084 RID: 132
		int BinarySearch(T item);

		// Token: 0x06000085 RID: 133
		int BinarySearch(T item, IComparer<T> comparer);

		// Token: 0x06000086 RID: 134
		int BinarySearch(int index, int count, T item, IComparer<T> comparer);
	}
}

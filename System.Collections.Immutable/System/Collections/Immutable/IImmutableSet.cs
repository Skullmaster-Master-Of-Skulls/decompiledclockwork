using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000013 RID: 19
	public interface IImmutableSet<T> : IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x0600008C RID: 140
		IImmutableSet<T> Clear();

		// Token: 0x0600008D RID: 141
		bool Contains(T value);

		// Token: 0x0600008E RID: 142
		IImmutableSet<T> Add(T value);

		// Token: 0x0600008F RID: 143
		IImmutableSet<T> Remove(T value);

		// Token: 0x06000090 RID: 144
		bool TryGetValue(T equalValue, out T actualValue);

		// Token: 0x06000091 RID: 145
		IImmutableSet<T> Intersect(IEnumerable<T> other);

		// Token: 0x06000092 RID: 146
		IImmutableSet<T> Except(IEnumerable<T> other);

		// Token: 0x06000093 RID: 147
		IImmutableSet<T> SymmetricExcept(IEnumerable<T> other);

		// Token: 0x06000094 RID: 148
		IImmutableSet<T> Union(IEnumerable<T> other);

		// Token: 0x06000095 RID: 149
		bool SetEquals(IEnumerable<T> other);

		// Token: 0x06000096 RID: 150
		bool IsProperSubsetOf(IEnumerable<T> other);

		// Token: 0x06000097 RID: 151
		bool IsProperSupersetOf(IEnumerable<T> other);

		// Token: 0x06000098 RID: 152
		bool IsSubsetOf(IEnumerable<T> other);

		// Token: 0x06000099 RID: 153
		bool IsSupersetOf(IEnumerable<T> other);

		// Token: 0x0600009A RID: 154
		bool Overlaps(IEnumerable<T> other);
	}
}

using System;

namespace System.Collections.Generic
{
	// Token: 0x020003CE RID: 974
	[__DynamicallyInvokable]
	public interface ISet<T> : ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06002548 RID: 9544
		[__DynamicallyInvokable]
		bool Add(T item);

		// Token: 0x06002549 RID: 9545
		[__DynamicallyInvokable]
		void UnionWith(IEnumerable<T> other);

		// Token: 0x0600254A RID: 9546
		[__DynamicallyInvokable]
		void IntersectWith(IEnumerable<T> other);

		// Token: 0x0600254B RID: 9547
		[__DynamicallyInvokable]
		void ExceptWith(IEnumerable<T> other);

		// Token: 0x0600254C RID: 9548
		[__DynamicallyInvokable]
		void SymmetricExceptWith(IEnumerable<T> other);

		// Token: 0x0600254D RID: 9549
		[__DynamicallyInvokable]
		bool IsSubsetOf(IEnumerable<T> other);

		// Token: 0x0600254E RID: 9550
		[__DynamicallyInvokable]
		bool IsSupersetOf(IEnumerable<T> other);

		// Token: 0x0600254F RID: 9551
		[__DynamicallyInvokable]
		bool IsProperSupersetOf(IEnumerable<T> other);

		// Token: 0x06002550 RID: 9552
		[__DynamicallyInvokable]
		bool IsProperSubsetOf(IEnumerable<T> other);

		// Token: 0x06002551 RID: 9553
		[__DynamicallyInvokable]
		bool Overlaps(IEnumerable<T> other);

		// Token: 0x06002552 RID: 9554
		[__DynamicallyInvokable]
		bool SetEquals(IEnumerable<T> other);
	}
}

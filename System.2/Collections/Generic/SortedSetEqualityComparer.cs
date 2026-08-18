using System;

namespace System.Collections.Generic
{
	// Token: 0x020003CC RID: 972
	internal class SortedSetEqualityComparer<T> : IEqualityComparer<SortedSet<T>>
	{
		// Token: 0x0600253E RID: 9534 RVA: 0x000ADAF3 File Offset: 0x000ABCF3
		public SortedSetEqualityComparer() : this(null, null)
		{
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000ADAFD File Offset: 0x000ABCFD
		public SortedSetEqualityComparer(IComparer<T> comparer) : this(comparer, null)
		{
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000ADB07 File Offset: 0x000ABD07
		public SortedSetEqualityComparer(IEqualityComparer<T> memberEqualityComparer) : this(null, memberEqualityComparer)
		{
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000ADB11 File Offset: 0x000ABD11
		public SortedSetEqualityComparer(IComparer<T> comparer, IEqualityComparer<T> memberEqualityComparer)
		{
			if (comparer == null)
			{
				this.comparer = Comparer<T>.Default;
			}
			else
			{
				this.comparer = comparer;
			}
			if (memberEqualityComparer == null)
			{
				this.e_comparer = EqualityComparer<T>.Default;
				return;
			}
			this.e_comparer = memberEqualityComparer;
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000ADB46 File Offset: 0x000ABD46
		public bool Equals(SortedSet<T> x, SortedSet<T> y)
		{
			return SortedSet<T>.SortedSetEquals(x, y, this.comparer);
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000ADB58 File Offset: 0x000ABD58
		public int GetHashCode(SortedSet<T> obj)
		{
			int num = 0;
			if (obj != null)
			{
				foreach (T obj2 in obj)
				{
					num ^= (this.e_comparer.GetHashCode(obj2) & int.MaxValue);
				}
			}
			return num;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000ADBBC File Offset: 0x000ABDBC
		public override bool Equals(object obj)
		{
			SortedSetEqualityComparer<T> sortedSetEqualityComparer = obj as SortedSetEqualityComparer<T>;
			return sortedSetEqualityComparer != null && this.comparer == sortedSetEqualityComparer.comparer;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000ADBE3 File Offset: 0x000ABDE3
		public override int GetHashCode()
		{
			return this.comparer.GetHashCode() ^ this.e_comparer.GetHashCode();
		}

		// Token: 0x0400204C RID: 8268
		private IComparer<T> comparer;

		// Token: 0x0400204D RID: 8269
		private IEqualityComparer<T> e_comparer;
	}
}

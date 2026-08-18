using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200015C RID: 348
	internal class EnumerableSorter<TElement, TKey> : EnumerableSorter<TElement>
	{
		// Token: 0x06000C28 RID: 3112 RVA: 0x0002D092 File Offset: 0x0002B292
		internal EnumerableSorter(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, EnumerableSorter<TElement> next)
		{
			this.keySelector = keySelector;
			this.comparer = comparer;
			this.descending = descending;
			this.next = next;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002D0B8 File Offset: 0x0002B2B8
		internal override void ComputeKeys(TElement[] elements, int count)
		{
			this.keys = new TKey[count];
			for (int i = 0; i < count; i++)
			{
				this.keys[i] = this.keySelector(elements[i]);
			}
			if (this.next != null)
			{
				this.next.ComputeKeys(elements, count);
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002D110 File Offset: 0x0002B310
		internal override int CompareKeys(int index1, int index2)
		{
			int num = this.comparer.Compare(this.keys[index1], this.keys[index2]);
			if (num == 0)
			{
				if (this.next == null)
				{
					return index1 - index2;
				}
				return this.next.CompareKeys(index1, index2);
			}
			else
			{
				if (!this.descending)
				{
					return num;
				}
				return -num;
			}
		}

		// Token: 0x04000793 RID: 1939
		internal Func<TElement, TKey> keySelector;

		// Token: 0x04000794 RID: 1940
		internal IComparer<TKey> comparer;

		// Token: 0x04000795 RID: 1941
		internal bool descending;

		// Token: 0x04000796 RID: 1942
		internal EnumerableSorter<TElement> next;

		// Token: 0x04000797 RID: 1943
		internal TKey[] keys;
	}
}

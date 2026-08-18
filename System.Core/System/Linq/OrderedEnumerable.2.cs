using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200015A RID: 346
	internal class OrderedEnumerable<TElement, TKey> : OrderedEnumerable<TElement>
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x0002CF18 File Offset: 0x0002B118
		internal OrderedEnumerable(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			this.source = source;
			this.parent = null;
			this.keySelector = keySelector;
			IComparer<TKey> comparer2;
			if (comparer == null)
			{
				IComparer<TKey> @default = Comparer<TKey>.Default;
				comparer2 = @default;
			}
			else
			{
				comparer2 = comparer;
			}
			this.comparer = comparer2;
			this.descending = descending;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002CF78 File Offset: 0x0002B178
		internal override EnumerableSorter<TElement> GetEnumerableSorter(EnumerableSorter<TElement> next)
		{
			EnumerableSorter<TElement> enumerableSorter = new EnumerableSorter<TElement, TKey>(this.keySelector, this.comparer, this.descending, next);
			if (this.parent != null)
			{
				enumerableSorter = this.parent.GetEnumerableSorter(enumerableSorter);
			}
			return enumerableSorter;
		}

		// Token: 0x0400078F RID: 1935
		internal OrderedEnumerable<TElement> parent;

		// Token: 0x04000790 RID: 1936
		internal Func<TElement, TKey> keySelector;

		// Token: 0x04000791 RID: 1937
		internal IComparer<TKey> comparer;

		// Token: 0x04000792 RID: 1938
		internal bool descending;
	}
}

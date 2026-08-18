using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000159 RID: 345
	internal abstract class OrderedEnumerable<TElement> : IOrderedEnumerable<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000C1C RID: 3100 RVA: 0x0002CED2 File Offset: 0x0002B0D2
		public IEnumerator<TElement> GetEnumerator()
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this.source);
			if (buffer.count > 0)
			{
				EnumerableSorter<TElement> enumerableSorter = this.GetEnumerableSorter(null);
				int[] map = enumerableSorter.Sort(buffer.items, buffer.count);
				int num;
				for (int i = 0; i < buffer.count; i = num + 1)
				{
					yield return buffer.items[map[i]];
					num = i;
				}
				map = null;
			}
			yield break;
		}

		// Token: 0x06000C1D RID: 3101
		internal abstract EnumerableSorter<TElement> GetEnumerableSorter(EnumerableSorter<TElement> next);

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002CEE1 File Offset: 0x0002B0E1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002CEEC File Offset: 0x0002B0EC
		IOrderedEnumerable<TElement> IOrderedEnumerable<!0>.CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
		{
			return new OrderedEnumerable<TElement, TKey>(this.source, keySelector, comparer, descending)
			{
				parent = this
			};
		}

		// Token: 0x0400078E RID: 1934
		internal IEnumerable<TElement> source;
	}
}

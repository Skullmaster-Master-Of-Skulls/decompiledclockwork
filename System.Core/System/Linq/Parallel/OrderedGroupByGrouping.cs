using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001DB RID: 475
	internal class OrderedGroupByGrouping<TGroupKey, TOrderKey, TElement> : IGrouping<TGroupKey, TElement>, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x00036F7F File Offset: 0x0003517F
		internal OrderedGroupByGrouping(TGroupKey groupKey, IComparer<TOrderKey> orderComparer)
		{
			this.m_groupKey = groupKey;
			this.m_values = new GrowingArray<TElement>();
			this.m_orderKeys = new GrowingArray<TOrderKey>();
			this.m_orderComparer = orderComparer;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00036FAB File Offset: 0x000351AB
		TGroupKey IGrouping<!0, !2>.Key
		{
			get
			{
				return this.m_groupKey;
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00036FB3 File Offset: 0x000351B3
		IEnumerator<TElement> IEnumerable<!2>.GetEnumerator()
		{
			int valueCount = this.m_values.Count;
			TElement[] valueArray = this.m_values.InternalArray;
			int num;
			for (int i = 0; i < valueCount; i = num + 1)
			{
				yield return valueArray[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00036FC2 File Offset: 0x000351C2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TElement>)this).GetEnumerator();
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00036FCA File Offset: 0x000351CA
		internal void Add(TElement value, TOrderKey orderKey)
		{
			this.m_values.Add(value);
			this.m_orderKeys.Add(orderKey);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00036FE4 File Offset: 0x000351E4
		internal void DoneAdding()
		{
			Array.Sort<TOrderKey, TElement>(this.m_orderKeys.InternalArray, this.m_values.InternalArray, 0, this.m_values.Count, this.m_orderComparer);
		}

		// Token: 0x040008D9 RID: 2265
		private TGroupKey m_groupKey;

		// Token: 0x040008DA RID: 2266
		private GrowingArray<TElement> m_values;

		// Token: 0x040008DB RID: 2267
		private GrowingArray<TOrderKey> m_orderKeys;

		// Token: 0x040008DC RID: 2268
		private IComparer<TOrderKey> m_orderComparer;
	}
}

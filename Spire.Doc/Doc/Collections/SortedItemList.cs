using System;
using System.Collections.Generic;

namespace Spire.Doc.Collections
{
	// Token: 0x02000545 RID: 1349
	public class SortedItemList<TKey, TValue> : TypedSortedListEx<TKey, TValue> where TKey : IComparable
	{
		// Token: 0x06004659 RID: 18009 RVA: 0x0040EA3C File Offset: 0x0040DA3C
		public SortedItemList()
		{
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x0040EA50 File Offset: 0x0040DA50
		public SortedItemList(IComparer<TKey> comparer) : base(comparer)
		{
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x0040EA64 File Offset: 0x0040DA64
		public SortedItemList(int count) : base(count)
		{
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x0040EA78 File Offset: 0x0040DA78
		public SortedItemList(IDictionary<TKey, TValue> dictionary) : base(dictionary)
		{
		}
	}
}

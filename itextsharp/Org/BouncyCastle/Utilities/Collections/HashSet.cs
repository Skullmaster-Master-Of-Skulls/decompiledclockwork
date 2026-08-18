using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x0200033D RID: 829
	public class HashSet : ISet, ICollection, IEnumerable
	{
		// Token: 0x06001DFE RID: 7678 RVA: 0x000B49B4 File Offset: 0x000B39B4
		public HashSet()
		{
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000B49C8 File Offset: 0x000B39C8
		public HashSet(ISet s)
		{
			foreach (object o in s)
			{
				this.Add(o);
			}
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x000B4A28 File Offset: 0x000B3A28
		public void Add(object o)
		{
			this.impl[o] = null;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x000B4A38 File Offset: 0x000B3A38
		public void AddAll(IEnumerable e)
		{
			foreach (object o in e)
			{
				this.Add(o);
			}
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000B4A88 File Offset: 0x000B3A88
		public void Clear()
		{
			this.impl.Clear();
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000B4A95 File Offset: 0x000B3A95
		public bool Contains(object o)
		{
			return this.impl.ContainsKey(o);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000B4AA3 File Offset: 0x000B3AA3
		public void CopyTo(Array array, int index)
		{
			this.impl.Keys.CopyTo(array, index);
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x000B4AB7 File Offset: 0x000B3AB7
		public int Count
		{
			get
			{
				return this.impl.Count;
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x000B4AC4 File Offset: 0x000B3AC4
		public IEnumerator GetEnumerator()
		{
			return this.impl.Keys.GetEnumerator();
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x000B4AD6 File Offset: 0x000B3AD6
		public bool IsEmpty
		{
			get
			{
				return this.impl.Count == 0;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x000B4AE6 File Offset: 0x000B3AE6
		public bool IsSynchronized
		{
			get
			{
				return this.impl.IsSynchronized;
			}
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000B4AF3 File Offset: 0x000B3AF3
		public void Remove(object o)
		{
			this.impl.Remove(o);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000B4B04 File Offset: 0x000B3B04
		public void RemoveAll(IEnumerable e)
		{
			foreach (object o in e)
			{
				this.Remove(o);
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x000B4B54 File Offset: 0x000B3B54
		public object SyncRoot
		{
			get
			{
				return this.impl.SyncRoot;
			}
		}

		// Token: 0x040014F2 RID: 5362
		private readonly Hashtable impl = new Hashtable();
	}
}

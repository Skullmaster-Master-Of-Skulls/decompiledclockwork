using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Expressions
{
	// Token: 0x02000272 RID: 626
	internal sealed class Set<T> : ICollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x0600165F RID: 5727 RVA: 0x00049FD0 File Offset: 0x000481D0
		internal Set()
		{
			this._data = new Dictionary<T, object>();
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00049FE3 File Offset: 0x000481E3
		internal Set(IEqualityComparer<T> comparer)
		{
			this._data = new Dictionary<T, object>(comparer);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00049FF8 File Offset: 0x000481F8
		internal Set(IList<T> list)
		{
			this._data = new Dictionary<T, object>(list.Count);
			foreach (T item in list)
			{
				this.Add(item);
			}
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0004A058 File Offset: 0x00048258
		internal Set(IEnumerable<T> list)
		{
			this._data = new Dictionary<T, object>();
			foreach (T item in list)
			{
				this.Add(item);
			}
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0004A0B4 File Offset: 0x000482B4
		internal Set(int capacity)
		{
			this._data = new Dictionary<T, object>(capacity);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0004A0C8 File Offset: 0x000482C8
		public void Add(T item)
		{
			this._data[item] = null;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0004A0D7 File Offset: 0x000482D7
		public void Clear()
		{
			this._data.Clear();
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0004A0E4 File Offset: 0x000482E4
		public bool Contains(T item)
		{
			return this._data.ContainsKey(item);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0004A0F2 File Offset: 0x000482F2
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._data.Keys.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0004A106 File Offset: 0x00048306
		public int Count
		{
			get
			{
				return this._data.Count;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0004A113 File Offset: 0x00048313
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0004A116 File Offset: 0x00048316
		public bool Remove(T item)
		{
			return this._data.Remove(item);
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0004A124 File Offset: 0x00048324
		public IEnumerator<T> GetEnumerator()
		{
			return this._data.Keys.GetEnumerator();
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0004A13B File Offset: 0x0004833B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._data.Keys.GetEnumerator();
		}

		// Token: 0x04000A6C RID: 2668
		private readonly Dictionary<T, object> _data;
	}
}

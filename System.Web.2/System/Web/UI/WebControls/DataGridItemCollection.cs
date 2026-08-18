using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003C0 RID: 960
	public class DataGridItemCollection : ICollection, IEnumerable
	{
		// Token: 0x06002E68 RID: 11880 RVA: 0x0009824F File Offset: 0x0009644F
		public DataGridItemCollection(ArrayList items)
		{
			this.items = items;
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x0009825E File Offset: 0x0009645E
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000D43 RID: 3395
		public DataGridItem this[int index]
		{
			get
			{
				return (DataGridItem)this.items[index];
			}
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x00098280 File Offset: 0x00096480
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000982B0 File Offset: 0x000964B0
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x04001FF1 RID: 8177
		private ArrayList items;
	}
}

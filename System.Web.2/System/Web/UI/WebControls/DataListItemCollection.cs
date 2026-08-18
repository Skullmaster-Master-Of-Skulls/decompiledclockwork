using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003D0 RID: 976
	public sealed class DataListItemCollection : ICollection, IEnumerable
	{
		// Token: 0x06002F2C RID: 12076 RVA: 0x0009A30E File Offset: 0x0009850E
		public DataListItemCollection(ArrayList items)
		{
			this.items = items;
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x0009A31D File Offset: 0x0009851D
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000D8F RID: 3471
		public DataListItem this[int index]
		{
			get
			{
				return (DataListItem)this.items[index];
			}
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x0009A340 File Offset: 0x00098540
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x0009A370 File Offset: 0x00098570
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x04002029 RID: 8233
		private ArrayList items;
	}
}

using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003E8 RID: 1000
	public class DetailsViewRowCollection : ICollection, IEnumerable
	{
		// Token: 0x06003063 RID: 12387 RVA: 0x0009E639 File Offset: 0x0009C839
		public DetailsViewRowCollection(ArrayList rows)
		{
			this._rows = rows;
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x0009E648 File Offset: 0x0009C848
		public int Count
		{
			get
			{
				return this._rows.Count;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06003067 RID: 12391 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000DF5 RID: 3573
		public DetailsViewRow this[int index]
		{
			get
			{
				return (DetailsViewRow)this._rows[index];
			}
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x00095DD9 File Offset: 0x00093FD9
		public void CopyTo(DetailsViewRow[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x0009E668 File Offset: 0x0009C868
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0009E698 File Offset: 0x0009C898
		public IEnumerator GetEnumerator()
		{
			return this._rows.GetEnumerator();
		}

		// Token: 0x04002089 RID: 8329
		private ArrayList _rows;
	}
}

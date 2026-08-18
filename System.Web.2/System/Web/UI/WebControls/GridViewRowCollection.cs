using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000423 RID: 1059
	public class GridViewRowCollection : ICollection, IEnumerable
	{
		// Token: 0x060033A5 RID: 13221 RVA: 0x000A910E File Offset: 0x000A730E
		public GridViewRowCollection(ArrayList rows)
		{
			this._rows = rows;
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x060033A6 RID: 13222 RVA: 0x000A911D File Offset: 0x000A731D
		public int Count
		{
			get
			{
				return this._rows.Count;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000EFB RID: 3835
		public GridViewRow this[int index]
		{
			get
			{
				return (GridViewRow)this._rows[index];
			}
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x00095DD9 File Offset: 0x00093FD9
		public void CopyTo(GridViewRow[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000A9140 File Offset: 0x000A7340
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000A9170 File Offset: 0x000A7370
		public IEnumerator GetEnumerator()
		{
			return this._rows.GetEnumerator();
		}

		// Token: 0x04002173 RID: 8563
		private ArrayList _rows;
	}
}

using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200021A RID: 538
	[ListBindable(false)]
	public class DataGridViewSelectedRowCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x0600230C RID: 8972 RVA: 0x000A7239 File Offset: 0x000A5439
		int IList.Add(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x000A73EF File Offset: 0x000A55EF
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000A73FD File Offset: 0x000A55FD
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.Remove(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x00013062 File Offset: 0x00011262
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x00013062 File Offset: 0x00011262
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000803 RID: 2051
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
			}
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000A7419 File Offset: 0x000A5619
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x000A7428 File Offset: 0x000A5628
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000A7435 File Offset: 0x000A5635
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000A7442 File Offset: 0x000A5642
		internal DataGridViewSelectedRowCollection()
		{
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x000A7455 File Offset: 0x000A5655
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000808 RID: 2056
		public DataGridViewRow this[int index]
		{
			get
			{
				return (DataGridViewRow)this.items[index];
			}
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000A7470 File Offset: 0x000A5670
		internal int Add(DataGridViewRow dataGridViewRow)
		{
			return this.items.Add(dataGridViewRow);
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x000A7239 File Offset: 0x000A5439
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000A747E File Offset: 0x000A567E
		public bool Contains(DataGridViewRow dataGridViewRow)
		{
			return this.items.IndexOf(dataGridViewRow) != -1;
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000A7419 File Offset: 0x000A5619
		public void CopyTo(DataGridViewRow[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000A7239 File Offset: 0x000A5439
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Insert(int index, DataGridViewRow dataGridViewRow)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x04000E6E RID: 3694
		private ArrayList items = new ArrayList();
	}
}

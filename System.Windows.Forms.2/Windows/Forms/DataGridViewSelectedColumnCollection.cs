using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000219 RID: 537
	[ListBindable(false)]
	public class DataGridViewSelectedColumnCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x060022F4 RID: 8948 RVA: 0x000A7239 File Offset: 0x000A5439
		int IList.Add(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000A734C File Offset: 0x000A554C
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000A735A File Offset: 0x000A555A
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.Remove(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x00013062 File Offset: 0x00011262
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x00013062 File Offset: 0x00011262
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007FB RID: 2043
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

		// Token: 0x060022FF RID: 8959 RVA: 0x000A7376 File Offset: 0x000A5576
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x000A7385 File Offset: 0x000A5585
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000A7392 File Offset: 0x000A5592
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x000A739F File Offset: 0x000A559F
		internal DataGridViewSelectedColumnCollection()
		{
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x000A73B2 File Offset: 0x000A55B2
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000800 RID: 2048
		public DataGridViewColumn this[int index]
		{
			get
			{
				return (DataGridViewColumn)this.items[index];
			}
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x000A73CD File Offset: 0x000A55CD
		internal int Add(DataGridViewColumn dataGridViewColumn)
		{
			return this.items.Add(dataGridViewColumn);
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x000A7239 File Offset: 0x000A5439
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x000A73DB File Offset: 0x000A55DB
		public bool Contains(DataGridViewColumn dataGridViewColumn)
		{
			return this.items.IndexOf(dataGridViewColumn) != -1;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000A7376 File Offset: 0x000A5576
		public void CopyTo(DataGridViewColumn[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000A7239 File Offset: 0x000A5439
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Insert(int index, DataGridViewColumn dataGridViewColumn)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x04000E6D RID: 3693
		private ArrayList items = new ArrayList();
	}
}

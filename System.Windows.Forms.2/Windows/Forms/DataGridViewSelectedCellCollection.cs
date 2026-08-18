using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000218 RID: 536
	[ListBindable(false)]
	public class DataGridViewSelectedCellCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x060022DB RID: 8923 RVA: 0x000A7239 File Offset: 0x000A5439
		int IList.Add(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000A724A File Offset: 0x000A544A
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000A7258 File Offset: 0x000A5458
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.Remove(object value)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000A7239 File Offset: 0x000A5439
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x00013062 File Offset: 0x00011262
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x00013062 File Offset: 0x00011262
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007F3 RID: 2035
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

		// Token: 0x060022E6 RID: 8934 RVA: 0x000A7274 File Offset: 0x000A5474
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x000A7283 File Offset: 0x000A5483
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000A7290 File Offset: 0x000A5490
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000A729D File Offset: 0x000A549D
		internal DataGridViewSelectedCellCollection()
		{
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x000A72B0 File Offset: 0x000A54B0
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170007F8 RID: 2040
		public DataGridViewCell this[int index]
		{
			get
			{
				return (DataGridViewCell)this.items[index];
			}
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000A72CB File Offset: 0x000A54CB
		internal int Add(DataGridViewCell dataGridViewCell)
		{
			return this.items.Add(dataGridViewCell);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000A72DC File Offset: 0x000A54DC
		internal void AddCellLinkedList(DataGridViewCellLinkedList dataGridViewCells)
		{
			foreach (object obj in ((IEnumerable)dataGridViewCells))
			{
				DataGridViewCell value = (DataGridViewCell)obj;
				this.items.Add(value);
			}
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000A7239 File Offset: 0x000A5439
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Clear()
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000A7338 File Offset: 0x000A5538
		public bool Contains(DataGridViewCell dataGridViewCell)
		{
			return this.items.IndexOf(dataGridViewCell) != -1;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000A7274 File Offset: 0x000A5474
		public void CopyTo(DataGridViewCell[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x000A7239 File Offset: 0x000A5439
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Insert(int index, DataGridViewCell dataGridViewCell)
		{
			throw new NotSupportedException(SR.GetString("DataGridView_ReadOnlyCollection"));
		}

		// Token: 0x04000E6C RID: 3692
		private ArrayList items = new ArrayList();
	}
}

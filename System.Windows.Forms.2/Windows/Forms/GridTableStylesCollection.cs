using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x0200018B RID: 395
	[ListBindable(false)]
	public class GridTableStylesCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06001820 RID: 6176 RVA: 0x00056B6D File Offset: 0x00054D6D
		int IList.Add(object value)
		{
			return this.Add((DataGridTableStyle)value);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00056B7B File Offset: 0x00054D7B
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00056B83 File Offset: 0x00054D83
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00056B91 File Offset: 0x00054D91
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0000A547 File Offset: 0x00008747
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00056B9F File Offset: 0x00054D9F
		void IList.Remove(object value)
		{
			this.Remove((DataGridTableStyle)value);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00056BAD File Offset: 0x00054DAD
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000571 RID: 1393
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00056BC4 File Offset: 0x00054DC4
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x00056BD3 File Offset: 0x00054DD3
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00056BE0 File Offset: 0x00054DE0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00056BED File Offset: 0x00054DED
		internal GridTableStylesCollection(DataGrid grid)
		{
			this.owner = grid;
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x00056C07 File Offset: 0x00054E07
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000576 RID: 1398
		public DataGridTableStyle this[int index]
		{
			get
			{
				return (DataGridTableStyle)this.items[index];
			}
		}

		// Token: 0x17000577 RID: 1399
		public DataGridTableStyle this[string tableName]
		{
			get
			{
				if (tableName == null)
				{
					throw new ArgumentNullException("tableName");
				}
				int count = this.items.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)this.items[i];
					if (string.Equals(dataGridTableStyle.MappingName, tableName, StringComparison.OrdinalIgnoreCase))
					{
						return dataGridTableStyle;
					}
				}
				return null;
			}
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00056C7C File Offset: 0x00054E7C
		internal void CheckForMappingNameDuplicates(DataGridTableStyle table)
		{
			if (string.IsNullOrEmpty(table.MappingName))
			{
				return;
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				if (((DataGridTableStyle)this.items[i]).MappingName.Equals(table.MappingName) && table != this.items[i])
				{
					throw new ArgumentException(SR.GetString("DataGridTableStyleDuplicateMappingName"), "table");
				}
			}
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00056CF4 File Offset: 0x00054EF4
		public virtual int Add(DataGridTableStyle table)
		{
			if (this.owner != null && this.owner.MinimumRowHeaderWidth() > table.RowHeaderWidth)
			{
				table.RowHeaderWidth = this.owner.MinimumRowHeaderWidth();
			}
			if (table.DataGrid != this.owner && table.DataGrid != null)
			{
				throw new ArgumentException(SR.GetString("DataGridTableStyleCollectionAddedParentedTableStyle"), "table");
			}
			table.DataGrid = this.owner;
			this.CheckForMappingNameDuplicates(table);
			table.MappingNameChanged += this.TableStyleMappingNameChanged;
			int result = this.items.Add(table);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
			return result;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00056D98 File Offset: 0x00054F98
		private void TableStyleMappingNameChanged(object sender, EventArgs pcea)
		{
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00056DA8 File Offset: 0x00054FA8
		public virtual void AddRange(DataGridTableStyle[] tables)
		{
			if (tables == null)
			{
				throw new ArgumentNullException("tables");
			}
			foreach (DataGridTableStyle dataGridTableStyle in tables)
			{
				dataGridTableStyle.DataGrid = this.owner;
				dataGridTableStyle.MappingNameChanged += this.TableStyleMappingNameChanged;
				this.items.Add(dataGridTableStyle);
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x14000104 RID: 260
		// (add) Token: 0x06001838 RID: 6200 RVA: 0x00056E0F File Offset: 0x0005500F
		// (remove) Token: 0x06001839 RID: 6201 RVA: 0x00056E28 File Offset: 0x00055028
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChanged, value);
			}
			remove
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChanged, value);
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00056E44 File Offset: 0x00055044
		public void Clear()
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)this.items[i];
				dataGridTableStyle.MappingNameChanged -= this.TableStyleMappingNameChanged;
			}
			this.items.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00056EA4 File Offset: 0x000550A4
		public bool Contains(DataGridTableStyle table)
		{
			int num = this.items.IndexOf(table);
			return num != -1;
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00056EC8 File Offset: 0x000550C8
		public bool Contains(string name)
		{
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)this.items[i];
				if (string.Compare(dataGridTableStyle.MappingName, name, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00056F18 File Offset: 0x00055118
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
			DataGrid dataGrid = this.owner;
			if (dataGrid != null)
			{
				dataGrid.checkHierarchy = true;
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00056F4C File Offset: 0x0005514C
		public void Remove(DataGridTableStyle table)
		{
			int num = -1;
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.items[i] == table)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				throw new ArgumentException(SR.GetString("DataGridTableCollectionMissingTable"), "table");
			}
			this.RemoveAt(num);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00056FA8 File Offset: 0x000551A8
		public void RemoveAt(int index)
		{
			DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)this.items[index];
			dataGridTableStyle.MappingNameChanged -= this.TableStyleMappingNameChanged;
			this.items.RemoveAt(index);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridTableStyle));
		}

		// Token: 0x04000AD0 RID: 2768
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x04000AD1 RID: 2769
		private ArrayList items = new ArrayList();

		// Token: 0x04000AD2 RID: 2770
		private DataGrid owner;
	}
}

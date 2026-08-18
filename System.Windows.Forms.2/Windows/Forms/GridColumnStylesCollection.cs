using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000182 RID: 386
	[Editor("System.Windows.Forms.Design.DataGridColumnCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ListBindable(false)]
	public class GridColumnStylesCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x060016AA RID: 5802 RVA: 0x0005118B File Offset: 0x0004F38B
		int IList.Add(object value)
		{
			return this.Add((DataGridColumnStyle)value);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00051199 File Offset: 0x0004F399
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x000511A1 File Offset: 0x0004F3A1
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000511AF File Offset: 0x0004F3AF
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0000A547 File Offset: 0x00008747
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000511BD File Offset: 0x0004F3BD
		void IList.Remove(object value)
		{
			this.Remove((DataGridColumnStyle)value);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000511CB File Offset: 0x0004F3CB
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000522 RID: 1314
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

		// Token: 0x060016B5 RID: 5813 RVA: 0x000511E2 File Offset: 0x0004F3E2
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x000511F1 File Offset: 0x0004F3F1
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000511FE File Offset: 0x0004F3FE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0005120B File Offset: 0x0004F40B
		internal GridColumnStylesCollection(DataGridTableStyle table)
		{
			this.owner = table;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00051225 File Offset: 0x0004F425
		internal GridColumnStylesCollection(DataGridTableStyle table, bool isDefault) : this(table)
		{
			this.isDefault = isDefault;
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00051235 File Offset: 0x0004F435
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000527 RID: 1319
		public DataGridColumnStyle this[int index]
		{
			get
			{
				return (DataGridColumnStyle)this.items[index];
			}
		}

		// Token: 0x17000528 RID: 1320
		public DataGridColumnStyle this[string columnName]
		{
			get
			{
				int count = this.items.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[i];
					if (string.Equals(dataGridColumnStyle.MappingName, columnName, StringComparison.OrdinalIgnoreCase))
					{
						return dataGridColumnStyle;
					}
				}
				return null;
			}
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0005129C File Offset: 0x0004F49C
		internal DataGridColumnStyle MapColumnStyleToPropertyName(string mappingName)
		{
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[i];
				if (string.Equals(dataGridColumnStyle.MappingName, mappingName, StringComparison.OrdinalIgnoreCase))
				{
					return dataGridColumnStyle;
				}
			}
			return null;
		}

		// Token: 0x17000529 RID: 1321
		public DataGridColumnStyle this[PropertyDescriptor propertyDesciptor]
		{
			get
			{
				int count = this.items.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[i];
					if (propertyDesciptor.Equals(dataGridColumnStyle.PropertyDescriptor))
					{
						return dataGridColumnStyle;
					}
				}
				return null;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x00051330 File Offset: 0x0004F530
		internal DataGridTableStyle DataGridTableStyle
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00051338 File Offset: 0x0004F538
		internal void CheckForMappingNameDuplicates(DataGridColumnStyle column)
		{
			if (string.IsNullOrEmpty(column.MappingName))
			{
				return;
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				if (((DataGridColumnStyle)this.items[i]).MappingName.Equals(column.MappingName) && column != this.items[i])
				{
					throw new ArgumentException(SR.GetString("DataGridColumnStyleDuplicateMappingName"), "column");
				}
			}
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x000513B0 File Offset: 0x0004F5B0
		private void ColumnStyleMappingNameChanged(object sender, EventArgs pcea)
		{
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000513BF File Offset: 0x0004F5BF
		private void ColumnStylePropDescChanged(object sender, EventArgs pcea)
		{
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, (DataGridColumnStyle)sender));
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x000513D4 File Offset: 0x0004F5D4
		public virtual int Add(DataGridColumnStyle column)
		{
			if (this.isDefault)
			{
				throw new ArgumentException(SR.GetString("DataGridDefaultColumnCollectionChanged"));
			}
			this.CheckForMappingNameDuplicates(column);
			column.SetDataGridTableInColumn(this.owner, true);
			column.MappingNameChanged += this.ColumnStyleMappingNameChanged;
			column.PropertyDescriptorChanged += this.ColumnStylePropDescChanged;
			if (this.DataGridTableStyle != null && column.Width == -1)
			{
				column.width = this.DataGridTableStyle.PreferredColumnWidth;
			}
			int result = this.items.Add(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
			return result;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00051470 File Offset: 0x0004F670
		public void AddRange(DataGridColumnStyle[] columns)
		{
			if (columns == null)
			{
				throw new ArgumentNullException("columns");
			}
			for (int i = 0; i < columns.Length; i++)
			{
				this.Add(columns[i]);
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000514A3 File Offset: 0x0004F6A3
		internal void AddDefaultColumn(DataGridColumnStyle column)
		{
			column.SetDataGridTableInColumn(this.owner, true);
			this.items.Add(column);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000514C0 File Offset: 0x0004F6C0
		internal void ResetDefaultColumnCollection()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].ReleaseHostedControl();
			}
			this.items.Clear();
		}

		// Token: 0x140000EF RID: 239
		// (add) Token: 0x060016C9 RID: 5833 RVA: 0x000514F5 File Offset: 0x0004F6F5
		// (remove) Token: 0x060016CA RID: 5834 RVA: 0x0005150E File Offset: 0x0004F70E
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

		// Token: 0x060016CB RID: 5835 RVA: 0x00051528 File Offset: 0x0004F728
		public void Clear()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].ReleaseHostedControl();
			}
			this.items.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0005156A File Offset: 0x0004F76A
		public bool Contains(PropertyDescriptor propertyDescriptor)
		{
			return this[propertyDescriptor] != null;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00051578 File Offset: 0x0004F778
		public bool Contains(DataGridColumnStyle column)
		{
			int num = this.items.IndexOf(column);
			return num != -1;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0005159C File Offset: 0x0004F79C
		public bool Contains(string name)
		{
			foreach (object obj in this.items)
			{
				DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)obj;
				if (string.Compare(dataGridColumnStyle.MappingName, name, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000515E4 File Offset: 0x0004F7E4
		public int IndexOf(DataGridColumnStyle element)
		{
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[i];
				if (element == dataGridColumnStyle)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00051624 File Offset: 0x0004F824
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
			DataGrid dataGrid = this.owner.DataGrid;
			if (dataGrid != null)
			{
				dataGrid.checkHierarchy = true;
			}
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0005165C File Offset: 0x0004F85C
		public void Remove(DataGridColumnStyle column)
		{
			if (this.isDefault)
			{
				throw new ArgumentException(SR.GetString("DataGridDefaultColumnCollectionChanged"));
			}
			int num = -1;
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.items[i] == column)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				throw new InvalidOperationException(SR.GetString("DataGridColumnCollectionMissing"));
			}
			this.RemoveAt(num);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x000516CC File Offset: 0x0004F8CC
		public void RemoveAt(int index)
		{
			if (this.isDefault)
			{
				throw new ArgumentException(SR.GetString("DataGridDefaultColumnCollectionChanged"));
			}
			DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)this.items[index];
			dataGridColumnStyle.SetDataGridTableInColumn(null, true);
			dataGridColumnStyle.MappingNameChanged -= this.ColumnStyleMappingNameChanged;
			dataGridColumnStyle.PropertyDescriptorChanged -= this.ColumnStylePropDescChanged;
			this.items.RemoveAt(index);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridColumnStyle));
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00051748 File Offset: 0x0004F948
		public void ResetPropertyDescriptors()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].PropertyDescriptor = null;
			}
		}

		// Token: 0x04000A58 RID: 2648
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x04000A59 RID: 2649
		private ArrayList items = new ArrayList();

		// Token: 0x04000A5A RID: 2650
		private DataGridTableStyle owner;

		// Token: 0x04000A5B RID: 2651
		private bool isDefault;
	}
}

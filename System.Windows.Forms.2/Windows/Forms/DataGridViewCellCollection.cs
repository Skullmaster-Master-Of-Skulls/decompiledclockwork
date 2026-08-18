using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020001A6 RID: 422
	[ListBindable(false)]
	public class DataGridViewCellCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06001E0A RID: 7690 RVA: 0x0008E8DE File Offset: 0x0008CADE
		int IList.Add(object value)
		{
			return this.Add((DataGridViewCell)value);
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0008E8EC File Offset: 0x0008CAEC
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x0008E8F4 File Offset: 0x0008CAF4
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x0008E902 File Offset: 0x0008CB02
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x0008E910 File Offset: 0x0008CB10
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (DataGridViewCell)value);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x0008E91F File Offset: 0x0008CB1F
		void IList.Remove(object value)
		{
			this.Remove((DataGridViewCell)value);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x0008E92D File Offset: 0x0008CB2D
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000686 RID: 1670
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (DataGridViewCell)value;
			}
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x0008E94E File Offset: 0x0008CB4E
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x0008E95D File Offset: 0x0008CB5D
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x0008E96A File Offset: 0x0008CB6A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0008E977 File Offset: 0x0008CB77
		public DataGridViewCellCollection(DataGridViewRow dataGridViewRow)
		{
			this.owner = dataGridViewRow;
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x0008E991 File Offset: 0x0008CB91
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x1700068B RID: 1675
		public DataGridViewCell this[int index]
		{
			get
			{
				return (DataGridViewCell)this.items[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.DataGridView != null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CellAlreadyBelongsToDataGridView"));
				}
				if (value.OwningRow != null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CellAlreadyBelongsToDataGridViewRow"));
				}
				if (this.owner.DataGridView != null)
				{
					this.owner.DataGridView.OnReplacingCell(this.owner, index);
				}
				DataGridViewCell dataGridViewCell = (DataGridViewCell)this.items[index];
				this.items[index] = value;
				value.OwningRowInternal = this.owner;
				value.StateInternal = dataGridViewCell.State;
				if (this.owner.DataGridView != null)
				{
					value.DataGridViewInternal = this.owner.DataGridView;
					value.OwningColumnInternal = this.owner.DataGridView.Columns[index];
					this.owner.DataGridView.OnReplacedCell(this.owner, index);
				}
				dataGridViewCell.DataGridViewInternal = null;
				dataGridViewCell.OwningRowInternal = null;
				dataGridViewCell.OwningColumnInternal = null;
				if (dataGridViewCell.ReadOnly)
				{
					dataGridViewCell.ReadOnlyInternal = false;
				}
				if (dataGridViewCell.Selected)
				{
					dataGridViewCell.SelectedInternal = false;
				}
			}
		}

		// Token: 0x1700068C RID: 1676
		public DataGridViewCell this[string columnName]
		{
			get
			{
				DataGridViewColumn dataGridViewColumn = null;
				if (this.owner.DataGridView != null)
				{
					dataGridViewColumn = this.owner.DataGridView.Columns[columnName];
				}
				if (dataGridViewColumn == null)
				{
					throw new ArgumentException(SR.GetString("DataGridViewColumnCollection_ColumnNotFound", new object[]
					{
						columnName
					}), "columnName");
				}
				return (DataGridViewCell)this.items[dataGridViewColumn.Index];
			}
			set
			{
				DataGridViewColumn dataGridViewColumn = null;
				if (this.owner.DataGridView != null)
				{
					dataGridViewColumn = this.owner.DataGridView.Columns[columnName];
				}
				if (dataGridViewColumn == null)
				{
					throw new ArgumentException(SR.GetString("DataGridViewColumnCollection_ColumnNotFound", new object[]
					{
						columnName
					}), "columnName");
				}
				this[dataGridViewColumn.Index] = value;
			}
		}

		// Token: 0x14000183 RID: 387
		// (add) Token: 0x06001E20 RID: 7712 RVA: 0x0008EBA6 File Offset: 0x0008CDA6
		// (remove) Token: 0x06001E21 RID: 7713 RVA: 0x0008EBBF File Offset: 0x0008CDBF
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

		// Token: 0x06001E22 RID: 7714 RVA: 0x0008EBD8 File Offset: 0x0008CDD8
		public virtual int Add(DataGridViewCell dataGridViewCell)
		{
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			if (dataGridViewCell.OwningRow != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CellAlreadyBelongsToDataGridViewRow"));
			}
			return this.AddInternal(dataGridViewCell);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0008EC18 File Offset: 0x0008CE18
		internal int AddInternal(DataGridViewCell dataGridViewCell)
		{
			int num = this.items.Add(dataGridViewCell);
			dataGridViewCell.OwningRowInternal = this.owner;
			DataGridView dataGridView = this.owner.DataGridView;
			if (dataGridView != null && dataGridView.Columns.Count > num)
			{
				dataGridViewCell.OwningColumnInternal = dataGridView.Columns[num];
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewCell));
			return num;
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0008EC7C File Offset: 0x0008CE7C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual void AddRange(params DataGridViewCell[] dataGridViewCells)
		{
			if (dataGridViewCells == null)
			{
				throw new ArgumentNullException("dataGridViewCells");
			}
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			foreach (DataGridViewCell dataGridViewCell in dataGridViewCells)
			{
				if (dataGridViewCell == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_AtLeastOneCellIsNull"));
				}
				if (dataGridViewCell.OwningRow != null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CellAlreadyBelongsToDataGridViewRow"));
				}
			}
			int num = dataGridViewCells.Length;
			for (int j = 0; j < num - 1; j++)
			{
				for (int k = j + 1; k < num; k++)
				{
					if (dataGridViewCells[j] == dataGridViewCells[k])
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CannotAddIdenticalCells"));
					}
				}
			}
			this.items.AddRange(dataGridViewCells);
			foreach (DataGridViewCell dataGridViewCell2 in dataGridViewCells)
			{
				dataGridViewCell2.OwningRowInternal = this.owner;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0008ED7C File Offset: 0x0008CF7C
		public virtual void Clear()
		{
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			foreach (object obj in this.items)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
				dataGridViewCell.OwningRowInternal = null;
			}
			this.items.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x0008E94E File Offset: 0x0008CB4E
		public void CopyTo(DataGridViewCell[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0008EE0C File Offset: 0x0008D00C
		public virtual bool Contains(DataGridViewCell dataGridViewCell)
		{
			int num = this.items.IndexOf(dataGridViewCell);
			return num != -1;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0008E902 File Offset: 0x0008CB02
		public int IndexOf(DataGridViewCell dataGridViewCell)
		{
			return this.items.IndexOf(dataGridViewCell);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0008EE30 File Offset: 0x0008D030
		public virtual void Insert(int index, DataGridViewCell dataGridViewCell)
		{
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			if (dataGridViewCell.OwningRow != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_CellAlreadyBelongsToDataGridViewRow"));
			}
			this.items.Insert(index, dataGridViewCell);
			dataGridViewCell.OwningRowInternal = this.owner;
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewCell));
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0008EE98 File Offset: 0x0008D098
		internal void InsertInternal(int index, DataGridViewCell dataGridViewCell)
		{
			this.items.Insert(index, dataGridViewCell);
			dataGridViewCell.OwningRowInternal = this.owner;
			DataGridView dataGridView = this.owner.DataGridView;
			if (dataGridView != null && dataGridView.Columns.Count > index)
			{
				dataGridViewCell.OwningColumnInternal = dataGridView.Columns[index];
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewCell));
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0008EEFA File Offset: 0x0008D0FA
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0008EF14 File Offset: 0x0008D114
		public virtual void Remove(DataGridViewCell cell)
		{
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			int num = -1;
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.items[i] == cell)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				throw new ArgumentException(SR.GetString("DataGridViewCellCollection_CellNotFound"));
			}
			this.RemoveAt(num);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0008EF86 File Offset: 0x0008D186
		public virtual void RemoveAt(int index)
		{
			if (this.owner.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewCellCollection_OwningRowAlreadyBelongsToDataGridView"));
			}
			this.RemoveAtInternal(index);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0008EFAC File Offset: 0x0008D1AC
		internal void RemoveAtInternal(int index)
		{
			DataGridViewCell dataGridViewCell = (DataGridViewCell)this.items[index];
			this.items.RemoveAt(index);
			dataGridViewCell.DataGridViewInternal = null;
			dataGridViewCell.OwningRowInternal = null;
			if (dataGridViewCell.ReadOnly)
			{
				dataGridViewCell.ReadOnlyInternal = false;
			}
			if (dataGridViewCell.Selected)
			{
				dataGridViewCell.SelectedInternal = false;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridViewCell));
		}

		// Token: 0x04000CC1 RID: 3265
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x04000CC2 RID: 3266
		private ArrayList items = new ArrayList();

		// Token: 0x04000CC3 RID: 3267
		private DataGridViewRow owner;
	}
}

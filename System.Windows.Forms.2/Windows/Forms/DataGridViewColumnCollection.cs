using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x020001C0 RID: 448
	[ListBindable(false)]
	public class DataGridViewColumnCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700070C RID: 1804
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00094657 File Offset: 0x00092857
		int IList.Add(object value)
		{
			return this.Add((DataGridViewColumn)value);
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00094665 File Offset: 0x00092865
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0009466D File Offset: 0x0009286D
		bool IList.Contains(object value)
		{
			return this.items.Contains(value);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0009467B File Offset: 0x0009287B
		int IList.IndexOf(object value)
		{
			return this.items.IndexOf(value);
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x00094689 File Offset: 0x00092889
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (DataGridViewColumn)value);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x00094698 File Offset: 0x00092898
		void IList.Remove(object value)
		{
			this.Remove((DataGridViewColumn)value);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x000946A6 File Offset: 0x000928A6
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x000946AF File Offset: 0x000928AF
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000946BC File Offset: 0x000928BC
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x000946CB File Offset: 0x000928CB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000946D8 File Offset: 0x000928D8
		public DataGridViewColumnCollection(DataGridView dataGridView)
		{
			this.InvalidateCachedColumnCounts();
			this.InvalidateCachedColumnsWidths();
			this.dataGridView = dataGridView;
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x00094705 File Offset: 0x00092905
		internal static IComparer ColumnCollectionOrderComparer
		{
			get
			{
				return DataGridViewColumnCollection.columnOrderComparer;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x0009470C File Offset: 0x0009290C
		protected override ArrayList List
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x00094714 File Offset: 0x00092914
		protected DataGridView DataGridView
		{
			get
			{
				return this.dataGridView;
			}
		}

		// Token: 0x17000713 RID: 1811
		public DataGridViewColumn this[int index]
		{
			get
			{
				return (DataGridViewColumn)this.items[index];
			}
		}

		// Token: 0x17000714 RID: 1812
		public DataGridViewColumn this[string columnName]
		{
			get
			{
				if (columnName == null)
				{
					throw new ArgumentNullException("columnName");
				}
				int count = this.items.Count;
				for (int i = 0; i < count; i++)
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[i];
					if (string.Equals(dataGridViewColumn.Name, columnName, StringComparison.OrdinalIgnoreCase))
					{
						return dataGridViewColumn;
					}
				}
				return null;
			}
		}

		// Token: 0x14000185 RID: 389
		// (add) Token: 0x06001F7A RID: 8058 RVA: 0x00094787 File Offset: 0x00092987
		// (remove) Token: 0x06001F7B RID: 8059 RVA: 0x000947A0 File Offset: 0x000929A0
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

		// Token: 0x06001F7C RID: 8060 RVA: 0x000947BC File Offset: 0x000929BC
		internal int GetVisibleIndex(DataGridViewColumn column)
		{
			for (int i = 0; i < this.Count; i++)
			{
				int num = this.ActualDisplayIndexToColumnIndex(i, DataGridViewElementStates.Visible);
				if (num != -1 && this.items[num] == column)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x000947FC File Offset: 0x000929FC
		internal int ActualDisplayIndexToColumnIndex(int actualDisplayIndex, DataGridViewElementStates includeFilter)
		{
			DataGridViewColumn dataGridViewColumn = this.GetFirstColumn(includeFilter);
			for (int i = 0; i < actualDisplayIndex; i++)
			{
				dataGridViewColumn = this.GetNextColumn(dataGridViewColumn, includeFilter, DataGridViewElementStates.None);
			}
			if (!AccessibilityImprovements.Level5 || dataGridViewColumn != null)
			{
				return dataGridViewColumn.Index;
			}
			return -1;
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x0009483C File Offset: 0x00092A3C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int Add(string columnName, string headerText)
		{
			return this.Add(new DataGridViewTextBoxColumn
			{
				Name = columnName,
				HeaderText = headerText
			});
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x00094864 File Offset: 0x00092A64
		public virtual int Add(DataGridViewColumn dataGridViewColumn)
		{
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.InDisplayIndexAdjustments)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_CannotAlterDisplayIndexWithinAdjustments"));
			}
			this.DataGridView.OnAddingColumn(dataGridViewColumn);
			this.InvalidateCachedColumnsOrder();
			int num = this.items.Add(dataGridViewColumn);
			dataGridViewColumn.IndexInternal = num;
			dataGridViewColumn.DataGridViewInternal = this.dataGridView;
			this.UpdateColumnCaches(dataGridViewColumn, true);
			this.DataGridView.OnAddedColumn(dataGridViewColumn);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewColumn), false, new Point(-1, -1));
			return num;
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x00094908 File Offset: 0x00092B08
		public virtual void AddRange(params DataGridViewColumn[] dataGridViewColumns)
		{
			if (dataGridViewColumns == null)
			{
				throw new ArgumentNullException("dataGridViewColumns");
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.InDisplayIndexAdjustments)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_CannotAlterDisplayIndexWithinAdjustments"));
			}
			ArrayList arrayList = new ArrayList(dataGridViewColumns.Length);
			ArrayList arrayList2 = new ArrayList(dataGridViewColumns.Length);
			foreach (DataGridViewColumn dataGridViewColumn in dataGridViewColumns)
			{
				if (dataGridViewColumn.DisplayIndex != -1)
				{
					arrayList.Add(dataGridViewColumn);
				}
			}
			int j;
			while (arrayList.Count > 0)
			{
				int num = int.MaxValue;
				int index = -1;
				for (j = 0; j < arrayList.Count; j++)
				{
					DataGridViewColumn dataGridViewColumn2 = (DataGridViewColumn)arrayList[j];
					if (dataGridViewColumn2.DisplayIndex < num)
					{
						num = dataGridViewColumn2.DisplayIndex;
						index = j;
					}
				}
				arrayList2.Add(arrayList[index]);
				arrayList.RemoveAt(index);
			}
			foreach (DataGridViewColumn dataGridViewColumn3 in dataGridViewColumns)
			{
				if (dataGridViewColumn3.DisplayIndex == -1)
				{
					arrayList2.Add(dataGridViewColumn3);
				}
			}
			j = 0;
			foreach (object obj in arrayList2)
			{
				DataGridViewColumn dataGridViewColumn4 = (DataGridViewColumn)obj;
				dataGridViewColumns[j] = dataGridViewColumn4;
				j++;
			}
			this.DataGridView.OnAddingColumns(dataGridViewColumns);
			foreach (DataGridViewColumn dataGridViewColumn5 in dataGridViewColumns)
			{
				this.InvalidateCachedColumnsOrder();
				j = this.items.Add(dataGridViewColumn5);
				dataGridViewColumn5.IndexInternal = j;
				dataGridViewColumn5.DataGridViewInternal = this.dataGridView;
				this.UpdateColumnCaches(dataGridViewColumn5, true);
				this.DataGridView.OnAddedColumn(dataGridViewColumn5);
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), false, new Point(-1, -1));
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x00094B08 File Offset: 0x00092D08
		public virtual void Clear()
		{
			if (this.Count > 0)
			{
				if (this.DataGridView.NoDimensionChangeAllowed)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
				}
				if (this.DataGridView.InDisplayIndexAdjustments)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_CannotAlterDisplayIndexWithinAdjustments"));
				}
				for (int i = 0; i < this.Count; i++)
				{
					DataGridViewColumn dataGridViewColumn = this[i];
					dataGridViewColumn.DataGridViewInternal = null;
					if (dataGridViewColumn.HasHeaderCell)
					{
						dataGridViewColumn.HeaderCell.DataGridViewInternal = null;
					}
				}
				DataGridViewColumn[] array = new DataGridViewColumn[this.items.Count];
				this.CopyTo(array, 0);
				this.DataGridView.OnClearingColumns();
				this.InvalidateCachedColumnsOrder();
				this.items.Clear();
				this.InvalidateCachedColumnCounts();
				this.InvalidateCachedColumnsWidths();
				foreach (DataGridViewColumn dataGridViewColumn2 in array)
				{
					this.DataGridView.OnColumnRemoved(dataGridViewColumn2);
					this.DataGridView.OnColumnHidden(dataGridViewColumn2);
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null), false, new Point(-1, -1));
			}
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00094C1C File Offset: 0x00092E1C
		internal int ColumnIndexToActualDisplayIndex(int columnIndex, DataGridViewElementStates includeFilter)
		{
			DataGridViewColumn dataGridViewColumn = this.GetFirstColumn(includeFilter);
			int num = 0;
			while (dataGridViewColumn != null && dataGridViewColumn.Index != columnIndex)
			{
				dataGridViewColumn = this.GetNextColumn(dataGridViewColumn, includeFilter, DataGridViewElementStates.None);
				num++;
			}
			return num;
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00094C50 File Offset: 0x00092E50
		public virtual bool Contains(DataGridViewColumn dataGridViewColumn)
		{
			return this.items.IndexOf(dataGridViewColumn) != -1;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00094C64 File Offset: 0x00092E64
		public virtual bool Contains(string columnName)
		{
			if (columnName == null)
			{
				throw new ArgumentNullException("columnName");
			}
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[i];
				if (string.Compare(dataGridViewColumn.Name, columnName, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000946BC File Offset: 0x000928BC
		public void CopyTo(DataGridViewColumn[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x00094CC0 File Offset: 0x00092EC0
		internal bool DisplayInOrder(int columnIndex1, int columnIndex2)
		{
			int displayIndex = ((DataGridViewColumn)this.items[columnIndex1]).DisplayIndex;
			int displayIndex2 = ((DataGridViewColumn)this.items[columnIndex2]).DisplayIndex;
			return displayIndex < displayIndex2;
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00094D00 File Offset: 0x00092F00
		internal DataGridViewColumn GetColumnAtDisplayIndex(int displayIndex)
		{
			if (displayIndex < 0 || displayIndex >= this.items.Count)
			{
				return null;
			}
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[displayIndex];
			if (dataGridViewColumn.DisplayIndex == displayIndex)
			{
				return dataGridViewColumn;
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				dataGridViewColumn = (DataGridViewColumn)this.items[i];
				if (dataGridViewColumn.DisplayIndex == displayIndex)
				{
					return dataGridViewColumn;
				}
			}
			return null;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00094D74 File Offset: 0x00092F74
		public int GetColumnCount(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
				{
					if (this.columnCountsVisibleSelected != -1)
					{
						return this.columnCountsVisibleSelected;
					}
				}
			}
			else if (this.columnCountsVisible != -1)
			{
				return this.columnCountsVisible;
			}
			int num = 0;
			if ((includeFilter & DataGridViewElementStates.Resizable) == DataGridViewElementStates.None)
			{
				for (int i = 0; i < this.items.Count; i++)
				{
					if (((DataGridViewColumn)this.items[i]).StateIncludes(includeFilter))
					{
						num++;
					}
				}
				if (includeFilter != DataGridViewElementStates.Visible)
				{
					if (includeFilter == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible))
					{
						this.columnCountsVisibleSelected = num;
					}
				}
				else
				{
					this.columnCountsVisible = num;
				}
			}
			else
			{
				DataGridViewElementStates elementState = includeFilter & ~DataGridViewElementStates.Resizable;
				for (int j = 0; j < this.items.Count; j++)
				{
					if (((DataGridViewColumn)this.items[j]).StateIncludes(elementState) && ((DataGridViewColumn)this.items[j]).Resizable == DataGridViewTriState.True)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00094E7C File Offset: 0x0009307C
		internal int GetColumnCount(DataGridViewElementStates includeFilter, int fromColumnIndex, int toColumnIndex)
		{
			int num = 0;
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[fromColumnIndex];
			while (dataGridViewColumn != (DataGridViewColumn)this.items[toColumnIndex])
			{
				dataGridViewColumn = this.GetNextColumn(dataGridViewColumn, includeFilter, DataGridViewElementStates.None);
				if (dataGridViewColumn.StateIncludes(includeFilter))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x00094ECC File Offset: 0x000930CC
		private int GetColumnSortedIndex(DataGridViewColumn dataGridViewColumn)
		{
			if (this.lastAccessedSortedIndex != -1 && this.itemsSorted[this.lastAccessedSortedIndex] == dataGridViewColumn)
			{
				return this.lastAccessedSortedIndex;
			}
			for (int i = 0; i < this.itemsSorted.Count; i++)
			{
				if (dataGridViewColumn.Index == ((DataGridViewColumn)this.itemsSorted[i]).Index)
				{
					this.lastAccessedSortedIndex = i;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x00094F3C File Offset: 0x0009313C
		internal float GetColumnsFillWeight(DataGridViewElementStates includeFilter)
		{
			float num = 0f;
			for (int i = 0; i < this.items.Count; i++)
			{
				if (((DataGridViewColumn)this.items[i]).StateIncludes(includeFilter))
				{
					num += ((DataGridViewColumn)this.items[i]).FillWeight;
				}
			}
			return num;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x00094F98 File Offset: 0x00093198
		public int GetColumnsWidth(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					if (this.columnsWidthVisibleFrozen != -1)
					{
						return this.columnsWidthVisibleFrozen;
					}
				}
			}
			else if (this.columnsWidthVisible != -1)
			{
				return this.columnsWidthVisible;
			}
			int num = 0;
			for (int i = 0; i < this.items.Count; i++)
			{
				if (((DataGridViewColumn)this.items[i]).StateIncludes(includeFilter))
				{
					num += ((DataGridViewColumn)this.items[i]).Thickness;
				}
			}
			if (includeFilter != DataGridViewElementStates.Visible)
			{
				if (includeFilter == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible))
				{
					this.columnsWidthVisibleFrozen = num;
				}
			}
			else
			{
				this.columnsWidthVisible = num;
			}
			return num;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0009505C File Offset: 0x0009325C
		public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if (this.itemsSorted == null)
			{
				this.UpdateColumnOrderCache();
			}
			for (int i = 0; i < this.itemsSorted.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.itemsSorted[i];
				if (dataGridViewColumn.StateIncludes(includeFilter))
				{
					this.lastAccessedSortedIndex = i;
					return dataGridViewColumn;
				}
			}
			return null;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000950D8 File Offset: 0x000932D8
		public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (excludeFilter == DataGridViewElementStates.None)
			{
				return this.GetFirstColumn(includeFilter);
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"excludeFilter"
				}));
			}
			if (this.itemsSorted == null)
			{
				this.UpdateColumnOrderCache();
			}
			for (int i = 0; i < this.itemsSorted.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.itemsSorted[i];
				if (dataGridViewColumn.StateIncludes(includeFilter) && dataGridViewColumn.StateExcludes(excludeFilter))
				{
					this.lastAccessedSortedIndex = i;
					return dataGridViewColumn;
				}
			}
			return null;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x0009518C File Offset: 0x0009338C
		public DataGridViewColumn GetLastColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"excludeFilter"
				}));
			}
			if (this.itemsSorted == null)
			{
				this.UpdateColumnOrderCache();
			}
			for (int i = this.itemsSorted.Count - 1; i >= 0; i--)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.itemsSorted[i];
				if (dataGridViewColumn.StateIncludes(includeFilter) && dataGridViewColumn.StateExcludes(excludeFilter))
				{
					this.lastAccessedSortedIndex = i;
					return dataGridViewColumn;
				}
			}
			return null;
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00095238 File Offset: 0x00093438
		public DataGridViewColumn GetNextColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (dataGridViewColumnStart == null)
			{
				throw new ArgumentNullException("dataGridViewColumnStart");
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"excludeFilter"
				}));
			}
			if (this.itemsSorted == null)
			{
				this.UpdateColumnOrderCache();
			}
			int i = this.GetColumnSortedIndex(dataGridViewColumnStart);
			if (i != -1)
			{
				for (i++; i < this.itemsSorted.Count; i++)
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.itemsSorted[i];
					if (dataGridViewColumn.StateIncludes(includeFilter) && dataGridViewColumn.StateExcludes(excludeFilter))
					{
						this.lastAccessedSortedIndex = i;
						return dataGridViewColumn;
					}
				}
				return null;
			}
			bool flag = false;
			int num = int.MaxValue;
			int num2 = int.MaxValue;
			for (i = 0; i < this.items.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn2 = (DataGridViewColumn)this.items[i];
				if (dataGridViewColumn2.StateIncludes(includeFilter) && dataGridViewColumn2.StateExcludes(excludeFilter) && (dataGridViewColumn2.DisplayIndex > dataGridViewColumnStart.DisplayIndex || (dataGridViewColumn2.DisplayIndex == dataGridViewColumnStart.DisplayIndex && dataGridViewColumn2.Index > dataGridViewColumnStart.Index)) && (dataGridViewColumn2.DisplayIndex < num2 || (dataGridViewColumn2.DisplayIndex == num2 && dataGridViewColumn2.Index < num)))
				{
					num = i;
					num2 = dataGridViewColumn2.DisplayIndex;
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return (DataGridViewColumn)this.items[num];
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000953C4 File Offset: 0x000935C4
		public DataGridViewColumn GetPreviousColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			if (dataGridViewColumnStart == null)
			{
				throw new ArgumentNullException("dataGridViewColumnStart");
			}
			if ((includeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"includeFilter"
				}));
			}
			if ((excludeFilter & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) != DataGridViewElementStates.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewElementStateCombination", new object[]
				{
					"excludeFilter"
				}));
			}
			if (this.itemsSorted == null)
			{
				this.UpdateColumnOrderCache();
			}
			int i = this.GetColumnSortedIndex(dataGridViewColumnStart);
			if (i != -1)
			{
				for (i--; i >= 0; i--)
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.itemsSorted[i];
					if (dataGridViewColumn.StateIncludes(includeFilter) && dataGridViewColumn.StateExcludes(excludeFilter))
					{
						this.lastAccessedSortedIndex = i;
						return dataGridViewColumn;
					}
				}
				return null;
			}
			bool flag = false;
			int num = -1;
			int num2 = -1;
			for (i = 0; i < this.items.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn2 = (DataGridViewColumn)this.items[i];
				if (dataGridViewColumn2.StateIncludes(includeFilter) && dataGridViewColumn2.StateExcludes(excludeFilter) && (dataGridViewColumn2.DisplayIndex < dataGridViewColumnStart.DisplayIndex || (dataGridViewColumn2.DisplayIndex == dataGridViewColumnStart.DisplayIndex && dataGridViewColumn2.Index < dataGridViewColumnStart.Index)) && (dataGridViewColumn2.DisplayIndex > num2 || (dataGridViewColumn2.DisplayIndex == num2 && dataGridViewColumn2.Index > num)))
				{
					num = i;
					num2 = dataGridViewColumn2.DisplayIndex;
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return (DataGridViewColumn)this.items[num];
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0009467B File Offset: 0x0009287B
		public int IndexOf(DataGridViewColumn dataGridViewColumn)
		{
			return this.items.IndexOf(dataGridViewColumn);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0009553C File Offset: 0x0009373C
		public virtual void Insert(int columnIndex, DataGridViewColumn dataGridViewColumn)
		{
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.InDisplayIndexAdjustments)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_CannotAlterDisplayIndexWithinAdjustments"));
			}
			if (dataGridViewColumn == null)
			{
				throw new ArgumentNullException("dataGridViewColumn");
			}
			int displayIndex = dataGridViewColumn.DisplayIndex;
			if (displayIndex == -1)
			{
				dataGridViewColumn.DisplayIndex = columnIndex;
			}
			Point newCurrentCell;
			try
			{
				this.DataGridView.OnInsertingColumn(columnIndex, dataGridViewColumn, out newCurrentCell);
			}
			finally
			{
				dataGridViewColumn.DisplayIndexInternal = displayIndex;
			}
			this.InvalidateCachedColumnsOrder();
			this.items.Insert(columnIndex, dataGridViewColumn);
			dataGridViewColumn.IndexInternal = columnIndex;
			dataGridViewColumn.DataGridViewInternal = this.dataGridView;
			this.UpdateColumnCaches(dataGridViewColumn, true);
			this.DataGridView.OnInsertedColumn_PreNotification(dataGridViewColumn);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewColumn), true, newCurrentCell);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00095618 File Offset: 0x00093818
		internal void InvalidateCachedColumnCount(DataGridViewElementStates includeFilter)
		{
			if (includeFilter == DataGridViewElementStates.Visible)
			{
				this.InvalidateCachedColumnCounts();
				return;
			}
			if (includeFilter == DataGridViewElementStates.Selected)
			{
				this.columnCountsVisibleSelected = -1;
			}
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x00095634 File Offset: 0x00093834
		internal void InvalidateCachedColumnCounts()
		{
			this.columnCountsVisible = (this.columnCountsVisibleSelected = -1);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x00095651 File Offset: 0x00093851
		internal void InvalidateCachedColumnsOrder()
		{
			this.itemsSorted = null;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0009565A File Offset: 0x0009385A
		internal void InvalidateCachedColumnsWidth(DataGridViewElementStates includeFilter)
		{
			if (includeFilter == DataGridViewElementStates.Visible)
			{
				this.InvalidateCachedColumnsWidths();
				return;
			}
			if (includeFilter == DataGridViewElementStates.Frozen)
			{
				this.columnsWidthVisibleFrozen = -1;
			}
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x00095674 File Offset: 0x00093874
		internal void InvalidateCachedColumnsWidths()
		{
			this.columnsWidthVisible = (this.columnsWidthVisibleFrozen = -1);
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x00095691 File Offset: 0x00093891
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000956A8 File Offset: 0x000938A8
		private void OnCollectionChanged(CollectionChangeEventArgs ccea, bool changeIsInsertion, Point newCurrentCell)
		{
			this.OnCollectionChanged_PreNotification(ccea);
			this.OnCollectionChanged(ccea);
			this.OnCollectionChanged_PostNotification(ccea, changeIsInsertion, newCurrentCell);
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000956C1 File Offset: 0x000938C1
		private void OnCollectionChanged_PreNotification(CollectionChangeEventArgs ccea)
		{
			this.DataGridView.OnColumnCollectionChanged_PreNotification(ccea);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000956D0 File Offset: 0x000938D0
		private void OnCollectionChanged_PostNotification(CollectionChangeEventArgs ccea, bool changeIsInsertion, Point newCurrentCell)
		{
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)ccea.Element;
			if (ccea.Action == CollectionChangeAction.Add && changeIsInsertion)
			{
				this.DataGridView.OnInsertedColumn_PostNotification(newCurrentCell);
			}
			else if (ccea.Action == CollectionChangeAction.Remove)
			{
				this.DataGridView.OnRemovedColumn_PostNotification(dataGridViewColumn, newCurrentCell);
			}
			this.DataGridView.OnColumnCollectionChanged_PostNotification(dataGridViewColumn);
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00095728 File Offset: 0x00093928
		public virtual void Remove(DataGridViewColumn dataGridViewColumn)
		{
			if (dataGridViewColumn == null)
			{
				throw new ArgumentNullException("dataGridViewColumn");
			}
			if (dataGridViewColumn.DataGridView != this.DataGridView)
			{
				throw new ArgumentException(SR.GetString("DataGridView_ColumnDoesNotBelongToDataGridView"), "dataGridViewColumn");
			}
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.items[i] == dataGridViewColumn)
				{
					this.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00095798 File Offset: 0x00093998
		public virtual void Remove(string columnName)
		{
			if (columnName == null)
			{
				throw new ArgumentNullException("columnName");
			}
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[i];
				if (string.Compare(dataGridViewColumn.Name, columnName, true, CultureInfo.InvariantCulture) == 0)
				{
					this.RemoveAt(i);
					return;
				}
			}
			throw new ArgumentException(SR.GetString("DataGridViewColumnCollection_ColumnNotFound", new object[]
			{
				columnName
			}), "columnName");
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x00095818 File Offset: 0x00093A18
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.DataGridView.NoDimensionChangeAllowed)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_ForbiddenOperationInEventHandler"));
			}
			if (this.DataGridView.InDisplayIndexAdjustments)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_CannotAlterDisplayIndexWithinAdjustments"));
			}
			this.RemoveAtInternal(index, false);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000958A8 File Offset: 0x00093AA8
		internal void RemoveAtInternal(int index, bool force)
		{
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.items[index];
			Point newCurrentCell;
			this.DataGridView.OnRemovingColumn(dataGridViewColumn, out newCurrentCell, force);
			this.InvalidateCachedColumnsOrder();
			this.items.RemoveAt(index);
			dataGridViewColumn.DataGridViewInternal = null;
			this.UpdateColumnCaches(dataGridViewColumn, false);
			this.DataGridView.OnRemovedColumn_PreNotification(dataGridViewColumn);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridViewColumn), false, newCurrentCell);
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00095914 File Offset: 0x00093B14
		private void UpdateColumnCaches(DataGridViewColumn dataGridViewColumn, bool adding)
		{
			if (this.columnCountsVisible != -1 || this.columnCountsVisibleSelected != -1 || this.columnsWidthVisible != -1 || this.columnsWidthVisibleFrozen != -1)
			{
				DataGridViewElementStates state = dataGridViewColumn.State;
				if ((state & DataGridViewElementStates.Visible) != DataGridViewElementStates.None)
				{
					int num = adding ? 1 : -1;
					int num2 = 0;
					if (this.columnsWidthVisible != -1 || (this.columnsWidthVisibleFrozen != -1 && (state & (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)))
					{
						num2 = (adding ? dataGridViewColumn.Width : (-dataGridViewColumn.Width));
					}
					if (this.columnCountsVisible != -1)
					{
						this.columnCountsVisible += num;
					}
					if (this.columnsWidthVisible != -1)
					{
						this.columnsWidthVisible += num2;
					}
					if ((state & (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible) && this.columnsWidthVisibleFrozen != -1)
					{
						this.columnsWidthVisibleFrozen += num2;
					}
					if ((state & (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) == (DataGridViewElementStates.Selected | DataGridViewElementStates.Visible) && this.columnCountsVisibleSelected != -1)
					{
						this.columnCountsVisibleSelected += num;
					}
				}
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000959FB File Offset: 0x00093BFB
		private void UpdateColumnOrderCache()
		{
			this.itemsSorted = (ArrayList)this.items.Clone();
			this.itemsSorted.Sort(DataGridViewColumnCollection.columnOrderComparer);
			this.lastAccessedSortedIndex = -1;
		}

		// Token: 0x04000D45 RID: 3397
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x04000D46 RID: 3398
		private ArrayList items = new ArrayList();

		// Token: 0x04000D47 RID: 3399
		private ArrayList itemsSorted;

		// Token: 0x04000D48 RID: 3400
		private int lastAccessedSortedIndex = -1;

		// Token: 0x04000D49 RID: 3401
		private int columnCountsVisible;

		// Token: 0x04000D4A RID: 3402
		private int columnCountsVisibleSelected;

		// Token: 0x04000D4B RID: 3403
		private int columnsWidthVisible;

		// Token: 0x04000D4C RID: 3404
		private int columnsWidthVisibleFrozen;

		// Token: 0x04000D4D RID: 3405
		private static DataGridViewColumnCollection.ColumnOrderComparer columnOrderComparer = new DataGridViewColumnCollection.ColumnOrderComparer();

		// Token: 0x04000D4E RID: 3406
		private DataGridView dataGridView;

		// Token: 0x0200066B RID: 1643
		private class ColumnOrderComparer : IComparer
		{
			// Token: 0x0600663D RID: 26173 RVA: 0x0017E05C File Offset: 0x0017C25C
			public int Compare(object x, object y)
			{
				DataGridViewColumn dataGridViewColumn = x as DataGridViewColumn;
				DataGridViewColumn dataGridViewColumn2 = y as DataGridViewColumn;
				return dataGridViewColumn.DisplayIndex - dataGridViewColumn2.DisplayIndex;
			}
		}
	}
}

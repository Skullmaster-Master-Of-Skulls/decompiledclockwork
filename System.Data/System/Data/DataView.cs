using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Threading;

namespace System.Data
{
	// Token: 0x020000A6 RID: 166
	[DefaultProperty("Table")]
	[Editor("Microsoft.VSDesigner.Data.Design.DataSourceEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("Microsoft.VSDesigner.Data.VS.DataViewDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("PositionChanged")]
	public class DataView : MarshalByValueComponent, IBindingListView, IBindingList, IList, ICollection, IEnumerable, ITypedList, ISupportInitializeNotification, ISupportInitialize
	{
		// Token: 0x06000B0C RID: 2828 RVA: 0x0020B8A8 File Offset: 0x0020ACA8
		internal DataView(DataTable table, bool locked)
		{
			GC.SuppressFinalize(this);
			Bid.Trace("<ds.DataView.DataView|INFO> %d#, table=%d, locked=%d{bool}\n", this.ObjectID, (table != null) ? table.ObjectID : 0, locked);
			this.dvListener = new DataViewListener(this);
			this.locked = locked;
			this.table = table;
			this.dvListener.RegisterMetaDataEvents(this.table);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0020B978 File Offset: 0x0020AD78
		public DataView() : this(null)
		{
			this.SetIndex2("", DataViewRowState.CurrentRows, null, true);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0020B9A8 File Offset: 0x0020ADA8
		public DataView(DataTable table) : this(table, false)
		{
			this.SetIndex2("", DataViewRowState.CurrentRows, null, true);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0020B9D8 File Offset: 0x0020ADD8
		public DataView(DataTable table, string RowFilter, string Sort, DataViewRowState RowState)
		{
			GC.SuppressFinalize(this);
			Bid.Trace("<ds.DataView.DataView|API> %d#, table=%d, RowFilter='%ls', Sort='%ls', RowState=%d{ds.DataViewRowState}\n", this.ObjectID, (table != null) ? table.ObjectID : 0, RowFilter, Sort, (int)RowState);
			if (table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			this.dvListener = new DataViewListener(this);
			this.locked = false;
			this.table = table;
			this.dvListener.RegisterMetaDataEvents(this.table);
			if ((RowState & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
			{
				throw ExceptionBuilder.RecordStateRange();
			}
			if ((RowState & DataViewRowState.ModifiedOriginal) != DataViewRowState.None && (RowState & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
			{
				throw ExceptionBuilder.SetRowStateFilter();
			}
			if (Sort == null)
			{
				Sort = "";
			}
			if (RowFilter == null)
			{
				RowFilter = "";
			}
			DataExpression newRowFilter = new DataExpression(table, RowFilter);
			this.SetIndex(Sort, RowState, newRowFilter);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0020BAF8 File Offset: 0x0020AEF8
		internal DataView(DataTable table, Predicate<DataRow> predicate, Comparison<DataRow> comparison, DataViewRowState RowState)
		{
			GC.SuppressFinalize(this);
			Bid.Trace("<ds.DataView.DataView|API> %d#, table=%d, RowState=%d{ds.DataViewRowState}\n", this.ObjectID, (table != null) ? table.ObjectID : 0, (int)RowState);
			if (table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			this.dvListener = new DataViewListener(this);
			this.locked = false;
			this.table = table;
			this.dvListener.RegisterMetaDataEvents(this.table);
			if ((RowState & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
			{
				throw ExceptionBuilder.RecordStateRange();
			}
			if ((RowState & DataViewRowState.ModifiedOriginal) != DataViewRowState.None && (RowState & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
			{
				throw ExceptionBuilder.SetRowStateFilter();
			}
			this._comparison = comparison;
			this.SetIndex2("", RowState, (predicate != null) ? new DataView.RowPredicateFilter(predicate) : null, true);
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0020BC18 File Offset: 0x0020B018
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x0020BC38 File Offset: 0x0020B038
		[ResDescription("DataViewAllowDeleteDescr")]
		[DefaultValue(true)]
		[ResCategory("DataCategory_Data")]
		public bool AllowDelete
		{
			get
			{
				return this.allowDelete;
			}
			set
			{
				if (this.allowDelete != value)
				{
					this.allowDelete = value;
					this.OnListChanged(DataView.ResetEventArgs);
				}
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0020BC68 File Offset: 0x0020B068
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x0020BC88 File Offset: 0x0020B088
		[DefaultValue(false)]
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DataViewApplyDefaultSortDescr")]
		public bool ApplyDefaultSort
		{
			get
			{
				return this.applyDefaultSort;
			}
			set
			{
				Bid.Trace("<ds.DataView.set_ApplyDefaultSort|API> %d#, %d{bool}\n", this.ObjectID, value);
				if (this.applyDefaultSort != value)
				{
					this._comparison = null;
					this.applyDefaultSort = value;
					this.UpdateIndex(true);
					this.OnListChanged(DataView.ResetEventArgs);
				}
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0020BCD8 File Offset: 0x0020B0D8
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x0020BCF8 File Offset: 0x0020B0F8
		[DefaultValue(true)]
		[ResDescription("DataViewAllowEditDescr")]
		[ResCategory("DataCategory_Data")]
		public bool AllowEdit
		{
			get
			{
				return this.allowEdit;
			}
			set
			{
				if (this.allowEdit != value)
				{
					this.allowEdit = value;
					this.OnListChanged(DataView.ResetEventArgs);
				}
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0020BD28 File Offset: 0x0020B128
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x0020BD48 File Offset: 0x0020B148
		[DefaultValue(true)]
		[ResDescription("DataViewAllowNewDescr")]
		[ResCategory("DataCategory_Data")]
		public bool AllowNew
		{
			get
			{
				return this.allowNew;
			}
			set
			{
				if (this.allowNew != value)
				{
					this.allowNew = value;
					this.OnListChanged(DataView.ResetEventArgs);
				}
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0020BD78 File Offset: 0x0020B178
		[Browsable(false)]
		[ResDescription("DataViewCountDescr")]
		public int Count
		{
			get
			{
				return this.rowViewCache.Count;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0020BD98 File Offset: 0x0020B198
		private int CountFromIndex
		{
			get
			{
				return ((this.index != null) ? this.index.RecordCount : 0) + ((this.addNewRow != null) ? 1 : 0);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0020BDC8 File Offset: 0x0020B1C8
		[ResDescription("DataViewDataViewManagerDescr")]
		[Browsable(false)]
		public DataViewManager DataViewManager
		{
			get
			{
				return this.dataViewManager;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0020BDE8 File Offset: 0x0020B1E8
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return !this.fInitInProgress;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0020BE08 File Offset: 0x0020B208
		[Browsable(false)]
		[ResDescription("DataViewIsOpenDescr")]
		protected bool IsOpen
		{
			get
			{
				return this.open;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0020BE28 File Offset: 0x0020B228
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0020BE38 File Offset: 0x0020B238
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x0020BE68 File Offset: 0x0020B268
		[DefaultValue("")]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataViewRowFilterDescr")]
		public virtual string RowFilter
		{
			get
			{
				DataExpression dataExpression = this.rowFilter as DataExpression;
				if (dataExpression != null)
				{
					return dataExpression.Expression;
				}
				return "";
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				Bid.Trace("<ds.DataView.set_RowFilter|API> %d#, '%ls'\n", this.ObjectID, value);
				if (this.fInitInProgress)
				{
					this.delayedRowFilter = value;
					return;
				}
				CultureInfo culture = (this.table != null) ? this.table.Locale : CultureInfo.CurrentCulture;
				if (this.rowFilter == null || string.Compare(this.RowFilter, value, false, culture) != 0)
				{
					DataExpression newRowFilter = new DataExpression(this.table, value);
					this.SetIndex(this.sort, this.recordStates, newRowFilter);
				}
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0020BEF8 File Offset: 0x0020B2F8
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0020BF28 File Offset: 0x0020B328
		internal Predicate<DataRow> RowPredicate
		{
			get
			{
				DataView.RowPredicateFilter rowPredicateFilter = this.GetFilter() as DataView.RowPredicateFilter;
				if (rowPredicateFilter == null)
				{
					return null;
				}
				return rowPredicateFilter.PredicateFilter;
			}
			set
			{
				if (!object.ReferenceEquals(this.RowPredicate, value))
				{
					this.SetIndex(this.Sort, this.RowStateFilter, (value != null) ? new DataView.RowPredicateFilter(value) : null);
				}
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0020BF68 File Offset: 0x0020B368
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x0020BF88 File Offset: 0x0020B388
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataViewRowStateFilterDescr")]
		[DefaultValue(DataViewRowState.CurrentRows)]
		public DataViewRowState RowStateFilter
		{
			get
			{
				return this.recordStates;
			}
			set
			{
				Bid.Trace("<ds.DataView.set_RowStateFilter|API> %d#, %d{ds.DataViewRowState}\n", this.ObjectID, (int)value);
				if (this.fInitInProgress)
				{
					this.delayedRecordStates = value;
					return;
				}
				if ((value & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
				{
					throw ExceptionBuilder.RecordStateRange();
				}
				if ((value & DataViewRowState.ModifiedOriginal) != DataViewRowState.None && (value & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
				{
					throw ExceptionBuilder.SetRowStateFilter();
				}
				if (this.recordStates != value)
				{
					this.SetIndex(this.sort, value, this.rowFilter);
				}
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0020BFF8 File Offset: 0x0020B3F8
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x0020C058 File Offset: 0x0020B458
		[DefaultValue("")]
		[ResDescription("DataViewSortDescr")]
		[ResCategory("DataCategory_Data")]
		public string Sort
		{
			get
			{
				if (this.sort.Length == 0 && this.applyDefaultSort && this.table != null && this.table._primaryIndex.Length > 0)
				{
					return this.table.FormatSortString(this.table._primaryIndex);
				}
				return this.sort;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				Bid.Trace("<ds.DataView.set_Sort|API> %d#, '%ls'\n", this.ObjectID, value);
				if (this.fInitInProgress)
				{
					this.delayedSort = value;
					return;
				}
				CultureInfo culture = (this.table != null) ? this.table.Locale : CultureInfo.CurrentCulture;
				if (string.Compare(this.sort, value, false, culture) != 0 || this._comparison != null)
				{
					this.CheckSort(value);
					this._comparison = null;
					this.SetIndex(value, this.recordStates, this.rowFilter);
				}
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0020C0E8 File Offset: 0x0020B4E8
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0020C108 File Offset: 0x0020B508
		internal Comparison<DataRow> SortComparison
		{
			get
			{
				return this._comparison;
			}
			set
			{
				Bid.Trace("<ds.DataView.set_SortComparison|API> %d#\n", this.ObjectID);
				if (!object.ReferenceEquals(this._comparison, value))
				{
					this._comparison = value;
					this.SetIndex("", this.recordStates, this.rowFilter);
				}
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0020C158 File Offset: 0x0020B558
		private void ResetSort()
		{
			this.sort = "";
			this.SetIndex(this.sort, this.recordStates, this.rowFilter);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0020C188 File Offset: 0x0020B588
		private bool ShouldSerializeSort()
		{
			return this.sort != null;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0020C1A8 File Offset: 0x0020B5A8
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0020C1B8 File Offset: 0x0020B5B8
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x0020C1D8 File Offset: 0x0020B5D8
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DataViewTableDescr")]
		[TypeConverter(typeof(DataTableTypeConverter))]
		[ResCategory("DataCategory_Data")]
		public DataTable Table
		{
			get
			{
				return this.table;
			}
			set
			{
				Bid.Trace("<ds.DataView.set_Table|API> %d#, %d\n", this.ObjectID, (value != null) ? value.ObjectID : 0);
				if (this.fInitInProgress && value != null)
				{
					this.delayedTable = value;
					return;
				}
				if (this.locked)
				{
					throw ExceptionBuilder.SetTable();
				}
				if (this.dataViewManager != null)
				{
					throw ExceptionBuilder.CanNotSetTable();
				}
				if (value != null && value.TableName.Length == 0)
				{
					throw ExceptionBuilder.CanNotBindTable();
				}
				if (this.table != value)
				{
					this.dvListener.UnregisterMetaDataEvents();
					this.table = value;
					if (this.table != null)
					{
						this.dvListener.RegisterMetaDataEvents(this.table);
					}
					this.SetIndex2("", DataViewRowState.CurrentRows, null, false);
					if (this.table != null)
					{
						this.OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, new DataTablePropertyDescriptor(this.table)));
					}
					this.OnListChanged(DataView.ResetEventArgs);
				}
			}
		}

		// Token: 0x17000180 RID: 384
		object IList.this[int recordIndex]
		{
			get
			{
				return this[recordIndex];
			}
			set
			{
				throw ExceptionBuilder.SetIListObject();
			}
		}

		// Token: 0x17000181 RID: 385
		public DataRowView this[int recordIndex]
		{
			get
			{
				return this.GetRowView(this.GetRow(recordIndex));
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0020C318 File Offset: 0x0020B718
		public virtual DataRowView AddNew()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataView.AddNew|API> %d#\n", this.ObjectID);
			DataRowView result;
			try
			{
				this.CheckOpen();
				if (!this.AllowNew)
				{
					throw ExceptionBuilder.AddNewNotAllowNull();
				}
				if (this.addNewRow != null)
				{
					this.rowViewCache[this.addNewRow].EndEdit();
				}
				this.addNewRow = this.table.NewRow();
				DataRowView dataRowView = new DataRowView(this, this.addNewRow);
				this.rowViewCache.Add(this.addNewRow, dataRowView);
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, this.IndexOf(dataRowView)));
				result = dataRowView;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0020C3D8 File Offset: 0x0020B7D8
		public void BeginInit()
		{
			this.fInitInProgress = true;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0020C3F8 File Offset: 0x0020B7F8
		public void EndInit()
		{
			if (this.delayedTable != null && this.delayedTable.fInitInProgress)
			{
				this.delayedTable.delayedViews.Add(this);
				return;
			}
			this.fInitInProgress = false;
			this.fEndInitInProgress = true;
			if (this.delayedTable != null)
			{
				this.Table = this.delayedTable;
				this.delayedTable = null;
			}
			if (this.delayedSort != null)
			{
				this.Sort = this.delayedSort;
				this.delayedSort = null;
			}
			if (this.delayedRowFilter != null)
			{
				this.RowFilter = this.delayedRowFilter;
				this.delayedRowFilter = null;
			}
			if (this.delayedRecordStates != (DataViewRowState)(-1))
			{
				this.RowStateFilter = this.delayedRecordStates;
				this.delayedRecordStates = (DataViewRowState)(-1);
			}
			this.fEndInitInProgress = false;
			this.SetIndex(this.Sort, this.RowStateFilter, this.rowFilter);
			this.OnInitialized();
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0020C4D8 File Offset: 0x0020B8D8
		private void CheckOpen()
		{
			if (!this.IsOpen)
			{
				throw ExceptionBuilder.NotOpen();
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0020C4F8 File Offset: 0x0020B8F8
		private void CheckSort(string sort)
		{
			if (this.table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			if (sort.Length == 0)
			{
				return;
			}
			this.table.ParseSortString(sort);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0020C538 File Offset: 0x0020B938
		protected void Close()
		{
			this.shouldOpen = false;
			this.UpdateIndex();
			this.dvListener.UnregisterMetaDataEvents();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0020C568 File Offset: 0x0020B968
		public void CopyTo(Array array, int index)
		{
			checked
			{
				if (this.index != null)
				{
					RBTree<int>.RBTreeEnumerator enumerator = this.index.GetEnumerator(0);
					while (enumerator.MoveNext())
					{
						int record = enumerator.Current;
						array.SetValue(this.GetRowView(record), index);
						index++;
					}
				}
				if (this.addNewRow != null)
				{
					array.SetValue(this.rowViewCache[this.addNewRow], index);
				}
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0020C5D8 File Offset: 0x0020B9D8
		private void CopyTo(DataRowView[] array, int index)
		{
			checked
			{
				if (this.index != null)
				{
					RBTree<int>.RBTreeEnumerator enumerator = this.index.GetEnumerator(0);
					while (enumerator.MoveNext())
					{
						int record = enumerator.Current;
						array[index] = this.GetRowView(record);
						index++;
					}
				}
				if (this.addNewRow != null)
				{
					array[index] = this.rowViewCache[this.addNewRow];
				}
			}
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0020C638 File Offset: 0x0020BA38
		public void Delete(int index)
		{
			this.Delete(this.GetRow(index));
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0020C658 File Offset: 0x0020BA58
		internal void Delete(DataRow row)
		{
			if (row != null)
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataView.Delete|API> %d#, row=%d#", this.ObjectID, row.ObjectID);
				try
				{
					this.CheckOpen();
					if (row == this.addNewRow)
					{
						this.FinishAddNew(false);
					}
					else
					{
						if (!this.AllowDelete)
						{
							throw ExceptionBuilder.CanNotDelete();
						}
						row.Delete();
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0020C6D8 File Offset: 0x0020BAD8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0020C6F8 File Offset: 0x0020BAF8
		public int Find(object key)
		{
			return this.FindByKey(key);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0020C718 File Offset: 0x0020BB18
		internal virtual int FindByKey(object key)
		{
			return this.index.FindRecordByKey(key);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0020C738 File Offset: 0x0020BB38
		public int Find(object[] key)
		{
			return this.FindByKey(key);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0020C758 File Offset: 0x0020BB58
		internal virtual int FindByKey(object[] key)
		{
			return this.index.FindRecordByKey(key);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0020C778 File Offset: 0x0020BB78
		public DataRowView[] FindRows(object key)
		{
			return this.FindRowsByKey(new object[]
			{
				key
			});
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0020C798 File Offset: 0x0020BB98
		public DataRowView[] FindRows(object[] key)
		{
			return this.FindRowsByKey(key);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0020C7B8 File Offset: 0x0020BBB8
		internal virtual DataRowView[] FindRowsByKey(object[] key)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataView.FindRows|API> %d#\n", this.ObjectID);
			DataRowView[] dataRowViewFromRange;
			try
			{
				Range range = this.index.FindRecords(key);
				dataRowViewFromRange = this.GetDataRowViewFromRange(range);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return dataRowViewFromRange;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0020C818 File Offset: 0x0020BC18
		internal Range FindRecords<TKey, TRow>(Index.ComparisonBySelector<TKey, TRow> comparison, TKey key) where TRow : DataRow
		{
			return this.index.FindRecords<TKey, TRow>(comparison, key);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0020C838 File Offset: 0x0020BC38
		internal DataRowView[] GetDataRowViewFromRange(Range range)
		{
			if (range.IsNull)
			{
				return new DataRowView[0];
			}
			DataRowView[] array = new DataRowView[range.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this[i + range.Min];
			}
			return array;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0020C888 File Offset: 0x0020BC88
		internal void FinishAddNew(bool success)
		{
			Bid.Trace("<ds.DataView.FinishAddNew|INFO> %d#, success=%d{bool}\n", this.ObjectID, success);
			DataRow dataRow = this.addNewRow;
			if (success)
			{
				if (DataRowState.Detached == dataRow.RowState)
				{
					this.table.Rows.Add(dataRow);
				}
				else
				{
					dataRow.EndEdit();
				}
			}
			if (dataRow == this.addNewRow)
			{
				this.rowViewCache.Remove(this.addNewRow);
				this.addNewRow = null;
				if (!success)
				{
					dataRow.CancelEdit();
				}
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, this.Count));
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0020C918 File Offset: 0x0020BD18
		public IEnumerator GetEnumerator()
		{
			DataRowView[] array = new DataRowView[this.Count];
			this.CopyTo(array, 0);
			return array.GetEnumerator();
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0020C948 File Offset: 0x0020BD48
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0020C958 File Offset: 0x0020BD58
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0020C968 File Offset: 0x0020BD68
		int IList.Add(object value)
		{
			if (value == null)
			{
				this.AddNew();
				return this.Count - 1;
			}
			throw ExceptionBuilder.AddExternalObject();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0020C998 File Offset: 0x0020BD98
		void IList.Clear()
		{
			throw ExceptionBuilder.CanNotClear();
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0020C9B8 File Offset: 0x0020BDB8
		bool IList.Contains(object value)
		{
			return 0 <= this.IndexOf(value as DataRowView);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0020C9D8 File Offset: 0x0020BDD8
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as DataRowView);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0020C9F8 File Offset: 0x0020BDF8
		internal int IndexOf(DataRowView rowview)
		{
			if (rowview != null)
			{
				if (object.ReferenceEquals(this.addNewRow, rowview.Row))
				{
					return this.Count - 1;
				}
				DataRowView dataRowView;
				if (this.index != null && DataRowState.Detached != rowview.Row.RowState && this.rowViewCache.TryGetValue(rowview.Row, out dataRowView) && dataRowView == rowview)
				{
					return this.IndexOfDataRowView(rowview);
				}
			}
			return -1;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0020CA68 File Offset: 0x0020BE68
		private int IndexOfDataRowView(DataRowView rowview)
		{
			return this.index.GetIndex(rowview.GetRecord());
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0020CA88 File Offset: 0x0020BE88
		void IList.Insert(int index, object value)
		{
			throw ExceptionBuilder.InsertExternalObject();
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0020CAA8 File Offset: 0x0020BEA8
		void IList.Remove(object value)
		{
			int num = this.IndexOf(value as DataRowView);
			if (0 <= num)
			{
				((IList)this).RemoveAt(num);
				return;
			}
			throw ExceptionBuilder.RemoveExternalObject();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0020CAD8 File Offset: 0x0020BED8
		void IList.RemoveAt(int index)
		{
			this.Delete(index);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0020CAF8 File Offset: 0x0020BEF8
		internal Index GetFindIndex(string column, bool keepIndex)
		{
			if (this.findIndexes == null)
			{
				this.findIndexes = new Dictionary<string, Index>();
			}
			Index index;
			if (this.findIndexes.TryGetValue(column, out index))
			{
				if (!keepIndex)
				{
					this.findIndexes.Remove(column);
					index.RemoveRef();
					if (index.RefCount == 1)
					{
						index.RemoveRef();
					}
				}
			}
			else if (keepIndex)
			{
				index = this.table.GetIndex(column, this.recordStates, this.GetFilter());
				this.findIndexes[column] = index;
				index.AddRef();
			}
			return index;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0020CB88 File Offset: 0x0020BF88
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0020CBA8 File Offset: 0x0020BFA8
		object IBindingList.AddNew()
		{
			return this.AddNew();
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0020CBC8 File Offset: 0x0020BFC8
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0020CBE8 File Offset: 0x0020BFE8
		bool IBindingList.AllowRemove
		{
			get
			{
				return this.AllowDelete;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0020CC08 File Offset: 0x0020C008
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0020CC18 File Offset: 0x0020C018
		bool IBindingList.SupportsSearching
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0020CC28 File Offset: 0x0020C028
		bool IBindingList.SupportsSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0020CC38 File Offset: 0x0020C038
		bool IBindingList.IsSorted
		{
			get
			{
				return this.Sort.Length != 0;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0020CC58 File Offset: 0x0020C058
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return this.GetSortProperty();
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0020CC78 File Offset: 0x0020C078
		internal PropertyDescriptor GetSortProperty()
		{
			if (this.table != null && this.index != null && this.index.IndexFields.Length == 1)
			{
				return new DataColumnPropertyDescriptor(this.index.IndexFields[0].Column);
			}
			return null;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0020CCC8 File Offset: 0x0020C0C8
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				if (this.index.IndexFields.Length == 1 && this.index.IndexFields[0].IsDescending)
				{
					return ListSortDirection.Descending;
				}
				return ListSortDirection.Ascending;
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000B5E RID: 2910 RVA: 0x0020CD08 File Offset: 0x0020C108
		// (remove) Token: 0x06000B5F RID: 2911 RVA: 0x0020CD48 File Offset: 0x0020C148
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataViewListChangedDescr")]
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				Bid.Trace("<ds.DataView.add_ListChanged|API> %d#\n", this.ObjectID);
				this.onListChanged = (ListChangedEventHandler)Delegate.Combine(this.onListChanged, value);
			}
			remove
			{
				Bid.Trace("<ds.DataView.remove_ListChanged|API> %d#\n", this.ObjectID);
				this.onListChanged = (ListChangedEventHandler)Delegate.Remove(this.onListChanged, value);
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000B60 RID: 2912 RVA: 0x0020CD88 File Offset: 0x0020C188
		// (remove) Token: 0x06000B61 RID: 2913 RVA: 0x0020CDB8 File Offset: 0x0020C1B8
		[ResDescription("DataSetInitializedDescr")]
		[ResCategory("DataCategory_Action")]
		public event EventHandler Initialized
		{
			add
			{
				this.onInitialized = (EventHandler)Delegate.Combine(this.onInitialized, value);
			}
			remove
			{
				this.onInitialized = (EventHandler)Delegate.Remove(this.onInitialized, value);
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0020CDE8 File Offset: 0x0020C1E8
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			this.GetFindIndex(property.Name, true);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0020CE08 File Offset: 0x0020C208
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			this.Sort = this.CreateSortString(property, direction);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0020CE28 File Offset: 0x0020C228
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			if (property != null)
			{
				bool flag = false;
				Index index = null;
				try
				{
					if (this.findIndexes == null || !this.findIndexes.TryGetValue(property.Name, out index))
					{
						flag = true;
						index = this.table.GetIndex(property.Name, this.recordStates, this.GetFilter());
						index.AddRef();
					}
					Range range = index.FindRecords(key);
					if (!range.IsNull)
					{
						return this.index.GetIndex(index.GetRecord(range.Min));
					}
				}
				finally
				{
					if (flag && index != null)
					{
						index.RemoveRef();
						if (index.RefCount == 1)
						{
							index.RemoveRef();
						}
					}
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0020CEF8 File Offset: 0x0020C2F8
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			this.GetFindIndex(property.Name, false);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0020CF18 File Offset: 0x0020C318
		void IBindingList.RemoveSort()
		{
			Bid.Trace("<ds.DataView.RemoveSort|API> %d#\n", this.ObjectID);
			this.Sort = string.Empty;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0020CF48 File Offset: 0x0020C348
		void IBindingListView.ApplySort(ListSortDescriptionCollection sorts)
		{
			if (sorts == null)
			{
				throw ExceptionBuilder.ArgumentNull("sorts");
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (object obj in ((IEnumerable)sorts))
			{
				ListSortDescription listSortDescription = (ListSortDescription)obj;
				if (listSortDescription == null)
				{
					throw ExceptionBuilder.ArgumentContainsNull("sorts");
				}
				PropertyDescriptor propertyDescriptor = listSortDescription.PropertyDescriptor;
				if (propertyDescriptor == null)
				{
					throw ExceptionBuilder.ArgumentNull("PropertyDescriptor");
				}
				if (!this.table.Columns.Contains(propertyDescriptor.Name))
				{
					throw ExceptionBuilder.ColumnToSortIsOutOfRange(propertyDescriptor.Name);
				}
				ListSortDirection sortDirection = listSortDescription.SortDirection;
				if (flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(this.CreateSortString(propertyDescriptor, sortDirection));
				if (!flag)
				{
					flag = true;
				}
			}
			this.Sort = stringBuilder.ToString();
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0020D048 File Offset: 0x0020C448
		private string CreateSortString(PropertyDescriptor property, ListSortDirection direction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append(property.Name);
			stringBuilder.Append(']');
			if (ListSortDirection.Descending == direction)
			{
				stringBuilder.Append(" DESC");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0020D098 File Offset: 0x0020C498
		void IBindingListView.RemoveFilter()
		{
			Bid.Trace("<ds.DataView.RemoveFilter|API> %d#\n", this.ObjectID);
			this.RowFilter = "";
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0020D0C8 File Offset: 0x0020C4C8
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0020D0E8 File Offset: 0x0020C4E8
		string IBindingListView.Filter
		{
			get
			{
				return this.RowFilter;
			}
			set
			{
				this.RowFilter = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0020D108 File Offset: 0x0020C508
		ListSortDescriptionCollection IBindingListView.SortDescriptions
		{
			get
			{
				return this.GetSortDescriptions();
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0020D128 File Offset: 0x0020C528
		internal ListSortDescriptionCollection GetSortDescriptions()
		{
			ListSortDescription[] array = new ListSortDescription[0];
			if (this.table != null && this.index != null && this.index.IndexFields.Length > 0)
			{
				array = new ListSortDescription[this.index.IndexFields.Length];
				for (int i = 0; i < this.index.IndexFields.Length; i++)
				{
					DataColumnPropertyDescriptor property = new DataColumnPropertyDescriptor(this.index.IndexFields[i].Column);
					if (this.index.IndexFields[i].IsDescending)
					{
						array[i] = new ListSortDescription(property, ListSortDirection.Descending);
					}
					else
					{
						array[i] = new ListSortDescription(property, ListSortDirection.Ascending);
					}
				}
			}
			return new ListSortDescriptionCollection(array);
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0020D1F8 File Offset: 0x0020C5F8
		bool IBindingListView.SupportsAdvancedSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0020D208 File Offset: 0x0020C608
		bool IBindingListView.SupportsFiltering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0020D218 File Offset: 0x0020C618
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			if (this.table != null)
			{
				if (listAccessors == null || listAccessors.Length == 0)
				{
					return this.table.TableName;
				}
				DataSet dataSet = this.table.DataSet;
				if (dataSet != null)
				{
					DataTable dataTable = dataSet.FindTable(this.table, listAccessors, 0);
					if (dataTable != null)
					{
						return dataTable.TableName;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0020D278 File Offset: 0x0020C678
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			if (this.table != null)
			{
				if (listAccessors == null || listAccessors.Length == 0)
				{
					return this.table.GetPropertyDescriptorCollection(null);
				}
				DataSet dataSet = this.table.DataSet;
				if (dataSet == null)
				{
					return new PropertyDescriptorCollection(null);
				}
				DataTable dataTable = dataSet.FindTable(this.table, listAccessors, 0);
				if (dataTable != null)
				{
					return dataTable.GetPropertyDescriptorCollection(null);
				}
			}
			return new PropertyDescriptorCollection(null);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0020D2D8 File Offset: 0x0020C6D8
		internal virtual IFilter GetFilter()
		{
			return this.rowFilter;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0020D2F8 File Offset: 0x0020C6F8
		private int GetRecord(int recordIndex)
		{
			if (this.Count <= recordIndex)
			{
				throw ExceptionBuilder.RowOutOfRange(recordIndex);
			}
			if (recordIndex == this.index.RecordCount)
			{
				return this.addNewRow.GetDefaultRecord();
			}
			return this.index.GetRecord(recordIndex);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0020D348 File Offset: 0x0020C748
		internal DataRow GetRow(int index)
		{
			int count = this.Count;
			if (count <= index)
			{
				throw ExceptionBuilder.GetElementIndex(index);
			}
			if (index == count - 1 && this.addNewRow != null)
			{
				return this.addNewRow;
			}
			return this.table.recordManager[this.GetRecord(index)];
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0020D398 File Offset: 0x0020C798
		private DataRowView GetRowView(int record)
		{
			return this.GetRowView(this.table.recordManager[record]);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0020D3C8 File Offset: 0x0020C7C8
		private DataRowView GetRowView(DataRow dr)
		{
			return this.rowViewCache[dr];
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0020D3E8 File Offset: 0x0020C7E8
		protected virtual void IndexListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType != ListChangedType.Reset)
			{
				this.OnListChanged(e);
			}
			if (this.addNewRow != null && this.index.RecordCount == 0)
			{
				this.FinishAddNew(false);
			}
			if (e.ListChangedType == ListChangedType.Reset)
			{
				this.OnListChanged(e);
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0020D438 File Offset: 0x0020C838
		internal void IndexListChangedInternal(ListChangedEventArgs e)
		{
			this.rowViewBuffer.Clear();
			if (ListChangedType.ItemAdded == e.ListChangedType && this.addNewMoved != null && this.addNewMoved.NewIndex != this.addNewMoved.OldIndex)
			{
				ListChangedEventArgs e2 = this.addNewMoved;
				this.addNewMoved = null;
				this.IndexListChanged(this, e2);
			}
			this.IndexListChanged(this, e);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0020D498 File Offset: 0x0020C898
		internal void MaintainDataView(ListChangedType changedType, DataRow row, bool trackAddRemove)
		{
			DataRowView dataRowView = null;
			switch (changedType)
			{
			case ListChangedType.Reset:
				this.ResetRowViewCache();
				break;
			case ListChangedType.ItemAdded:
				if (trackAddRemove && this.rowViewBuffer.TryGetValue(row, out dataRowView))
				{
					this.rowViewBuffer.Remove(row);
				}
				if (row == this.addNewRow)
				{
					int newIndex = this.IndexOfDataRowView(this.rowViewCache[this.addNewRow]);
					this.addNewRow = null;
					this.addNewMoved = new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, this.Count - 1);
					return;
				}
				if (!this.rowViewCache.ContainsKey(row))
				{
					this.rowViewCache.Add(row, dataRowView ?? new DataRowView(this, row));
					return;
				}
				break;
			case ListChangedType.ItemDeleted:
				if (trackAddRemove)
				{
					this.rowViewCache.TryGetValue(row, out dataRowView);
					if (dataRowView != null)
					{
						this.rowViewBuffer.Add(row, dataRowView);
					}
				}
				if (!this.rowViewCache.Remove(row))
				{
					return;
				}
				break;
			case ListChangedType.ItemMoved:
			case ListChangedType.ItemChanged:
			case ListChangedType.PropertyDescriptorAdded:
			case ListChangedType.PropertyDescriptorDeleted:
			case ListChangedType.PropertyDescriptorChanged:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0020D598 File Offset: 0x0020C998
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			Bid.Trace("<ds.DataView.OnListChanged|INFO> %d#, ListChangedType=%d{ListChangedType}\n", this.ObjectID, (int)e.ListChangedType);
			try
			{
				DataColumn dataColumn = null;
				string text = null;
				switch (e.ListChangedType)
				{
				case ListChangedType.ItemMoved:
				case ListChangedType.ItemChanged:
					if (0 <= e.NewIndex)
					{
						DataRow row = this.GetRow(e.NewIndex);
						if (row.HasPropertyChanged)
						{
							dataColumn = row.LastChangedColumn;
							text = ((dataColumn != null) ? dataColumn.ColumnName : string.Empty);
						}
						row.ResetLastChangedColumn();
					}
					break;
				}
				if (this.onListChanged != null)
				{
					if (dataColumn != null && e.NewIndex == e.OldIndex)
					{
						ListChangedEventArgs e2 = new ListChangedEventArgs(e.ListChangedType, e.NewIndex, new DataColumnPropertyDescriptor(dataColumn));
						this.onListChanged(this, e2);
					}
					else
					{
						this.onListChanged(this, e);
					}
				}
				if (text != null)
				{
					this[e.NewIndex].RaisePropertyChangedEvent(text);
				}
			}
			catch (Exception e3)
			{
				if (!ADP.IsCatchableExceptionType(e3))
				{
					throw;
				}
				ExceptionBuilder.TraceExceptionWithoutRethrow(e3);
			}
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0020D6C8 File Offset: 0x0020CAC8
		private void OnInitialized()
		{
			if (this.onInitialized != null)
			{
				this.onInitialized(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0020D6F8 File Offset: 0x0020CAF8
		protected void Open()
		{
			this.shouldOpen = true;
			this.UpdateIndex();
			this.dvListener.RegisterMetaDataEvents(this.table);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0020D728 File Offset: 0x0020CB28
		protected void Reset()
		{
			if (this.IsOpen)
			{
				this.index.Reset();
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0020D748 File Offset: 0x0020CB48
		internal void ResetRowViewCache()
		{
			Dictionary<DataRow, DataRowView> dictionary = new Dictionary<DataRow, DataRowView>(this.CountFromIndex, DataView.DataRowReferenceComparer.Default);
			if (this.index != null)
			{
				RBTree<int>.RBTreeEnumerator enumerator = this.index.GetEnumerator(0);
				while (enumerator.MoveNext())
				{
					int record = enumerator.Current;
					DataRow dataRow = this.table.recordManager[record];
					DataRowView value;
					if (!this.rowViewCache.TryGetValue(dataRow, out value))
					{
						value = new DataRowView(this, dataRow);
					}
					dictionary.Add(dataRow, value);
				}
			}
			if (this.addNewRow != null)
			{
				DataRowView value;
				this.rowViewCache.TryGetValue(this.addNewRow, out value);
				dictionary.Add(this.addNewRow, value);
			}
			this.rowViewCache = dictionary;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0020D7F8 File Offset: 0x0020CBF8
		internal void SetDataViewManager(DataViewManager dataViewManager)
		{
			if (this.table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			if (this.dataViewManager != dataViewManager)
			{
				if (dataViewManager != null)
				{
					dataViewManager.nViews--;
				}
				this.dataViewManager = dataViewManager;
				if (dataViewManager != null)
				{
					dataViewManager.nViews++;
					DataViewSetting dataViewSetting = dataViewManager.DataViewSettings[this.table];
					try
					{
						this.applyDefaultSort = dataViewSetting.ApplyDefaultSort;
						DataExpression newRowFilter = new DataExpression(this.table, dataViewSetting.RowFilter);
						this.SetIndex(dataViewSetting.Sort, dataViewSetting.RowStateFilter, newRowFilter);
					}
					catch (Exception e)
					{
						if (!ADP.IsCatchableExceptionType(e))
						{
							throw;
						}
						ExceptionBuilder.TraceExceptionWithoutRethrow(e);
					}
					this.locked = true;
					return;
				}
				this.SetIndex("", DataViewRowState.CurrentRows, null);
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0020D8D8 File Offset: 0x0020CCD8
		internal virtual void SetIndex(string newSort, DataViewRowState newRowStates, IFilter newRowFilter)
		{
			this.SetIndex2(newSort, newRowStates, newRowFilter, true);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0020D8F8 File Offset: 0x0020CCF8
		internal void SetIndex2(string newSort, DataViewRowState newRowStates, IFilter newRowFilter, bool fireEvent)
		{
			Bid.Trace("<ds.DataView.SetIndex|INFO> %d#, newSort='%ls', newRowStates=%d{ds.DataViewRowState}\n", this.ObjectID, newSort, (int)newRowStates);
			this.sort = newSort;
			this.recordStates = newRowStates;
			this.rowFilter = newRowFilter;
			if (this.fEndInitInProgress)
			{
				return;
			}
			if (fireEvent)
			{
				this.UpdateIndex(true);
			}
			else
			{
				this.UpdateIndex(true, false);
			}
			if (this.findIndexes != null)
			{
				Dictionary<string, Index> dictionary = this.findIndexes;
				this.findIndexes = null;
				foreach (KeyValuePair<string, Index> keyValuePair in dictionary)
				{
					keyValuePair.Value.RemoveRef();
				}
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0020D9B8 File Offset: 0x0020CDB8
		protected void UpdateIndex()
		{
			this.UpdateIndex(false);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0020D9D8 File Offset: 0x0020CDD8
		protected virtual void UpdateIndex(bool force)
		{
			this.UpdateIndex(force, true);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0020D9F8 File Offset: 0x0020CDF8
		internal void UpdateIndex(bool force, bool fireEvent)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataView.UpdateIndex|INFO> %d#, force=%d{bool}\n", this.ObjectID, force);
			try
			{
				if (this.open != this.shouldOpen || force)
				{
					this.open = this.shouldOpen;
					Index index = null;
					if (this.open && this.table != null)
					{
						if (this.SortComparison != null)
						{
							index = new Index(this.table, this.SortComparison, this.recordStates, this.GetFilter());
							index.AddRef();
						}
						else
						{
							index = this.table.GetIndex(this.Sort, this.recordStates, this.GetFilter());
						}
					}
					if (this.index != index)
					{
						if (this.index == null)
						{
							DataTable dataTable = index.Table;
						}
						else
						{
							DataTable dataTable2 = this.index.Table;
						}
						if (this.index != null)
						{
							this.dvListener.UnregisterListChangedEvent();
						}
						this.index = index;
						if (this.index != null)
						{
							this.dvListener.RegisterListChangedEvent(this.index);
						}
						this.ResetRowViewCache();
						if (fireEvent)
						{
							this.OnListChanged(DataView.ResetEventArgs);
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0020DB38 File Offset: 0x0020CF38
		internal void ChildRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0020DBA8 File Offset: 0x0020CFA8
		internal void ParentRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0020DC18 File Offset: 0x0020D018
		protected virtual void ColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataColumnPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataColumnPropertyDescriptor((DataColumn)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataColumnPropertyDescriptor((DataColumn)e.Element)) : null)));
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0020DC88 File Offset: 0x0020D088
		internal void ColumnCollectionChangedInternal(object sender, CollectionChangeEventArgs e)
		{
			this.ColumnCollectionChanged(sender, e);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0020DCA8 File Offset: 0x0020D0A8
		public DataTable ToTable()
		{
			return this.ToTable(null, false, new string[0]);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0020DCC8 File Offset: 0x0020D0C8
		public DataTable ToTable(string tableName)
		{
			return this.ToTable(tableName, false, new string[0]);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0020DCE8 File Offset: 0x0020D0E8
		public DataTable ToTable(bool distinct, params string[] columnNames)
		{
			return this.ToTable(null, distinct, columnNames);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0020DD08 File Offset: 0x0020D108
		public DataTable ToTable(string tableName, bool distinct, params string[] columnNames)
		{
			Bid.Trace("<ds.DataView.ToTable|API> %d#, TableName='%ls', distinct=%d{bool}\n", this.ObjectID, tableName, distinct);
			if (columnNames == null)
			{
				throw ExceptionBuilder.ArgumentNull("columnNames");
			}
			DataTable dataTable = new DataTable();
			dataTable.Locale = this.table.Locale;
			dataTable.CaseSensitive = this.table.CaseSensitive;
			dataTable.TableName = ((tableName != null) ? tableName : this.table.TableName);
			dataTable.Namespace = this.table.Namespace;
			dataTable.Prefix = this.table.Prefix;
			if (columnNames.Length == 0)
			{
				columnNames = new string[this.Table.Columns.Count];
				for (int i = 0; i < columnNames.Length; i++)
				{
					columnNames[i] = this.Table.Columns[i].ColumnName;
				}
			}
			int[] array = new int[columnNames.Length];
			List<object[]> list = new List<object[]>();
			for (int j = 0; j < columnNames.Length; j++)
			{
				DataColumn dataColumn = this.Table.Columns[columnNames[j]];
				if (dataColumn == null)
				{
					throw ExceptionBuilder.ColumnNotInTheUnderlyingTable(columnNames[j], this.Table.TableName);
				}
				dataTable.Columns.Add(dataColumn.Clone());
				array[j] = this.Table.Columns.IndexOf(dataColumn);
			}
			foreach (object obj in this)
			{
				DataRowView dataRowView = (DataRowView)obj;
				object[] array2 = new object[columnNames.Length];
				for (int k = 0; k < array.Length; k++)
				{
					array2[k] = dataRowView[array[k]];
				}
				if (!distinct || !this.RowExist(list, array2))
				{
					dataTable.Rows.Add(array2);
					list.Add(array2);
				}
			}
			return dataTable;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0020DEF8 File Offset: 0x0020D2F8
		private bool RowExist(List<object[]> arraylist, object[] objectArray)
		{
			for (int i = 0; i < arraylist.Count; i++)
			{
				object[] array = arraylist[i];
				bool flag = true;
				for (int j = 0; j < objectArray.Length; j++)
				{
					flag &= array[j].Equals(objectArray[j]);
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0020DF48 File Offset: 0x0020D348
		public virtual bool Equals(DataView view)
		{
			return view != null && this.Table == view.Table && this.Count == view.Count && string.Compare(this.RowFilter, view.RowFilter, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Sort, view.Sort, StringComparison.OrdinalIgnoreCase) == 0 && object.ReferenceEquals(this.SortComparison, view.SortComparison) && object.ReferenceEquals(this.RowPredicate, view.RowPredicate) && this.RowStateFilter == view.RowStateFilter && this.DataViewManager == view.DataViewManager && this.AllowDelete == view.AllowDelete && this.AllowNew == view.AllowNew && this.AllowEdit == view.AllowEdit;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0020E018 File Offset: 0x0020D418
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x0400083A RID: 2106
		private DataViewManager dataViewManager;

		// Token: 0x0400083B RID: 2107
		private DataTable table;

		// Token: 0x0400083C RID: 2108
		private bool locked;

		// Token: 0x0400083D RID: 2109
		private Index index;

		// Token: 0x0400083E RID: 2110
		private Dictionary<string, Index> findIndexes;

		// Token: 0x0400083F RID: 2111
		private string sort = "";

		// Token: 0x04000840 RID: 2112
		private Comparison<DataRow> _comparison;

		// Token: 0x04000841 RID: 2113
		private IFilter rowFilter;

		// Token: 0x04000842 RID: 2114
		private DataViewRowState recordStates = DataViewRowState.CurrentRows;

		// Token: 0x04000843 RID: 2115
		private bool shouldOpen = true;

		// Token: 0x04000844 RID: 2116
		private bool open;

		// Token: 0x04000845 RID: 2117
		private bool allowNew = true;

		// Token: 0x04000846 RID: 2118
		private bool allowEdit = true;

		// Token: 0x04000847 RID: 2119
		private bool allowDelete = true;

		// Token: 0x04000848 RID: 2120
		private bool applyDefaultSort;

		// Token: 0x04000849 RID: 2121
		internal DataRow addNewRow;

		// Token: 0x0400084A RID: 2122
		private ListChangedEventArgs addNewMoved;

		// Token: 0x0400084B RID: 2123
		private ListChangedEventHandler onListChanged;

		// Token: 0x0400084C RID: 2124
		private EventHandler onInitialized;

		// Token: 0x0400084D RID: 2125
		internal static ListChangedEventArgs ResetEventArgs = new ListChangedEventArgs(ListChangedType.Reset, -1);

		// Token: 0x0400084E RID: 2126
		private DataTable delayedTable;

		// Token: 0x0400084F RID: 2127
		private string delayedRowFilter;

		// Token: 0x04000850 RID: 2128
		private string delayedSort;

		// Token: 0x04000851 RID: 2129
		private DataViewRowState delayedRecordStates = (DataViewRowState)(-1);

		// Token: 0x04000852 RID: 2130
		private bool fInitInProgress;

		// Token: 0x04000853 RID: 2131
		private bool fEndInitInProgress;

		// Token: 0x04000854 RID: 2132
		private Dictionary<DataRow, DataRowView> rowViewCache = new Dictionary<DataRow, DataRowView>(DataView.DataRowReferenceComparer.Default);

		// Token: 0x04000855 RID: 2133
		private readonly Dictionary<DataRow, DataRowView> rowViewBuffer = new Dictionary<DataRow, DataRowView>(DataView.DataRowReferenceComparer.Default);

		// Token: 0x04000856 RID: 2134
		private DataViewListener dvListener;

		// Token: 0x04000857 RID: 2135
		private static int _objectTypeCount;

		// Token: 0x04000858 RID: 2136
		private readonly int _objectID = Interlocked.Increment(ref DataView._objectTypeCount);

		// Token: 0x020000A7 RID: 167
		private sealed class DataRowReferenceComparer : IEqualityComparer<DataRow>
		{
			// Token: 0x06000B91 RID: 2961 RVA: 0x0020E058 File Offset: 0x0020D458
			private DataRowReferenceComparer()
			{
			}

			// Token: 0x06000B92 RID: 2962 RVA: 0x0020E078 File Offset: 0x0020D478
			public bool Equals(DataRow x, DataRow y)
			{
				return x == y;
			}

			// Token: 0x06000B93 RID: 2963 RVA: 0x0020E098 File Offset: 0x0020D498
			public int GetHashCode(DataRow obj)
			{
				return obj.ObjectID;
			}

			// Token: 0x04000859 RID: 2137
			internal static readonly DataView.DataRowReferenceComparer Default = new DataView.DataRowReferenceComparer();
		}

		// Token: 0x020000A9 RID: 169
		private sealed class RowPredicateFilter : IFilter
		{
			// Token: 0x06000B96 RID: 2966 RVA: 0x0020E0D8 File Offset: 0x0020D4D8
			internal RowPredicateFilter(Predicate<DataRow> predicate)
			{
				this.PredicateFilter = predicate;
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x0020E0F8 File Offset: 0x0020D4F8
			bool IFilter.Invoke(DataRow row, DataRowVersion version)
			{
				return this.PredicateFilter(row);
			}

			// Token: 0x0400085A RID: 2138
			internal readonly Predicate<DataRow> PredicateFilter;
		}
	}
}

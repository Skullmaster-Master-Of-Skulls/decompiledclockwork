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
	// Token: 0x020000D8 RID: 216
	[DefaultEvent("PositionChanged")]
	[Designer("Microsoft.VSDesigner.Data.VS.DataViewDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Table")]
	[Editor("Microsoft.VSDesigner.Data.Design.DataSourceEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataView : MarshalByValueComponent, IBindingListView, IBindingList, IList, ICollection, IEnumerable, ITypedList, ISupportInitializeNotification, ISupportInitialize
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x000757CC File Offset: 0x00074BCC
		internal DataView(DataTable table, bool locked)
		{
			GC.SuppressFinalize(this);
			Bid.Trace("<ds.DataView.DataView|INFO> %d#, table=%d, locked=%d{bool}\n", this.ObjectID, (table != null) ? table.ObjectID : 0, locked);
			this.dvListener = new DataViewListener(this);
			this.locked = locked;
			this.table = table;
			this.dvListener.RegisterMetaDataEvents(this.table);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00075894 File Offset: 0x00074C94
		public DataView() : this(null)
		{
			this.SetIndex2("", DataViewRowState.CurrentRows, null, true);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000758B8 File Offset: 0x00074CB8
		public DataView(DataTable table) : this(table, false)
		{
			this.SetIndex2("", DataViewRowState.CurrentRows, null, true);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000758DC File Offset: 0x00074CDC
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

		// Token: 0x06000E1E RID: 3614 RVA: 0x000759F8 File Offset: 0x00074DF8
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

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00075B0C File Offset: 0x00074F0C
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x00075B20 File Offset: 0x00074F20
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataViewAllowDeleteDescr")]
		[DefaultValue(true)]
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

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00075B48 File Offset: 0x00074F48
		// (set) Token: 0x06000E22 RID: 3618 RVA: 0x00075B5C File Offset: 0x00074F5C
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DataViewApplyDefaultSortDescr")]
		[DefaultValue(false)]
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

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x00075BA4 File Offset: 0x00074FA4
		// (set) Token: 0x06000E24 RID: 3620 RVA: 0x00075BB8 File Offset: 0x00074FB8
		[ResDescription("DataViewAllowEditDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue(true)]
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

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x00075BE0 File Offset: 0x00074FE0
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x00075BF4 File Offset: 0x00074FF4
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataViewAllowNewDescr")]
		[DefaultValue(true)]
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00075C1C File Offset: 0x0007501C
		[Browsable(false)]
		[ResDescription("DataViewCountDescr")]
		public int Count
		{
			get
			{
				return this.rowViewCache.Count;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00075C34 File Offset: 0x00075034
		private int CountFromIndex
		{
			get
			{
				return ((this.index != null) ? this.index.RecordCount : 0) + ((this.addNewRow != null) ? 1 : 0);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00075C64 File Offset: 0x00075064
		[Browsable(false)]
		[ResDescription("DataViewDataViewManagerDescr")]
		public DataViewManager DataViewManager
		{
			get
			{
				return this.dataViewManager;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00075C78 File Offset: 0x00075078
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return !this.fInitInProgress;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00075C90 File Offset: 0x00075090
		[Browsable(false)]
		[ResDescription("DataViewIsOpenDescr")]
		protected bool IsOpen
		{
			get
			{
				return this.open;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00075CA4 File Offset: 0x000750A4
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00075CB4 File Offset: 0x000750B4
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x00075CDC File Offset: 0x000750DC
		[ResDescription("DataViewRowFilterDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
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

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00075D68 File Offset: 0x00075168
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x00075D8C File Offset: 0x0007518C
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
				if (this.RowPredicate != value)
				{
					this.SetIndex(this.Sort, this.RowStateFilter, (value != null) ? new DataView.RowPredicateFilter(value) : null);
				}
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00075DC0 File Offset: 0x000751C0
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x00075DD4 File Offset: 0x000751D4
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

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x00075E3C File Offset: 0x0007523C
		// (set) Token: 0x06000E34 RID: 3636 RVA: 0x00075E94 File Offset: 0x00075294
		[ResDescription("DataViewSortDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
		public string Sort
		{
			get
			{
				if (this.sort.Length == 0 && this.applyDefaultSort && this.table != null && this.table._primaryIndex.Length != 0)
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

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x00075F20 File Offset: 0x00075320
		// (set) Token: 0x06000E36 RID: 3638 RVA: 0x00075F34 File Offset: 0x00075334
		internal Comparison<DataRow> SortComparison
		{
			get
			{
				return this._comparison;
			}
			set
			{
				Bid.Trace("<ds.DataView.set_SortComparison|API> %d#\n", this.ObjectID);
				if (this._comparison != value)
				{
					this._comparison = value;
					this.SetIndex("", this.recordStates, this.rowFilter);
				}
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00075F78 File Offset: 0x00075378
		private void ResetSort()
		{
			this.sort = "";
			this.SetIndex(this.sort, this.recordStates, this.rowFilter);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00075FA8 File Offset: 0x000753A8
		private bool ShouldSerializeSort()
		{
			return this.sort != null;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00075FC0 File Offset: 0x000753C0
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00075FD0 File Offset: 0x000753D0
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x00075FE4 File Offset: 0x000753E4
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DataViewTableDescr")]
		[TypeConverter(typeof(DataTableTypeConverter))]
		[DefaultValue(null)]
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

		// Token: 0x17000214 RID: 532
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

		// Token: 0x17000215 RID: 533
		public DataRowView this[int recordIndex]
		{
			get
			{
				return this.GetRowView(this.GetRow(recordIndex));
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00076104 File Offset: 0x00075504
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

		// Token: 0x06000E40 RID: 3648 RVA: 0x000761C4 File Offset: 0x000755C4
		public void BeginInit()
		{
			this.fInitInProgress = true;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000761D8 File Offset: 0x000755D8
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

		// Token: 0x06000E42 RID: 3650 RVA: 0x000762AC File Offset: 0x000756AC
		private void CheckOpen()
		{
			if (!this.IsOpen)
			{
				throw ExceptionBuilder.NotOpen();
			}
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x000762C8 File Offset: 0x000756C8
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

		// Token: 0x06000E44 RID: 3652 RVA: 0x000762FC File Offset: 0x000756FC
		protected void Close()
		{
			this.shouldOpen = false;
			this.UpdateIndex();
			this.dvListener.UnregisterMetaDataEvents();
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00076324 File Offset: 0x00075724
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

		// Token: 0x06000E46 RID: 3654 RVA: 0x0007638C File Offset: 0x0007578C
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

		// Token: 0x06000E47 RID: 3655 RVA: 0x000763EC File Offset: 0x000757EC
		public void Delete(int index)
		{
			this.Delete(this.GetRow(index));
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00076408 File Offset: 0x00075808
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

		// Token: 0x06000E49 RID: 3657 RVA: 0x00076484 File Offset: 0x00075884
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000764A4 File Offset: 0x000758A4
		public int Find(object key)
		{
			return this.FindByKey(key);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000764B8 File Offset: 0x000758B8
		internal virtual int FindByKey(object key)
		{
			return this.index.FindRecordByKey(key);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000764D4 File Offset: 0x000758D4
		public int Find(object[] key)
		{
			return this.FindByKey(key);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000764E8 File Offset: 0x000758E8
		internal virtual int FindByKey(object[] key)
		{
			return this.index.FindRecordByKey(key);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00076504 File Offset: 0x00075904
		public DataRowView[] FindRows(object key)
		{
			return this.FindRowsByKey(new object[]
			{
				key
			});
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00076524 File Offset: 0x00075924
		public DataRowView[] FindRows(object[] key)
		{
			return this.FindRowsByKey(key);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00076538 File Offset: 0x00075938
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

		// Token: 0x06000E51 RID: 3665 RVA: 0x00076594 File Offset: 0x00075994
		internal Range FindRecords<TKey, TRow>(Index.ComparisonBySelector<TKey, TRow> comparison, TKey key) where TRow : DataRow
		{
			return this.index.FindRecords<TKey, TRow>(comparison, key);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000765B0 File Offset: 0x000759B0
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

		// Token: 0x06000E53 RID: 3667 RVA: 0x000765FC File Offset: 0x000759FC
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
				bool flag = this.rowViewCache.Remove(this.addNewRow);
				this.addNewRow = null;
				if (!success)
				{
					dataRow.CancelEdit();
				}
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, this.Count));
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00076684 File Offset: 0x00075A84
		public IEnumerator GetEnumerator()
		{
			DataRowView[] array = new DataRowView[this.Count];
			this.CopyTo(array, 0);
			return array.GetEnumerator();
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000766AC File Offset: 0x00075AAC
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x000766BC File Offset: 0x00075ABC
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x000766CC File Offset: 0x00075ACC
		int IList.Add(object value)
		{
			if (value == null)
			{
				this.AddNew();
				return this.Count - 1;
			}
			throw ExceptionBuilder.AddExternalObject();
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x000766F4 File Offset: 0x00075AF4
		void IList.Clear()
		{
			throw ExceptionBuilder.CanNotClear();
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00076708 File Offset: 0x00075B08
		bool IList.Contains(object value)
		{
			return 0 <= this.IndexOf(value as DataRowView);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00076728 File Offset: 0x00075B28
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as DataRowView);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00076744 File Offset: 0x00075B44
		internal int IndexOf(DataRowView rowview)
		{
			if (rowview != null)
			{
				if (this.addNewRow == rowview.Row)
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

		// Token: 0x06000E5C RID: 3676 RVA: 0x000767A4 File Offset: 0x00075BA4
		private int IndexOfDataRowView(DataRowView rowview)
		{
			return this.index.GetIndex(rowview.Row.GetRecordFromVersion(rowview.Row.GetDefaultRowVersion(this.RowStateFilter) & (DataRowVersion)(-1025)));
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000767E0 File Offset: 0x00075BE0
		void IList.Insert(int index, object value)
		{
			throw ExceptionBuilder.InsertExternalObject();
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000767F4 File Offset: 0x00075BF4
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

		// Token: 0x06000E5F RID: 3679 RVA: 0x00076820 File Offset: 0x00075C20
		void IList.RemoveAt(int index)
		{
			this.Delete(index);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00076834 File Offset: 0x00075C34
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

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x000768C0 File Offset: 0x00075CC0
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000768D4 File Offset: 0x00075CD4
		object IBindingList.AddNew()
		{
			return this.AddNew();
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x000768E8 File Offset: 0x00075CE8
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x000768FC File Offset: 0x00075CFC
		bool IBindingList.AllowRemove
		{
			get
			{
				return this.AllowDelete;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00076910 File Offset: 0x00075D10
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00076920 File Offset: 0x00075D20
		bool IBindingList.SupportsSearching
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x00076930 File Offset: 0x00075D30
		bool IBindingList.SupportsSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00076940 File Offset: 0x00075D40
		bool IBindingList.IsSorted
		{
			get
			{
				return this.Sort.Length != 0;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0007695C File Offset: 0x00075D5C
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return this.GetSortProperty();
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00076970 File Offset: 0x00075D70
		internal PropertyDescriptor GetSortProperty()
		{
			if (this.table != null && this.index != null && this.index.IndexFields.Length == 1)
			{
				return new DataColumnPropertyDescriptor(this.index.IndexFields[0].Column);
			}
			return null;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x000769BC File Offset: 0x00075DBC
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

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000E6C RID: 3692 RVA: 0x000769F4 File Offset: 0x00075DF4
		// (remove) Token: 0x06000E6D RID: 3693 RVA: 0x00076A28 File Offset: 0x00075E28
		[ResDescription("DataViewListChangedDescr")]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000E6E RID: 3694 RVA: 0x00076A5C File Offset: 0x00075E5C
		// (remove) Token: 0x06000E6F RID: 3695 RVA: 0x00076A80 File Offset: 0x00075E80
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

		// Token: 0x06000E70 RID: 3696 RVA: 0x00076AA4 File Offset: 0x00075EA4
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			this.GetFindIndex(property.Name, true);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00076AC0 File Offset: 0x00075EC0
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			this.Sort = this.CreateSortString(property, direction);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00076ADC File Offset: 0x00075EDC
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

		// Token: 0x06000E73 RID: 3699 RVA: 0x00076BA4 File Offset: 0x00075FA4
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			this.GetFindIndex(property.Name, false);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00076BC0 File Offset: 0x00075FC0
		void IBindingList.RemoveSort()
		{
			Bid.Trace("<ds.DataView.RemoveSort|API> %d#\n", this.ObjectID);
			this.Sort = string.Empty;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00076BE8 File Offset: 0x00075FE8
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

		// Token: 0x06000E76 RID: 3702 RVA: 0x00076CE0 File Offset: 0x000760E0
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

		// Token: 0x06000E77 RID: 3703 RVA: 0x00076D28 File Offset: 0x00076128
		void IBindingListView.RemoveFilter()
		{
			Bid.Trace("<ds.DataView.RemoveFilter|API> %d#\n", this.ObjectID);
			this.RowFilter = "";
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00076D50 File Offset: 0x00076150
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x00076D64 File Offset: 0x00076164
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

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00076D78 File Offset: 0x00076178
		ListSortDescriptionCollection IBindingListView.SortDescriptions
		{
			get
			{
				return this.GetSortDescriptions();
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00076D8C File Offset: 0x0007618C
		internal ListSortDescriptionCollection GetSortDescriptions()
		{
			ListSortDescription[] array = new ListSortDescription[0];
			if (this.table != null && this.index != null && this.index.IndexFields.Length != 0)
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

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00076E44 File Offset: 0x00076244
		bool IBindingListView.SupportsAdvancedSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00076E54 File Offset: 0x00076254
		bool IBindingListView.SupportsFiltering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00076E64 File Offset: 0x00076264
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

		// Token: 0x06000E7F RID: 3711 RVA: 0x00076EBC File Offset: 0x000762BC
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

		// Token: 0x06000E80 RID: 3712 RVA: 0x00076F1C File Offset: 0x0007631C
		internal virtual IFilter GetFilter()
		{
			return this.rowFilter;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00076F30 File Offset: 0x00076330
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

		// Token: 0x06000E82 RID: 3714 RVA: 0x00076F74 File Offset: 0x00076374
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

		// Token: 0x06000E83 RID: 3715 RVA: 0x00076FC0 File Offset: 0x000763C0
		private DataRowView GetRowView(int record)
		{
			return this.GetRowView(this.table.recordManager[record]);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00076FE4 File Offset: 0x000763E4
		private DataRowView GetRowView(DataRow dr)
		{
			return this.rowViewCache[dr];
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00077000 File Offset: 0x00076400
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

		// Token: 0x06000E86 RID: 3718 RVA: 0x00077048 File Offset: 0x00076448
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

		// Token: 0x06000E87 RID: 3719 RVA: 0x000770A8 File Offset: 0x000764A8
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
					bool flag = this.rowViewBuffer.Remove(row);
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
				this.rowViewCache.Remove(row);
				return;
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

		// Token: 0x06000E88 RID: 3720 RVA: 0x0007719C File Offset: 0x0007659C
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

		// Token: 0x06000E89 RID: 3721 RVA: 0x000772C0 File Offset: 0x000766C0
		private void OnInitialized()
		{
			if (this.onInitialized != null)
			{
				this.onInitialized(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000772E8 File Offset: 0x000766E8
		protected void Open()
		{
			this.shouldOpen = true;
			this.UpdateIndex();
			this.dvListener.RegisterMetaDataEvents(this.table);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00077314 File Offset: 0x00076714
		protected void Reset()
		{
			if (this.IsOpen)
			{
				this.index.Reset();
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00077334 File Offset: 0x00076734
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

		// Token: 0x06000E8D RID: 3725 RVA: 0x000773DC File Offset: 0x000767DC
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

		// Token: 0x06000E8E RID: 3726 RVA: 0x000774B8 File Offset: 0x000768B8
		internal virtual void SetIndex(string newSort, DataViewRowState newRowStates, IFilter newRowFilter)
		{
			this.SetIndex2(newSort, newRowStates, newRowFilter, true);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000774D0 File Offset: 0x000768D0
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

		// Token: 0x06000E90 RID: 3728 RVA: 0x0007758C File Offset: 0x0007698C
		protected void UpdateIndex()
		{
			this.UpdateIndex(false);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000775A0 File Offset: 0x000769A0
		protected virtual void UpdateIndex(bool force)
		{
			this.UpdateIndex(force, true);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000775B8 File Offset: 0x000769B8
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
						DataTable dataTable = (this.index != null) ? this.index.Table : index.Table;
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

		// Token: 0x06000E93 RID: 3731 RVA: 0x000776F0 File Offset: 0x00076AF0
		internal void ChildRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0007775C File Offset: 0x00076B5C
		internal void ParentRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000777C8 File Offset: 0x00076BC8
		protected virtual void ColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataColumnPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataColumnPropertyDescriptor((DataColumn)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataColumnPropertyDescriptor((DataColumn)e.Element)) : null)));
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00077834 File Offset: 0x00076C34
		internal void ColumnCollectionChangedInternal(object sender, CollectionChangeEventArgs e)
		{
			this.ColumnCollectionChanged(sender, e);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0007784C File Offset: 0x00076C4C
		public DataTable ToTable()
		{
			return this.ToTable(null, false, new string[0]);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00077868 File Offset: 0x00076C68
		public DataTable ToTable(string tableName)
		{
			return this.ToTable(tableName, false, new string[0]);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00077884 File Offset: 0x00076C84
		public DataTable ToTable(bool distinct, params string[] columnNames)
		{
			return this.ToTable(null, distinct, columnNames);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0007789C File Offset: 0x00076C9C
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

		// Token: 0x06000E9B RID: 3739 RVA: 0x00077A84 File Offset: 0x00076E84
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

		// Token: 0x06000E9C RID: 3740 RVA: 0x00077AD0 File Offset: 0x00076ED0
		public virtual bool Equals(DataView view)
		{
			return view != null && this.Table == view.Table && this.Count == view.Count && string.Compare(this.RowFilter, view.RowFilter, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Sort, view.Sort, StringComparison.OrdinalIgnoreCase) == 0 && this.SortComparison == view.SortComparison && this.RowPredicate == view.RowPredicate && this.RowStateFilter == view.RowStateFilter && this.DataViewManager == view.DataViewManager && this.AllowDelete == view.AllowDelete && this.AllowNew == view.AllowNew && this.AllowEdit == view.AllowEdit;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00077B98 File Offset: 0x00076F98
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x04000418 RID: 1048
		private DataViewManager dataViewManager;

		// Token: 0x04000419 RID: 1049
		private DataTable table;

		// Token: 0x0400041A RID: 1050
		private bool locked;

		// Token: 0x0400041B RID: 1051
		private Index index;

		// Token: 0x0400041C RID: 1052
		private Dictionary<string, Index> findIndexes;

		// Token: 0x0400041D RID: 1053
		private string sort = "";

		// Token: 0x0400041E RID: 1054
		private Comparison<DataRow> _comparison;

		// Token: 0x0400041F RID: 1055
		private IFilter rowFilter;

		// Token: 0x04000420 RID: 1056
		private DataViewRowState recordStates = DataViewRowState.CurrentRows;

		// Token: 0x04000421 RID: 1057
		private bool shouldOpen = true;

		// Token: 0x04000422 RID: 1058
		private bool open;

		// Token: 0x04000423 RID: 1059
		private bool allowNew = true;

		// Token: 0x04000424 RID: 1060
		private bool allowEdit = true;

		// Token: 0x04000425 RID: 1061
		private bool allowDelete = true;

		// Token: 0x04000426 RID: 1062
		private bool applyDefaultSort;

		// Token: 0x04000427 RID: 1063
		internal DataRow addNewRow;

		// Token: 0x04000428 RID: 1064
		private ListChangedEventArgs addNewMoved;

		// Token: 0x04000429 RID: 1065
		private ListChangedEventHandler onListChanged;

		// Token: 0x0400042A RID: 1066
		private EventHandler onInitialized;

		// Token: 0x0400042B RID: 1067
		internal static ListChangedEventArgs ResetEventArgs = new ListChangedEventArgs(ListChangedType.Reset, -1);

		// Token: 0x0400042C RID: 1068
		private DataTable delayedTable;

		// Token: 0x0400042D RID: 1069
		private string delayedRowFilter;

		// Token: 0x0400042E RID: 1070
		private string delayedSort;

		// Token: 0x0400042F RID: 1071
		private DataViewRowState delayedRecordStates = (DataViewRowState)(-1);

		// Token: 0x04000430 RID: 1072
		private bool fInitInProgress;

		// Token: 0x04000431 RID: 1073
		private bool fEndInitInProgress;

		// Token: 0x04000432 RID: 1074
		private Dictionary<DataRow, DataRowView> rowViewCache = new Dictionary<DataRow, DataRowView>(DataView.DataRowReferenceComparer.Default);

		// Token: 0x04000433 RID: 1075
		private readonly Dictionary<DataRow, DataRowView> rowViewBuffer = new Dictionary<DataRow, DataRowView>(DataView.DataRowReferenceComparer.Default);

		// Token: 0x04000434 RID: 1076
		private DataViewListener dvListener;

		// Token: 0x04000435 RID: 1077
		private static int _objectTypeCount;

		// Token: 0x04000436 RID: 1078
		private readonly int _objectID = Interlocked.Increment(ref DataView._objectTypeCount);

		// Token: 0x0200034D RID: 845
		private sealed class DataRowReferenceComparer : IEqualityComparer<DataRow>
		{
			// Token: 0x06003404 RID: 13316 RVA: 0x0013FEBC File Offset: 0x0013F2BC
			private DataRowReferenceComparer()
			{
			}

			// Token: 0x06003405 RID: 13317 RVA: 0x0013FED0 File Offset: 0x0013F2D0
			public bool Equals(DataRow x, DataRow y)
			{
				return x == y;
			}

			// Token: 0x06003406 RID: 13318 RVA: 0x0013FEE4 File Offset: 0x0013F2E4
			public int GetHashCode(DataRow obj)
			{
				return obj.ObjectID;
			}

			// Token: 0x04001EBB RID: 7867
			internal static readonly DataView.DataRowReferenceComparer Default = new DataView.DataRowReferenceComparer();
		}

		// Token: 0x0200034E RID: 846
		private sealed class RowPredicateFilter : IFilter
		{
			// Token: 0x06003408 RID: 13320 RVA: 0x0013FF10 File Offset: 0x0013F310
			internal RowPredicateFilter(Predicate<DataRow> predicate)
			{
				this.PredicateFilter = predicate;
			}

			// Token: 0x06003409 RID: 13321 RVA: 0x0013FF2C File Offset: 0x0013F32C
			bool IFilter.Invoke(DataRow row, DataRowVersion version)
			{
				return this.PredicateFilter(row);
			}

			// Token: 0x04001EBC RID: 7868
			internal readonly Predicate<DataRow> PredicateFilter;
		}
	}
}

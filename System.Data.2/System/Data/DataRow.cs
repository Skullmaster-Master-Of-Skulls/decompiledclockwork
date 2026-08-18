using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000BD RID: 189
	public class DataRow
	{
		// Token: 0x06000AF1 RID: 2801 RVA: 0x00061288 File Offset: 0x00060688
		protected internal DataRow(DataRowBuilder builder)
		{
			this.tempRecord = builder._record;
			this._table = builder._table;
			this._columns = this._table.Columns;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x000612EC File Offset: 0x000606EC
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x00061304 File Offset: 0x00060704
		internal XmlBoundElement Element
		{
			get
			{
				return (XmlBoundElement)this._element;
			}
			set
			{
				this._element = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00061318 File Offset: 0x00060718
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x00061338 File Offset: 0x00060738
		internal DataColumn LastChangedColumn
		{
			get
			{
				if (this._countColumnChange != 1)
				{
					return null;
				}
				return this._lastChangedColumn;
			}
			set
			{
				this._countColumnChange++;
				this._lastChangedColumn = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0006135C File Offset: 0x0006075C
		internal bool HasPropertyChanged
		{
			get
			{
				return 0 < this._countColumnChange;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00061374 File Offset: 0x00060774
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x00061388 File Offset: 0x00060788
		internal int RBTreeNodeId
		{
			get
			{
				return this._rbTreeNodeId;
			}
			set
			{
				Bid.Trace("<ds.DataRow.set_RBTreeNodeId|INFO> %d#, value=%d\n", this.ObjectID, value);
				this._rbTreeNodeId = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x000613B0 File Offset: 0x000607B0
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x000613D8 File Offset: 0x000607D8
		public string RowError
		{
			get
			{
				if (this.error != null)
				{
					return this.error.Text;
				}
				return string.Empty;
			}
			set
			{
				Bid.Trace("<ds.DataRow.set_RowError|API> %d#, value='%ls'\n", this.ObjectID, value);
				if (this.error == null)
				{
					if (!ADP.IsEmpty(value))
					{
						this.error = new DataError(value);
					}
					this.RowErrorChanged();
					return;
				}
				if (this.error.Text != value)
				{
					this.error.Text = value;
					this.RowErrorChanged();
				}
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00061440 File Offset: 0x00060840
		private void RowErrorChanged()
		{
			if (this.oldRecord != -1)
			{
				this._table.RecordChanged(this.oldRecord);
			}
			if (this.newRecord != -1)
			{
				this._table.RecordChanged(this.newRecord);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00061484 File Offset: 0x00060884
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x00061498 File Offset: 0x00060898
		internal long rowID
		{
			get
			{
				return this._rowID;
			}
			set
			{
				this.ResetLastChangedColumn();
				this._rowID = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x000614B4 File Offset: 0x000608B4
		public DataRowState RowState
		{
			get
			{
				if (this.oldRecord == this.newRecord)
				{
					if (this.oldRecord == -1)
					{
						return DataRowState.Detached;
					}
					if (0 < this._columns.ColumnsImplementingIChangeTrackingCount)
					{
						foreach (DataColumn column in this._columns.ColumnsImplementingIChangeTracking)
						{
							object obj = this[column];
							if (DBNull.Value != obj && ((IChangeTracking)obj).IsChanged)
							{
								return DataRowState.Modified;
							}
						}
					}
					return DataRowState.Unchanged;
				}
				else
				{
					if (this.oldRecord == -1)
					{
						return DataRowState.Added;
					}
					if (this.newRecord == -1)
					{
						return DataRowState.Deleted;
					}
					return DataRowState.Modified;
				}
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00061540 File Offset: 0x00060940
		public DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000194 RID: 404
		public object this[int columnIndex]
		{
			get
			{
				DataColumn dataColumn = this._columns[columnIndex];
				int defaultRecord = this.GetDefaultRecord();
				return dataColumn[defaultRecord];
			}
			set
			{
				DataColumn column = this._columns[columnIndex];
				this[column] = value;
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000615A0 File Offset: 0x000609A0
		internal void CheckForLoops(DataRelation rel)
		{
			if (this._table.fInLoadDiffgram || (this._table.DataSet != null && this._table.DataSet.fInLoadDiffgram))
			{
				return;
			}
			int count = this._table.Rows.Count;
			int num = 0;
			for (DataRow parentRow = this.GetParentRow(rel); parentRow != null; parentRow = parentRow.GetParentRow(rel))
			{
				if (parentRow == this || num > count)
				{
					throw ExceptionBuilder.NestedCircular(this._table.TableName);
				}
				num++;
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00061620 File Offset: 0x00060A20
		internal int GetNestedParentCount()
		{
			int num = 0;
			DataRelation[] nestedParentRelations = this._table.NestedParentRelations;
			foreach (DataRelation dataRelation in nestedParentRelations)
			{
				if (dataRelation != null)
				{
					if (dataRelation.ParentTable == this._table)
					{
						this.CheckForLoops(dataRelation);
					}
					DataRow parentRow = this.GetParentRow(dataRelation);
					if (parentRow != null)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x17000195 RID: 405
		public object this[string columnName]
		{
			get
			{
				DataColumn dataColumn = this.GetDataColumn(columnName);
				int defaultRecord = this.GetDefaultRecord();
				return dataColumn[defaultRecord];
			}
			set
			{
				DataColumn dataColumn = this.GetDataColumn(columnName);
				this[dataColumn] = value;
			}
		}

		// Token: 0x17000196 RID: 406
		public object this[DataColumn column]
		{
			get
			{
				this.CheckColumn(column);
				int defaultRecord = this.GetDefaultRecord();
				return column[defaultRecord];
			}
			set
			{
				this.CheckColumn(column);
				if (this.inChangingEvent)
				{
					throw ExceptionBuilder.EditInRowChanging();
				}
				if (-1L != this.rowID && column.ReadOnly)
				{
					throw ExceptionBuilder.ReadOnly(column.ColumnName);
				}
				DataColumnChangeEventArgs dataColumnChangeEventArgs = null;
				if (this._table.NeedColumnChangeEvents)
				{
					dataColumnChangeEventArgs = new DataColumnChangeEventArgs(this, column, value);
					this._table.OnColumnChanging(dataColumnChangeEventArgs);
				}
				if (column.Table != this._table)
				{
					throw ExceptionBuilder.ColumnNotInTheTable(column.ColumnName, this._table.TableName);
				}
				if (-1L != this.rowID && column.ReadOnly)
				{
					throw ExceptionBuilder.ReadOnly(column.ColumnName);
				}
				object obj = (dataColumnChangeEventArgs != null) ? dataColumnChangeEventArgs.ProposedValue : value;
				if (obj == null)
				{
					if (column.IsValueType)
					{
						throw ExceptionBuilder.CannotSetToNull(column);
					}
					obj = DBNull.Value;
				}
				bool flag = this.BeginEditInternal();
				try
				{
					int proposedRecordNo = this.GetProposedRecordNo();
					column[proposedRecordNo] = obj;
				}
				catch (Exception e)
				{
					if (ADP.IsCatchableOrSecurityExceptionType(e) && flag)
					{
						this.CancelEdit();
					}
					throw;
				}
				this.LastChangedColumn = column;
				if (dataColumnChangeEventArgs != null)
				{
					this._table.OnColumnChanged(dataColumnChangeEventArgs);
				}
				if (flag)
				{
					this.EndEdit();
				}
			}
		}

		// Token: 0x17000197 RID: 407
		public object this[int columnIndex, DataRowVersion version]
		{
			get
			{
				DataColumn dataColumn = this._columns[columnIndex];
				int recordFromVersion = this.GetRecordFromVersion(version);
				return dataColumn[recordFromVersion];
			}
		}

		// Token: 0x17000198 RID: 408
		public object this[string columnName, DataRowVersion version]
		{
			get
			{
				DataColumn dataColumn = this.GetDataColumn(columnName);
				int recordFromVersion = this.GetRecordFromVersion(version);
				return dataColumn[recordFromVersion];
			}
		}

		// Token: 0x17000199 RID: 409
		public object this[DataColumn column, DataRowVersion version]
		{
			get
			{
				this.CheckColumn(column);
				int recordFromVersion = this.GetRecordFromVersion(version);
				return column[recordFromVersion];
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00061890 File Offset: 0x00060C90
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x000618DC File Offset: 0x00060CDC
		public object[] ItemArray
		{
			get
			{
				int defaultRecord = this.GetDefaultRecord();
				object[] array = new object[this._columns.Count];
				for (int i = 0; i < array.Length; i++)
				{
					DataColumn dataColumn = this._columns[i];
					array[i] = dataColumn[defaultRecord];
				}
				return array;
			}
			set
			{
				if (value == null)
				{
					throw ExceptionBuilder.ArgumentNull("ItemArray");
				}
				if (this._columns.Count < value.Length)
				{
					throw ExceptionBuilder.ValueArrayLength();
				}
				DataColumnChangeEventArgs dataColumnChangeEventArgs = null;
				if (this._table.NeedColumnChangeEvents)
				{
					dataColumnChangeEventArgs = new DataColumnChangeEventArgs(this);
				}
				bool flag = this.BeginEditInternal();
				for (int i = 0; i < value.Length; i++)
				{
					if (value[i] != null)
					{
						DataColumn dataColumn = this._columns[i];
						if (-1L != this.rowID && dataColumn.ReadOnly)
						{
							throw ExceptionBuilder.ReadOnly(dataColumn.ColumnName);
						}
						if (dataColumnChangeEventArgs != null)
						{
							dataColumnChangeEventArgs.InitializeColumnChangeEvent(dataColumn, value[i]);
							this._table.OnColumnChanging(dataColumnChangeEventArgs);
						}
						if (dataColumn.Table != this._table)
						{
							throw ExceptionBuilder.ColumnNotInTheTable(dataColumn.ColumnName, this._table.TableName);
						}
						if (-1L != this.rowID && dataColumn.ReadOnly)
						{
							throw ExceptionBuilder.ReadOnly(dataColumn.ColumnName);
						}
						if (this.tempRecord == -1)
						{
							this.BeginEditInternal();
						}
						object obj = (dataColumnChangeEventArgs != null) ? dataColumnChangeEventArgs.ProposedValue : value[i];
						if (obj == null)
						{
							if (dataColumn.IsValueType)
							{
								throw ExceptionBuilder.CannotSetToNull(dataColumn);
							}
							obj = DBNull.Value;
						}
						try
						{
							int proposedRecordNo = this.GetProposedRecordNo();
							dataColumn[proposedRecordNo] = obj;
						}
						catch (Exception e)
						{
							if (ADP.IsCatchableOrSecurityExceptionType(e) && flag)
							{
								this.CancelEdit();
							}
							throw;
						}
						this.LastChangedColumn = dataColumn;
						if (dataColumnChangeEventArgs != null)
						{
							this._table.OnColumnChanged(dataColumnChangeEventArgs);
						}
					}
				}
				this.EndEdit();
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00061A6C File Offset: 0x00060E6C
		public void AcceptChanges()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataRow.AcceptChanges|API> %d#\n", this.ObjectID);
			try
			{
				this.EndEdit();
				if (this.RowState != DataRowState.Detached && this.RowState != DataRowState.Deleted && this._columns.ColumnsImplementingIChangeTrackingCount > 0)
				{
					foreach (DataColumn column in this._columns.ColumnsImplementingIChangeTracking)
					{
						object obj = this[column];
						if (DBNull.Value != obj)
						{
							IChangeTracking changeTracking = (IChangeTracking)obj;
							if (changeTracking.IsChanged)
							{
								changeTracking.AcceptChanges();
							}
						}
					}
				}
				this._table.CommitRow(this);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00061B28 File Offset: 0x00060F28
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void BeginEdit()
		{
			this.BeginEditInternal();
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00061B3C File Offset: 0x00060F3C
		private bool BeginEditInternal()
		{
			if (this.inChangingEvent)
			{
				throw ExceptionBuilder.BeginEditInRowChanging();
			}
			if (this.tempRecord != -1)
			{
				if (this.tempRecord < this._table.recordManager.LastFreeRecord)
				{
					return false;
				}
				this.tempRecord = -1;
			}
			if (this.oldRecord != -1 && this.newRecord == -1)
			{
				throw ExceptionBuilder.DeletedRowInaccessible();
			}
			this.ResetLastChangedColumn();
			this.tempRecord = this._table.NewRecord(this.newRecord);
			return true;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00061BB8 File Offset: 0x00060FB8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void CancelEdit()
		{
			if (this.inChangingEvent)
			{
				throw ExceptionBuilder.CancelEditInRowChanging();
			}
			this._table.FreeRecord(ref this.tempRecord);
			this.ResetLastChangedColumn();
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00061BEC File Offset: 0x00060FEC
		private void CheckColumn(DataColumn column)
		{
			if (column == null)
			{
				throw ExceptionBuilder.ArgumentNull("column");
			}
			if (column.Table != this._table)
			{
				throw ExceptionBuilder.ColumnNotInTheTable(column.ColumnName, this._table.TableName);
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00061C2C File Offset: 0x0006102C
		internal void CheckInTable()
		{
			if (this.rowID == -1L)
			{
				throw ExceptionBuilder.RowNotInTheTable();
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00061C4C File Offset: 0x0006104C
		public void Delete()
		{
			if (this.inDeletingEvent)
			{
				throw ExceptionBuilder.DeleteInRowDeleting();
			}
			if (this.newRecord == -1)
			{
				return;
			}
			this._table.DeleteRow(this);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00061C80 File Offset: 0x00061080
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndEdit()
		{
			if (this.inChangingEvent)
			{
				throw ExceptionBuilder.EndEditInRowChanging();
			}
			if (this.newRecord == -1)
			{
				return;
			}
			if (this.tempRecord != -1)
			{
				try
				{
					this._table.SetNewRecord(this, this.tempRecord, DataRowAction.Change, false, true, true);
				}
				finally
				{
					this.ResetLastChangedColumn();
				}
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00061CEC File Offset: 0x000610EC
		public void SetColumnError(int columnIndex, string error)
		{
			DataColumn dataColumn = this._columns[columnIndex];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.ColumnOutOfRange(columnIndex);
			}
			this.SetColumnError(dataColumn, error);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00061D18 File Offset: 0x00061118
		public void SetColumnError(string columnName, string error)
		{
			DataColumn dataColumn = this.GetDataColumn(columnName);
			this.SetColumnError(dataColumn, error);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00061D38 File Offset: 0x00061138
		public void SetColumnError(DataColumn column, string error)
		{
			this.CheckColumn(column);
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataRow.SetColumnError|API> %d#, column=%d, error='%ls'\n", this.ObjectID, column.ObjectID, error);
			try
			{
				if (this.error == null)
				{
					this.error = new DataError();
				}
				if (this.GetColumnError(column) != error)
				{
					this.error.SetColumnError(column, error);
					this.RowErrorChanged();
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00061DC0 File Offset: 0x000611C0
		public string GetColumnError(int columnIndex)
		{
			DataColumn column = this._columns[columnIndex];
			return this.GetColumnError(column);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00061DE4 File Offset: 0x000611E4
		public string GetColumnError(string columnName)
		{
			DataColumn dataColumn = this.GetDataColumn(columnName);
			return this.GetColumnError(dataColumn);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00061E00 File Offset: 0x00061200
		public string GetColumnError(DataColumn column)
		{
			this.CheckColumn(column);
			if (this.error == null)
			{
				this.error = new DataError();
			}
			return this.error.GetColumnError(column);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00061E34 File Offset: 0x00061234
		public void ClearErrors()
		{
			if (this.error != null)
			{
				this.error.Clear();
				this.RowErrorChanged();
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00061E5C File Offset: 0x0006125C
		internal void ClearError(DataColumn column)
		{
			if (this.error != null)
			{
				this.error.Clear(column);
				this.RowErrorChanged();
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00061E84 File Offset: 0x00061284
		public bool HasErrors
		{
			get
			{
				return this.error != null && this.error.HasErrors;
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00061EA8 File Offset: 0x000612A8
		public DataColumn[] GetColumnsInError()
		{
			if (this.error == null)
			{
				return DataTable.zeroColumns;
			}
			return this.error.GetColumnsInError();
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00061ED0 File Offset: 0x000612D0
		public DataRow[] GetChildRows(string relationName)
		{
			return this.GetChildRows(this._table.ChildRelations[relationName], DataRowVersion.Default);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00061EFC File Offset: 0x000612FC
		public DataRow[] GetChildRows(string relationName, DataRowVersion version)
		{
			return this.GetChildRows(this._table.ChildRelations[relationName], version);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00061F24 File Offset: 0x00061324
		public DataRow[] GetChildRows(DataRelation relation)
		{
			return this.GetChildRows(relation, DataRowVersion.Default);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00061F40 File Offset: 0x00061340
		public DataRow[] GetChildRows(DataRelation relation, DataRowVersion version)
		{
			if (relation == null)
			{
				return this._table.NewRowArray(0);
			}
			if (relation.DataSet != this._table.DataSet)
			{
				throw ExceptionBuilder.RowNotInTheDataSet();
			}
			if (relation.ParentKey.Table != this._table)
			{
				throw ExceptionBuilder.RelationForeignTable(relation.ParentTable.TableName, this._table.TableName);
			}
			return DataRelation.GetChildRows(relation.ParentKey, relation.ChildKey, this, version);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00061FBC File Offset: 0x000613BC
		internal DataColumn GetDataColumn(string columnName)
		{
			DataColumn dataColumn = this._columns[columnName];
			if (dataColumn != null)
			{
				return dataColumn;
			}
			throw ExceptionBuilder.ColumnNotInTheTable(columnName, this._table.TableName);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00061FEC File Offset: 0x000613EC
		public DataRow GetParentRow(string relationName)
		{
			return this.GetParentRow(this._table.ParentRelations[relationName], DataRowVersion.Default);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00062018 File Offset: 0x00061418
		public DataRow GetParentRow(string relationName, DataRowVersion version)
		{
			return this.GetParentRow(this._table.ParentRelations[relationName], version);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00062040 File Offset: 0x00061440
		public DataRow GetParentRow(DataRelation relation)
		{
			return this.GetParentRow(relation, DataRowVersion.Default);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0006205C File Offset: 0x0006145C
		public DataRow GetParentRow(DataRelation relation, DataRowVersion version)
		{
			if (relation == null)
			{
				return null;
			}
			if (relation.DataSet != this._table.DataSet)
			{
				throw ExceptionBuilder.RelationForeignRow();
			}
			if (relation.ChildKey.Table != this._table)
			{
				throw ExceptionBuilder.GetParentRowTableMismatch(relation.ChildTable.TableName, this._table.TableName);
			}
			return DataRelation.GetParentRow(relation.ParentKey, relation.ChildKey, this, version);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000620CC File Offset: 0x000614CC
		internal DataRow GetNestedParentRow(DataRowVersion version)
		{
			DataRelation[] nestedParentRelations = this._table.NestedParentRelations;
			foreach (DataRelation dataRelation in nestedParentRelations)
			{
				if (dataRelation != null)
				{
					if (dataRelation.ParentTable == this._table)
					{
						this.CheckForLoops(dataRelation);
					}
					DataRow parentRow = this.GetParentRow(dataRelation, version);
					if (parentRow != null)
					{
						return parentRow;
					}
				}
			}
			return null;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00062124 File Offset: 0x00061524
		public DataRow[] GetParentRows(string relationName)
		{
			return this.GetParentRows(this._table.ParentRelations[relationName], DataRowVersion.Default);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00062150 File Offset: 0x00061550
		public DataRow[] GetParentRows(string relationName, DataRowVersion version)
		{
			return this.GetParentRows(this._table.ParentRelations[relationName], version);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00062178 File Offset: 0x00061578
		public DataRow[] GetParentRows(DataRelation relation)
		{
			return this.GetParentRows(relation, DataRowVersion.Default);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00062194 File Offset: 0x00061594
		public DataRow[] GetParentRows(DataRelation relation, DataRowVersion version)
		{
			if (relation == null)
			{
				return this._table.NewRowArray(0);
			}
			if (relation.DataSet != this._table.DataSet)
			{
				throw ExceptionBuilder.RowNotInTheDataSet();
			}
			if (relation.ChildKey.Table != this._table)
			{
				throw ExceptionBuilder.GetParentRowTableMismatch(relation.ChildTable.TableName, this._table.TableName);
			}
			return DataRelation.GetParentRows(relation.ParentKey, relation.ChildKey, this, version);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00062210 File Offset: 0x00061610
		internal object[] GetColumnValues(DataColumn[] columns)
		{
			return this.GetColumnValues(columns, DataRowVersion.Default);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0006222C File Offset: 0x0006162C
		internal object[] GetColumnValues(DataColumn[] columns, DataRowVersion version)
		{
			DataKey key = new DataKey(columns, false);
			return this.GetKeyValues(key, version);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0006224C File Offset: 0x0006164C
		internal object[] GetKeyValues(DataKey key)
		{
			int defaultRecord = this.GetDefaultRecord();
			return key.GetKeyValues(defaultRecord);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00062268 File Offset: 0x00061668
		internal object[] GetKeyValues(DataKey key, DataRowVersion version)
		{
			int recordFromVersion = this.GetRecordFromVersion(version);
			return key.GetKeyValues(recordFromVersion);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00062288 File Offset: 0x00061688
		internal int GetCurrentRecordNo()
		{
			if (this.newRecord == -1)
			{
				throw ExceptionBuilder.NoCurrentData();
			}
			return this.newRecord;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000622AC File Offset: 0x000616AC
		internal int GetDefaultRecord()
		{
			if (this.tempRecord != -1)
			{
				return this.tempRecord;
			}
			if (this.newRecord != -1)
			{
				return this.newRecord;
			}
			if (this.oldRecord == -1)
			{
				throw ExceptionBuilder.RowRemovedFromTheTable();
			}
			throw ExceptionBuilder.DeletedRowInaccessible();
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000622F0 File Offset: 0x000616F0
		internal int GetOriginalRecordNo()
		{
			if (this.oldRecord == -1)
			{
				throw ExceptionBuilder.NoOriginalData();
			}
			return this.oldRecord;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00062314 File Offset: 0x00061714
		private int GetProposedRecordNo()
		{
			if (this.tempRecord == -1)
			{
				throw ExceptionBuilder.NoProposedData();
			}
			return this.tempRecord;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00062338 File Offset: 0x00061738
		internal int GetRecordFromVersion(DataRowVersion version)
		{
			if (version <= DataRowVersion.Current)
			{
				if (version == DataRowVersion.Original)
				{
					return this.GetOriginalRecordNo();
				}
				if (version == DataRowVersion.Current)
				{
					return this.GetCurrentRecordNo();
				}
			}
			else
			{
				if (version == DataRowVersion.Proposed)
				{
					return this.GetProposedRecordNo();
				}
				if (version == DataRowVersion.Default)
				{
					return this.GetDefaultRecord();
				}
			}
			throw ExceptionBuilder.InvalidRowVersion();
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00062394 File Offset: 0x00061794
		internal DataRowVersion GetDefaultRowVersion(DataViewRowState viewState)
		{
			if (this.oldRecord == this.newRecord)
			{
				int num = this.oldRecord;
				return DataRowVersion.Default;
			}
			if (this.oldRecord == -1)
			{
				return DataRowVersion.Default;
			}
			if (this.newRecord == -1)
			{
				return DataRowVersion.Original;
			}
			if ((DataViewRowState.ModifiedCurrent & viewState) != DataViewRowState.None)
			{
				return DataRowVersion.Default;
			}
			return DataRowVersion.Original;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000623F0 File Offset: 0x000617F0
		internal DataViewRowState GetRecordState(int record)
		{
			if (record == -1)
			{
				return DataViewRowState.None;
			}
			if (record == this.oldRecord && record == this.newRecord)
			{
				return DataViewRowState.Unchanged;
			}
			if (record == this.oldRecord)
			{
				if (this.newRecord == -1)
				{
					return DataViewRowState.Deleted;
				}
				return DataViewRowState.ModifiedOriginal;
			}
			else
			{
				if (record != this.newRecord)
				{
					return DataViewRowState.None;
				}
				if (this.oldRecord == -1)
				{
					return DataViewRowState.Added;
				}
				return DataViewRowState.ModifiedCurrent;
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00062448 File Offset: 0x00061848
		internal bool HasKeyChanged(DataKey key)
		{
			return this.HasKeyChanged(key, DataRowVersion.Current, DataRowVersion.Proposed);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00062468 File Offset: 0x00061868
		internal bool HasKeyChanged(DataKey key, DataRowVersion version1, DataRowVersion version2)
		{
			return !this.HasVersion(version1) || !this.HasVersion(version2) || !key.RecordsEqual(this.GetRecordFromVersion(version1), this.GetRecordFromVersion(version2));
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000624A4 File Offset: 0x000618A4
		public bool HasVersion(DataRowVersion version)
		{
			if (version <= DataRowVersion.Current)
			{
				if (version == DataRowVersion.Original)
				{
					return this.oldRecord != -1;
				}
				if (version == DataRowVersion.Current)
				{
					return this.newRecord != -1;
				}
			}
			else
			{
				if (version == DataRowVersion.Proposed)
				{
					return this.tempRecord != -1;
				}
				if (version == DataRowVersion.Default)
				{
					return this.tempRecord != -1 || this.newRecord != -1;
				}
			}
			throw ExceptionBuilder.InvalidRowVersion();
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00062524 File Offset: 0x00061924
		internal bool HasChanges()
		{
			if (!this.HasVersion(DataRowVersion.Original) || !this.HasVersion(DataRowVersion.Current))
			{
				return true;
			}
			foreach (object obj in this.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.Compare(this.oldRecord, this.newRecord) != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000625C0 File Offset: 0x000619C0
		internal bool HaveValuesChanged(DataColumn[] columns)
		{
			return this.HaveValuesChanged(columns, DataRowVersion.Current, DataRowVersion.Proposed);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000625E0 File Offset: 0x000619E0
		internal bool HaveValuesChanged(DataColumn[] columns, DataRowVersion version1, DataRowVersion version2)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				this.CheckColumn(columns[i]);
			}
			DataKey key = new DataKey(columns, false);
			return this.HasKeyChanged(key, version1, version2);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00062618 File Offset: 0x00061A18
		public bool IsNull(int columnIndex)
		{
			DataColumn dataColumn = this._columns[columnIndex];
			int defaultRecord = this.GetDefaultRecord();
			return dataColumn.IsNull(defaultRecord);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00062640 File Offset: 0x00061A40
		public bool IsNull(string columnName)
		{
			DataColumn dataColumn = this.GetDataColumn(columnName);
			int defaultRecord = this.GetDefaultRecord();
			return dataColumn.IsNull(defaultRecord);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00062664 File Offset: 0x00061A64
		public bool IsNull(DataColumn column)
		{
			this.CheckColumn(column);
			int defaultRecord = this.GetDefaultRecord();
			return column.IsNull(defaultRecord);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00062688 File Offset: 0x00061A88
		public bool IsNull(DataColumn column, DataRowVersion version)
		{
			this.CheckColumn(column);
			int recordFromVersion = this.GetRecordFromVersion(version);
			return column.IsNull(recordFromVersion);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000626AC File Offset: 0x00061AAC
		public void RejectChanges()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataRow.RejectChanges|API> %d#\n", this.ObjectID);
			try
			{
				if (this.RowState != DataRowState.Detached)
				{
					if (this._columns.ColumnsImplementingIChangeTrackingCount != this._columns.ColumnsImplementingIRevertibleChangeTrackingCount)
					{
						foreach (DataColumn dataColumn in this._columns.ColumnsImplementingIChangeTracking)
						{
							if (!dataColumn.ImplementsIRevertibleChangeTracking)
							{
								object obj;
								if (this.RowState != DataRowState.Deleted)
								{
									obj = this[dataColumn];
								}
								else
								{
									obj = this[dataColumn, DataRowVersion.Original];
								}
								if (DBNull.Value != obj && ((IChangeTracking)obj).IsChanged)
								{
									throw ExceptionBuilder.UDTImplementsIChangeTrackingButnotIRevertible(dataColumn.DataType.AssemblyQualifiedName);
								}
							}
						}
					}
					foreach (DataColumn column in this._columns.ColumnsImplementingIChangeTracking)
					{
						object obj2;
						if (this.RowState != DataRowState.Deleted)
						{
							obj2 = this[column];
						}
						else
						{
							obj2 = this[column, DataRowVersion.Original];
						}
						if (DBNull.Value != obj2)
						{
							IChangeTracking changeTracking = (IChangeTracking)obj2;
							if (changeTracking.IsChanged)
							{
								((IRevertibleChangeTracking)obj2).RejectChanges();
							}
						}
					}
				}
				this._table.RollbackRow(this);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00062804 File Offset: 0x00061C04
		internal void ResetLastChangedColumn()
		{
			this._lastChangedColumn = null;
			this._countColumnChange = 0;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00062820 File Offset: 0x00061C20
		internal void SetKeyValues(DataKey key, object[] keyValues)
		{
			bool flag = true;
			bool flag2 = this.tempRecord == -1;
			for (int i = 0; i < keyValues.Length; i++)
			{
				object obj = this[key.ColumnsReference[i]];
				if (!obj.Equals(keyValues[i]))
				{
					if (flag2 && flag)
					{
						flag = false;
						this.BeginEditInternal();
					}
					this[key.ColumnsReference[i]] = keyValues[i];
				}
			}
			if (!flag)
			{
				this.EndEdit();
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0006288C File Offset: 0x00061C8C
		protected void SetNull(DataColumn column)
		{
			this[column] = DBNull.Value;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000628A8 File Offset: 0x00061CA8
		internal void SetNestedParentRow(DataRow parentRow, bool setNonNested)
		{
			if (parentRow == null)
			{
				this.SetParentRowToDBNull();
				return;
			}
			foreach (object obj in this._table.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if ((dataRelation.Nested || setNonNested) && dataRelation.ParentKey.Table == parentRow._table)
				{
					object[] keyValues = parentRow.GetKeyValues(dataRelation.ParentKey);
					this.SetKeyValues(dataRelation.ChildKey, keyValues);
					if (dataRelation.Nested)
					{
						if (parentRow._table == this._table)
						{
							this.CheckForLoops(dataRelation);
						}
						else
						{
							this.GetParentRow(dataRelation);
						}
					}
				}
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00062978 File Offset: 0x00061D78
		public void SetParentRow(DataRow parentRow)
		{
			this.SetNestedParentRow(parentRow, true);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00062990 File Offset: 0x00061D90
		public void SetParentRow(DataRow parentRow, DataRelation relation)
		{
			if (relation == null)
			{
				this.SetParentRow(parentRow);
				return;
			}
			if (parentRow == null)
			{
				this.SetParentRowToDBNull(relation);
				return;
			}
			if (this._table.DataSet != parentRow._table.DataSet)
			{
				throw ExceptionBuilder.ParentRowNotInTheDataSet();
			}
			if (relation.ChildKey.Table != this._table)
			{
				throw ExceptionBuilder.SetParentRowTableMismatch(relation.ChildKey.Table.TableName, this._table.TableName);
			}
			if (relation.ParentKey.Table != parentRow._table)
			{
				throw ExceptionBuilder.SetParentRowTableMismatch(relation.ParentKey.Table.TableName, parentRow._table.TableName);
			}
			object[] keyValues = parentRow.GetKeyValues(relation.ParentKey);
			this.SetKeyValues(relation.ChildKey, keyValues);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00062A60 File Offset: 0x00061E60
		internal void SetParentRowToDBNull()
		{
			foreach (object obj in this._table.ParentRelations)
			{
				DataRelation parentRowToDBNull = (DataRelation)obj;
				this.SetParentRowToDBNull(parentRowToDBNull);
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00062ACC File Offset: 0x00061ECC
		internal void SetParentRowToDBNull(DataRelation relation)
		{
			if (relation.ChildKey.Table != this._table)
			{
				throw ExceptionBuilder.SetParentRowTableMismatch(relation.ChildKey.Table.TableName, this._table.TableName);
			}
			object[] keyValues = new object[]
			{
				DBNull.Value
			};
			this.SetKeyValues(relation.ChildKey, keyValues);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00062B30 File Offset: 0x00061F30
		public void SetAdded()
		{
			if (this.RowState == DataRowState.Unchanged)
			{
				this._table.SetOldRecord(this, -1);
				return;
			}
			throw ExceptionBuilder.SetAddedAndModifiedCalledOnnonUnchanged();
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00062B5C File Offset: 0x00061F5C
		public void SetModified()
		{
			if (this.RowState != DataRowState.Unchanged)
			{
				throw ExceptionBuilder.SetAddedAndModifiedCalledOnnonUnchanged();
			}
			this.tempRecord = this._table.NewRecord(this.newRecord);
			if (this.tempRecord != -1)
			{
				this._table.SetNewRecord(this, this.tempRecord, DataRowAction.Change, false, true, true);
				return;
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00062BB0 File Offset: 0x00061FB0
		internal int CopyValuesIntoStore(ArrayList storeList, ArrayList nullbitList, int storeIndex)
		{
			int num = 0;
			if (this.oldRecord != -1)
			{
				for (int i = 0; i < this._columns.Count; i++)
				{
					this._columns[i].CopyValueIntoStore(this.oldRecord, storeList[i], (BitArray)nullbitList[i], storeIndex);
				}
				num++;
				storeIndex++;
			}
			DataRowState rowState = this.RowState;
			if (DataRowState.Added == rowState || DataRowState.Modified == rowState)
			{
				for (int j = 0; j < this._columns.Count; j++)
				{
					this._columns[j].CopyValueIntoStore(this.newRecord, storeList[j], (BitArray)nullbitList[j], storeIndex);
				}
				num++;
				storeIndex++;
			}
			if (-1 != this.tempRecord)
			{
				for (int k = 0; k < this._columns.Count; k++)
				{
					this._columns[k].CopyValueIntoStore(this.tempRecord, storeList[k], (BitArray)nullbitList[k], storeIndex);
				}
				num++;
				storeIndex++;
			}
			return num;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00062CC4 File Offset: 0x000620C4
		[Conditional("DEBUG")]
		private void VerifyValueFromStorage(DataColumn column, DataRowVersion version, object valueFromStorage)
		{
			if (column.DataExpression != null && !this.inChangingEvent && this.tempRecord == -1 && this.newRecord != -1 && version == DataRowVersion.Original && this.oldRecord == this.newRecord)
			{
				version = DataRowVersion.Current;
			}
		}

		// Token: 0x04000347 RID: 839
		private readonly DataTable _table;

		// Token: 0x04000348 RID: 840
		private readonly DataColumnCollection _columns;

		// Token: 0x04000349 RID: 841
		internal int oldRecord = -1;

		// Token: 0x0400034A RID: 842
		internal int newRecord = -1;

		// Token: 0x0400034B RID: 843
		internal int tempRecord;

		// Token: 0x0400034C RID: 844
		internal long _rowID = -1L;

		// Token: 0x0400034D RID: 845
		internal DataRowAction _action;

		// Token: 0x0400034E RID: 846
		internal bool inChangingEvent;

		// Token: 0x0400034F RID: 847
		internal bool inDeletingEvent;

		// Token: 0x04000350 RID: 848
		internal bool inCascade;

		// Token: 0x04000351 RID: 849
		private DataColumn _lastChangedColumn;

		// Token: 0x04000352 RID: 850
		private int _countColumnChange;

		// Token: 0x04000353 RID: 851
		private DataError error;

		// Token: 0x04000354 RID: 852
		private object _element;

		// Token: 0x04000355 RID: 853
		private int _rbTreeNodeId;

		// Token: 0x04000356 RID: 854
		private static int _objectTypeCount;

		// Token: 0x04000357 RID: 855
		internal readonly int ObjectID = Interlocked.Increment(ref DataRow._objectTypeCount);
	}
}

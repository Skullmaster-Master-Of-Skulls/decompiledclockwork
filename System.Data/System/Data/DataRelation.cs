using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace System.Data
{
	// Token: 0x0200007C RID: 124
	[TypeConverter(typeof(RelationshipConverter))]
	[Editor("Microsoft.VSDesigner.Data.Design.DataRelationEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("RelationName")]
	public class DataRelation
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x001F0FA8 File Offset: 0x001F03A8
		public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn) : this(relationName, parentColumn, childColumn, true)
		{
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x001F0FC8 File Offset: 0x001F03C8
		public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn, bool createConstraints)
		{
			this.relationName = "";
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation._objectTypeCount);
			base..ctor();
			Bid.Trace("<ds.DataRelation.DataRelation|API> %d#, relationName='%ls', parentColumn=%d, childColumn=%d, createConstraints=%d{bool}\n", this.ObjectID, relationName, (parentColumn != null) ? parentColumn.ObjectID : 0, (childColumn != null) ? childColumn.ObjectID : 0, createConstraints);
			this.Create(relationName, new DataColumn[]
			{
				parentColumn
			}, new DataColumn[]
			{
				childColumn
			}, createConstraints);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x001F1058 File Offset: 0x001F0458
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns) : this(relationName, parentColumns, childColumns, true)
		{
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x001F1078 File Offset: 0x001F0478
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			this.relationName = "";
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation._objectTypeCount);
			base..ctor();
			this.Create(relationName, parentColumns, childColumns, createConstraints);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x001F10B8 File Offset: 0x001F04B8
		[Browsable(false)]
		public DataRelation(string relationName, string parentTableName, string childTableName, string[] parentColumnNames, string[] childColumnNames, bool nested)
		{
			this.relationName = "";
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation._objectTypeCount);
			base..ctor();
			this.relationName = relationName;
			this.parentColumnNames = parentColumnNames;
			this.childColumnNames = childColumnNames;
			this.parentTableName = parentTableName;
			this.childTableName = childTableName;
			this.nested = nested;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x001F1128 File Offset: 0x001F0528
		[Browsable(false)]
		public DataRelation(string relationName, string parentTableName, string parentTableNamespace, string childTableName, string childTableNamespace, string[] parentColumnNames, string[] childColumnNames, bool nested)
		{
			this.relationName = "";
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation._objectTypeCount);
			base..ctor();
			this.relationName = relationName;
			this.parentColumnNames = parentColumnNames;
			this.childColumnNames = childColumnNames;
			this.parentTableName = parentTableName;
			this.childTableName = childTableName;
			this.parentTableNamespace = parentTableNamespace;
			this.childTableNamespace = childTableNamespace;
			this.nested = nested;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x001F11A8 File Offset: 0x001F05A8
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataRelationChildColumnsDescr")]
		public virtual DataColumn[] ChildColumns
		{
			get
			{
				this.CheckStateForProperty();
				return this.childKey.ToArray();
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x001F11C8 File Offset: 0x001F05C8
		internal DataColumn[] ChildColumnsReference
		{
			get
			{
				this.CheckStateForProperty();
				return this.childKey.ColumnsReference;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x001F11E8 File Offset: 0x001F05E8
		internal DataKey ChildKey
		{
			get
			{
				this.CheckStateForProperty();
				return this.childKey;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x001F1208 File Offset: 0x001F0608
		public virtual DataTable ChildTable
		{
			get
			{
				this.CheckStateForProperty();
				return this.childKey.Table;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x001F1228 File Offset: 0x001F0628
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual DataSet DataSet
		{
			get
			{
				this.CheckStateForProperty();
				return this.dataSet;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x001F1248 File Offset: 0x001F0648
		internal string[] ParentColumnNames
		{
			get
			{
				return this.parentKey.GetColumnNames();
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x001F1268 File Offset: 0x001F0668
		internal string[] ChildColumnNames
		{
			get
			{
				return this.childKey.GetColumnNames();
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x001F1288 File Offset: 0x001F0688
		private static bool IsKeyNull(object[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (!DataStorage.IsObjectNull(values[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x001F12B8 File Offset: 0x001F06B8
		internal static DataRow[] GetChildRows(DataKey parentKey, DataKey childKey, DataRow parentRow, DataRowVersion version)
		{
			object[] keyValues = parentRow.GetKeyValues(parentKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return childKey.Table.NewRowArray(0);
			}
			Index sortIndex = childKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows);
			return sortIndex.GetRows(keyValues);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x001F1308 File Offset: 0x001F0708
		internal static DataRow[] GetParentRows(DataKey parentKey, DataKey childKey, DataRow childRow, DataRowVersion version)
		{
			object[] keyValues = childRow.GetKeyValues(childKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return parentKey.Table.NewRowArray(0);
			}
			Index sortIndex = parentKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows);
			return sortIndex.GetRows(keyValues);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x001F1358 File Offset: 0x001F0758
		internal static DataRow GetParentRow(DataKey parentKey, DataKey childKey, DataRow childRow, DataRowVersion version)
		{
			if (!childRow.HasVersion((version == DataRowVersion.Original) ? DataRowVersion.Original : DataRowVersion.Current) && childRow.tempRecord == -1)
			{
				return null;
			}
			object[] keyValues = childRow.GetKeyValues(childKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return null;
			}
			Index sortIndex = parentKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows);
			Range range = sortIndex.FindRecords(keyValues);
			if (range.IsNull)
			{
				return null;
			}
			if (range.Count > 1)
			{
				throw ExceptionBuilder.MultipleParents();
			}
			return parentKey.Table.recordManager[sortIndex.GetRecord(range.Min)];
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x001F13F8 File Offset: 0x001F07F8
		internal void SetDataSet(DataSet dataSet)
		{
			if (this.dataSet != dataSet)
			{
				this.dataSet = dataSet;
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x001F1418 File Offset: 0x001F0818
		internal void SetParentRowRecords(DataRow childRow, DataRow parentRow)
		{
			object[] keyValues = parentRow.GetKeyValues(this.ParentKey);
			if (childRow.tempRecord != -1)
			{
				this.ChildTable.recordManager.SetKeyValues(childRow.tempRecord, this.ChildKey, keyValues);
			}
			if (childRow.newRecord != -1)
			{
				this.ChildTable.recordManager.SetKeyValues(childRow.newRecord, this.ChildKey, keyValues);
			}
			if (childRow.oldRecord != -1)
			{
				this.ChildTable.recordManager.SetKeyValues(childRow.oldRecord, this.ChildKey, keyValues);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x001F14A8 File Offset: 0x001F08A8
		[ResDescription("DataRelationParentColumnsDescr")]
		[ResCategory("DataCategory_Data")]
		public virtual DataColumn[] ParentColumns
		{
			get
			{
				this.CheckStateForProperty();
				return this.parentKey.ToArray();
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x001F14C8 File Offset: 0x001F08C8
		internal DataColumn[] ParentColumnsReference
		{
			get
			{
				return this.parentKey.ColumnsReference;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x001F14E8 File Offset: 0x001F08E8
		internal DataKey ParentKey
		{
			get
			{
				this.CheckStateForProperty();
				return this.parentKey;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x001F1508 File Offset: 0x001F0908
		public virtual DataTable ParentTable
		{
			get
			{
				this.CheckStateForProperty();
				return this.parentKey.Table;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x001F1528 File Offset: 0x001F0928
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x001F1548 File Offset: 0x001F0948
		[ResDescription("DataRelationRelationNameDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
		public virtual string RelationName
		{
			get
			{
				this.CheckStateForProperty();
				return this.relationName;
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataRelation.set_RelationName|API> %d#, '%ls'\n", this.ObjectID, value);
				try
				{
					if (value == null)
					{
						value = "";
					}
					CultureInfo culture = (this.dataSet != null) ? this.dataSet.Locale : CultureInfo.CurrentCulture;
					if (string.Compare(this.relationName, value, true, culture) != 0)
					{
						if (this.dataSet != null)
						{
							if (value.Length == 0)
							{
								throw ExceptionBuilder.NoRelationName();
							}
							this.dataSet.Relations.RegisterName(value);
							if (this.relationName.Length != 0)
							{
								this.dataSet.Relations.UnregisterName(this.relationName);
							}
						}
						this.relationName = value;
						((DataRelationCollection.DataTableRelationCollection)this.ParentTable.ChildRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						((DataRelationCollection.DataTableRelationCollection)this.ChildTable.ParentRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
					}
					else if (string.Compare(this.relationName, value, false, culture) != 0)
					{
						this.relationName = value;
						((DataRelationCollection.DataTableRelationCollection)this.ParentTable.ChildRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						((DataRelationCollection.DataTableRelationCollection)this.ChildTable.ParentRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x001F16A8 File Offset: 0x001F0AA8
		internal void CheckNamespaceValidityForNestedRelations(string ns)
		{
			foreach (object obj in this.ChildTable.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if ((dataRelation == this || dataRelation.Nested) && dataRelation.ParentTable.Namespace != ns)
				{
					throw ExceptionBuilder.InValidNestedRelation(this.ChildTable.TableName);
				}
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x001F1748 File Offset: 0x001F0B48
		internal void CheckNestedRelations()
		{
			Bid.Trace("<ds.DataRelation.CheckNestedRelations|INFO> %d#\n", this.ObjectID);
			DataTable parentTable = this.ParentTable;
			if (this.ChildTable != this.ParentTable)
			{
				List<DataTable> list = new List<DataTable>();
				list.Add(this.ChildTable);
				for (int i = 0; i < list.Count; i++)
				{
					DataRelation[] nestedParentRelations = list[i].NestedParentRelations;
					foreach (DataRelation dataRelation in nestedParentRelations)
					{
						if (dataRelation.ParentTable == this.ChildTable && dataRelation.ChildTable != this.ChildTable)
						{
							throw ExceptionBuilder.LoopInNestedRelations(this.ChildTable.TableName);
						}
						if (!list.Contains(dataRelation.ParentTable))
						{
							list.Add(dataRelation.ParentTable);
						}
					}
				}
				return;
			}
			if (string.Compare(this.ChildTable.TableName, this.ChildTable.DataSet.DataSetName, true, this.ChildTable.DataSet.Locale) == 0)
			{
				throw ExceptionBuilder.SelfnestedDatasetConflictingName(this.ChildTable.TableName);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x001F1858 File Offset: 0x001F0C58
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x001F1878 File Offset: 0x001F0C78
		[ResCategory("DataCategory_Data")]
		[DefaultValue(false)]
		[ResDescription("DataRelationNested")]
		public virtual bool Nested
		{
			get
			{
				this.CheckStateForProperty();
				return this.nested;
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataRelation.set_Nested|API> %d#, %d{bool}\n", this.ObjectID, value);
				try
				{
					if (this.nested != value)
					{
						if (this.dataSet != null && value)
						{
							if (this.ChildTable.IsNamespaceInherited())
							{
								this.CheckNamespaceValidityForNestedRelations(this.ParentTable.Namespace);
							}
							ForeignKeyConstraint foreignKeyConstraint = this.ChildTable.Constraints.FindForeignKeyConstraint(this.ChildKey.ColumnsReference, this.ParentKey.ColumnsReference);
							if (foreignKeyConstraint != null)
							{
								foreignKeyConstraint.CheckConstraint();
							}
							this.ValidateMultipleNestedRelations();
						}
						if (!value && this.parentKey.ColumnsReference[0].ColumnMapping == MappingType.Hidden)
						{
							throw ExceptionBuilder.RelationNestedReadOnly();
						}
						if (value)
						{
							this.ParentTable.Columns.RegisterColumnName(this.ChildTable.TableName, null, this.ChildTable);
						}
						else
						{
							this.ParentTable.Columns.UnregisterName(this.ChildTable.TableName);
						}
						this.RaisePropertyChanging("Nested");
						if (value)
						{
							this.CheckNestedRelations();
							if (this.DataSet != null)
							{
								if (this.ParentTable == this.ChildTable)
								{
									foreach (object obj in this.ChildTable.Rows)
									{
										DataRow dataRow = (DataRow)obj;
										dataRow.CheckForLoops(this);
									}
									if (this.ChildTable.DataSet != null && string.Compare(this.ChildTable.TableName, this.ChildTable.DataSet.DataSetName, true, this.ChildTable.DataSet.Locale) == 0)
									{
										throw ExceptionBuilder.DatasetConflictingName(this.dataSet.DataSetName);
									}
									this.ChildTable.fNestedInDataset = false;
								}
								else
								{
									foreach (object obj2 in this.ChildTable.Rows)
									{
										DataRow dataRow2 = (DataRow)obj2;
										dataRow2.GetParentRow(this);
									}
								}
							}
							this.ParentTable.ElementColumnCount++;
						}
						else
						{
							this.ParentTable.ElementColumnCount--;
						}
						this.nested = value;
						this.ChildTable.CacheNestedParent();
						if (value && ADP.IsEmpty(this.ChildTable.Namespace) && (this.ChildTable.NestedParentsCount > 1 || (this.ChildTable.NestedParentsCount > 0 && !this.ChildTable.DataSet.Relations.Contains(this.RelationName))))
						{
							string text = null;
							foreach (object obj3 in this.ChildTable.ParentRelations)
							{
								DataRelation dataRelation = (DataRelation)obj3;
								if (dataRelation.Nested)
								{
									if (text == null)
									{
										text = dataRelation.ParentTable.Namespace;
									}
									else if (string.Compare(text, dataRelation.ParentTable.Namespace, StringComparison.Ordinal) != 0)
									{
										this.nested = false;
										throw ExceptionBuilder.InvalidParentNamespaceinNestedRelation(this.ChildTable.TableName);
									}
								}
							}
							if (this.CheckMultipleNested && this.ChildTable.tableNamespace != null && this.ChildTable.tableNamespace.Length == 0)
							{
								throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
							}
							this.ChildTable.tableNamespace = null;
						}
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x001F1C68 File Offset: 0x001F1068
		public virtual UniqueConstraint ParentKeyConstraint
		{
			get
			{
				this.CheckStateForProperty();
				return this.parentKeyConstraint;
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x001F1C88 File Offset: 0x001F1088
		internal void SetParentKeyConstraint(UniqueConstraint value)
		{
			this.parentKeyConstraint = value;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x001F1CA8 File Offset: 0x001F10A8
		public virtual ForeignKeyConstraint ChildKeyConstraint
		{
			get
			{
				this.CheckStateForProperty();
				return this.childKeyConstraint;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x001F1CC8 File Offset: 0x001F10C8
		[Browsable(false)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("ExtendedPropertiesDescr")]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				if (this.extendedProperties == null)
				{
					this.extendedProperties = new PropertyCollection();
				}
				return this.extendedProperties;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x001F1CF8 File Offset: 0x001F10F8
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x001F1D18 File Offset: 0x001F1118
		internal bool CheckMultipleNested
		{
			get
			{
				return this._checkMultipleNested;
			}
			set
			{
				this._checkMultipleNested = value;
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x001F1D38 File Offset: 0x001F1138
		internal void SetChildKeyConstraint(ForeignKeyConstraint value)
		{
			this.childKeyConstraint = value;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000710 RID: 1808 RVA: 0x001F1D58 File Offset: 0x001F1158
		// (remove) Token: 0x06000711 RID: 1809 RVA: 0x001F1D88 File Offset: 0x001F1188
		internal event PropertyChangedEventHandler PropertyChanging
		{
			add
			{
				this.onPropertyChangingDelegate = (PropertyChangedEventHandler)Delegate.Combine(this.onPropertyChangingDelegate, value);
			}
			remove
			{
				this.onPropertyChangingDelegate = (PropertyChangedEventHandler)Delegate.Remove(this.onPropertyChangingDelegate, value);
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x001F1DB8 File Offset: 0x001F11B8
		internal void CheckState()
		{
			if (this.dataSet == null)
			{
				this.parentKey.CheckState();
				this.childKey.CheckState();
				if (this.parentKey.Table.DataSet != this.childKey.Table.DataSet)
				{
					throw ExceptionBuilder.RelationDataSetMismatch();
				}
				if (this.childKey.ColumnsEqual(this.parentKey))
				{
					throw ExceptionBuilder.KeyColumnsIdentical();
				}
				for (int i = 0; i < this.parentKey.ColumnsReference.Length; i++)
				{
					if (this.parentKey.ColumnsReference[i].DataType != this.childKey.ColumnsReference[i].DataType || (this.parentKey.ColumnsReference[i].DataType == typeof(DateTime) && this.parentKey.ColumnsReference[i].DateTimeMode != this.childKey.ColumnsReference[i].DateTimeMode && (this.parentKey.ColumnsReference[i].DateTimeMode & this.childKey.ColumnsReference[i].DateTimeMode) != DataSetDateTime.Unspecified))
					{
						throw ExceptionBuilder.ColumnsTypeMismatch();
					}
				}
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x001F1EE8 File Offset: 0x001F12E8
		protected void CheckStateForProperty()
		{
			try
			{
				this.CheckState();
			}
			catch (Exception ex)
			{
				if (ADP.IsCatchableExceptionType(ex))
				{
					throw ExceptionBuilder.BadObjectPropertyAccess(ex.Message);
				}
				throw;
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x001F1F38 File Offset: 0x001F1338
		private void Create(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataRelation.Create|INFO> %d#, relationName='%ls', createConstraints=%d{bool}\n", this.ObjectID, relationName, createConstraints);
			try
			{
				this.parentKey = new DataKey(parentColumns, true);
				this.childKey = new DataKey(childColumns, true);
				if (parentColumns.Length != childColumns.Length)
				{
					throw ExceptionBuilder.KeyLengthMismatch();
				}
				for (int i = 0; i < parentColumns.Length; i++)
				{
					if (parentColumns[i].Table.DataSet == null || childColumns[i].Table.DataSet == null)
					{
						throw ExceptionBuilder.ParentOrChildColumnsDoNotHaveDataSet();
					}
				}
				this.CheckState();
				this.relationName = ((relationName == null) ? "" : relationName);
				this.createConstraints = createConstraints;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x001F2008 File Offset: 0x001F1408
		internal DataRelation Clone(DataSet destination)
		{
			Bid.Trace("<ds.DataRelation.Clone|INFO> %d#, destination=%d\n", this.ObjectID, (destination != null) ? destination.ObjectID : 0);
			DataTable dataTable = destination.Tables[this.ParentTable.TableName, this.ParentTable.Namespace];
			DataTable dataTable2 = destination.Tables[this.ChildTable.TableName, this.ChildTable.Namespace];
			int num = this.parentKey.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num];
			DataColumn[] array2 = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = dataTable.Columns[this.ParentKey.ColumnsReference[i].ColumnName];
				array2[i] = dataTable2.Columns[this.ChildKey.ColumnsReference[i].ColumnName];
			}
			DataRelation dataRelation = new DataRelation(this.relationName, array, array2, false);
			dataRelation.CheckMultipleNested = false;
			dataRelation.Nested = this.Nested;
			dataRelation.CheckMultipleNested = true;
			if (this.extendedProperties != null)
			{
				foreach (object key in this.extendedProperties.Keys)
				{
					dataRelation.ExtendedProperties[key] = this.extendedProperties[key];
				}
			}
			return dataRelation;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x001F2198 File Offset: 0x001F1598
		protected internal void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this.onPropertyChangingDelegate != null)
			{
				Bid.Trace("<ds.DataRelation.OnPropertyChanging|INFO> %d#\n", this.ObjectID);
				this.onPropertyChangingDelegate(this, pcevent);
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x001F21D8 File Offset: 0x001F15D8
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x001F21F8 File Offset: 0x001F15F8
		public override string ToString()
		{
			return this.RelationName;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x001F2218 File Offset: 0x001F1618
		internal void ValidateMultipleNestedRelations()
		{
			if (!this.Nested || !this.CheckMultipleNested)
			{
				return;
			}
			if (0 < this.ChildTable.NestedParentRelations.Length)
			{
				DataColumn[] childColumns = this.ChildColumns;
				if (childColumns.Length != 1 || !this.IsAutoGenerated(childColumns[0]))
				{
					throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
				}
				if (!XmlTreeGen.AutoGenerated(this))
				{
					throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
				}
				foreach (object obj in this.ChildTable.Constraints)
				{
					Constraint constraint = (Constraint)obj;
					if (constraint is ForeignKeyConstraint)
					{
						ForeignKeyConstraint fk = (ForeignKeyConstraint)constraint;
						if (!XmlTreeGen.AutoGenerated(fk, true))
						{
							throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
						}
					}
					else
					{
						UniqueConstraint unique = (UniqueConstraint)constraint;
						if (!XmlTreeGen.AutoGenerated(unique))
						{
							throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
						}
					}
				}
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x001F2338 File Offset: 0x001F1738
		private bool IsAutoGenerated(DataColumn col)
		{
			if (col.ColumnMapping != MappingType.Hidden)
			{
				return false;
			}
			if (col.DataType != typeof(int))
			{
				return false;
			}
			string text = col.Table.TableName + "_Id";
			if (col.ColumnName == text || col.ColumnName == text + "_0")
			{
				return true;
			}
			text = this.ParentColumnsReference[0].Table.TableName + "_Id";
			return col.ColumnName == text || col.ColumnName == text + "_0";
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x001F23E8 File Offset: 0x001F17E8
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x04000723 RID: 1827
		private DataSet dataSet;

		// Token: 0x04000724 RID: 1828
		internal PropertyCollection extendedProperties;

		// Token: 0x04000725 RID: 1829
		internal string relationName;

		// Token: 0x04000726 RID: 1830
		private PropertyChangedEventHandler onPropertyChangingDelegate;

		// Token: 0x04000727 RID: 1831
		private DataKey childKey;

		// Token: 0x04000728 RID: 1832
		private DataKey parentKey;

		// Token: 0x04000729 RID: 1833
		private UniqueConstraint parentKeyConstraint;

		// Token: 0x0400072A RID: 1834
		private ForeignKeyConstraint childKeyConstraint;

		// Token: 0x0400072B RID: 1835
		internal string[] parentColumnNames;

		// Token: 0x0400072C RID: 1836
		internal string[] childColumnNames;

		// Token: 0x0400072D RID: 1837
		internal string parentTableName;

		// Token: 0x0400072E RID: 1838
		internal string childTableName;

		// Token: 0x0400072F RID: 1839
		internal string parentTableNamespace;

		// Token: 0x04000730 RID: 1840
		internal string childTableNamespace;

		// Token: 0x04000731 RID: 1841
		internal bool nested;

		// Token: 0x04000732 RID: 1842
		internal bool createConstraints;

		// Token: 0x04000733 RID: 1843
		private bool _checkMultipleNested;

		// Token: 0x04000734 RID: 1844
		private static int _objectTypeCount;

		// Token: 0x04000735 RID: 1845
		private readonly int _objectID;
	}
}

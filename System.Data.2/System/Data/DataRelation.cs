using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace System.Data
{
	// Token: 0x020000BA RID: 186
	[TypeConverter(typeof(RelationshipConverter))]
	[DefaultProperty("RelationName")]
	[Editor("Microsoft.VSDesigner.Data.Design.DataRelationEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataRelation
	{
		// Token: 0x06000A92 RID: 2706 RVA: 0x0005F59C File Offset: 0x0005E99C
		public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn) : this(relationName, parentColumn, childColumn, true)
		{
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0005F5B4 File Offset: 0x0005E9B4
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

		// Token: 0x06000A94 RID: 2708 RVA: 0x0005F638 File Offset: 0x0005EA38
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns) : this(relationName, parentColumns, childColumns, true)
		{
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0005F650 File Offset: 0x0005EA50
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			this.relationName = "";
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation._objectTypeCount);
			base..ctor();
			this.Create(relationName, parentColumns, childColumns, createConstraints);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0005F690 File Offset: 0x0005EA90
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

		// Token: 0x06000A97 RID: 2711 RVA: 0x0005F6F4 File Offset: 0x0005EAF4
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0005F768 File Offset: 0x0005EB68
		[ResDescription("DataRelationChildColumnsDescr")]
		[ResCategory("DataCategory_Data")]
		public virtual DataColumn[] ChildColumns
		{
			get
			{
				this.CheckStateForProperty();
				return this.childKey.ToArray();
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0005F788 File Offset: 0x0005EB88
		internal DataColumn[] ChildColumnsReference
		{
			get
			{
				this.CheckStateForProperty();
				return this.childKey.ColumnsReference;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0005F7A8 File Offset: 0x0005EBA8
		internal DataKey ChildKey
		{
			get
			{
				this.CheckStateForProperty();
				return this.childKey;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0005F7C4 File Offset: 0x0005EBC4
		public virtual DataTable ChildTable
		{
			get
			{
				this.CheckStateForProperty();
				return this.childKey.Table;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0005F7E4 File Offset: 0x0005EBE4
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0005F800 File Offset: 0x0005EC00
		internal string[] ParentColumnNames
		{
			get
			{
				return this.parentKey.GetColumnNames();
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0005F818 File Offset: 0x0005EC18
		internal string[] ChildColumnNames
		{
			get
			{
				return this.childKey.GetColumnNames();
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0005F830 File Offset: 0x0005EC30
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

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0005F858 File Offset: 0x0005EC58
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

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0005F8A4 File Offset: 0x0005ECA4
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

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0005F8F0 File Offset: 0x0005ECF0
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

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0005F990 File Offset: 0x0005ED90
		internal void SetDataSet(DataSet dataSet)
		{
			if (this.dataSet != dataSet)
			{
				this.dataSet = dataSet;
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0005F9B0 File Offset: 0x0005EDB0
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

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0005FA3C File Offset: 0x0005EE3C
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

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0005FA5C File Offset: 0x0005EE5C
		internal DataColumn[] ParentColumnsReference
		{
			get
			{
				return this.parentKey.ColumnsReference;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0005FA74 File Offset: 0x0005EE74
		internal DataKey ParentKey
		{
			get
			{
				this.CheckStateForProperty();
				return this.parentKey;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0005FA90 File Offset: 0x0005EE90
		public virtual DataTable ParentTable
		{
			get
			{
				this.CheckStateForProperty();
				return this.parentKey.Table;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0005FAB0 File Offset: 0x0005EEB0
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0005FACC File Offset: 0x0005EECC
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

		// Token: 0x06000AAB RID: 2731 RVA: 0x0005FC24 File Offset: 0x0005F024
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

		// Token: 0x06000AAC RID: 2732 RVA: 0x0005FCB8 File Offset: 0x0005F0B8
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

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0005FDC0 File Offset: 0x0005F1C0
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x0005FDDC File Offset: 0x0005F1DC
		[DefaultValue(false)]
		[ResDescription("DataRelationNested")]
		[ResCategory("DataCategory_Data")]
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
							this.ParentTable.Columns.RegisterColumnName(this.ChildTable.TableName, null);
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
							DataTable parentTable = this.ParentTable;
							int elementColumnCount = parentTable.ElementColumnCount;
							parentTable.ElementColumnCount = elementColumnCount + 1;
						}
						else
						{
							DataTable parentTable2 = this.ParentTable;
							int elementColumnCount = parentTable2.ElementColumnCount;
							parentTable2.ElementColumnCount = elementColumnCount - 1;
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

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000601BC File Offset: 0x0005F5BC
		public virtual UniqueConstraint ParentKeyConstraint
		{
			get
			{
				this.CheckStateForProperty();
				return this.parentKeyConstraint;
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000601D8 File Offset: 0x0005F5D8
		internal void SetParentKeyConstraint(UniqueConstraint value)
		{
			this.parentKeyConstraint = value;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x000601EC File Offset: 0x0005F5EC
		public virtual ForeignKeyConstraint ChildKeyConstraint
		{
			get
			{
				this.CheckStateForProperty();
				return this.childKeyConstraint;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00060208 File Offset: 0x0005F608
		[Browsable(false)]
		[ResDescription("ExtendedPropertiesDescr")]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00060230 File Offset: 0x0005F630
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x00060244 File Offset: 0x0005F644
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

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00060258 File Offset: 0x0005F658
		internal void SetChildKeyConstraint(ForeignKeyConstraint value)
		{
			this.childKeyConstraint = value;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000AB6 RID: 2742 RVA: 0x0006026C File Offset: 0x0005F66C
		// (remove) Token: 0x06000AB7 RID: 2743 RVA: 0x00060290 File Offset: 0x0005F690
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

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000602B4 File Offset: 0x0005F6B4
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

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000603E4 File Offset: 0x0005F7E4
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

		// Token: 0x06000ABA RID: 2746 RVA: 0x00060430 File Offset: 0x0005F830
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

		// Token: 0x06000ABB RID: 2747 RVA: 0x000604F4 File Offset: 0x0005F8F4
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

		// Token: 0x06000ABC RID: 2748 RVA: 0x0006067C File Offset: 0x0005FA7C
		protected internal void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this.onPropertyChangingDelegate != null)
			{
				Bid.Trace("<ds.DataRelation.OnPropertyChanging|INFO> %d#\n", this.ObjectID);
				this.onPropertyChangingDelegate(this, pcevent);
			}
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000606B0 File Offset: 0x0005FAB0
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000606CC File Offset: 0x0005FACC
		public override string ToString()
		{
			return this.RelationName;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000606E0 File Offset: 0x0005FAE0
		internal void ValidateMultipleNestedRelations()
		{
			if (!this.Nested || !this.CheckMultipleNested)
			{
				return;
			}
			if (this.ChildTable.NestedParentRelations.Length != 0)
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

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000607F4 File Offset: 0x0005FBF4
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

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000608AC File Offset: 0x0005FCAC
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x0400032D RID: 813
		private DataSet dataSet;

		// Token: 0x0400032E RID: 814
		internal PropertyCollection extendedProperties;

		// Token: 0x0400032F RID: 815
		internal string relationName;

		// Token: 0x04000330 RID: 816
		private PropertyChangedEventHandler onPropertyChangingDelegate;

		// Token: 0x04000331 RID: 817
		private DataKey childKey;

		// Token: 0x04000332 RID: 818
		private DataKey parentKey;

		// Token: 0x04000333 RID: 819
		private UniqueConstraint parentKeyConstraint;

		// Token: 0x04000334 RID: 820
		private ForeignKeyConstraint childKeyConstraint;

		// Token: 0x04000335 RID: 821
		internal string[] parentColumnNames;

		// Token: 0x04000336 RID: 822
		internal string[] childColumnNames;

		// Token: 0x04000337 RID: 823
		internal string parentTableName;

		// Token: 0x04000338 RID: 824
		internal string childTableName;

		// Token: 0x04000339 RID: 825
		internal string parentTableNamespace;

		// Token: 0x0400033A RID: 826
		internal string childTableNamespace;

		// Token: 0x0400033B RID: 827
		internal bool nested;

		// Token: 0x0400033C RID: 828
		internal bool createConstraints;

		// Token: 0x0400033D RID: 829
		private bool _checkMultipleNested;

		// Token: 0x0400033E RID: 830
		private static int _objectTypeCount;

		// Token: 0x0400033F RID: 831
		private readonly int _objectID;
	}
}

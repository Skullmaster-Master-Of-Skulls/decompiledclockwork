using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x020000B6 RID: 182
	[DefaultProperty("ConstraintName")]
	[Editor("Microsoft.VSDesigner.Data.Design.ForeignKeyConstraintEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ForeignKeyConstraint : Constraint
	{
		// Token: 0x06000C20 RID: 3104 RVA: 0x0020FA58 File Offset: 0x0020EE58
		public ForeignKeyConstraint(DataColumn parentColumn, DataColumn childColumn) : this(null, parentColumn, childColumn)
		{
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0020FA78 File Offset: 0x0020EE78
		public ForeignKeyConstraint(string constraintName, DataColumn parentColumn, DataColumn childColumn)
		{
			this.deleteRule = Rule.Cascade;
			this.updateRule = Rule.Cascade;
			base..ctor();
			DataColumn[] parentColumns = new DataColumn[]
			{
				parentColumn
			};
			DataColumn[] childColumns = new DataColumn[]
			{
				childColumn
			};
			this.Create(constraintName, parentColumns, childColumns);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0020FAC8 File Offset: 0x0020EEC8
		public ForeignKeyConstraint(DataColumn[] parentColumns, DataColumn[] childColumns) : this(null, parentColumns, childColumns)
		{
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0020FAE8 File Offset: 0x0020EEE8
		public ForeignKeyConstraint(string constraintName, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			this.deleteRule = Rule.Cascade;
			this.updateRule = Rule.Cascade;
			base..ctor();
			this.Create(constraintName, parentColumns, childColumns);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0020FB18 File Offset: 0x0020EF18
		[Browsable(false)]
		public ForeignKeyConstraint(string constraintName, string parentTableName, string[] parentColumnNames, string[] childColumnNames, AcceptRejectRule acceptRejectRule, Rule deleteRule, Rule updateRule)
		{
			this.deleteRule = Rule.Cascade;
			this.updateRule = Rule.Cascade;
			base..ctor();
			this.constraintName = constraintName;
			this.parentColumnNames = parentColumnNames;
			this.childColumnNames = childColumnNames;
			this.parentTableName = parentTableName;
			this.acceptRejectRule = acceptRejectRule;
			this.deleteRule = deleteRule;
			this.updateRule = updateRule;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0020FB78 File Offset: 0x0020EF78
		[Browsable(false)]
		public ForeignKeyConstraint(string constraintName, string parentTableName, string parentTableNamespace, string[] parentColumnNames, string[] childColumnNames, AcceptRejectRule acceptRejectRule, Rule deleteRule, Rule updateRule)
		{
			this.deleteRule = Rule.Cascade;
			this.updateRule = Rule.Cascade;
			base..ctor();
			this.constraintName = constraintName;
			this.parentColumnNames = parentColumnNames;
			this.childColumnNames = childColumnNames;
			this.parentTableName = parentTableName;
			this.parentTableNamespace = parentTableNamespace;
			this.acceptRejectRule = acceptRejectRule;
			this.deleteRule = deleteRule;
			this.updateRule = updateRule;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0020FBD8 File Offset: 0x0020EFD8
		internal DataKey ChildKey
		{
			get
			{
				base.CheckStateForProperty();
				return this.childKey;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0020FBF8 File Offset: 0x0020EFF8
		[ResDescription("ForeignKeyConstraintChildColumnsDescr")]
		[ResCategory("DataCategory_Data")]
		[ReadOnly(true)]
		public virtual DataColumn[] Columns
		{
			get
			{
				base.CheckStateForProperty();
				return this.childKey.ToArray();
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0020FC18 File Offset: 0x0020F018
		[ReadOnly(true)]
		[ResDescription("ConstraintTableDescr")]
		[ResCategory("DataCategory_Data")]
		public override DataTable Table
		{
			get
			{
				base.CheckStateForProperty();
				return this.childKey.Table;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0020FC38 File Offset: 0x0020F038
		internal string[] ParentColumnNames
		{
			get
			{
				return this.parentKey.GetColumnNames();
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0020FC58 File Offset: 0x0020F058
		internal string[] ChildColumnNames
		{
			get
			{
				return this.childKey.GetColumnNames();
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0020FC78 File Offset: 0x0020F078
		internal override void CheckCanAddToCollection(ConstraintCollection constraints)
		{
			if (this.Table != constraints.Table)
			{
				throw ExceptionBuilder.ConstraintAddFailed(constraints.Table);
			}
			if (this.Table.Locale.LCID != this.RelatedTable.Locale.LCID || this.Table.CaseSensitive != this.RelatedTable.CaseSensitive)
			{
				throw ExceptionBuilder.CaseLocaleMismatch();
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0020FCE8 File Offset: 0x0020F0E8
		internal override bool CanBeRemovedFromCollection(ConstraintCollection constraints, bool fThrowException)
		{
			return true;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0020FCF8 File Offset: 0x0020F0F8
		internal bool IsKeyNull(object[] values)
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

		// Token: 0x06000C2E RID: 3118 RVA: 0x0020FD28 File Offset: 0x0020F128
		internal override bool IsConstraintViolated()
		{
			Index sortIndex = this.childKey.GetSortIndex();
			object[] uniqueKeyValues = sortIndex.GetUniqueKeyValues();
			bool result = false;
			Index sortIndex2 = this.parentKey.GetSortIndex();
			foreach (object[] array in uniqueKeyValues)
			{
				if (!this.IsKeyNull(array) && !sortIndex2.IsKeyInIndex(array))
				{
					DataRow[] rows = sortIndex.GetRows(sortIndex.FindRecords(array));
					string @string = Res.GetString("DataConstraint_ForeignKeyViolation", new object[]
					{
						this.ConstraintName,
						ExceptionBuilder.KeysToString(array)
					});
					for (int j = 0; j < rows.Length; j++)
					{
						rows[j].RowError = @string;
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0020FDE8 File Offset: 0x0020F1E8
		internal override bool CanEnableConstraint()
		{
			if (this.Table.DataSet == null || !this.Table.DataSet.EnforceConstraints)
			{
				return true;
			}
			Index sortIndex = this.childKey.GetSortIndex();
			object[] uniqueKeyValues = sortIndex.GetUniqueKeyValues();
			Index sortIndex2 = this.parentKey.GetSortIndex();
			foreach (object[] array in uniqueKeyValues)
			{
				if (!this.IsKeyNull(array) && !sortIndex2.IsKeyInIndex(array))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0020FE68 File Offset: 0x0020F268
		internal void CascadeCommit(DataRow row)
		{
			if (row.RowState == DataRowState.Detached)
			{
				return;
			}
			if (this.acceptRejectRule == AcceptRejectRule.Cascade)
			{
				Index sortIndex = this.childKey.GetSortIndex((row.RowState == DataRowState.Deleted) ? DataViewRowState.Deleted : DataViewRowState.CurrentRows);
				object[] keyValues = row.GetKeyValues(this.parentKey, (row.RowState == DataRowState.Deleted) ? DataRowVersion.Original : DataRowVersion.Default);
				if (this.IsKeyNull(keyValues))
				{
					return;
				}
				Range range = sortIndex.FindRecords(keyValues);
				if (!range.IsNull)
				{
					DataRow[] rows = sortIndex.GetRows(range);
					foreach (DataRow dataRow in rows)
					{
						if (DataRowState.Detached != dataRow.RowState && !dataRow.inCascade)
						{
							dataRow.AcceptChanges();
						}
					}
				}
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0020FF28 File Offset: 0x0020F328
		internal void CascadeDelete(DataRow row)
		{
			if (-1 == row.newRecord)
			{
				return;
			}
			object[] keyValues = row.GetKeyValues(this.parentKey, DataRowVersion.Current);
			if (this.IsKeyNull(keyValues))
			{
				return;
			}
			Index sortIndex = this.childKey.GetSortIndex();
			switch (this.DeleteRule)
			{
			case Rule.None:
				if (row.Table.DataSet.EnforceConstraints)
				{
					Range range = sortIndex.FindRecords(keyValues);
					if (!range.IsNull)
					{
						if (range.Count == 1 && sortIndex.GetRow(range.Min) == row)
						{
							return;
						}
						throw ExceptionBuilder.FailedCascadeDelete(this.ConstraintName);
					}
				}
				break;
			case Rule.Cascade:
			{
				object[] keyValues2 = row.GetKeyValues(this.parentKey, DataRowVersion.Default);
				Range range2 = sortIndex.FindRecords(keyValues2);
				if (!range2.IsNull)
				{
					foreach (DataRow dataRow in sortIndex.GetRows(range2))
					{
						if (!dataRow.inCascade)
						{
							dataRow.Table.DeleteRow(dataRow);
						}
					}
					return;
				}
				break;
			}
			case Rule.SetNull:
			{
				object[] array = new object[this.childKey.ColumnsReference.Length];
				for (int j = 0; j < this.childKey.ColumnsReference.Length; j++)
				{
					array[j] = DBNull.Value;
				}
				Range range3 = sortIndex.FindRecords(keyValues);
				if (!range3.IsNull)
				{
					DataRow[] rows2 = sortIndex.GetRows(range3);
					for (int k = 0; k < rows2.Length; k++)
					{
						if (row != rows2[k])
						{
							rows2[k].SetKeyValues(this.childKey, array);
						}
					}
					return;
				}
				break;
			}
			case Rule.SetDefault:
			{
				object[] array2 = new object[this.childKey.ColumnsReference.Length];
				for (int l = 0; l < this.childKey.ColumnsReference.Length; l++)
				{
					array2[l] = this.childKey.ColumnsReference[l].DefaultValue;
				}
				Range range4 = sortIndex.FindRecords(keyValues);
				if (!range4.IsNull)
				{
					DataRow[] rows3 = sortIndex.GetRows(range4);
					for (int m = 0; m < rows3.Length; m++)
					{
						if (row != rows3[m])
						{
							rows3[m].SetKeyValues(this.childKey, array2);
						}
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00210148 File Offset: 0x0020F548
		internal void CascadeRollback(DataRow row)
		{
			Index sortIndex = this.childKey.GetSortIndex((row.RowState == DataRowState.Deleted) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows);
			object[] keyValues = row.GetKeyValues(this.parentKey, (row.RowState == DataRowState.Modified) ? DataRowVersion.Current : DataRowVersion.Default);
			if (this.IsKeyNull(keyValues))
			{
				return;
			}
			Range range = sortIndex.FindRecords(keyValues);
			if (this.acceptRejectRule == AcceptRejectRule.Cascade)
			{
				if (!range.IsNull)
				{
					DataRow[] rows = sortIndex.GetRows(range);
					for (int i = 0; i < rows.Length; i++)
					{
						if (!rows[i].inCascade)
						{
							rows[i].RejectChanges();
						}
					}
					return;
				}
			}
			else if (row.RowState != DataRowState.Deleted && row.Table.DataSet.EnforceConstraints && !range.IsNull)
			{
				if (range.Count == 1 && sortIndex.GetRow(range.Min) == row)
				{
					return;
				}
				if (row.HasKeyChanged(this.parentKey))
				{
					throw ExceptionBuilder.FailedCascadeUpdate(this.ConstraintName);
				}
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00210248 File Offset: 0x0020F648
		internal void CascadeUpdate(DataRow row)
		{
			if (-1 == row.newRecord)
			{
				return;
			}
			object[] keyValues = row.GetKeyValues(this.parentKey, DataRowVersion.Current);
			if (!this.Table.DataSet.fInReadXml && this.IsKeyNull(keyValues))
			{
				return;
			}
			Index sortIndex = this.childKey.GetSortIndex();
			switch (this.UpdateRule)
			{
			case Rule.None:
				if (row.Table.DataSet.EnforceConstraints && !sortIndex.FindRecords(keyValues).IsNull)
				{
					throw ExceptionBuilder.FailedCascadeUpdate(this.ConstraintName);
				}
				break;
			case Rule.Cascade:
			{
				Range range = sortIndex.FindRecords(keyValues);
				if (!range.IsNull)
				{
					object[] keyValues2 = row.GetKeyValues(this.parentKey, DataRowVersion.Proposed);
					DataRow[] rows = sortIndex.GetRows(range);
					for (int i = 0; i < rows.Length; i++)
					{
						rows[i].SetKeyValues(this.childKey, keyValues2);
					}
					return;
				}
				break;
			}
			case Rule.SetNull:
			{
				object[] array = new object[this.childKey.ColumnsReference.Length];
				for (int j = 0; j < this.childKey.ColumnsReference.Length; j++)
				{
					array[j] = DBNull.Value;
				}
				Range range2 = sortIndex.FindRecords(keyValues);
				if (!range2.IsNull)
				{
					DataRow[] rows2 = sortIndex.GetRows(range2);
					for (int k = 0; k < rows2.Length; k++)
					{
						rows2[k].SetKeyValues(this.childKey, array);
					}
					return;
				}
				break;
			}
			case Rule.SetDefault:
			{
				object[] array2 = new object[this.childKey.ColumnsReference.Length];
				for (int l = 0; l < this.childKey.ColumnsReference.Length; l++)
				{
					array2[l] = this.childKey.ColumnsReference[l].DefaultValue;
				}
				Range range3 = sortIndex.FindRecords(keyValues);
				if (!range3.IsNull)
				{
					DataRow[] rows3 = sortIndex.GetRows(range3);
					for (int m = 0; m < rows3.Length; m++)
					{
						rows3[m].SetKeyValues(this.childKey, array2);
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00210448 File Offset: 0x0020F848
		internal void CheckCanClearParentTable(DataTable table)
		{
			if (this.Table.DataSet.EnforceConstraints && this.Table.Rows.Count > 0)
			{
				throw ExceptionBuilder.FailedClearParentTable(table.TableName, this.ConstraintName, this.Table.TableName);
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00210498 File Offset: 0x0020F898
		internal void CheckCanRemoveParentRow(DataRow row)
		{
			if (!this.Table.DataSet.EnforceConstraints)
			{
				return;
			}
			if (DataRelation.GetChildRows(this.ParentKey, this.ChildKey, row, DataRowVersion.Default).Length > 0)
			{
				throw ExceptionBuilder.RemoveParentRow(this);
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x002104E8 File Offset: 0x0020F8E8
		internal void CheckCascade(DataRow row, DataRowAction action)
		{
			if (row.inCascade)
			{
				return;
			}
			row.inCascade = true;
			try
			{
				if (action == DataRowAction.Change)
				{
					if (row.HasKeyChanged(this.parentKey))
					{
						this.CascadeUpdate(row);
					}
				}
				else if (action == DataRowAction.Delete)
				{
					this.CascadeDelete(row);
				}
				else if (action == DataRowAction.Commit)
				{
					this.CascadeCommit(row);
				}
				else if (action == DataRowAction.Rollback)
				{
					this.CascadeRollback(row);
				}
			}
			finally
			{
				row.inCascade = false;
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00210578 File Offset: 0x0020F978
		internal override void CheckConstraint(DataRow childRow, DataRowAction action)
		{
			if ((action == DataRowAction.Change || action == DataRowAction.Add || action == DataRowAction.Rollback) && this.Table.DataSet != null && this.Table.DataSet.EnforceConstraints && childRow.HasKeyChanged(this.childKey))
			{
				DataRowVersion dataRowVersion = (action == DataRowAction.Rollback) ? DataRowVersion.Original : DataRowVersion.Current;
				object[] keyValues = childRow.GetKeyValues(this.childKey);
				if (childRow.HasVersion(dataRowVersion))
				{
					DataRow parentRow = DataRelation.GetParentRow(this.ParentKey, this.ChildKey, childRow, dataRowVersion);
					if (parentRow != null && parentRow.inCascade)
					{
						object[] keyValues2 = parentRow.GetKeyValues(this.parentKey, (action == DataRowAction.Rollback) ? dataRowVersion : DataRowVersion.Default);
						int num = childRow.Table.NewRecord();
						childRow.Table.SetKeyValues(this.childKey, keyValues2, num);
						if (this.childKey.RecordsEqual(childRow.tempRecord, num))
						{
							return;
						}
					}
				}
				object[] keyValues3 = childRow.GetKeyValues(this.childKey);
				if (!this.IsKeyNull(keyValues3))
				{
					Index sortIndex = this.parentKey.GetSortIndex();
					if (!sortIndex.IsKeyInIndex(keyValues3))
					{
						if (this.childKey.Table == this.parentKey.Table && childRow.tempRecord != -1)
						{
							int i;
							for (i = 0; i < keyValues3.Length; i++)
							{
								DataColumn dataColumn = this.parentKey.ColumnsReference[i];
								object value = dataColumn.ConvertValue(keyValues3[i]);
								if (dataColumn.CompareValueTo(childRow.tempRecord, value) != 0)
								{
									break;
								}
							}
							if (i == keyValues3.Length)
							{
								return;
							}
						}
						throw ExceptionBuilder.ForeignKeyViolation(this.ConstraintName, keyValues);
					}
				}
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00210708 File Offset: 0x0020FB08
		private void NonVirtualCheckState()
		{
			if (this._DataSet == null)
			{
				this.parentKey.CheckState();
				this.childKey.CheckState();
				if (this.parentKey.Table.DataSet != this.childKey.Table.DataSet)
				{
					throw ExceptionBuilder.TablesInDifferentSets();
				}
				for (int i = 0; i < this.parentKey.ColumnsReference.Length; i++)
				{
					if (this.parentKey.ColumnsReference[i].DataType != this.childKey.ColumnsReference[i].DataType || (this.parentKey.ColumnsReference[i].DataType == typeof(DateTime) && this.parentKey.ColumnsReference[i].DateTimeMode != this.childKey.ColumnsReference[i].DateTimeMode && (this.parentKey.ColumnsReference[i].DateTimeMode & this.childKey.ColumnsReference[i].DateTimeMode) != DataSetDateTime.Unspecified))
					{
						throw ExceptionBuilder.ColumnsTypeMismatch();
					}
				}
				if (this.childKey.ColumnsEqual(this.parentKey))
				{
					throw ExceptionBuilder.KeyColumnsIdentical();
				}
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00210838 File Offset: 0x0020FC38
		internal override void CheckState()
		{
			this.NonVirtualCheckState();
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00210858 File Offset: 0x0020FC58
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x00210878 File Offset: 0x0020FC78
		[ResDescription("ForeignKeyConstraintAcceptRejectRuleDescr")]
		[DefaultValue(AcceptRejectRule.None)]
		[ResCategory("DataCategory_Data")]
		public virtual AcceptRejectRule AcceptRejectRule
		{
			get
			{
				base.CheckStateForProperty();
				return this.acceptRejectRule;
			}
			set
			{
				switch (value)
				{
				case AcceptRejectRule.None:
				case AcceptRejectRule.Cascade:
					this.acceptRejectRule = value;
					return;
				default:
					throw ADP.InvalidAcceptRejectRule(value);
				}
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x002108A8 File Offset: 0x0020FCA8
		internal override bool ContainsColumn(DataColumn column)
		{
			return this.parentKey.ContainsColumn(column) || this.childKey.ContainsColumn(column);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x002108D8 File Offset: 0x0020FCD8
		internal override Constraint Clone(DataSet destination)
		{
			return this.Clone(destination, false);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x002108F8 File Offset: 0x0020FCF8
		internal override Constraint Clone(DataSet destination, bool ignorNSforTableLookup)
		{
			int num;
			if (ignorNSforTableLookup)
			{
				num = destination.Tables.IndexOf(this.Table.TableName);
			}
			else
			{
				num = destination.Tables.IndexOf(this.Table.TableName, this.Table.Namespace, false);
			}
			if (num < 0)
			{
				return null;
			}
			DataTable dataTable = destination.Tables[num];
			if (ignorNSforTableLookup)
			{
				num = destination.Tables.IndexOf(this.RelatedTable.TableName);
			}
			else
			{
				num = destination.Tables.IndexOf(this.RelatedTable.TableName, this.RelatedTable.Namespace, false);
			}
			if (num < 0)
			{
				return null;
			}
			DataTable dataTable2 = destination.Tables[num];
			int num2 = this.Columns.Length;
			DataColumn[] array = new DataColumn[num2];
			DataColumn[] array2 = new DataColumn[num2];
			for (int i = 0; i < num2; i++)
			{
				DataColumn dataColumn = this.Columns[i];
				num = dataTable.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array[i] = dataTable.Columns[num];
				dataColumn = this.RelatedColumnsReference[i];
				num = dataTable2.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array2[i] = dataTable2.Columns[num];
			}
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(this.ConstraintName, array2, array);
			foreignKeyConstraint.UpdateRule = this.UpdateRule;
			foreignKeyConstraint.DeleteRule = this.DeleteRule;
			foreignKeyConstraint.AcceptRejectRule = this.AcceptRejectRule;
			foreach (object key in base.ExtendedProperties.Keys)
			{
				foreignKeyConstraint.ExtendedProperties[key] = base.ExtendedProperties[key];
			}
			return foreignKeyConstraint;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00210AE8 File Offset: 0x0020FEE8
		internal ForeignKeyConstraint Clone(DataTable destination)
		{
			int num = this.Columns.Length;
			DataColumn[] array = new DataColumn[num];
			DataColumn[] array2 = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				DataColumn dataColumn = this.Columns[i];
				int num2 = destination.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array[i] = destination.Columns[num2];
				dataColumn = this.RelatedColumnsReference[i];
				num2 = destination.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array2[i] = destination.Columns[num2];
			}
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(this.ConstraintName, array2, array);
			foreignKeyConstraint.UpdateRule = this.UpdateRule;
			foreignKeyConstraint.DeleteRule = this.DeleteRule;
			foreignKeyConstraint.AcceptRejectRule = this.AcceptRejectRule;
			foreach (object key in base.ExtendedProperties.Keys)
			{
				foreignKeyConstraint.ExtendedProperties[key] = base.ExtendedProperties[key];
			}
			return foreignKeyConstraint;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00210C38 File Offset: 0x00210038
		private void Create(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			if (parentColumns.Length == 0 || childColumns.Length == 0)
			{
				throw ExceptionBuilder.KeyLengthZero();
			}
			if (parentColumns.Length != childColumns.Length)
			{
				throw ExceptionBuilder.KeyLengthMismatch();
			}
			for (int i = 0; i < parentColumns.Length; i++)
			{
				if (parentColumns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(parentColumns[i]);
				}
				if (childColumns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(childColumns[i]);
				}
			}
			this.parentKey = new DataKey(parentColumns, true);
			this.childKey = new DataKey(childColumns, true);
			this.ConstraintName = relationName;
			this.NonVirtualCheckState();
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00210CC8 File Offset: 0x002100C8
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x00210CE8 File Offset: 0x002100E8
		[DefaultValue(Rule.Cascade)]
		[ResDescription("ForeignKeyConstraintDeleteRuleDescr")]
		[ResCategory("DataCategory_Data")]
		public virtual Rule DeleteRule
		{
			get
			{
				base.CheckStateForProperty();
				return this.deleteRule;
			}
			set
			{
				switch (value)
				{
				case Rule.None:
				case Rule.Cascade:
				case Rule.SetNull:
				case Rule.SetDefault:
					this.deleteRule = value;
					return;
				default:
					throw ADP.InvalidRule(value);
				}
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00210D28 File Offset: 0x00210128
		public override bool Equals(object key)
		{
			if (!(key is ForeignKeyConstraint))
			{
				return false;
			}
			ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)key;
			return this.ParentKey.ColumnsEqual(foreignKeyConstraint.ParentKey) && this.ChildKey.ColumnsEqual(foreignKeyConstraint.ChildKey);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00210D78 File Offset: 0x00210178
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00210D98 File Offset: 0x00210198
		[ReadOnly(true)]
		[ResDescription("ForeignKeyConstraintParentColumnsDescr")]
		[ResCategory("DataCategory_Data")]
		public virtual DataColumn[] RelatedColumns
		{
			get
			{
				base.CheckStateForProperty();
				return this.parentKey.ToArray();
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00210DB8 File Offset: 0x002101B8
		internal DataColumn[] RelatedColumnsReference
		{
			get
			{
				base.CheckStateForProperty();
				return this.parentKey.ColumnsReference;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00210DD8 File Offset: 0x002101D8
		internal DataKey ParentKey
		{
			get
			{
				base.CheckStateForProperty();
				return this.parentKey;
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00210DF8 File Offset: 0x002101F8
		internal DataRelation FindParentRelation()
		{
			DataRelationCollection parentRelations = this.Table.ParentRelations;
			for (int i = 0; i < parentRelations.Count; i++)
			{
				if (parentRelations[i].ChildKeyConstraint == this)
				{
					return parentRelations[i];
				}
			}
			return null;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00210E48 File Offset: 0x00210248
		[ResDescription("ForeignKeyRelatedTableDescr")]
		[ReadOnly(true)]
		[ResCategory("DataCategory_Data")]
		public virtual DataTable RelatedTable
		{
			get
			{
				base.CheckStateForProperty();
				return this.parentKey.Table;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00210E68 File Offset: 0x00210268
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x00210E88 File Offset: 0x00210288
		[ResDescription("ForeignKeyConstraintUpdateRuleDescr")]
		[DefaultValue(Rule.Cascade)]
		[ResCategory("DataCategory_Data")]
		public virtual Rule UpdateRule
		{
			get
			{
				base.CheckStateForProperty();
				return this.updateRule;
			}
			set
			{
				switch (value)
				{
				case Rule.None:
				case Rule.Cascade:
				case Rule.SetNull:
				case Rule.SetDefault:
					this.updateRule = value;
					return;
				default:
					throw ADP.InvalidRule(value);
				}
			}
		}

		// Token: 0x0400089E RID: 2206
		internal const Rule Rule_Default = Rule.Cascade;

		// Token: 0x0400089F RID: 2207
		internal const AcceptRejectRule AcceptRejectRule_Default = AcceptRejectRule.None;

		// Token: 0x040008A0 RID: 2208
		internal Rule deleteRule;

		// Token: 0x040008A1 RID: 2209
		internal Rule updateRule;

		// Token: 0x040008A2 RID: 2210
		internal AcceptRejectRule acceptRejectRule;

		// Token: 0x040008A3 RID: 2211
		private DataKey childKey;

		// Token: 0x040008A4 RID: 2212
		private DataKey parentKey;

		// Token: 0x040008A5 RID: 2213
		internal string constraintName;

		// Token: 0x040008A6 RID: 2214
		internal string[] parentColumnNames;

		// Token: 0x040008A7 RID: 2215
		internal string[] childColumnNames;

		// Token: 0x040008A8 RID: 2216
		internal string parentTableName;

		// Token: 0x040008A9 RID: 2217
		internal string parentTableNamespace;
	}
}

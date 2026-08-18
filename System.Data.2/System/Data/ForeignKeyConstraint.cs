using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x020000FD RID: 253
	[Editor("Microsoft.VSDesigner.Data.Design.ForeignKeyConstraintEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("ConstraintName")]
	public class ForeignKeyConstraint : Constraint
	{
		// Token: 0x0600102B RID: 4139 RVA: 0x00080C28 File Offset: 0x00080028
		public ForeignKeyConstraint(DataColumn parentColumn, DataColumn childColumn) : this(null, parentColumn, childColumn)
		{
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00080C40 File Offset: 0x00080040
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

		// Token: 0x0600102D RID: 4141 RVA: 0x00080C80 File Offset: 0x00080080
		public ForeignKeyConstraint(DataColumn[] parentColumns, DataColumn[] childColumns) : this(null, parentColumns, childColumns)
		{
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00080C98 File Offset: 0x00080098
		public ForeignKeyConstraint(string constraintName, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			this.deleteRule = Rule.Cascade;
			this.updateRule = Rule.Cascade;
			base..ctor();
			this.Create(constraintName, parentColumns, childColumns);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00080CC4 File Offset: 0x000800C4
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

		// Token: 0x06001030 RID: 4144 RVA: 0x00080D1C File Offset: 0x0008011C
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

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00080D7C File Offset: 0x0008017C
		internal DataKey ChildKey
		{
			get
			{
				base.CheckStateForProperty();
				return this.childKey;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00080D98 File Offset: 0x00080198
		[ResDescription("ForeignKeyConstraintChildColumnsDescr")]
		[ReadOnly(true)]
		[ResCategory("DataCategory_Data")]
		public virtual DataColumn[] Columns
		{
			get
			{
				base.CheckStateForProperty();
				return this.childKey.ToArray();
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00080DB8 File Offset: 0x000801B8
		[ResCategory("DataCategory_Data")]
		[ReadOnly(true)]
		[ResDescription("ConstraintTableDescr")]
		public override DataTable Table
		{
			get
			{
				base.CheckStateForProperty();
				return this.childKey.Table;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x00080DD8 File Offset: 0x000801D8
		internal string[] ParentColumnNames
		{
			get
			{
				return this.parentKey.GetColumnNames();
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00080DF0 File Offset: 0x000801F0
		internal string[] ChildColumnNames
		{
			get
			{
				return this.childKey.GetColumnNames();
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00080E08 File Offset: 0x00080208
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

		// Token: 0x06001037 RID: 4151 RVA: 0x00080E70 File Offset: 0x00080270
		internal override bool CanBeRemovedFromCollection(ConstraintCollection constraints, bool fThrowException)
		{
			return true;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00080E80 File Offset: 0x00080280
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

		// Token: 0x06001039 RID: 4153 RVA: 0x00080EA8 File Offset: 0x000802A8
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

		// Token: 0x0600103A RID: 4154 RVA: 0x00080F58 File Offset: 0x00080358
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

		// Token: 0x0600103B RID: 4155 RVA: 0x00080FD4 File Offset: 0x000803D4
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

		// Token: 0x0600103C RID: 4156 RVA: 0x00081088 File Offset: 0x00080488
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

		// Token: 0x0600103D RID: 4157 RVA: 0x000812A8 File Offset: 0x000806A8
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

		// Token: 0x0600103E RID: 4158 RVA: 0x000813A0 File Offset: 0x000807A0
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

		// Token: 0x0600103F RID: 4159 RVA: 0x0008159C File Offset: 0x0008099C
		internal void CheckCanClearParentTable(DataTable table)
		{
			if (this.Table.DataSet.EnforceConstraints && this.Table.Rows.Count > 0)
			{
				throw ExceptionBuilder.FailedClearParentTable(table.TableName, this.ConstraintName, this.Table.TableName);
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x000815EC File Offset: 0x000809EC
		internal void CheckCanRemoveParentRow(DataRow row)
		{
			if (!this.Table.DataSet.EnforceConstraints)
			{
				return;
			}
			if (DataRelation.GetChildRows(this.ParentKey, this.ChildKey, row, DataRowVersion.Default).Length != 0)
			{
				throw ExceptionBuilder.RemoveParentRow(this);
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00081630 File Offset: 0x00080A30
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

		// Token: 0x06001042 RID: 4162 RVA: 0x000816BC File Offset: 0x00080ABC
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

		// Token: 0x06001043 RID: 4163 RVA: 0x0008184C File Offset: 0x00080C4C
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

		// Token: 0x06001044 RID: 4164 RVA: 0x0008197C File Offset: 0x00080D7C
		internal override void CheckState()
		{
			this.NonVirtualCheckState();
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x00081990 File Offset: 0x00080D90
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x000819AC File Offset: 0x00080DAC
		[DefaultValue(AcceptRejectRule.None)]
		[ResDescription("ForeignKeyConstraintAcceptRejectRuleDescr")]
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
				if (value <= AcceptRejectRule.Cascade)
				{
					this.acceptRejectRule = value;
					return;
				}
				throw ADP.InvalidAcceptRejectRule(value);
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000819CC File Offset: 0x00080DCC
		internal override bool ContainsColumn(DataColumn column)
		{
			return this.parentKey.ContainsColumn(column) || this.childKey.ContainsColumn(column);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000819F8 File Offset: 0x00080DF8
		internal override Constraint Clone(DataSet destination)
		{
			return this.Clone(destination, false);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00081A10 File Offset: 0x00080E10
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

		// Token: 0x0600104A RID: 4170 RVA: 0x00081BFC File Offset: 0x00080FFC
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

		// Token: 0x0600104B RID: 4171 RVA: 0x00081D40 File Offset: 0x00081140
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

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00081DC4 File Offset: 0x000811C4
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x00081DE0 File Offset: 0x000811E0
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
				if (value <= Rule.SetDefault)
				{
					this.deleteRule = value;
					return;
				}
				throw ADP.InvalidRule(value);
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00081E00 File Offset: 0x00081200
		public override bool Equals(object key)
		{
			if (!(key is ForeignKeyConstraint))
			{
				return false;
			}
			ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)key;
			return this.ParentKey.ColumnsEqual(foreignKeyConstraint.ParentKey) && this.ChildKey.ColumnsEqual(foreignKeyConstraint.ChildKey);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00081E4C File Offset: 0x0008124C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x00081E60 File Offset: 0x00081260
		[ResCategory("DataCategory_Data")]
		[ReadOnly(true)]
		[ResDescription("ForeignKeyConstraintParentColumnsDescr")]
		public virtual DataColumn[] RelatedColumns
		{
			get
			{
				base.CheckStateForProperty();
				return this.parentKey.ToArray();
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x00081E80 File Offset: 0x00081280
		internal DataColumn[] RelatedColumnsReference
		{
			get
			{
				base.CheckStateForProperty();
				return this.parentKey.ColumnsReference;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x00081EA0 File Offset: 0x000812A0
		internal DataKey ParentKey
		{
			get
			{
				base.CheckStateForProperty();
				return this.parentKey;
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00081EBC File Offset: 0x000812BC
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

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00081F00 File Offset: 0x00081300
		[ReadOnly(true)]
		[ResDescription("ForeignKeyRelatedTableDescr")]
		[ResCategory("DataCategory_Data")]
		public virtual DataTable RelatedTable
		{
			get
			{
				base.CheckStateForProperty();
				return this.parentKey.Table;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00081F20 File Offset: 0x00081320
		// (set) Token: 0x06001056 RID: 4182 RVA: 0x00081F3C File Offset: 0x0008133C
		[DefaultValue(Rule.Cascade)]
		[ResDescription("ForeignKeyConstraintUpdateRuleDescr")]
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
				if (value <= Rule.SetDefault)
				{
					this.updateRule = value;
					return;
				}
				throw ADP.InvalidRule(value);
			}
		}

		// Token: 0x04000568 RID: 1384
		internal const Rule Rule_Default = Rule.Cascade;

		// Token: 0x04000569 RID: 1385
		internal const AcceptRejectRule AcceptRejectRule_Default = AcceptRejectRule.None;

		// Token: 0x0400056A RID: 1386
		internal Rule deleteRule;

		// Token: 0x0400056B RID: 1387
		internal Rule updateRule;

		// Token: 0x0400056C RID: 1388
		internal AcceptRejectRule acceptRejectRule;

		// Token: 0x0400056D RID: 1389
		private DataKey childKey;

		// Token: 0x0400056E RID: 1390
		private DataKey parentKey;

		// Token: 0x0400056F RID: 1391
		internal string constraintName;

		// Token: 0x04000570 RID: 1392
		internal string[] parentColumnNames;

		// Token: 0x04000571 RID: 1393
		internal string[] childColumnNames;

		// Token: 0x04000572 RID: 1394
		internal string parentTableName;

		// Token: 0x04000573 RID: 1395
		internal string parentTableNamespace;
	}
}

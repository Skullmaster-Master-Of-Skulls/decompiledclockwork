using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x020000C8 RID: 200
	internal sealed class Merger
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x00210F38 File Offset: 0x00210338
		internal Merger(DataSet dataSet, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			this.dataSet = dataSet;
			this.preserveChanges = preserveChanges;
			if (missingSchemaAction == MissingSchemaAction.AddWithKey)
			{
				this.missingSchemaAction = MissingSchemaAction.Add;
				return;
			}
			this.missingSchemaAction = missingSchemaAction;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00210F78 File Offset: 0x00210378
		internal Merger(DataTable dataTable, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			this.isStandAlonetable = true;
			this.dataTable = dataTable;
			this.preserveChanges = preserveChanges;
			if (missingSchemaAction == MissingSchemaAction.AddWithKey)
			{
				this.missingSchemaAction = MissingSchemaAction.Add;
				return;
			}
			this.missingSchemaAction = missingSchemaAction;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00210FB8 File Offset: 0x002103B8
		internal void MergeDataSet(DataSet source)
		{
			if (source == this.dataSet)
			{
				return;
			}
			bool enforceConstraints = this.dataSet.EnforceConstraints;
			this.dataSet.EnforceConstraints = false;
			this._IgnoreNSforTableLookup = (this.dataSet.namespaceURI != source.namespaceURI);
			List<DataColumn> list = null;
			if (MissingSchemaAction.Add == this.missingSchemaAction)
			{
				list = new List<DataColumn>();
				foreach (object obj in this.dataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					foreach (object obj2 in dataTable.Columns)
					{
						DataColumn item = (DataColumn)obj2;
						list.Add(item);
					}
				}
			}
			for (int i = 0; i < source.Tables.Count; i++)
			{
				this.MergeTableData(source.Tables[i]);
			}
			if (MissingSchemaAction.Ignore != this.missingSchemaAction)
			{
				this.MergeConstraints(source);
				for (int j = 0; j < source.Relations.Count; j++)
				{
					this.MergeRelation(source.Relations[j]);
				}
			}
			if (MissingSchemaAction.Add == this.missingSchemaAction)
			{
				foreach (object obj3 in source.Tables)
				{
					DataTable dataTable2 = (DataTable)obj3;
					DataTable dataTable3;
					if (this._IgnoreNSforTableLookup)
					{
						dataTable3 = this.dataSet.Tables[dataTable2.TableName];
					}
					else
					{
						dataTable3 = this.dataSet.Tables[dataTable2.TableName, dataTable2.Namespace];
					}
					foreach (object obj4 in dataTable2.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj4;
						if (dataColumn.Computed)
						{
							DataColumn dataColumn2 = dataTable3.Columns[dataColumn.ColumnName];
							if (!list.Contains(dataColumn2))
							{
								dataColumn2.Expression = dataColumn.Expression;
							}
						}
					}
				}
			}
			this.MergeExtendedProperties(source.ExtendedProperties, this.dataSet.ExtendedProperties);
			foreach (object obj5 in this.dataSet.Tables)
			{
				DataTable dataTable4 = (DataTable)obj5;
				dataTable4.EvaluateExpressions();
			}
			this.dataSet.EnforceConstraints = enforceConstraints;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x002112F8 File Offset: 0x002106F8
		internal void MergeTable(DataTable src)
		{
			bool enforceConstraints = false;
			if (!this.isStandAlonetable)
			{
				if (src.DataSet == this.dataSet)
				{
					return;
				}
				enforceConstraints = this.dataSet.EnforceConstraints;
				this.dataSet.EnforceConstraints = false;
			}
			else
			{
				if (src == this.dataTable)
				{
					return;
				}
				this.dataTable.SuspendEnforceConstraints = true;
			}
			if (this.dataSet != null)
			{
				if (src.DataSet == null || src.DataSet.namespaceURI != this.dataSet.namespaceURI)
				{
					this._IgnoreNSforTableLookup = true;
				}
			}
			else if (this.dataTable.DataSet == null || src.DataSet == null || src.DataSet.namespaceURI != this.dataTable.DataSet.namespaceURI)
			{
				this._IgnoreNSforTableLookup = true;
			}
			this.MergeTableData(src);
			DataTable dataTable = this.dataTable;
			if (dataTable == null && this.dataSet != null)
			{
				if (this._IgnoreNSforTableLookup)
				{
					dataTable = this.dataSet.Tables[src.TableName];
				}
				else
				{
					dataTable = this.dataSet.Tables[src.TableName, src.Namespace];
				}
			}
			if (dataTable != null)
			{
				dataTable.EvaluateExpressions();
			}
			if (!this.isStandAlonetable)
			{
				this.dataSet.EnforceConstraints = enforceConstraints;
				return;
			}
			this.dataTable.SuspendEnforceConstraints = false;
			try
			{
				if (this.dataTable.EnforceConstraints)
				{
					this.dataTable.EnableConstraints();
				}
			}
			catch (ConstraintException)
			{
				if (this.dataTable.DataSet != null)
				{
					this.dataTable.DataSet.EnforceConstraints = false;
				}
				throw;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x002114A8 File Offset: 0x002108A8
		private void MergeTable(DataTable src, DataTable dst)
		{
			int count = src.Rows.Count;
			bool flag = dst.Rows.Count == 0;
			if (0 < count)
			{
				Index index = null;
				DataKey key = default(DataKey);
				dst.SuspendIndexEvents();
				try
				{
					if (!flag && dst.primaryKey != null)
					{
						key = this.GetSrcKey(src, dst);
						if (key.HasValue)
						{
							index = dst.primaryKey.Key.GetSortIndex(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedOriginal);
						}
					}
					foreach (object obj in src.Rows)
					{
						DataRow row = (DataRow)obj;
						DataRow targetRow = null;
						if (index != null)
						{
							targetRow = dst.FindMergeTarget(row, key, index);
						}
						dst.MergeRow(row, targetRow, this.preserveChanges, index);
					}
				}
				finally
				{
					dst.RestoreIndexEvents(true);
				}
			}
			this.MergeExtendedProperties(src.ExtendedProperties, dst.ExtendedProperties);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x002115C8 File Offset: 0x002109C8
		internal void MergeRows(DataRow[] rows)
		{
			DataTable dataTable = null;
			DataTable dataTable2 = null;
			DataKey key = default(DataKey);
			Index index = null;
			bool enforceConstraints = this.dataSet.EnforceConstraints;
			this.dataSet.EnforceConstraints = false;
			for (int i = 0; i < rows.Length; i++)
			{
				DataRow dataRow = rows[i];
				if (dataRow == null)
				{
					throw ExceptionBuilder.ArgumentNull("rows[" + i + "]");
				}
				if (dataRow.Table == null)
				{
					throw ExceptionBuilder.ArgumentNull("rows[" + i + "].Table");
				}
				if (dataRow.Table.DataSet != this.dataSet)
				{
					if (dataTable != dataRow.Table)
					{
						dataTable = dataRow.Table;
						dataTable2 = this.MergeSchema(dataRow.Table);
						if (dataTable2 == null)
						{
							this.dataSet.EnforceConstraints = enforceConstraints;
							return;
						}
						if (dataTable2.primaryKey != null)
						{
							key = this.GetSrcKey(dataTable, dataTable2);
						}
						if (key.HasValue)
						{
							if (index != null)
							{
								index.RemoveRef();
							}
							index = new Index(dataTable2, dataTable2.primaryKey.Key.GetIndexDesc(), DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedOriginal, null);
							index.AddRef();
							index.AddRef();
						}
					}
					if (dataRow.newRecord != -1 || dataRow.oldRecord != -1)
					{
						DataRow dataRow2 = null;
						if (0 < dataTable2.Rows.Count && index != null)
						{
							dataRow2 = dataTable2.FindMergeTarget(dataRow, key, index);
						}
						dataRow2 = dataTable2.MergeRow(dataRow, dataRow2, this.preserveChanges, index);
						if (dataRow2.Table.dependentColumns != null && dataRow2.Table.dependentColumns.Count > 0)
						{
							dataRow2.Table.EvaluateExpressions(dataRow2, DataRowAction.Change, null);
						}
					}
				}
			}
			if (index != null)
			{
				index.RemoveRef();
			}
			this.dataSet.EnforceConstraints = enforceConstraints;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00211788 File Offset: 0x00210B88
		private DataTable MergeSchema(DataTable table)
		{
			DataTable dataTable = null;
			if (!this.isStandAlonetable)
			{
				if (this.dataSet.Tables.Contains(table.TableName, true))
				{
					if (this._IgnoreNSforTableLookup)
					{
						dataTable = this.dataSet.Tables[table.TableName];
					}
					else
					{
						dataTable = this.dataSet.Tables[table.TableName, table.Namespace];
					}
				}
			}
			else
			{
				dataTable = this.dataTable;
			}
			if (dataTable == null)
			{
				if (MissingSchemaAction.Add == this.missingSchemaAction)
				{
					dataTable = table.Clone(table.DataSet);
					this.dataSet.Tables.Add(dataTable);
				}
				else if (MissingSchemaAction.Error == this.missingSchemaAction)
				{
					throw ExceptionBuilder.MergeMissingDefinition(table.TableName);
				}
			}
			else
			{
				if (MissingSchemaAction.Ignore != this.missingSchemaAction)
				{
					int count = dataTable.Columns.Count;
					for (int i = 0; i < table.Columns.Count; i++)
					{
						DataColumn dataColumn = table.Columns[i];
						DataColumn dataColumn2 = dataTable.Columns.Contains(dataColumn.ColumnName, true) ? dataTable.Columns[dataColumn.ColumnName] : null;
						if (dataColumn2 == null)
						{
							if (MissingSchemaAction.Add == this.missingSchemaAction)
							{
								dataColumn2 = dataColumn.Clone();
								dataTable.Columns.Add(dataColumn2);
							}
							else
							{
								if (this.isStandAlonetable)
								{
									throw ExceptionBuilder.MergeFailed(Res.GetString("DataMerge_MissingColumnDefinition", new object[]
									{
										table.TableName,
										dataColumn.ColumnName
									}));
								}
								this.dataSet.RaiseMergeFailed(dataTable, Res.GetString("DataMerge_MissingColumnDefinition", new object[]
								{
									table.TableName,
									dataColumn.ColumnName
								}), this.missingSchemaAction);
							}
						}
						else
						{
							if (dataColumn2.DataType != dataColumn.DataType || (dataColumn2.DataType == typeof(DateTime) && dataColumn2.DateTimeMode != dataColumn.DateTimeMode && (dataColumn2.DateTimeMode & dataColumn.DateTimeMode) != DataSetDateTime.Unspecified))
							{
								if (this.isStandAlonetable)
								{
									throw ExceptionBuilder.MergeFailed(Res.GetString("DataMerge_DataTypeMismatch", new object[]
									{
										dataColumn.ColumnName
									}));
								}
								this.dataSet.RaiseMergeFailed(dataTable, Res.GetString("DataMerge_DataTypeMismatch", new object[]
								{
									dataColumn.ColumnName
								}), MissingSchemaAction.Error);
							}
							this.MergeExtendedProperties(dataColumn.ExtendedProperties, dataColumn2.ExtendedProperties);
						}
					}
					if (this.isStandAlonetable)
					{
						for (int j = count; j < dataTable.Columns.Count; j++)
						{
							dataTable.Columns[j].Expression = table.Columns[dataTable.Columns[j].ColumnName].Expression;
						}
					}
					DataColumn[] primaryKey = dataTable.PrimaryKey;
					DataColumn[] primaryKey2 = table.PrimaryKey;
					if (primaryKey.Length != primaryKey2.Length)
					{
						if (primaryKey.Length == 0)
						{
							DataColumn[] array = new DataColumn[primaryKey2.Length];
							for (int k = 0; k < primaryKey2.Length; k++)
							{
								array[k] = dataTable.Columns[primaryKey2[k].ColumnName];
							}
							dataTable.PrimaryKey = array;
						}
						else if (primaryKey2.Length != 0)
						{
							this.dataSet.RaiseMergeFailed(dataTable, Res.GetString("DataMerge_PrimaryKeyMismatch"), this.missingSchemaAction);
						}
					}
					else
					{
						for (int l = 0; l < primaryKey.Length; l++)
						{
							if (string.Compare(primaryKey[l].ColumnName, primaryKey2[l].ColumnName, false, dataTable.Locale) != 0)
							{
								this.dataSet.RaiseMergeFailed(table, Res.GetString("DataMerge_PrimaryKeyColumnsMismatch", new object[]
								{
									primaryKey[l].ColumnName,
									primaryKey2[l].ColumnName
								}), this.missingSchemaAction);
							}
						}
					}
				}
				this.MergeExtendedProperties(table.ExtendedProperties, dataTable.ExtendedProperties);
			}
			return dataTable;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00211B68 File Offset: 0x00210F68
		private void MergeTableData(DataTable src)
		{
			DataTable dataTable = this.MergeSchema(src);
			if (dataTable == null)
			{
				return;
			}
			dataTable.MergingData = true;
			try
			{
				this.MergeTable(src, dataTable);
			}
			finally
			{
				dataTable.MergingData = false;
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00211BB8 File Offset: 0x00210FB8
		private void MergeConstraints(DataSet source)
		{
			for (int i = 0; i < source.Tables.Count; i++)
			{
				this.MergeConstraints(source.Tables[i]);
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00211BF8 File Offset: 0x00210FF8
		private void MergeConstraints(DataTable table)
		{
			for (int i = 0; i < table.Constraints.Count; i++)
			{
				Constraint constraint = table.Constraints[i];
				Constraint constraint2 = constraint.Clone(this.dataSet, this._IgnoreNSforTableLookup);
				if (constraint2 == null)
				{
					this.dataSet.RaiseMergeFailed(table, Res.GetString("DataMerge_MissingConstraint", new object[]
					{
						constraint.GetType().FullName,
						constraint.ConstraintName
					}), this.missingSchemaAction);
				}
				else
				{
					Constraint constraint3 = constraint2.Table.Constraints.FindConstraint(constraint2);
					if (constraint3 == null)
					{
						if (MissingSchemaAction.Add == this.missingSchemaAction)
						{
							try
							{
								constraint2.Table.Constraints.Add(constraint2);
								goto IL_11F;
							}
							catch (DuplicateNameException)
							{
								constraint2.ConstraintName = "";
								constraint2.Table.Constraints.Add(constraint2);
								goto IL_11F;
							}
						}
						if (MissingSchemaAction.Error == this.missingSchemaAction)
						{
							this.dataSet.RaiseMergeFailed(table, Res.GetString("DataMerge_MissingConstraint", new object[]
							{
								constraint.GetType().FullName,
								constraint.ConstraintName
							}), this.missingSchemaAction);
						}
					}
					else
					{
						this.MergeExtendedProperties(constraint.ExtendedProperties, constraint3.ExtendedProperties);
					}
				}
				IL_11F:;
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00211D58 File Offset: 0x00211158
		private void MergeRelation(DataRelation relation)
		{
			DataRelation dataRelation = null;
			int num = this.dataSet.Relations.InternalIndexOf(relation.RelationName);
			if (num < 0)
			{
				if (MissingSchemaAction.Add == this.missingSchemaAction)
				{
					DataTable dataTable;
					if (this._IgnoreNSforTableLookup)
					{
						dataTable = this.dataSet.Tables[relation.ParentTable.TableName];
					}
					else
					{
						dataTable = this.dataSet.Tables[relation.ParentTable.TableName, relation.ParentTable.Namespace];
					}
					DataTable dataTable2;
					if (this._IgnoreNSforTableLookup)
					{
						dataTable2 = this.dataSet.Tables[relation.ChildTable.TableName];
					}
					else
					{
						dataTable2 = this.dataSet.Tables[relation.ChildTable.TableName, relation.ChildTable.Namespace];
					}
					DataColumn[] array = new DataColumn[relation.ParentKey.ColumnsReference.Length];
					DataColumn[] array2 = new DataColumn[relation.ParentKey.ColumnsReference.Length];
					for (int i = 0; i < relation.ParentKey.ColumnsReference.Length; i++)
					{
						array[i] = dataTable.Columns[relation.ParentKey.ColumnsReference[i].ColumnName];
						array2[i] = dataTable2.Columns[relation.ChildKey.ColumnsReference[i].ColumnName];
					}
					try
					{
						dataRelation = new DataRelation(relation.RelationName, array, array2, relation.createConstraints);
						dataRelation.Nested = relation.Nested;
						this.dataSet.Relations.Add(dataRelation);
						goto IL_34C;
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
						ExceptionBuilder.TraceExceptionForCapture(ex);
						this.dataSet.RaiseMergeFailed(null, ex.Message, this.missingSchemaAction);
						goto IL_34C;
					}
				}
				throw ExceptionBuilder.MergeMissingDefinition(relation.RelationName);
			}
			dataRelation = this.dataSet.Relations[num];
			if (relation.ParentKey.ColumnsReference.Length != dataRelation.ParentKey.ColumnsReference.Length)
			{
				this.dataSet.RaiseMergeFailed(null, Res.GetString("DataMerge_MissingDefinition", new object[]
				{
					relation.RelationName
				}), this.missingSchemaAction);
			}
			for (int j = 0; j < relation.ParentKey.ColumnsReference.Length; j++)
			{
				DataColumn dataColumn = dataRelation.ParentKey.ColumnsReference[j];
				DataColumn dataColumn2 = relation.ParentKey.ColumnsReference[j];
				if (string.Compare(dataColumn.ColumnName, dataColumn2.ColumnName, false, dataColumn.Table.Locale) != 0)
				{
					this.dataSet.RaiseMergeFailed(null, Res.GetString("DataMerge_ReltionKeyColumnsMismatch", new object[]
					{
						relation.RelationName
					}), this.missingSchemaAction);
				}
				dataColumn = dataRelation.ChildKey.ColumnsReference[j];
				dataColumn2 = relation.ChildKey.ColumnsReference[j];
				if (string.Compare(dataColumn.ColumnName, dataColumn2.ColumnName, false, dataColumn.Table.Locale) != 0)
				{
					this.dataSet.RaiseMergeFailed(null, Res.GetString("DataMerge_ReltionKeyColumnsMismatch", new object[]
					{
						relation.RelationName
					}), this.missingSchemaAction);
				}
			}
			IL_34C:
			this.MergeExtendedProperties(relation.ExtendedProperties, dataRelation.ExtendedProperties);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x002120E8 File Offset: 0x002114E8
		private void MergeExtendedProperties(PropertyCollection src, PropertyCollection dst)
		{
			if (MissingSchemaAction.Ignore == this.missingSchemaAction)
			{
				return;
			}
			IDictionaryEnumerator enumerator = src.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!this.preserveChanges || dst[enumerator.Key] == null)
				{
					dst[enumerator.Key] = enumerator.Value;
				}
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00212138 File Offset: 0x00211538
		private DataKey GetSrcKey(DataTable src, DataTable dst)
		{
			if (src.primaryKey != null)
			{
				return src.primaryKey.Key;
			}
			DataKey result = default(DataKey);
			if (dst.primaryKey != null)
			{
				DataColumn[] columnsReference = dst.primaryKey.Key.ColumnsReference;
				DataColumn[] array = new DataColumn[columnsReference.Length];
				for (int i = 0; i < columnsReference.Length; i++)
				{
					array[i] = src.Columns[columnsReference[i].ColumnName];
				}
				result = new DataKey(array, false);
			}
			return result;
		}

		// Token: 0x040008BD RID: 2237
		private DataSet dataSet;

		// Token: 0x040008BE RID: 2238
		private DataTable dataTable;

		// Token: 0x040008BF RID: 2239
		private bool preserveChanges;

		// Token: 0x040008C0 RID: 2240
		private MissingSchemaAction missingSchemaAction;

		// Token: 0x040008C1 RID: 2241
		private bool isStandAlonetable;

		// Token: 0x040008C2 RID: 2242
		private bool _IgnoreNSforTableLookup;
	}
}

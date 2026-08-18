using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000EA RID: 234
	[DefaultProperty("ConstraintName")]
	[Editor("Microsoft.VSDesigner.Data.Design.UniqueConstraintEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class UniqueConstraint : Constraint
	{
		// Token: 0x06000D9D RID: 3485 RVA: 0x002174B8 File Offset: 0x002168B8
		public UniqueConstraint(string name, DataColumn column)
		{
			this.Create(name, new DataColumn[]
			{
				column
			});
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x002174E8 File Offset: 0x002168E8
		public UniqueConstraint(DataColumn column)
		{
			this.Create(null, new DataColumn[]
			{
				column
			});
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00217518 File Offset: 0x00216918
		public UniqueConstraint(string name, DataColumn[] columns)
		{
			this.Create(name, columns);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00217538 File Offset: 0x00216938
		public UniqueConstraint(DataColumn[] columns)
		{
			this.Create(null, columns);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00217558 File Offset: 0x00216958
		[Browsable(false)]
		public UniqueConstraint(string name, string[] columnNames, bool isPrimaryKey)
		{
			this.constraintName = name;
			this.columnNames = columnNames;
			this.bPrimaryKey = isPrimaryKey;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00217588 File Offset: 0x00216988
		public UniqueConstraint(string name, DataColumn column, bool isPrimaryKey)
		{
			DataColumn[] columns = new DataColumn[]
			{
				column
			};
			this.bPrimaryKey = isPrimaryKey;
			this.Create(name, columns);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x002175B8 File Offset: 0x002169B8
		public UniqueConstraint(DataColumn column, bool isPrimaryKey)
		{
			DataColumn[] columns = new DataColumn[]
			{
				column
			};
			this.bPrimaryKey = isPrimaryKey;
			this.Create(null, columns);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x002175E8 File Offset: 0x002169E8
		public UniqueConstraint(string name, DataColumn[] columns, bool isPrimaryKey)
		{
			this.bPrimaryKey = isPrimaryKey;
			this.Create(name, columns);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00217618 File Offset: 0x00216A18
		public UniqueConstraint(DataColumn[] columns, bool isPrimaryKey)
		{
			this.bPrimaryKey = isPrimaryKey;
			this.Create(null, columns);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00217648 File Offset: 0x00216A48
		internal string[] ColumnNames
		{
			get
			{
				return this.key.GetColumnNames();
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00217668 File Offset: 0x00216A68
		internal Index ConstraintIndex
		{
			get
			{
				return this._constraintIndex;
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00217688 File Offset: 0x00216A88
		internal void ConstraintIndexClear()
		{
			if (this._constraintIndex != null)
			{
				this._constraintIndex.RemoveRef();
				this._constraintIndex = null;
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x002176B8 File Offset: 0x00216AB8
		internal void ConstraintIndexInitialize()
		{
			if (this._constraintIndex == null)
			{
				this._constraintIndex = this.key.GetSortIndex();
				this._constraintIndex.AddRef();
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x002176F8 File Offset: 0x00216AF8
		internal override void CheckState()
		{
			this.NonVirtualCheckState();
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00217718 File Offset: 0x00216B18
		private void NonVirtualCheckState()
		{
			this.key.CheckState();
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00217738 File Offset: 0x00216B38
		internal override void CheckCanAddToCollection(ConstraintCollection constraints)
		{
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00217748 File Offset: 0x00216B48
		internal override bool CanBeRemovedFromCollection(ConstraintCollection constraints, bool fThrowException)
		{
			if (!this.Equals(constraints.Table.primaryKey))
			{
				ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this.Table.DataSet, this.Table);
				while (parentForeignKeyConstraintEnumerator.GetNext())
				{
					ForeignKeyConstraint foreignKeyConstraint = parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
					if (this.key.ColumnsEqual(foreignKeyConstraint.ParentKey))
					{
						if (!fThrowException)
						{
							return false;
						}
						throw ExceptionBuilder.NeededForForeignKeyConstraint(this, foreignKeyConstraint);
					}
				}
				return true;
			}
			if (!fThrowException)
			{
				return false;
			}
			throw ExceptionBuilder.RemovePrimaryKey(constraints.Table);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x002177C8 File Offset: 0x00216BC8
		internal override bool CanEnableConstraint()
		{
			return !this.Table.EnforceConstraints || this.ConstraintIndex.CheckUnique();
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x002177F8 File Offset: 0x00216BF8
		internal override bool IsConstraintViolated()
		{
			bool result = false;
			Index constraintIndex = this.ConstraintIndex;
			if (constraintIndex.HasDuplicates)
			{
				object[] uniqueKeyValues = constraintIndex.GetUniqueKeyValues();
				for (int i = 0; i < uniqueKeyValues.Length; i++)
				{
					Range range = constraintIndex.FindRecords((object[])uniqueKeyValues[i]);
					if (1 < range.Count)
					{
						DataRow[] rows = constraintIndex.GetRows(range);
						string text = ExceptionBuilder.UniqueConstraintViolationText(this.key.ColumnsReference, (object[])uniqueKeyValues[i]);
						for (int j = 0; j < rows.Length; j++)
						{
							rows[j].RowError = text;
							foreach (DataColumn column in this.key.ColumnsReference)
							{
								rows[j].SetColumnError(column, text);
							}
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x002178C8 File Offset: 0x00216CC8
		internal override void CheckConstraint(DataRow row, DataRowAction action)
		{
			if (this.Table.EnforceConstraints && (action == DataRowAction.Add || action == DataRowAction.Change || (action == DataRowAction.Rollback && row.tempRecord != -1)) && row.HaveValuesChanged(this.ColumnsReference) && this.ConstraintIndex.IsKeyRecordInIndex(row.GetDefaultRecord()))
			{
				object[] columnValues = row.GetColumnValues(this.ColumnsReference);
				throw ExceptionBuilder.ConstraintViolation(this.ColumnsReference, columnValues);
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00217938 File Offset: 0x00216D38
		internal override bool ContainsColumn(DataColumn column)
		{
			return this.key.ContainsColumn(column);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00217958 File Offset: 0x00216D58
		internal override Constraint Clone(DataSet destination)
		{
			return this.Clone(destination, false);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00217978 File Offset: 0x00216D78
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
			int num2 = this.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num2];
			for (int i = 0; i < num2; i++)
			{
				DataColumn dataColumn = this.ColumnsReference[i];
				num = dataTable.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array[i] = dataTable.Columns[num];
			}
			UniqueConstraint uniqueConstraint = new UniqueConstraint(this.ConstraintName, array);
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				uniqueConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return uniqueConstraint;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00217AB8 File Offset: 0x00216EB8
		internal UniqueConstraint Clone(DataTable table)
		{
			int num = this.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				DataColumn dataColumn = this.ColumnsReference[i];
				int num2 = table.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array[i] = table.Columns[num2];
			}
			UniqueConstraint uniqueConstraint = new UniqueConstraint(this.ConstraintName, array);
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				uniqueConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return uniqueConstraint;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00217B98 File Offset: 0x00216F98
		[ReadOnly(true)]
		[ResDescription("KeyConstraintColumnsDescr")]
		[ResCategory("DataCategory_Data")]
		public virtual DataColumn[] Columns
		{
			get
			{
				return this.key.ToArray();
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00217BB8 File Offset: 0x00216FB8
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this.key.ColumnsReference;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00217BD8 File Offset: 0x00216FD8
		[ResDescription("KeyConstraintIsPrimaryKeyDescr")]
		[ResCategory("DataCategory_Data")]
		public bool IsPrimaryKey
		{
			get
			{
				return this.Table != null && this == this.Table.primaryKey;
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00217C08 File Offset: 0x00217008
		private void Create(string constraintName, DataColumn[] columns)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				if (columns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(columns[i]);
				}
			}
			this.key = new DataKey(columns, true);
			this.ConstraintName = constraintName;
			this.NonVirtualCheckState();
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00217C58 File Offset: 0x00217058
		public override bool Equals(object key2)
		{
			return key2 is UniqueConstraint && this.Key.ColumnsEqual(((UniqueConstraint)key2).Key);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00217C88 File Offset: 0x00217088
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000210 RID: 528
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x00217CA8 File Offset: 0x002170A8
		internal override bool InCollection
		{
			set
			{
				base.InCollection = value;
				if (this.key.ColumnsReference.Length == 1)
				{
					this.key.ColumnsReference[0].InternalUnique(value);
				}
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00217CE8 File Offset: 0x002170E8
		internal DataKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00217D08 File Offset: 0x00217108
		[ResCategory("DataCategory_Data")]
		[ReadOnly(true)]
		[ResDescription("ConstraintTableDescr")]
		public override DataTable Table
		{
			get
			{
				if (this.key.HasValue)
				{
					return this.key.Table;
				}
				return null;
			}
		}

		// Token: 0x0400096D RID: 2413
		private DataKey key;

		// Token: 0x0400096E RID: 2414
		private Index _constraintIndex;

		// Token: 0x0400096F RID: 2415
		internal bool bPrimaryKey;

		// Token: 0x04000970 RID: 2416
		internal string constraintName;

		// Token: 0x04000971 RID: 2417
		internal string[] columnNames;
	}
}

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x02000133 RID: 307
	[DefaultProperty("ConstraintName")]
	[Editor("Microsoft.VSDesigner.Data.Design.UniqueConstraintEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class UniqueConstraint : Constraint
	{
		// Token: 0x06001209 RID: 4617 RVA: 0x0008A024 File Offset: 0x00089424
		public UniqueConstraint(string name, DataColumn column)
		{
			this.Create(name, new DataColumn[]
			{
				column
			});
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0008A04C File Offset: 0x0008944C
		public UniqueConstraint(DataColumn column)
		{
			this.Create(null, new DataColumn[]
			{
				column
			});
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0008A074 File Offset: 0x00089474
		public UniqueConstraint(string name, DataColumn[] columns)
		{
			this.Create(name, columns);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0008A090 File Offset: 0x00089490
		public UniqueConstraint(DataColumn[] columns)
		{
			this.Create(null, columns);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0008A0AC File Offset: 0x000894AC
		[Browsable(false)]
		public UniqueConstraint(string name, string[] columnNames, bool isPrimaryKey)
		{
			this.constraintName = name;
			this.columnNames = columnNames;
			this.bPrimaryKey = isPrimaryKey;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0008A0D4 File Offset: 0x000894D4
		public UniqueConstraint(string name, DataColumn column, bool isPrimaryKey)
		{
			DataColumn[] columns = new DataColumn[]
			{
				column
			};
			this.bPrimaryKey = isPrimaryKey;
			this.Create(name, columns);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0008A104 File Offset: 0x00089504
		public UniqueConstraint(DataColumn column, bool isPrimaryKey)
		{
			DataColumn[] columns = new DataColumn[]
			{
				column
			};
			this.bPrimaryKey = isPrimaryKey;
			this.Create(null, columns);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0008A134 File Offset: 0x00089534
		public UniqueConstraint(string name, DataColumn[] columns, bool isPrimaryKey)
		{
			this.bPrimaryKey = isPrimaryKey;
			this.Create(name, columns);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0008A158 File Offset: 0x00089558
		public UniqueConstraint(DataColumn[] columns, bool isPrimaryKey)
		{
			this.bPrimaryKey = isPrimaryKey;
			this.Create(null, columns);
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0008A17C File Offset: 0x0008957C
		internal string[] ColumnNames
		{
			get
			{
				return this.key.GetColumnNames();
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0008A194 File Offset: 0x00089594
		internal Index ConstraintIndex
		{
			get
			{
				return this._constraintIndex;
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0008A1A8 File Offset: 0x000895A8
		[Conditional("DEBUG")]
		private void AssertConstraintAndKeyIndexes()
		{
			DataColumn[] array = new DataColumn[this._constraintIndex.IndexFields.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._constraintIndex.IndexFields[i].Column;
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0008A1F0 File Offset: 0x000895F0
		internal void ConstraintIndexClear()
		{
			if (this._constraintIndex != null)
			{
				this._constraintIndex.RemoveRef();
				this._constraintIndex = null;
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0008A218 File Offset: 0x00089618
		internal void ConstraintIndexInitialize()
		{
			if (this._constraintIndex == null)
			{
				this._constraintIndex = this.key.GetSortIndex();
				this._constraintIndex.AddRef();
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0008A24C File Offset: 0x0008964C
		internal override void CheckState()
		{
			this.NonVirtualCheckState();
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0008A260 File Offset: 0x00089660
		private void NonVirtualCheckState()
		{
			this.key.CheckState();
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0008A278 File Offset: 0x00089678
		internal override void CheckCanAddToCollection(ConstraintCollection constraints)
		{
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0008A288 File Offset: 0x00089688
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

		// Token: 0x0600121B RID: 4635 RVA: 0x0008A304 File Offset: 0x00089704
		internal override bool CanEnableConstraint()
		{
			return !this.Table.EnforceConstraints || this.ConstraintIndex.CheckUnique();
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0008A32C File Offset: 0x0008972C
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

		// Token: 0x0600121D RID: 4637 RVA: 0x0008A3FC File Offset: 0x000897FC
		internal override void CheckConstraint(DataRow row, DataRowAction action)
		{
			if (this.Table.EnforceConstraints && (action == DataRowAction.Add || action == DataRowAction.Change || (action == DataRowAction.Rollback && row.tempRecord != -1)) && row.HaveValuesChanged(this.ColumnsReference) && this.ConstraintIndex.IsKeyRecordInIndex(row.GetDefaultRecord()))
			{
				object[] columnValues = row.GetColumnValues(this.ColumnsReference);
				throw ExceptionBuilder.ConstraintViolation(this.ColumnsReference, columnValues);
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0008A468 File Offset: 0x00089868
		internal override bool ContainsColumn(DataColumn column)
		{
			return this.key.ContainsColumn(column);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0008A484 File Offset: 0x00089884
		internal override Constraint Clone(DataSet destination)
		{
			return this.Clone(destination, false);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0008A49C File Offset: 0x0008989C
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

		// Token: 0x06001221 RID: 4641 RVA: 0x0008A5D0 File Offset: 0x000899D0
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

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0008A6B0 File Offset: 0x00089AB0
		[ReadOnly(true)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("KeyConstraintColumnsDescr")]
		public virtual DataColumn[] Columns
		{
			get
			{
				return this.key.ToArray();
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0008A6C8 File Offset: 0x00089AC8
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this.key.ColumnsReference;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0008A6E0 File Offset: 0x00089AE0
		[ResCategory("DataCategory_Data")]
		[ResDescription("KeyConstraintIsPrimaryKeyDescr")]
		public bool IsPrimaryKey
		{
			get
			{
				return this.Table != null && this == this.Table.primaryKey;
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0008A708 File Offset: 0x00089B08
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

		// Token: 0x06001226 RID: 4646 RVA: 0x0008A750 File Offset: 0x00089B50
		public override bool Equals(object key2)
		{
			return key2 is UniqueConstraint && this.Key.ColumnsEqual(((UniqueConstraint)key2).Key);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0008A780 File Offset: 0x00089B80
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x170002BB RID: 699
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x0008A794 File Offset: 0x00089B94
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

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0008A7CC File Offset: 0x00089BCC
		internal DataKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0008A7E0 File Offset: 0x00089BE0
		[ReadOnly(true)]
		[ResDescription("ConstraintTableDescr")]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x04000649 RID: 1609
		private DataKey key;

		// Token: 0x0400064A RID: 1610
		private Index _constraintIndex;

		// Token: 0x0400064B RID: 1611
		internal bool bPrimaryKey;

		// Token: 0x0400064C RID: 1612
		internal string constraintName;

		// Token: 0x0400064D RID: 1613
		internal string[] columnNames;
	}
}

using System;

namespace System.Data
{
	// Token: 0x020000B9 RID: 185
	internal struct DataKey
	{
		// Token: 0x06000A7E RID: 2686 RVA: 0x0005F13C File Offset: 0x0005E53C
		internal DataKey(DataColumn[] columns, bool copyColumns)
		{
			if (columns == null)
			{
				throw ExceptionBuilder.ArgumentNull("columns");
			}
			if (columns.Length == 0)
			{
				throw ExceptionBuilder.KeyNoColumns();
			}
			if (columns.Length > 32)
			{
				throw ExceptionBuilder.KeyTooManyColumns(32);
			}
			for (int i = 0; i < columns.Length; i++)
			{
				if (columns[i] == null)
				{
					throw ExceptionBuilder.ArgumentNull("column");
				}
			}
			for (int j = 0; j < columns.Length; j++)
			{
				for (int k = 0; k < j; k++)
				{
					if (columns[j] == columns[k])
					{
						throw ExceptionBuilder.KeyDuplicateColumns(columns[j].ColumnName);
					}
				}
			}
			if (copyColumns)
			{
				this.columns = new DataColumn[columns.Length];
				for (int l = 0; l < columns.Length; l++)
				{
					this.columns[l] = columns[l];
				}
			}
			else
			{
				this.columns = columns;
			}
			this.CheckState();
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0005F1F8 File Offset: 0x0005E5F8
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0005F20C File Offset: 0x0005E60C
		internal bool HasValue
		{
			get
			{
				return this.columns != null;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0005F224 File Offset: 0x0005E624
		internal DataTable Table
		{
			get
			{
				return this.columns[0].Table;
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0005F240 File Offset: 0x0005E640
		internal void CheckState()
		{
			DataTable table = this.columns[0].Table;
			if (table == null)
			{
				throw ExceptionBuilder.ColumnNotInAnyTable();
			}
			for (int i = 1; i < this.columns.Length; i++)
			{
				if (this.columns[i].Table == null)
				{
					throw ExceptionBuilder.ColumnNotInAnyTable();
				}
				if (this.columns[i].Table != table)
				{
					throw ExceptionBuilder.KeyTableMismatch();
				}
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0005F2A4 File Offset: 0x0005E6A4
		internal bool ColumnsEqual(DataKey key)
		{
			return DataKey.ColumnsEqual(this.columns, key.columns);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0005F2C4 File Offset: 0x0005E6C4
		internal static bool ColumnsEqual(DataColumn[] column1, DataColumn[] column2)
		{
			if (column1 == column2)
			{
				return true;
			}
			if (column1 == null || column2 == null)
			{
				return false;
			}
			if (column1.Length != column2.Length)
			{
				return false;
			}
			for (int i = 0; i < column1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < column2.Length; j++)
				{
					if (column1[i].Equals(column2[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0005F320 File Offset: 0x0005E720
		internal bool ContainsColumn(DataColumn column)
		{
			for (int i = 0; i < this.columns.Length; i++)
			{
				if (column == this.columns[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0005F350 File Offset: 0x0005E750
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0005F370 File Offset: 0x0005E770
		public static bool operator ==(DataKey x, DataKey y)
		{
			return x.Equals(y);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0005F390 File Offset: 0x0005E790
		public static bool operator !=(DataKey x, DataKey y)
		{
			return !x.Equals(y);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0005F3B4 File Offset: 0x0005E7B4
		public override bool Equals(object value)
		{
			return this.Equals((DataKey)value);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0005F3D0 File Offset: 0x0005E7D0
		internal bool Equals(DataKey value)
		{
			DataColumn[] array = this.columns;
			DataColumn[] array2 = value.columns;
			if (array == array2)
			{
				return true;
			}
			if (array == null || array2 == null)
			{
				return false;
			}
			if (array.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].Equals(array2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0005F424 File Offset: 0x0005E824
		internal string[] GetColumnNames()
		{
			string[] array = new string[this.columns.Length];
			for (int i = 0; i < this.columns.Length; i++)
			{
				array[i] = this.columns[i].ColumnName;
			}
			return array;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0005F464 File Offset: 0x0005E864
		internal IndexField[] GetIndexDesc()
		{
			IndexField[] array = new IndexField[this.columns.Length];
			for (int i = 0; i < this.columns.Length; i++)
			{
				array[i] = new IndexField(this.columns[i], false);
			}
			return array;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0005F4A8 File Offset: 0x0005E8A8
		internal object[] GetKeyValues(int record)
		{
			object[] array = new object[this.columns.Length];
			for (int i = 0; i < this.columns.Length; i++)
			{
				array[i] = this.columns[i][record];
			}
			return array;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0005F4E8 File Offset: 0x0005E8E8
		internal Index GetSortIndex()
		{
			return this.GetSortIndex(DataViewRowState.CurrentRows);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0005F500 File Offset: 0x0005E900
		internal Index GetSortIndex(DataViewRowState recordStates)
		{
			IndexField[] indexDesc = this.GetIndexDesc();
			return this.columns[0].Table.GetIndex(indexDesc, recordStates, null);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0005F52C File Offset: 0x0005E92C
		internal bool RecordsEqual(int record1, int record2)
		{
			for (int i = 0; i < this.columns.Length; i++)
			{
				if (this.columns[i].Compare(record1, record2) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0005F560 File Offset: 0x0005E960
		internal DataColumn[] ToArray()
		{
			DataColumn[] array = new DataColumn[this.columns.Length];
			for (int i = 0; i < this.columns.Length; i++)
			{
				array[i] = this.columns[i];
			}
			return array;
		}

		// Token: 0x0400032B RID: 811
		private const int maxColumns = 32;

		// Token: 0x0400032C RID: 812
		private readonly DataColumn[] columns;
	}
}

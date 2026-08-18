using System;

namespace System.Data
{
	// Token: 0x0200007B RID: 123
	internal struct DataKey
	{
		// Token: 0x060006D7 RID: 1751 RVA: 0x001F0AA8 File Offset: 0x001EFEA8
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x001F0B68 File Offset: 0x001EFF68
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x001F0B88 File Offset: 0x001EFF88
		internal bool HasValue
		{
			get
			{
				return null != this.columns;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x001F0BA8 File Offset: 0x001EFFA8
		internal DataTable Table
		{
			get
			{
				return this.columns[0].Table;
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x001F0BC8 File Offset: 0x001EFFC8
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

		// Token: 0x060006DC RID: 1756 RVA: 0x001F0C38 File Offset: 0x001F0038
		internal bool ColumnsEqual(DataKey key)
		{
			DataColumn[] array = this.columns;
			DataColumn[] array2 = key.columns;
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
				bool flag = false;
				for (int j = 0; j < array2.Length; j++)
				{
					if (array[i].Equals(array2[j]))
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

		// Token: 0x060006DD RID: 1757 RVA: 0x001F0CA8 File Offset: 0x001F00A8
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

		// Token: 0x060006DE RID: 1758 RVA: 0x001F0CD8 File Offset: 0x001F00D8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x001F0CF8 File Offset: 0x001F00F8
		public static bool operator ==(DataKey x, DataKey y)
		{
			return x.Equals(y);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x001F0D18 File Offset: 0x001F0118
		public static bool operator !=(DataKey x, DataKey y)
		{
			return !x.Equals(y);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x001F0D48 File Offset: 0x001F0148
		public override bool Equals(object value)
		{
			return this.Equals((DataKey)value);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x001F0D68 File Offset: 0x001F0168
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

		// Token: 0x060006E3 RID: 1763 RVA: 0x001F0DC8 File Offset: 0x001F01C8
		internal string[] GetColumnNames()
		{
			string[] array = new string[this.columns.Length];
			for (int i = 0; i < this.columns.Length; i++)
			{
				array[i] = this.columns[i].ColumnName;
			}
			return array;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x001F0E08 File Offset: 0x001F0208
		internal IndexField[] GetIndexDesc()
		{
			IndexField[] array = new IndexField[this.columns.Length];
			for (int i = 0; i < this.columns.Length; i++)
			{
				array[i] = new IndexField(this.columns[i], false);
			}
			return array;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x001F0E58 File Offset: 0x001F0258
		internal object[] GetKeyValues(int record)
		{
			object[] array = new object[this.columns.Length];
			for (int i = 0; i < this.columns.Length; i++)
			{
				array[i] = this.columns[i][record];
			}
			return array;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x001F0E98 File Offset: 0x001F0298
		internal Index GetSortIndex()
		{
			return this.GetSortIndex(DataViewRowState.CurrentRows);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x001F0EB8 File Offset: 0x001F02B8
		internal Index GetSortIndex(DataViewRowState recordStates)
		{
			IndexField[] indexDesc = this.GetIndexDesc();
			return this.columns[0].Table.GetIndex(indexDesc, recordStates, null);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x001F0EE8 File Offset: 0x001F02E8
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

		// Token: 0x060006E9 RID: 1769 RVA: 0x001F0F28 File Offset: 0x001F0328
		internal DataColumn[] ToArray()
		{
			DataColumn[] array = new DataColumn[this.columns.Length];
			for (int i = 0; i < this.columns.Length; i++)
			{
				array[i] = this.columns[i];
			}
			return array;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x001F0F68 File Offset: 0x001F0368
		internal static int ColumnOrder(int indexDesc)
		{
			return indexDesc & 65535;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x001F0F88 File Offset: 0x001F0388
		internal static bool SortDecending(int indexDesc)
		{
			return (indexDesc & int.MinValue) != 0;
		}

		// Token: 0x0400071F RID: 1823
		internal const int COLUMN = 65535;

		// Token: 0x04000720 RID: 1824
		internal const int DESCENDING = -2147483648;

		// Token: 0x04000721 RID: 1825
		private const int maxColumns = 32;

		// Token: 0x04000722 RID: 1826
		private readonly DataColumn[] columns;
	}
}

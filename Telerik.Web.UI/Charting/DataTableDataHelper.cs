using System;
using System.Data;

namespace Telerik.Charting
{
	// Token: 0x020016F3 RID: 5875
	internal class DataTableDataHelper : DataHelper
	{
		// Token: 0x0600E432 RID: 58418 RVA: 0x0032AA59 File Offset: 0x00328C59
		public DataTableDataHelper(DataTable data)
		{
			this.data = data;
			if (data != null)
			{
				this.rowsCount = data.Rows.Count;
				this.columnsCount = data.Columns.Count;
			}
		}

		// Token: 0x170045A8 RID: 17832
		// (get) Token: 0x0600E433 RID: 58419 RVA: 0x0032AA8D File Offset: 0x00328C8D
		public override int RowsCount
		{
			get
			{
				return this.rowsCount;
			}
		}

		// Token: 0x170045A9 RID: 17833
		// (get) Token: 0x0600E434 RID: 58420 RVA: 0x0032AA95 File Offset: 0x00328C95
		public override int ColumnsCount
		{
			get
			{
				return this.columnsCount;
			}
		}

		// Token: 0x0600E435 RID: 58421 RVA: 0x0032AAA0 File Offset: 0x00328CA0
		public override object GetObjectValue(int rowIndex, int columnIndex)
		{
			object obj = null;
			if (rowIndex >= 0 && rowIndex < this.RowsCount && columnIndex >= 0 && columnIndex < this.ColumnsCount)
			{
				obj = this.data.Rows[rowIndex][columnIndex];
			}
			if (obj == DBNull.Value || obj == null)
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x0600E436 RID: 58422 RVA: 0x0032AAF0 File Offset: 0x00328CF0
		public override bool IsColumnNumeric(int columnIndex)
		{
			int num = 0;
			int num2 = 0;
			int num3 = Math.Min(this.RowsCount, 10);
			if (columnIndex < 0 || columnIndex >= this.ColumnsCount)
			{
				return false;
			}
			if (DataHelper.IsTypeNumeric(this.data.Columns[columnIndex].DataType))
			{
				return true;
			}
			for (int i = 0; i < num3; i++)
			{
				object objectValue = this.GetObjectValue(i, columnIndex);
				if (DataHelper.IsValueNumeric(objectValue))
				{
					num++;
				}
				else if (objectValue == null)
				{
					num2++;
				}
				else
				{
					num--;
				}
			}
			return num > 0 && num2 != num3;
		}

		// Token: 0x0600E437 RID: 58423 RVA: 0x0032AB7B File Offset: 0x00328D7B
		public override bool IsColumnString(int columnIndex)
		{
			return columnIndex >= 0 && columnIndex < this.ColumnsCount && (columnIndex >= 0 && DataHelper.IsTypeString(this.data.Columns[columnIndex].DataType));
		}

		// Token: 0x0600E438 RID: 58424 RVA: 0x0032ABB0 File Offset: 0x00328DB0
		public override int GetColumnIndex(string columnName)
		{
			if (!string.IsNullOrEmpty(columnName))
			{
				return this.data.Columns.IndexOf(columnName);
			}
			return -1;
		}

		// Token: 0x0600E439 RID: 58425 RVA: 0x0032ABCD File Offset: 0x00328DCD
		public override string GetColumnName(int columnIndex)
		{
			if (columnIndex >= 0 && columnIndex < this.ColumnsCount)
			{
				return this.data.Columns[columnIndex].ColumnName;
			}
			return string.Empty;
		}

		// Token: 0x170045AA RID: 17834
		// (get) Token: 0x0600E43A RID: 58426 RVA: 0x0032ABF8 File Offset: 0x00328DF8
		public override bool ColumnNameSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170045AB RID: 17835
		// (get) Token: 0x0600E43B RID: 58427 RVA: 0x0032ABFB File Offset: 0x00328DFB
		public DataTable DataTable
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x040041E3 RID: 16867
		private DataTable data;

		// Token: 0x040041E4 RID: 16868
		private int rowsCount;

		// Token: 0x040041E5 RID: 16869
		private int columnsCount;
	}
}

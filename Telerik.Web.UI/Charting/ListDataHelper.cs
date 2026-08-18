using System;
using System.Collections;

namespace Telerik.Charting
{
	// Token: 0x020016FB RID: 5883
	internal class ListDataHelper : DataHelper
	{
		// Token: 0x0600E47E RID: 58494 RVA: 0x0032B5EB File Offset: 0x003297EB
		public ListDataHelper(IList list)
		{
			this.data = list;
			if (this.data != null)
			{
				this.rowsCount = this.data.Count;
			}
		}

		// Token: 0x170045BB RID: 17851
		// (get) Token: 0x0600E47F RID: 58495 RVA: 0x0032B61A File Offset: 0x0032981A
		public override int RowsCount
		{
			get
			{
				return this.rowsCount;
			}
		}

		// Token: 0x170045BC RID: 17852
		// (get) Token: 0x0600E480 RID: 58496 RVA: 0x0032B622 File Offset: 0x00329822
		public override int ColumnsCount
		{
			get
			{
				return this.columnsCount;
			}
		}

		// Token: 0x0600E481 RID: 58497 RVA: 0x0032B62C File Offset: 0x0032982C
		public override object GetObjectValue(int rowIndex, int columnIndex)
		{
			object obj = null;
			if (rowIndex >= 0 && rowIndex < this.RowsCount && columnIndex >= 0 && columnIndex < this.ColumnsCount)
			{
				obj = this.data[rowIndex];
			}
			if (obj == DBNull.Value)
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x0600E482 RID: 58498 RVA: 0x0032B670 File Offset: 0x00329870
		public override bool IsColumnNumeric(int columnIndex)
		{
			return columnIndex >= 0 && columnIndex < this.ColumnsCount && this.data[columnIndex] != null && (DataHelper.IsTypeNumeric(this.data[columnIndex].GetType()) || DataHelper.IsValueNumeric(this.data[columnIndex]));
		}

		// Token: 0x0600E483 RID: 58499 RVA: 0x0032B6C5 File Offset: 0x003298C5
		public override bool IsColumnString(int columnIndex)
		{
			return columnIndex >= 0 && columnIndex < this.ColumnsCount && this.data[columnIndex] != null && DataHelper.IsTypeString(this.data[columnIndex].GetType());
		}

		// Token: 0x0600E484 RID: 58500 RVA: 0x0032B6FA File Offset: 0x003298FA
		public override int GetColumnIndex(string columnName)
		{
			return -1;
		}

		// Token: 0x0600E485 RID: 58501 RVA: 0x0032B6FD File Offset: 0x003298FD
		public override string GetColumnName(int columnIndex)
		{
			return string.Empty;
		}

		// Token: 0x170045BD RID: 17853
		// (get) Token: 0x0600E486 RID: 58502 RVA: 0x0032B704 File Offset: 0x00329904
		public override bool ColumnNameSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040041F1 RID: 16881
		internal IList data;

		// Token: 0x040041F2 RID: 16882
		private int rowsCount;

		// Token: 0x040041F3 RID: 16883
		private int columnsCount = 1;
	}
}

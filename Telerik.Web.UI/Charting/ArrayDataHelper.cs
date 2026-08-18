using System;

namespace Telerik.Charting
{
	// Token: 0x020016ED RID: 5869
	internal class ArrayDataHelper : DataHelper
	{
		// Token: 0x0600E3EC RID: 58348 RVA: 0x00329238 File Offset: 0x00327438
		private int[] indicies(int columnIndex)
		{
			int[] array = new int[this.data.Rank];
			array.Initialize();
			array.SetValue(columnIndex, array.GetUpperBound(0));
			return array;
		}

		// Token: 0x0600E3ED RID: 58349 RVA: 0x00329270 File Offset: 0x00327470
		public ArrayDataHelper(Array array)
		{
			this.data = array;
			if (this.data != null)
			{
				this.rowsCount = this.data.GetLength(0);
				if (this.data.Rank == 1)
				{
					this.columnsCount = this.data.Rank;
					return;
				}
				if (this.data.Rank > 1)
				{
					this.columnsCount = this.data.GetLength(1);
				}
			}
		}

		// Token: 0x17004590 RID: 17808
		// (get) Token: 0x0600E3EE RID: 58350 RVA: 0x003292E4 File Offset: 0x003274E4
		public override int RowsCount
		{
			get
			{
				return this.rowsCount;
			}
		}

		// Token: 0x17004591 RID: 17809
		// (get) Token: 0x0600E3EF RID: 58351 RVA: 0x003292EC File Offset: 0x003274EC
		public override int ColumnsCount
		{
			get
			{
				return this.columnsCount;
			}
		}

		// Token: 0x0600E3F0 RID: 58352 RVA: 0x003292F4 File Offset: 0x003274F4
		public override object GetObjectValue(int rowIndex, int columnIndex)
		{
			object obj = null;
			if (rowIndex >= 0 && rowIndex < this.RowsCount && columnIndex >= 0 && columnIndex < this.ColumnsCount)
			{
				if (this.ColumnsCount == 1)
				{
					obj = this.data.GetValue(rowIndex);
				}
				else
				{
					obj = this.data.GetValue(rowIndex, columnIndex);
				}
			}
			if (obj == DBNull.Value)
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x0600E3F1 RID: 58353 RVA: 0x00329350 File Offset: 0x00327550
		public override bool IsColumnNumeric(int columnIndex)
		{
			return columnIndex >= 0 && columnIndex < this.ColumnsCount && this.data.GetValue(this.indicies(columnIndex)) != null && (DataHelper.IsTypeNumeric(this.data.GetValue(this.indicies(columnIndex)).GetType()) || DataHelper.IsValueNumeric(this.data.GetValue(this.indicies(columnIndex))));
		}

		// Token: 0x0600E3F2 RID: 58354 RVA: 0x003293B8 File Offset: 0x003275B8
		public override bool IsColumnString(int columnIndex)
		{
			return columnIndex >= 0 && columnIndex < this.ColumnsCount && this.data.GetValue(this.indicies(columnIndex)) != null && DataHelper.IsTypeString(this.data.GetValue(this.indicies(columnIndex)).GetType());
		}

		// Token: 0x0600E3F3 RID: 58355 RVA: 0x00329404 File Offset: 0x00327604
		public override int GetColumnIndex(string columnName)
		{
			return -1;
		}

		// Token: 0x0600E3F4 RID: 58356 RVA: 0x00329407 File Offset: 0x00327607
		public override string GetColumnName(int columnIndex)
		{
			return string.Empty;
		}

		// Token: 0x17004592 RID: 17810
		// (get) Token: 0x0600E3F5 RID: 58357 RVA: 0x0032940E File Offset: 0x0032760E
		public override bool ColumnNameSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040041BC RID: 16828
		internal Array data;

		// Token: 0x040041BD RID: 16829
		private int rowsCount;

		// Token: 0x040041BE RID: 16830
		private int columnsCount;
	}
}

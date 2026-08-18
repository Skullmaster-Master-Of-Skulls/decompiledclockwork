using System;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B23 RID: 6947
	public class GridExportExcelMLRowCreatedArgs : EventArgs
	{
		// Token: 0x06010CC8 RID: 68808 RVA: 0x003BA583 File Offset: 0x003B8783
		public GridExportExcelMLRowCreatedArgs(RowElement row, GridExportExcelMLRowType rowType, WorksheetElement worksheet)
		{
			this._row = row;
			this._worksheet = worksheet;
			this._rowType = rowType;
		}

		// Token: 0x170051D5 RID: 20949
		// (get) Token: 0x06010CC9 RID: 68809 RVA: 0x003BA5B2 File Offset: 0x003B87B2
		public GridExportExcelMLRowType RowType
		{
			get
			{
				return this._rowType;
			}
		}

		// Token: 0x170051D6 RID: 20950
		// (get) Token: 0x06010CCA RID: 68810 RVA: 0x003BA5BA File Offset: 0x003B87BA
		public RowElement Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x170051D7 RID: 20951
		// (get) Token: 0x06010CCB RID: 68811 RVA: 0x003BA5C2 File Offset: 0x003B87C2
		public WorksheetElement Worksheet
		{
			get
			{
				return this._worksheet;
			}
		}

		// Token: 0x04004B28 RID: 19240
		private RowElement _row = new RowElement();

		// Token: 0x04004B29 RID: 19241
		private GridExportExcelMLRowType _rowType = GridExportExcelMLRowType.DataRow;

		// Token: 0x04004B2A RID: 19242
		private WorksheetElement _worksheet;
	}
}

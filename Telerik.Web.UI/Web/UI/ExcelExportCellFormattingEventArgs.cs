using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001136 RID: 4406
	public class ExcelExportCellFormattingEventArgs : EventArgs
	{
		// Token: 0x17003A01 RID: 14849
		// (get) Token: 0x0600B38A RID: 45962 RVA: 0x002717B3 File Offset: 0x0026F9B3
		public GridColumn FormattedColumn
		{
			get
			{
				return this._formattedColumn;
			}
		}

		// Token: 0x17003A02 RID: 14850
		// (get) Token: 0x0600B38B RID: 45963 RVA: 0x002717BB File Offset: 0x0026F9BB
		public TableCell Cell
		{
			get
			{
				return this._cell;
			}
		}

		// Token: 0x0600B38C RID: 45964 RVA: 0x002717C3 File Offset: 0x0026F9C3
		public ExcelExportCellFormattingEventArgs(GridColumn formattedColumn, TableCell cell)
		{
			this._formattedColumn = formattedColumn;
			this._cell = cell;
		}

		// Token: 0x04002F50 RID: 12112
		private GridColumn _formattedColumn;

		// Token: 0x04002F51 RID: 12113
		private TableCell _cell;
	}
}

using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001137 RID: 4407
	public class ExportCellFormattingEventArgs : EventArgs
	{
		// Token: 0x17003A03 RID: 14851
		// (get) Token: 0x0600B38D RID: 45965 RVA: 0x002717D9 File Offset: 0x0026F9D9
		public GridColumn FormattedColumn
		{
			get
			{
				return this._formattedColumn;
			}
		}

		// Token: 0x17003A04 RID: 14852
		// (get) Token: 0x0600B38E RID: 45966 RVA: 0x002717E1 File Offset: 0x0026F9E1
		public TableCell Cell
		{
			get
			{
				return this._cell;
			}
		}

		// Token: 0x0600B38F RID: 45967 RVA: 0x002717E9 File Offset: 0x0026F9E9
		public ExportCellFormattingEventArgs(GridColumn formattedColumn, TableCell cell)
		{
			this._formattedColumn = formattedColumn;
			this._cell = cell;
		}

		// Token: 0x04002F52 RID: 12114
		private GridColumn _formattedColumn;

		// Token: 0x04002F53 RID: 12115
		private TableCell _cell;
	}
}

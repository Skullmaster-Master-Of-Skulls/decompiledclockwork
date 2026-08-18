using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001145 RID: 4421
	public class GridCellDataBoundEventArgs : EventArgs
	{
		// Token: 0x0600B42E RID: 46126 RVA: 0x00277074 File Offset: 0x00275274
		public GridCellDataBoundEventArgs(GridColumn column, TableCell cell)
		{
			this._cell = cell;
			this._column = column;
		}

		// Token: 0x17003A3C RID: 14908
		// (get) Token: 0x0600B42F RID: 46127 RVA: 0x0027708A File Offset: 0x0027528A
		public TableCell Cell
		{
			get
			{
				return this._cell;
			}
		}

		// Token: 0x17003A3D RID: 14909
		// (get) Token: 0x0600B430 RID: 46128 RVA: 0x00277092 File Offset: 0x00275292
		public GridColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x04002F67 RID: 12135
		private TableCell _cell;

		// Token: 0x04002F68 RID: 12136
		private GridColumn _column;
	}
}

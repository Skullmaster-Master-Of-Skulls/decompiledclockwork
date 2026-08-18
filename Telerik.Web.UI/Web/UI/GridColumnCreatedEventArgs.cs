using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010B1 RID: 4273
	public class GridColumnCreatedEventArgs : EventArgs
	{
		// Token: 0x0600AE27 RID: 44583 RVA: 0x0025898B File Offset: 0x00256B8B
		public GridColumnCreatedEventArgs(GridColumn column, GridTableView tableView)
		{
			this.column = column;
			this.tableView = tableView;
		}

		// Token: 0x17003846 RID: 14406
		// (get) Token: 0x0600AE28 RID: 44584 RVA: 0x002589A1 File Offset: 0x00256BA1
		public GridColumn Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x17003847 RID: 14407
		// (get) Token: 0x0600AE29 RID: 44585 RVA: 0x002589A9 File Offset: 0x00256BA9
		public GridTableView OwnerTableView
		{
			get
			{
				return this.tableView;
			}
		}

		// Token: 0x04002DFB RID: 11771
		private GridColumn column;

		// Token: 0x04002DFC RID: 11772
		private GridTableView tableView;
	}
}

using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010AF RID: 4271
	public class GridColumnCreatingEventArgs : EventArgs
	{
		// Token: 0x0600AE1E RID: 44574 RVA: 0x00258942 File Offset: 0x00256B42
		public GridColumnCreatingEventArgs(GridColumn column, GridTableView tableView, string columnType)
		{
			this.column = column;
			this.tableView = tableView;
			this.columnType = columnType;
		}

		// Token: 0x17003843 RID: 14403
		// (get) Token: 0x0600AE1F RID: 44575 RVA: 0x0025896A File Offset: 0x00256B6A
		// (set) Token: 0x0600AE20 RID: 44576 RVA: 0x00258972 File Offset: 0x00256B72
		public GridColumn Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		// Token: 0x17003844 RID: 14404
		// (get) Token: 0x0600AE21 RID: 44577 RVA: 0x0025897B File Offset: 0x00256B7B
		public string ColumnType
		{
			get
			{
				return this.columnType;
			}
		}

		// Token: 0x17003845 RID: 14405
		// (get) Token: 0x0600AE22 RID: 44578 RVA: 0x00258983 File Offset: 0x00256B83
		public GridTableView OwnerTableView
		{
			get
			{
				return this.tableView;
			}
		}

		// Token: 0x04002DF8 RID: 11768
		private string columnType = "";

		// Token: 0x04002DF9 RID: 11769
		private GridColumn column;

		// Token: 0x04002DFA RID: 11770
		private GridTableView tableView;
	}
}

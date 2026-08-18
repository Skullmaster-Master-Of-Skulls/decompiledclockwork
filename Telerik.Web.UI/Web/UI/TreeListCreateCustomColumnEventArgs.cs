using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200125F RID: 4703
	public class TreeListCreateCustomColumnEventArgs : EventArgs
	{
		// Token: 0x0600C1F0 RID: 49648 RVA: 0x002B4FF7 File Offset: 0x002B31F7
		public TreeListCreateCustomColumnEventArgs(TreeListColumn column, string columnType)
		{
			this.Column = column;
			this.ColumnType = columnType;
		}

		// Token: 0x17003E89 RID: 16009
		// (get) Token: 0x0600C1F1 RID: 49649 RVA: 0x002B500D File Offset: 0x002B320D
		// (set) Token: 0x0600C1F2 RID: 49650 RVA: 0x002B5015 File Offset: 0x002B3215
		public TreeListColumn Column { get; set; }

		// Token: 0x17003E8A RID: 16010
		// (get) Token: 0x0600C1F3 RID: 49651 RVA: 0x002B501E File Offset: 0x002B321E
		// (set) Token: 0x0600C1F4 RID: 49652 RVA: 0x002B5026 File Offset: 0x002B3226
		public string ColumnType { get; set; }
	}
}

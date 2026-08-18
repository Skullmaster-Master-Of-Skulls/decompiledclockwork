using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200124A RID: 4682
	public class TreeListCellDataBoundEventArgs : EventArgs
	{
		// Token: 0x0600C0F6 RID: 49398 RVA: 0x002AFE9B File Offset: 0x002AE09B
		public TreeListCellDataBoundEventArgs(TreeListColumn column, TableCell cell)
		{
			this.Cell = cell;
			this.Column = column;
		}

		// Token: 0x17003E32 RID: 15922
		// (get) Token: 0x0600C0F7 RID: 49399 RVA: 0x002AFEB1 File Offset: 0x002AE0B1
		// (set) Token: 0x0600C0F8 RID: 49400 RVA: 0x002AFEB9 File Offset: 0x002AE0B9
		public TableCell Cell { get; internal set; }

		// Token: 0x17003E33 RID: 15923
		// (get) Token: 0x0600C0F9 RID: 49401 RVA: 0x002AFEC2 File Offset: 0x002AE0C2
		// (set) Token: 0x0600C0FA RID: 49402 RVA: 0x002AFECA File Offset: 0x002AE0CA
		public TreeListColumn Column { get; internal set; }
	}
}

using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001284 RID: 4740
	public class TreeListTableCell : TableCell
	{
		// Token: 0x0600C5B4 RID: 50612 RVA: 0x002C2C7D File Offset: 0x002C0E7D
		public TreeListTableCell()
		{
		}

		// Token: 0x0600C5B5 RID: 50613 RVA: 0x002C2C85 File Offset: 0x002C0E85
		public TreeListTableCell(bool useNbsp)
		{
			this.Text = (useNbsp ? "&nbsp;" : "");
		}
	}
}

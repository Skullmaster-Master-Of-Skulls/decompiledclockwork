using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000670 RID: 1648
	public class PivotGridTableCell : TableCell
	{
		// Token: 0x06003C3C RID: 15420 RVA: 0x000C39C4 File Offset: 0x000C1BC4
		public PivotGridTableCell()
		{
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x000C39CC File Offset: 0x000C1BCC
		public PivotGridTableCell(bool useNbsp)
		{
			this.Text = (useNbsp ? "&nbsp;" : "");
		}
	}
}

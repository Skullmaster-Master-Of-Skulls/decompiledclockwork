using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000450 RID: 1104
	internal sealed class LayoutTableCell : TableCell
	{
		// Token: 0x0600353D RID: 13629 RVA: 0x000ACA32 File Offset: 0x000AAC32
		protected internal override void AddedControl(Control control, int index)
		{
			if (control.Page == null)
			{
				control.Page = this.Page;
			}
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x00006164 File Offset: 0x00004364
		protected internal override void RemovedControl(Control control)
		{
		}
	}
}

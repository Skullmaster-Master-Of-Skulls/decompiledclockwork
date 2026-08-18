using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000E76 RID: 3702
	internal class SchedulerTable : Table
	{
		// Token: 0x06008C65 RID: 35941 RVA: 0x001FDA08 File Offset: 0x001FBC08
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.GridLines = GridLines.None;
			this.RenderingCompatibility = new Version(4, 0);
			if (this.Page.Request != null && this.Page.Request.Browser.Browser == "IE" && this.Page.Request.Browser.MajorVersion <= 7)
			{
				this.CellPadding = 0;
				this.CellSpacing = 0;
				return;
			}
			this.CellPadding = -1;
			this.CellSpacing = -1;
		}
	}
}

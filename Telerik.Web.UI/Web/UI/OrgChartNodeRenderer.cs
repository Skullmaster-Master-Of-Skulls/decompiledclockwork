using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000C11 RID: 3089
	[ToolboxItem(false)]
	public class OrgChartNodeRenderer : OrgChartNodeRendererBase
	{
		// Token: 0x060075BF RID: 30143 RVA: 0x001B647F File Offset: 0x001B467F
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			base.RenderLines(writer);
		}
	}
}

using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000635 RID: 1589
	[ToolboxItem(false)]
	public class OrgChartNodeRendererLite : OrgChartNodeRendererBase
	{
		// Token: 0x060039F7 RID: 14839 RVA: 0x000BE098 File Offset: 0x000BC298
		protected override string GetMainContainerCssClass()
		{
			string text = base.GetMainContainerCssClass();
			if (!base.HasNodes)
			{
				text = string.Format("{0} {1}", text, "rocLastLevel").Trim();
			}
			return text;
		}
	}
}

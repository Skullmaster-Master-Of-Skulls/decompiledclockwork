using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000631 RID: 1585
	[ToolboxItem(false)]
	public class OrgChartGroupItemCollectionRendererLite : OrgChartGroupItemCollectionRendererBase
	{
		// Token: 0x060039A6 RID: 14758 RVA: 0x000BD6D8 File Offset: 0x000BB8D8
		protected override void RenderExpandCollapseArrow(string arrowCollapsedState, bool collapsed, HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, arrowCollapsedState);
			string value = collapsed ? "expand" : "collapse";
			writer.AddAttribute(HtmlTextWriterAttribute.Title, value);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.RenderEndTag();
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000BD714 File Offset: 0x000BB914
		protected override void RenderGroupExpandCollapseArrow(string arrowCollapsedState, bool groupCollapsed, HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, arrowCollapsedState);
			string value = groupCollapsed ? "expand" : "collapse";
			writer.AddAttribute(HtmlTextWriterAttribute.Title, value);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.RenderEndTag();
		}

		// Token: 0x04000F5A RID: 3930
		private const string ExpandTitle = "expand";

		// Token: 0x04000F5B RID: 3931
		private const string CollapseTitle = "collapse";
	}
}

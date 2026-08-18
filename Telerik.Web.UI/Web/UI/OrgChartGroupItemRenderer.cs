using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000C0F RID: 3087
	[ToolboxItem(false)]
	public class OrgChartGroupItemRenderer : OrgChartGroupItemRendererBase
	{
		// Token: 0x060075B6 RID: 30134 RVA: 0x001B62D8 File Offset: 0x001B44D8
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			string text = (!base.IsSimpleBinding) ? base.GetListItemWrapperCssClass() : "rocItem";
			if (!string.IsNullOrEmpty(base.CssClass))
			{
				text = string.Format("{0} {1}", text, base.CssClass).Trim();
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			base.RenderBeginTag(writer);
		}

		// Token: 0x060075B7 RID: 30135 RVA: 0x001B6330 File Offset: 0x001B4530
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (!base.IsSimpleBinding)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItem");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			if (base.IsTemplated)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItemTemplate");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				base.RenderContents(writer);
				writer.RenderEndTag();
			}
			else
			{
				base.RenderContents(writer);
				this.RenderInnerContainer(writer);
			}
			if (!base.IsSimpleBinding)
			{
				writer.RenderEndTag();
				return;
			}
			if (base.EnableCollapsing && base.HasNodes)
			{
				string nodeCollapsedState = base.Collapsed ? "rocExpandArrow" : "rocCollapseArrow";
				this.RenderExpandCollapseArrow(nodeCollapsedState, base.Collapsed, writer);
				return;
			}
			if (base.HasNodesForLoad)
			{
				this.RenderExpandCollapseArrow("rocExpandArrow", true, writer);
			}
		}
	}
}

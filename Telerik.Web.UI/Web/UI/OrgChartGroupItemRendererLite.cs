using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000633 RID: 1587
	[ToolboxItem(false)]
	public class OrgChartGroupItemRendererLite : OrgChartGroupItemRendererBase
	{
		// Token: 0x060039D6 RID: 14806 RVA: 0x000BDB65 File Offset: 0x000BBD65
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			if (!base.IsSimpleBinding && base.IsLastInRow && !base.IsLast)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				writer.RenderEndTag();
			}
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000BDB94 File Offset: 0x000BBD94
		protected string GetMainContainerClass()
		{
			string result;
			if (base.IsTemplated)
			{
				result = (base.IsTemplated ? string.Format("{0} {1}", "rocItem", "rocItemTemplate").Trim() : "");
			}
			else
			{
				string arg = "";
				if (string.IsNullOrEmpty(base.ImageUrl))
				{
					if (base.ShouldRenderImage)
					{
						arg = "rocImageReplacement";
					}
					else
					{
						arg = "rocNoOwnImage";
					}
				}
				result = string.Format("{0} {1}", "rocItem", arg).Trim();
			}
			return result;
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000BDC1C File Offset: 0x000BBE1C
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			string text = (!base.IsSimpleBinding) ? base.GetListItemWrapperCssClass() : this.GetMainContainerClass();
			if (!string.IsNullOrEmpty(base.CssClass))
			{
				text = string.Format("{0} {1}", text, base.CssClass).Trim();
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			base.RenderBeginTag(writer);
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000BDC74 File Offset: 0x000BBE74
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (!base.IsSimpleBinding)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetMainContainerClass());
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			if (base.IsTemplated)
			{
				base.RenderContents(writer);
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

		// Token: 0x060039DA RID: 14810 RVA: 0x000BDD18 File Offset: 0x000BBF18
		protected override void RenderImage(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.ImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocImage");
				writer.AddAttribute(HtmlTextWriterAttribute.Src, base.ResolveUrl(base.ImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, (base.ImageAltText == null) ? "" : base.ImageAltText);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000BDD7D File Offset: 0x000BBF7D
		protected override void RenderInnerContainer(HtmlTextWriter writer)
		{
			this.RenderImage(writer);
			if (!string.IsNullOrEmpty(base.Text))
			{
				base.RenderText(writer);
			}
			base.RenderFields(writer);
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000BDDA4 File Offset: 0x000BBFA4
		protected override void RenderExpandCollapseArrow(string nodeCollapsedState, bool collapsed, HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, nodeCollapsedState);
			string value = collapsed ? "expand" : "collapse";
			writer.AddAttribute(HtmlTextWriterAttribute.Title, value);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.RenderEndTag();
		}

		// Token: 0x04000F6F RID: 3951
		private const string ExpandTitle = "expand";

		// Token: 0x04000F70 RID: 3952
		private const string CollapseTitle = "collapse";
	}
}

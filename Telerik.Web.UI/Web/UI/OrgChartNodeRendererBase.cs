using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000634 RID: 1588
	[ToolboxItem(false)]
	public class OrgChartNodeRendererBase : WebControl
	{
		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x060039DE RID: 14814 RVA: 0x000BDDE8 File Offset: 0x000BBFE8
		// (set) Token: 0x060039DF RID: 14815 RVA: 0x000BDDF0 File Offset: 0x000BBFF0
		public bool IsRoot { get; set; }

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x060039E0 RID: 14816 RVA: 0x000BDDF9 File Offset: 0x000BBFF9
		// (set) Token: 0x060039E1 RID: 14817 RVA: 0x000BDE01 File Offset: 0x000BC001
		public bool IsFirst { get; set; }

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x060039E2 RID: 14818 RVA: 0x000BDE0A File Offset: 0x000BC00A
		// (set) Token: 0x060039E3 RID: 14819 RVA: 0x000BDE12 File Offset: 0x000BC012
		public bool IsLast { get; set; }

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x060039E4 RID: 14820 RVA: 0x000BDE1B File Offset: 0x000BC01B
		// (set) Token: 0x060039E5 RID: 14821 RVA: 0x000BDE23 File Offset: 0x000BC023
		internal bool HasNodes { get; set; }

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x060039E6 RID: 14822 RVA: 0x000BDE2C File Offset: 0x000BC02C
		// (set) Token: 0x060039E7 RID: 14823 RVA: 0x000BDE34 File Offset: 0x000BC034
		internal bool EnableCollapsing { get; set; }

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x060039E8 RID: 14824 RVA: 0x000BDE3D File Offset: 0x000BC03D
		// (set) Token: 0x060039E9 RID: 14825 RVA: 0x000BDE45 File Offset: 0x000BC045
		internal bool Collapsed { get; set; }

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x060039EA RID: 14826 RVA: 0x000BDE4E File Offset: 0x000BC04E
		// (set) Token: 0x060039EB RID: 14827 RVA: 0x000BDE56 File Offset: 0x000BC056
		internal bool IsDrilled { get; set; }

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000BDE5F File Offset: 0x000BC05F
		// (set) Token: 0x060039ED RID: 14829 RVA: 0x000BDE67 File Offset: 0x000BC067
		public new string CssClass { get; set; }

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x060039EE RID: 14830 RVA: 0x000BDE70 File Offset: 0x000BC070
		// (set) Token: 0x060039EF RID: 14831 RVA: 0x000BDE78 File Offset: 0x000BC078
		internal bool HasNodesForLoad { get; set; }

		// Token: 0x060039F0 RID: 14832 RVA: 0x000BDE81 File Offset: 0x000BC081
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetMainContainerCssClass());
			base.RenderBeginTag(writer);
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x000BDE98 File Offset: 0x000BC098
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x060039F2 RID: 14834 RVA: 0x000BDEA1 File Offset: 0x000BC0A1
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000BDEA8 File Offset: 0x000BC0A8
		internal void RenderLines(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocNodeLines");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocLineHorizontal");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- -->");
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocLineUp");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- -->");
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocLineDown");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- -->");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000BDF44 File Offset: 0x000BC144
		protected virtual string GetMainContainerCssClass()
		{
			string text = string.Format("{0} {1}", "rocNode", this.IsRoot ? "rocRootNode" : this.GetPositionCssClass()).Trim();
			if (this.IsRoot && this.IsDrilled)
			{
				text = string.Format("{0} {1}", text, "rocDrillDownNode").Trim();
			}
			if (this.EnableCollapsing && this.HasNodes)
			{
				text = string.Format("{0} {1}", text, this.Collapsed ? "rocCollapsedNode" : "rocExpandedNode").Trim();
			}
			else if (this.HasNodesForLoad)
			{
				text = string.Format("{0} {1}", text, "rocCollapsedNode").Trim();
			}
			if (!string.IsNullOrEmpty(this.CssClass))
			{
				text = string.Format("{0} {1}", text, this.CssClass).Trim();
			}
			return text;
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000BE01C File Offset: 0x000BC21C
		private string GetPositionCssClass()
		{
			string arg = string.Format("{0} {1}", this.IsFirst ? "rocFirst" : "", this.IsLast ? "rocLast" : "").Trim();
			return string.Format("{0} {1}", arg, (this.IsFirst && this.IsLast) ? "rocOnly" : "").Trim();
		}
	}
}

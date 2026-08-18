using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000794 RID: 1940
	internal class RibbonBarTabClassicRenderer : RendererBase
	{
		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x000D57F5 File Offset: 0x000D39F5
		// (set) Token: 0x06004415 RID: 17429 RVA: 0x000D57FD File Offset: 0x000D39FD
		protected RibbonBarTab Owner { get; set; }

		// Token: 0x06004416 RID: 17430 RVA: 0x000D5806 File Offset: 0x000D3A06
		public RibbonBarTabClassicRenderer(RibbonBarTab owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x000D5815 File Offset: 0x000D3A15
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x000D581C File Offset: 0x000D3A1C
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.Owner.CssClass;
			string text = this.Owner.Selected ? "rrbSelectedTab" : "";
			string text2 = this.Owner.Enabled ? "" : "rrbDisabled";
			this.Owner.CssClass = RibbonBarStyles.Combine(new string[]
			{
				"rrbTab",
				this.Owner.CssClass
			});
			this.Owner.CssClass = RibbonBarStyles.Combine(new string[]
			{
				this.Owner.CssClass,
				text,
				text2
			});
			string accessKey = this.Owner.AccessKey;
			this.Owner.AccessKey = "";
			this.Owner.BaseAddAttributesToRender(writer);
			this.Owner.AccessKey = accessKey;
			if (!string.IsNullOrEmpty(cssClass))
			{
				this.Owner.CssClass = cssClass;
			}
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x000D5916 File Offset: 0x000D3B16
		public override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderLinkElement(writer);
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x000D591F File Offset: 0x000D3B1F
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x000D5950 File Offset: 0x000D3B50
		protected void RenderLinkElement(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbTabLabel");
			writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.Owner.AccessKey);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			if (!string.IsNullOrEmpty(this.Owner.AccessKey))
			{
				this.RenderKeyboardBox(writer);
			}
			this.RenderTabText(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x000D59B6 File Offset: 0x000D3BB6
		protected void RenderTabText(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbTabText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.Owner.Text);
			writer.RenderEndTag();
		}
	}
}

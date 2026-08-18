using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007BC RID: 1980
	internal class RibbonBarTabLiteRenderer : RendererBase
	{
		// Token: 0x0600450E RID: 17678 RVA: 0x000DAA8C File Offset: 0x000D8C8C
		public RibbonBarTabLiteRenderer(RibbonBarTab owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x0600450F RID: 17679 RVA: 0x000DAA9B File Offset: 0x000D8C9B
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x000DAAA0 File Offset: 0x000D8CA0
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.Owner.CssClass;
			string text = this.Owner.Selected ? "rrbSelected" : "";
			string text2 = this.Owner.Enabled ? "" : "rrbDisabled";
			this.Owner.CssClass = RibbonBarStyles.Combine(new string[]
			{
				"rrbItem",
				text,
				this.Owner.CssClass,
				text2
			});
			this.Owner.BaseAddAttributesToRender(writer);
			if (!string.IsNullOrEmpty(cssClass))
			{
				this.Owner.CssClass = cssClass;
			}
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x000DAB43 File Offset: 0x000D8D43
		public override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderLinkElement(writer);
		}

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x000DAB4C File Offset: 0x000D8D4C
		// (set) Token: 0x06004513 RID: 17683 RVA: 0x000DAB54 File Offset: 0x000D8D54
		protected RibbonBarTab Owner { get; set; }

		// Token: 0x06004514 RID: 17684 RVA: 0x000DAB5D File Offset: 0x000D8D5D
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x000DAB8B File Offset: 0x000D8D8B
		protected void RenderLinkElement(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Owner.AccessKey))
			{
				this.RenderKeyboardBox(writer);
			}
			this.RenderTabText(writer);
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x000DABAD File Offset: 0x000D8DAD
		protected void RenderTabText(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbLink");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.Owner.Text);
			writer.RenderEndTag();
		}
	}
}

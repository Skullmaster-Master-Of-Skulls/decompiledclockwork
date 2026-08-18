using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200079C RID: 1948
	internal class RibbonBarSplitButtonClassicRenderer : RibbonBarMenuBaseItemClassicRenderBase
	{
		// Token: 0x0600445B RID: 17499 RVA: 0x000D6DC5 File Offset: 0x000D4FC5
		public RibbonBarSplitButtonClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x0600445C RID: 17500 RVA: 0x000D6DD0 File Offset: 0x000D4FD0
		protected override string ImageUrlToRender
		{
			get
			{
				string text = ((RibbonBarSplitButton)base.Owner).Enabled ? ((RibbonBarSplitButton)base.Owner).CurrentImageUrl : ((RibbonBarSplitButton)base.Owner).CurrentDisabledImageUrl;
				string text2 = ((RibbonBarSplitButton)base.Owner).Enabled ? ((RibbonBarSplitButton)base.Owner).CurrentImageUrlLarge : ((RibbonBarSplitButton)base.Owner).CurrentDisabledImageUrlLarge;
				string text3 = ((RibbonBarClickableItem)base.Owner).Enabled ? "Telerik.Web.UI.Skins.Common.RibbonBar.NoImage.png" : "Telerik.Web.UI.Skins.Common.RibbonBar.NoDisabledImage.png";
				text3 = ((RibbonBarClickableItem)base.Owner).Page.ClientScript.GetWebResourceUrl(typeof(RadRibbonBar), text3);
				string text4 = ((RibbonBarClickableItem)base.Owner).Enabled ? "Telerik.Web.UI.Skins.Common.RibbonBar.NoImageLarge.png" : "Telerik.Web.UI.Skins.Common.RibbonBar.NoDisabledImageLarge.png";
				text4 = ((RibbonBarClickableItem)base.Owner).Page.ClientScript.GetWebResourceUrl(typeof(RadRibbonBar), text4);
				bool flag = ((RibbonBarClickableItem)base.Owner).ImageRenderingMode == RibbonBarImageRenderingMode.Clip || ((RibbonBarClickableItem)base.Owner).Size != RibbonBarItemSize.Large;
				bool flag2 = (flag && string.IsNullOrEmpty(text)) || (!flag && string.IsNullOrEmpty(text2));
				string result;
				if (flag)
				{
					result = (flag2 ? text3 : base.Owner.ResolveUrl(text));
				}
				else
				{
					result = (flag2 ? text4 : base.Owner.ResolveUrl(text2));
				}
				return result;
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x000D6F58 File Offset: 0x000D5158
		internal override string TextToRender
		{
			get
			{
				string result = string.Empty;
				if (!string.IsNullOrEmpty(((RibbonBarClickableItem)base.Owner).Text))
				{
					result = ((RibbonBarClickableItem)base.Owner).Text;
				}
				else if (((RibbonBarSplitButton)base.Owner).SelectedButton != null)
				{
					result = ((RibbonBarSplitButton)base.Owner).SelectedButton.Text;
				}
				return result;
			}
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x000D6FC0 File Offset: 0x000D51C0
		protected override void RenderTextStructure(HtmlTextWriter writer)
		{
			if (((RibbonBarSplitButton)base.Owner).Size == RibbonBarItemSize.Large)
			{
				if (this.CurrentOwner.ShouldRenderTextContent)
				{
					this.RenderText(writer);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonArrow");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbIcon");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write("&nbsp;");
				writer.RenderEndTag();
				if (this.CurrentOwner.ShouldRenderTextContent)
				{
					this.RenderText(writer);
				}
				writer.RenderEndTag();
				return;
			}
			if (this.CurrentOwner.ShouldRenderTextContent)
			{
				this.RenderText(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonArrow");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbIcon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x000D70A0 File Offset: 0x000D52A0
		private void RenderText(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbTextContent");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.TextToRender);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x000D70F0 File Offset: 0x000D52F0
		protected override void RenderDropDownContents(HtmlTextWriter writer)
		{
			RibbonBarImageRenderingMode imageRenderingMode = ((RibbonBarClickableItem)base.Owner).ImageRenderingMode;
			foreach (RibbonBarButton ribbonBarButton in ((RibbonBarSplitButton)base.Owner).Buttons)
			{
				ribbonBarButton.Text = (string.IsNullOrEmpty(ribbonBarButton.Text) ? "&nbsp;" : ribbonBarButton.Text);
				ribbonBarButton.Size = RibbonBarItemSize.Medium;
				ribbonBarButton.ImageRenderingMode = imageRenderingMode;
				ribbonBarButton.RenderControl(writer);
			}
		}
	}
}

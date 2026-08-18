using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007B6 RID: 1974
	internal class RibbonBarSplitButtonLiteRenderer : RibbonBarMenuBaseItemLiteRenderBase
	{
		// Token: 0x060044D6 RID: 17622 RVA: 0x000D95C4 File Offset: 0x000D77C4
		public RibbonBarSplitButtonLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x060044D7 RID: 17623 RVA: 0x000D95D0 File Offset: 0x000D77D0
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

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x060044D8 RID: 17624 RVA: 0x000D9758 File Offset: 0x000D7958
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

		// Token: 0x060044D9 RID: 17625 RVA: 0x000D97C0 File Offset: 0x000D79C0
		protected override void RenderTextStructure(HtmlTextWriter writer)
		{
			if (((RibbonBarSplitButton)base.Owner).Size == RibbonBarItemSize.Large)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbArrow");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				if (this.CurrentOwner.ShouldRenderTextContent)
				{
					this.RenderText(writer);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
				{
					"radIcon",
					"radIconDown"
				}));
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write("&nbsp;");
				writer.RenderEndTag();
				writer.RenderEndTag();
				return;
			}
			if (this.CurrentOwner.ShouldRenderTextContent)
			{
				this.RenderText(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbArrow");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"radIcon",
				"radIconDown"
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x000D98BB File Offset: 0x000D7ABB
		private void RenderText(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.TextToRender);
			writer.RenderEndTag();
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x000D98E4 File Offset: 0x000D7AE4
		protected override void RenderDropDownContents(HtmlTextWriter writer)
		{
			RibbonBarImageRenderingMode imageRenderingMode = ((RibbonBarClickableItem)base.Owner).ImageRenderingMode;
			foreach (RibbonBarButton ribbonBarButton in ((RibbonBarSplitButton)base.Owner).Buttons)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbList");
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				ribbonBarButton.Text = (string.IsNullOrEmpty(ribbonBarButton.Text) ? "&nbsp;" : ribbonBarButton.Text);
				ribbonBarButton.Size = RibbonBarItemSize.Medium;
				ribbonBarButton.ImageRenderingMode = imageRenderingMode;
				ribbonBarButton.RenderControl(writer);
				writer.RenderEndTag();
			}
		}
	}
}

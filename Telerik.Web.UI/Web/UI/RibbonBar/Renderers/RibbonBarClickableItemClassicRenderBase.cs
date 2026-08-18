using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200079A RID: 1946
	internal class RibbonBarClickableItemClassicRenderBase : RibbonBarItemRenderBase
	{
		// Token: 0x0600444A RID: 17482 RVA: 0x000D679B File Offset: 0x000D499B
		public RibbonBarClickableItemClassicRenderBase(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x0600444B RID: 17483 RVA: 0x000D67A4 File Offset: 0x000D49A4
		internal virtual string TextToRender
		{
			get
			{
				return ((RibbonBarClickableItem)base.Owner).Text;
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x0600444C RID: 17484 RVA: 0x000D67B6 File Offset: 0x000D49B6
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.A;
			}
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x0600444D RID: 17485 RVA: 0x000D67BC File Offset: 0x000D49BC
		public override string CssClassFormatString
		{
			get
			{
				string text = (((RibbonBarClickableItem)base.Owner).ImageRenderingMode == RibbonBarImageRenderingMode.Dual) ? "rrbDualImage" : string.Empty;
				return RibbonBarStyles.Combine(new string[]
				{
					"rrbButtonOut",
					this.SizeCssClass,
					text
				});
			}
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x000D6810 File Offset: 0x000D4A10
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = base.Owner.CssClass;
			string text = base.Owner.Enabled ? string.Empty : "rrbDisabled";
			base.Owner.CssClass = RibbonBarStyles.Combine(new string[]
			{
				this.CssClassFormatString,
				base.Owner.CssClass,
				text
			});
			base.Owner.BaseAddAttributesToRender(writer);
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, base.Owner.AccessKey);
			}
			if (!string.IsNullOrEmpty(((RibbonBarClickableItem)base.Owner).Text) && ((RibbonBarClickableItem)base.Owner).Text != "&nbsp;")
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, ((RibbonBarClickableItem)base.Owner).Text);
			}
			base.Owner.CssClass = cssClass;
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x000D6900 File Offset: 0x000D4B00
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				this.RenderKeyboardBox(writer);
			}
			string text = string.Format("{0} {1}", "rrbButtonMid", ((RibbonBarClickableItem)base.Owner).RibbonBarItemTypeCssClass);
			text = text.Trim();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonIn");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderImage(writer);
			this.RenderTextStructure(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x000D6990 File Offset: 0x000D4B90
		protected virtual void RenderTextStructure(HtmlTextWriter writer)
		{
			if (!((RibbonBarClickableItem)base.Owner).ShouldRenderTextStructure)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (((RibbonBarClickableItem)base.Owner).ShouldRenderTextContent)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbTextContent");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(this.TextToRender);
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x000D6A04 File Offset: 0x000D4C04
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x000D6A34 File Offset: 0x000D4C34
		protected virtual void RenderImage(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbImagePlaceholder");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonImage");
			string value = string.IsNullOrEmpty(((RibbonBarClickableItem)base.Owner).ImageAltText) ? "Item Image" : ((RibbonBarClickableItem)base.Owner).ImageAltText;
			string imageUrlToRender = this.ImageUrlToRender;
			writer.AddAttribute(HtmlTextWriterAttribute.Src, imageUrlToRender);
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06004453 RID: 17491 RVA: 0x000D6AC0 File Offset: 0x000D4CC0
		protected virtual string ImageUrlToRender
		{
			get
			{
				string text = base.Owner.Enabled ? ((RibbonBarClickableItem)base.Owner).ImageUrl : ((RibbonBarClickableItem)base.Owner).DisabledImageUrl;
				string text2 = base.Owner.Enabled ? ((RibbonBarClickableItem)base.Owner).ImageUrlLarge : ((RibbonBarClickableItem)base.Owner).DisabledImageUrlLarge;
				string text3 = base.Owner.Enabled ? "Telerik.Web.UI.Skins.Common.RibbonBar.NoImage.png" : "Telerik.Web.UI.Skins.Common.RibbonBar.NoDisabledImage.png";
				text3 = base.Owner.Page.ClientScript.GetWebResourceUrl(typeof(RadRibbonBar), text3);
				string text4 = base.Owner.Enabled ? "Telerik.Web.UI.Skins.Common.RibbonBar.NoImageLarge.png" : "Telerik.Web.UI.Skins.Common.RibbonBar.NoDisabledImageLarge.png";
				text4 = base.Owner.Page.ClientScript.GetWebResourceUrl(typeof(RadRibbonBar), text4);
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

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x000D6C28 File Offset: 0x000D4E28
		protected string SizeCssClass
		{
			get
			{
				string result;
				switch (((RibbonBarClickableItem)base.Owner).Size)
				{
				case RibbonBarItemSize.Small:
					result = "rrbButton";
					break;
				case RibbonBarItemSize.Medium:
					result = "rrbMediumButton";
					break;
				case RibbonBarItemSize.Large:
					result = "rrbLargeButton";
					break;
				default:
					result = "rrbButton";
					break;
				}
				return result;
			}
		}

		// Token: 0x040011F7 RID: 4599
		internal const string NonBreakingSpace = "&nbsp;";
	}
}

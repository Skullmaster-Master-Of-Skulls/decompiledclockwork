using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007AB RID: 1963
	internal class RibbonBarClickableItemLiteRenderBase : RibbonBarItemRenderBase
	{
		// Token: 0x060044A2 RID: 17570 RVA: 0x000D8669 File Offset: 0x000D6869
		public RibbonBarClickableItemLiteRenderBase(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x000D8672 File Offset: 0x000D6872
		internal virtual string TextToRender
		{
			get
			{
				return ((RibbonBarClickableItem)base.Owner).Text;
			}
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x000D8684 File Offset: 0x000D6884
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.A;
			}
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x060044A5 RID: 17573 RVA: 0x000D8688 File Offset: 0x000D6888
		public override string CssClassFormatString
		{
			get
			{
				string text = (((RibbonBarClickableItem)base.Owner).ImageRenderingMode == RibbonBarImageRenderingMode.Dual) ? "rrbDualImage" : string.Empty;
				return RibbonBarStyles.Combine(new string[]
				{
					"rrbButton",
					this.SizeCssClass,
					((RibbonBarClickableItem)base.Owner).RibbonBarItemTypeCssClass,
					text
				});
			}
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x000D86EC File Offset: 0x000D68EC
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

		// Token: 0x060044A7 RID: 17575 RVA: 0x000D87D9 File Offset: 0x000D69D9
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				this.RenderKeyboardBox(writer);
			}
			this.RenderImage(writer);
			this.RenderTextStructure(writer);
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x000D8804 File Offset: 0x000D6A04
		protected virtual void RenderTextStructure(HtmlTextWriter writer)
		{
			if (!((RibbonBarClickableItem)base.Owner).ShouldRenderTextStructure)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (((RibbonBarClickableItem)base.Owner).ShouldRenderTextContent)
			{
				writer.Write(this.TextToRender);
			}
			writer.RenderEndTag();
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x000D885D File Offset: 0x000D6A5D
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x000D888C File Offset: 0x000D6A8C
		protected virtual void RenderImage(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbImagePlaceholder");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbImage");
			string value = string.IsNullOrEmpty(((RibbonBarClickableItem)base.Owner).ImageAltText) ? "Item Image" : ((RibbonBarClickableItem)base.Owner).ImageAltText;
			string imageUrlToRender = this.ImageUrlToRender;
			writer.AddAttribute(HtmlTextWriterAttribute.Src, imageUrlToRender);
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x000D8918 File Offset: 0x000D6B18
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

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x000D8A80 File Offset: 0x000D6C80
		protected string SizeCssClass
		{
			get
			{
				string result;
				switch (((RibbonBarClickableItem)base.Owner).Size)
				{
				case RibbonBarItemSize.Small:
					result = "rrbSmallButton";
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

		// Token: 0x040011F8 RID: 4600
		internal const string NonBreakingSpace = "&nbsp;";
	}
}

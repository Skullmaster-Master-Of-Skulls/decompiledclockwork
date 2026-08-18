using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020005C0 RID: 1472
	internal class MediaPlayerBannerStructureControl : Control
	{
		// Token: 0x0600348A RID: 13450 RVA: 0x000ADE9C File Offset: 0x000AC09C
		public MediaPlayerBannerStructureControl(RadMediaPlayer owner)
		{
			this.OwnerMadiaPlayer = owner;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000ADEAC File Offset: 0x000AC0AC
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.OwnerMadiaPlayer.Banners.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpBanner");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpActionButton rmpCloseBannerButton");
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.OwnerMadiaPlayer.BannerCloseButtonToolTip);
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "return false;");
				writer.RenderBeginTag(HtmlTextWriterTag.Button);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpIcon rmpCloseBannerIcon");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpButtonText");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.AddAttribute(HtmlTextWriterAttribute.Href, this.OwnerMadiaPlayer.Banners[0].NavigateURL);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, this.OwnerMadiaPlayer.ResolveClientUrlIfNeeded(this.OwnerMadiaPlayer.Banners[0].ImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.OwnerMadiaPlayer.Banners[0].AlternateText);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x04000E46 RID: 3654
		private RadMediaPlayer OwnerMadiaPlayer;
	}
}

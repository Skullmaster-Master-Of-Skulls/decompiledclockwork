using System;
using System.Web.UI;

namespace Telerik.Web.UI.Notification.Renderers
{
	// Token: 0x0200062B RID: 1579
	public class ClassicRenderer : BaseRenderer
	{
		// Token: 0x06003979 RID: 14713 RVA: 0x000BCD9E File Offset: 0x000BAF9E
		public ClassicRenderer(RadNotification notification) : base(notification)
		{
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000BCDA8 File Offset: 0x000BAFA8
		protected override void RenderTitleBarIcon(HtmlTextWriter writer)
		{
			if (this.ShouldRenderIcon(this.notification.TitleIcon.ToLower()))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rnTitleBarIcon");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				string iconUrl = this.notification.GetIconUrl(this.notification.TitleIcon);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, iconUrl);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000BCE22 File Offset: 0x000BB022
		private bool ShouldRenderIcon(string iconImageData)
		{
			return iconImageData != "none" && !string.IsNullOrEmpty(iconImageData);
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000BCE3C File Offset: 0x000BB03C
		protected override void RenderSimpleContentIcon(HtmlTextWriter writer)
		{
			if (this.ShouldRenderIcon(this.notification.ContentIcon.ToLower()))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rnContentIconClipIn");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				string text = "rnContentIconClip";
				string iconUrl = this.notification.GetIconUrl(this.notification.ContentIcon);
				if (!this.IsContentIconEmbedded(iconUrl))
				{
					text += " rnCustomIcon";
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, iconUrl);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x000BCEEB File Offset: 0x000BB0EB
		private bool IsContentIconEmbedded(string iconSrc)
		{
			return iconSrc != this.notification.ContentIcon && !this.notification.ContentIcon.StartsWith("~");
		}
	}
}

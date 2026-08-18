using System;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000234 RID: 564
	internal static class ControlRenderingHelper
	{
		// Token: 0x06001AA4 RID: 6820 RVA: 0x00053BF4 File Offset: 0x00051DF4
		internal static void WriteSkipLinkStart(HtmlTextWriter writer, Version renderingCompatibility, bool designMode, string skipLinkText, string spacerImageUrl, string clientID)
		{
			if (skipLinkText.Length != 0 && !designMode)
			{
				if (renderingCompatibility >= VersionUtil.Framework45)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + clientID + ControlRenderingHelper.SkipLinkContentMark);
					writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Left, "-10000px");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "auto");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "1px");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "1px");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "hidden");
					writer.RenderBeginTag(HtmlTextWriterTag.A);
					writer.Write(skipLinkText);
					writer.RenderEndTag();
					return;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + clientID + ControlRenderingHelper.SkipLinkContentMark);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, skipLinkText);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, spacerImageUrl);
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
				writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
				writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00053D07 File Offset: 0x00051F07
		internal static void WriteSkipLinkEnd(HtmlTextWriter writer, bool designMode, string skipLinkText, string clientID)
		{
			if (skipLinkText.Length != 0 && !designMode)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, clientID + ControlRenderingHelper.SkipLinkContentMark);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderEndTag();
			}
		}

		// Token: 0x0400184D RID: 6221
		private static readonly string SkipLinkContentMark = "_SkipLink";
	}
}

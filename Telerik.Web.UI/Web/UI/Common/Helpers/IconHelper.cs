using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Common.Helpers
{
	// Token: 0x020000B9 RID: 185
	public static class IconHelper
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x0001C769 File Offset: 0x0001A969
		public static string GetIconHtml(string iconName)
		{
			return string.Format(IconHelper.iconHtmlTemplate, iconName);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001C778 File Offset: 0x0001A978
		public static WebControl CreateIcon(string iconName)
		{
			return new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = string.Format(IconHelper.iconCssClassTemplate, iconName)
			};
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001C79F File Offset: 0x0001A99F
		public static void RenderIcon(HtmlTextWriter writer, string iconName)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format(IconHelper.iconCssClassTemplate, iconName));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x04000183 RID: 387
		private static readonly string iconHtmlTemplate = "<span class=\"p-icon p-i-{0}\"></span>";

		// Token: 0x04000184 RID: 388
		private static readonly string iconCssClassTemplate = "p-icon p-i-{0}";
	}
}

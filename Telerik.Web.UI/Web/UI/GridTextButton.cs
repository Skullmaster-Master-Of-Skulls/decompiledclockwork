using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200119C RID: 4508
	[ToolboxItem(false)]
	public class GridTextButton : LinkButton
	{
		// Token: 0x0600B92A RID: 47402 RVA: 0x0028F5B8 File Offset: 0x0028D7B8
		protected override void Render(HtmlTextWriter writer)
		{
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer2 = new HtmlTextWriter(stringWriter);
			base.Render(writer2);
			string text = stringWriter.ToString();
			text = text.Replace("<a", "<span");
			if (string.IsNullOrEmpty(this.OnClientClick))
			{
				text = text.Replace("href", "onclick");
			}
			else
			{
				text = Regex.Replace(text, "href=\"[^\"]+\"", string.Empty);
			}
			text = text.Replace("</a>", "</span>");
			writer.Write(text);
		}
	}
}

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020007E0 RID: 2016
	internal class FooterControlLite : FooterControl
	{
		// Token: 0x0600462F RID: 17967 RVA: 0x000DC595 File Offset: 0x000DA795
		public FooterControlLite(bool renderFulltimeLink, string fulltimeLinkText) : base(renderFulltimeLink, fulltimeLinkText)
		{
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x000DC5A0 File Offset: 0x000DA7A0
		protected override void AddLink(string fulltimeLinkText)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span);
			webControl.CssClass = string.Format("{0} {1}", "rsButton", "rsFullTime");
			this.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Span);
			webControl2.CssClass = string.Format("{0} {1}", "p-icon", "p-i-clock");
			webControl.Controls.Add(webControl2);
			LiteralControl child = new LiteralControl(fulltimeLinkText);
			webControl.Controls.Add(child);
		}
	}
}

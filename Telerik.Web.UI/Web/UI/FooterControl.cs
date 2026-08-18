using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020007DF RID: 2015
	internal class FooterControl : WebControl
	{
		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x0600462B RID: 17963 RVA: 0x000DC514 File Offset: 0x000DA714
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x000DC518 File Offset: 0x000DA718
		public FooterControl(bool renderFulltimeLink, string fulltimeLinkText)
		{
			this.CreateFooterControl(renderFulltimeLink, fulltimeLinkText);
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x000DC528 File Offset: 0x000DA728
		private void CreateFooterControl(bool renderFulltimeLink, string fulltimeLinkText)
		{
			this.CssClass = "rsFooter";
			if (renderFulltimeLink)
			{
				this.AddLink(fulltimeLinkText);
			}
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x000DC540 File Offset: 0x000DA740
		protected virtual void AddLink(string fulltimeLinkText)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("a");
			htmlGenericControl.Attributes["href"] = "#";
			htmlGenericControl.Attributes["class"] = "rsFullTime";
			htmlGenericControl.InnerHtml = fulltimeLinkText;
			this.Controls.Add(htmlGenericControl);
		}
	}
}

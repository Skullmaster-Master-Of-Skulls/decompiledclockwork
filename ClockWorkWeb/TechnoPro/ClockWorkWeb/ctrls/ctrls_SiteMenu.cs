using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.ClockWorkWeb.ctrls
{
	// Token: 0x02000125 RID: 293
	public class ctrls_SiteMenu : UserControl
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x0003EC44 File Offset: 0x0003CE44
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x04000699 RID: 1689
		protected HyperLink lnkHome;

		// Token: 0x0400069A RID: 1690
		protected Repeater menu;
	}
}

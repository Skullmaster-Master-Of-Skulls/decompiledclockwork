using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000C0 RID: 192
	public class MiscMaster_nomenuR : MasterPage
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x000246A7 File Offset: 0x000228A7
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
		}

		// Token: 0x0400040F RID: 1039
		protected HtmlForm form1;

		// Token: 0x04000410 RID: 1040
		protected ContentPlaceHolder ContentPlaceHolder1;
	}
}

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000BF RID: 191
	public class user_misc_MiscMaster_nomenu : MasterPage
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x000246A7 File Offset: 0x000228A7
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
		}

		// Token: 0x0400040E RID: 1038
		protected ContentPlaceHolder placeholder_content;
	}
}

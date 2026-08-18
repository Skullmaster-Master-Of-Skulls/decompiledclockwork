using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000BE RID: 190
	public class user_misc_MiscMaster : MasterPage
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x000246A7 File Offset: 0x000228A7
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
		}

		// Token: 0x0400040C RID: 1036
		protected ContentPlaceHolder placeholder_mainmenu;

		// Token: 0x0400040D RID: 1037
		protected ContentPlaceHolder placeholder_content;
	}
}

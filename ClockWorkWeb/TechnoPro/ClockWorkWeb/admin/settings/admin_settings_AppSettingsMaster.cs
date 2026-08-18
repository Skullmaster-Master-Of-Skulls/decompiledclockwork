using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.admin.settings
{
	// Token: 0x0200018E RID: 398
	public class admin_settings_AppSettingsMaster : MasterPage
	{
		// Token: 0x06000BB6 RID: 2998 RVA: 0x000246A7 File Offset: 0x000228A7
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
		}

		// Token: 0x04000865 RID: 2149
		protected ContentPlaceHolder placeholder_content;
	}
}

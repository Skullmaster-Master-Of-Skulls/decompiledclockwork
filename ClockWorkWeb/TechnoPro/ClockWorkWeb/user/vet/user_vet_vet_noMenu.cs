using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.vet
{
	// Token: 0x02000035 RID: 53
	public class user_vet_vet_noMenu : MasterPage
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00009E70 File Offset: 0x00008070
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.MODULES_ENABLED_Veterans);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("~/custom/misc/home.aspx?msgcode=moduledisabled", true);
			}
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.VETERANS);
		}

		// Token: 0x040000FF RID: 255
		protected ScriptManager bbb;

		// Token: 0x04000100 RID: 256
		protected ContentPlaceHolder placeholder_content;
	}
}

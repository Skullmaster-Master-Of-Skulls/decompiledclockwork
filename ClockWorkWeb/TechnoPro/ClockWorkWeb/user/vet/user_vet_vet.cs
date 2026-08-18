using System;
using System.Web.UI;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.vet
{
	// Token: 0x02000034 RID: 52
	public class user_vet_vet : MasterPage
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00009E1C File Offset: 0x0000801C
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
	}
}

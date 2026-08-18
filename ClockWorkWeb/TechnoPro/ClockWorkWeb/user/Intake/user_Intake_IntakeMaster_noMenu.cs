using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.Intake
{
	// Token: 0x020000C7 RID: 199
	public class user_Intake_IntakeMaster_noMenu : MasterPage
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x0002B008 File Offset: 0x00029208
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.MODULES_ENABLED_Intake);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("~/custom/misc/home.aspx?msgcode=moduledisabled", true);
			}
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.INTAKE);
		}

		// Token: 0x04000423 RID: 1059
		protected ContentPlaceHolder placeholder_content;
	}
}

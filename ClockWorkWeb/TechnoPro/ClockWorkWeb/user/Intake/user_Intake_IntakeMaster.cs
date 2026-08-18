using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.Intake
{
	// Token: 0x020000C6 RID: 198
	public class user_Intake_IntakeMaster : MasterPage
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x0002AF18 File Offset: 0x00029118
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.MODULES_ENABLED_Intake);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("~/custom/misc/home.aspx?msgcode=moduledisabled", true);
			}
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(TechnoPro.Common.Public.Entities.Settings.Group.INTAKE);
			bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INTAKE_RequireStudentsToLoginFirst);
			bool flag2 = !settingValue2;
			if (flag2)
			{
				this.RadMenu1.Items[3].Visible = false;
				this.RadMenu1.Items[4].Visible = false;
				this.RadMenu1.Items[5].Visible = false;
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0002AFD0 File Offset: 0x000291D0
		protected void RadMenu1_ItemClick(object sender, RadMenuEventArgs e)
		{
			bool flag = e.Item.Value.ToLower().Contains("logout");
			if (flag)
			{
				WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
			}
		}

		// Token: 0x0400041F RID: 1055
		protected ScriptManager bbb;

		// Token: 0x04000420 RID: 1056
		protected RadMenu RadMenu1;

		// Token: 0x04000421 RID: 1057
		protected Label lbl_mainMessage;

		// Token: 0x04000422 RID: 1058
		protected ContentPlaceHolder placeholder_content;
	}
}

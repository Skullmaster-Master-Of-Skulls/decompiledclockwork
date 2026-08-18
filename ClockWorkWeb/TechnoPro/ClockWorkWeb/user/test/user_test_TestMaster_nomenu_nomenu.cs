using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x0200006F RID: 111
	public class user_test_TestMaster_nomenu_nomenu : MasterPage
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0001F5C8 File Offset: 0x0001D7C8
		private int GetPidIfExists()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid_DontTryToAuthenticate(this.Page);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001F5EC File Offset: 0x0001D7EC
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.MODULES_ENABLED_TestBooking);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("~/user/misc/NotAllowed.aspx?code=module", true);
			}
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(TechnoPro.Common.Public.Entities.Settings.Group.TESTBOOKING);
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				int[] settingValue2 = new WebSettingsClientManager().GetSettingValue<int[]>(Setting.TESTBOOKING_RestrictLoginTo);
				bool flag3 = settingValue2 != null && settingValue2.Length != 0;
				if (flag3)
				{
					int pidIfExists = this.GetPidIfExists();
					bool flag4 = pidIfExists > 0;
					if (flag4)
					{
						bool flag5 = Array.IndexOf<int>(settingValue2, pidIfExists) < 0;
						if (flag5)
						{
							NavigatorClientManager.CurrentInstance.NotAllowed(Setting.TESTBOOKING_ErrorMessage_Pilot, this.Page);
						}
					}
				}
			}
		}

		// Token: 0x04000218 RID: 536
		protected ContentPlaceHolder placeholder_content;
	}
}

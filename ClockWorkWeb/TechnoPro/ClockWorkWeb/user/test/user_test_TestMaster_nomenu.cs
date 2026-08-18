using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x0200006E RID: 110
	public class user_test_TestMaster_nomenu : MasterPage
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x0001F4D8 File Offset: 0x0001D6D8
		private int GetPidIfExists()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid_DontTryToAuthenticate(this.Page);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001F4FC File Offset: 0x0001D6FC
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

		// Token: 0x04000216 RID: 534
		protected Label lbl_mainMessage;

		// Token: 0x04000217 RID: 535
		protected ContentPlaceHolder placeholder_content;
	}
}

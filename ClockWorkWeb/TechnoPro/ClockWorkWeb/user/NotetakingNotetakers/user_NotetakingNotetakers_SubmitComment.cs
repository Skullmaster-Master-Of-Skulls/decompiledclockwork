using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000B3 RID: 179
	public class user_NotetakingNotetakers_SubmitComment : Page
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x00029AD8 File Offset: 0x00027CD8
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				IClockWorkMasterPage clockWorkMasterPage = (IClockWorkMasterPage)this.Page.Master;
				clockWorkMasterPage.SetCurrentPage(eClockWorkWebPage.NotetakingNotetakers_SubmitComment);
				clockWorkMasterPage.SetCausesValidationForAllMenuItems(false);
			}
			int pid = this.GetPid();
			bool flag2 = pid <= 0;
			if (flag2)
			{
				base.Response.Redirect("NotetakerAppNew.aspx", true);
			}
			string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_AdminEmail);
			bool flag3 = string.IsNullOrEmpty(settingValue);
			if (flag3)
			{
				settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Notetaking);
			}
			this.cwSubmitComment1.Init(settingValue);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00029B98 File Offset: 0x00027D98
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetNotetakerId(this.Page);
		}

		// Token: 0x040003EA RID: 1002
		protected ScriptManager bbb;

		// Token: 0x040003EB RID: 1003
		protected user_SubmitComment cwSubmitComment1;
	}
}

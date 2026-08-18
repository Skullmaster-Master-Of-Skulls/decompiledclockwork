using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.Intake
{
	// Token: 0x020000C5 RID: 197
	public class user_Intake_Default : Page
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x0002AED8 File Offset: 0x000290D8
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.INTAKE_InformationPageText);
				this.lbl_info.Text = settingValue;
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x0400041C RID: 1052
		protected Button btn_register;

		// Token: 0x0400041D RID: 1053
		protected Panel p_info;

		// Token: 0x0400041E RID: 1054
		protected Label lbl_info;
	}
}

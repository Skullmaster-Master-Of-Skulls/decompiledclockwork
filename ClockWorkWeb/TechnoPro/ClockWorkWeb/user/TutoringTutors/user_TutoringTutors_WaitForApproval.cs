using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x0200004A RID: 74
	public class user_TutoringTutors_WaitForApproval : Page
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.TUTORING_ContactInfo);
				bool flag2 = string.IsNullOrEmpty(settingValue);
				if (flag2)
				{
					settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_DepartmentContactInformation);
				}
				this.lbl_contactInfo.Text = (settingValue ?? "");
			}
		}

		// Token: 0x04000168 RID: 360
		protected Label lblTitle;

		// Token: 0x04000169 RID: 361
		protected Panel p_info;

		// Token: 0x0400016A RID: 362
		protected Label lbl_info;

		// Token: 0x0400016B RID: 363
		protected Label lbl_contactInfo;
	}
}

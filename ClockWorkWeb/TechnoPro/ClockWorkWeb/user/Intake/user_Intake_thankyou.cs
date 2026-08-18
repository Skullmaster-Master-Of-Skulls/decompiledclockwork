using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.ClockWorkWeb.user.Intake
{
	// Token: 0x020000CC RID: 204
	public class user_Intake_thankyou : Page
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x0002BC1C File Offset: 0x00029E1C
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.INTAKE_ThankYouMessage);
				this.lbl_info.Text = settingValue;
			}
		}

		// Token: 0x04000443 RID: 1091
		protected Label lbl_info;
	}
}

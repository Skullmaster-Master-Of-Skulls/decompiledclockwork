using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000B6 RID: 182
	public class user_misc_error : Page
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00029ED8 File Offset: 0x000280D8
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				bool flag = !this.Page.IsPostBack;
				if (flag)
				{
					db conn = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
					string text;
					try
					{
						bool flag2 = base.Server != null && base.Server.GetLastError() != null;
						if (flag2)
						{
							text = base.Server.GetLastError().ToString();
						}
						else
						{
							text = "?";
						}
					}
					catch (Exception ex)
					{
						text = ex.ToString();
					}
					bool settingValueBool = AppSettingsV2.GetSettingValueBool(Setting.GENERAL_ShowErrors, conn, base.Cache);
					bool flag3 = settingValueBool;
					if (flag3)
					{
						this.lbl_err.Text = text;
					}
				}
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000400 RID: 1024
		protected Panel p_err;

		// Token: 0x04000401 RID: 1025
		protected Label lbl_err;
	}
}

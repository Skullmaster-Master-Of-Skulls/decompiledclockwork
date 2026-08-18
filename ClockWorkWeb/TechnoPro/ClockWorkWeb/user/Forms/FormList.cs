using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.Forms
{
	// Token: 0x020000E7 RID: 231
	public class FormList : Page
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x0003512C File Offset: 0x0003332C
		protected void Page_Load(object sender, EventArgs e)
		{
			int num = this.LookupStudentPid();
			bool flag = num < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool flag2 = !webSettingsClientManager.GetSettingValue<bool>(Setting.MODULES_ENABLED_OnlineForms);
				if (flag2)
				{
					base.Response.Redirect("~/custom/misc/home.aspx");
				}
				else
				{
					bool flag3 = !this.Page.IsPostBack;
					if (flag3)
					{
						string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.ONLINEFORMS_StudentFilesIntro);
						bool flag4 = !string.IsNullOrWhiteSpace(settingValue);
						if (flag4)
						{
							this.lbl_formsIntro.Text = settingValue;
						}
					}
				}
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000351D0 File Offset: 0x000333D0
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x04000540 RID: 1344
		protected Label lbl_formsIntro;
	}
}

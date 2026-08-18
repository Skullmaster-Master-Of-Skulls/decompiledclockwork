using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000DE RID: 222
	public class user_instructor_Login : Page
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x00033008 File Offset: 0x00031208
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0003302C File Offset: 0x0003122C
		protected override void OnPreRender(EventArgs e)
		{
			string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_Login_Title);
			string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_Login_Intro);
			string settingValue3 = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_Login_Username_Label);
			this.cwLogin1.FailureText = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_Login_LoginFailedMessage);
			this.cwLogin1.LoginFormType = "instructor";
			bool flag = !string.IsNullOrEmpty(settingValue);
			if (flag)
			{
				this.cwLogin1.TitleText = settingValue;
			}
			bool flag2 = !string.IsNullOrEmpty(settingValue2);
			if (flag2)
			{
				this.cwLogin1.InstructionText = settingValue2;
			}
			bool flag3 = !string.IsNullOrEmpty(settingValue3);
			if (flag3)
			{
				this.cwLogin1.UsernameLabel = settingValue3;
			}
			base.OnPreRender(e);
		}

		// Token: 0x0400050F RID: 1295
		protected ClockWorkLoginControl cwLogin1;

		// Token: 0x04000510 RID: 1296
		protected Panel p_additionalOptions;

		// Token: 0x04000511 RID: 1297
		protected HyperLink link_recoverPassword;
	}
}

using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000043 RID: 67
	public class user_tutoringTutors_login : Page
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000B6E4 File Offset: 0x000098E4
		protected override void OnPreRender(EventArgs e)
		{
			string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_StudentLoginTitle);
			string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_StudentLoginUsernameLabelText);
			string settingValue3 = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_StudentLoginInstructionText);
			string settingValue4 = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_LoginMessage);
			this.cwLogin1.TitleText = settingValue;
			this.cwLogin1.UsernameLabel = settingValue2;
			this.cwLogin1.InstructionText = settingValue3;
			this.cwLogin1.InstructionText2 = settingValue4;
			base.OnPreRender(e);
		}

		// Token: 0x04000143 RID: 323
		protected ClockWorkLoginControl cwLogin1;
	}
}

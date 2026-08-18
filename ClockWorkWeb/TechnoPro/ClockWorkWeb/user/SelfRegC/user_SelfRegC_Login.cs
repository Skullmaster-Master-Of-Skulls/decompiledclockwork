using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.SelfRegC
{
	// Token: 0x02000087 RID: 135
	public class user_SelfRegC_Login : Page
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000211C0 File Offset: 0x0001F3C0
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

		// Token: 0x04000279 RID: 633
		protected ClockWorkLoginControl cwLogin1;
	}
}

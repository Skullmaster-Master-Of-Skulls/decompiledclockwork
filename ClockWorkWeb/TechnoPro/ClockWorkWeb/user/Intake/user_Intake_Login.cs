using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.Intake
{
	// Token: 0x020000C8 RID: 200
	public class user_Intake_Login : Page
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0002B05C File Offset: 0x0002925C
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

		// Token: 0x060005DA RID: 1498 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x04000424 RID: 1060
		protected ClockWorkLoginControl cwLogin1;
	}
}

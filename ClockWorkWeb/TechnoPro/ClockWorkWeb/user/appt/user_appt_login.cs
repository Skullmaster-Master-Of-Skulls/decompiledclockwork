using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000F6 RID: 246
	public class user_appt_login : Page
	{
		// Token: 0x06000722 RID: 1826 RVA: 0x00036A48 File Offset: 0x00034C48
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00036A6C File Offset: 0x00034C6C
		protected override void OnPreRender(EventArgs e)
		{
			this.cwLogin1.UsernameLabel = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_StudentLoginUsernameLabelText);
			this.cwLogin1.InstructionText = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_StudentLoginInstructionText);
			base.OnPreRender(e);
		}

		// Token: 0x04000566 RID: 1382
		protected ClockWorkLoginControl cwLogin1;
	}
}

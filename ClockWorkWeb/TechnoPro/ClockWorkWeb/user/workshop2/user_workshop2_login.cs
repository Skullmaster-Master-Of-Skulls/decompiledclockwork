using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.workshop2
{
	// Token: 0x02000021 RID: 33
	public class user_workshop2_login : Page
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00005ACC File Offset: 0x00003CCC
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005B04 File Offset: 0x00003D04
		protected override void OnPreRender(EventArgs e)
		{
			this.ClockWorkLoginControl1.UsernameLabel = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_StudentLoginUsernameLabelText);
			this.ClockWorkLoginControl1.InstructionText = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_StudentLoginInstructionText);
			base.OnPreRender(e);
		}

		// Token: 0x04000084 RID: 132
		protected ClockWorkLoginControl ClockWorkLoginControl1;
	}
}

using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000A7 RID: 167
	public class user_NotetakingNotetakers_login : Page
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x0002608C File Offset: 0x0002428C
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.SetReturnUrlSpecific("/user/notetakingnotetakers/notetakerapp.aspx");
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000260C0 File Offset: 0x000242C0
		protected override void OnPreRender(EventArgs e)
		{
			this.ClockWorkLoginControl1.UsernameLabel = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_StudentLoginUsernameLabelText);
			this.ClockWorkLoginControl1.InstructionText = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_StudentLoginInstructionText);
			base.OnPreRender(e);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x0400031B RID: 795
		protected ScriptManager bbb;

		// Token: 0x0400031C RID: 796
		protected ClockWorkLoginControl ClockWorkLoginControl1;
	}
}

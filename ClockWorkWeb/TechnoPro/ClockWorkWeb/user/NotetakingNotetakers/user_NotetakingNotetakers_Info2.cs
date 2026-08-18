using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000A6 RID: 166
	public class user_NotetakingNotetakers_Info2 : Page
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0002605C File Offset: 0x0002425C
		protected void Page_Load(object sender, EventArgs e)
		{
			ClockWorkWebCore.DisableNoCache(base.Master);
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000319 RID: 793
		protected ScriptManager bbb;

		// Token: 0x0400031A RID: 794
		protected Label lbl_notes;
	}
}

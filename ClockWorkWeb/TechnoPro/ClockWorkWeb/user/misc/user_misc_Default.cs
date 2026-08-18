using System;
using System.Web.UI;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000B5 RID: 181
	public class user_misc_Default : Page
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x00020282 File Offset: 0x0001E482
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Redirect("~/custom/misc/home.aspx", true);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}
	}
}

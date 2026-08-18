using System;
using System.Web.UI;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.Forms
{
	// Token: 0x020000E2 RID: 226
	public class user_Forms_Default : Page
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x00020282 File Offset: 0x0001E482
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Redirect("~/custom/misc/home.aspx", true);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}
	}
}

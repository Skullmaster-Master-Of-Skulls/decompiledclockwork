using System;
using System.Web.UI;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000BD RID: 189
	public class logoutb : Page
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x0002A391 File Offset: 0x00028591
		protected void Page_Load(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.LogoutFromClockWork();
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}
	}
}

using System;
using System.Web.UI;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.custom.misc
{
	// Token: 0x02000119 RID: 281
	public class custom_misc_Default : Page
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x0003B267 File Offset: 0x00039467
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Redirect("home.aspx", true);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}
	}
}

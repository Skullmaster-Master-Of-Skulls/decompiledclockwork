using System;
using System.Web.UI;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.custom.misc
{
	// Token: 0x0200011A RID: 282
	public class custom_misc_Default2 : Page
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x0003B267 File Offset: 0x00039467
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Redirect("home.aspx", true);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}
	}
}

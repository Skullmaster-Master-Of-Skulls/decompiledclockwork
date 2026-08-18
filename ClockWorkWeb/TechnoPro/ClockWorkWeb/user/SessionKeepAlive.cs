using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user
{
	// Token: 0x0200001B RID: 27
	public class SessionKeepAlive : Page
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00004241 File Offset: 0x00002441
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.ContentType = "text/html";
			base.Response.Write("alive");
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000033 RID: 51
		protected HtmlForm form1;
	}
}

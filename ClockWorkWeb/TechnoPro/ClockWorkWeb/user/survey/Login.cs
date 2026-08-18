using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.survey
{
	// Token: 0x02000074 RID: 116
	public class Login : Page
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00003E0A File Offset: 0x0000200A
		protected override void OnPreRender(EventArgs e)
		{
		}

		// Token: 0x0400022D RID: 557
		protected ClockWorkLoginControl cwLogin1;
	}
}

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x02000095 RID: 149
	public class user_NotetakingStudents_Info2 : Page
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x00023B8C File Offset: 0x00021D8C
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x040002C1 RID: 705
		protected ScriptManager bbb;

		// Token: 0x040002C2 RID: 706
		protected Label lbl_notes;
	}
}

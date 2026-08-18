using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.survey
{
	// Token: 0x02000073 RID: 115
	public class user_survey_Default : Page
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x0001F9F8 File Offset: 0x0001DBF8
		private int GetScreenNum()
		{
			string text = base.Request.QueryString["screennum"];
			int num;
			return (string.IsNullOrEmpty(text) || !int.TryParse(text, out num)) ? 0 : num;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001FA38 File Offset: 0x0001DC38
		protected void Page_Load(object sender, EventArgs e)
		{
			int screenNum = this.GetScreenNum();
			bool flag = screenNum > 0;
			if (flag)
			{
				base.Response.Redirect("IntakeForm.aspx", true);
			}
			else
			{
				bool flag2 = !this.Page.IsPostBack;
				if (flag2)
				{
				}
			}
		}

		// Token: 0x04000229 RID: 553
		protected ScriptManager bbb;

		// Token: 0x0400022A RID: 554
		protected Panel p_errmsg;

		// Token: 0x0400022B RID: 555
		protected Label lbl_msg;

		// Token: 0x0400022C RID: 556
		protected Button btn_home;
	}
}

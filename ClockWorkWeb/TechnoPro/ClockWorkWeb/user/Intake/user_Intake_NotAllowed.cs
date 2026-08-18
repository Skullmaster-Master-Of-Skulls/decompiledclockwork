using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.ClockWorkWeb.user.Intake
{
	// Token: 0x020000CA RID: 202
	public class user_Intake_NotAllowed : Page
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x0002B0E8 File Offset: 0x000292E8
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string a = (base.Request.QueryString["code"] ?? "").Trim().ToLower();
				bool flag2 = a == "incw";
				if (flag2)
				{
					this.lbl_msg.Text = "You have already completed the intake process.";
				}
			}
		}

		// Token: 0x04000425 RID: 1061
		protected Panel p_msg;

		// Token: 0x04000426 RID: 1062
		protected Label lbl_msg;

		// Token: 0x04000427 RID: 1063
		protected Button btn_home;
	}
}

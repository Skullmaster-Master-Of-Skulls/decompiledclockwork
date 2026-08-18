using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000AD RID: 173
	public class user_NotetakingNotetakers_NotetakerBecomeUnavailable : Page
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x0002860C File Offset: 0x0002680C
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00024958 File Offset: 0x00022B58
		protected void btn_cancel1_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("NotetakerApp.aspx");
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0002862E File Offset: 0x0002682E
		protected void btn_accept1_Click(object sender, EventArgs e)
		{
			this.Session["msgcode"] = "becomeunavailable";
			this.Session["msgcodedesc"] = "1";
			base.Response.Redirect("NotetakerApp.aspx");
		}

		// Token: 0x0400038F RID: 911
		protected ScriptManager bbb;

		// Token: 0x04000390 RID: 912
		protected Label lblTitle;

		// Token: 0x04000391 RID: 913
		protected Label lbl_course;

		// Token: 0x04000392 RID: 914
		protected Panel p_regular;

		// Token: 0x04000393 RID: 915
		protected Panel p_special;

		// Token: 0x04000394 RID: 916
		protected Label lbl_specialnote;

		// Token: 0x04000395 RID: 917
		protected Label lbl_msgregular;

		// Token: 0x04000396 RID: 918
		protected Button btn_accept1;

		// Token: 0x04000397 RID: 919
		protected Button btn_cancel1;
	}
}

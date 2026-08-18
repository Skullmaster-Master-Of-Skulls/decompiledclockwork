using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x02000097 RID: 151
	public class Message : Page
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x00023C30 File Offset: 0x00021E30
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				object obj = base.Request.QueryString["msgcode"];
				bool flag2 = obj != null;
				if (flag2)
				{
					string text = obj.ToString();
					string a = text;
					if (!(a == "banned"))
					{
						if (a == "expired")
						{
							this.lbl_message.Text = "Your accommodations are expired.  Please contact us in order to renew them.";
						}
					}
				}
			}
		}

		// Token: 0x040002C7 RID: 711
		protected RadScriptManager radScriptManager1;

		// Token: 0x040002C8 RID: 712
		protected Panel p_message;

		// Token: 0x040002C9 RID: 713
		protected Label lbl_message;
	}
}

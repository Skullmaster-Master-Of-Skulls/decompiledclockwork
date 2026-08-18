using System;
using System.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.student
{
	// Token: 0x0200007A RID: 122
	public class Default : Page
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x00020282 File Offset: 0x0001E482
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Redirect("~/custom/misc/home.aspx", true);
		}
	}
}

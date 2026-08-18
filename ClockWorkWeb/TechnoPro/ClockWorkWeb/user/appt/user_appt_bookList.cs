using System;
using System.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000F2 RID: 242
	public class user_appt_bookList : Page
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x000360DC File Offset: 0x000342DC
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Redirect("book.aspx");
		}
	}
}

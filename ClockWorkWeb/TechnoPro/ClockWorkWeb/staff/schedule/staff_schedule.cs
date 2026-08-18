using System;
using System.Web.UI;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x02000107 RID: 263
	public class staff_schedule : Page
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x000397A1 File Offset: 0x000379A1
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Redirect("StaffCalendar.aspx", true);
		}

		// Token: 0x040005F9 RID: 1529
		protected ScriptManager bbb;
	}
}

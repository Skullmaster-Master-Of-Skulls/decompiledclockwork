using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.Configuration;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000DF RID: 223
	public class user_instructor_Message : Page
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x000330F0 File Offset: 0x000312F0
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				object obj = base.Request.QueryString["msgcode"];
				bool flag2 = obj != null;
				if (flag2)
				{
					db conn = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
					string text = obj.ToString();
					string a = text;
					if (!(a == "notallowed"))
					{
						if (a == "notallowedtoaddexam")
						{
							this.lbl_message.Text = "You cannot add a new test or exam for a date that is outside of the course start and end date range.  Please check that you are adding the correct test date for the correct course.  Please contact us if you require assistance.";
						}
					}
					else
					{
						string contactUsString = ClockWorkWebCore.GetContactUsString(conn, base.Cache);
						this.lbl_message.Text = "This site is only for instructors.  If you are an instructor please " + ClockWorkWebCore.GetContactUsString(conn, base.Cache) + " for assistance.";
					}
				}
			}
		}

		// Token: 0x04000512 RID: 1298
		protected Panel p_message;

		// Token: 0x04000513 RID: 1299
		protected Label lbl_message;
	}
}

using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.workshop2
{
	// Token: 0x02000023 RID: 35
	public class user_workshop2_MyUpcomingAppts : Page
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00005C44 File Offset: 0x00003E44
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005C68 File Offset: 0x00003E68
		protected void Page_Load(object sender, EventArgs e)
		{
			int num = this.LookupStudentPid();
			bool flag = num <= 0;
			if (flag)
			{
				bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_allowNonClockWorkStudentsToRegister);
				bool flag2 = settingValue;
				if (flag2)
				{
					base.Response.Redirect("NewUser.aspx", true);
				}
				else
				{
					base.Response.Redirect("Message.aspx?msgcode=notallowed", true);
				}
			}
			else
			{
				bool flag3 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
				if (flag3)
				{
					((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.WorkshopBooking_Calendar);
				}
				bool flag4 = !this.Page.IsPostBack;
				if (flag4)
				{
				}
			}
		}

		// Token: 0x04000087 RID: 135
		protected MyUpcomingAppointmentsControl AppsControl1;
	}
}

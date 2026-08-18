using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x0200006C RID: 108
	public class user_TutorSchedule_MyUpcomingAppts2 : Page
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			int num = this.LookupStudentPid();
			bool flag = num <= 0;
			if (flag)
			{
				bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_allowNonClockWorkStudentsToRegister);
				bool flag2 = settingValue;
				if (flag2)
				{
					try
					{
						base.Response.Redirect("NewUser.aspx", true);
					}
					catch
					{
						NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
					}
				}
				else
				{
					NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
				}
			}
			else
			{
				bool flag3 = !this.Page.IsPostBack;
				if (flag3)
				{
					bool flag4 = base.Master != null && base.Master is IClockWorkMasterPage;
					if (flag4)
					{
						((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TestBooking_Calendar);
					}
				}
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001F1C0 File Offset: 0x0001D3C0
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x04000210 RID: 528
		protected MyUpcomingAppointmentsControl AppsControl1;
	}
}

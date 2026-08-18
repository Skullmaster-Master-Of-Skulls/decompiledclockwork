using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000FA RID: 250
	public class user_appt_submitcomment : Page
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x00037888 File Offset: 0x00035A88
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			int pid = this.GetPid();
			bool flag = pid <= 0;
			if (flag)
			{
				base.Response.Redirect("Message.aspx?msgcode=notallowed", true);
			}
			bool flag2 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag2)
			{
				IClockWorkMasterPage clockWorkMasterPage = (IClockWorkMasterPage)this.Page.Master;
				clockWorkMasterPage.SetCurrentPage(eClockWorkWebPage.AppointmentBooking_SubmitComment);
				clockWorkMasterPage.SetCausesValidationForAllMenuItems(false);
			}
			this.cwSubmitComment1.Init(new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_DefaultFrom_AppointmentBooking));
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00037938 File Offset: 0x00035B38
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x04000585 RID: 1413
		protected user_SubmitComment cwSubmitComment1;
	}
}

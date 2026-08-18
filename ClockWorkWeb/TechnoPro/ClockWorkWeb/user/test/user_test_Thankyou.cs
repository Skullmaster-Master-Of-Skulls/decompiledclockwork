using System;
using System.Data;
using System.Data.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI.TestBooking;
using Databases;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x02000070 RID: 112
	public class user_test_Thankyou : Page
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0001F6B8 File Offset: 0x0001D8B8
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				bool flag2 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag2)
				{
					((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TestBooking_BookTest);
				}
				this.lbl_thankyou.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.TESTBOOKING_WizardSetting_FinishedBookingMsg);
				object obj = this.Session["lastbookedtest"];
				bool flag3 = obj != null && obj is BookedTest;
				if (flag3)
				{
					BookedTest bookedTest = (BookedTest)obj;
					int appointmentId = this.GetAppointmentId();
					int personId = this.GetPersonId();
					bool flag4 = appointmentId > 0;
					if (flag4)
					{
						this.AppToScreen(appointmentId, personId);
					}
				}
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001F784 File Offset: 0x0001D984
		private void AppToScreen(int appId, int pid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@appid", DbType.Int32, appId),
				clockWork.GetParameter("@pid", DbType.Int32, pid)
			};
			string query = "SELECT    a.startdate,a.enddate,a.\r\n            ,at.description\r\n            ,p.firstname,p.lastname,p.student_no\r\n            ,proom.firstnam AS room\r\nFROM        apps a LEFT JOIN appointmenttypes at ON at.apptypeid=a.apptypeid\r\n            LEFT JOIN people p ON p.personid=a.personid\r\n            LEFT JOIN perstudentdata2 ps2 ON ps2.personid=a.personid AND\r\n            LEFT JOIN attendees attroom ON attroom.appointmentid=a.appointmentid AND attroom.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n            LEFT JOIN people proom ON proom.personid=attroom.personid\r\n            LEFT JOIN examaccommodations ea ON ea.appointmentid=a.appointmentid\r\nWHERE a.appointmentid=@appid AND a.personid=@pid";
			DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		private int GetAppointmentId()
		{
			string text = base.Request.QueryString["appid"];
			bool flag = string.IsNullOrEmpty(text);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num;
				bool flag2 = !int.TryParse(text, out num);
				if (flag2)
				{
					result = 0;
				}
				else
				{
					result = num;
				}
			}
			return result;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001F840 File Offset: 0x0001DA40
		private int GetPersonId()
		{
			string text = base.Request.QueryString["pid"];
			bool flag = string.IsNullOrEmpty(text);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num;
				bool flag2 = !int.TryParse(text, out num);
				if (flag2)
				{
					result = 0;
				}
				else
				{
					result = num;
				}
			}
			return result;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000D21E File Offset: 0x0000B41E
		protected void btn_again_click(object sender, EventArgs e)
		{
			base.Response.Redirect("book.aspx", true);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00004233 File Offset: 0x00002433
		protected void btn_logout_Click(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
		}

		// Token: 0x04000219 RID: 537
		protected Label lbl_thankyou;

		// Token: 0x0400021A RID: 538
		protected Panel p_bookagain;

		// Token: 0x0400021B RID: 539
		protected Label lbl_bookagain;

		// Token: 0x0400021C RID: 540
		protected Button btn_again;

		// Token: 0x0400021D RID: 541
		protected Button btn_logout;
	}
}

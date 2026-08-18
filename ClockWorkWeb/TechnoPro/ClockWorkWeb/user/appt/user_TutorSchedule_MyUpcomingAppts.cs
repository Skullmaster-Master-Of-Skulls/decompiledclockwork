using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using Databases;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000F8 RID: 248
	public class user_TutorSchedule_MyUpcomingAppts : Page
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x00036C04 File Offset: 0x00034E04
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00036C28 File Offset: 0x00034E28
		protected void AppsControl1_AppointmentCancelled(object sender, int appId, EventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@appid", DbType.Int32, appId)
			};
			DataTable dataTable = clockWork.ExecuteQuery(ClockWorkWebAPI.QueryStorage.QS_Select_WaitingList, parameters);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[0];
				int num2 = (int)dataRow[1];
				int num3 = (int)dataRow[2];
				bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_NoConsecutiveOrOverlapping);
				int settingValue2 = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_maxNumApptsInFuture);
				int settingValue3 = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_MaxNumAppsPerWeek);
				int settingValue4 = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedNumDays);
				int settingValue5 = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedExpiryDateCid);
				int settingValue6 = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedNumNoshows);
				string text;
				bool flag = Appointment.IsUserAllowedToBook(appId, num, settingValue2, settingValue3, settingValue5, settingValue4, settingValue, out text);
				bool flag2 = flag;
				if (flag2)
				{
					DbTransaction transaction = clockWork.BeginDbTransaction();
					try
					{
						parameters = new DbParameter[]
						{
							clockWork.GetParameter("@appid", DbType.Int32, appId)
						};
						clockWork.ExecuteNonQueryTransaction(ClockWorkWebAPI.QueryStorage.QS_Delete_DeleteAttendee, transaction, parameters);
						parameters = new DbParameter[]
						{
							clockWork.GetParameter("@pid", DbType.Int32, num),
							clockWork.GetParameter("@appid", DbType.Int32, appId)
						};
						clockWork.ExecuteNonQueryTransaction(ClockWorkWebAPI.QueryStorage.QS_INSERT_NewAttendee, transaction, parameters);
						clockWork.CommitDbTransaction(transaction);
					}
					catch
					{
						clockWork.RollbackDbTransaction(transaction);
					}
					string[] array = new string[]
					{
						"maininfo",
						"otherinfo",
						"datetimeinfo",
						"imageinfo"
					};
					foreach (string text2 in array)
					{
						parameters = new DbParameter[]
						{
							clockWork.GetParameter("@appid", DbType.Int32, appId),
							clockWork.GetParameter("@pid", DbType.Int32, num),
							clockWork.GetParameter("@waitinglistid", DbType.Int32, num2)
						};
						string query = string.Concat(new string[]
						{
							"INSERT INTO ",
							text2,
							"pa (screennum,personid,controlid,controlvalue,appointmentid) SELECT screennum,personid,controlid,controlvalue,@appid FROM ",
							text2,
							"wl WHERE personid=@pid AND appointmentid=@waitinglistid"
						});
						clockWork.ExecuteNonQuery(query, parameters);
					}
					parameters = new DbParameter[]
					{
						clockWork.GetParameter("@id", DbType.Int32, num2)
					};
					clockWork.ExecuteNonQuery(ClockWorkWebAPI.QueryStorage.QS_Delete_WaitingListEntry, parameters);
					break;
				}
				parameters = new DbParameter[]
				{
					clockWork.GetParameter("@reason", DbType.String, ""),
					clockWork.GetParameter("@id", DbType.Int32, num2)
				};
				clockWork.ExecuteNonQuery(ClockWorkWebAPI.QueryStorage.QS_Update_WaitingListEntry, parameters);
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00036F64 File Offset: 0x00035164
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid <= 0;
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
					((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.AppointmentBooking_Calendar);
				}
				List<int> list;
				List<int> list2;
				List<AppType> list3;
				PersonList.GetUniquePidsAndAvailabilityGroupIds(base.Cache, out list, out list2, out list3, new List<string>());
				this.AppsControl1.IsFacilitator = list.Contains(pid);
				bool flag4 = !this.Page.IsPostBack;
				if (flag4)
				{
					string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(Setting.APPOINTMENTBOOKING_MyUpcomingAppointmentsIntro);
					bool flag5 = settingValue2.Length > 0;
					if (flag5)
					{
						this.p_intro.Visible = true;
						this.lbl_intro.Text = settingValue2;
					}
				}
			}
		}

		// Token: 0x04000569 RID: 1385
		protected Panel p_intro;

		// Token: 0x0400056A RID: 1386
		protected Label lbl_intro;

		// Token: 0x0400056B RID: 1387
		protected MyUpcomingAppointmentsControl AppsControl1;
	}
}

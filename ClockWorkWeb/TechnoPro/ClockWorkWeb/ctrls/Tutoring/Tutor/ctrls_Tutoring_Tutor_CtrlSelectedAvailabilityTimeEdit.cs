using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.Core.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AvailabilitySchedule;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutor
{
	// Token: 0x0200012B RID: 299
	public class ctrls_Tutoring_Tutor_CtrlSelectedAvailabilityTimeEdit : UserControl
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x0003F71C File Offset: 0x0003D91C
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				IList<DateTime> list = (IList<DateTime>)base.Session["tutordates"];
				bool flag2 = list != null;
				if (flag2)
				{
					this.lbl_dates.Text = string.Join(", ", list.ToList<DateTime>().ConvertAll<string>((DateTime g) => g.ToString("ddd MMM d")).ToArray());
				}
				string s = base.Request.QueryString["msg"];
				int num;
				bool flag3 = !int.TryParse(s, out num);
				if (flag3)
				{
					num = 0;
				}
				string text = base.Request.QueryString["t"];
				string text2 = base.Request.QueryString["d"];
				DateTime date = DateTime.Now.Date;
				int num2;
				int num3;
				bool flag4 = !string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && int.TryParse(text, out num2) && int.TryParse(text2, out num3) && num2 >= 0 && num3 > 0;
				if (flag4)
				{
					DateTime dateTime = date.AddMinutes((double)num2);
					DateTime dateTime2 = date.AddMinutes((double)(num2 + num3));
					this.cmb.SelectedValue = num3.ToString();
					this.tp.SelectedTime = new TimeSpan?(dateTime.TimeOfDay);
					bool flag5 = num == 1;
					if (flag5)
					{
						this.DisplayAddNewMessage(string.Format("Successfully added {0} to {1}", dateTime.ToString("h:mm tt"), dateTime2.ToString("h:mm tt")));
					}
				}
				bool flag6 = num == 2;
				if (flag6)
				{
					string text3 = base.Request.QueryString["st"];
					string text4 = base.Request.QueryString["et"];
					int num4;
					int num5;
					bool flag7 = !string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4) && int.TryParse(text3, out num4) && int.TryParse(text4, out num5);
					if (flag7)
					{
						DateTime dateTime3 = date.AddMinutes((double)num4);
						DateTime dateTime4 = date.AddMinutes((double)num5);
						this.DisplayDeleteMessage(string.Format("Successfully deleted {0} to {1}", dateTime3.ToString("h:mm tt"), dateTime4.ToString("h:mm tt")));
						this.cmb.SelectedValue = (num5 - num4).ToString();
						this.tp.SelectedTime = new TimeSpan?(dateTime3.TimeOfDay);
					}
				}
				string text5 = new WebSettingsClientManager().GetSettingValue<string>(Setting.TUTORING_Availability_DurationsAvailable) ?? "";
				string[] array = text5.Split(new char[]
				{
					','
				});
				foreach (string text6 in array)
				{
					int num6;
					bool flag8 = int.TryParse(text6.Trim(), out num6) && num6 > 0;
					if (flag8)
					{
						string durationDescription = num6.GetDurationDescription();
						this.cmb.Items.Add(new ListItem(durationDescription, num6.ToString()));
					}
				}
				this.RadGrid1.Rebind();
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0003FA48 File Offset: 0x0003DC48
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0003FA6C File Offset: 0x0003DC6C
		private void RefreshTimes()
		{
			IList<DateTime> list = (IList<DateTime>)base.Session["tutordates"];
			bool flag = list != null;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
				AvailabilityScheduleContextDTO availabilityScheduleContextDTO = new AvailabilityScheduleContextDTO();
				availabilityScheduleContextDTO.PersonId = this.LookupStudentPid();
				availabilityScheduleContextDTO.AvailabilityGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
			}
			else
			{
				IList<ctrls_Tutoring_Tutor_CtrlSelectedAvailabilityTimeEdit.AvailabilityItemWrapper> list2 = new List<ctrls_Tutoring_Tutor_CtrlSelectedAvailabilityTimeEdit.AvailabilityItemWrapper>();
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0003FAD0 File Offset: 0x0003DCD0
		protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
		{
			IList<DateTime> list = (IList<DateTime>)base.Session["tutordates"];
			bool flag = list != null;
			if (flag)
			{
				bool flag2 = e.CommandName == "delete";
				if (flag2)
				{
					ctrls_Tutoring_Tutor_CtrlSelectedAvailabilityTimeEdit.AvailabilityItemWrapper availabilityItemWrapper = ctrls_Tutoring_Tutor_CtrlSelectedAvailabilityTimeEdit.AvailabilityItemWrapper.FromTime(e.CommandArgument.ToString());
					bool flag3 = availabilityItemWrapper == null;
					if (!flag3)
					{
						ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
						int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
						AvailabilityScheduleContextDTO availabilityScheduleContextDTO = new AvailabilityScheduleContextDTO();
						availabilityScheduleContextDTO.AvailabilityGroupId = tutorAvailabilityScheduleGroupId;
						availabilityScheduleContextDTO.PersonId = this.LookupStudentPid();
						IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
						base.Response.Redirect(string.Format("availabilityEdit.aspx?msg=2&st={0}&et={1}", availabilityItemWrapper.StartTime.TotalMinutes.ToString(), availabilityItemWrapper.EndTime.TotalMinutes.ToString()));
					}
				}
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0003FBB4 File Offset: 0x0003DDB4
		public IList<Range<TimeSpan>> SelectedTimes
		{
			get
			{
				return new List<Range<TimeSpan>>();
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0003FBCD File Offset: 0x0003DDCD
		protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			this.RefreshTimes();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0003FBD7 File Offset: 0x0003DDD7
		private void DisplayDeleteMessage(string msg)
		{
			this.lbl_delMessage.Text = msg;
			this.p_delMessage.Visible = true;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0003FBF4 File Offset: 0x0003DDF4
		private void DisplayAddNewMessage(string msg)
		{
			this.lbl_addNewMessage.Text = msg;
			this.p_addNewMessage.Visible = true;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0003FC14 File Offset: 0x0003DE14
		protected void btn_addOtherTime_Click(object sender, EventArgs e)
		{
			IList<DateTime> list = (IList<DateTime>)base.Session["tutordates"];
			bool flag = list != null;
			if (flag)
			{
				bool flag2 = this.tp.SelectedDate == null;
				if (flag2)
				{
					this.DisplayAddNewMessage("Please select a valid time first.");
				}
				else
				{
					TimeSpan timeOfDay = this.tp.SelectedDate.Value.TimeOfDay;
					string selectedValue = this.cmb.SelectedValue;
					int num;
					bool flag3 = string.IsNullOrEmpty(selectedValue) || !int.TryParse(selectedValue, out num);
					if (flag3)
					{
						this.DisplayAddNewMessage("Please select a valid duration first.");
					}
					else
					{
						DateTime date = DateTime.Now.Date;
						DateTime dateTime = date.Add(timeOfDay);
						DateTime dateTime2 = date.Add(timeOfDay).AddMinutes((double)num);
						TimeSpan timeOfDay2 = dateTime.TimeOfDay;
						TimeSpan timeOfDay3 = dateTime2.TimeOfDay;
						ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
						AvailabilityScheduleContextDTO availabilityScheduleContextDTO = new AvailabilityScheduleContextDTO();
						availabilityScheduleContextDTO.AvailabilityGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
						availabilityScheduleContextDTO.PersonId = this.LookupStudentPid();
						base.Response.Redirect(string.Format("availabilityEdit.aspx?msg=1&t={0}&d={1}", timeOfDay2.TotalMinutes.ToString(), num.ToString()));
					}
				}
			}
		}

		// Token: 0x040006CB RID: 1739
		protected Panel p_editAvailability;

		// Token: 0x040006CC RID: 1740
		protected Panel p_delMessage;

		// Token: 0x040006CD RID: 1741
		protected Label lbl_delMessage;

		// Token: 0x040006CE RID: 1742
		protected RadGrid RadGrid1;

		// Token: 0x040006CF RID: 1743
		protected Label lbl_notAll;

		// Token: 0x040006D0 RID: 1744
		protected Label lbl_infoNotAll;

		// Token: 0x040006D1 RID: 1745
		protected Panel p_dates;

		// Token: 0x040006D2 RID: 1746
		protected Label lbl_dates;

		// Token: 0x040006D3 RID: 1747
		protected Panel p_add;

		// Token: 0x040006D4 RID: 1748
		protected Panel p_addNewMessage;

		// Token: 0x040006D5 RID: 1749
		protected Label lbl_addNewMessage;

		// Token: 0x040006D6 RID: 1750
		protected RadTimePicker tp;

		// Token: 0x040006D7 RID: 1751
		protected DropDownList cmb;

		// Token: 0x040006D8 RID: 1752
		protected LinkButton btn_addOtherTime;

		// Token: 0x02000241 RID: 577
		internal class AvailabilityItemWrapper
		{
			// Token: 0x06000EDC RID: 3804 RVA: 0x0000AF9E File Offset: 0x0000919E
			public AvailabilityItemWrapper()
			{
			}

			// Token: 0x1700034A RID: 842
			// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00050BF6 File Offset: 0x0004EDF6
			// (set) Token: 0x06000EDE RID: 3806 RVA: 0x00050BFE File Offset: 0x0004EDFE
			public TimeSpan StartTime { get; set; }

			// Token: 0x1700034B RID: 843
			// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00050C07 File Offset: 0x0004EE07
			// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00050C0F File Offset: 0x0004EE0F
			public TimeSpan EndTime { get; set; }

			// Token: 0x1700034C RID: 844
			// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00050C18 File Offset: 0x0004EE18
			// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00050C20 File Offset: 0x0004EE20
			public bool AppliesToAllDates { get; set; }

			// Token: 0x1700034D RID: 845
			// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00050C2C File Offset: 0x0004EE2C
			public bool IsDifferentOnAtLeastOneDate
			{
				get
				{
					return !this.AppliesToAllDates;
				}
			}

			// Token: 0x1700034E RID: 846
			// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00050C48 File Offset: 0x0004EE48
			public string FormattedTime
			{
				get
				{
					DateTime date = DateTime.Now.Date;
					DateTime d = date.Add(this.StartTime);
					DateTime d2 = date.Add(this.EndTime);
					int durationInMinutes = Convert.ToInt32((d2 - d).TotalMinutes);
					return d.ToString("h:mm tt") + " (" + durationInMinutes.GetDurationDescription() + ")";
				}
			}

			// Token: 0x1700034F RID: 847
			// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00050CC0 File Offset: 0x0004EEC0
			public string Time
			{
				get
				{
					DateTime date = DateTime.Now.Date;
					DateTime dateTime = date.Add(this.StartTime);
					DateTime dateTime2 = date.Add(this.EndTime);
					return dateTime.ToString("h:mm tt") + "-" + dateTime2.ToString("h:mm tt");
				}
			}

			// Token: 0x06000EE6 RID: 3814 RVA: 0x00050D20 File Offset: 0x0004EF20
			public AvailabilityItemWrapper(TimeSpan st, TimeSpan et)
			{
				this.StartTime = st;
				this.EndTime = et;
			}

			// Token: 0x06000EE7 RID: 3815 RVA: 0x00050D3C File Offset: 0x0004EF3C
			public static ctrls_Tutoring_Tutor_CtrlSelectedAvailabilityTimeEdit.AvailabilityItemWrapper FromTime(string time)
			{
				int num = (time == null) ? -1 : time.IndexOf("-");
				bool flag = num < 1;
				ctrls_Tutoring_Tutor_CtrlSelectedAvailabilityTimeEdit.AvailabilityItemWrapper result;
				if (flag)
				{
					result = null;
				}
				else
				{
					string text = time.Substring(0, num);
					string text2 = time.Substring(num + 1);
					string str = DateTime.Now.ToString("yyyy-MM-dd");
					text = str + " " + text;
					text2 = str + " " + text2;
					DateTime dateTime;
					DateTime dateTime2;
					bool flag2 = DateTime.TryParse(text, out dateTime) && DateTime.TryParse(text2, out dateTime2);
					if (flag2)
					{
						result = new ctrls_Tutoring_Tutor_CtrlSelectedAvailabilityTimeEdit.AvailabilityItemWrapper(dateTime.TimeOfDay, dateTime2.TimeOfDay);
					}
					else
					{
						result = null;
					}
				}
				return result;
			}
		}
	}
}

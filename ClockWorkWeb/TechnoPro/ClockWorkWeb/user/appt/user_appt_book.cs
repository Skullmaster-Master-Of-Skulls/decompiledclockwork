using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.StudentAppointmentBooking;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.AppointmentsCalendar;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsCalendar;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.Accessible;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000F0 RID: 240
	public class user_appt_book : Page
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x000357EC File Offset: 0x000339EC
		[WebMethod(EnableSession = true)]
		public static IList<user_appt_book.AvailabilityItem> LoadAvailability(string channelId, string optionalCalendarTitle, DateTime startDate, int numDays)
		{
			int studentPid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid();
			HttpSessionState session = HttpContext.Current.Session;
			session.Add("AppointmentBookingCalendarContext_ChannelId", channelId ?? "");
			session.Add("AppointmentBookingCalendarContext_Date", startDate.ToString("yyyy-MM-dd"));
			session.Add("AppointmentBookingCalendarContext_WithWhom", optionalCalendarTitle ?? "");
			IAppointmentBookingStudentClientManager appointmentBookingStudentClientManager = new AppointmentBookingStudentClientManager();
			IList<ChannelCalendarWithAvailabilityDTO> list = appointmentBookingStudentClientManager.LoadAvailabilityForChannel(studentPid, channelId, optionalCalendarTitle, startDate, numDays);
			List<user_appt_book.AvailabilityItem> list2 = new List<user_appt_book.AvailabilityItem>();
			foreach (ChannelCalendarWithAvailabilityDTO channelCalendarWithAvailabilityDTO in list)
			{
				foreach (AvailabilityForChannelCalendarDTO availabilityForChannelCalendarDTO in channelCalendarWithAvailabilityDTO.Availabilities)
				{
					list2.Add(new user_appt_book.AvailabilityItem
					{
						ChannelId = channelId,
						AvailabilityGroupId = availabilityForChannelCalendarDTO.AvailabilityGroupId,
						AvailabilityTitle = availabilityForChannelCalendarDTO.AvailabilityTitle,
						StartDateTime = availabilityForChannelCalendarDTO.StartDateTime,
						EndDateTime = availabilityForChannelCalendarDTO.EndDateTime,
						PersonIds = availabilityForChannelCalendarDTO.PersonIds,
						CalendarTitle = channelCalendarWithAvailabilityDTO.CalendarTitle
					});
				}
			}
			list2.Sort((user_appt_book.AvailabilityItem g1, user_appt_book.AvailabilityItem g2) => g1.StartDateTime.CompareTo(g2.StartDateTime));
			return list2;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0003598C File Offset: 0x00033B8C
		[WebMethod(EnableSession = true)]
		public static user_appt_book.ChannelListItem[] GetAvailableFors()
		{
			bool flag = true;
			IList<string> allowedChannels = null;
			bool flag2 = flag;
			if (flag2)
			{
				HttpSessionState session = HttpContext.Current.Session;
				object obj = session["allowedChannels"];
				allowedChannels = ((obj != null && obj is IList<string>) ? ((IList<string>)obj) : null);
			}
			int studentPid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid();
			IAppointmentBookingStudentClientManager appointmentBookingStudentClientManager = new AppointmentBookingStudentClientManager();
			IList<ChannelDTO> source = appointmentBookingStudentClientManager.GetActiveChannelsForStudent(studentPid);
			bool flag3 = allowedChannels != null;
			if (flag3)
			{
				source = (from g in source
				where allowedChannels.Any(delegate(string h)
				{
					ChannelDTO g = g;
					return g != null && g.Id.Equals(h, StringComparison.OrdinalIgnoreCase);
				})
				select g).ToList<ChannelDTO>();
			}
			return source.Select(delegate(ChannelDTO q)
			{
				user_appt_book.ChannelListItem channelListItem = new user_appt_book.ChannelListItem();
				channelListItem.Title = q.Title;
				channelListItem.Id = q.Id;
				channelListItem.CalendarTitles = (from m in q.Availabilities.SelectMany((ChannelAvailabilityDTO g) => g.PersonCollection)
				select m.Title).Distinct<string>().ToArray<string>();
				return channelListItem;
			}).ToArray<user_appt_book.ChannelListItem>();
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00035A60 File Offset: 0x00033C60
		[WebMethod(EnableSession = true)]
		public static void SetStudentAccessibleView(bool isGraphical)
		{
			int studentPid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid();
			bool flag = studentPid < 1;
			if (!flag)
			{
				IAccessibilityWebClientManager accessibilityWebClientManager = new AccessibilityWebClientManager();
				accessibilityWebClientManager.SetStudentAccessibleViewSetting(studentPid, isGraphical ? eClockWorkWebAccessibleView.GraphicalView : eClockWorkWebAccessibleView.ListView);
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00035A98 File Offset: 0x00033C98
		private static PreCalendarQuestionnaireOptions GetPreCalendarQuestionnaireOptions()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string s = (webSettingsClientManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_PreCalendarQuestionnaire) ?? "").Trim();
			return s.GetPreCalendarQuestionnaireOptionsFromString();
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00035AD0 File Offset: 0x00033CD0
		protected void Page_Load(object sender, EventArgs e)
		{
			int getWhoAmIPid = this.GetWhoAmIPid;
			PreCalendarQuestionnaireOptions preCalendarQuestionnaireOptions = user_appt_book.GetPreCalendarQuestionnaireOptions();
			bool flag = preCalendarQuestionnaireOptions != null && preCalendarQuestionnaireOptions.IsEnabled;
			bool flag2 = flag;
			if (flag2)
			{
				this.p_questionnaire.Visible = true;
				bool flag3 = !string.IsNullOrWhiteSpace(preCalendarQuestionnaireOptions.NoChannelsAvailableMessage);
				if (flag3)
				{
					this.lbl_NoChannelsMessage.Text = preCalendarQuestionnaireOptions.NoChannelsAvailableMessage;
				}
				object obj = this.Session["allowedChannels"];
				IList<string> list = (obj != null && obj is IList<string>) ? ((IList<string>)obj) : null;
				bool flag4 = list == null;
				if (flag4)
				{
					base.Response.Redirect("bookfrm.aspx");
					return;
				}
			}
			bool flag5 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag5)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.AppointmentBooking_ScheduleAppointment);
			}
			IAppointmentBookingStudentWebClientManager appointmentBookingStudentWebClientManager = new AppointmentBookingStudentWebClientManager();
			bool flag6 = appointmentBookingStudentWebClientManager.IsStudentBannedFromOnlineAppointmentBooking(getWhoAmIPid);
			if (flag6)
			{
				base.Response.Redirect("Message.aspx?code=banned", true);
			}
			HttpSessionState session = this.Session;
			object obj2 = session["AppointmentBookingCalendarContext_ChannelId"];
			string value = ((obj2 != null) ? obj2.ToString() : null) ?? "";
			object obj3 = session["AppointmentBookingCalendarContext_WithWhom"];
			string value2 = ((obj3 != null) ? obj3.ToString() : null) ?? "";
			object obj4 = session["AppointmentBookingCalendarContext_Date"];
			string value3 = ((obj4 != null) ? obj4.ToString() : null) ?? "";
			this.hidden_channelId.Value = value;
			this.hidden_date.Value = value3;
			this.hidden_withWhom.Value = value2;
			IAccessibilityWebClientManager accessibilityWebClientManager = new AccessibilityWebClientManager();
			eClockWorkWebAccessibleView studentAccessibleViewSetting = accessibilityWebClientManager.GetStudentAccessibleViewSetting(getWhoAmIPid);
			this.hidden_showGraphical.Value = ((studentAccessibleViewSetting == eClockWorkWebAccessibleView.GraphicalView) ? "1" : "0");
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_RemoveStaffTutorsFromCalendarWhoDontHaveAnyAvailability);
			this.hidden_removeStaffWithNoEvents.Value = (settingValue ? "1" : "");
			bool flag7 = !this.Page.IsPostBack;
			if (flag7)
			{
				string text = base.Request.QueryString["successfulBooking"];
				bool flag8 = text != null && text == "1";
				if (flag8)
				{
					this.p_message.Visible = true;
				}
				string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_starttime);
				string settingValue3 = webSettingsClientManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_endtime);
				TimeSpan? timespanFromMilitaryTimeString = this.GetTimespanFromMilitaryTimeString(settingValue2);
				TimeSpan? timespanFromMilitaryTimeString2 = this.GetTimespanFromMilitaryTimeString(settingValue3);
				this.hidden_visibleStartTime.Value = ((timespanFromMilitaryTimeString != null) ? settingValue2 : "5:30");
				this.hidden_visibleEndTime.Value = ((timespanFromMilitaryTimeString2 != null) ? settingValue3 : "22:30");
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00035DA8 File Offset: 0x00033FA8
		private TimeSpan? GetTimespanFromMilitaryTimeString(string s)
		{
			bool flag = string.IsNullOrWhiteSpace(s);
			TimeSpan? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string s2 = DateTime.Now.ToString("yyyy-MM-dd") + " " + s;
				DateTime dateTime;
				result = ((!DateTime.TryParse(s2, out dateTime)) ? null : new TimeSpan?(dateTime.TimeOfDay));
			}
			return result;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00035E14 File Offset: 0x00034014
		private int GetWhoAmIPid
		{
			get
			{
				return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			}
		}

		// Token: 0x04000549 RID: 1353
		protected Panel p_availabilityTypes;

		// Token: 0x0400054A RID: 1354
		protected HiddenField hidden_channelId;

		// Token: 0x0400054B RID: 1355
		protected HiddenField hidden_date;

		// Token: 0x0400054C RID: 1356
		protected HiddenField hidden_withWhom;

		// Token: 0x0400054D RID: 1357
		protected HiddenField hidden_showGraphical;

		// Token: 0x0400054E RID: 1358
		protected HiddenField hidden_removeStaffWithNoEvents;

		// Token: 0x0400054F RID: 1359
		protected HiddenField hidden_visibleStartTime;

		// Token: 0x04000550 RID: 1360
		protected HiddenField hidden_visibleEndTime;

		// Token: 0x04000551 RID: 1361
		protected Panel p_message;

		// Token: 0x04000552 RID: 1362
		protected HtmlGenericControl p_questionnaire;

		// Token: 0x04000553 RID: 1363
		protected Label lbl_NoChannelsMessage;

		// Token: 0x0200021E RID: 542
		public class AvailabilityItem
		{
			// Token: 0x1700032E RID: 814
			// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00050653 File Offset: 0x0004E853
			// (set) Token: 0x06000E41 RID: 3649 RVA: 0x0005065B File Offset: 0x0004E85B
			public string ChannelId { get; set; }

			// Token: 0x1700032F RID: 815
			// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00050664 File Offset: 0x0004E864
			// (set) Token: 0x06000E43 RID: 3651 RVA: 0x0005066C File Offset: 0x0004E86C
			public string CalendarTitle { get; set; }

			// Token: 0x17000330 RID: 816
			// (get) Token: 0x06000E44 RID: 3652 RVA: 0x00050675 File Offset: 0x0004E875
			// (set) Token: 0x06000E45 RID: 3653 RVA: 0x0005067D File Offset: 0x0004E87D
			public IList<int> PersonIds { get; set; }

			// Token: 0x17000331 RID: 817
			// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00050686 File Offset: 0x0004E886
			// (set) Token: 0x06000E47 RID: 3655 RVA: 0x0005068E File Offset: 0x0004E88E
			public DateTime StartDateTime { get; set; }

			// Token: 0x17000332 RID: 818
			// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00050697 File Offset: 0x0004E897
			// (set) Token: 0x06000E49 RID: 3657 RVA: 0x0005069F File Offset: 0x0004E89F
			public DateTime EndDateTime { get; set; }

			// Token: 0x17000333 RID: 819
			// (get) Token: 0x06000E4A RID: 3658 RVA: 0x000506A8 File Offset: 0x0004E8A8
			// (set) Token: 0x06000E4B RID: 3659 RVA: 0x000506B0 File Offset: 0x0004E8B0
			public int AvailabilityGroupId { get; set; }

			// Token: 0x17000334 RID: 820
			// (get) Token: 0x06000E4C RID: 3660 RVA: 0x000506B9 File Offset: 0x0004E8B9
			// (set) Token: 0x06000E4D RID: 3661 RVA: 0x000506C1 File Offset: 0x0004E8C1
			public string AvailabilityTitle { get; set; }
		}

		// Token: 0x0200021F RID: 543
		public class ChannelListItem
		{
			// Token: 0x17000335 RID: 821
			// (get) Token: 0x06000E4F RID: 3663 RVA: 0x000506CA File Offset: 0x0004E8CA
			// (set) Token: 0x06000E50 RID: 3664 RVA: 0x000506D2 File Offset: 0x0004E8D2
			public string Title { get; set; }

			// Token: 0x17000336 RID: 822
			// (get) Token: 0x06000E51 RID: 3665 RVA: 0x000506DB File Offset: 0x0004E8DB
			// (set) Token: 0x06000E52 RID: 3666 RVA: 0x000506E3 File Offset: 0x0004E8E3
			public string Id { get; set; }

			// Token: 0x17000337 RID: 823
			// (get) Token: 0x06000E53 RID: 3667 RVA: 0x000506EC File Offset: 0x0004E8EC
			// (set) Token: 0x06000E54 RID: 3668 RVA: 0x000506F4 File Offset: 0x0004E8F4
			public string[] CalendarTitles { get; set; }
		}
	}
}

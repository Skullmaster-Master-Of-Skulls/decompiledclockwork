using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkWeb.ctrls.Common;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.AppointmentBooking;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutor
{
	// Token: 0x0200012F RID: 303
	public class ctrls_Tutoring_Tutor_CtrlTutorListCalendar : UserControl
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x00041108 File Offset: 0x0003F308
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = false;
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				object obj = base.Session["calendar_lastdateviewed"];
				DateTime value = (obj == null) ? DateTime.Now.Date : ((DateTime)obj).Date;
				this.ctrlCalendarSingleDayNavigator1.SetSelectedDate(new DateTime?(value));
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.RefreshSchedule();
				this.RadGrid1.DataBind();
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00041194 File Offset: 0x0003F394
		private List<AppointmentOrAvailabilityWrapper> Appointments
		{
			get
			{
				List<AppointmentOrAvailabilityWrapper> list = base.Session["ClockWork.TutorAppointments"] as List<AppointmentOrAvailabilityWrapper>;
				bool flag = list == null;
				if (flag)
				{
					list = new List<AppointmentOrAvailabilityWrapper>();
					base.Session["ClockWork.TutorAppointments"] = list;
				}
				return list;
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600090A RID: 2314 RVA: 0x000411E0 File Offset: 0x0003F3E0
		// (remove) Token: 0x0600090B RID: 2315 RVA: 0x00041218 File Offset: 0x0003F418
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<UserEventArgs> OnLoggedInUserPidRequested;

		// Token: 0x0600090C RID: 2316 RVA: 0x00041250 File Offset: 0x0003F450
		private int GetUserPid()
		{
			EventHandler<UserEventArgs> onLoggedInUserPidRequested = this.OnLoggedInUserPidRequested;
			bool flag = onLoggedInUserPidRequested != null;
			int result;
			if (flag)
			{
				UserEventArgs userEventArgs = new UserEventArgs();
				onLoggedInUserPidRequested(this, userEventArgs);
				result = userEventArgs.PersonId;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0004128C File Offset: 0x0003F48C
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x000412A4 File Offset: 0x0003F4A4
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				this._isDisabled = value;
				bool flag = !this._isDisabled;
				if (flag)
				{
					this.RefreshSchedule();
				}
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000412D0 File Offset: 0x0003F4D0
		protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			bool isDisabled = this._isDisabled;
			if (!isDisabled)
			{
				this.RadGrid1.DataSource = this.Appointments;
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000412FC File Offset: 0x0003F4FC
		protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
		{
			bool flag = e.CommandName == "info";
			if (flag)
			{
				string s = (e.CommandArgument == null) ? "" : e.CommandArgument.ToString();
				int num;
				bool flag2 = int.TryParse(s, out num) && num > 0;
				if (flag2)
				{
					INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
					base.Response.Redirect("app.aspx?appid=" + navigatorClientManager.ConvertIntParameterToUrlString(num), true);
				}
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
		{
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00041378 File Offset: 0x0003F578
		protected void ctrlCalendarSingleDayNavigator1_DateChanged(object sender, DateArgs e)
		{
			this.RefreshSchedule();
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00041384 File Offset: 0x0003F584
		public void RefreshSchedule()
		{
			bool flag = this._isDisabled || this.ctrlCalendarSingleDayNavigator1.SelectedDate == null;
			if (!flag)
			{
				int userPid = this.GetUserPid();
				DateTime date = this.ctrlCalendarSingleDayNavigator1.SelectedDate.Value.Date;
				DateTime dateTime = date;
				DateTime endDateTime = date;
				base.Session.Add("calendar_lastdateviewed", dateTime);
				IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
				Dictionary<int, IList<int>> dictionary = new Dictionary<int, IList<int>>();
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
				dictionary.Add(userPid, new List<int>
				{
					userPid,
					tutorAvailabilityScheduleGroupId
				});
				AppointmentsWithAvailabilityAndTimetableDTO appointmentsWithAvailabilityAndTimetableDTO = appointmentClientManager.LoadAppointmentsAndAvailability(new AppointmentLoadOptionsDTO
				{
					PersonIds = new List<int>
					{
						userPid
					},
					DontLoadHolidays = true,
					StartDateTime = dateTime,
					EndDateTime = endDateTime,
					HideCancelledAppointments = true,
					AvailabilityGroupIdsByPersonId = dictionary,
					LoadRecurringSchedule = true
				});
				List<AppointmentOrAvailabilityWrapper> list = new List<AppointmentOrAvailabilityWrapper>();
				foreach (AppointmentDTO app in appointmentsWithAvailabilityAndTimetableDTO.Appointments)
				{
					list.Add(new AppointmentOrAvailabilityWrapper(app));
				}
				base.Session.Add("ClockWork.TutorAppointments", list);
				this.RadGrid1.DataSource = list;
				this.RadGrid1.DataBind();
			}
		}

		// Token: 0x040006FD RID: 1789
		protected ctrls_Common_CtrlCalendarSingleDayNavigator ctrlCalendarSingleDayNavigator1;

		// Token: 0x040006FE RID: 1790
		protected RadGrid RadGrid1;

		// Token: 0x040006FF RID: 1791
		protected Label lbl_ct;

		// Token: 0x04000700 RID: 1792
		private const string AppointmentsKey = "ClockWork.TutorAppointments";

		// Token: 0x04000702 RID: 1794
		private bool _isDisabled = false;
	}
}

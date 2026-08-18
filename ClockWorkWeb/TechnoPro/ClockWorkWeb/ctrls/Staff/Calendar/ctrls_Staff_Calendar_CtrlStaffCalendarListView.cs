using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.ctrls.Common;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.UI.Web.Entity.AppointmentBooking;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Staff.Calendar
{
	// Token: 0x02000140 RID: 320
	public class ctrls_Staff_Calendar_CtrlStaffCalendarListView : UserControl
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x000445C0 File Offset: 0x000427C0
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

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0004464C File Offset: 0x0004284C
		private List<AppointmentOrAvailabilityWrapper> Appointments
		{
			get
			{
				List<AppointmentOrAvailabilityWrapper> list = base.Session["ClockWork.StaffAppointments"] as List<AppointmentOrAvailabilityWrapper>;
				bool flag = list == null;
				if (flag)
				{
					list = new List<AppointmentOrAvailabilityWrapper>();
					base.Session["ClockWork.StaffAppointments"] = list;
				}
				return list;
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060009B6 RID: 2486 RVA: 0x00044698 File Offset: 0x00042898
		// (remove) Token: 0x060009B7 RID: 2487 RVA: 0x000446D0 File Offset: 0x000428D0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<UserEventArgs> OnLoggedInUserPidRequested;

		// Token: 0x060009B8 RID: 2488 RVA: 0x00044708 File Offset: 0x00042908
		private int GetStaffPid()
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

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00044744 File Offset: 0x00042944
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x0004475C File Offset: 0x0004295C
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

		// Token: 0x060009BB RID: 2491 RVA: 0x00044788 File Offset: 0x00042988
		protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			bool isDisabled = this._isDisabled;
			if (!isDisabled)
			{
				this.RadGrid1.DataSource = this.Appointments;
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000447B4 File Offset: 0x000429B4
		protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
		{
			bool flag = e.CommandName == "cancel";
			if (flag)
			{
				string s = (e.CommandArgument == null) ? "" : e.CommandArgument.ToString();
				int num;
				bool flag2 = int.TryParse(s, out num) && num > 0;
				if (flag2)
				{
					AppCancelInfoDTO cancelInfo = new AppCancelInfoDTO
					{
						CancelledBy = new PersonBaseDTO
						{
							PersonId = this.GetStaffPid()
						},
						CancelledDate = DateTime.Now,
						CancelReasonText = ""
					};
					IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
					appointmentClientManager.CancelAppointment(num, cancelInfo);
					this.RefreshSchedule();
				}
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
		{
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0004485E File Offset: 0x00042A5E
		protected void ctrlCalendarSingleDayNavigator1_DateChanged(object sender, DateArgs e)
		{
			this.RefreshSchedule();
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00044868 File Offset: 0x00042A68
		public void RefreshSchedule()
		{
			bool flag = this._isDisabled || this.ctrlCalendarSingleDayNavigator1.SelectedDate == null;
			if (!flag)
			{
				int staffPid = this.GetStaffPid();
				DateTime date = this.ctrlCalendarSingleDayNavigator1.SelectedDate.Value.Date;
				DateTime dateTime = date;
				DateTime endDateTime = date;
				base.Session.Add("calendar_lastdateviewed", dateTime);
				IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
				IList<AppointmentDTO> list = appointmentClientManager.LoadAppointments(new List<int>
				{
					staffPid
				}, null, true, false, false, dateTime, endDateTime);
				List<AppointmentOrAvailabilityWrapper> list2 = new List<AppointmentOrAvailabilityWrapper>();
				foreach (AppointmentDTO app in list)
				{
					AppointmentOrAvailabilityWrapper item = new AppointmentOrAvailabilityWrapper(app);
					list2.Add(item);
				}
				base.Session.Add("ClockWork.StaffAppointments", list2);
				this.RadGrid1.DataSource = list2;
				this.RadGrid1.DataBind();
			}
		}

		// Token: 0x040007A1 RID: 1953
		protected ctrls_Common_CtrlCalendarSingleDayNavigator ctrlCalendarSingleDayNavigator1;

		// Token: 0x040007A2 RID: 1954
		protected RadGrid RadGrid1;

		// Token: 0x040007A3 RID: 1955
		protected Label lbl_ct;

		// Token: 0x040007A4 RID: 1956
		private const string AppointmentsKey = "ClockWork.StaffAppointments";

		// Token: 0x040007A6 RID: 1958
		private bool _isDisabled = false;
	}
}

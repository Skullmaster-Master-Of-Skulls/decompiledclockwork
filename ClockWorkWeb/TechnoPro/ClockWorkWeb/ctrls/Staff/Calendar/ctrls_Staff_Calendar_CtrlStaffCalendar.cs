using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Staff.Calendar
{
	// Token: 0x0200013F RID: 319
	public class ctrls_Staff_Calendar_CtrlStaffCalendar : UserControl
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x000440D0 File Offset: 0x000422D0
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00044103 File Offset: 0x00042303
		private Appointment EditedAppointment
		{
			get
			{
				return (this.EditedAppointmentID != null) ? this.RadScheduler1.Appointments.FindByID(this.EditedAppointmentID) : null;
			}
			set
			{
				this.EditedAppointmentID = value.ID;
				this.EditedAppointmentParentID = value.RecurrenceParentID;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00044120 File Offset: 0x00042320
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00044142 File Offset: 0x00042342
		private object EditedAppointmentID
		{
			get
			{
				return this.ViewState["EditedAppointmentID"];
			}
			set
			{
				this.ViewState["EditedAppointmentID"] = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00044158 File Offset: 0x00042358
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x0004417A File Offset: 0x0004237A
		private object EditedAppointmentParentID
		{
			get
			{
				return this.ViewState["EditedAppointmentParentID"];
			}
			set
			{
				this.ViewState["EditedAppointmentParentID"] = value;
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00044190 File Offset: 0x00042390
		protected void RadScheduler1_FormCreating(object sender, SchedulerFormCreatingEventArgs e)
		{
			bool flag = e.Mode == SchedulerFormMode.Insert;
			if (flag)
			{
				Appointment appointment = this.RadScheduler1.PrepareToEdit(e.Appointment, this.RadScheduler1.EditingRecurringSeries);
				e.Cancel = true;
				ScriptManager.RegisterStartupScript(this.Page, base.GetType(), "formScript", "Sys.Application.add_load(openForm);", true);
				this.ctrlAppointmentEdit1.InitNew(e.Appointment.Start, e.Appointment.End);
			}
			else
			{
				bool flag2 = e.Mode == SchedulerFormMode.Edit;
				if (flag2)
				{
					this.EditedAppointment = e.Appointment;
					e.Cancel = true;
					ScriptManager.RegisterStartupScript(this.Page, base.GetType(), "formScript", "Sys.Application.add_load(openForm);", true);
					this.ctrlAppointmentEdit1.InitEdit(e.Appointment.ID);
				}
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0004426C File Offset: 0x0004246C
		protected void Page_Init(object sender, EventArgs e)
		{
			ctrls_Staff_Calendar_CtrlAppointmentEdit ctrls_Staff_Calendar_CtrlAppointmentEdit = this.ctrlAppointmentEdit1;
			ctrls_Staff_Calendar_CtrlAppointmentEdit.OnSaveCompleted = (EventHandler)Delegate.Combine(ctrls_Staff_Calendar_CtrlAppointmentEdit.OnSaveCompleted, new EventHandler(this.OnSaveCompleted));
			this.ctrlAppointmentEdit1.OnLoggedInUserPidRequested += delegate(object o, UserEventArgs args)
			{
				args.PersonId = this.LookupStaffPid();
			};
			object obj = base.Session["staffCalendarStart"];
			DateTime? dateTime = null;
			bool flag = obj != null;
			if (flag)
			{
				dateTime = new DateTime?((DateTime)obj);
			}
			bool flag2 = dateTime != null;
			if (flag2)
			{
				this.RadScheduler1.SelectedDate = dateTime.Value;
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00044308 File Offset: 0x00042508
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
			this.RefreshCalendar();
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00044331 File Offset: 0x00042531
		private void FireOnRefreshCalendarRequested()
		{
			EventHandler onRefreshCalendarRequested = this.OnRefreshCalendarRequested;
			if (onRefreshCalendarRequested != null)
			{
				onRefreshCalendarRequested(this, new EventArgs());
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0004434C File Offset: 0x0004254C
		private void OnSaveCompleted(object sender, EventArgs eventArgs)
		{
			this.FireOnRefreshCalendarRequested();
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00044358 File Offset: 0x00042558
		public void RefreshCalendar()
		{
			int item = this.LookupStaffPid();
			DateTime visibleRangeStart = this.RadScheduler1.VisibleRangeStart;
			DateTime visibleRangeEnd = this.RadScheduler1.VisibleRangeEnd;
			base.Session.Add("staffCalendarStart", visibleRangeStart);
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			IList<AppointmentDTO> source = appointmentClientManager.LoadAppointments(new List<int>
			{
				item
			}, null, true, false, false, visibleRangeStart, visibleRangeEnd);
			List<AppointmentWrapper> dataSource = source.ToList<AppointmentDTO>().ConvertAll<AppointmentWrapper>((AppointmentDTO g) => new AppointmentWrapper(g));
			this.RadScheduler1.DataSource = dataSource;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x000443F8 File Offset: 0x000425F8
		protected void RadScheduler1_AppointmentDelete(object sender, SchedulerCancelEventArgs e)
		{
			bool flag = e.Appointment.Attributes["ReadOnly"] == "true";
			if (flag)
			{
				e.Cancel = true;
			}
			else
			{
				string s = e.Appointment.ID.ToString();
				int num;
				int.TryParse(s, out num);
				bool flag2 = num < 1;
				if (!flag2)
				{
					ITutorClientManager tutorClientManager = new TutorWebClientManager();
					TutorAppointmentDTO tutorAppointmentDTO = tutorClientManager.LoadTutorAppointment(num);
					AppCancelInfoDTO cancelInfo = new AppCancelInfoDTO
					{
						CancelledBy = new PersonBaseDTO
						{
							PersonId = this.LookupStaffPid()
						},
						CancelledDate = DateTime.Now,
						CancelReason = null,
						CancelReasonText = ""
					};
					IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
					appointmentClientManager.CancelAppointment(num, cancelInfo);
					this.RefreshCalendar();
				}
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060009AD RID: 2477 RVA: 0x000444C8 File Offset: 0x000426C8
		// (remove) Token: 0x060009AE RID: 2478 RVA: 0x00044500 File Offset: 0x00042700
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<UserEventArgs> OnLoggedInUserPidRequested;

		// Token: 0x060009AF RID: 2479 RVA: 0x00044538 File Offset: 0x00042738
		private int LookupStaffPid()
		{
			EventHandler<UserEventArgs> onLoggedInUserPidRequested = this.OnLoggedInUserPidRequested;
			bool flag = onLoggedInUserPidRequested == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				UserEventArgs userEventArgs = new UserEventArgs();
				onLoggedInUserPidRequested(this, userEventArgs);
				result = userEventArgs.PersonId;
			}
			return result;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00044572 File Offset: 0x00042772
		protected void RadScheduler1_NavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
		{
			this.RefreshCalendar();
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0004457C File Offset: 0x0004277C
		protected void RadScheduler1_AppointmentClick(object sender, SchedulerEventArgs e)
		{
			string s = e.Appointment.ID.ToString();
			int num;
			int.TryParse(s, out num);
			bool flag = num < 1;
			if (flag)
			{
			}
		}

		// Token: 0x04000795 RID: 1941
		protected RadScriptBlock RadScriptBlock1;

		// Token: 0x04000796 RID: 1942
		protected RadAjaxLoadingPanel RadAjaxLoadingPanel1;

		// Token: 0x04000797 RID: 1943
		protected RadAjaxManager RadAjaxManager1;

		// Token: 0x04000798 RID: 1944
		protected Panel DockPanel;

		// Token: 0x04000799 RID: 1945
		protected RadDock RadDock1;

		// Token: 0x0400079A RID: 1946
		protected Panel PanelDock;

		// Token: 0x0400079B RID: 1947
		protected Label StatusLabel;

		// Token: 0x0400079C RID: 1948
		protected ctrls_Staff_Calendar_CtrlAppointmentEdit ctrlAppointmentEdit1;

		// Token: 0x0400079D RID: 1949
		protected RadScheduler RadScheduler1;

		// Token: 0x0400079E RID: 1950
		private const string calendarSelectedDateKey = "staffCalendarStart";

		// Token: 0x0400079F RID: 1951
		public EventHandler OnRefreshCalendarRequested;
	}
}

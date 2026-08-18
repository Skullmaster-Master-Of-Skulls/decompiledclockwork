using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Core.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AvailabilitySchedule;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.AppointmentBooking;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutor
{
	// Token: 0x0200012E RID: 302
	public class ctrls_Tutoring_Tutor_CtrlTutorCalendar : UserControl
	{
		// Token: 0x060008F8 RID: 2296 RVA: 0x00040908 File Offset: 0x0003EB08
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				base.Session.Remove("ClockWork.TutorAppointments");
				object obj = base.Session["calendar_lastdateviewed"];
				DateTime selectedDate = (obj == null) ? DateTime.Now.Date : ((DateTime)obj).Date;
				this.RadScheduler1.SelectedDate = selectedDate;
				this.InitializeResources();
				this.RefreshSchedule();
			}
			this.RadScheduler1.DataSource = this.Appointments;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0004099C File Offset: 0x0003EB9C
		protected void RadScheduler1_Click(object sender, SchedulerEventArgs e)
		{
			bool flag = e.Appointment == null || e.Appointment.ID == null || !(e.Appointment.ID is string);
			if (!flag)
			{
				string s = (string)e.Appointment.ID;
				int num;
				bool flag2 = int.TryParse(s, out num) && num > 0;
				if (flag2)
				{
					INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
					base.Response.Redirect("app.aspx?appid=" + navigatorClientManager.ConvertIntParameterToUrlString(num), true);
				}
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00040A2C File Offset: 0x0003EC2C
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

		// Token: 0x060008FB RID: 2299 RVA: 0x00040A78 File Offset: 0x0003EC78
		protected void RadScheduler1_AppointmentInsert(object sender, SchedulerCancelEventArgs e)
		{
			AppointmentOrAvailabilityWrapper item = new AppointmentOrAvailabilityWrapper();
			this.CopyInfo(ref item, e.Appointment);
			this.Appointments.Add(item);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00040AA8 File Offset: 0x0003ECA8
		private void CopyInfo(ref AppointmentOrAvailabilityWrapper aiDest, Appointment source)
		{
			aiDest.Subject = source.Subject;
			aiDest.Start = source.Start;
			aiDest.End = source.End;
			aiDest.RecurrenceRule = source.RecurrenceRule;
			bool flag = source.RecurrenceParentID != null;
			if (flag)
			{
				aiDest.RecurrenceParentID = source.RecurrenceParentID.ToString();
			}
			aiDest.Reminder = ((source.Reminders != null && source.Reminders.Count > 0) ? source.Reminders[0].ToString() : null);
			Resource resourceByType = source.Resources.GetResourceByType("User");
			aiDest.UserID = ((resourceByType != null) ? ((int?)resourceByType.Key) : null);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00040B78 File Offset: 0x0003ED78
		protected void RadScheduler1_AppointmentUpdate(object sender, AppointmentUpdateEventArgs e)
		{
			bool flag = e.ModifiedAppointment == null;
			if (!flag)
			{
				AppointmentOrAvailabilityWrapper appointmentOrAvailabilityWrapper = this.FindById(e.ModifiedAppointment.ID);
				bool flag2 = appointmentOrAvailabilityWrapper == null;
				if (!flag2)
				{
					this.CopyInfo(ref appointmentOrAvailabilityWrapper, e.ModifiedAppointment);
					ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
					int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
					AvailabilityScheduleContextDTO availabilityScheduleContextDTO = new AvailabilityScheduleContextDTO();
					availabilityScheduleContextDTO.AvailabilityGroupId = tutorAvailabilityScheduleGroupId;
					availabilityScheduleContextDTO.PersonId = this.GetUserPid();
					IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
					base.Response.Redirect("TutorCalendar.aspx", true);
				}
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00040C08 File Offset: 0x0003EE08
		protected void RadScheduler1_AppointmentDelete(object sender, SchedulerCancelEventArgs e)
		{
			this.Appointments.Remove(this.FindById(e.Appointment.ID));
			ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
			int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
			AvailabilityScheduleContextDTO availabilityScheduleContextDTO = new AvailabilityScheduleContextDTO();
			availabilityScheduleContextDTO.AvailabilityGroupId = tutorAvailabilityScheduleGroupId;
			availabilityScheduleContextDTO.PersonId = this.GetUserPid();
			base.Response.Redirect("TutorCalendar.aspx", true);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00040C70 File Offset: 0x0003EE70
		private void InitializeResources()
		{
			ResourceType resourceType = new ResourceType("User");
			resourceType.ForeignKeyField = "UserID";
			this.RadScheduler1.ResourceTypes.Add(resourceType);
			this.RadScheduler1.Resources.Add(new Resource("User", 0, "Alex"));
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000900 RID: 2304 RVA: 0x00040CD0 File Offset: 0x0003EED0
		// (remove) Token: 0x06000901 RID: 2305 RVA: 0x00040D08 File Offset: 0x0003EF08
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<UserEventArgs> OnLoggedInUserPidRequested;

		// Token: 0x06000902 RID: 2306 RVA: 0x00040D40 File Offset: 0x0003EF40
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

		// Token: 0x06000903 RID: 2307 RVA: 0x00040D7C File Offset: 0x0003EF7C
		private void RefreshSchedule()
		{
			int userPid = this.GetUserPid();
			DateTime visibleRangeStart = this.RadScheduler1.VisibleRangeStart;
			DateTime visibleRangeEnd = this.RadScheduler1.VisibleRangeEnd;
			base.Session.Add("calendar_lastdateviewed", visibleRangeStart);
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
				StartDateTime = visibleRangeStart,
				EndDateTime = visibleRangeEnd,
				HideCancelledAppointments = true,
				AvailabilityGroupIdsByPersonId = dictionary,
				LoadRecurringSchedule = true
			});
			List<AppointmentOrAvailabilityWrapper> list = new List<AppointmentOrAvailabilityWrapper>();
			foreach (AppointmentDTO appointmentDTO in appointmentsWithAvailabilityAndTimetableDTO.Appointments)
			{
				AppointmentOrAvailabilityWrapper item = new AppointmentOrAvailabilityWrapper(appointmentDTO);
				int appointmentId = appointmentDTO.AppointmentId;
				string titleAndSubtitle = appointmentDTO.GetTitleAndSubtitle();
				DateTime startDateTime = appointmentDTO.StartDateTime;
				DateTime endDateTime = appointmentDTO.EndDateTime;
				list.Add(item);
			}
			foreach (AvailabilityScheduleItemsForContextDTO availabilityScheduleItemsForContextDTO in appointmentsWithAvailabilityAndTimetableDTO.AvailabilitySchedules)
			{
				int personId = availabilityScheduleItemsForContextDTO.Context.PersonId;
				IList<AvailabilityScheduleItemInfoDTO> availabilityScheduleItems = availabilityScheduleItemsForContextDTO.AvailabilityScheduleItems;
				foreach (AvailabilityScheduleItemInfoDTO availabilityScheduleItemInfoDTO in availabilityScheduleItems)
				{
					DateTime sdt = availabilityScheduleItemInfoDTO.DayAndTime.Date.Date.Add(availabilityScheduleItemInfoDTO.DayAndTime.Time.StartTime);
					DateTime edt = availabilityScheduleItemInfoDTO.DayAndTime.Date.Date.Add(availabilityScheduleItemInfoDTO.DayAndTime.Time.EndTime);
					AppointmentDTO appointmentDTO2 = appointmentsWithAvailabilityAndTimetableDTO.Appointments.FirstOrDefault((AppointmentDTO g) => !(edt <= g.StartDateTime) && !(sdt >= g.EndDateTime));
					bool flag = appointmentDTO2 == null;
					if (flag)
					{
						list.Add(new AppointmentOrAvailabilityWrapper(availabilityScheduleItemInfoDTO));
					}
				}
			}
			base.Session["ClockWork.TutorAppointments"] = list;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00041050 File Offset: 0x0003F250
		protected void RadScheduler1_AppointmentDataBound(object sender, SchedulerEventArgs e)
		{
			e.Appointment.CssClass = "Appointment";
			AppointmentOrAvailabilityWrapper appointmentOrAvailabilityWrapper = (AppointmentOrAvailabilityWrapper)e.Appointment.DataItem;
			bool flag = appointmentOrAvailabilityWrapper != null;
			if (flag)
			{
				Color backColor = (appointmentOrAvailabilityWrapper.Colour == 0) ? Color.AliceBlue : Color.FromArgb(appointmentOrAvailabilityWrapper.Colour);
				e.Appointment.BackColor = backColor;
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000410B2 File Offset: 0x0003F2B2
		protected void RadScheduler1_NavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
		{
			this.RefreshSchedule();
			this.RadScheduler1.DataSource = this.Appointments;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000410D0 File Offset: 0x0003F2D0
		private AppointmentOrAvailabilityWrapper FindById(object ID)
		{
			return this.Appointments.FirstOrDefault((AppointmentOrAvailabilityWrapper ai) => ai.ID.Equals(ID));
		}

		// Token: 0x040006F9 RID: 1785
		protected RadScriptBlock RadScriptBlock1;

		// Token: 0x040006FA RID: 1786
		protected RadScheduler RadScheduler1;

		// Token: 0x040006FB RID: 1787
		private const string AppointmentsKey = "ClockWork.TutorAppointments";
	}
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.TextFormat.Adapters;
using Telerik.Web.UI;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsCalendar
{
	// Token: 0x02000024 RID: 36
	public class ClockWorkAppointmentProvider : SchedulerProviderBase
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x0000921C File Offset: 0x0000741C
		public ClockWorkAppointmentProvider(int whoseCalendarPid)
		{
			this.WhoseCalendarPid = whoseCalendarPid;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000922D File Offset: 0x0000742D
		public ClockWorkAppointmentProvider()
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00009238 File Offset: 0x00007438
		public override void Initialize(string name, NameValueCollection config)
		{
			bool flag = config == null;
			if (flag)
			{
				throw new ArgumentNullException("config");
			}
			bool flag2 = string.IsNullOrEmpty(name);
			if (flag2)
			{
				name = "ClockWorkSchedulerProvider";
			}
			base.Initialize(name, config);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00009274 File Offset: 0x00007474
		public override IEnumerable<ResourceType> GetResourceTypes(RadScheduler owner)
		{
			return new List<ResourceType>();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000928C File Offset: 0x0000748C
		public override IDictionary<ResourceType, IEnumerable<Resource>> GetResources(ISchedulerInfo schedulerInfo)
		{
			return new Dictionary<ResourceType, IEnumerable<Resource>>();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000092A4 File Offset: 0x000074A4
		public override IEnumerable<Resource> GetResourcesByType(RadScheduler owner, string resourceType)
		{
			return new List<Resource>();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000092BC File Offset: 0x000074BC
		public override IEnumerable<Telerik.Web.UI.Appointment> GetAppointments(RadScheduler owner)
		{
			DateTime visibleRangeStart = owner.VisibleRangeStart;
			DateTime visibleRangeEnd = owner.VisibleRangeEnd;
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			IDictionary<int, IList<eAppointmentPermissionRestriction>> restrictions;
			this._apps = appointmentClientManager.LoadAppointmentsWithSpecialPermissions(new List<int>
			{
				this.WhoseCalendarPid
			}, null, !this.ShowCancelledAppointments, visibleRangeStart, Convert.ToInt32((visibleRangeEnd.Date - visibleRangeStart.Date).TotalDays) + 1, out restrictions);
			return (from g in this._apps
			select ClockWorkAppointmentProvider.ConvertToTelerikAppointment(g, restrictions.ContainsKey(g.AppointmentId) ? restrictions[g.AppointmentId] : null)).ToList<Telerik.Web.UI.Appointment>();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000935C File Offset: 0x0000755C
		public static Telerik.Web.UI.Appointment ConvertToTelerikAppointment(AppointmentDTO cwApp, IList<eAppointmentPermissionRestriction> restrictions)
		{
			Telerik.Web.UI.Appointment appointment = new Telerik.Web.UI.Appointment(cwApp.AppointmentId, cwApp.StartDateTime, cwApp.EndDateTime, cwApp.SubTitle)
			{
				Description = (cwApp.Memo ?? "")
			};
			bool isAllDay = cwApp.IsAllDay;
			if (isAllDay)
			{
				appointment.Start = new DateTime(appointment.Start.Year, appointment.Start.Month, appointment.Start.Day, 0, 0, 0);
				appointment.End = new DateTime(appointment.Start.Year, appointment.Start.Month, appointment.Start.Day, 0, 0, 0).AddDays(1.0);
			}
			appointment.SetAppType(cwApp.AppType);
			appointment.SetMemoPlainTextByRtf(cwApp.Memo);
			appointment.SetAttendees(cwApp.Attendees);
			appointment.SetIsPrivate(cwApp.IsPrivate);
			appointment.SetIsCancelled(cwApp.IsCancelled);
			appointment.SetRoomAndLocation(cwApp.Room, cwApp.Location);
			appointment.SetAppCssClass(cwApp);
			appointment.SetRestrictions(restrictions);
			Telerik.Web.UI.Appointment app = appointment;
			PersonBaseDTO whoBooked = cwApp.WhoBooked;
			app.SetWhobookedPid((whoBooked != null) ? whoBooked.PersonId : 0);
			return appointment;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000094B7 File Offset: 0x000076B7
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000094BF File Offset: 0x000076BF
		public bool ShowCancelledAppointments { get; set; }

		// Token: 0x060000DF RID: 223 RVA: 0x000094C8 File Offset: 0x000076C8
		public static void UpdateClockWorkAppFromTelerikAppointment(AppointmentDTO cwApp, Telerik.Web.UI.Appointment app)
		{
			cwApp.StartDateTime = app.Start;
			cwApp.EndDateTime = app.End;
			DateTime startDateTime = cwApp.StartDateTime;
			bool flag = cwApp.StartDateTime.Hour == 0 && cwApp.StartDateTime.Minute == 0 && cwApp.EndDateTime.Hour == 0 && cwApp.EndDateTime.Minute == 0;
			if (flag)
			{
				cwApp.StartDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, 0, 1, 0);
				cwApp.EndDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, 23, 59, 0).AddDays(1.0);
			}
			bool flag2 = cwApp.StartDateTime.Date != cwApp.EndDateTime.Date;
			if (flag2)
			{
				cwApp.EndDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, cwApp.EndDateTime.Hour, cwApp.EndDateTime.Minute, 0);
			}
			cwApp.AppType = app.GetAppType();
			cwApp.SubTitle = app.GetSubTitle();
			string memoPlainText = app.GetMemoPlainText();
			bool flag3 = memoPlainText != null;
			if (flag3)
			{
				cwApp.Memo = memoPlainText.ConvertPlainTextToRtf();
			}
			cwApp.IsCancelled = app.GetIsCancelled();
			cwApp.IsPrivate = app.GetIsPrivate();
			int whoBookedPid = app.GetWhoBookedPid();
			object whoBooked;
			if (whoBookedPid <= 0)
			{
				whoBooked = null;
			}
			else
			{
				(whoBooked = new PersonBaseDTO()).PersonId = whoBookedPid;
			}
			cwApp.WhoBooked = whoBooked;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00009680 File Offset: 0x00007880
		public static AppointmentDTO CreateClockWorkAppFromTelerikApp(Telerik.Web.UI.Appointment app)
		{
			AppointmentDTO appointmentDTO = new AppointmentDTO
			{
				AppointmentId = (app.ID as int?).GetValueOrDefault(),
				Attendees = new List<AttendeeDTO>()
			};
			ClockWorkAppointmentProvider.UpdateClockWorkAppFromTelerikAppointment(appointmentDTO, app);
			return appointmentDTO;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000096D0 File Offset: 0x000078D0
		public override void Insert(RadScheduler owner, Telerik.Web.UI.Appointment appointmentToInsert)
		{
			AppointmentDTO appointmentDTO = ClockWorkAppointmentProvider.CreateClockWorkAppFromTelerikApp(appointmentToInsert);
			appointmentDTO.Attendees.Add(new AttendeeDTO
			{
				Person = new PersonBaseDTO
				{
					PersonId = this.WhoseCalendarPid
				}
			});
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			int appointmentId = appointmentClientManager.CreateAppointment(appointmentDTO);
			this._apps.Add(appointmentClientManager.LoadAppointment(appointmentId));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00009730 File Offset: 0x00007930
		public override void Update(RadScheduler owner, Telerik.Web.UI.Appointment appointmentToUpdate)
		{
			int appId = (appointmentToUpdate.ID as int?).GetValueOrDefault();
			bool flag = appId < 1;
			if (!flag)
			{
				IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
				AppointmentDTO appointmentDTO = appointmentClientManager.LoadAppointment(appId);
				bool flag2 = appointmentDTO == null;
				if (!flag2)
				{
					ClockWorkAppointmentProvider.UpdateClockWorkAppFromTelerikAppointment(appointmentDTO, appointmentToUpdate);
					appointmentClientManager.UpdateAppointment(appointmentDTO);
					AppointmentDTO appointmentDTO2 = this._apps.FirstOrDefault((AppointmentDTO g) => g.AppointmentId == appId);
					bool flag3 = appointmentDTO2 != null;
					if (flag3)
					{
						this._apps.Remove(appointmentDTO2);
					}
					this._apps.Add(appointmentDTO);
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000097E0 File Offset: 0x000079E0
		public override void Delete(RadScheduler owner, Telerik.Web.UI.Appointment appointmentToDelete)
		{
			IList<eAppointmentPermissionRestriction> restrictions = appointmentToDelete.GetRestrictions();
			bool flag;
			if (restrictions == null)
			{
				flag = false;
			}
			else
			{
				IList<eAppointmentPermissionRestriction> restrictions2 = restrictions;
				eAppointmentPermissionRestrictionResult[] array = new eAppointmentPermissionRestrictionResult[2];
				array[0] = eAppointmentPermissionRestrictionResult.NotAllowedToDelete;
				flag = restrictions2.HasRestriction(array);
			}
			bool flag2 = flag;
			if (!flag2)
			{
				int appId = (appointmentToDelete.ID as int?).GetValueOrDefault();
				bool flag3 = appId < 1;
				if (!flag3)
				{
					AppointmentDTO appointmentDTO = this._apps.FirstOrDefault((AppointmentDTO g) => g.AppointmentId == appId);
					bool flag4 = appointmentDTO == null;
					if (!flag4)
					{
						this._apps.Remove(appointmentDTO);
						IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
						appointmentClientManager.DeleteAppointment(appId);
					}
				}
			}
		}

		// Token: 0x0400001E RID: 30
		private IList<AppointmentDTO> _apps;

		// Token: 0x0400001F RID: 31
		private int WhoseCalendarPid;
	}
}

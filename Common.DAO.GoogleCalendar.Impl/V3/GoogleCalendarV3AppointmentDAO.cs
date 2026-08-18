using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.GoogleCalendar.Impl.Adapters;
using TechnoPro.Common.DAO.GoogleCalendar.Impl.Mappers;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentSync.FastSync;
using TechnoPro.Common.Public.Exceptions;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.V3
{
	// Token: 0x02000003 RID: 3
	public class GoogleCalendarV3AppointmentDAO : IExternalAppointmentDAO, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000023E4 File Offset: 0x000005E4
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000023FC File Offset: 0x000005FC
		public SyncOperationContext OpContext
		{
			get
			{
				return this._opContext;
			}
			set
			{
				this._opContext = value;
				this.CalendarService = this._opContext.CreateGoogleCalendarService();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002418 File Offset: 0x00000618
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002420 File Offset: 0x00000620
		public int PagingSize { get; set; }

		// Token: 0x0600000E RID: 14 RVA: 0x00002429 File Offset: 0x00000629
		public void UpdateClockWorkAppId(string uniqueId, int cwappid)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000242C File Offset: 0x0000062C
		public ExternalAppointment LoadOcurrenceOfRecurringSerieByAnyOcurrenceId(string uniqueId, int ocurrenceIndex)
		{
			return null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002440 File Offset: 0x00000640
		public string ResetSyncState(string username)
		{
			return null;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002454 File Offset: 0x00000654
		ExternalSyncAppointmentChangesResponse IExternalAppointmentDAO.LoadAppointmentChanges(ExternalSyncAppointmentChangesRequest request)
		{
			return null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002468 File Offset: 0x00000668
		public string LoadNativeAppointmentInfo(string appId)
		{
			Event @event = this.GetEvent(appId);
			return @event.ToDisplayString();
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002488 File Offset: 0x00000688
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002490 File Offset: 0x00000690
		protected CalendarService CalendarService { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002499 File Offset: 0x00000699
		protected bool ValidUserCalendar
		{
			get
			{
				SyncOperationContext opContext = this.OpContext;
				return !string.IsNullOrEmpty((opContext != null) ? opContext.CalendarUsername : null);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024B5 File Offset: 0x000006B5
		public GoogleCalendarV3AppointmentDAO(SyncOperationContext operationContext)
		{
			this.PagingSize = 25;
			this.OpContext = operationContext;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024D0 File Offset: 0x000006D0
		public IList<ExternalAppointment> LoadAppointments(ExternalAttendee user, DateTime startdate, DateTime endDate)
		{
			return this.LoadAppointmentsByPage(user, startdate, endDate, this.PagingSize);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024F4 File Offset: 0x000006F4
		public IList<ExternalAppointment> LoadModifiedAppointments(ExternalAttendee user, DateTime startdate, DateTime thresholdTime, bool sortedByDate = true)
		{
			return this.LoadAppointmentsByPage(user, startdate, thresholdTime, this.PagingSize);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002518 File Offset: 0x00000718
		public IList<ExternalAppointment> LoadOccurrenceAppointmentsOfRecurrenceSerie(string masterAppUid, DateTime? startDatetime = null, int count = 100, bool loadMapping = false)
		{
			return new List<ExternalAppointment>();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002530 File Offset: 0x00000730
		public IList<ExternalAppointment> LoadAppointments(ExternalAttendee user, DateTime startdate, DateTime endDate, bool sortedByDate)
		{
			IList<ExternalAppointment> collection = this.LoadAppointments(user, startdate, endDate);
			List<ExternalAppointment> list = new List<ExternalAppointment>(collection);
			list.Sort((ExternalAppointment a1, ExternalAppointment a2) => a1.StartDate.CompareTo(a2.StartDate));
			return list;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000257C File Offset: 0x0000077C
		public ExternalAppointment LoadAppointment(string eId)
		{
			bool flag = !this.ValidUserCalendar;
			if (flag)
			{
				throw new InvalidUserCalendarException("Calendar username was not specified");
			}
			ExternalAppointment result;
			try
			{
				Event @event = this.GetEvent(eId);
				result = ((@event != null) ? @event.ToDomainObject() : null);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("GoogleCalendarAppointmentDAO::LoadAppointment:: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025F0 File Offset: 0x000007F0
		public IList<ExternalAppointment> LoadAppointments(IList<string> appUidList)
		{
			return null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002604 File Offset: 0x00000804
		public ExternalAppointment LoadOccurrenceOfRecurringSerieByMasterId(string masterAppUid, int occurenceIndex)
		{
			return null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002618 File Offset: 0x00000818
		public ExternalAppointment LoadAppointmentByClockWorkAppointmentId(int cwappid)
		{
			return null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000262C File Offset: 0x0000082C
		public ExternalAppointment CreateAppointment(ExternalAppointment appointment)
		{
			string calendarUsername = this.OpContext.CalendarUsername;
			this.OpContext.CalendarUsername = appointment.FirstClockWorkSyncAttendee(this.OpContext.SyncSettings);
			bool flag = !this.ValidUserCalendar;
			if (flag)
			{
				throw new InvalidUserCalendarException("Calendar username was not specified");
			}
			ExternalAppointment result;
			try
			{
				Event @event = appointment.ToEventEntryDAO();
				@event = this.CalendarService.Events.Insert(@event, this.OpContext.CalendarUsername).Execute();
				ExternalAppointment externalAppointment = @event.ToDomainObject();
				externalAppointment.UniqueId = (appointment.UniqueId = @event.Id);
				externalAppointment.LegacyGlobalAppointmentId = (appointment.LegacyGlobalAppointmentId = @event.ICalUID);
				result = externalAppointment;
			}
			finally
			{
				this.OpContext.CalendarUsername = calendarUsername;
			}
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002708 File Offset: 0x00000908
		public void UpdateAppointment(ExternalAppointment appointment)
		{
			string calendarUsername = this.OpContext.CalendarUsername;
			this.OpContext.CalendarUsername = appointment.FirstClockWorkSyncAttendee(this.OpContext.SyncSettings);
			bool flag = !this.ValidUserCalendar;
			if (flag)
			{
				throw new InvalidUserCalendarException("Calendar username was not specified");
			}
			try
			{
				Event @event = this.GetEvent(appointment.UniqueId);
				@event = appointment.ToEventEntryDAO(@event);
				this.CalendarService.Events.Update(@event, this.OpContext.CalendarUsername, @event.Id).Execute();
			}
			finally
			{
				this.OpContext.CalendarUsername = calendarUsername;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027BC File Offset: 0x000009BC
		public void DeleteAppointment(ExternalAppointment exApp)
		{
			string calendarUsername = this.OpContext.CalendarUsername;
			this.OpContext.CalendarUsername = exApp.FirstClockWorkSyncAttendee(this.OpContext.SyncSettings);
			bool flag = !this.ValidUserCalendar;
			if (flag)
			{
				throw new InvalidUserCalendarException("Calendar username was not specified");
			}
			try
			{
				Event @event = this.GetEvent(exApp.UniqueId);
				try
				{
					this.CalendarService.Events.Delete(this.OpContext.CalendarUsername, @event.Id).Execute();
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("GoogleCalendarV3AppointmentDAO::DeleteAppointment:: Trying to delete appointment failed, appId='{0}', CalendarUsername='{1}', Error={2}", exApp.UniqueId ?? "NULL", this.OpContext.CalendarUsername ?? "NULL", ex.ToString()), ex);
					try
					{
						IEnumerable<EventAttendee> enumerable = from p in @event.Attendees
						where this.OpContext.SyncSettings.SyncUsers.Any((ClockWorkExternalApplicationSyncUser u) => u.ExternalApplicationUsername.Equals(p.Email, StringComparison.OrdinalIgnoreCase))
						select p;
						foreach (EventAttendee item in enumerable)
						{
							@event.Attendees.Remove(item);
						}
						this.CalendarService.Events.Update(@event, this.OpContext.CalendarUsername, @event.Id).Execute();
					}
					catch (Exception ex2)
					{
						CWLogger.Logger.ErrorException(string.Format("GoogleCalendarV3AppointmentDAO::DeleteAppointment:: Trying to remove clockwork attendees from appointment failed, appId='{0}', CalendarUsername='{1}', Error={2}", exApp.UniqueId ?? "NULL", this.OpContext.CalendarUsername ?? "NULL", ex2.ToString()), ex2);
					}
				}
			}
			finally
			{
				this.OpContext.CalendarUsername = calendarUsername;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000029CC File Offset: 0x00000BCC
		private Event GetEvent(string eId)
		{
			Event result;
			try
			{
				result = this.CalendarService.Events.Get(this.OpContext.CalendarUsername, eId).Execute();
			}
			catch (Exception exception)
			{
				CWLogger.Logger.DebugException(string.Format("GoogleCalendarAppointmentDAO::GetEvent:: Google event '{0}' organizer changed", eId), exception);
				result = this.GetEventAnyCalendar(eId);
			}
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A34 File Offset: 0x00000C34
		private Event GetEventAnyCalendar(string eId)
		{
			string calendarUsername = this.OpContext.CalendarUsername;
			try
			{
				foreach (ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser in this.OpContext.SyncSettings.SyncUsers)
				{
					try
					{
						this.OpContext.CalendarUsername = clockWorkExternalApplicationSyncUser.ExternalApplicationUsername;
						Event @event = this.CalendarService.Events.Get(this.OpContext.CalendarUsername, eId).Execute();
						bool flag = @event != null;
						if (flag)
						{
							CWLogger.Logger.Trace("GoogleCalendarAppointmentDAO::GetEventAnyCalendar:: Event found in '{0}' calendar", clockWorkExternalApplicationSyncUser.ExternalApplicationUsername);
							return @event;
						}
					}
					catch
					{
					}
				}
			}
			finally
			{
				this.OpContext.CalendarUsername = calendarUsername;
			}
			return null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002B30 File Offset: 0x00000D30
		protected IList<ExternalAppointment> LoadAppointmentsByPage(ExternalAttendee user, DateTime startdate, DateTime endDate, int pageSize)
		{
			List<Event> list = new List<Event>();
			string calendarUsername = this.OpContext.CalendarUsername;
			IList<ExternalAppointment> result;
			try
			{
				this.OpContext.CalendarUsername = user.Username;
				EventsResource.ListRequest listRequest = this.CalendarService.Events.List(this.OpContext.CalendarUsername);
				listRequest.MaxResults = new int?(pageSize);
				listRequest.TimeMin = new DateTime?(startdate);
				listRequest.TimeMax = new DateTime?(endDate);
				listRequest.SingleEvents = new bool?(true);
				string text = null;
				do
				{
					listRequest.PageToken = text;
					Events events = listRequest.Execute();
					bool flag = events.Items != null && events.Items.Count > 0;
					if (flag)
					{
						list.AddRange(events.Items);
					}
					text = events.NextPageToken;
				}
				while (text != null);
				result = list.ToDomainObject();
			}
			finally
			{
				this.OpContext.CalendarUsername = calendarUsername;
			}
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002C3C File Offset: 0x00000E3C
		protected IList<ExternalAppointment> LoadModifiedAppointmentsByPage(ExternalAttendee user, DateTime startdate, DateTime thresholdTime, int pageSize)
		{
			List<Event> list = new List<Event>();
			string calendarUsername = this.OpContext.CalendarUsername;
			IList<ExternalAppointment> result;
			try
			{
				this.OpContext.CalendarUsername = user.Username;
				EventsResource.ListRequest listRequest = this.CalendarService.Events.List(this.OpContext.CalendarUsername);
				listRequest.MaxResults = new int?(pageSize);
				listRequest.TimeMin = new DateTime?(startdate);
				listRequest.UpdatedMin = new DateTime?(thresholdTime);
				listRequest.SingleEvents = new bool?(true);
				string text = null;
				do
				{
					listRequest.PageToken = text;
					Events events = listRequest.Execute();
					bool flag = events.Items != null && events.Items.Count > 0;
					if (flag)
					{
						list.AddRange(events.Items);
					}
					text = events.NextPageToken;
				}
				while (text != null);
				result = list.ToDomainObject();
			}
			finally
			{
				this.OpContext.CalendarUsername = calendarUsername;
			}
			return result;
		}

		// Token: 0x04000004 RID: 4
		private SyncOperationContext _opContext;
	}
}

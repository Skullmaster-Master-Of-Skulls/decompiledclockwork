using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ClockWorkLogger;
using Google.Apis.Calendar.v3.Data;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.Mappers
{
	// Token: 0x02000009 RID: 9
	internal class ExternalAppointmentTypeConverter : ITypeConverter<Event, ExternalAppointment>
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00003944 File Offset: 0x00001B44
		public ExternalAppointment Convert(ResolutionContext context)
		{
			Event @event = context.SourceValue as Event;
			bool flag = @event == null;
			ExternalAppointment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ExternalAppointment app = ((ExternalAppointment)context.DestinationValue) ?? new ExternalAppointment();
				app.UniqueId = @event.Id;
				app.LegacyGlobalAppointmentId = @event.ICalUID;
				app.Subject = (@event.Summary ?? string.Empty);
				app.IsAllDayEvent = !string.IsNullOrEmpty(@event.Start.Date);
				bool isAllDayEvent = app.IsAllDayEvent;
				if (isAllDayEvent)
				{
					DateTime dateTime = DateTime.Parse(@event.Start.Date);
					DateTime dateTime2 = DateTime.Parse(@event.End.Date);
					app.StartDate = dateTime.Date.Add(new TimeSpan(0, 1, 0));
					app.EndDate = dateTime2.Date.Add(new TimeSpan(11, 59, 0));
				}
				else
				{
					app.StartDate = @event.Start.DateTime.GetValueOrDefault();
					app.EndDate = @event.End.DateTime.GetValueOrDefault();
				}
				app.Memo = (@event.Description ?? string.Empty);
				app.LastModifiedTime = @event.Updated.GetValueOrDefault();
				app.Location = @event.Location;
				app.Attendees = @event.Attendees.ToDomainObject();
				ExternalAppointment app2 = app;
				ExternalAttendee organizer;
				if ((organizer = @event.Organizer.ToDomainObject()) == null)
				{
					IList<EventAttendee> attendees = @event.Attendees;
					if (attendees == null)
					{
						organizer = null;
					}
					else
					{
						EventAttendee eventAttendee = attendees.FirstOrDefault(delegate(EventAttendee a)
						{
							bool? organizer2 = a.Organizer;
							bool flag5 = true;
							return organizer2.GetValueOrDefault() == flag5 & organizer2 != null;
						});
						organizer = ((eventAttendee != null) ? eventAttendee.ToDomainObject() : null);
					}
				}
				app2.Organizer = organizer;
				ExternalAttendee externalAttendee = (app.Organizer != null) ? app.Attendees.FirstOrDefault((ExternalAttendee a) => a.Username.Equals(app.Organizer.Username, StringComparison.OrdinalIgnoreCase)) : null;
				bool flag2 = externalAttendee != null;
				if (flag2)
				{
					externalAttendee.AttendeeType = eAttendeeType.EVENT_ORGANIZER;
				}
				bool flag3 = app.Organizer != null;
				if (flag3)
				{
					ExternalAttendee externalAttendee2 = app.Attendees.FirstOrDefault((ExternalAttendee a) => a.Username.Equals(app.Organizer.Username, StringComparison.OrdinalIgnoreCase));
					bool flag4 = externalAttendee2 == null;
					if (flag4)
					{
						app.Attendees.Add(app.Organizer);
					}
				}
				app.IsRecurring = (@event.Recurrence != null || !string.IsNullOrEmpty(@event.RecurringEventId));
				app.IsPrivate = (!string.IsNullOrEmpty(@event.Visibility) && @event.Visibility.Equals("private", StringComparison.OrdinalIgnoreCase));
				app.IsCancelled = (!string.IsNullOrEmpty(@event.Status) && @event.Status.Equals("cancelled", StringComparison.OrdinalIgnoreCase));
				CWLogger.Logger.Debug("UniqueId={0}, IsPrivate={1}, EventVisibility={2}, IsCancelled={3}, EventStatus={4}", new object[]
				{
					app.UniqueId,
					app.IsPrivate,
					@event.Visibility,
					app.IsCancelled,
					@event.Status
				});
				result = app;
			}
			return result;
		}
	}
}

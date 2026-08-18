using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.Mappers
{
	// Token: 0x0200000A RID: 10
	internal class AppointmentEventEntryTypeConverter : ITypeConverter<ExternalAppointment, Event>
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public Event Convert(ResolutionContext context)
		{
			ExternalAppointment externalAppointment = context.SourceValue as ExternalAppointment;
			bool flag = externalAppointment == null;
			Event result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = context.DestinationValue != null;
				Event evenEntry;
				if (flag2)
				{
					evenEntry = (Event)context.DestinationValue;
					evenEntry.Summary = externalAppointment.Subject;
					evenEntry.Location = externalAppointment.Location;
					evenEntry.Description = externalAppointment.Memo;
				}
				else
				{
					evenEntry = new Event
					{
						Summary = externalAppointment.Subject,
						Description = externalAppointment.Memo,
						Location = externalAppointment.Location
					};
				}
				bool flag3 = evenEntry.Start == null;
				if (flag3)
				{
					evenEntry.Start = new EventDateTime();
				}
				bool flag4 = evenEntry.End == null;
				if (flag4)
				{
					evenEntry.End = new EventDateTime();
				}
				bool isAllDayEvent = externalAppointment.IsAllDayEvent;
				if (isAllDayEvent)
				{
					evenEntry.Start.Date = externalAppointment.StartDate.ToString("yyyy-MM-dd");
					evenEntry.End.Date = externalAppointment.EndDate.ToString("yyyy-MM-dd");
				}
				else
				{
					evenEntry.Start.DateTime = new DateTime?(externalAppointment.StartDate);
					evenEntry.End.DateTime = new DateTime?(externalAppointment.EndDate);
				}
				bool flag5 = evenEntry.Attendees == null;
				if (flag5)
				{
					evenEntry.Attendees = new List<EventAttendee>();
				}
				else
				{
					evenEntry.Attendees.Clear();
				}
				externalAppointment.Attendees.ToDAOWho().ToList<EventAttendee>().ForEach(delegate(EventAttendee e)
				{
					evenEntry.Attendees.Add(e);
				});
				evenEntry.Visibility = (externalAppointment.IsPrivate ? "private" : "default");
				bool isCancelled = externalAppointment.IsCancelled;
				if (isCancelled)
				{
					evenEntry.Status = "cancelled";
				}
				result = evenEntry;
			}
			return result;
		}
	}
}

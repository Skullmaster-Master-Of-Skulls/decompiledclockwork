using System;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.Mappers
{
	// Token: 0x02000008 RID: 8
	public class AttendeeTypeResolver : ValueResolver<EventAttendee, eAttendeeType>
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00003904 File Offset: 0x00001B04
		protected override eAttendeeType ResolveCore(EventAttendee source)
		{
			return (source.Organizer != null && source.Organizer.Value) ? eAttendeeType.EVENT_ORGANIZER : eAttendeeType.EVENT_ATTENDEE;
		}
	}
}

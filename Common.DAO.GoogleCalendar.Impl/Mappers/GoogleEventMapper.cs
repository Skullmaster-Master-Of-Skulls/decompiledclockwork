using System;
using System.Collections.Generic;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.Mappers
{
	// Token: 0x02000004 RID: 4
	internal static class GoogleEventMapper
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002D83 File Offset: 0x00000F83
		static GoogleEventMapper()
		{
			GoogleWhoMapper.CreateMap();
			GoogleOrganizerMapper.CreateMap();
			Mapper.CreateMap<Event, ExternalAppointment>().ConvertUsing<ExternalAppointmentTypeConverter>();
			Mapper.CreateMap<ExternalAppointment, Event>().ConvertUsing<AppointmentEventEntryTypeConverter>();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002429 File Offset: 0x00000629
		public static void CreateMap()
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public static Event ToEventEntryDAO(this ExternalAppointment externalAppointment)
		{
			return Mapper.Map<ExternalAppointment, Event>(externalAppointment);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public static Event ToEventEntryDAO(this ExternalAppointment externalAppointment, Event eventEntry)
		{
			return Mapper.Map<ExternalAppointment, Event>(externalAppointment, eventEntry);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002DDC File Offset: 0x00000FDC
		public static ExternalAppointment ToDomainObject(this Event eventEntry)
		{
			return Mapper.Map<Event, ExternalAppointment>(eventEntry);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public static IList<Event> ToEventEntryDAO(this IList<ExternalAppointment> outlookAppointment)
		{
			return Mapper.Map<IList<ExternalAppointment>, IList<Event>>(outlookAppointment);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002E0C File Offset: 0x0000100C
		public static IList<ExternalAppointment> ToDomainObject(this IList<Event> eventEntry)
		{
			return Mapper.Map<IList<Event>, IList<ExternalAppointment>>(eventEntry);
		}
	}
}

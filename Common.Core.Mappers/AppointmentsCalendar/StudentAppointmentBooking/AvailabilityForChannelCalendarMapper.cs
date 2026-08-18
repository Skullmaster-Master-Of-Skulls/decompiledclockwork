using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.StudentAppointmentBooking;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar.StudentAppointmentBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsCalendar.StudentAppointmentBooking
{
	// Token: 0x02000205 RID: 517
	public static class AvailabilityForChannelCalendarMapper
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x00025C48 File Offset: 0x00023E48
		static AvailabilityForChannelCalendarMapper()
		{
			Mapper.CreateMap<AvailabilityForChannelCalendarDTO, AvailabilityForChannelCalendar>();
			Mapper.CreateMap<AvailabilityForChannelCalendar, AvailabilityForChannelCalendarDTO>();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00025C58 File Offset: 0x00023E58
		public static AvailabilityForChannelCalendar ToDomainObject(this AvailabilityForChannelCalendarDTO appointmentDTO)
		{
			return Mapper.Map<AvailabilityForChannelCalendarDTO, AvailabilityForChannelCalendar>(appointmentDTO);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00025C70 File Offset: 0x00023E70
		public static AvailabilityForChannelCalendarDTO ToDTO(this AvailabilityForChannelCalendar appointment)
		{
			return Mapper.Map<AvailabilityForChannelCalendar, AvailabilityForChannelCalendarDTO>(appointment);
		}
	}
}

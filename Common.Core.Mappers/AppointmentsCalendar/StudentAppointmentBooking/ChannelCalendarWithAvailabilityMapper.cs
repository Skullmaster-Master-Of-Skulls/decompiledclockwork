using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.StudentAppointmentBooking;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar.StudentAppointmentBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsCalendar.StudentAppointmentBooking
{
	// Token: 0x02000206 RID: 518
	public static class ChannelCalendarWithAvailabilityMapper
	{
		// Token: 0x060008BE RID: 2238 RVA: 0x00025C88 File Offset: 0x00023E88
		static ChannelCalendarWithAvailabilityMapper()
		{
			AvailabilityForChannelCalendarMapper.CreateMap();
			Mapper.CreateMap<ChannelCalendarWithAvailabilityDTO, ChannelCalendarWithAvailability>().ForMember((ChannelCalendarWithAvailability pb) => pb.Availabilities, delegate(IMemberConfigurationExpression<ChannelCalendarWithAvailabilityDTO> m)
			{
				m.MapFrom<List<AvailabilityForChannelCalendar>>((ChannelCalendarWithAvailabilityDTO pbdto) => (pbdto.Availabilities == null) ? null : (from g in pbdto.Availabilities
				select g.ToDomainObject()).ToList<AvailabilityForChannelCalendar>());
			});
			Mapper.CreateMap<ChannelCalendarWithAvailability, ChannelCalendarWithAvailabilityDTO>().ForMember((ChannelCalendarWithAvailabilityDTO pb) => pb.Availabilities, delegate(IMemberConfigurationExpression<ChannelCalendarWithAvailability> m)
			{
				m.MapFrom<List<AvailabilityForChannelCalendarDTO>>((ChannelCalendarWithAvailability pbdto) => (pbdto.Availabilities == null) ? null : (from g in pbdto.Availabilities
				select g.ToDTO()).ToList<AvailabilityForChannelCalendarDTO>());
			});
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00025D48 File Offset: 0x00023F48
		public static ChannelCalendarWithAvailability ToDomainObject(this ChannelCalendarWithAvailabilityDTO appointmentDTO)
		{
			return Mapper.Map<ChannelCalendarWithAvailabilityDTO, ChannelCalendarWithAvailability>(appointmentDTO);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00025D60 File Offset: 0x00023F60
		public static ChannelCalendarWithAvailabilityDTO ToDTO(this ChannelCalendarWithAvailability appointment)
		{
			return Mapper.Map<ChannelCalendarWithAvailability, ChannelCalendarWithAvailabilityDTO>(appointment);
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.AvailabilitySchedule2;
using TechnoPro.Common.Public.Entities.AppointmentsList;

namespace TechnoPro.Common.Core.Mappers.AppointmentsList
{
	// Token: 0x020001FE RID: 510
	public static class ListAppointmentOrAvailabilityMapper
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x00024F9C File Offset: 0x0002319C
		static ListAppointmentOrAvailabilityMapper()
		{
			BaseExtendedAppointmentMapper.CreateMap();
			ListAppointmentMapper.CreateMap();
			Availability2ItemMapper.CreateMap();
			Mapper.CreateMap<ListAppointmentOrAvailabilityDTO, ListAppointmentOrAvailability>();
			Mapper.CreateMap<ListAppointmentOrAvailability, ListAppointmentOrAvailabilityDTO>();
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00024FC0 File Offset: 0x000231C0
		public static ListAppointmentOrAvailability ToDomainObject(this ListAppointmentOrAvailabilityDTO listAppointmentDTO)
		{
			return Mapper.Map<ListAppointmentOrAvailabilityDTO, ListAppointmentOrAvailability>(listAppointmentDTO);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00024FD8 File Offset: 0x000231D8
		public static ListAppointmentOrAvailabilityDTO ToDTO(this ListAppointmentOrAvailability listAppointment)
		{
			return Mapper.Map<ListAppointmentOrAvailability, ListAppointmentOrAvailabilityDTO>(listAppointment);
		}
	}
}

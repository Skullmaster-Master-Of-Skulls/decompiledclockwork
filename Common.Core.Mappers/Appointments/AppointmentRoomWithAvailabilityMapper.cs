using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001A9 RID: 425
	public static class AppointmentRoomWithAvailabilityMapper
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x0001FC74 File Offset: 0x0001DE74
		static AppointmentRoomWithAvailabilityMapper()
		{
			AppointmentRoomMapper.CreateMap();
			Mapper.CreateMap<AppointmentRoomWithAvailabilityDTO, AppointmentRoomWithAvailability>().ForMember((AppointmentRoomWithAvailability pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AppointmentRoomWithAvailabilityDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<AppointmentRoomWithAvailability, AppointmentRoomWithAvailabilityDTO>();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001FCF8 File Offset: 0x0001DEF8
		public static AppointmentRoomWithAvailability ToDomainObject(this AppointmentRoomWithAvailabilityDTO attendeeDTO)
		{
			return Mapper.Map<AppointmentRoomWithAvailabilityDTO, AppointmentRoomWithAvailability>(attendeeDTO);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001FD10 File Offset: 0x0001DF10
		public static AppointmentRoomWithAvailabilityDTO ToDTO(this AppointmentRoomWithAvailability attendee)
		{
			return Mapper.Map<AppointmentRoomWithAvailability, AppointmentRoomWithAvailabilityDTO>(attendee);
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001E4 RID: 484
	public static class AppointmentMapper
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x00023150 File Offset: 0x00021350
		static AppointmentMapper()
		{
			BaseBasicAppointmentMapper.CreateMap();
			Mapper.CreateMap<AppointmentDTO, Appointment>().ForMember((Appointment bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<Appointment, AppointmentDTO>();
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000231D4 File Offset: 0x000213D4
		public static Appointment ToDomainObject(this AppointmentDTO accommodationForTestDTO)
		{
			return Mapper.Map<AppointmentDTO, Appointment>(accommodationForTestDTO);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000231EC File Offset: 0x000213EC
		public static AppointmentDTO ToDTO(this Appointment accommodationForTest)
		{
			return Mapper.Map<Appointment, AppointmentDTO>(accommodationForTest);
		}
	}
}

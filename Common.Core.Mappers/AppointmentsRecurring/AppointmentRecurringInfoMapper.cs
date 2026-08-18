using System;
using System.Collections.Generic;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.Common.Core.Mappers.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;

namespace TechnoPro.Common.Core.Mappers.AppointmentsRecurring
{
	// Token: 0x020001FB RID: 507
	public static class AppointmentRecurringInfoMapper
	{
		// Token: 0x06000893 RID: 2195 RVA: 0x00024A2C File Offset: 0x00022C2C
		static AppointmentRecurringInfoMapper()
		{
			AppointmentMapper.CreateMap();
			RecurringAppointmentMapper.CreateMap();
			Mapper.CreateMap<AppointmentRecurringInfoDTO, AppointmentRecurringInfo>().ForMember((AppointmentRecurringInfo pb) => pb.Appointments, delegate(IMemberConfigurationExpression<AppointmentRecurringInfoDTO> m)
			{
				m.MapFrom<IList<RecurringAppointment>>((AppointmentRecurringInfoDTO pbdto) => (pbdto.Appointments == null) ? null : pbdto.Appointments.ToDomainObject());
			});
			Mapper.CreateMap<AppointmentRecurringInfo, AppointmentRecurringInfoDTO>().ForMember((AppointmentRecurringInfoDTO pb) => pb.Appointments, delegate(IMemberConfigurationExpression<AppointmentRecurringInfo> m)
			{
				m.MapFrom<IList<RecurringAppointmentDTO>>((AppointmentRecurringInfo pbdto) => (pbdto.Appointments == null) ? null : pbdto.Appointments.ToDTO());
			});
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00024AF0 File Offset: 0x00022CF0
		public static AppointmentRecurringInfo ToDomainObject(this AppointmentRecurringInfoDTO appointmentRecurringInfoDTO)
		{
			return Mapper.Map<AppointmentRecurringInfoDTO, AppointmentRecurringInfo>(appointmentRecurringInfoDTO);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00024B08 File Offset: 0x00022D08
		public static AppointmentRecurringInfoDTO ToDTO(this AppointmentRecurringInfo appointmentRecurringInfo)
		{
			return Mapper.Map<AppointmentRecurringInfo, AppointmentRecurringInfoDTO>(appointmentRecurringInfo);
		}
	}
}

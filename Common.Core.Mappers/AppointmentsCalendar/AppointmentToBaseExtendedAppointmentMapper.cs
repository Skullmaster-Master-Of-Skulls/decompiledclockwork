using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.Common.Core.Mappers.Appointments;

namespace TechnoPro.Common.Core.Mappers.AppointmentsCalendar
{
	// Token: 0x02000203 RID: 515
	public static class AppointmentToBaseExtendedAppointmentMapper
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x00025A60 File Offset: 0x00023C60
		static AppointmentToBaseExtendedAppointmentMapper()
		{
			AttendeeMapper.CreateMap();
			AppTypeMapper.CreateMap();
			AppShowTimeAsTypeMapper.CreateMap();
			Mapper.CreateMap<AppointmentDTO, BaseExtendedAppointmentDTO>();
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00025A7C File Offset: 0x00023C7C
		public static BaseExtendedAppointmentDTO ToBaseExtendedAppointmentDTO(this AppointmentDTO appointment)
		{
			return Mapper.Map<AppointmentDTO, BaseExtendedAppointmentDTO>(appointment);
		}
	}
}

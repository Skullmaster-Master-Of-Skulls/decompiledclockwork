using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Core.Mappers.Appointments;

namespace TechnoPro.Common.Core.Mappers.AppointmentsWorkshops
{
	// Token: 0x0200019C RID: 412
	public static class WorkshopAppointmentToBaseExtendedAppointmentMapper
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x0001F0D4 File Offset: 0x0001D2D4
		static WorkshopAppointmentToBaseExtendedAppointmentMapper()
		{
			AttendeeMapper.CreateMap();
			AppTypeMapper.CreateMap();
			AppShowTimeAsTypeMapper.CreateMap();
			Mapper.CreateMap<WorkshopAppointmentDTO, BaseExtendedAppointmentDTO>();
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001F0F0 File Offset: 0x0001D2F0
		public static BaseExtendedAppointmentDTO ToBaseExtendedAppointmentDTO(this WorkshopAppointmentDTO appointment)
		{
			return Mapper.Map<WorkshopAppointmentDTO, BaseExtendedAppointmentDTO>(appointment);
		}
	}
}

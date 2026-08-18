using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.Core.Mappers.AppointmentsWorkshops
{
	// Token: 0x0200019B RID: 411
	public static class WorkshopAppointmentMapper
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x0001F014 File Offset: 0x0001D214
		static WorkshopAppointmentMapper()
		{
			BaseExtendedAppointmentMapper.CreateMap();
			AppointmentIconMapper.CreateMap();
			WorkshopDefinitionMapper.CreateMap();
			Mapper.CreateMap<WorkshopAppointmentDTO, WorkshopAppointment>().ForMember((WorkshopAppointment pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<WorkshopAppointmentDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<WorkshopAppointment, WorkshopAppointmentDTO>();
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
		public static WorkshopAppointment ToDomainObject(this WorkshopAppointmentDTO dto)
		{
			return Mapper.Map<WorkshopAppointmentDTO, WorkshopAppointment>(dto);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001F0BC File Offset: 0x0001D2BC
		public static WorkshopAppointmentDTO ToDTO(this WorkshopAppointment item)
		{
			return Mapper.Map<WorkshopAppointment, WorkshopAppointmentDTO>(item);
		}
	}
}

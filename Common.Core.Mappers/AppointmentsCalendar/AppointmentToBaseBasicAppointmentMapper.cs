using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.Common.Core.Mappers.Appointments;

namespace TechnoPro.Common.Core.Mappers.AppointmentsCalendar
{
	// Token: 0x02000202 RID: 514
	public static class AppointmentToBaseBasicAppointmentMapper
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x000258AC File Offset: 0x00023AAC
		static AppointmentToBaseBasicAppointmentMapper()
		{
			AttendeeMapper.CreateMap();
			AppTypeMapper.CreateMap();
			AppShowTimeAsTypeMapper.CreateMap();
			Mapper.CreateMap<AppointmentDTO, BaseBasicAppointmentDTO>().ForMember((BaseBasicAppointmentDTO pb) => (object)pb.IsTentative, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((BaseBasicAppointmentDTO pb) => (object)pb.IsRecurring, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((BaseBasicAppointmentDTO pb) => (object)pb.IsPointOfContact, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((BaseBasicAppointmentDTO pb) => (object)pb.IsAllDay, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00025A48 File Offset: 0x00023C48
		public static BaseBasicAppointmentDTO ToBaseBasicAppointmentDTO(this AppointmentDTO appointment)
		{
			return Mapper.Map<AppointmentDTO, BaseBasicAppointmentDTO>(appointment);
		}
	}
}

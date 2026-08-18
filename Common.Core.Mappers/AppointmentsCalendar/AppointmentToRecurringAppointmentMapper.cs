using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.Common.Core.Mappers.Appointments;

namespace TechnoPro.Common.Core.Mappers.AppointmentsCalendar
{
	// Token: 0x02000204 RID: 516
	public static class AppointmentToRecurringAppointmentMapper
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x00025A94 File Offset: 0x00023C94
		static AppointmentToRecurringAppointmentMapper()
		{
			AttendeeMapper.CreateMap();
			AppTypeMapper.CreateMap();
			AppShowTimeAsTypeMapper.CreateMap();
			Mapper.CreateMap<AppointmentDTO, RecurringAppointmentDTO>().ForMember((RecurringAppointmentDTO pb) => (object)pb.IsTentative, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((RecurringAppointmentDTO pb) => (object)pb.IsRecurring, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((RecurringAppointmentDTO pb) => (object)pb.IsPointOfContact, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((RecurringAppointmentDTO pb) => (object)pb.IsAllDay, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00025C30 File Offset: 0x00023E30
		public static RecurringAppointmentDTO ToRecurringAppointmentDTO(this AppointmentDTO appointment)
		{
			return Mapper.Map<AppointmentDTO, RecurringAppointmentDTO>(appointment);
		}
	}
}

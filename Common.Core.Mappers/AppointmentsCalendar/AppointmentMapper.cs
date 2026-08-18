using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.AppointmentsRecurring;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.AppointmentsWorkshops;
using TechnoPro.Common.Core.Mappers.Cases;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;
using TechnoPro.Common.Public.Entities.Cases;

namespace TechnoPro.Common.Core.Mappers.AppointmentsCalendar
{
	// Token: 0x02000201 RID: 513
	public static class AppointmentMapper
	{
		// Token: 0x060008AD RID: 2221 RVA: 0x00025500 File Offset: 0x00023700
		static AppointmentMapper()
		{
			AppShowTimeAsTypeMapper.CreateMap();
			AppTypeMapper.CreateMap();
			AppCancelInfoMapper.CreateMap();
			AppointmentIconMapper.CreateMap();
			AppointmentWorkshopInfoMapper.CreateMap();
			AttendeeMapper.CreateMap();
			AppointmentRecurringInfoMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			CaseBaseMapper.CreateMap();
			BasicAppointmentTestExamInfoMapper.CreateMap();
			AppointmentRoomMapper.CreateMap();
			Mapper.CreateMap<AppointmentDTO, Appointment>().ForMember((Appointment pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((Appointment pb) => pb.Icons, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.MapFrom<List<AppointmentIcon>>((AppointmentDTO pbdto) => (pbdto.Icons == null) ? null : (from g in pbdto.Icons
				select g.ToDomainObject()).ToList<AppointmentIcon>());
			}).ForMember((Appointment pb) => pb.CaseInfo, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.MapFrom<CaseBase>((AppointmentDTO pbdto) => (pbdto.CaseInfo == null) ? null : pbdto.CaseInfo.ToDomainObject());
			}).ForMember((Appointment pb) => pb.TestExamInfo, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.MapFrom<BasicAppointmentTestExamInfo>((AppointmentDTO pbdto) => (pbdto.TestExamInfo == null) ? null : pbdto.TestExamInfo.ToDomainObject());
			}).ForMember((Appointment pb) => pb.WorkshopInfo, delegate(IMemberConfigurationExpression<AppointmentDTO> m)
			{
				m.MapFrom<AppointmentWorkshopInfo>((AppointmentDTO pbdto) => (pbdto.WorkshopInfo == null) ? null : pbdto.WorkshopInfo.ToDomainObject());
			});
			Mapper.CreateMap<Appointment, AppointmentDTO>().ForMember((AppointmentDTO pb) => pb.Tag, delegate(IMemberConfigurationExpression<Appointment> m)
			{
				m.Ignore();
			}).ForMember((AppointmentDTO pb) => pb.Icons, delegate(IMemberConfigurationExpression<Appointment> m)
			{
				m.MapFrom<List<AppointmentIconDTO>>((Appointment pbdto) => (pbdto.Icons == null) ? null : (from g in pbdto.Icons
				select g.ToDTO()).ToList<AppointmentIconDTO>());
			}).ForMember((AppointmentDTO pb) => pb.CaseInfo, delegate(IMemberConfigurationExpression<Appointment> m)
			{
				m.MapFrom<CaseBaseDTO>((Appointment pbdto) => (pbdto.CaseInfo == null) ? null : pbdto.CaseInfo.ToDTO());
			}).ForMember((AppointmentDTO pb) => pb.TestExamInfo, delegate(IMemberConfigurationExpression<Appointment> m)
			{
				m.MapFrom<BasicAppointmentTestExamInfoDTO>((Appointment pbdto) => (pbdto.TestExamInfo == null) ? null : pbdto.TestExamInfo.ToDTO());
			}).ForMember((AppointmentDTO pb) => pb.WorkshopInfo, delegate(IMemberConfigurationExpression<Appointment> m)
			{
				m.MapFrom<AppointmentWorkshopInfoDTO>((Appointment pbdto) => (pbdto.WorkshopInfo == null) ? null : pbdto.WorkshopInfo.ToDTO());
			});
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0002587C File Offset: 0x00023A7C
		public static Appointment ToDomainObject(this AppointmentDTO appointmentDTO)
		{
			return Mapper.Map<AppointmentDTO, Appointment>(appointmentDTO);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00025894 File Offset: 0x00023A94
		public static AppointmentDTO ToDTO(this Appointment appointment)
		{
			return Mapper.Map<Appointment, AppointmentDTO>(appointment);
		}
	}
}

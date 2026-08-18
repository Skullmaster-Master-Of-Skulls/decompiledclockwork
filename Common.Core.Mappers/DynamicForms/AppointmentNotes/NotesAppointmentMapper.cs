using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes
{
	// Token: 0x0200012B RID: 299
	public static class NotesAppointmentMapper
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x00018DFC File Offset: 0x00016FFC
		static NotesAppointmentMapper()
		{
			AppTypeMapper.CreateMap();
			AppShowTimeAsTypeMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			AttendeeMapper.CreateMap();
			Mapper.CreateMap<NotesAppointmentDTO, NotesAppointment>().ForMember((NotesAppointment pb) => pb.AppointmentType, delegate(IMemberConfigurationExpression<NotesAppointmentDTO> m)
			{
				m.MapFrom<AppType>((NotesAppointmentDTO pbdto) => (pbdto.AppointmentType == null) ? null : pbdto.AppointmentType.ToDomainObject());
			}).ForMember((NotesAppointment pb) => pb.ShowTimeAs, delegate(IMemberConfigurationExpression<NotesAppointmentDTO> m)
			{
				m.MapFrom<AppShowTimeAsType>((NotesAppointmentDTO pbdto) => (pbdto.ShowTimeAs == null) ? null : pbdto.ShowTimeAs.ToDomainObject());
			}).ForMember((NotesAppointment pb) => pb.PrimaryStudent, delegate(IMemberConfigurationExpression<NotesAppointmentDTO> m)
			{
				m.MapFrom<PersonBase>((NotesAppointmentDTO pbdto) => (pbdto.PrimaryStudent == null) ? null : pbdto.PrimaryStudent.ToDomainObject());
			}).ForMember((NotesAppointment pb) => pb.Attendees, delegate(IMemberConfigurationExpression<NotesAppointmentDTO> m)
			{
				m.MapFrom<List<Attendee>>((NotesAppointmentDTO pbdto) => (pbdto.Attendees == null) ? null : (from g in pbdto.Attendees
				select g.ToDomainObject()).ToList<Attendee>());
			});
			Mapper.CreateMap<NotesAppointment, NotesAppointmentDTO>().ForMember((NotesAppointmentDTO pb) => pb.AppointmentType, delegate(IMemberConfigurationExpression<NotesAppointment> m)
			{
				m.MapFrom<AppTypeDTO>((NotesAppointment pbdto) => (pbdto.AppointmentType == null) ? null : pbdto.AppointmentType.ToDTO());
			}).ForMember((NotesAppointmentDTO pb) => pb.ShowTimeAs, delegate(IMemberConfigurationExpression<NotesAppointment> m)
			{
				m.MapFrom<AppShowTimeAsTypeDTO>((NotesAppointment pbdto) => (pbdto.ShowTimeAs == null) ? null : pbdto.ShowTimeAs.ToDTO());
			}).ForMember((NotesAppointmentDTO pb) => pb.PrimaryStudent, delegate(IMemberConfigurationExpression<NotesAppointment> m)
			{
				m.MapFrom<PersonBaseDTO>((NotesAppointment pbdto) => (pbdto.PrimaryStudent == null) ? null : pbdto.PrimaryStudent.ToDTO());
			}).ForMember((NotesAppointmentDTO pb) => pb.Attendees, delegate(IMemberConfigurationExpression<NotesAppointment> m)
			{
				m.MapFrom<List<AttendeeDTO>>((NotesAppointment pbdto) => (pbdto.Attendees == null) ? null : (from g in pbdto.Attendees
				select g.ToDTO()).ToList<AttendeeDTO>());
			});
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000190A0 File Offset: 0x000172A0
		public static NotesAppointment ToDomainObject(this NotesAppointmentDTO dynamicDataDTO)
		{
			return Mapper.Map<NotesAppointmentDTO, NotesAppointment>(dynamicDataDTO);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000190B8 File Offset: 0x000172B8
		public static NotesAppointmentDTO ToDTO(this NotesAppointment dynamicData)
		{
			return Mapper.Map<NotesAppointment, NotesAppointmentDTO>(dynamicData);
		}
	}
}

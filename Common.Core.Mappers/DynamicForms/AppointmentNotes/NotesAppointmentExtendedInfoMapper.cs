using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes
{
	// Token: 0x0200012A RID: 298
	public static class NotesAppointmentExtendedInfoMapper
	{
		// Token: 0x0600051D RID: 1309 RVA: 0x00018C0C File Offset: 0x00016E0C
		static NotesAppointmentExtendedInfoMapper()
		{
			AttendeeMapper.CreateMap();
			StudentClassTestMapper.CreateMap();
			Mapper.CreateMap<NotesAppointmentExtendedInfoDTO, NotesAppointmentExtendedInfo>().ForMember((NotesAppointmentExtendedInfo pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<NotesAppointmentExtendedInfoDTO> m)
			{
				m.Ignore();
			}).ForMember((NotesAppointmentExtendedInfo pb) => pb.Attendees, delegate(IMemberConfigurationExpression<NotesAppointmentExtendedInfoDTO> m)
			{
				m.MapFrom<IEnumerable<Attendee>>((NotesAppointmentExtendedInfoDTO pbdto) => (pbdto.Attendees == null) ? null : (from g in pbdto.Attendees
				select g.ToDomainObject()));
			}).ForMember((NotesAppointmentExtendedInfo pb) => pb.StudentClassTestInfo, delegate(IMemberConfigurationExpression<NotesAppointmentExtendedInfoDTO> m)
			{
				m.MapFrom<StudentClassTest>((NotesAppointmentExtendedInfoDTO pbdto) => (pbdto.StudentClassTestInfo == null) ? null : pbdto.StudentClassTestInfo.ToDomainObject());
			});
			Mapper.CreateMap<NotesAppointmentExtendedInfo, NotesAppointmentExtendedInfoDTO>().ForMember((NotesAppointmentExtendedInfoDTO pb) => pb.Attendees, delegate(IMemberConfigurationExpression<NotesAppointmentExtendedInfo> m)
			{
				m.MapFrom<IEnumerable<AttendeeDTO>>((NotesAppointmentExtendedInfo pbdto) => (pbdto.Attendees == null) ? null : (from g in pbdto.Attendees
				select g.ToDTO()));
			}).ForMember((NotesAppointmentExtendedInfoDTO pb) => pb.StudentClassTestInfo, delegate(IMemberConfigurationExpression<NotesAppointmentExtendedInfo> m)
			{
				m.MapFrom<StudentClassTestDTO>((NotesAppointmentExtendedInfo pbdto) => (pbdto.StudentClassTestInfo == null) ? null : pbdto.StudentClassTestInfo.ToDTO());
			});
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00018DCC File Offset: 0x00016FCC
		public static NotesAppointmentExtendedInfo ToDomainObject(this NotesAppointmentExtendedInfoDTO dto)
		{
			return Mapper.Map<NotesAppointmentExtendedInfoDTO, NotesAppointmentExtendedInfo>(dto);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00018DE4 File Offset: 0x00016FE4
		public static NotesAppointmentExtendedInfoDTO ToDTO(this NotesAppointmentExtendedInfo item)
		{
			return Mapper.Map<NotesAppointmentExtendedInfo, NotesAppointmentExtendedInfoDTO>(item);
		}
	}
}

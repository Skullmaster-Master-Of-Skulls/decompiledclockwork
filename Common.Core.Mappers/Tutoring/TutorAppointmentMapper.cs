using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.Core.Mappers.Tutoring
{
	// Token: 0x0200002A RID: 42
	public static class TutorAppointmentMapper
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00005A64 File Offset: 0x00003C64
		static TutorAppointmentMapper()
		{
			BaseBasicAppointmentMapper.CreateMap();
			AppTypeMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<TutorAppointmentDTO, TutorAppointment>().ForMember((TutorAppointment pb) => pb.Tutor, delegate(IMemberConfigurationExpression<TutorAppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((TutorAppointment pb) => pb.Student, delegate(IMemberConfigurationExpression<TutorAppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((TutorAppointment pb) => pb.StudentNoteToTutor, delegate(IMemberConfigurationExpression<TutorAppointmentDTO> m)
			{
				m.Ignore();
			}).ForMember((TutorAppointment pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TutorAppointmentDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TutorAppointment, TutorAppointmentDTO>().ForMember((TutorAppointmentDTO pb) => pb.Tutor, delegate(IMemberConfigurationExpression<TutorAppointment> m)
			{
				m.Ignore();
			}).ForMember((TutorAppointmentDTO pb) => pb.StudentNoteToTutor, delegate(IMemberConfigurationExpression<TutorAppointment> m)
			{
				m.Ignore();
			}).ForMember((TutorAppointmentDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<TutorAppointment> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005CC8 File Offset: 0x00003EC8
		public static TutorAppointment ToDomainObject(this TutorAppointmentDTO listAppointmentDTO)
		{
			return Mapper.Map<TutorAppointmentDTO, TutorAppointment>(listAppointmentDTO);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public static TutorAppointmentDTO ToDTO(this TutorAppointment listAppointment)
		{
			return Mapper.Map<TutorAppointment, TutorAppointmentDTO>(listAppointment);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.CourseRegistrations;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Students;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.MergeDuplicatesStudents
{
	// Token: 0x020000BC RID: 188
	public static class DuplicateStudentMapper
	{
		// Token: 0x06000320 RID: 800 RVA: 0x000102D4 File Offset: 0x0000E4D4
		static DuplicateStudentMapper()
		{
			PersonBaseMapper.CreateMap();
			BaseBasicAppointmentMapper.CreateMap();
			DynamicDataMapper.CreateMap();
			CourseRegistrationMapper.CreateMap();
			Mapper.CreateMap<DuplicateStudentDTO, DuplicateStudent>().ForMember((DuplicateStudent pb) => pb.Courses, delegate(IMemberConfigurationExpression<DuplicateStudentDTO> m)
			{
				m.MapFrom<List<CourseRegistration>>((DuplicateStudentDTO pbdto) => (pbdto.Courses == null) ? null : pbdto.Courses.ToList<CourseRegistrationDTO>().ConvertAll<CourseRegistration>((CourseRegistrationDTO g) => g.ToDomainObject()));
			}).ForMember((DuplicateStudent pb) => pb.PerStudentDataItems, delegate(IMemberConfigurationExpression<DuplicateStudentDTO> m)
			{
				m.MapFrom<List<DynamicData>>((DuplicateStudentDTO pbdto) => (pbdto.PerStudentDataItems == null) ? null : pbdto.PerStudentDataItems.ToList<DynamicDataDTO>().ConvertAll<DynamicData>((DynamicDataDTO g) => g.ToDomainObject()));
			}).ForMember((DuplicateStudent pb) => pb.Student, delegate(IMemberConfigurationExpression<DuplicateStudentDTO> m)
			{
				m.MapFrom<PersonBase>((DuplicateStudentDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((DuplicateStudent pb) => pb.Appointments, delegate(IMemberConfigurationExpression<DuplicateStudentDTO> m)
			{
				m.MapFrom<List<BaseBasicAppointment>>((DuplicateStudentDTO pbdto) => (pbdto.Appointments == null) ? null : pbdto.Appointments.ToList<BaseBasicAppointmentDTO>().ConvertAll<BaseBasicAppointment>((BaseBasicAppointmentDTO g) => g.ToDomainObject()));
			});
			Mapper.CreateMap<DuplicateStudent, DuplicateStudentDTO>().ForMember((DuplicateStudentDTO pb) => pb.Courses, delegate(IMemberConfigurationExpression<DuplicateStudent> m)
			{
				m.MapFrom<List<CourseRegistrationDTO>>((DuplicateStudent pbdto) => (pbdto.Courses == null) ? null : pbdto.Courses.ToList<CourseRegistration>().ConvertAll<CourseRegistrationDTO>((CourseRegistration g) => g.ToDTO()));
			}).ForMember((DuplicateStudentDTO pb) => pb.PerStudentDataItems, delegate(IMemberConfigurationExpression<DuplicateStudent> m)
			{
				m.MapFrom<List<DynamicDataDTO>>((DuplicateStudent pbdto) => (pbdto.PerStudentDataItems == null) ? null : pbdto.PerStudentDataItems.ToList<DynamicData>().ConvertAll<DynamicDataDTO>((DynamicData g) => g.ToDTO()));
			}).ForMember((DuplicateStudentDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<DuplicateStudent> m)
			{
				m.MapFrom<PersonBaseDTO>((DuplicateStudent pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((DuplicateStudentDTO pb) => pb.Appointments, delegate(IMemberConfigurationExpression<DuplicateStudent> m)
			{
				m.MapFrom<List<BaseBasicAppointmentDTO>>((DuplicateStudent pbdto) => (pbdto.Appointments == null) ? null : pbdto.Appointments.ToList<BaseBasicAppointment>().ConvertAll<BaseBasicAppointmentDTO>((BaseBasicAppointment g) => g.ToDTO()));
			});
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00010578 File Offset: 0x0000E778
		public static DuplicateStudent ToDomainObject(this DuplicateStudentDTO dto)
		{
			return Mapper.Map<DuplicateStudentDTO, DuplicateStudent>(dto);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00010590 File Offset: 0x0000E790
		public static DuplicateStudentDTO ToDTO(this DuplicateStudent item)
		{
			return Mapper.Map<DuplicateStudent, DuplicateStudentDTO>(item);
		}
	}
}

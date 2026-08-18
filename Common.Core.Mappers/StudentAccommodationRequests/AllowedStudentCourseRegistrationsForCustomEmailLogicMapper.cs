using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Core.Mappers.CourseRegistrations;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.Mappers.StudentAccommodationRequests
{
	// Token: 0x0200005B RID: 91
	public static class AllowedStudentCourseRegistrationsForCustomEmailLogicMapper
	{
		// Token: 0x06000174 RID: 372 RVA: 0x0000A04C File Offset: 0x0000824C
		static AllowedStudentCourseRegistrationsForCustomEmailLogicMapper()
		{
			PersonBaseMapper.CreateMap();
			CourseRegistrationMapper.CreateMap();
			Mapper.CreateMap<AllowedStudentCourseRegistrationsForCustomEmailLogicDTO, AllowedStudentCourseRegistrationsForCustomEmailLogic>().ForMember((AllowedStudentCourseRegistrationsForCustomEmailLogic pb) => pb.Student, delegate(IMemberConfigurationExpression<AllowedStudentCourseRegistrationsForCustomEmailLogicDTO> m)
			{
				m.MapFrom<PersonBase>((AllowedStudentCourseRegistrationsForCustomEmailLogicDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((AllowedStudentCourseRegistrationsForCustomEmailLogic pb) => pb.CourseRegistrations, delegate(IMemberConfigurationExpression<AllowedStudentCourseRegistrationsForCustomEmailLogicDTO> m)
			{
				m.MapFrom<List<CourseRegistration>>((AllowedStudentCourseRegistrationsForCustomEmailLogicDTO pbdto) => (pbdto.CourseRegistrations == null) ? null : (from h in pbdto.CourseRegistrations
				select h.ToDomainObject()).ToList<CourseRegistration>());
			});
			Mapper.CreateMap<AllowedStudentCourseRegistrationsForCustomEmailLogic, AllowedStudentCourseRegistrationsForCustomEmailLogicDTO>().ForMember((AllowedStudentCourseRegistrationsForCustomEmailLogicDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<AllowedStudentCourseRegistrationsForCustomEmailLogic> m)
			{
				m.MapFrom<PersonBaseDTO>((AllowedStudentCourseRegistrationsForCustomEmailLogic pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((AllowedStudentCourseRegistrationsForCustomEmailLogicDTO pb) => pb.CourseRegistrations, delegate(IMemberConfigurationExpression<AllowedStudentCourseRegistrationsForCustomEmailLogic> m)
			{
				m.MapFrom<List<CourseRegistrationDTO>>((AllowedStudentCourseRegistrationsForCustomEmailLogic pbdto) => (pbdto.CourseRegistrations == null) ? null : (from h in pbdto.CourseRegistrations
				select h.ToDTO()).ToList<CourseRegistrationDTO>());
			});
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000A1AC File Offset: 0x000083AC
		public static AllowedStudentCourseRegistrationsForCustomEmailLogic ToDomainObject(this AllowedStudentCourseRegistrationsForCustomEmailLogicDTO dto)
		{
			return Mapper.Map<AllowedStudentCourseRegistrationsForCustomEmailLogicDTO, AllowedStudentCourseRegistrationsForCustomEmailLogic>(dto);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000A1C4 File Offset: 0x000083C4
		public static AllowedStudentCourseRegistrationsForCustomEmailLogicDTO ToDTO(this AllowedStudentCourseRegistrationsForCustomEmailLogic item)
		{
			return Mapper.Map<AllowedStudentCourseRegistrationsForCustomEmailLogic, AllowedStudentCourseRegistrationsForCustomEmailLogicDTO>(item);
		}
	}
}

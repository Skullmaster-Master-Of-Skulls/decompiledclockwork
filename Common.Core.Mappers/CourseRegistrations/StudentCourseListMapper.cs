using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.CourseRegistrations;

namespace TechnoPro.Common.Core.Mappers.CourseRegistrations
{
	// Token: 0x02000164 RID: 356
	public static class StudentCourseListMapper
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x0001C200 File Offset: 0x0001A400
		static StudentCourseListMapper()
		{
			CourseRegistrationMapper.CreateMap();
			Mapper.CreateMap<StudentCourseListDTO, StudentCourseList>().ForMember((StudentCourseList pb) => pb.Courses, delegate(IMemberConfigurationExpression<StudentCourseListDTO> m)
			{
				m.MapFrom<List<CourseRegistration>>((StudentCourseListDTO pbdto) => (pbdto.Courses == null) ? null : (from g in pbdto.Courses
				select g.ToDomainObject()).ToList<CourseRegistration>());
			});
			Mapper.CreateMap<StudentCourseList, StudentCourseListDTO>().ForMember((StudentCourseListDTO pb) => pb.Courses, delegate(IMemberConfigurationExpression<StudentCourseList> m)
			{
				m.MapFrom<List<CourseRegistrationDTO>>((StudentCourseList pbdto) => (pbdto.Courses == null) ? null : (from g in pbdto.Courses
				select g.ToDTO()).ToList<CourseRegistrationDTO>());
			});
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001C2BC File Offset: 0x0001A4BC
		public static StudentCourseList ToDomainObject(this StudentCourseListDTO courseRegistrationDTO)
		{
			return Mapper.Map<StudentCourseListDTO, StudentCourseList>(courseRegistrationDTO);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		public static StudentCourseListDTO ToDTO(this StudentCourseList courseRegistration)
		{
			return Mapper.Map<StudentCourseList, StudentCourseListDTO>(courseRegistration);
		}
	}
}

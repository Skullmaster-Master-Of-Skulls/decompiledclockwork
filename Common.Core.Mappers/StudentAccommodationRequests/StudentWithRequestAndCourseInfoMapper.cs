using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.Mappers.StudentAccommodationRequests
{
	// Token: 0x02000063 RID: 99
	public static class StudentWithRequestAndCourseInfoMapper
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000A92C File Offset: 0x00008B2C
		static StudentWithRequestAndCourseInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<StudentWithRequestAndCourseInfoDTO, StudentWithRequestAndCourseInfo>().ForMember((StudentWithRequestAndCourseInfo pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentWithRequestAndCourseInfoDTO> m)
			{
				m.Ignore();
			}).ForMember((StudentWithRequestAndCourseInfo pb) => pb.Student, delegate(IMemberConfigurationExpression<StudentWithRequestAndCourseInfoDTO> m)
			{
				m.MapFrom<PersonBase>((StudentWithRequestAndCourseInfoDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((StudentWithRequestAndCourseInfo pb) => pb.CourseBase, delegate(IMemberConfigurationExpression<StudentWithRequestAndCourseInfoDTO> m)
			{
				m.MapFrom<LookupCourseBase>((StudentWithRequestAndCourseInfoDTO pbdto) => (pbdto.CourseBase == null) ? null : pbdto.CourseBase.ToDomainObject());
			});
			Mapper.CreateMap<StudentWithRequestAndCourseInfo, StudentWithRequestAndCourseInfoDTO>();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000AA50 File Offset: 0x00008C50
		public static StudentWithRequestAndCourseInfo ToDomainObject(this StudentWithRequestAndCourseInfoDTO dto)
		{
			return Mapper.Map<StudentWithRequestAndCourseInfoDTO, StudentWithRequestAndCourseInfo>(dto);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000AA68 File Offset: 0x00008C68
		public static StudentWithRequestAndCourseInfoDTO ToDTO(this StudentWithRequestAndCourseInfo item)
		{
			return Mapper.Map<StudentWithRequestAndCourseInfo, StudentWithRequestAndCourseInfoDTO>(item);
		}
	}
}

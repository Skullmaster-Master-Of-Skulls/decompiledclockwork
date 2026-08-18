using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.CourseRegistrations
{
	// Token: 0x02000165 RID: 357
	public static class StudentWithCourseAndAccommodationInfoMapper
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x0001C2EC File Offset: 0x0001A4EC
		static StudentWithCourseAndAccommodationInfoMapper()
		{
			LookupCourseBaseMapper.CreateMap();
			BasicPersonMapper.CreateMap();
			Mapper.CreateMap<StudentWithCourseAndAccommodationInfoDTO, StudentWithCourseAndAccommodationInfo>().ForMember((StudentWithCourseAndAccommodationInfo pb) => pb.Student, delegate(IMemberConfigurationExpression<StudentWithCourseAndAccommodationInfoDTO> m)
			{
				m.MapFrom<BasicPerson>((StudentWithCourseAndAccommodationInfoDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((StudentWithCourseAndAccommodationInfo pb) => pb.CourseBase, delegate(IMemberConfigurationExpression<StudentWithCourseAndAccommodationInfoDTO> m)
			{
				m.MapFrom<LookupCourseBase>((StudentWithCourseAndAccommodationInfoDTO pbdto) => (pbdto.CourseBase == null) ? null : pbdto.CourseBase.ToDomainObject());
			});
			Mapper.CreateMap<StudentWithCourseAndAccommodationInfo, StudentWithCourseAndAccommodationInfoDTO>().ForMember((StudentWithCourseAndAccommodationInfoDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<StudentWithCourseAndAccommodationInfo> m)
			{
				m.MapFrom<BasicPersonDTO>((StudentWithCourseAndAccommodationInfo pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((StudentWithCourseAndAccommodationInfoDTO pb) => pb.CourseBase, delegate(IMemberConfigurationExpression<StudentWithCourseAndAccommodationInfo> m)
			{
				m.MapFrom<LookupCourseBaseDTO>((StudentWithCourseAndAccommodationInfo pbdto) => (pbdto.CourseBase == null) ? null : pbdto.CourseBase.ToDTO());
			});
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001C44C File Offset: 0x0001A64C
		public static StudentWithCourseAndAccommodationInfo ToDomainObject(this StudentWithCourseAndAccommodationInfoDTO dto)
		{
			return Mapper.Map<StudentWithCourseAndAccommodationInfoDTO, StudentWithCourseAndAccommodationInfo>(dto);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001C464 File Offset: 0x0001A664
		public static StudentWithCourseAndAccommodationInfoDTO ToDTO(this StudentWithCourseAndAccommodationInfo item)
		{
			return Mapper.Map<StudentWithCourseAndAccommodationInfo, StudentWithCourseAndAccommodationInfoDTO>(item);
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Core.Mappers.StudentAccommodationRequests;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.Mappers.CourseRegistrations
{
	// Token: 0x0200015D RID: 349
	public static class CourseRegistrationBaseMapper
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x0001B820 File Offset: 0x00019A20
		static CourseRegistrationBaseMapper()
		{
			LookupCourseBaseMapper.CreateMap();
			CourseRequestBaseMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<CourseRegistrationBaseDTO, CourseRegistrationBase>().ForMember((CourseRegistrationBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CourseRegistrationBaseDTO> m)
			{
				m.Ignore();
			}).ForMember((CourseRegistrationBase pb) => pb.Course, delegate(IMemberConfigurationExpression<CourseRegistrationBaseDTO> m)
			{
				m.MapFrom<LookupCourseBase>((CourseRegistrationBaseDTO pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDomainObject());
			}).ForMember((CourseRegistrationBase pb) => pb.Student, delegate(IMemberConfigurationExpression<CourseRegistrationBaseDTO> m)
			{
				m.MapFrom<PersonBase>((CourseRegistrationBaseDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((CourseRegistrationBase pb) => pb.CourseAccommodationRequestBase, delegate(IMemberConfigurationExpression<CourseRegistrationBaseDTO> m)
			{
				m.MapFrom<CourseRequestBase>((CourseRegistrationBaseDTO pbdto) => (pbdto.CourseAccommodationRequestBase == null) ? null : pbdto.CourseAccommodationRequestBase.ToDomainObject());
			});
			Mapper.CreateMap<CourseRegistrationBase, CourseRegistrationBaseDTO>().ForMember((CourseRegistrationBaseDTO pb) => pb.Course, delegate(IMemberConfigurationExpression<CourseRegistrationBase> m)
			{
				m.MapFrom<LookupCourseBaseDTO>((CourseRegistrationBase pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDTO());
			}).ForMember((CourseRegistrationBaseDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<CourseRegistrationBase> m)
			{
				m.MapFrom<PersonBaseDTO>((CourseRegistrationBase pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((CourseRegistrationBaseDTO pb) => pb.CourseAccommodationRequestBase, delegate(IMemberConfigurationExpression<CourseRegistrationBase> m)
			{
				m.MapFrom<CourseRequestBaseDTO>((CourseRegistrationBase pbdto) => (pbdto.CourseAccommodationRequestBase == null) ? null : pbdto.CourseAccommodationRequestBase.ToDTO());
			});
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001BA84 File Offset: 0x00019C84
		public static CourseRegistrationBase ToDomainObject(this CourseRegistrationBaseDTO dto)
		{
			return Mapper.Map<CourseRegistrationBaseDTO, CourseRegistrationBase>(dto);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001BA9C File Offset: 0x00019C9C
		public static CourseRegistrationBaseDTO ToDTO(this CourseRegistrationBase item)
		{
			return Mapper.Map<CourseRegistrationBase, CourseRegistrationBaseDTO>(item);
		}
	}
}

using System;
using AutoMapper;
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
	// Token: 0x0200015E RID: 350
	public static class CourseRegistrationMapper
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		static CourseRegistrationMapper()
		{
			eRegistrationStatusMapper.CreateMap();
			LookupCourseMapper.CreateMap();
			CourseRequestBaseMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<CourseRegistrationDTO, CourseRegistration>().ForMember((CourseRegistration pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CourseRegistrationDTO> m)
			{
				m.Ignore();
			}).ForMember((CourseRegistration pb) => pb.Course, delegate(IMemberConfigurationExpression<CourseRegistrationDTO> m)
			{
				m.MapFrom<LookupCourse>((CourseRegistrationDTO pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDomainObject());
			}).ForMember((CourseRegistration pb) => pb.Student, delegate(IMemberConfigurationExpression<CourseRegistrationDTO> m)
			{
				m.MapFrom<PersonBase>((CourseRegistrationDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((CourseRegistration pb) => pb.WhoAdded, delegate(IMemberConfigurationExpression<CourseRegistrationDTO> m)
			{
				m.MapFrom<PersonBase>((CourseRegistrationDTO pbdto) => (pbdto.WhoAdded == null) ? null : pbdto.WhoAdded.ToDomainObject());
			}).ForMember((CourseRegistration pb) => pb.CourseAccommodationRequestBase, delegate(IMemberConfigurationExpression<CourseRegistrationDTO> m)
			{
				m.MapFrom<CourseRequestBase>((CourseRegistrationDTO pbdto) => (pbdto.CourseAccommodationRequestBase == null) ? null : pbdto.CourseAccommodationRequestBase.ToDomainObject());
			});
			Mapper.CreateMap<CourseRegistration, CourseRegistrationDTO>().ForMember((CourseRegistrationDTO pb) => pb.Course, delegate(IMemberConfigurationExpression<CourseRegistration> m)
			{
				m.MapFrom<LookupCourseDTO>((CourseRegistration pbdto) => (pbdto.Course == null) ? null : pbdto.Course.ToDTO());
			}).ForMember((CourseRegistrationDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<CourseRegistration> m)
			{
				m.MapFrom<PersonBaseDTO>((CourseRegistration pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((CourseRegistrationDTO pb) => pb.WhoAdded, delegate(IMemberConfigurationExpression<CourseRegistration> m)
			{
				m.MapFrom<PersonBaseDTO>((CourseRegistration pbdto) => (pbdto.WhoAdded == null) ? null : pbdto.WhoAdded.ToDTO());
			}).ForMember((CourseRegistrationDTO pb) => pb.CourseAccommodationRequestBase, delegate(IMemberConfigurationExpression<CourseRegistration> m)
			{
				m.MapFrom<CourseRequestBaseDTO>((CourseRegistration pbdto) => (pbdto.CourseAccommodationRequestBase == null) ? null : pbdto.CourseAccommodationRequestBase.ToDTO());
			});
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		public static CourseRegistration ToDomainObject(this CourseRegistrationDTO courseRegistrationDTO)
		{
			return Mapper.Map<CourseRegistrationDTO, CourseRegistration>(courseRegistrationDTO);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001BDD0 File Offset: 0x00019FD0
		public static CourseRegistrationDTO ToDTO(this CourseRegistration courseRegistration)
		{
			return Mapper.Map<CourseRegistration, CourseRegistrationDTO>(courseRegistration);
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.Mappers.StudentAccommodationRequests
{
	// Token: 0x0200005D RID: 93
	public static class CourseRequestBaseMapper
	{
		// Token: 0x0600017C RID: 380 RVA: 0x0000A228 File Offset: 0x00008428
		static CourseRequestBaseMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<CourseRequestBaseDTO, CourseRequestBase>().ForMember((CourseRequestBase pb) => pb.WhoEntered, delegate(IMemberConfigurationExpression<CourseRequestBaseDTO> m)
			{
				m.MapFrom<PersonBase>((CourseRequestBaseDTO pbdto) => (pbdto.WhoEntered == null) ? null : pbdto.WhoEntered.ToDomainObject());
			}).ForMember((CourseRequestBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CourseRequestBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<CourseRequestBase, CourseRequestBaseDTO>().ForMember((CourseRequestBaseDTO pb) => pb.WhoEntered, delegate(IMemberConfigurationExpression<CourseRequestBase> m)
			{
				m.MapFrom<PersonBaseDTO>((CourseRequestBase pbdto) => (pbdto.WhoEntered == null) ? null : pbdto.WhoEntered.ToDTO());
			});
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000A348 File Offset: 0x00008548
		public static CourseRequestBase ToDomainObject(this CourseRequestBaseDTO dto)
		{
			return Mapper.Map<CourseRequestBaseDTO, CourseRequestBase>(dto);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000A360 File Offset: 0x00008560
		public static CourseRequestBaseDTO ToDTO(this CourseRequestBase item)
		{
			return Mapper.Map<CourseRequestBase, CourseRequestBaseDTO>(item);
		}
	}
}

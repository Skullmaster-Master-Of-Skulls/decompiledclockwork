using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.Mappers.StudentAccommodationRequests
{
	// Token: 0x0200005E RID: 94
	public static class StudentCourseAccommodationModificationRequestItemMapper
	{
		// Token: 0x06000180 RID: 384 RVA: 0x0000A378 File Offset: 0x00008578
		static StudentCourseAccommodationModificationRequestItemMapper()
		{
			DynamicDataMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<StudentCourseAccommodationModificationRequestItemDTO, StudentCourseAccommodationModificationRequestItem>().ForMember((StudentCourseAccommodationModificationRequestItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentCourseAccommodationModificationRequestItemDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StudentCourseAccommodationModificationRequestItem, StudentCourseAccommodationModificationRequestItemDTO>();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000A400 File Offset: 0x00008600
		public static StudentCourseAccommodationModificationRequestItem ToDomainObject(this StudentCourseAccommodationModificationRequestItemDTO dto)
		{
			return Mapper.Map<StudentCourseAccommodationModificationRequestItemDTO, StudentCourseAccommodationModificationRequestItem>(dto);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000A418 File Offset: 0x00008618
		public static StudentCourseAccommodationModificationRequestItemDTO ToDTO(this StudentCourseAccommodationModificationRequestItem item)
		{
			return Mapper.Map<StudentCourseAccommodationModificationRequestItem, StudentCourseAccommodationModificationRequestItemDTO>(item);
		}
	}
}

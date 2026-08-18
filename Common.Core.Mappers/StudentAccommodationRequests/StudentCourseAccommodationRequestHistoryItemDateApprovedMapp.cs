using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.Mappers.StudentAccommodationRequests
{
	// Token: 0x0200005F RID: 95
	public static class StudentCourseAccommodationRequestHistoryItemDateApprovedMapper
	{
		// Token: 0x06000184 RID: 388 RVA: 0x0000A430 File Offset: 0x00008630
		static StudentCourseAccommodationRequestHistoryItemDateApprovedMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<StudentCourseAccommodationRequestHistoryItemDateApprovedDTO, StudentCourseAccommodationRequestHistoryItemDateApproved>().ForMember((StudentCourseAccommodationRequestHistoryItemDateApproved pb) => pb.WhoApproved, delegate(IMemberConfigurationExpression<StudentCourseAccommodationRequestHistoryItemDateApprovedDTO> m)
			{
				m.MapFrom<PersonBase>((StudentCourseAccommodationRequestHistoryItemDateApprovedDTO pbdto) => (pbdto.WhoApproved == null) ? null : pbdto.WhoApproved.ToDomainObject());
			});
			Mapper.CreateMap<StudentCourseAccommodationRequestHistoryItemDateApproved, StudentCourseAccommodationRequestHistoryItemDateApprovedDTO>().ForMember((StudentCourseAccommodationRequestHistoryItemDateApprovedDTO pb) => pb.WhoApproved, delegate(IMemberConfigurationExpression<StudentCourseAccommodationRequestHistoryItemDateApproved> m)
			{
				m.MapFrom<PersonBaseDTO>((StudentCourseAccommodationRequestHistoryItemDateApproved pbdto) => (pbdto.WhoApproved == null) ? null : pbdto.WhoApproved.ToDTO());
			});
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000A4EC File Offset: 0x000086EC
		public static StudentCourseAccommodationRequestHistoryItemDateApproved ToDomainObject(this StudentCourseAccommodationRequestHistoryItemDateApprovedDTO dto)
		{
			return Mapper.Map<StudentCourseAccommodationRequestHistoryItemDateApprovedDTO, StudentCourseAccommodationRequestHistoryItemDateApproved>(dto);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000A504 File Offset: 0x00008704
		public static StudentCourseAccommodationRequestHistoryItemDateApprovedDTO ToDTO(this StudentCourseAccommodationRequestHistoryItemDateApproved item)
		{
			return Mapper.Map<StudentCourseAccommodationRequestHistoryItemDateApproved, StudentCourseAccommodationRequestHistoryItemDateApprovedDTO>(item);
		}
	}
}

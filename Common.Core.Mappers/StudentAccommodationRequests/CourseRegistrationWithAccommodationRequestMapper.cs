using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Core.Mappers.CourseRegistrations;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.Mappers.StudentAccommodationRequests
{
	// Token: 0x0200005C RID: 92
	public static class CourseRegistrationWithAccommodationRequestMapper
	{
		// Token: 0x06000178 RID: 376 RVA: 0x0000A1DC File Offset: 0x000083DC
		static CourseRegistrationWithAccommodationRequestMapper()
		{
			StudentCourseAccommodationRequestMapper.CreateMap();
			CourseRegistrationWithAccommodationsMapper.CreateMap();
			Mapper.CreateMap<CourseRegistrationWithAccommodationRequestDTO, CourseRegistrationWithAccommodationRequest>();
			Mapper.CreateMap<CourseRegistrationWithAccommodationRequest, CourseRegistrationWithAccommodationRequestDTO>();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public static CourseRegistrationWithAccommodationRequest ToDomainObject(this CourseRegistrationWithAccommodationRequestDTO dto)
		{
			return Mapper.Map<CourseRegistrationWithAccommodationRequestDTO, CourseRegistrationWithAccommodationRequest>(dto);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000A210 File Offset: 0x00008410
		public static CourseRegistrationWithAccommodationRequestDTO ToDTO(this CourseRegistrationWithAccommodationRequest item)
		{
			return Mapper.Map<CourseRegistrationWithAccommodationRequest, CourseRegistrationWithAccommodationRequestDTO>(item);
		}
	}
}

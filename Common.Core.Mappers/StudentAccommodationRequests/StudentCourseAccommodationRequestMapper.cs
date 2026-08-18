using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.Mappers.StudentAccommodationRequests
{
	// Token: 0x02000062 RID: 98
	public static class StudentCourseAccommodationRequestMapper
	{
		// Token: 0x06000190 RID: 400 RVA: 0x0000A868 File Offset: 0x00008A68
		static StudentCourseAccommodationRequestMapper()
		{
			LookupCourseBaseWithPrimaryInstructorMapper.CreateMap();
			StudentCourseAccommodationModificationRequestItemMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<StudentCourseAccommodationRequestDTO, StudentCourseAccommodationRequest>().ForMember((StudentCourseAccommodationRequest pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentCourseAccommodationRequestDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StudentCourseAccommodationRequest, StudentCourseAccommodationRequestDTO>();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000A8FC File Offset: 0x00008AFC
		public static StudentCourseAccommodationRequest ToDomainObject(this StudentCourseAccommodationRequestDTO dto)
		{
			return Mapper.Map<StudentCourseAccommodationRequestDTO, StudentCourseAccommodationRequest>(dto);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000A914 File Offset: 0x00008B14
		public static StudentCourseAccommodationRequestDTO ToDTO(this StudentCourseAccommodationRequest item)
		{
			return Mapper.Map<StudentCourseAccommodationRequest, StudentCourseAccommodationRequestDTO>(item);
		}
	}
}

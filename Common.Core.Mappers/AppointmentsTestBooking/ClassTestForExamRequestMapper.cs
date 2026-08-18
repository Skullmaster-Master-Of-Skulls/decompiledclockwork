using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001C1 RID: 449
	public static class ClassTestForExamRequestMapper
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x0002139C File Offset: 0x0001F59C
		static ClassTestForExamRequestMapper()
		{
			ClassTestBaseMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<ClassTestForExamRequestDTO, ClassTestForExamRequest>().ForMember((ClassTestForExamRequest pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ClassTestForExamRequestDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ClassTestForExamRequest, ClassTestForExamRequestDTO>();
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00021424 File Offset: 0x0001F624
		public static ClassTestForExamRequest ToDomainObject(this ClassTestForExamRequestDTO ClassTestForExamRequestDTO)
		{
			return Mapper.Map<ClassTestForExamRequestDTO, ClassTestForExamRequest>(ClassTestForExamRequestDTO);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0002143C File Offset: 0x0001F63C
		public static ClassTestForExamRequestDTO ToDTO(this ClassTestForExamRequest classTestForExamRequest)
		{
			return Mapper.Map<ClassTestForExamRequest, ClassTestForExamRequestDTO>(classTestForExamRequest);
		}
	}
}

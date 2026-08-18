using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001C8 RID: 456
	public static class StudentClassTestBaseMapper
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x00021A1C File Offset: 0x0001FC1C
		static StudentClassTestBaseMapper()
		{
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<StudentClassTestBaseDTO, StudentClassTestBase>().ForMember((StudentClassTestBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentClassTestBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StudentClassTestBase, StudentClassTestBaseDTO>();
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00021AA0 File Offset: 0x0001FCA0
		public static StudentClassTestBase ToDomainObject(this StudentClassTestBaseDTO studentClassTestDTO)
		{
			return Mapper.Map<StudentClassTestBaseDTO, StudentClassTestBase>(studentClassTestDTO);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00021AB8 File Offset: 0x0001FCB8
		public static StudentClassTestBaseDTO ToDTO(this StudentClassTestBase studentClassTest)
		{
			return Mapper.Map<StudentClassTestBase, StudentClassTestBaseDTO>(studentClassTest);
		}
	}
}

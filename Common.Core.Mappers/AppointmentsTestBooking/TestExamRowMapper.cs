using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestExamBrowser;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001CB RID: 459
	public static class TestExamRowMapper
	{
		// Token: 0x060007CD RID: 1997 RVA: 0x00021D14 File Offset: 0x0001FF14
		static TestExamRowMapper()
		{
			Mapper.CreateMap<TestExamRowDTO, TestExamRow>().ForMember((TestExamRow pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TestExamRowDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TestExamRow, TestExamRowDTO>();
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00021D90 File Offset: 0x0001FF90
		public static TestExamRow ToDomainObject(this TestExamRowDTO accommodationForTestDTO)
		{
			return Mapper.Map<TestExamRowDTO, TestExamRow>(accommodationForTestDTO);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00021DA8 File Offset: 0x0001FFA8
		public static TestExamRowDTO ToDTO(this TestExamRow accommodationForTest)
		{
			return Mapper.Map<TestExamRow, TestExamRowDTO>(accommodationForTest);
		}
	}
}

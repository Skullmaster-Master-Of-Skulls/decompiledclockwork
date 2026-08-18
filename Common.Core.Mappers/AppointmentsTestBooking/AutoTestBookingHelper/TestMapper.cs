using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001F5 RID: 501
	public static class TestMapper
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x00024678 File Offset: 0x00022878
		static TestMapper()
		{
			RoomMapper.CreateMap();
			Mapper.CreateMap<TestDTO, Test>();
			Mapper.CreateMap<Test, TestDTO>();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00024690 File Offset: 0x00022890
		public static Test ToDomainObject(this TestDTO accommodationForTestDTO)
		{
			return Mapper.Map<TestDTO, Test>(accommodationForTestDTO);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000246A8 File Offset: 0x000228A8
		public static TestDTO ToDTO(this Test accommodationForTest)
		{
			return Mapper.Map<Test, TestDTO>(accommodationForTest);
		}
	}
}

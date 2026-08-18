using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.FullTest
{
	// Token: 0x020001D8 RID: 472
	public static class TestForEditBookingSpecificMapper
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x0002263C File Offset: 0x0002083C
		static TestForEditBookingSpecificMapper()
		{
			DynamicDataMapper.CreateMap();
			Mapper.CreateMap<TestForEditBookingSpecificDTO, TestForEditBookingSpecific>();
			Mapper.CreateMap<TestForEditBookingSpecific, TestForEditBookingSpecificDTO>();
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00022654 File Offset: 0x00020854
		public static TestForEditBookingSpecific ToDomainObject(this TestForEditBookingSpecificDTO dto)
		{
			return Mapper.Map<TestForEditBookingSpecificDTO, TestForEditBookingSpecific>(dto);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0002266C File Offset: 0x0002086C
		public static TestForEditBookingSpecificDTO ToDTO(this TestForEditBookingSpecific item)
		{
			return Mapper.Map<TestForEditBookingSpecific, TestForEditBookingSpecificDTO>(item);
		}
	}
}

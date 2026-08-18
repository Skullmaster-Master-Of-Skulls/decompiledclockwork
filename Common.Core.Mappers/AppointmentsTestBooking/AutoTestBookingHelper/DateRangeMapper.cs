using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001EC RID: 492
	public static class DateRangeMapper
	{
		// Token: 0x06000853 RID: 2131 RVA: 0x00023814 File Offset: 0x00021A14
		static DateRangeMapper()
		{
			Mapper.CreateMap<DateRangeDTO, DateRange>();
			Mapper.CreateMap<DateRange, DateRangeDTO>();
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00023824 File Offset: 0x00021A24
		public static DateRange ToDomainObject(this DateRangeDTO accommodationForTestDTO)
		{
			return Mapper.Map<DateRangeDTO, DateRange>(accommodationForTestDTO);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002383C File Offset: 0x00021A3C
		public static DateRangeDTO ToDTO(this DateRange accommodationForTest)
		{
			return Mapper.Map<DateRange, DateRangeDTO>(accommodationForTest);
		}
	}
}

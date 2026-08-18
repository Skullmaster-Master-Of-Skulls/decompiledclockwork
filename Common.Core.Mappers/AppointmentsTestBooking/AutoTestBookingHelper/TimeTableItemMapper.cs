using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001F7 RID: 503
	public static class TimeTableItemMapper
	{
		// Token: 0x0600087F RID: 2175 RVA: 0x000247A8 File Offset: 0x000229A8
		static TimeTableItemMapper()
		{
			Mapper.CreateMap<TimeTableItemDTO, TimeTableItem>();
			Mapper.CreateMap<TimeTableItem, TimeTableItemDTO>();
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x000247B8 File Offset: 0x000229B8
		public static TimeTableItem ToDomainObject(this TimeTableItemDTO accommodationForTestDTO)
		{
			return Mapper.Map<TimeTableItemDTO, TimeTableItem>(accommodationForTestDTO);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000247D0 File Offset: 0x000229D0
		public static TimeTableItemDTO ToDTO(this TimeTableItem accommodationForTest)
		{
			return Mapper.Map<TimeTableItem, TimeTableItemDTO>(accommodationForTest);
		}
	}
}

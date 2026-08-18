using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001EA RID: 490
	public static class BookingResultsMapper
	{
		// Token: 0x0600084B RID: 2123 RVA: 0x00023794 File Offset: 0x00021994
		static BookingResultsMapper()
		{
			Mapper.CreateMap<BookingResultsDTO, BookingResults>();
			Mapper.CreateMap<BookingResults, BookingResultsDTO>();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000237A4 File Offset: 0x000219A4
		public static BookingResults ToDomainObject(this BookingResultsDTO accommodationForTestDTO)
		{
			return Mapper.Map<BookingResultsDTO, BookingResults>(accommodationForTestDTO);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000237BC File Offset: 0x000219BC
		public static BookingResultsDTO ToDTO(this BookingResults accommodationForTest)
		{
			return Mapper.Map<BookingResults, BookingResultsDTO>(accommodationForTest);
		}
	}
}

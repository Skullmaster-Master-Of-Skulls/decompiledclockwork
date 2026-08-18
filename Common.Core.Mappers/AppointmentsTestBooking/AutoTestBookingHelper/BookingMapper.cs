using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001E9 RID: 489
	public static class BookingMapper
	{
		// Token: 0x06000847 RID: 2119 RVA: 0x00023754 File Offset: 0x00021954
		static BookingMapper()
		{
			Mapper.CreateMap<BookingDTO, Booking>();
			Mapper.CreateMap<Booking, BookingDTO>();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00023764 File Offset: 0x00021964
		public static Booking ToDomainObject(this BookingDTO accommodationForTestDTO)
		{
			return Mapper.Map<BookingDTO, Booking>(accommodationForTestDTO);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002377C File Offset: 0x0002197C
		public static BookingDTO ToDTO(this Booking accommodationForTest)
		{
			return Mapper.Map<Booking, BookingDTO>(accommodationForTest);
		}
	}
}

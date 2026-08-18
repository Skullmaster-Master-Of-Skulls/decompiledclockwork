using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001E1 RID: 481
	public static class AccommodationBasicMapper
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x00022E08 File Offset: 0x00021008
		static AccommodationBasicMapper()
		{
			Mapper.CreateMap<AccommodationBasicDTO, AccommodationBasic>();
			Mapper.CreateMap<AccommodationBasic, AccommodationBasicDTO>();
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00022E18 File Offset: 0x00021018
		public static AccommodationBasic ToDomainObject(this AccommodationBasicDTO accommodationForTestDTO)
		{
			return Mapper.Map<AccommodationBasicDTO, AccommodationBasic>(accommodationForTestDTO);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00022E30 File Offset: 0x00021030
		public static AccommodationBasicDTO ToDTO(this AccommodationBasic accommodationForTest)
		{
			return Mapper.Map<AccommodationBasic, AccommodationBasicDTO>(accommodationForTest);
		}
	}
}

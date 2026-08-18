using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001E2 RID: 482
	public static class AccommodationMapper
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x00022E48 File Offset: 0x00021048
		static AccommodationMapper()
		{
			Mapper.CreateMap<AccommodationDTO, Accommodation>();
			Mapper.CreateMap<Accommodation, AccommodationDTO>();
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00022E58 File Offset: 0x00021058
		public static Accommodation ToDomainObject(this AccommodationDTO accommodationForTestDTO)
		{
			return Mapper.Map<AccommodationDTO, Accommodation>(accommodationForTestDTO);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00022E70 File Offset: 0x00021070
		public static AccommodationDTO ToDTO(this Accommodation accommodationForTest)
		{
			return Mapper.Map<Accommodation, AccommodationDTO>(accommodationForTest);
		}
	}
}

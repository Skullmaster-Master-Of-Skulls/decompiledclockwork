using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001BB RID: 443
	public static class AccommodationForTestMapper
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00020CE4 File Offset: 0x0001EEE4
		static AccommodationForTestMapper()
		{
			DynamicDataMapper.CreateMap();
			Mapper.CreateMap<AccommodationForTestDTO, AccommodationForTest>();
			Mapper.CreateMap<AccommodationForTest, AccommodationForTestDTO>();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00020CFC File Offset: 0x0001EEFC
		public static AccommodationForTest ToDomainObject(this AccommodationForTestDTO accommodationForTestDTO)
		{
			return Mapper.Map<AccommodationForTestDTO, AccommodationForTest>(accommodationForTestDTO);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00020D14 File Offset: 0x0001EF14
		public static AccommodationForTestDTO ToDTO(this AccommodationForTest accommodationForTest)
		{
			return Mapper.Map<AccommodationForTest, AccommodationForTestDTO>(accommodationForTest);
		}
	}
}

using System;
using AutoMapper;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.Booker2
{
	// Token: 0x020001DB RID: 475
	public static class TryToBookAccommodationToUseMapper
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x00022964 File Offset: 0x00020B64
		static TryToBookAccommodationToUseMapper()
		{
			Mapper.CreateMap<TryToBookAccommodationToUseDTO, TryToBookAccommodationToUse>();
			Mapper.CreateMap<TryToBookAccommodationToUse, TryToBookAccommodationToUseDTO>();
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00022974 File Offset: 0x00020B74
		public static TryToBookAccommodationToUse ToDomainObject(this TryToBookAccommodationToUseDTO accommodationForTestDTO)
		{
			return Mapper.Map<TryToBookAccommodationToUseDTO, TryToBookAccommodationToUse>(accommodationForTestDTO);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0002298C File Offset: 0x00020B8C
		public static TryToBookAccommodationToUseDTO ToDTO(this TryToBookAccommodationToUse accommodationForTest)
		{
			return Mapper.Map<TryToBookAccommodationToUse, TryToBookAccommodationToUseDTO>(accommodationForTest);
		}
	}
}

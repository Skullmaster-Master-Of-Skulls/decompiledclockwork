using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001F4 RID: 500
	public static class SpecialAccommodationMapper
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x00024638 File Offset: 0x00022838
		static SpecialAccommodationMapper()
		{
			Mapper.CreateMap<SpecialAccommodationDTO, SpecialAccommodation>();
			Mapper.CreateMap<SpecialAccommodation, SpecialAccommodationDTO>();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00024648 File Offset: 0x00022848
		public static SpecialAccommodation ToDomainObject(this SpecialAccommodationDTO accommodationForTestDTO)
		{
			return Mapper.Map<SpecialAccommodationDTO, SpecialAccommodation>(accommodationForTestDTO);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00024660 File Offset: 0x00022860
		public static SpecialAccommodationDTO ToDTO(this SpecialAccommodation accommodationForTest)
		{
			return Mapper.Map<SpecialAccommodation, SpecialAccommodationDTO>(accommodationForTest);
		}
	}
}

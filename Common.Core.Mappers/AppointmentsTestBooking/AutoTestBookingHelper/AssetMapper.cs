using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001E5 RID: 485
	public static class AssetMapper
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00023204 File Offset: 0x00021404
		static AssetMapper()
		{
			AccommodationMapper.CreateMap();
			Mapper.CreateMap<AssetDTO, Asset>().ForMember((Asset pb) => pb.AccommodationsSupported, delegate(IMemberConfigurationExpression<AssetDTO> m)
			{
				m.MapFrom<List<Accommodation>>((AssetDTO pbdto) => (pbdto.AccommodationsSupported == null) ? null : pbdto.AccommodationsSupported.ToList<AccommodationDTO>().ConvertAll<Accommodation>((AccommodationDTO g) => g.ToDomainObject()));
			}).ForMember((Asset pb) => pb.Id, delegate(IMemberConfigurationExpression<AssetDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<Asset, AssetDTO>().ForMember((AssetDTO pb) => pb.AccommodationsSupported, delegate(IMemberConfigurationExpression<Asset> m)
			{
				m.MapFrom<List<AccommodationDTO>>((Asset pbdto) => (pbdto.AccommodationsSupported == null) ? null : pbdto.AccommodationsSupported.ToList<Accommodation>().ConvertAll<AccommodationDTO>((Accommodation g) => g.ToDTO()));
			});
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00023314 File Offset: 0x00021514
		public static Asset ToDomainObject(this AssetDTO accommodationForTestDTO)
		{
			return Mapper.Map<AssetDTO, Asset>(accommodationForTestDTO);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0002332C File Offset: 0x0002152C
		public static AssetDTO ToDTO(this Asset accommodationForTest)
		{
			return Mapper.Map<Asset, AssetDTO>(accommodationForTest);
		}
	}
}

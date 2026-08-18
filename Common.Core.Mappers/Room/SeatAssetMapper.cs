using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.Public.Entities.Room;

namespace TechnoPro.Common.Core.Mappers.Room
{
	// Token: 0x02000082 RID: 130
	public static class SeatAssetMapper
	{
		// Token: 0x06000234 RID: 564 RVA: 0x0000CC14 File Offset: 0x0000AE14
		static SeatAssetMapper()
		{
			SeatAssetAccommodationMapper.CreateMap();
			Mapper.CreateMap<SeatAssetDTO, SeatAsset>().ForMember((SeatAsset pb) => pb.Id, delegate(IMemberConfigurationExpression<SeatAssetDTO> m)
			{
				m.Ignore();
			}).ForMember((SeatAsset pb) => pb.AccommodationsBehind, delegate(IMemberConfigurationExpression<SeatAssetDTO> m)
			{
				m.MapFrom<List<SeatAssetAccommodation>>((SeatAssetDTO pbdto) => (pbdto.AccommodationsBehind == null) ? null : (from g in pbdto.AccommodationsBehind
				select g.ToDomainObject()).ToList<SeatAssetAccommodation>());
			});
			Mapper.CreateMap<SeatAsset, SeatAssetDTO>().ForMember((SeatAssetDTO pb) => pb.AccommodationsBehind, delegate(IMemberConfigurationExpression<SeatAsset> m)
			{
				m.MapFrom<List<SeatAssetAccommodationDTO>>((SeatAsset pbdto) => (pbdto.AccommodationsBehind == null) ? null : (from g in pbdto.AccommodationsBehind
				select g.ToDTO()).ToList<SeatAssetAccommodationDTO>());
			});
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000CD24 File Offset: 0x0000AF24
		public static SeatAsset ToDomainObject(this SeatAssetDTO dto)
		{
			return Mapper.Map<SeatAssetDTO, SeatAsset>(dto);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		public static SeatAssetDTO ToDTO(this SeatAsset item)
		{
			return Mapper.Map<SeatAsset, SeatAssetDTO>(item);
		}
	}
}

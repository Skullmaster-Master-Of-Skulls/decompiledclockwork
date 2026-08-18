using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.Room;

namespace TechnoPro.Common.Core.Mappers.Room
{
	// Token: 0x02000083 RID: 131
	public static class SeatCollectionMapper
	{
		// Token: 0x06000238 RID: 568 RVA: 0x0000CD54 File Offset: 0x0000AF54
		static SeatCollectionMapper()
		{
			AppointmentRoomMapper.CreateMap();
			SeatAssetMapper.CreateMap();
			SeatAssetAccommodationMapper.CreateMap();
			SeatMapper.CreateMap();
			Mapper.CreateMap<SeatCollectionDTO, SeatCollection>().ForMember((SeatCollection pb) => pb.AllAssets, delegate(IMemberConfigurationExpression<SeatCollectionDTO> m)
			{
				m.MapFrom<List<SeatAsset>>((SeatCollectionDTO pbdto) => (pbdto.AllAssets == null) ? null : (from g in pbdto.AllAssets
				select g.ToDomainObject()).ToList<SeatAsset>());
			}).ForMember((SeatCollection pb) => pb.AllSeatGroups, delegate(IMemberConfigurationExpression<SeatCollectionDTO> m)
			{
				m.MapFrom<List<SeatGroup>>((SeatCollectionDTO pbdto) => (pbdto.AllSeatGroups == null) ? null : (from g in pbdto.AllSeatGroups
				select g.ToDomainObject()).ToList<SeatGroup>());
			}).ForMember((SeatCollection pb) => pb.Seats, delegate(IMemberConfigurationExpression<SeatCollectionDTO> m)
			{
				m.MapFrom<List<Seat>>((SeatCollectionDTO pbdto) => (pbdto.Seats == null) ? null : (from g in pbdto.Seats
				select g.ToDomainObject()).ToList<Seat>());
			});
			Mapper.CreateMap<SeatCollection, SeatCollectionDTO>().ForMember((SeatCollectionDTO pb) => pb.AllAssets, delegate(IMemberConfigurationExpression<SeatCollection> m)
			{
				m.MapFrom<List<SeatAssetDTO>>((SeatCollection pbdto) => (pbdto.AllAssets == null) ? null : (from g in pbdto.AllAssets
				select g.ToDTO()).ToList<SeatAssetDTO>());
			}).ForMember((SeatCollectionDTO pb) => pb.AllSeatGroups, delegate(IMemberConfigurationExpression<SeatCollection> m)
			{
				m.MapFrom<List<SeatGroupDTO>>((SeatCollection pbdto) => (pbdto.AllSeatGroups == null) ? null : (from g in pbdto.AllSeatGroups
				select g.ToDTO()).ToList<SeatGroupDTO>());
			}).ForMember((SeatCollectionDTO pb) => pb.Seats, delegate(IMemberConfigurationExpression<SeatCollection> m)
			{
				m.MapFrom<List<SeatDTO>>((SeatCollection pbdto) => (pbdto.Seats == null) ? null : (from g in pbdto.Seats
				select g.ToDTO()).ToList<SeatDTO>());
			});
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000CF5C File Offset: 0x0000B15C
		public static SeatCollection ToDomainObject(this SeatCollectionDTO dto)
		{
			return Mapper.Map<SeatCollectionDTO, SeatCollection>(dto);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000CF74 File Offset: 0x0000B174
		public static SeatCollectionDTO ToDTO(this SeatCollection item)
		{
			return Mapper.Map<SeatCollection, SeatCollectionDTO>(item);
		}
	}
}

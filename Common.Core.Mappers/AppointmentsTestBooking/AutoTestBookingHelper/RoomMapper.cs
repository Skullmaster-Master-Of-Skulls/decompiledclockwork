using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001F3 RID: 499
	public static class RoomMapper
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x0002440C File Offset: 0x0002260C
		static RoomMapper()
		{
			AccommodationMapper.CreateMap();
			AssetMapper.CreateMap();
			Mapper.CreateMap<RoomDTO, Room>().ForMember((Room pb) => pb.GivePriorityToStudentsWithTheseAccommodations, delegate(IMemberConfigurationExpression<RoomDTO> m)
			{
				m.MapFrom<List<Accommodation>>((RoomDTO pbdto) => (pbdto.GivePriorityToStudentsWithTheseAccommodations == null) ? null : pbdto.GivePriorityToStudentsWithTheseAccommodations.ToList<AccommodationDTO>().ConvertAll<Accommodation>((AccommodationDTO g) => g.ToDomainObject()));
			}).ForMember((Room pb) => pb.Assets, delegate(IMemberConfigurationExpression<RoomDTO> m)
			{
				m.MapFrom<List<Asset>>((RoomDTO pbdto) => (pbdto.Assets == null) ? null : pbdto.Assets.ToList<AssetDTO>().ConvertAll<Asset>((AssetDTO g) => g.ToDomainObject()));
			}).ForMember((Room pb) => pb.Campuses, delegate(IMemberConfigurationExpression<RoomDTO> m)
			{
				m.MapFrom<List<string>>((RoomDTO pbdto) => (pbdto.Campuses == null) ? null : pbdto.Campuses.ToList<string>());
			});
			Mapper.CreateMap<Room, RoomDTO>().ForMember((RoomDTO pb) => pb.GivePriorityToStudentsWithTheseAccommodations, delegate(IMemberConfigurationExpression<Room> m)
			{
				m.MapFrom<List<AccommodationDTO>>((Room pbdto) => (pbdto.GivePriorityToStudentsWithTheseAccommodations == null) ? null : pbdto.GivePriorityToStudentsWithTheseAccommodations.ToList<Accommodation>().ConvertAll<AccommodationDTO>((Accommodation g) => g.ToDTO()));
			}).ForMember((RoomDTO pb) => pb.Assets, delegate(IMemberConfigurationExpression<Room> m)
			{
				m.MapFrom<List<AssetDTO>>((Room pbdto) => (pbdto.Assets == null) ? null : pbdto.Assets.ToList<Asset>().ConvertAll<AssetDTO>((Asset g) => g.ToDTO()));
			}).ForMember((RoomDTO pb) => pb.Campuses, delegate(IMemberConfigurationExpression<Room> m)
			{
				m.MapFrom<List<string>>((Room pbdto) => (pbdto.Campuses == null) ? null : pbdto.Campuses.ToList<string>());
			});
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00024608 File Offset: 0x00022808
		public static Room ToDomainObject(this RoomDTO accommodationForTestDTO)
		{
			return Mapper.Map<RoomDTO, Room>(accommodationForTestDTO);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00024620 File Offset: 0x00022820
		public static RoomDTO ToDTO(this Room accommodationForTest)
		{
			return Mapper.Map<Room, RoomDTO>(accommodationForTest);
		}
	}
}

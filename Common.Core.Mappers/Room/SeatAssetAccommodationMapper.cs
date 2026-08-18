using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.Public.Entities.Room;

namespace TechnoPro.Common.Core.Mappers.Room
{
	// Token: 0x02000081 RID: 129
	public static class SeatAssetAccommodationMapper
	{
		// Token: 0x06000230 RID: 560 RVA: 0x0000CA68 File Offset: 0x0000AC68
		static SeatAssetAccommodationMapper()
		{
			CampusMapper.CreateMap();
			Mapper.CreateMap<SeatAssetAccommodationDTO, SeatAssetAccommodation>().ForMember((SeatAssetAccommodation pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SeatAssetAccommodationDTO> m)
			{
				m.Ignore();
			}).ForMember((SeatAssetAccommodation pb) => pb.LookupText, delegate(IMemberConfigurationExpression<SeatAssetAccommodationDTO> m)
			{
				m.Ignore();
			}).ForMember((SeatAssetAccommodation pb) => pb.SubText, delegate(IMemberConfigurationExpression<SeatAssetAccommodationDTO> m)
			{
				m.Ignore();
			}).ForMember((SeatAssetAccommodation pb) => (object)pb.Level, delegate(IMemberConfigurationExpression<SeatAssetAccommodationDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<SeatAssetAccommodation, SeatAssetAccommodationDTO>();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		public static SeatAssetAccommodation ToDomainObject(this SeatAssetAccommodationDTO dto)
		{
			return Mapper.Map<SeatAssetAccommodationDTO, SeatAssetAccommodation>(dto);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		public static SeatAssetAccommodationDTO ToDTO(this SeatAssetAccommodation item)
		{
			return Mapper.Map<SeatAssetAccommodation, SeatAssetAccommodationDTO>(item);
		}
	}
}

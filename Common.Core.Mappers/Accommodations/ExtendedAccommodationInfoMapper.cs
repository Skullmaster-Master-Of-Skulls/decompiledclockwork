using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.Common.Public.Entities.Accommodations;

namespace TechnoPro.Common.Core.Mappers.Accommodations
{
	// Token: 0x0200022F RID: 559
	public static class ExtendedAccommodationInfoMapper
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0002B7B0 File Offset: 0x000299B0
		static ExtendedAccommodationInfoMapper()
		{
			Mapper.CreateMap<ExtendedAccommodationInfoDTO, ExtendedAccommodationInfo>().ForMember((ExtendedAccommodationInfo pb) => (object)pb.AccommodationType, delegate(IMemberConfigurationExpression<ExtendedAccommodationInfoDTO> m)
			{
				m.MapFrom<eAccommodationTypeDTO>((ExtendedAccommodationInfoDTO pbdto) => (eAccommodationTypeDTO)pbdto.AccommodationType);
			}).ForMember((ExtendedAccommodationInfo pb) => (object)pb.Group, delegate(IMemberConfigurationExpression<ExtendedAccommodationInfoDTO> m)
			{
				m.MapFrom<eAccommodationGroupDTO>((ExtendedAccommodationInfoDTO pbdto) => (eAccommodationGroupDTO)pbdto.Group);
			});
			Mapper.CreateMap<ExtendedAccommodationInfo, ExtendedAccommodationInfoDTO>().ForMember((ExtendedAccommodationInfoDTO pb) => (object)pb.AccommodationType, delegate(IMemberConfigurationExpression<ExtendedAccommodationInfo> m)
			{
				m.MapFrom<eAccommodationType>((ExtendedAccommodationInfo pbdto) => (eAccommodationType)pbdto.AccommodationType);
			}).ForMember((ExtendedAccommodationInfoDTO pb) => (object)pb.Group, delegate(IMemberConfigurationExpression<ExtendedAccommodationInfo> m)
			{
				m.MapFrom<eAccommodationGroup>((ExtendedAccommodationInfo pbdto) => (eAccommodationGroup)pbdto.Group);
			});
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002B940 File Offset: 0x00029B40
		public static ExtendedAccommodationInfo ToDomainObject(this ExtendedAccommodationInfoDTO dto)
		{
			return Mapper.Map<ExtendedAccommodationInfoDTO, ExtendedAccommodationInfo>(dto);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002B958 File Offset: 0x00029B58
		public static ExtendedAccommodationInfoDTO ToDTO(this ExtendedAccommodationInfo item)
		{
			return Mapper.Map<ExtendedAccommodationInfo, ExtendedAccommodationInfoDTO>(item);
		}
	}
}

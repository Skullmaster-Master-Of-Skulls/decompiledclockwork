using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.Accommodations
{
	// Token: 0x0200022E RID: 558
	public static class AccommodationDataMapper
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x0002B590 File Offset: 0x00029790
		static AccommodationDataMapper()
		{
			DynamicDataMapper.CreateMap();
			ExtendedAccommodationInfoMapper.CreateMap();
			Mapper.CreateMap<AccommodationDataDTO, AccommodationData>().ForMember((AccommodationData pb) => pb.Data, delegate(IMemberConfigurationExpression<AccommodationDataDTO> m)
			{
				m.MapFrom<DynamicData>((AccommodationDataDTO pbdto) => (pbdto.Data == null) ? null : pbdto.Data.ToDomainObject());
			}).ForMember((AccommodationData pb) => pb.Detail, delegate(IMemberConfigurationExpression<AccommodationDataDTO> m)
			{
				m.MapFrom<ExtendedAccommodationInfo>((AccommodationDataDTO pbdto) => (pbdto.Detail == null) ? null : pbdto.Detail.ToDomainObject());
			});
			Mapper.CreateMap<AccommodationData, AccommodationDataDTO>().ForMember((AccommodationDataDTO pb) => pb.Data, delegate(IMemberConfigurationExpression<AccommodationData> m)
			{
				m.MapFrom<DynamicDataDTO>((AccommodationData pbdto) => (pbdto.Data == null) ? null : pbdto.Data.ToDTO());
			}).ForMember((AccommodationDataDTO pb) => pb.Detail, delegate(IMemberConfigurationExpression<AccommodationData> m)
			{
				m.MapFrom<ExtendedAccommodationInfoDTO>((AccommodationData pbdto) => (pbdto.Detail == null) ? null : pbdto.Detail.ToDTO());
			});
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002B6F0 File Offset: 0x000298F0
		public static AccommodationData ToDomainObject(this AccommodationDataDTO dto)
		{
			return Mapper.Map<AccommodationDataDTO, AccommodationData>(dto);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002B70C File Offset: 0x0002990C
		public static AccommodationDataDTO ToDTO(this AccommodationData item)
		{
			return Mapper.Map<AccommodationData, AccommodationDataDTO>(item);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002B728 File Offset: 0x00029928
		public static IList<AccommodationData> ToDomainObject(this IList<AccommodationDataDTO> list)
		{
			IList<AccommodationData> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<AccommodationData>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002B76C File Offset: 0x0002996C
		public static IList<AccommodationDataDTO> ToDTO(this IList<AccommodationData> list)
		{
			IList<AccommodationDataDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<AccommodationDataDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

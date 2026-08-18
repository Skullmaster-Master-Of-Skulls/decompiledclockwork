using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Core.Mappers.Accommodations;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues
{
	// Token: 0x020000C8 RID: 200
	public static class MailMergeValueAccommodationDataMapper
	{
		// Token: 0x06000354 RID: 852 RVA: 0x00011128 File Offset: 0x0000F328
		static MailMergeValueAccommodationDataMapper()
		{
			MailMergeValueBaseMapper.CreateMap();
			AccommodationDataMapper.CreateMap();
			Mapper.CreateMap<MailMergeValueAccommodationDataDTO, MailMergeValueAccommodationData>().ForMember((MailMergeValueAccommodationData pb) => pb.Value, delegate(IMemberConfigurationExpression<MailMergeValueAccommodationDataDTO> m)
			{
				m.MapFrom<AccommodationData>((MailMergeValueAccommodationDataDTO pbdto) => (pbdto.Value == null) ? null : pbdto.Value.ToDomainObject());
			});
			Mapper.CreateMap<MailMergeValueAccommodationData, MailMergeValueAccommodationDataDTO>().ForMember((MailMergeValueAccommodationDataDTO pb) => pb.Value, delegate(IMemberConfigurationExpression<MailMergeValueAccommodationData> m)
			{
				m.MapFrom<AccommodationDataDTO>((MailMergeValueAccommodationData pbdto) => (pbdto.Value == null) ? null : pbdto.Value.ToDTO());
			});
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000111EC File Offset: 0x0000F3EC
		public static MailMergeValueAccommodationData ToDomainObject(this MailMergeValueAccommodationDataDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeValueAccommodationDataDTO, MailMergeValueAccommodationData>(mailMergeCodeDTO);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00011204 File Offset: 0x0000F404
		public static MailMergeValueAccommodationDataDTO ToDTO(this MailMergeValueAccommodationData mailMergeCode)
		{
			return Mapper.Map<MailMergeValueAccommodationData, MailMergeValueAccommodationDataDTO>(mailMergeCode);
		}
	}
}

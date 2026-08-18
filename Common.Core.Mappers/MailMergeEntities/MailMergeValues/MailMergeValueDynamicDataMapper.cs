using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues
{
	// Token: 0x020000CF RID: 207
	public static class MailMergeValueDynamicDataMapper
	{
		// Token: 0x06000370 RID: 880 RVA: 0x00011404 File Offset: 0x0000F604
		static MailMergeValueDynamicDataMapper()
		{
			MailMergeValueBaseMapper.CreateMap();
			DynamicDataMapper.CreateMap();
			Mapper.CreateMap<MailMergeValueDynamicDataDTO, MailMergeValueDynamicData>().ForMember((MailMergeValueDynamicData pb) => pb.Value, delegate(IMemberConfigurationExpression<MailMergeValueDynamicDataDTO> m)
			{
				m.MapFrom<DynamicData>((MailMergeValueDynamicDataDTO pbdto) => (pbdto.Value == null) ? null : pbdto.Value.ToDomainObject());
			});
			Mapper.CreateMap<MailMergeValueDynamicData, MailMergeValueDynamicDataDTO>().ForMember((MailMergeValueDynamicDataDTO pb) => pb.Value, delegate(IMemberConfigurationExpression<MailMergeValueDynamicData> m)
			{
				m.MapFrom<DynamicDataDTO>((MailMergeValueDynamicData pbdto) => (pbdto.Value == null) ? null : pbdto.Value.ToDTO());
			});
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000114C8 File Offset: 0x0000F6C8
		public static MailMergeValueDynamicData ToDomainObject(this MailMergeValueDynamicDataDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeValueDynamicDataDTO, MailMergeValueDynamicData>(mailMergeCodeDTO);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000114E0 File Offset: 0x0000F6E0
		public static MailMergeValueDynamicDataDTO ToDTO(this MailMergeValueDynamicData mailMergeCode)
		{
			return Mapper.Map<MailMergeValueDynamicData, MailMergeValueDynamicDataDTO>(mailMergeCode);
		}
	}
}

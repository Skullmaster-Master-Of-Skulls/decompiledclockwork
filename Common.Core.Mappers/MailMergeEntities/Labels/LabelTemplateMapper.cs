using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.Labels;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Labels;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.Labels
{
	// Token: 0x020000D3 RID: 211
	public static class LabelTemplateMapper
	{
		// Token: 0x06000380 RID: 896 RVA: 0x00011634 File Offset: 0x0000F834
		static LabelTemplateMapper()
		{
			MailMergeDefaultPrinterSettingsMapper.CreateMap();
			MailMergeTemplateMapper.CreateMap();
			Mapper.CreateMap<LabelTemplateDTO, LabelTemplate>();
			Mapper.CreateMap<LabelTemplate, LabelTemplateDTO>();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011650 File Offset: 0x0000F850
		public static LabelTemplate ToDomainObject(this LabelTemplateDTO labelTemplateDTO)
		{
			return Mapper.Map<LabelTemplateDTO, LabelTemplate>(labelTemplateDTO);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00011668 File Offset: 0x0000F868
		public static LabelTemplateDTO ToDTO(this LabelTemplate labelTemplate)
		{
			return Mapper.Map<LabelTemplate, LabelTemplateDTO>(labelTemplate);
		}
	}
}

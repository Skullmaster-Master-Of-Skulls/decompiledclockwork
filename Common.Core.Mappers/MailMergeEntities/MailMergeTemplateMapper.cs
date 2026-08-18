using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities
{
	// Token: 0x020000C6 RID: 198
	public static class MailMergeTemplateMapper
	{
		// Token: 0x0600034C RID: 844 RVA: 0x000110A8 File Offset: 0x0000F2A8
		static MailMergeTemplateMapper()
		{
			Mapper.CreateMap<MailMergeTemplateDTO, MailMergeTemplate>();
			Mapper.CreateMap<MailMergeTemplate, MailMergeTemplateDTO>();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000110B8 File Offset: 0x0000F2B8
		public static MailMergeTemplate ToDomainObject(this MailMergeTemplateDTO mailMergeTemplateDTO)
		{
			return Mapper.Map<MailMergeTemplateDTO, MailMergeTemplate>(mailMergeTemplateDTO);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000110D0 File Offset: 0x0000F2D0
		public static MailMergeTemplateDTO ToDTO(this MailMergeTemplate mailMergeTemplate)
		{
			return Mapper.Map<MailMergeTemplate, MailMergeTemplateDTO>(mailMergeTemplate);
		}
	}
}

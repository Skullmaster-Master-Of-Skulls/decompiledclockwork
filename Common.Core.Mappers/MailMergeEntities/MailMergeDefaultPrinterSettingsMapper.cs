using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities
{
	// Token: 0x020000C5 RID: 197
	public static class MailMergeDefaultPrinterSettingsMapper
	{
		// Token: 0x06000348 RID: 840 RVA: 0x00011068 File Offset: 0x0000F268
		static MailMergeDefaultPrinterSettingsMapper()
		{
			Mapper.CreateMap<MailMergeDefaultPrinterSettingsDTO, MailMergeDefaultPrinterSettings>();
			Mapper.CreateMap<MailMergeDefaultPrinterSettings, MailMergeDefaultPrinterSettingsDTO>();
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00011078 File Offset: 0x0000F278
		public static MailMergeDefaultPrinterSettings ToDomainObject(this MailMergeDefaultPrinterSettingsDTO mailMergeDefaultPrinterSettingsDTO)
		{
			return Mapper.Map<MailMergeDefaultPrinterSettingsDTO, MailMergeDefaultPrinterSettings>(mailMergeDefaultPrinterSettingsDTO);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00011090 File Offset: 0x0000F290
		public static MailMergeDefaultPrinterSettingsDTO ToDTO(this MailMergeDefaultPrinterSettings mailMergeDefaultPrinterSettings)
		{
			return Mapper.Map<MailMergeDefaultPrinterSettings, MailMergeDefaultPrinterSettingsDTO>(mailMergeDefaultPrinterSettings);
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities
{
	// Token: 0x020000C7 RID: 199
	public static class MailMergeValueFormatMapper
	{
		// Token: 0x06000350 RID: 848 RVA: 0x000110E8 File Offset: 0x0000F2E8
		static MailMergeValueFormatMapper()
		{
			Mapper.CreateMap<MailMergeValueFormatDTO, MailMergeValueFormat>();
			Mapper.CreateMap<MailMergeValueFormat, MailMergeValueFormatDTO>();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000110F8 File Offset: 0x0000F2F8
		public static MailMergeValueFormat ToDomainObject(this MailMergeValueFormatDTO mailMergeValueFormatDTO)
		{
			return Mapper.Map<MailMergeValueFormatDTO, MailMergeValueFormat>(mailMergeValueFormatDTO);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00011110 File Offset: 0x0000F310
		public static MailMergeValueFormatDTO ToDTO(this MailMergeValueFormat mailMergeValueFormat)
		{
			return Mapper.Map<MailMergeValueFormat, MailMergeValueFormatDTO>(mailMergeValueFormat);
		}
	}
}

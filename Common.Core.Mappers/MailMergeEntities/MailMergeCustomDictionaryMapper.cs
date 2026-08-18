using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities
{
	// Token: 0x020000C4 RID: 196
	public static class MailMergeCustomDictionaryMapper
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00011020 File Offset: 0x0000F220
		static MailMergeCustomDictionaryMapper()
		{
			MailMergeValueFormatMapper.CreateMap();
			Mapper.CreateMap<MailMergeCustomDictionaryDTO, MailMergeCustomDictionary>();
			Mapper.CreateMap<MailMergeCustomDictionary, MailMergeCustomDictionaryDTO>();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00011038 File Offset: 0x0000F238
		public static MailMergeCustomDictionary ToDomainObject(this MailMergeCustomDictionaryDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeCustomDictionaryDTO, MailMergeCustomDictionary>(mailMergeCodeDTO);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00011050 File Offset: 0x0000F250
		public static MailMergeCustomDictionaryDTO ToDTO(this MailMergeCustomDictionary mailMergeCode)
		{
			return Mapper.Map<MailMergeCustomDictionary, MailMergeCustomDictionaryDTO>(mailMergeCode);
		}
	}
}

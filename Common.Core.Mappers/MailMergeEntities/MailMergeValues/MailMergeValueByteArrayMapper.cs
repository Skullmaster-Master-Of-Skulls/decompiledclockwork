using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues
{
	// Token: 0x020000CB RID: 203
	public static class MailMergeValueByteArrayMapper
	{
		// Token: 0x06000360 RID: 864 RVA: 0x000112E4 File Offset: 0x0000F4E4
		static MailMergeValueByteArrayMapper()
		{
			MailMergeValueBaseMapper.CreateMap();
			Mapper.CreateMap<MailMergeValueByteArrayDTO, MailMergeValueByteArray>();
			Mapper.CreateMap<MailMergeValueByteArray, MailMergeValueByteArrayDTO>();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000112FC File Offset: 0x0000F4FC
		public static MailMergeValueByteArray ToDomainObject(this MailMergeValueByteArrayDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeValueByteArrayDTO, MailMergeValueByteArray>(mailMergeCodeDTO);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00011314 File Offset: 0x0000F514
		public static MailMergeValueByteArrayDTO ToDTO(this MailMergeValueByteArray mailMergeCode)
		{
			return Mapper.Map<MailMergeValueByteArray, MailMergeValueByteArrayDTO>(mailMergeCode);
		}
	}
}

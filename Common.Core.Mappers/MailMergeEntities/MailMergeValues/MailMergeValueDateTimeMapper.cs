using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues
{
	// Token: 0x020000CC RID: 204
	public static class MailMergeValueDateTimeMapper
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0001132C File Offset: 0x0000F52C
		static MailMergeValueDateTimeMapper()
		{
			MailMergeValueBaseMapper.CreateMap();
			Mapper.CreateMap<MailMergeValueDateTimeDTO, MailMergeValueDateTime>();
			Mapper.CreateMap<MailMergeValueDateTime, MailMergeValueDateTimeDTO>();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00011344 File Offset: 0x0000F544
		public static MailMergeValueDateTime ToDomainObject(this MailMergeValueDateTimeDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeValueDateTimeDTO, MailMergeValueDateTime>(mailMergeCodeDTO);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001135C File Offset: 0x0000F55C
		public static MailMergeValueDateTimeDTO ToDTO(this MailMergeValueDateTime mailMergeCode)
		{
			return Mapper.Map<MailMergeValueDateTime, MailMergeValueDateTimeDTO>(mailMergeCode);
		}
	}
}

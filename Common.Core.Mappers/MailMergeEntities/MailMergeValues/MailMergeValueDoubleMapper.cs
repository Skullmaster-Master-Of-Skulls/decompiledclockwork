using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues
{
	// Token: 0x020000CE RID: 206
	public static class MailMergeValueDoubleMapper
	{
		// Token: 0x0600036C RID: 876 RVA: 0x000113BC File Offset: 0x0000F5BC
		static MailMergeValueDoubleMapper()
		{
			MailMergeValueBaseMapper.CreateMap();
			Mapper.CreateMap<MailMergeValueDoubleDTO, MailMergeValueDouble>();
			Mapper.CreateMap<MailMergeValueDouble, MailMergeValueDoubleDTO>();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000113D4 File Offset: 0x0000F5D4
		public static MailMergeValueDouble ToDomainObject(this MailMergeValueDoubleDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeValueDoubleDTO, MailMergeValueDouble>(mailMergeCodeDTO);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000113EC File Offset: 0x0000F5EC
		public static MailMergeValueDoubleDTO ToDTO(this MailMergeValueDouble mailMergeCode)
		{
			return Mapper.Map<MailMergeValueDouble, MailMergeValueDoubleDTO>(mailMergeCode);
		}
	}
}

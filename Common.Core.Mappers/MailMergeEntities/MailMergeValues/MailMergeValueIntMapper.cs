using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues
{
	// Token: 0x020000D0 RID: 208
	public static class MailMergeValueIntMapper
	{
		// Token: 0x06000374 RID: 884 RVA: 0x000114F8 File Offset: 0x0000F6F8
		static MailMergeValueIntMapper()
		{
			MailMergeValueBaseMapper.CreateMap();
			Mapper.CreateMap<MailMergeValueIntDTO, MailMergeValueInt>();
			Mapper.CreateMap<MailMergeValueInt, MailMergeValueIntDTO>();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00011510 File Offset: 0x0000F710
		public static MailMergeValueInt ToDomainObject(this MailMergeValueIntDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeValueIntDTO, MailMergeValueInt>(mailMergeCodeDTO);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00011528 File Offset: 0x0000F728
		public static MailMergeValueIntDTO ToDTO(this MailMergeValueInt mailMergeCode)
		{
			return Mapper.Map<MailMergeValueInt, MailMergeValueIntDTO>(mailMergeCode);
		}
	}
}

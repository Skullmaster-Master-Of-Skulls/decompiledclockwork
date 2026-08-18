using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues
{
	// Token: 0x020000CA RID: 202
	public static class MailMergeValueBoolMapper
	{
		// Token: 0x0600035C RID: 860 RVA: 0x0001129C File Offset: 0x0000F49C
		static MailMergeValueBoolMapper()
		{
			MailMergeValueBaseMapper.CreateMap();
			Mapper.CreateMap<MailMergeValueBoolDTO, MailMergeValueBool>();
			Mapper.CreateMap<MailMergeValueBool, MailMergeValueBoolDTO>();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000112B4 File Offset: 0x0000F4B4
		public static MailMergeValueBool ToDomainObject(this MailMergeValueBoolDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeValueBoolDTO, MailMergeValueBool>(mailMergeCodeDTO);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000112CC File Offset: 0x0000F4CC
		public static MailMergeValueBoolDTO ToDTO(this MailMergeValueBool mailMergeCode)
		{
			return Mapper.Map<MailMergeValueBool, MailMergeValueBoolDTO>(mailMergeCode);
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues
{
	// Token: 0x020000CD RID: 205
	public static class MailMergeValueDateTimeNullableMapper
	{
		// Token: 0x06000368 RID: 872 RVA: 0x00011374 File Offset: 0x0000F574
		static MailMergeValueDateTimeNullableMapper()
		{
			MailMergeValueBaseMapper.CreateMap();
			Mapper.CreateMap<MailMergeValueDateTimeNullableDTO, MailMergeValueDateTimeNullable>();
			Mapper.CreateMap<MailMergeValueDateTimeNullable, MailMergeValueDateTimeNullableDTO>();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001138C File Offset: 0x0000F58C
		public static MailMergeValueDateTimeNullable ToDomainObject(this MailMergeValueDateTimeNullableDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeValueDateTimeNullableDTO, MailMergeValueDateTimeNullable>(mailMergeCodeDTO);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000113A4 File Offset: 0x0000F5A4
		public static MailMergeValueDateTimeNullableDTO ToDTO(this MailMergeValueDateTimeNullable mailMergeCode)
		{
			return Mapper.Map<MailMergeValueDateTimeNullable, MailMergeValueDateTimeNullableDTO>(mailMergeCode);
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues
{
	// Token: 0x020000C9 RID: 201
	public static class MailMergeValueBaseMapper
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0001121C File Offset: 0x0000F41C
		static MailMergeValueBaseMapper()
		{
			MailMergeValueAccommodationDataMapper.CreateMap();
			MailMergeValueBoolMapper.CreateMap();
			MailMergeValueByteArrayMapper.CreateMap();
			MailMergeValueDateTimeMapper.CreateMap();
			MailMergeValueDateTimeNullableMapper.CreateMap();
			MailMergeValueDoubleMapper.CreateMap();
			MailMergeValueDynamicDataMapper.CreateMap();
			MailMergeValueIntMapper.CreateMap();
			MailMergeValueStringMapper.CreateMap();
			Mapper.CreateMap<MailMergeValueBaseDTO, MailMergeValueBase>();
			Mapper.CreateMap<MailMergeValueBase, MailMergeValueBaseDTO>();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001126C File Offset: 0x0000F46C
		public static MailMergeValueBase ToDomainObject(this MailMergeValueBaseDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeValueBaseDTO, MailMergeValueBase>(mailMergeCodeDTO);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011284 File Offset: 0x0000F484
		public static MailMergeValueBaseDTO ToDTO(this MailMergeValueBase mailMergeCode)
		{
			return Mapper.Map<MailMergeValueBase, MailMergeValueBaseDTO>(mailMergeCode);
		}
	}
}

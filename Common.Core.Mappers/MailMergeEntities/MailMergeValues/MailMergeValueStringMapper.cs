using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues
{
	// Token: 0x020000D1 RID: 209
	public static class MailMergeValueStringMapper
	{
		// Token: 0x06000378 RID: 888 RVA: 0x00011540 File Offset: 0x0000F740
		static MailMergeValueStringMapper()
		{
			MailMergeValueBaseMapper.CreateMap();
			Mapper.CreateMap<MailMergeValueStringDTO, MailMergeValueString>();
			Mapper.CreateMap<MailMergeValueString, MailMergeValueStringDTO>();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00011558 File Offset: 0x0000F758
		public static MailMergeValueString ToDomainObject(this MailMergeValueStringDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeValueStringDTO, MailMergeValueString>(mailMergeCodeDTO);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00011570 File Offset: 0x0000F770
		public static MailMergeValueStringDTO ToDTO(this MailMergeValueString mailMergeCode)
		{
			return Mapper.Map<MailMergeValueString, MailMergeValueStringDTO>(mailMergeCode);
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities
{
	// Token: 0x020000C2 RID: 194
	public static class MailMergeContextMapper
	{
		// Token: 0x0600033C RID: 828 RVA: 0x00010E4C File Offset: 0x0000F04C
		static MailMergeContextMapper()
		{
			Mapper.CreateMap<MailMergeContextDTO, MailMergeContext>();
			Mapper.CreateMap<MailMergeContext, MailMergeContextDTO>();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010E5C File Offset: 0x0000F05C
		public static MailMergeContext ToDomainObject(this MailMergeContextDTO mailMergeContextDTO)
		{
			return Mapper.Map<MailMergeContextDTO, MailMergeContext>(mailMergeContextDTO);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00010E74 File Offset: 0x0000F074
		public static MailMergeContextDTO ToDTO(this MailMergeContext mailMergeContext)
		{
			return Mapper.Map<MailMergeContext, MailMergeContextDTO>(mailMergeContext);
		}
	}
}

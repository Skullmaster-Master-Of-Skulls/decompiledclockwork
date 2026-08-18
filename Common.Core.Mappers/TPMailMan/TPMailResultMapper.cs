using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Mappers.TPMailMan
{
	// Token: 0x02000034 RID: 52
	public static class TPMailResultMapper
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00006714 File Offset: 0x00004914
		static TPMailResultMapper()
		{
			Mapper.CreateMap<TPMailResultDTO, TPMailResult>().ForMember((TPMailResult pb) => pb.Id, delegate(IMemberConfigurationExpression<TPMailResultDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TPMailResult, TPMailResultDTO>();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006784 File Offset: 0x00004984
		public static TPMailResult ToDomainObject(this TPMailResultDTO tPMailResultDTO)
		{
			return Mapper.Map<TPMailResultDTO, TPMailResult>(tPMailResultDTO);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000679C File Offset: 0x0000499C
		public static TPMailResultDTO ToDTO(this TPMailResult tPMailResult)
		{
			return Mapper.Map<TPMailResult, TPMailResultDTO>(tPMailResult);
		}
	}
}

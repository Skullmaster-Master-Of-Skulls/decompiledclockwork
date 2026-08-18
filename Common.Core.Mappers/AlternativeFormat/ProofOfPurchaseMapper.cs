using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000218 RID: 536
	public static class ProofOfPurchaseMapper
	{
		// Token: 0x0600091B RID: 2331 RVA: 0x00028950 File Offset: 0x00026B50
		static ProofOfPurchaseMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<ProofOfPurchaseInfo, ProofOfPurchaseInfoDTO>();
			Mapper.CreateMap<ProofOfPurchaseInfoDTO, ProofOfPurchaseInfo>().ForMember((ProofOfPurchaseInfo bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<ProofOfPurchaseInfoDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000289D4 File Offset: 0x00026BD4
		public static ProofOfPurchaseInfo ToDomainObject(this ProofOfPurchaseInfoDTO dto)
		{
			return Mapper.Map<ProofOfPurchaseInfoDTO, ProofOfPurchaseInfo>(dto);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000289EC File Offset: 0x00026BEC
		public static ProofOfPurchaseInfoDTO ToDTO(this ProofOfPurchaseInfo bo)
		{
			return Mapper.Map<ProofOfPurchaseInfo, ProofOfPurchaseInfoDTO>(bo);
		}
	}
}

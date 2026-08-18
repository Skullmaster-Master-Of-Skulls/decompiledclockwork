using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.Common.Public.Entities.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication
{
	// Token: 0x0200018E RID: 398
	public static class CASAuthenticationResultMapper
	{
		// Token: 0x060006CD RID: 1741 RVA: 0x0001E8BC File Offset: 0x0001CABC
		static CASAuthenticationResultMapper()
		{
			Mapper.CreateMap<CASAuthenticationResultDTO, CASAuthenticationResult>();
			Mapper.CreateMap<CASAuthenticationResult, CASAuthenticationResultDTO>();
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001E8CC File Offset: 0x0001CACC
		public static CASAuthenticationResult ToDomainObject(this CASAuthenticationResultDTO ldapConnectionInfoDTO)
		{
			return Mapper.Map<CASAuthenticationResultDTO, CASAuthenticationResult>(ldapConnectionInfoDTO);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001E8E4 File Offset: 0x0001CAE4
		public static CASAuthenticationResultDTO ToDTO(this CASAuthenticationResult ldapConnectionInfo)
		{
			return Mapper.Map<CASAuthenticationResult, CASAuthenticationResultDTO>(ldapConnectionInfo);
		}
	}
}

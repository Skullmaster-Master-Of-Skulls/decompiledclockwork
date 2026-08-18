using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.Common.Public.Entities.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication
{
	// Token: 0x0200018D RID: 397
	public static class CASAuthenticationOptionsMapper
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x0001E87C File Offset: 0x0001CA7C
		static CASAuthenticationOptionsMapper()
		{
			Mapper.CreateMap<CASAuthenticationOptionsDTO, CASAuthenticationOptions>();
			Mapper.CreateMap<CASAuthenticationOptions, CASAuthenticationOptionsDTO>();
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001E88C File Offset: 0x0001CA8C
		public static CASAuthenticationOptions ToDomainObject(this CASAuthenticationOptionsDTO ldapConnectionInfoDTO)
		{
			return Mapper.Map<CASAuthenticationOptionsDTO, CASAuthenticationOptions>(ldapConnectionInfoDTO);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
		public static CASAuthenticationOptionsDTO ToDTO(this CASAuthenticationOptions ldapConnectionInfo)
		{
			return Mapper.Map<CASAuthenticationOptions, CASAuthenticationOptionsDTO>(ldapConnectionInfo);
		}
	}
}

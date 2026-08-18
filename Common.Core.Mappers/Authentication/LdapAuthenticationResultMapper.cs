using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.Common.Public.Entities.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication
{
	// Token: 0x02000191 RID: 401
	public static class LdapAuthenticationResultMapper
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		static LdapAuthenticationResultMapper()
		{
			Mapper.CreateMap<LdapAuthenticationResultDTO, LdapAuthenticationResult>();
			Mapper.CreateMap<LdapAuthenticationResult, LdapAuthenticationResultDTO>();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001EAB0 File Offset: 0x0001CCB0
		public static LdapAuthenticationResult ToDomainObject(this LdapAuthenticationResultDTO dto)
		{
			return Mapper.Map<LdapAuthenticationResultDTO, LdapAuthenticationResult>(dto);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001EAC8 File Offset: 0x0001CCC8
		public static LdapAuthenticationResultDTO ToDTO(this LdapAuthenticationResult item)
		{
			return Mapper.Map<LdapAuthenticationResult, LdapAuthenticationResultDTO>(item);
		}
	}
}

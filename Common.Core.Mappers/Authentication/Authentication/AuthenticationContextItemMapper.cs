using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication.Authentication
{
	// Token: 0x02000194 RID: 404
	public static class AuthenticationContextItemMapper
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x0001ED04 File Offset: 0x0001CF04
		static AuthenticationContextItemMapper()
		{
			Mapper.CreateMap<AuthenticationContextItemDTO, AuthenticationContextItem>();
			Mapper.CreateMap<AuthenticationContextItem, AuthenticationContextItemDTO>();
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001ED14 File Offset: 0x0001CF14
		public static AuthenticationContextItem ToDomainObject(this AuthenticationContextItemDTO ldapConnectionInfoDTO)
		{
			return Mapper.Map<AuthenticationContextItemDTO, AuthenticationContextItem>(ldapConnectionInfoDTO);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001ED2C File Offset: 0x0001CF2C
		public static AuthenticationContextItemDTO ToDTO(this AuthenticationContextItem ldapConnectionInfo)
		{
			return Mapper.Map<AuthenticationContextItem, AuthenticationContextItemDTO>(ldapConnectionInfo);
		}
	}
}

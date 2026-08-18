using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication.Authentication
{
	// Token: 0x02000195 RID: 405
	public static class AuthenticationContextMapper
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x0001ED44 File Offset: 0x0001CF44
		static AuthenticationContextMapper()
		{
			AuthenticationContextItemMapper.CreateMap();
			Mapper.CreateMap<AuthenticationContextDTO, AuthenticationContext>();
			Mapper.CreateMap<AuthenticationContext, AuthenticationContextDTO>();
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001ED5C File Offset: 0x0001CF5C
		public static AuthenticationContext ToDomainObject(this AuthenticationContextDTO ldapConnectionInfoDTO)
		{
			return Mapper.Map<AuthenticationContextDTO, AuthenticationContext>(ldapConnectionInfoDTO);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001ED74 File Offset: 0x0001CF74
		public static AuthenticationContextDTO ToDTO(this AuthenticationContext ldapConnectionInfo)
		{
			return Mapper.Map<AuthenticationContext, AuthenticationContextDTO>(ldapConnectionInfo);
		}
	}
}

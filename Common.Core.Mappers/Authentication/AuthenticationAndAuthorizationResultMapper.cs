using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.Common.Public.Entities.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication
{
	// Token: 0x0200018A RID: 394
	public static class AuthenticationAndAuthorizationResultMapper
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x0001E708 File Offset: 0x0001C908
		static AuthenticationAndAuthorizationResultMapper()
		{
			ClockWorkUserMapper.CreateMap();
			Mapper.CreateMap<AuthenticationAndAuthorizationResultDTO, AuthenticationAndAuthorizationResult>();
			Mapper.CreateMap<AuthenticationAndAuthorizationResult, AuthenticationAndAuthorizationResultDTO>();
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001E720 File Offset: 0x0001C920
		public static AuthenticationAndAuthorizationResult ToDomainObject(this AuthenticationAndAuthorizationResultDTO ldapConnectionInfoDTO)
		{
			return Mapper.Map<AuthenticationAndAuthorizationResultDTO, AuthenticationAndAuthorizationResult>(ldapConnectionInfoDTO);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001E738 File Offset: 0x0001C938
		public static AuthenticationAndAuthorizationResultDTO ToDTO(this AuthenticationAndAuthorizationResult ldapConnectionInfo)
		{
			return Mapper.Map<AuthenticationAndAuthorizationResult, AuthenticationAndAuthorizationResultDTO>(ldapConnectionInfo);
		}
	}
}

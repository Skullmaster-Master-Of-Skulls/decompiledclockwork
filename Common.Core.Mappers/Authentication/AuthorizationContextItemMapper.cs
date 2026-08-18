using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;

namespace TechnoPro.Common.Core.Mappers.Authentication
{
	// Token: 0x0200018B RID: 395
	public static class AuthorizationContextItemMapper
	{
		// Token: 0x060006C1 RID: 1729 RVA: 0x0001E750 File Offset: 0x0001C950
		static AuthorizationContextItemMapper()
		{
			Mapper.CreateMap<AuthorizationContextItemDTO, AuthorizationContextItem>();
			Mapper.CreateMap<AuthorizationContextItem, AuthorizationContextItemDTO>();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001E760 File Offset: 0x0001C960
		public static AuthorizationContextItem ToDomainObject(this AuthorizationContextItemDTO ldapConnectionInfoDTO)
		{
			return Mapper.Map<AuthorizationContextItemDTO, AuthorizationContextItem>(ldapConnectionInfoDTO);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001E778 File Offset: 0x0001C978
		public static AuthorizationContextItemDTO ToDTO(this AuthorizationContextItem ldapConnectionInfo)
		{
			return Mapper.Map<AuthorizationContextItem, AuthorizationContextItemDTO>(ldapConnectionInfo);
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication.Authentication
{
	// Token: 0x02000196 RID: 406
	public static class AuthenticationRequestParametersMapper
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x0001ED8C File Offset: 0x0001CF8C
		static AuthenticationRequestParametersMapper()
		{
			AuthenticationContextItemMapper.CreateMap();
			Mapper.CreateMap<AuthenticationRequestParametersDTO, AuthenticationRequestParameters>();
			Mapper.CreateMap<AuthenticationRequestParameters, AuthenticationRequestParametersDTO>();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001EDA4 File Offset: 0x0001CFA4
		public static AuthenticationRequestParameters ToDomainObject(this AuthenticationRequestParametersDTO ldapConnectionInfoDTO)
		{
			return Mapper.Map<AuthenticationRequestParametersDTO, AuthenticationRequestParameters>(ldapConnectionInfoDTO);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001EDBC File Offset: 0x0001CFBC
		public static AuthenticationRequestParametersDTO ToDTO(this AuthenticationRequestParameters ldapConnectionInfo)
		{
			return Mapper.Map<AuthenticationRequestParameters, AuthenticationRequestParametersDTO>(ldapConnectionInfo);
		}
	}
}

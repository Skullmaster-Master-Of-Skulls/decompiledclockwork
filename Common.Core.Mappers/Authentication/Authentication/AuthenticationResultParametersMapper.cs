using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication.Authentication
{
	// Token: 0x02000197 RID: 407
	public static class AuthenticationResultParametersMapper
	{
		// Token: 0x060006F0 RID: 1776 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
		static AuthenticationResultParametersMapper()
		{
			ExternalUserInfoMapper.CreateMap();
			Mapper.CreateMap<AuthenticationResultParametersDTO, AuthenticationResultParameters>().ForMember((AuthenticationResultParameters pb) => pb.ExternalUserInfo, delegate(IMemberConfigurationExpression<AuthenticationResultParametersDTO> m)
			{
				m.MapFrom<ExternalUserInfo>((AuthenticationResultParametersDTO pbdto) => (pbdto.ExternalUserInfo == null) ? null : pbdto.ExternalUserInfo.ToDomainObject());
			});
			Mapper.CreateMap<AuthenticationResultParameters, AuthenticationResultParametersDTO>().ForMember((AuthenticationResultParametersDTO pb) => pb.ExternalUserInfo, delegate(IMemberConfigurationExpression<AuthenticationResultParameters> m)
			{
				m.MapFrom<ExternalUserInfoDTO>((AuthenticationResultParameters pbdto) => (pbdto.ExternalUserInfo == null) ? null : pbdto.ExternalUserInfo.ToDTO());
			});
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001EE90 File Offset: 0x0001D090
		public static AuthenticationResultParameters ToDomainObject(this AuthenticationResultParametersDTO ldapConnectionInfoDTO)
		{
			return Mapper.Map<AuthenticationResultParametersDTO, AuthenticationResultParameters>(ldapConnectionInfoDTO);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001EEA8 File Offset: 0x0001D0A8
		public static AuthenticationResultParametersDTO ToDTO(this AuthenticationResultParameters ldapConnectionInfo)
		{
			return Mapper.Map<AuthenticationResultParameters, AuthenticationResultParametersDTO>(ldapConnectionInfo);
		}
	}
}

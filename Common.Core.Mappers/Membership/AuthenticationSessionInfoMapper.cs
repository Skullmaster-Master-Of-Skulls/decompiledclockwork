using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public.Entities.Membership;

namespace TechnoPro.Common.Core.Mappers.Membership
{
	// Token: 0x020000BF RID: 191
	public static class AuthenticationSessionInfoMapper
	{
		// Token: 0x0600032C RID: 812 RVA: 0x0001081C File Offset: 0x0000EA1C
		static AuthenticationSessionInfoMapper()
		{
			LogonUserInfoMapper.CreateMap();
			Mapper.CreateMap<AuthenticationSessionInfo, AuthenticationSessionInfoDTO>().ForMember((AuthenticationSessionInfoDTO authsessdto) => (object)authsessdto.Status, delegate(IMemberConfigurationExpression<AuthenticationSessionInfo> m)
			{
				m.MapFrom<eSessionTokenStatusDTO>((AuthenticationSessionInfo authsess) => (eSessionTokenStatusDTO)authsess.Status);
			}).ForMember((AuthenticationSessionInfoDTO authsessdto) => authsessdto.LogonUsers, delegate(IMemberConfigurationExpression<AuthenticationSessionInfo> m)
			{
				m.MapFrom<List<LogonUserInfoDTO>>((AuthenticationSessionInfo authsess) => (authsess.LogonUsers == null) ? null : authsess.LogonUsers.ToList<LogonUserInfo>().ConvertAll<LogonUserInfoDTO>((LogonUserInfo u) => u.ToDTO()));
			});
			Mapper.CreateMap<AuthenticationSessionInfoDTO, AuthenticationSessionInfo>().ForMember((AuthenticationSessionInfo authsess) => (object)authsess.Status, delegate(IMemberConfigurationExpression<AuthenticationSessionInfoDTO> m)
			{
				m.MapFrom<eSessionTokenStatus>((AuthenticationSessionInfoDTO authsessdto) => (eSessionTokenStatus)authsessdto.Status);
			}).ForMember((AuthenticationSessionInfo authsess) => authsess.LogonUsers, delegate(IMemberConfigurationExpression<AuthenticationSessionInfoDTO> m)
			{
				m.MapFrom<List<LogonUserInfo>>((AuthenticationSessionInfoDTO authsessdto) => (authsessdto.LogonUsers == null) ? null : authsessdto.LogonUsers.ToList<LogonUserInfoDTO>().ConvertAll<LogonUserInfo>((LogonUserInfoDTO u) => u.ToDomainObject()));
			});
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00010994 File Offset: 0x0000EB94
		public static AuthenticationSessionInfoDTO ToDTO(this AuthenticationSessionInfo authenticationSessionInfo)
		{
			return Mapper.Map<AuthenticationSessionInfo, AuthenticationSessionInfoDTO>(authenticationSessionInfo);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000109AC File Offset: 0x0000EBAC
		public static AuthenticationSessionInfo ToDomainObject(this AuthenticationSessionInfoDTO authenticationSessionInfo)
		{
			return Mapper.Map<AuthenticationSessionInfoDTO, AuthenticationSessionInfo>(authenticationSessionInfo);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication.Authentication
{
	// Token: 0x02000193 RID: 403
	public static class AuthenticationArgsMapper
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x0001EB80 File Offset: 0x0001CD80
		static AuthenticationArgsMapper()
		{
			Mapper.CreateMap<AuthenticationArgsDTO, AuthenticationArgs>().ForMember((AuthenticationArgs pb) => pb.SecureArgs, delegate(IMemberConfigurationExpression<AuthenticationArgsDTO> m)
			{
				m.MapFrom<Dictionary<string, string>>((AuthenticationArgsDTO pbdto) => (pbdto.SecureArgs == null) ? null : pbdto.SecureArgs.ToDictionary((KeyValuePair<string, string> g) => g.Key, (KeyValuePair<string, string> g) => g.Value));
			}).ForMember((AuthenticationArgs pb) => pb.InsecureArgs, delegate(IMemberConfigurationExpression<AuthenticationArgsDTO> m)
			{
				m.MapFrom<Dictionary<string, string>>((AuthenticationArgsDTO pbdto) => (pbdto.InsecureArgs == null) ? null : pbdto.InsecureArgs.ToDictionary((KeyValuePair<string, string> g) => g.Key, (KeyValuePair<string, string> g) => g.Value));
			});
			Mapper.CreateMap<AuthenticationArgs, AuthenticationArgsDTO>().ForMember((AuthenticationArgsDTO pb) => pb.SecureArgs, delegate(IMemberConfigurationExpression<AuthenticationArgs> m)
			{
				m.MapFrom<Dictionary<string, string>>((AuthenticationArgs pbdto) => (pbdto.SecureArgs == null) ? null : pbdto.SecureArgs.ToDictionary((KeyValuePair<string, string> g) => g.Key, (KeyValuePair<string, string> g) => g.Value));
			}).ForMember((AuthenticationArgsDTO pb) => pb.InsecureArgs, delegate(IMemberConfigurationExpression<AuthenticationArgs> m)
			{
				m.MapFrom<Dictionary<string, string>>((AuthenticationArgs pbdto) => (pbdto.InsecureArgs == null) ? null : pbdto.InsecureArgs.ToDictionary((KeyValuePair<string, string> g) => g.Key, (KeyValuePair<string, string> g) => g.Value));
			});
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001ECD4 File Offset: 0x0001CED4
		public static AuthenticationArgs ToDomainObject(this AuthenticationArgsDTO ldapConnectionInfoDTO)
		{
			return Mapper.Map<AuthenticationArgsDTO, AuthenticationArgs>(ldapConnectionInfoDTO);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001ECEC File Offset: 0x0001CEEC
		public static AuthenticationArgsDTO ToDTO(this AuthenticationArgs ldapConnectionInfo)
		{
			return Mapper.Map<AuthenticationArgs, AuthenticationArgsDTO>(ldapConnectionInfo);
		}
	}
}

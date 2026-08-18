using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.Common.Public.Entities.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication
{
	// Token: 0x02000192 RID: 402
	public static class LdapConnectionInfoMapper
	{
		// Token: 0x060006DC RID: 1756 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		static LdapConnectionInfoMapper()
		{
			Mapper.CreateMap<LdapConnectionInfoDTO, LdapConnectionInfo>().ForMember((LdapConnectionInfo pb) => pb.Id, delegate(IMemberConfigurationExpression<LdapConnectionInfoDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<LdapConnectionInfo, LdapConnectionInfoDTO>();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001EB50 File Offset: 0x0001CD50
		public static LdapConnectionInfo ToDomainObject(this LdapConnectionInfoDTO ldapConnectionInfoDTO)
		{
			return Mapper.Map<LdapConnectionInfoDTO, LdapConnectionInfo>(ldapConnectionInfoDTO);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001EB68 File Offset: 0x0001CD68
		public static LdapConnectionInfoDTO ToDTO(this LdapConnectionInfo ldapConnectionInfo)
		{
			return Mapper.Map<LdapConnectionInfo, LdapConnectionInfoDTO>(ldapConnectionInfo);
		}
	}
}

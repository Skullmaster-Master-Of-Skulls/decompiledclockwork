using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount;
using TechnoPro.Common.Public.Entities.UserAccount;

namespace TechnoPro.Common.Core.Mappers.UserAccount
{
	// Token: 0x02000020 RID: 32
	public static class PasswordPolicyMapper
	{
		// Token: 0x06000088 RID: 136 RVA: 0x000053E4 File Offset: 0x000035E4
		static PasswordPolicyMapper()
		{
			Mapper.CreateMap<PasswordPolicy, PasswordPolicyDTO>();
			Mapper.CreateMap<PasswordPolicyDTO, PasswordPolicy>();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000053F4 File Offset: 0x000035F4
		public static PasswordPolicy ToDomainObject(this PasswordPolicyDTO groupDTO)
		{
			return Mapper.Map<PasswordPolicyDTO, PasswordPolicy>(groupDTO);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000540C File Offset: 0x0000360C
		public static PasswordPolicyDTO ToDTO(this PasswordPolicy group)
		{
			return Mapper.Map<PasswordPolicy, PasswordPolicyDTO>(group);
		}
	}
}

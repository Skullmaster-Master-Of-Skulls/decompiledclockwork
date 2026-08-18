using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;

namespace TechnoPro.Common.Core.Mappers.Authentication
{
	// Token: 0x02000190 RID: 400
	public static class ClockWorkUserMapper
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x0001EA60 File Offset: 0x0001CC60
		static ClockWorkUserMapper()
		{
			Mapper.CreateMap<ClockWorkUserDTO, ClockWorkUser>();
			Mapper.CreateMap<ClockWorkUser, ClockWorkUserDTO>();
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001EA70 File Offset: 0x0001CC70
		public static ClockWorkUser ToDomainObject(this ClockWorkUserDTO ldapConnectionInfoDTO)
		{
			return Mapper.Map<ClockWorkUserDTO, ClockWorkUser>(ldapConnectionInfoDTO);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001EA88 File Offset: 0x0001CC88
		public static ClockWorkUserDTO ToDTO(this ClockWorkUser ldapConnectionInfo)
		{
			return Mapper.Map<ClockWorkUser, ClockWorkUserDTO>(ldapConnectionInfo);
		}
	}
}

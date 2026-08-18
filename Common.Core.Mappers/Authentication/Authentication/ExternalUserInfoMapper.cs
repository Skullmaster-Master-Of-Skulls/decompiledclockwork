using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication.Authentication
{
	// Token: 0x02000198 RID: 408
	public static class ExternalUserInfoMapper
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x0001EEC0 File Offset: 0x0001D0C0
		static ExternalUserInfoMapper()
		{
			Mapper.CreateMap<ExternalUserInfoDTO, ExternalUserInfo>();
			Mapper.CreateMap<ExternalUserInfo, ExternalUserInfoDTO>();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001EED0 File Offset: 0x0001D0D0
		public static ExternalUserInfo ToDomainObject(this ExternalUserInfoDTO dto)
		{
			return Mapper.Map<ExternalUserInfoDTO, ExternalUserInfo>(dto);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001EEE8 File Offset: 0x0001D0E8
		public static ExternalUserInfoDTO ToDTO(this ExternalUserInfo item)
		{
			return Mapper.Map<ExternalUserInfo, ExternalUserInfoDTO>(item);
		}
	}
}

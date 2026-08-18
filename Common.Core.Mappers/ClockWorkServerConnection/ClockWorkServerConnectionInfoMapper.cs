using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.Core.Mappers.ClockWorkServerConnection
{
	// Token: 0x02000170 RID: 368
	public static class ClockWorkServerConnectionInfoMapper
	{
		// Token: 0x06000655 RID: 1621 RVA: 0x0001D0F7 File Offset: 0x0001B2F7
		static ClockWorkServerConnectionInfoMapper()
		{
			CertificateInfoMapper.CreateMap();
			InternetInformationServicesVersionMapper.CreateMap();
			Mapper.CreateMap<ClockWorkServerPreferredConnectionInfoDTO, ClockWorkServerPreferredConnectionInfo>();
			Mapper.CreateMap<ClockWorkServerPreferredConnectionInfo, ClockWorkServerPreferredConnectionInfoDTO>();
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001D114 File Offset: 0x0001B314
		public static ClockWorkServerPreferredConnectionInfo ToDomainObject(this ClockWorkServerPreferredConnectionInfoDTO dto)
		{
			return Mapper.Map<ClockWorkServerPreferredConnectionInfoDTO, ClockWorkServerPreferredConnectionInfo>(dto);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001D12C File Offset: 0x0001B32C
		public static ClockWorkServerPreferredConnectionInfoDTO ToDTO(this ClockWorkServerPreferredConnectionInfo item)
		{
			return Mapper.Map<ClockWorkServerPreferredConnectionInfo, ClockWorkServerPreferredConnectionInfoDTO>(item);
		}
	}
}

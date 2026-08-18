using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.Core.Mappers.ClockWorkServerConnection
{
	// Token: 0x0200016E RID: 366
	public static class CertificateInfoMapper
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x0001D0A7 File Offset: 0x0001B2A7
		static CertificateInfoMapper()
		{
			Mapper.CreateMap<CertificateInfoDTO, CertificateInfo>();
			Mapper.CreateMap<CertificateInfo, CertificateInfoDTO>();
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001D0B8 File Offset: 0x0001B2B8
		public static CertificateInfo ToDomainObject(this CertificateInfoDTO dto)
		{
			return Mapper.Map<CertificateInfoDTO, CertificateInfo>(dto);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001D0D0 File Offset: 0x0001B2D0
		public static CertificateInfoDTO ToDTO(this CertificateInfo item)
		{
			return Mapper.Map<CertificateInfo, CertificateInfoDTO>(item);
		}
	}
}

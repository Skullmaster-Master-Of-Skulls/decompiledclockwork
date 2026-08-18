using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Database;
using TechnoPro.Common.Public.Entities.Database;

namespace TechnoPro.Common.Core.Mappers.Database
{
	// Token: 0x02000151 RID: 337
	public static class DbConnectionInfoMapper
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x0001AA6C File Offset: 0x00018C6C
		static DbConnectionInfoMapper()
		{
			Mapper.CreateMap<DbConnectionInfo, DbConnectionInfoDTO>();
			Mapper.CreateMap<DbConnectionInfo, DbConnectionInfoDTO>();
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001AA7C File Offset: 0x00018C7C
		public static DbConnectionInfo ToDomainObject(this DbConnectionInfoDTO dto)
		{
			return Mapper.Map<DbConnectionInfoDTO, DbConnectionInfo>(dto);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001AA94 File Offset: 0x00018C94
		public static DbConnectionInfoDTO ToDTO(this DbConnectionInfo item)
		{
			return Mapper.Map<DbConnectionInfo, DbConnectionInfoDTO>(item);
		}
	}
}

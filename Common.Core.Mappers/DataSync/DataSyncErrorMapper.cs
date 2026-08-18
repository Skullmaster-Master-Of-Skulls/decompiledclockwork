using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x02000138 RID: 312
	public static class DataSyncErrorMapper
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x00019A9A File Offset: 0x00017C9A
		static DataSyncErrorMapper()
		{
			Mapper.CreateMap<DataSyncErrorDTO, DataSyncError>();
			Mapper.CreateMap<DataSyncError, DataSyncErrorDTO>();
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00019AAC File Offset: 0x00017CAC
		public static DataSyncError ToDomainObject(this DataSyncErrorDTO dto)
		{
			return Mapper.Map<DataSyncErrorDTO, DataSyncError>(dto);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00019AC4 File Offset: 0x00017CC4
		public static DataSyncErrorDTO ToDTO(this DataSyncError item)
		{
			return Mapper.Map<DataSyncError, DataSyncErrorDTO>(item);
		}
	}
}

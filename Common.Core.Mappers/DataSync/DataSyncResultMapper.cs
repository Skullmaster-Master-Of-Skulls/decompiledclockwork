using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x02000145 RID: 325
	public static class DataSyncResultMapper
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x0001A104 File Offset: 0x00018304
		static DataSyncResultMapper()
		{
			DataSyncErrorMapper.CreateMap();
			Mapper.CreateMap<DataSyncResultDTO, DataSyncResult>();
			Mapper.CreateMap<DataSyncResult, DataSyncResultDTO>();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001A11C File Offset: 0x0001831C
		public static DataSyncResult ToDomainObject(this DataSyncResultDTO dto)
		{
			return Mapper.Map<DataSyncResultDTO, DataSyncResult>(dto);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001A134 File Offset: 0x00018334
		public static DataSyncResultDTO ToDTO(this DataSyncResult item)
		{
			return Mapper.Map<DataSyncResult, DataSyncResultDTO>(item);
		}
	}
}

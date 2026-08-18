using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x02000144 RID: 324
	public static class DataSyncPreviewResultMapper
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x0001A0B8 File Offset: 0x000182B8
		static DataSyncPreviewResultMapper()
		{
			DataSyncExternalDataMapper.CreateMap();
			DataSyncErrorMapper.CreateMap();
			Mapper.CreateMap<DataSyncPreviewResultDTO, DataSyncPreviewResult>();
			Mapper.CreateMap<DataSyncPreviewResult, DataSyncPreviewResultDTO>();
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001A0D4 File Offset: 0x000182D4
		public static DataSyncPreviewResult ToDomainObject(this DataSyncPreviewResultDTO dto)
		{
			return Mapper.Map<DataSyncPreviewResultDTO, DataSyncPreviewResult>(dto);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001A0EC File Offset: 0x000182EC
		public static DataSyncPreviewResultDTO ToDTO(this DataSyncPreviewResult item)
		{
			return Mapper.Map<DataSyncPreviewResult, DataSyncPreviewResultDTO>(item);
		}
	}
}

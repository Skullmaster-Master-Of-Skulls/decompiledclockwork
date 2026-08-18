using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x02000143 RID: 323
	public static class DataSyncInfoMapper
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x0001A078 File Offset: 0x00018278
		static DataSyncInfoMapper()
		{
			Mapper.CreateMap<DataSyncInfoDTO, DataSyncInfo>();
			Mapper.CreateMap<DataSyncInfo, DataSyncInfoDTO>();
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001A088 File Offset: 0x00018288
		public static DataSyncInfo ToDomainObject(this DataSyncInfoDTO dataSyncInfoDTO)
		{
			return Mapper.Map<DataSyncInfoDTO, DataSyncInfo>(dataSyncInfoDTO);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001A0A0 File Offset: 0x000182A0
		public static DataSyncInfoDTO ToDTO(this DataSyncInfo dataSyncInfo)
		{
			return Mapper.Map<DataSyncInfo, DataSyncInfoDTO>(dataSyncInfo);
		}
	}
}

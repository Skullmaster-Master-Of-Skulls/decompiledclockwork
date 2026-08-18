using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x02000140 RID: 320
	public static class DataSyncExternalCourseSyncResultMapper
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x00019F44 File Offset: 0x00018144
		static DataSyncExternalCourseSyncResultMapper()
		{
			DataSyncExternalCourseMapper.CreateMap();
			ClassTestBaseMapper.CreateMap();
			Mapper.CreateMap<DataSyncExternalCourseSyncResultDTO, DataSyncExternalCourseSyncResult>();
			Mapper.CreateMap<DataSyncExternalCourseSyncResult, DataSyncExternalCourseSyncResultDTO>();
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00019F60 File Offset: 0x00018160
		public static DataSyncExternalCourseSyncResult ToDomainObject(this DataSyncExternalCourseSyncResultDTO dataSyncExternalCourseSyncResultDTO)
		{
			return Mapper.Map<DataSyncExternalCourseSyncResultDTO, DataSyncExternalCourseSyncResult>(dataSyncExternalCourseSyncResultDTO);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00019F78 File Offset: 0x00018178
		public static DataSyncExternalCourseSyncResultDTO ToDTO(this DataSyncExternalCourseSyncResult dataSyncExternalCourseSyncResult)
		{
			return Mapper.Map<DataSyncExternalCourseSyncResult, DataSyncExternalCourseSyncResultDTO>(dataSyncExternalCourseSyncResult);
		}
	}
}

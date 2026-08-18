using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x02000141 RID: 321
	public static class DataSyncExternalCourseTimetableItemMapper
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x00019F90 File Offset: 0x00018190
		static DataSyncExternalCourseTimetableItemMapper()
		{
			DataSyncExternalCourseInstructorMapper.CreateMap();
			Mapper.CreateMap<DataSyncExternalCourseTimetableItemDTO, DataSyncExternalCourseTimetableItem>();
			Mapper.CreateMap<DataSyncExternalCourseTimetableItem, DataSyncExternalCourseTimetableItemDTO>();
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00019FA8 File Offset: 0x000181A8
		public static DataSyncExternalCourseTimetableItem ToDomainObject(this DataSyncExternalCourseTimetableItemDTO dataSyncExternalCourseTimetableItemDTO)
		{
			return Mapper.Map<DataSyncExternalCourseTimetableItemDTO, DataSyncExternalCourseTimetableItem>(dataSyncExternalCourseTimetableItemDTO);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00019FC0 File Offset: 0x000181C0
		public static DataSyncExternalCourseTimetableItemDTO ToDTO(this DataSyncExternalCourseTimetableItem dataSyncExternalCourseTimetableItem)
		{
			return Mapper.Map<DataSyncExternalCourseTimetableItem, DataSyncExternalCourseTimetableItemDTO>(dataSyncExternalCourseTimetableItem);
		}
	}
}

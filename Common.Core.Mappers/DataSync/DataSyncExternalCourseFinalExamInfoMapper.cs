using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncCourses;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x0200013A RID: 314
	public static class DataSyncExternalCourseFinalExamInfoMapper
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x00019B7C File Offset: 0x00017D7C
		static DataSyncExternalCourseFinalExamInfoMapper()
		{
			Mapper.CreateMap<DataSyncExternalCourseFinalExamInfoDTO, DataSyncExternalCourseFinalExamInfo>();
			Mapper.CreateMap<DataSyncExternalCourseFinalExamInfo, DataSyncExternalCourseFinalExamInfoDTO>();
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00019B8C File Offset: 0x00017D8C
		public static DataSyncExternalCourseFinalExamInfo ToDomainObject(this DataSyncExternalCourseFinalExamInfoDTO dto)
		{
			return Mapper.Map<DataSyncExternalCourseFinalExamInfoDTO, DataSyncExternalCourseFinalExamInfo>(dto);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00019BA4 File Offset: 0x00017DA4
		public static DataSyncExternalCourseFinalExamInfoDTO ToDTO(this DataSyncExternalCourseFinalExamInfo item)
		{
			return Mapper.Map<DataSyncExternalCourseFinalExamInfo, DataSyncExternalCourseFinalExamInfoDTO>(item);
		}
	}
}

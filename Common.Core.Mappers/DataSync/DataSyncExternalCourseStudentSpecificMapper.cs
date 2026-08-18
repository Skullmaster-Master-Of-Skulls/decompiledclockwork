using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x0200013D RID: 317
	public static class DataSyncExternalCourseStudentSpecificMapper
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00019DC0 File Offset: 0x00017FC0
		static DataSyncExternalCourseStudentSpecificMapper()
		{
			Mapper.CreateMap<DataSyncExternalCourseStudentSpecificDTO, DataSyncExternalCourseStudentSpecific>();
			Mapper.CreateMap<DataSyncExternalCourseStudentSpecific, DataSyncExternalCourseStudentSpecificDTO>();
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00019DD0 File Offset: 0x00017FD0
		public static DataSyncExternalCourseStudentSpecific ToDomainObject(this DataSyncExternalCourseStudentSpecificDTO dto)
		{
			return Mapper.Map<DataSyncExternalCourseStudentSpecificDTO, DataSyncExternalCourseStudentSpecific>(dto);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00019DE8 File Offset: 0x00017FE8
		public static DataSyncExternalCourseStudentSpecificDTO ToDTO(this DataSyncExternalCourseStudentSpecific item)
		{
			return Mapper.Map<DataSyncExternalCourseStudentSpecific, DataSyncExternalCourseStudentSpecificDTO>(item);
		}
	}
}

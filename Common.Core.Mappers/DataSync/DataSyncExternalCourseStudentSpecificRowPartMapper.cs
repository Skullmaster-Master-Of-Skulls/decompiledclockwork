using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x0200013F RID: 319
	public static class DataSyncExternalCourseStudentSpecificRowPartMapper
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x00019F04 File Offset: 0x00018104
		static DataSyncExternalCourseStudentSpecificRowPartMapper()
		{
			Mapper.CreateMap<DataSyncExternalCourseStudentSpecificRowPartDTO, DataSyncExternalCourseStudentSpecificRowPart>();
			Mapper.CreateMap<DataSyncExternalCourseStudentSpecificRowPart, DataSyncExternalCourseStudentSpecificRowPartDTO>();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00019F14 File Offset: 0x00018114
		public static DataSyncExternalCourseStudentSpecificRowPart ToDomainObject(this DataSyncExternalCourseStudentSpecificRowPartDTO dataSyncExternalCourseRowPartDTO)
		{
			return Mapper.Map<DataSyncExternalCourseStudentSpecificRowPartDTO, DataSyncExternalCourseStudentSpecificRowPart>(dataSyncExternalCourseRowPartDTO);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00019F2C File Offset: 0x0001812C
		public static DataSyncExternalCourseStudentSpecificRowPartDTO ToDTO(this DataSyncExternalCourseStudentSpecificRowPart dataSyncExternalCourseRowPart)
		{
			return Mapper.Map<DataSyncExternalCourseStudentSpecificRowPart, DataSyncExternalCourseStudentSpecificRowPartDTO>(dataSyncExternalCourseRowPart);
		}
	}
}

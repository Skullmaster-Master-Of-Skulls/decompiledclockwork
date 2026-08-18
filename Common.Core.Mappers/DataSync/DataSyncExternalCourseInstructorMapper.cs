using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x0200013B RID: 315
	public static class DataSyncExternalCourseInstructorMapper
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x00019BBC File Offset: 0x00017DBC
		static DataSyncExternalCourseInstructorMapper()
		{
			LookupInstructorMapper.CreateMap();
			Mapper.CreateMap<DataSyncExternalCourseInstructorDTO, DataSyncExternalCourseInstructor>().ForMember((DataSyncExternalCourseInstructor pb) => pb.Id, delegate(IMemberConfigurationExpression<DataSyncExternalCourseInstructorDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DataSyncExternalCourseInstructor, DataSyncExternalCourseInstructorDTO>();
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00019C30 File Offset: 0x00017E30
		public static DataSyncExternalCourseInstructor ToDomainObject(this DataSyncExternalCourseInstructorDTO dataSyncExternalCourseInstructorDTO)
		{
			return Mapper.Map<DataSyncExternalCourseInstructorDTO, DataSyncExternalCourseInstructor>(dataSyncExternalCourseInstructorDTO);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00019C48 File Offset: 0x00017E48
		public static DataSyncExternalCourseInstructorDTO ToDTO(this DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor)
		{
			return Mapper.Map<DataSyncExternalCourseInstructor, DataSyncExternalCourseInstructorDTO>(dataSyncExternalCourseInstructor);
		}
	}
}

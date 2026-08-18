using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x0200013C RID: 316
	public static class DataSyncExternalCourseMapper
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x00019C60 File Offset: 0x00017E60
		static DataSyncExternalCourseMapper()
		{
			DataSyncExternalCourseAltContactMapper.CreateMap();
			DataSyncExternalCourseInstructorMapper.CreateMap();
			LookupCourseMapper.CreateMap();
			DataSyncExternalCourseFinalExamInfoMapper.CreateMap();
			DataSyncExternalCourseTimetableItemMapper.CreateMap();
			DataSyncExternalCourseStudentSpecificMapper.CreateMap();
			Mapper.CreateMap<DataSyncExternalCourseDTO, DataSyncExternalCourse>().ForMember((DataSyncExternalCourse pb) => pb.Id, delegate(IMemberConfigurationExpression<DataSyncExternalCourseDTO> m)
			{
				m.Ignore();
			}).ForMember((DataSyncExternalCourse pb) => pb.StudentSpecificInfo, delegate(IMemberConfigurationExpression<DataSyncExternalCourseDTO> m)
			{
				m.MapFrom<DataSyncExternalCourseStudentSpecific>((DataSyncExternalCourseDTO pbdto) => (pbdto.StudentSpecificInfo == null) ? null : pbdto.StudentSpecificInfo.ToDomainObject());
			});
			Mapper.CreateMap<DataSyncExternalCourse, DataSyncExternalCourseDTO>().ForMember((DataSyncExternalCourseDTO pb) => pb.StudentSpecificInfo, delegate(IMemberConfigurationExpression<DataSyncExternalCourse> m)
			{
				m.MapFrom<DataSyncExternalCourseStudentSpecificDTO>((DataSyncExternalCourse pbdto) => (pbdto.StudentSpecificInfo == null) ? null : pbdto.StudentSpecificInfo.ToDTO());
			});
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00019D90 File Offset: 0x00017F90
		public static DataSyncExternalCourse ToDomainObject(this DataSyncExternalCourseDTO dataSyncExternalCourseDTO)
		{
			return Mapper.Map<DataSyncExternalCourseDTO, DataSyncExternalCourse>(dataSyncExternalCourseDTO);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00019DA8 File Offset: 0x00017FA8
		public static DataSyncExternalCourseDTO ToDTO(this DataSyncExternalCourse dataSyncExternalCourse)
		{
			return Mapper.Map<DataSyncExternalCourse, DataSyncExternalCourseDTO>(dataSyncExternalCourse);
		}
	}
}

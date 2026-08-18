using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public.Entities.DataSync;

namespace TechnoPro.Common.Core.Mappers.DataSync
{
	// Token: 0x0200013E RID: 318
	public static class DataSyncExternalCourseRowPartMapper
	{
		// Token: 0x06000571 RID: 1393 RVA: 0x00019E00 File Offset: 0x00018000
		static DataSyncExternalCourseRowPartMapper()
		{
			DataSyncExternalCourseAltContactMapper.CreateMap();
			DataSyncExternalCourseInstructorMapper.CreateMap();
			DataSyncExternalCourseFinalExamInfoMapper.CreateMap();
			DataSyncExternalCourseTimetableItemMapper.CreateMap();
			DataSyncExternalCourseStudentSpecificRowPartMapper.CreateMap();
			Mapper.CreateMap<DataSyncExternalCourseRowPartDTO, DataSyncExternalCourseRowPart>().ForMember((DataSyncExternalCourseRowPart pb) => pb.StudentSpecificInfo, delegate(IMemberConfigurationExpression<DataSyncExternalCourseRowPartDTO> m)
			{
				m.MapFrom<DataSyncExternalCourseStudentSpecificRowPart>((DataSyncExternalCourseRowPartDTO pbdto) => (pbdto.StudentSpecificInfo == null) ? null : pbdto.StudentSpecificInfo.ToDomainObject());
			});
			Mapper.CreateMap<DataSyncExternalCourseRowPart, DataSyncExternalCourseRowPartDTO>().ForMember((DataSyncExternalCourseRowPartDTO pb) => pb.StudentSpecificInfo, delegate(IMemberConfigurationExpression<DataSyncExternalCourseRowPart> m)
			{
				m.MapFrom<DataSyncExternalCourseStudentSpecificRowPartDTO>((DataSyncExternalCourseRowPart pbdto) => (pbdto.StudentSpecificInfo == null) ? null : pbdto.StudentSpecificInfo.ToDTO());
			});
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00019ED4 File Offset: 0x000180D4
		public static DataSyncExternalCourseRowPart ToDomainObject(this DataSyncExternalCourseRowPartDTO dataSyncExternalCourseRowPartDTO)
		{
			return Mapper.Map<DataSyncExternalCourseRowPartDTO, DataSyncExternalCourseRowPart>(dataSyncExternalCourseRowPartDTO);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00019EEC File Offset: 0x000180EC
		public static DataSyncExternalCourseRowPartDTO ToDTO(this DataSyncExternalCourseRowPart dataSyncExternalCourseRowPart)
		{
			return Mapper.Map<DataSyncExternalCourseRowPart, DataSyncExternalCourseRowPartDTO>(dataSyncExternalCourseRowPart);
		}
	}
}

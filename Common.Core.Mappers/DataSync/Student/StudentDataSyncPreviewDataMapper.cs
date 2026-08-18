using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Student;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos;
using TechnoPro.Common.Public.Entities.DataSync.Student;

namespace TechnoPro.Common.Core.Mappers.DataSync.Student
{
	// Token: 0x02000146 RID: 326
	public static class StudentDataSyncPreviewDataMapper
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x0001A14C File Offset: 0x0001834C
		static StudentDataSyncPreviewDataMapper()
		{
			Mapper.CreateMap<StudentDataSyncPreviewDataDTO, StudentDataSyncPreviewData>().ForMember((StudentDataSyncPreviewData pb) => pb.Id, delegate(IMemberConfigurationExpression<StudentDataSyncPreviewDataDTO> m)
			{
				m.Ignore();
			}).ForMember((StudentDataSyncPreviewData pb) => pb.ExternalDataItems, delegate(IMemberConfigurationExpression<StudentDataSyncPreviewDataDTO> m)
			{
				m.MapFrom<List<DataSyncExternalData>>((StudentDataSyncPreviewDataDTO pbdto) => (pbdto.ExternalDataItems == null) ? null : pbdto.ExternalDataItems.ToList<DataSyncExternalDataDTO>().ConvertAll<DataSyncExternalData>((DataSyncExternalDataDTO g) => g.ToDomainObject()));
			});
			Mapper.CreateMap<StudentDataSyncPreviewData, StudentDataSyncPreviewDataDTO>().ForMember((StudentDataSyncPreviewDataDTO pb) => pb.ExternalDataItems, delegate(IMemberConfigurationExpression<StudentDataSyncPreviewData> m)
			{
				m.MapFrom<List<DataSyncExternalDataDTO>>((StudentDataSyncPreviewData pbdto) => (pbdto.ExternalDataItems == null) ? null : pbdto.ExternalDataItems.ToList<DataSyncExternalData>().ConvertAll<DataSyncExternalDataDTO>((DataSyncExternalData g) => g.ToDTO()));
			});
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001A258 File Offset: 0x00018458
		public static StudentDataSyncPreviewData ToDomainObject(this StudentDataSyncPreviewDataDTO dataSyncExternalCourseAltContactDTO)
		{
			return Mapper.Map<StudentDataSyncPreviewDataDTO, StudentDataSyncPreviewData>(dataSyncExternalCourseAltContactDTO);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001A270 File Offset: 0x00018470
		public static StudentDataSyncPreviewDataDTO ToDTO(this StudentDataSyncPreviewData dataSyncExternalCourseAltContact)
		{
			return Mapper.Map<StudentDataSyncPreviewData, StudentDataSyncPreviewDataDTO>(dataSyncExternalCourseAltContact);
		}
	}
}

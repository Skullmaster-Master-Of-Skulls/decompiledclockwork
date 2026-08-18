using System;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Core.Mappers.StudentFiles
{
	// Token: 0x02000059 RID: 89
	public static class StudentFilesQueueStudentItemMapper
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00009F20 File Offset: 0x00008120
		static StudentFilesQueueStudentItemMapper()
		{
			StudentFilesQueueFileItemMapper.CreateMap();
			Mapper.CreateMap<StudentFilesQueueStudentItemDTO, StudentFilesQueueStudentItem>().ForMember((StudentFilesQueueStudentItem pb) => pb.FileItems, delegate(IMemberConfigurationExpression<StudentFilesQueueStudentItemDTO> m)
			{
				m.MapFrom<StudentFilesQueueFileItem[]>((StudentFilesQueueStudentItemDTO pbdto) => (pbdto.FileItems == null) ? null : (from g in pbdto.FileItems
				select g.ToDomainObject()).ToArray<StudentFilesQueueFileItem>());
			});
			Mapper.CreateMap<StudentFilesQueueStudentItem, StudentFilesQueueStudentItemDTO>().ForMember((StudentFilesQueueStudentItemDTO pb) => pb.FileItems, delegate(IMemberConfigurationExpression<StudentFilesQueueStudentItem> m)
			{
				m.MapFrom<StudentFilesQueueFileItemDTO[]>((StudentFilesQueueStudentItem pbdto) => (pbdto.FileItems == null) ? null : (from g in pbdto.FileItems
				select g.ToDTO()).ToArray<StudentFilesQueueFileItemDTO>());
			});
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00009FDC File Offset: 0x000081DC
		public static StudentFilesQueueStudentItem ToDomainObject(this StudentFilesQueueStudentItemDTO dynamicDataDTO)
		{
			return Mapper.Map<StudentFilesQueueStudentItemDTO, StudentFilesQueueStudentItem>(dynamicDataDTO);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009FF4 File Offset: 0x000081F4
		public static StudentFilesQueueStudentItemDTO ToDTO(this StudentFilesQueueStudentItem dynamicData)
		{
			return Mapper.Map<StudentFilesQueueStudentItem, StudentFilesQueueStudentItemDTO>(dynamicData);
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Core.Mappers.StudentFiles
{
	// Token: 0x02000056 RID: 86
	public static class StudentFilesQueueFileItemMapper
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00009D00 File Offset: 0x00007F00
		static StudentFilesQueueFileItemMapper()
		{
			StudentFilesStatusMapper.CreateMap();
			Mapper.CreateMap<StudentFilesQueueFileItemDTO, StudentFilesQueueFileItem>().ForMember((StudentFilesQueueFileItem pb) => pb.Status, delegate(IMemberConfigurationExpression<StudentFilesQueueFileItemDTO> m)
			{
				m.MapFrom<StudentFilesStatus>((StudentFilesQueueFileItemDTO pbdto) => (pbdto.Status == null) ? null : pbdto.Status.ToDomainObject());
			});
			Mapper.CreateMap<StudentFilesQueueFileItem, StudentFilesQueueFileItemDTO>().ForMember((StudentFilesQueueFileItemDTO pb) => pb.Status, delegate(IMemberConfigurationExpression<StudentFilesQueueFileItem> m)
			{
				m.MapFrom<StudentFilesStatusDTO>((StudentFilesQueueFileItem pbdto) => (pbdto.Status == null) ? null : pbdto.Status.ToDTO());
			});
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00009DBC File Offset: 0x00007FBC
		public static StudentFilesQueueFileItem ToDomainObject(this StudentFilesQueueFileItemDTO dynamicDataDTO)
		{
			return Mapper.Map<StudentFilesQueueFileItemDTO, StudentFilesQueueFileItem>(dynamicDataDTO);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00009DD4 File Offset: 0x00007FD4
		public static StudentFilesQueueFileItemDTO ToDTO(this StudentFilesQueueFileItem dynamicData)
		{
			return Mapper.Map<StudentFilesQueueFileItem, StudentFilesQueueFileItemDTO>(dynamicData);
		}
	}
}

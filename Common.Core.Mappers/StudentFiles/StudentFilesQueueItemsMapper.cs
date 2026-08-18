using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Core.Mappers.StudentFiles
{
	// Token: 0x02000057 RID: 87
	public static class StudentFilesQueueItemsMapper
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00009DEC File Offset: 0x00007FEC
		static StudentFilesQueueItemsMapper()
		{
			StudentFilesQueueStudentItemMapper.CreateMap();
			StudentFilesLookupStatusMapper.CreateMap();
			Mapper.CreateMap<StudentFilesQueueItemsDTO, StudentFilesQueueItems>().ForMember((StudentFilesQueueItems pb) => pb.StudentItems, delegate(IMemberConfigurationExpression<StudentFilesQueueItemsDTO> m)
			{
				m.MapFrom<IEnumerable<StudentFilesQueueStudentItem>>((StudentFilesQueueItemsDTO pbdto) => (pbdto.StudentItems == null) ? null : (from g in pbdto.StudentItems
				select g.ToDomainObject()));
			});
			Mapper.CreateMap<StudentFilesQueueItems, StudentFilesQueueItemsDTO>().ForMember((StudentFilesQueueItemsDTO pb) => pb.StudentItems, delegate(IMemberConfigurationExpression<StudentFilesQueueItems> m)
			{
				m.MapFrom<IEnumerable<StudentFilesQueueStudentItemDTO>>((StudentFilesQueueItems pbdto) => (pbdto.StudentItems == null) ? null : (from g in pbdto.StudentItems
				select g.ToDTO()));
			});
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00009EB0 File Offset: 0x000080B0
		public static StudentFilesQueueItems ToDomainObject(this StudentFilesQueueItemsDTO dynamicDataDTO)
		{
			return Mapper.Map<StudentFilesQueueItemsDTO, StudentFilesQueueItems>(dynamicDataDTO);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00009EC8 File Offset: 0x000080C8
		public static StudentFilesQueueItemsDTO ToDTO(this StudentFilesQueueItems dynamicData)
		{
			return Mapper.Map<StudentFilesQueueItems, StudentFilesQueueItemsDTO>(dynamicData);
		}
	}
}

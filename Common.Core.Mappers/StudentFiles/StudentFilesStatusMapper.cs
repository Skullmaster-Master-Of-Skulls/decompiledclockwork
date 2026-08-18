using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Core.Mappers.StudentFiles
{
	// Token: 0x0200005A RID: 90
	public static class StudentFilesStatusMapper
	{
		// Token: 0x06000170 RID: 368 RVA: 0x0000A00C File Offset: 0x0000820C
		static StudentFilesStatusMapper()
		{
			Mapper.CreateMap<StudentFilesStatusDTO, StudentFilesStatus>();
			Mapper.CreateMap<StudentFilesStatus, StudentFilesStatusDTO>();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000A01C File Offset: 0x0000821C
		public static StudentFilesStatus ToDomainObject(this StudentFilesStatusDTO dynamicDataDTO)
		{
			return Mapper.Map<StudentFilesStatusDTO, StudentFilesStatus>(dynamicDataDTO);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000A034 File Offset: 0x00008234
		public static StudentFilesStatusDTO ToDTO(this StudentFilesStatus dynamicData)
		{
			return Mapper.Map<StudentFilesStatus, StudentFilesStatusDTO>(dynamicData);
		}
	}
}

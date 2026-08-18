using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Core.Mappers.StudentFiles
{
	// Token: 0x02000055 RID: 85
	public static class StudentFilesLookupStatusMapper
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00009CC0 File Offset: 0x00007EC0
		static StudentFilesLookupStatusMapper()
		{
			Mapper.CreateMap<StudentFilesLookupStatusDTO, StudentFilesLookupStatus>();
			Mapper.CreateMap<StudentFilesLookupStatus, StudentFilesLookupStatusDTO>();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00009CD0 File Offset: 0x00007ED0
		public static StudentFilesLookupStatus ToDomainObject(this StudentFilesLookupStatusDTO dynamicDataDTO)
		{
			return Mapper.Map<StudentFilesLookupStatusDTO, StudentFilesLookupStatus>(dynamicDataDTO);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00009CE8 File Offset: 0x00007EE8
		public static StudentFilesLookupStatusDTO ToDTO(this StudentFilesLookupStatus dynamicData)
		{
			return Mapper.Map<StudentFilesLookupStatus, StudentFilesLookupStatusDTO>(dynamicData);
		}
	}
}

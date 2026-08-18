using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Core.Mappers.StudentFiles
{
	// Token: 0x02000058 RID: 88
	public static class StudentFilesQueueLoadParametersMapper
	{
		// Token: 0x06000168 RID: 360 RVA: 0x00009EE0 File Offset: 0x000080E0
		static StudentFilesQueueLoadParametersMapper()
		{
			Mapper.CreateMap<StudentFilesQueueLoadParametersDTO, StudentFilesQueueLoadParameters>();
			Mapper.CreateMap<StudentFilesQueueLoadParameters, StudentFilesQueueLoadParametersDTO>();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009EF0 File Offset: 0x000080F0
		public static StudentFilesQueueLoadParameters ToDomainObject(this StudentFilesQueueLoadParametersDTO dynamicDataDTO)
		{
			return Mapper.Map<StudentFilesQueueLoadParametersDTO, StudentFilesQueueLoadParameters>(dynamicDataDTO);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00009F08 File Offset: 0x00008108
		public static StudentFilesQueueLoadParametersDTO ToDTO(this StudentFilesQueueLoadParameters dynamicData)
		{
			return Mapper.Map<StudentFilesQueueLoadParameters, StudentFilesQueueLoadParametersDTO>(dynamicData);
		}
	}
}

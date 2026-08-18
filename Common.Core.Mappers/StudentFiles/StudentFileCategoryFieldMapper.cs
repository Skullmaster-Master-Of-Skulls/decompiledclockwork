using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.Core.Mappers.StudentFiles
{
	// Token: 0x02000054 RID: 84
	public static class StudentFileCategoryFieldMapper
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00009C80 File Offset: 0x00007E80
		static StudentFileCategoryFieldMapper()
		{
			Mapper.CreateMap<StudentFileCategoryFieldDTO, StudentFileCategoryField>();
			Mapper.CreateMap<StudentFileCategoryField, StudentFileCategoryFieldDTO>();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00009C90 File Offset: 0x00007E90
		public static StudentFileCategoryField ToDomainObject(this StudentFileCategoryFieldDTO dynamicDataDTO)
		{
			return Mapper.Map<StudentFileCategoryFieldDTO, StudentFileCategoryField>(dynamicDataDTO);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00009CA8 File Offset: 0x00007EA8
		public static StudentFileCategoryFieldDTO ToDTO(this StudentFileCategoryField dynamicData)
		{
			return Mapper.Map<StudentFileCategoryField, StudentFileCategoryFieldDTO>(dynamicData);
		}
	}
}

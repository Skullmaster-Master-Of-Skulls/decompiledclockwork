using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Mappers.Updates
{
	// Token: 0x02000022 RID: 34
	internal static class FileTypeDTOMapper
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00005524 File Offset: 0x00003724
		static FileTypeDTOMapper()
		{
			Mapper.CreateMap<FileTypeDTO, FileType>();
			Mapper.CreateMap<FileType, FileTypeDTO>();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005534 File Offset: 0x00003734
		public static FileTypeDTO ToDTO(this FileType fileType)
		{
			return Mapper.Map<FileType, FileTypeDTO>(fileType);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000554C File Offset: 0x0000374C
		public static FileType ToDomainObject(this FileTypeDTO fileTypeDTO)
		{
			return Mapper.Map<FileTypeDTO, FileType>(fileTypeDTO);
		}
	}
}

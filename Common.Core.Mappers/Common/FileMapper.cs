using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Common;
using TechnoPro.Common.Public.Entities.Common;

namespace TechnoPro.Common.Core.Mappers.Common
{
	// Token: 0x02000167 RID: 359
	public static class FileMapper
	{
		// Token: 0x0600062D RID: 1581 RVA: 0x0001C528 File Offset: 0x0001A728
		static FileMapper()
		{
			Mapper.CreateMap<FileDTO, File>();
			Mapper.CreateMap<File, FileDTO>();
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001C538 File Offset: 0x0001A738
		public static File ToDomainObject(this FileDTO fileDTO)
		{
			return Mapper.Map<FileDTO, File>(fileDTO);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001C550 File Offset: 0x0001A750
		public static FileDTO ToDTO(this File file)
		{
			return Mapper.Map<File, FileDTO>(file);
		}
	}
}

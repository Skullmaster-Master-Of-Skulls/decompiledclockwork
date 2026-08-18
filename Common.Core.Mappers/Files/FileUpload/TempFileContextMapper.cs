using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload;
using TechnoPro.Common.Public.Entities.Files.FileUpload;

namespace TechnoPro.Common.Core.Mappers.Files.FileUpload
{
	// Token: 0x02000112 RID: 274
	public static class TempFileContextMapper
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x00016E32 File Offset: 0x00015032
		static TempFileContextMapper()
		{
			Mapper.CreateMap<TempFileContextDTO, TempFileContext>();
			Mapper.CreateMap<TempFileContext, TempFileContextDTO>();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00016E44 File Offset: 0x00015044
		public static TempFileContext ToDomainObject(this TempFileContextDTO dto)
		{
			return Mapper.Map<TempFileContextDTO, TempFileContext>(dto);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00016E5C File Offset: 0x0001505C
		public static TempFileContextDTO ToDTO(this TempFileContext item)
		{
			return Mapper.Map<TempFileContext, TempFileContextDTO>(item);
		}
	}
}

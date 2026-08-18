using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000214 RID: 532
	public static class MediaContentIdentifierMapper
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x00026CD2 File Offset: 0x00024ED2
		static MediaContentIdentifierMapper()
		{
			Mapper.CreateMap<MediaContentIdentifier, MediaContentIdentifierDTO>();
			Mapper.CreateMap<MediaContentIdentifierDTO, MediaContentIdentifier>();
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00026CE4 File Offset: 0x00024EE4
		public static MediaContentIdentifier ToDomainObject(this MediaContentIdentifierDTO dto)
		{
			return Mapper.Map<MediaContentIdentifierDTO, MediaContentIdentifier>(dto);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00026CFC File Offset: 0x00024EFC
		public static MediaContentIdentifierDTO ToDTO(this MediaContentIdentifier bo)
		{
			return Mapper.Map<MediaContentIdentifier, MediaContentIdentifierDTO>(bo);
		}
	}
}

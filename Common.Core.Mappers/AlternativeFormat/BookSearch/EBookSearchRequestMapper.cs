using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat.BookSearch
{
	// Token: 0x02000224 RID: 548
	public static class EBookSearchRequestMapper
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x0002A0AA File Offset: 0x000282AA
		static EBookSearchRequestMapper()
		{
			Mapper.CreateMap<EBookSearchRequest, EBookSearchRequestDTO>();
			Mapper.CreateMap<EBookSearchRequestDTO, EBookSearchRequest>();
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0002A0BC File Offset: 0x000282BC
		public static EBookSearchRequest ToDomainObject(this EBookSearchRequestDTO dto)
		{
			return Mapper.Map<EBookSearchRequestDTO, EBookSearchRequest>(dto);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0002A0D4 File Offset: 0x000282D4
		public static EBookSearchRequestDTO ToDTO(this EBookSearchRequest bo)
		{
			return Mapper.Map<EBookSearchRequest, EBookSearchRequestDTO>(bo);
		}
	}
}

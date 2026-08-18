using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Accommodations;
using TechnoPro.Common.Public.Entities.DynamicForms.Accommodations;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.Accommodations
{
	// Token: 0x02000134 RID: 308
	public static class AccommodationLetterGenerateContextMapper
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x00019774 File Offset: 0x00017974
		static AccommodationLetterGenerateContextMapper()
		{
			Mapper.CreateMap<AccommodationLetterGenerateContextDTO, AccommodationLetterGenerateContext>();
			Mapper.CreateMap<AccommodationLetterGenerateContext, AccommodationLetterGenerateContextDTO>();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00019784 File Offset: 0x00017984
		public static AccommodationLetterGenerateContext ToDomainObject(this AccommodationLetterGenerateContextDTO dynamicDataDTO)
		{
			return Mapper.Map<AccommodationLetterGenerateContextDTO, AccommodationLetterGenerateContext>(dynamicDataDTO);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001979C File Offset: 0x0001799C
		public static AccommodationLetterGenerateContextDTO ToDTO(this AccommodationLetterGenerateContext dynamicData)
		{
			return Mapper.Map<AccommodationLetterGenerateContext, AccommodationLetterGenerateContextDTO>(dynamicData);
		}
	}
}

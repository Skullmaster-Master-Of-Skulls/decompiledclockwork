using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy;
using TechnoPro.Common.Public.Entities.DynamicForms.Legacy;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.Legacy
{
	// Token: 0x02000127 RID: 295
	public static class LegacySaveDataResultMapper
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x00018704 File Offset: 0x00016904
		static LegacySaveDataResultMapper()
		{
			Mapper.CreateMap<LegacySaveDataResultDTO, LegacySaveDataResult>();
			Mapper.CreateMap<LegacySaveDataResult, LegacySaveDataResultDTO>();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00018714 File Offset: 0x00016914
		public static LegacySaveDataResult ToDomainObject(this LegacySaveDataResultDTO dynamicDataDTO)
		{
			return Mapper.Map<LegacySaveDataResultDTO, LegacySaveDataResult>(dynamicDataDTO);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001872C File Offset: 0x0001692C
		public static LegacySaveDataResultDTO ToDTO(this LegacySaveDataResult dynamicData)
		{
			return Mapper.Map<LegacySaveDataResult, LegacySaveDataResultDTO>(dynamicData);
		}
	}
}

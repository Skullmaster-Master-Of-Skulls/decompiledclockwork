using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.Common.Core.Mappers.Legacy.DynamicData
{
	// Token: 0x020000EA RID: 234
	public static class LegacyDynamicDataItemItemsToBeDecryptedMapper
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x000129E4 File Offset: 0x00010BE4
		static LegacyDynamicDataItemItemsToBeDecryptedMapper()
		{
			Mapper.CreateMap<LegacyDynamicDataItemItemsToBeDecryptedDTO, LegacyDynamicDataItemItemsToBeDecrypted>();
			Mapper.CreateMap<LegacyDynamicDataItemItemsToBeDecrypted, LegacyDynamicDataItemItemsToBeDecryptedDTO>();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000129F4 File Offset: 0x00010BF4
		public static LegacyDynamicDataItemItemsToBeDecrypted ToDomainObject(this LegacyDynamicDataItemItemsToBeDecryptedDTO dynamicDataDTO)
		{
			return Mapper.Map<LegacyDynamicDataItemItemsToBeDecryptedDTO, LegacyDynamicDataItemItemsToBeDecrypted>(dynamicDataDTO);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00012A0C File Offset: 0x00010C0C
		public static LegacyDynamicDataItemItemsToBeDecryptedDTO ToDTO(this LegacyDynamicDataItemItemsToBeDecrypted dynamicData)
		{
			return Mapper.Map<LegacyDynamicDataItemItemsToBeDecrypted, LegacyDynamicDataItemItemsToBeDecryptedDTO>(dynamicData);
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.Common.Core.Mappers.Legacy.DynamicData
{
	// Token: 0x020000E9 RID: 233
	public static class LegacyDynamicDataItemItemsThatHaveBeenDecryptedMapper
	{
		// Token: 0x060003DD RID: 989 RVA: 0x000129A4 File Offset: 0x00010BA4
		static LegacyDynamicDataItemItemsThatHaveBeenDecryptedMapper()
		{
			Mapper.CreateMap<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO, LegacyDynamicDataItemItemsThatHaveBeenDecrypted>();
			Mapper.CreateMap<LegacyDynamicDataItemItemsThatHaveBeenDecrypted, LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO>();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000129B4 File Offset: 0x00010BB4
		public static LegacyDynamicDataItemItemsThatHaveBeenDecrypted ToDomainObject(this LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO dynamicDataDTO)
		{
			return Mapper.Map<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO, LegacyDynamicDataItemItemsThatHaveBeenDecrypted>(dynamicDataDTO);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000129CC File Offset: 0x00010BCC
		public static LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO ToDTO(this LegacyDynamicDataItemItemsThatHaveBeenDecrypted dynamicData)
		{
			return Mapper.Map<LegacyDynamicDataItemItemsThatHaveBeenDecrypted, LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO>(dynamicData);
		}
	}
}

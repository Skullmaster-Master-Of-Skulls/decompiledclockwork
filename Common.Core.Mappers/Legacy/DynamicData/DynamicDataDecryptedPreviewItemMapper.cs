using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.Common.Core.Mappers.Legacy.DynamicData
{
	// Token: 0x020000E8 RID: 232
	public static class DynamicDataDecryptedPreviewItemMapper
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x00012962 File Offset: 0x00010B62
		static DynamicDataDecryptedPreviewItemMapper()
		{
			Mapper.CreateMap<DynamicDataDecryptedPreviewItemDTO, DynamicDataDecryptedPreviewItem>();
			Mapper.CreateMap<DynamicDataDecryptedPreviewItem, DynamicDataDecryptedPreviewItemDTO>();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00012974 File Offset: 0x00010B74
		public static DynamicDataDecryptedPreviewItem ToDomainObject(this DynamicDataDecryptedPreviewItemDTO dynamicDataDTO)
		{
			return Mapper.Map<DynamicDataDecryptedPreviewItemDTO, DynamicDataDecryptedPreviewItem>(dynamicDataDTO);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001298C File Offset: 0x00010B8C
		public static DynamicDataDecryptedPreviewItemDTO ToDTO(this DynamicDataDecryptedPreviewItem dynamicData)
		{
			return Mapper.Map<DynamicDataDecryptedPreviewItem, DynamicDataDecryptedPreviewItemDTO>(dynamicData);
		}
	}
}

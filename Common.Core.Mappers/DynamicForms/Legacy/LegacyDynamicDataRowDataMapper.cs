using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy;
using TechnoPro.Common.Public.Entities.DynamicForms.Legacy;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.Legacy
{
	// Token: 0x02000125 RID: 293
	public static class LegacyDynamicDataRowDataMapper
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x000185D8 File Offset: 0x000167D8
		static LegacyDynamicDataRowDataMapper()
		{
			Mapper.CreateMap<LegacyDynamicDataRowDataDTO, LegacyDynamicDataRowData>();
			Mapper.CreateMap<LegacyDynamicDataRowData, LegacyDynamicDataRowDataDTO>();
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000185E8 File Offset: 0x000167E8
		public static LegacyDynamicDataRowData ToDomainObject(this LegacyDynamicDataRowDataDTO dynamicDataDTO)
		{
			return Mapper.Map<LegacyDynamicDataRowDataDTO, LegacyDynamicDataRowData>(dynamicDataDTO);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00018600 File Offset: 0x00016800
		public static LegacyDynamicDataRowDataDTO ToDTO(this LegacyDynamicDataRowData dynamicData)
		{
			return Mapper.Map<LegacyDynamicDataRowData, LegacyDynamicDataRowDataDTO>(dynamicData);
		}
	}
}

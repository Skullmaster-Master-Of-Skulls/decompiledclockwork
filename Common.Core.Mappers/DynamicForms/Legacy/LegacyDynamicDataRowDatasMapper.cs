using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy;
using TechnoPro.Common.Public.Entities.DynamicForms.Legacy;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.Legacy
{
	// Token: 0x02000126 RID: 294
	public static class LegacyDynamicDataRowDatasMapper
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x00018618 File Offset: 0x00016818
		static LegacyDynamicDataRowDatasMapper()
		{
			LegacyDynamicDataRowDataMapper.CreateMap();
			Mapper.CreateMap<LegacyDynamicDataRowDatasDTO, LegacyDynamicDataRowDatas>().ForMember((LegacyDynamicDataRowDatas pb) => pb.RowDatas, delegate(IMemberConfigurationExpression<LegacyDynamicDataRowDatasDTO> m)
			{
				m.MapFrom<IEnumerable<LegacyDynamicDataRowData>>((LegacyDynamicDataRowDatasDTO pbdto) => (pbdto.RowDatas == null) ? null : (from g in pbdto.RowDatas
				select g.ToDomainObject()));
			});
			Mapper.CreateMap<LegacyDynamicDataRowDatas, LegacyDynamicDataRowDatasDTO>().ForMember((LegacyDynamicDataRowDatasDTO pb) => pb.RowDatas, delegate(IMemberConfigurationExpression<LegacyDynamicDataRowDatas> m)
			{
				m.MapFrom<IEnumerable<LegacyDynamicDataRowDataDTO>>((LegacyDynamicDataRowDatas pbdto) => (pbdto.RowDatas == null) ? null : (from g in pbdto.RowDatas
				select g.ToDTO()));
			});
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000186D4 File Offset: 0x000168D4
		public static LegacyDynamicDataRowDatas ToDomainObject(this LegacyDynamicDataRowDatasDTO dynamicDataDTO)
		{
			return Mapper.Map<LegacyDynamicDataRowDatasDTO, LegacyDynamicDataRowDatas>(dynamicDataDTO);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000186EC File Offset: 0x000168EC
		public static LegacyDynamicDataRowDatasDTO ToDTO(this LegacyDynamicDataRowDatas dynamicData)
		{
			return Mapper.Map<LegacyDynamicDataRowDatas, LegacyDynamicDataRowDatasDTO>(dynamicData);
		}
	}
}

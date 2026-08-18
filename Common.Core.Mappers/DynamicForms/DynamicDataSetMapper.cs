using System;
using System.Collections.Generic;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000117 RID: 279
	public static class DynamicDataSetMapper
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x000171A0 File Offset: 0x000153A0
		static DynamicDataSetMapper()
		{
			DynamicDataContextMapper.CreateMap();
			DynamicDataMapper.CreateMap();
			Mapper.CreateMap<DynamicDataSetDTO, DynamicDataSet>().ForMember((DynamicDataSet pb) => pb.Context, delegate(IMemberConfigurationExpression<DynamicDataSetDTO> m)
			{
				m.MapFrom<DynamicDataContext>((DynamicDataSetDTO pbdto) => (pbdto.Context == null) ? null : pbdto.Context.ToDomainObject());
			}).ForMember((DynamicDataSet pb) => pb.Data, delegate(IMemberConfigurationExpression<DynamicDataSetDTO> m)
			{
				m.MapFrom<List<DynamicData>>((DynamicDataSetDTO pbdto) => (pbdto.Data == null) ? null : pbdto.Data.ConvertAll<DynamicData>((DynamicDataDTO g) => g.ToDomainObject()));
			});
			Mapper.CreateMap<DynamicDataSet, DynamicDataSetDTO>().ForMember((DynamicDataSetDTO pb) => pb.Context, delegate(IMemberConfigurationExpression<DynamicDataSet> m)
			{
				m.MapFrom<DynamicDataContextDTO>((DynamicDataSet pbdto) => (pbdto.Context == null) ? null : pbdto.Context.ToDTO());
			}).ForMember((DynamicDataSetDTO pb) => pb.Data, delegate(IMemberConfigurationExpression<DynamicDataSet> m)
			{
				m.MapFrom<List<DynamicDataDTO>>((DynamicDataSet pbdto) => (pbdto.Data == null) ? null : pbdto.Data.ConvertAll<DynamicDataDTO>((DynamicData g) => g.ToDTO()));
			});
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00017300 File Offset: 0x00015500
		public static DynamicDataSet ToDomainObject(this DynamicDataSetDTO dynamicDataDTO)
		{
			return Mapper.Map<DynamicDataSetDTO, DynamicDataSet>(dynamicDataDTO);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00017318 File Offset: 0x00015518
		public static DynamicDataSetDTO ToDTO(this DynamicDataSet dynamicData)
		{
			return Mapper.Map<DynamicDataSet, DynamicDataSetDTO>(dynamicData);
		}
	}
}

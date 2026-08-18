using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.Common.Core.Mappers.CustomForms.Data.Context;
using TechnoPro.Common.Core.Mappers.CustomForms.Data.DataHolders;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.Common.Core.Mappers.CustomForms.Data
{
	// Token: 0x02000157 RID: 343
	public static class CustomDataSetMapper
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x0001B18C File Offset: 0x0001938C
		static CustomDataSetMapper()
		{
			CustomDataHolderCollectionMapper.CreateMap();
			CustomDataHolderMapper.CreateMap();
			CustomDataPerDateContextMapper.CreateMap();
			CustomDataPerSemesterContextMapper.CreateMap();
			CustomDataPerStudentContextMapper.CreateMap();
			Mapper.CreateMap<CustomDataSetDTO, CustomDataSet>().ForMember((CustomDataSet pb) => pb.Data, delegate(IMemberConfigurationExpression<CustomDataSetDTO> m)
			{
				m.MapFrom<IEnumerable<CustomDataHolderCollection>>((CustomDataSetDTO pbdto) => (pbdto.Data == null) ? null : (from g in pbdto.Data
				select g.ToDomainObject()));
			});
			Mapper.CreateMap<CustomDataSet, CustomDataSetDTO>().ForMember((CustomDataSetDTO pb) => pb.Data, delegate(IMemberConfigurationExpression<CustomDataSet> m)
			{
				m.MapFrom<IEnumerable<CustomDataHolderCollectionDTO>>((CustomDataSet pbdto) => (pbdto.Data == null) ? null : (from g in pbdto.Data
				select g.ToDTO()));
			});
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001B260 File Offset: 0x00019460
		public static CustomDataSet ToDomainObject(this CustomDataSetDTO dto)
		{
			return Mapper.Map<CustomDataSetDTO, CustomDataSet>(dto);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001B278 File Offset: 0x00019478
		public static CustomDataSetDTO ToDTO(this CustomDataSet item)
		{
			return Mapper.Map<CustomDataSet, CustomDataSetDTO>(item);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001B290 File Offset: 0x00019490
		public static IList<CustomDataSet> ToDomainObject(this IList<CustomDataSetDTO> dtos)
		{
			IList<CustomDataSet> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<CustomDataSet>();
			}
			return result;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001B2D4 File Offset: 0x000194D4
		public static IList<CustomDataSetDTO> ToDTO(this IList<CustomDataSet> items)
		{
			IList<CustomDataSetDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<CustomDataSetDTO>();
			}
			return result;
		}
	}
}

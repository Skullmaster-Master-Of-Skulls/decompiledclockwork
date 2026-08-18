using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;
using TechnoPro.Common.Core.Mappers.CustomForms.Data.DataHolders;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Core.Mappers.CustomForms.Data
{
	// Token: 0x02000156 RID: 342
	public static class CustomDataHolderCollectionMapper
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x0001B01C File Offset: 0x0001921C
		static CustomDataHolderCollectionMapper()
		{
			Mapper.CreateMap<CustomDataHolderCollectionDTO, CustomDataHolderCollection>().ForMember((CustomDataHolderCollection pb) => pb.Datas, delegate(IMemberConfigurationExpression<CustomDataHolderCollectionDTO> m)
			{
				m.MapFrom<IEnumerable<CustomDataHolder>>((CustomDataHolderCollectionDTO pbdto) => (pbdto.Datas == null) ? null : (from g in pbdto.Datas
				select g.ToDomainObject()));
			});
			Mapper.CreateMap<CustomDataHolderCollection, CustomDataHolderCollectionDTO>().ForMember((CustomDataHolderCollectionDTO pb) => pb.Datas, delegate(IMemberConfigurationExpression<CustomDataHolderCollection> m)
			{
				m.MapFrom<IEnumerable<CustomDataHolderDTO>>((CustomDataHolderCollection pbdto) => (pbdto.Datas == null) ? null : (from g in pbdto.Datas
				select g.ToDTO()));
			});
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001B0D4 File Offset: 0x000192D4
		public static CustomDataHolderCollection ToDomainObject(this CustomDataHolderCollectionDTO dto)
		{
			return Mapper.Map<CustomDataHolderCollectionDTO, CustomDataHolderCollection>(dto);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001B0EC File Offset: 0x000192EC
		public static CustomDataHolderCollectionDTO ToDTO(this CustomDataHolderCollection item)
		{
			return Mapper.Map<CustomDataHolderCollection, CustomDataHolderCollectionDTO>(item);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001B104 File Offset: 0x00019304
		public static IList<CustomDataHolderCollection> ToDomainObject(this IList<CustomDataHolderCollectionDTO> dtos)
		{
			IList<CustomDataHolderCollection> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<CustomDataHolderCollection>();
			}
			return result;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001B148 File Offset: 0x00019348
		public static IList<CustomDataHolderCollectionDTO> ToDTO(this IList<CustomDataHolderCollection> items)
		{
			IList<CustomDataHolderCollectionDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<CustomDataHolderCollectionDTO>();
			}
			return result;
		}
	}
}

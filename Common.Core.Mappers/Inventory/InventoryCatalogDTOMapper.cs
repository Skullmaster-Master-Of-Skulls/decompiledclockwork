using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000F1 RID: 241
	public static class InventoryCatalogDTOMapper
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x00013178 File Offset: 0x00011378
		static InventoryCatalogDTOMapper()
		{
			PersonBaseMapper.CreateMap();
			InventoryCategoryDTOMapper.CreateMap();
			Mapper.CreateMap<InventoryCatalog, InventoryCatalogDTO>().ForMember((InventoryCatalogDTO dto) => dto.Categories, delegate(IMemberConfigurationExpression<InventoryCatalog> m)
			{
				m.MapFrom<List<InventoryCategoryDTO>>((InventoryCatalog bo) => (bo.Categories != null) ? bo.Categories.ToList<InventoryCategory>().ConvertAll<InventoryCategoryDTO>((InventoryCategory c) => c.ToDTO()) : null);
			});
			Mapper.CreateMap<InventoryCatalogDTO, InventoryCatalog>().ForMember((InventoryCatalog bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryCatalogDTO> m)
			{
				m.Ignore();
			}).ForMember((InventoryCatalog bo) => bo.Categories, delegate(IMemberConfigurationExpression<InventoryCatalogDTO> m)
			{
				m.MapFrom<List<InventoryCategory>>((InventoryCatalogDTO dto) => (dto.Categories != null) ? dto.Categories.ToList<InventoryCategoryDTO>().ConvertAll<InventoryCategory>((InventoryCategoryDTO c) => c.ToDomainObject()) : null);
			});
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001329C File Offset: 0x0001149C
		public static InventoryCatalogDTO ToDTO(this InventoryCatalog catalog)
		{
			return Mapper.Map<InventoryCatalog, InventoryCatalogDTO>(catalog);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000132B4 File Offset: 0x000114B4
		public static InventoryCatalog ToDomainObject(this InventoryCatalogDTO catalogDTO)
		{
			return Mapper.Map<InventoryCatalogDTO, InventoryCatalog>(catalogDTO);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000132CC File Offset: 0x000114CC
		public static IList<InventoryCatalogDTO> ToDTO(this IList<InventoryCatalog> list)
		{
			IList<InventoryCatalogDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryCatalogDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00013310 File Offset: 0x00011510
		public static IList<InventoryCatalog> ToDomainObject(this IList<InventoryCatalogDTO> list)
		{
			IList<InventoryCatalog> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryCatalog>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

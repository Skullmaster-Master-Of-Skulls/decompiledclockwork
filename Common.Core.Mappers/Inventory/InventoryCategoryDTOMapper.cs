using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000F2 RID: 242
	public static class InventoryCategoryDTOMapper
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00013354 File Offset: 0x00011554
		static InventoryCategoryDTOMapper()
		{
			Mapper.CreateMap<InventoryCategory, InventoryCategoryDTO>();
			Mapper.CreateMap<InventoryCategoryDTO, InventoryCategory>().ForMember((InventoryCategory bo) => bo.Id, delegate(IMemberConfigurationExpression<InventoryCategoryDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000133C4 File Offset: 0x000115C4
		public static InventoryCategory ToDomainObject(this InventoryCategoryDTO categoryDTO)
		{
			return Mapper.Map<InventoryCategoryDTO, InventoryCategory>(categoryDTO);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000133DC File Offset: 0x000115DC
		public static InventoryCategoryDTO ToDTO(this InventoryCategory category)
		{
			return Mapper.Map<InventoryCategory, InventoryCategoryDTO>(category);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000133F4 File Offset: 0x000115F4
		public static IList<InventoryCategory> ToDomainObject(this IList<InventoryCategoryDTO> list)
		{
			IList<InventoryCategory> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryCategory>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00013438 File Offset: 0x00011638
		public static IList<InventoryCategoryDTO> ToDTO(this IList<InventoryCategory> list)
		{
			IList<InventoryCategoryDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryCategoryDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

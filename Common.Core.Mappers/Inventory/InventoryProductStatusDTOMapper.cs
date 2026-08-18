using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000FF RID: 255
	public static class InventoryProductStatusDTOMapper
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x00015B5C File Offset: 0x00013D5C
		static InventoryProductStatusDTOMapper()
		{
			Mapper.CreateMap<InventoryProductStatus, InventoryProductStatusDTO>();
			Mapper.CreateMap<InventoryProductStatusDTO, InventoryProductStatus>().ForMember((InventoryProductStatus bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryProductStatusDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00015BD8 File Offset: 0x00013DD8
		public static InventoryProductStatusDTO ToDTO(this InventoryProductStatus productStatus)
		{
			return Mapper.Map<InventoryProductStatus, InventoryProductStatusDTO>(productStatus);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00015BF0 File Offset: 0x00013DF0
		public static InventoryProductStatus ToDomainObject(this InventoryProductStatusDTO productStatusDTO)
		{
			return Mapper.Map<InventoryProductStatusDTO, InventoryProductStatus>(productStatusDTO);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00015C08 File Offset: 0x00013E08
		public static IList<InventoryProductStatusDTO> ToDTO(this IList<InventoryProductStatus> list)
		{
			IList<InventoryProductStatusDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryProductStatusDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00015C4C File Offset: 0x00013E4C
		public static IList<InventoryProductStatus> ToDomainObject(this IList<InventoryProductStatusDTO> list)
		{
			IList<InventoryProductStatus> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryProductStatus>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000FE RID: 254
	public static class InventoryProductBookedTimeMapper
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x00015A8C File Offset: 0x00013C8C
		static InventoryProductBookedTimeMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<InventoryProductBookedTime, InventoryProductBookedTimeDTO>();
			Mapper.CreateMap<InventoryProductBookedTimeDTO, InventoryProductBookedTime>();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00015AA4 File Offset: 0x00013CA4
		public static InventoryProductBookedTime ToDomainObject(this InventoryProductBookedTimeDTO productDTO)
		{
			return Mapper.Map<InventoryProductBookedTimeDTO, InventoryProductBookedTime>(productDTO);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00015ABC File Offset: 0x00013CBC
		public static InventoryProductBookedTimeDTO ToDTO(this InventoryProductBookedTime product)
		{
			return Mapper.Map<InventoryProductBookedTime, InventoryProductBookedTimeDTO>(product);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00015AD4 File Offset: 0x00013CD4
		public static IList<InventoryProductBookedTime> ToDomainObject(this IList<InventoryProductBookedTimeDTO> list)
		{
			IList<InventoryProductBookedTime> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryProductBookedTime>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00015B18 File Offset: 0x00013D18
		public static IList<InventoryProductBookedTimeDTO> ToDTO(this IList<InventoryProductBookedTime> list)
		{
			IList<InventoryProductBookedTimeDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryProductBookedTimeDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

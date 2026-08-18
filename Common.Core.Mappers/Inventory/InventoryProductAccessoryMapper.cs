using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000FC RID: 252
	public static class InventoryProductAccessoryMapper
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x00015618 File Offset: 0x00013818
		static InventoryProductAccessoryMapper()
		{
			Mapper.CreateMap<InventoryProductAccessory, InventoryProductAccessoryDTO>().ForMember((InventoryProductAccessoryDTO dto) => dto.Name, delegate(IMemberConfigurationExpression<InventoryProductAccessory> m)
			{
				m.MapFrom<string>((InventoryProductAccessory bo) => bo.Name);
			}).ForMember((InventoryProductAccessoryDTO dto) => dto.Description, delegate(IMemberConfigurationExpression<InventoryProductAccessory> m)
			{
				m.MapFrom<string>((InventoryProductAccessory bo) => bo.Description);
			});
			Mapper.CreateMap<InventoryProductAccessoryDTO, InventoryProductAccessory>().ForMember((InventoryProductAccessory bo) => bo.Id, delegate(IMemberConfigurationExpression<InventoryProductAccessoryDTO> m)
			{
				m.Ignore();
			}).ForMember((InventoryProductAccessory bo) => bo.Name, delegate(IMemberConfigurationExpression<InventoryProductAccessoryDTO> m)
			{
				m.MapFrom<string>((InventoryProductAccessoryDTO dto) => dto.Name);
			}).ForMember((InventoryProductAccessory bo) => bo.Description, delegate(IMemberConfigurationExpression<InventoryProductAccessoryDTO> m)
			{
				m.MapFrom<string>((InventoryProductAccessoryDTO dto) => dto.Description);
			});
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000157C0 File Offset: 0x000139C0
		public static InventoryProductAccessory ToDomainObject(this InventoryProductAccessoryDTO dto)
		{
			return Mapper.Map<InventoryProductAccessoryDTO, InventoryProductAccessory>(dto);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000157D8 File Offset: 0x000139D8
		public static InventoryProductAccessoryDTO ToDTO(this InventoryProductAccessory bo)
		{
			return Mapper.Map<InventoryProductAccessory, InventoryProductAccessoryDTO>(bo);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000157F0 File Offset: 0x000139F0
		public static IList<InventoryProductAccessory> ToDomainObject(this IList<InventoryProductAccessoryDTO> list)
		{
			IList<InventoryProductAccessory> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryProductAccessory>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00015834 File Offset: 0x00013A34
		public static IList<InventoryProductAccessoryDTO> ToDTO(this IList<InventoryProductAccessory> list)
		{
			IList<InventoryProductAccessoryDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryProductAccessoryDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

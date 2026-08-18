using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000F3 RID: 243
	public static class InventoryGroupDTOMapper
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0001347C File Offset: 0x0001167C
		static InventoryGroupDTOMapper()
		{
			Mapper.CreateMap<InventoryGroup, InventoryGroupDTO>();
			Mapper.CreateMap<InventoryGroupDTO, InventoryGroup>().ForMember((InventoryGroup bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryGroupDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000134F8 File Offset: 0x000116F8
		public static InventoryGroup ToDomainObject(this InventoryGroupDTO groupDTO)
		{
			return Mapper.Map<InventoryGroupDTO, InventoryGroup>(groupDTO);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00013510 File Offset: 0x00011710
		public static InventoryGroupDTO ToDTO(this InventoryGroup group)
		{
			return Mapper.Map<InventoryGroup, InventoryGroupDTO>(group);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00013528 File Offset: 0x00011728
		public static IList<InventoryGroup> ToDomainObject(this IList<InventoryGroupDTO> list)
		{
			IList<InventoryGroup> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryGroup>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001356C File Offset: 0x0001176C
		public static IList<InventoryGroupDTO> ToDTO(this IList<InventoryGroup> list)
		{
			IList<InventoryGroupDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryGroupDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

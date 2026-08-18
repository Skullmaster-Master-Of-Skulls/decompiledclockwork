using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x02000101 RID: 257
	public static class InventoryReservationGroupDTOMapper
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x00015DD0 File Offset: 0x00013FD0
		static InventoryReservationGroupDTOMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<InventoryReservationGroup, InventoryReservationGroupDTO>();
			Mapper.CreateMap<InventoryReservationGroupDTO, InventoryReservationGroup>().ForMember((InventoryReservationGroup bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryReservationGroupDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00015E54 File Offset: 0x00014054
		public static InventoryReservationGroup ToDomainObject(this InventoryReservationGroupDTO reservationGroupDTO)
		{
			return Mapper.Map<InventoryReservationGroupDTO, InventoryReservationGroup>(reservationGroupDTO);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00015E6C File Offset: 0x0001406C
		public static InventoryReservationGroupDTO ToDTO(this InventoryReservationGroup reservationGroup)
		{
			return Mapper.Map<InventoryReservationGroup, InventoryReservationGroupDTO>(reservationGroup);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00015E84 File Offset: 0x00014084
		public static IList<InventoryReservationGroup> ToDomainObject(this IList<InventoryReservationGroupDTO> list)
		{
			IList<InventoryReservationGroup> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryReservationGroup>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00015EC8 File Offset: 0x000140C8
		public static IList<InventoryReservationGroupDTO> ToDTO(this IList<InventoryReservationGroup> list)
		{
			IList<InventoryReservationGroupDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryReservationGroupDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

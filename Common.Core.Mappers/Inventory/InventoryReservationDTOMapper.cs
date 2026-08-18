using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x02000100 RID: 256
	public static class InventoryReservationDTOMapper
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x00015C90 File Offset: 0x00013E90
		static InventoryReservationDTOMapper()
		{
			InventoryProductDTOMapper.CreateMap();
			InventoryReservationGroupDTOMapper.CreateMap();
			Mapper.CreateMap<InventoryReservation, InventoryReservationDTO>();
			Mapper.CreateMap<InventoryReservationDTO, InventoryReservation>().ForMember((InventoryReservation bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryReservationDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00015D18 File Offset: 0x00013F18
		public static InventoryReservation ToDomainObject(this InventoryReservationDTO reservationDTO)
		{
			return Mapper.Map<InventoryReservationDTO, InventoryReservation>(reservationDTO);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00015D30 File Offset: 0x00013F30
		public static InventoryReservationDTO ToDTO(this InventoryReservation reservation)
		{
			return Mapper.Map<InventoryReservation, InventoryReservationDTO>(reservation);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00015D48 File Offset: 0x00013F48
		public static IList<InventoryReservation> ToDomainObject(this IList<InventoryReservationDTO> list)
		{
			IList<InventoryReservation> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryReservation>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00015D8C File Offset: 0x00013F8C
		public static IList<InventoryReservationDTO> ToDTO(this IList<InventoryReservation> list)
		{
			IList<InventoryReservationDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryReservationDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

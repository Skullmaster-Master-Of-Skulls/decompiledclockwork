using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000F9 RID: 249
	public static class InventoryLocationDTOMapper
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x00014708 File Offset: 0x00012908
		static InventoryLocationDTOMapper()
		{
			Mapper.CreateMap<InventoryLocation, InventoryLocationDTO>().ForMember((InventoryLocationDTO dto) => (object)dto.LocationId, delegate(IMemberConfigurationExpression<InventoryLocation> m)
			{
				m.MapFrom<int>((InventoryLocation bo) => bo.LocationId);
			}).ForMember((InventoryLocationDTO dto) => dto.Campus, delegate(IMemberConfigurationExpression<InventoryLocation> m)
			{
				m.MapFrom<string>((InventoryLocation bo) => bo.Campus);
			}).ForMember((InventoryLocationDTO dto) => dto.Building, delegate(IMemberConfigurationExpression<InventoryLocation> m)
			{
				m.MapFrom<string>((InventoryLocation bo) => bo.Building);
			}).ForMember((InventoryLocationDTO dto) => dto.RoomNumber, delegate(IMemberConfigurationExpression<InventoryLocation> m)
			{
				m.MapFrom<string>((InventoryLocation bo) => bo.RoomNumber);
			}).ForMember((InventoryLocationDTO dto) => dto.Seat, delegate(IMemberConfigurationExpression<InventoryLocation> m)
			{
				m.MapFrom<string>((InventoryLocation bo) => bo.Seat);
			}).ForMember((InventoryLocationDTO dto) => dto.Notes, delegate(IMemberConfigurationExpression<InventoryLocation> m)
			{
				m.MapFrom<string>((InventoryLocation bo) => bo.Notes);
			});
			Mapper.CreateMap<InventoryLocationDTO, InventoryLocation>().ForMember((InventoryLocation bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryLocationDTO> m)
			{
				m.Ignore();
			}).ForMember((InventoryLocation bo) => (object)bo.LocationId, delegate(IMemberConfigurationExpression<InventoryLocationDTO> m)
			{
				m.MapFrom<int>((InventoryLocationDTO dto) => dto.LocationId);
			}).ForMember((InventoryLocation bo) => bo.Campus, delegate(IMemberConfigurationExpression<InventoryLocationDTO> m)
			{
				m.MapFrom<string>((InventoryLocationDTO dto) => dto.Campus);
			}).ForMember((InventoryLocation bo) => bo.Building, delegate(IMemberConfigurationExpression<InventoryLocationDTO> m)
			{
				m.MapFrom<string>((InventoryLocationDTO dto) => dto.Building);
			}).ForMember((InventoryLocation bo) => bo.RoomNumber, delegate(IMemberConfigurationExpression<InventoryLocationDTO> m)
			{
				m.MapFrom<string>((InventoryLocationDTO dto) => dto.RoomNumber);
			}).ForMember((InventoryLocation bo) => bo.Seat, delegate(IMemberConfigurationExpression<InventoryLocationDTO> m)
			{
				m.MapFrom<string>((InventoryLocationDTO dto) => dto.Seat);
			}).ForMember((InventoryLocation bo) => bo.Notes, delegate(IMemberConfigurationExpression<InventoryLocationDTO> m)
			{
				m.MapFrom<string>((InventoryLocationDTO dto) => dto.Notes);
			});
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00014B4C File Offset: 0x00012D4C
		public static InventoryLocation ToDomainObject(this InventoryLocationDTO locationDTO)
		{
			return Mapper.Map<InventoryLocationDTO, InventoryLocation>(locationDTO);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00014B64 File Offset: 0x00012D64
		public static InventoryLocationDTO ToDTO(this InventoryLocation location)
		{
			return Mapper.Map<InventoryLocation, InventoryLocationDTO>(location);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00014B7C File Offset: 0x00012D7C
		public static IList<InventoryLocation> ToDomainObject(this IList<InventoryLocationDTO> list)
		{
			IList<InventoryLocation> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryLocation>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00014BC0 File Offset: 0x00012DC0
		public static IList<InventoryLocationDTO> ToDTO(this IList<InventoryLocation> list)
		{
			IList<InventoryLocationDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryLocationDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.Core.Mappers.CustomForms.Field
{
	// Token: 0x02000155 RID: 341
	public static class CustomListItemMapper
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x0001AEE8 File Offset: 0x000190E8
		static CustomListItemMapper()
		{
			Mapper.CreateMap<CustomListItemDTO, CustomListItem>().ForMember((CustomListItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CustomListItemDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<CustomListItem, CustomListItemDTO>();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001AF64 File Offset: 0x00019164
		public static CustomListItem ToDomainObject(this CustomListItemDTO dto)
		{
			return Mapper.Map<CustomListItemDTO, CustomListItem>(dto);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001AF7C File Offset: 0x0001917C
		public static CustomListItemDTO ToDTO(this CustomListItem item)
		{
			return Mapper.Map<CustomListItem, CustomListItemDTO>(item);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001AF94 File Offset: 0x00019194
		public static IList<CustomListItem> ToDomainObject(this IList<CustomListItemDTO> dtos)
		{
			IList<CustomListItem> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<CustomListItem>();
			}
			return result;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001AFD8 File Offset: 0x000191D8
		public static IList<CustomListItemDTO> ToDTO(this IList<CustomListItem> items)
		{
			IList<CustomListItemDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<CustomListItemDTO>();
			}
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.Core.Mappers.CustomForms.Field
{
	// Token: 0x02000154 RID: 340
	public static class CustomListItemGroupMapper
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x0001AD14 File Offset: 0x00018F14
		static CustomListItemGroupMapper()
		{
			CustomListItemMapper.CreateMap();
			Mapper.CreateMap<CustomListItemGroupDTO, CustomListItemGroup>().ForMember((CustomListItemGroup pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CustomListItemGroupDTO> m)
			{
				m.Ignore();
			}).ForMember((CustomListItemGroup pb) => pb.ListItems, delegate(IMemberConfigurationExpression<CustomListItemGroupDTO> m)
			{
				m.MapFrom<IEnumerable<CustomListItem>>((CustomListItemGroupDTO pbdto) => (pbdto.ListItems == null) ? null : (from g in pbdto.ListItems
				select g.ToDomainObject()));
			});
			Mapper.CreateMap<CustomListItemGroup, CustomListItemGroupDTO>().ForMember((CustomListItemGroupDTO pb) => pb.ListItems, delegate(IMemberConfigurationExpression<CustomListItemGroup> m)
			{
				m.MapFrom<IEnumerable<CustomListItemDTO>>((CustomListItemGroup pbdto) => (pbdto.ListItems == null) ? null : (from g in pbdto.ListItems
				select g.ToDTO()));
			});
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001AE30 File Offset: 0x00019030
		public static CustomListItemGroup ToDomainObject(this CustomListItemGroupDTO dto)
		{
			return Mapper.Map<CustomListItemGroupDTO, CustomListItemGroup>(dto);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001AE48 File Offset: 0x00019048
		public static CustomListItemGroupDTO ToDTO(this CustomListItemGroup item)
		{
			return Mapper.Map<CustomListItemGroup, CustomListItemGroupDTO>(item);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001AE60 File Offset: 0x00019060
		public static IList<CustomListItemGroup> ToDomainObject(this IList<CustomListItemGroupDTO> dtos)
		{
			IList<CustomListItemGroup> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<CustomListItemGroup>();
			}
			return result;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001AEA4 File Offset: 0x000190A4
		public static IList<CustomListItemGroupDTO> ToDTO(this IList<CustomListItemGroup> items)
		{
			IList<CustomListItemGroupDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<CustomListItemGroupDTO>();
			}
			return result;
		}
	}
}

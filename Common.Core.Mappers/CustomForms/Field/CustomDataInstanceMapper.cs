using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.Core.Mappers.CustomForms.Field
{
	// Token: 0x02000153 RID: 339
	public static class CustomDataInstanceMapper
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x0001ABE0 File Offset: 0x00018DE0
		static CustomDataInstanceMapper()
		{
			Mapper.CreateMap<CustomDataInstanceDTO, CustomDataInstance>().ForMember((CustomDataInstance pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CustomDataInstanceDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<CustomDataInstance, CustomDataInstanceDTO>();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001AC5C File Offset: 0x00018E5C
		public static CustomDataInstance ToDomainObject(this CustomDataInstanceDTO dto)
		{
			return Mapper.Map<CustomDataInstanceDTO, CustomDataInstance>(dto);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001AC74 File Offset: 0x00018E74
		public static CustomDataInstanceDTO ToDTO(this CustomDataInstance item)
		{
			return Mapper.Map<CustomDataInstance, CustomDataInstanceDTO>(item);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001AC8C File Offset: 0x00018E8C
		public static IList<CustomDataInstance> ToDomainObject(this IList<CustomDataInstanceDTO> dtos)
		{
			IList<CustomDataInstance> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<CustomDataInstance>();
			}
			return result;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001ACD0 File Offset: 0x00018ED0
		public static IList<CustomDataInstanceDTO> ToDTO(this IList<CustomDataInstance> items)
		{
			IList<CustomDataInstanceDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<CustomDataInstanceDTO>();
			}
			return result;
		}
	}
}

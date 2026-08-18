using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form;
using TechnoPro.Common.Public.Entities.CustomForms.Form;

namespace TechnoPro.Common.Core.Mappers.CustomForms.Form
{
	// Token: 0x02000152 RID: 338
	public static class CustomFormMapper
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x0001AAAC File Offset: 0x00018CAC
		static CustomFormMapper()
		{
			Mapper.CreateMap<CustomFormDTO, CustomForm>().ForMember((CustomForm pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CustomFormDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<CustomForm, CustomFormDTO>();
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001AB28 File Offset: 0x00018D28
		public static CustomForm ToDomainObject(this CustomFormDTO dto)
		{
			return Mapper.Map<CustomFormDTO, CustomForm>(dto);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001AB40 File Offset: 0x00018D40
		public static CustomFormDTO ToDTO(this CustomForm item)
		{
			return Mapper.Map<CustomForm, CustomFormDTO>(item);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001AB58 File Offset: 0x00018D58
		public static IList<CustomForm> ToDomainObject(this IList<CustomFormDTO> dtos)
		{
			IList<CustomForm> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<CustomForm>();
			}
			return result;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001AB9C File Offset: 0x00018D9C
		public static IList<CustomFormDTO> ToDTO(this IList<CustomForm> items)
		{
			IList<CustomFormDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<CustomFormDTO>();
			}
			return result;
		}
	}
}

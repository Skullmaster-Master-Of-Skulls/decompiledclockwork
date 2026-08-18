using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context;
using TechnoPro.Common.Public.Entities.CustomForms.Data.Context;

namespace TechnoPro.Common.Core.Mappers.CustomForms.Data.Context
{
	// Token: 0x0200015A RID: 346
	public static class CustomDataPerDateContextMapper
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x0001B5B8 File Offset: 0x000197B8
		static CustomDataPerDateContextMapper()
		{
			Mapper.CreateMap<CustomDataPerDateContextDTO, CustomDataPerDateContext>();
			Mapper.CreateMap<CustomDataPerDateContext, CustomDataPerDateContextDTO>();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001B5C8 File Offset: 0x000197C8
		public static CustomDataPerDateContext ToDomainObject(this CustomDataPerDateContextDTO dto)
		{
			return Mapper.Map<CustomDataPerDateContextDTO, CustomDataPerDateContext>(dto);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001B5E0 File Offset: 0x000197E0
		public static CustomDataPerDateContextDTO ToDTO(this CustomDataPerDateContext item)
		{
			return Mapper.Map<CustomDataPerDateContext, CustomDataPerDateContextDTO>(item);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001B5F8 File Offset: 0x000197F8
		public static IList<CustomDataPerDateContext> ToDomainObject(this IList<CustomDataPerDateContextDTO> dtos)
		{
			IList<CustomDataPerDateContext> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<CustomDataPerDateContext>();
			}
			return result;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001B63C File Offset: 0x0001983C
		public static IList<CustomDataPerDateContextDTO> ToDTO(this IList<CustomDataPerDateContext> items)
		{
			IList<CustomDataPerDateContextDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<CustomDataPerDateContextDTO>();
			}
			return result;
		}
	}
}

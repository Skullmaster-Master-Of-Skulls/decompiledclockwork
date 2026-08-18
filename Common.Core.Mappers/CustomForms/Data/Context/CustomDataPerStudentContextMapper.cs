using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context;
using TechnoPro.Common.Public.Entities.CustomForms.Data.Context;

namespace TechnoPro.Common.Core.Mappers.CustomForms.Data.Context
{
	// Token: 0x0200015C RID: 348
	public static class CustomDataPerStudentContextMapper
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x0001B74E File Offset: 0x0001994E
		static CustomDataPerStudentContextMapper()
		{
			Mapper.CreateMap<CustomDataPerStudentContextDTO, CustomDataPerStudentContext>().IncludeBase<CustomDataContext, CustomDataContextDTO>();
			Mapper.CreateMap<CustomDataPerStudentContext, CustomDataPerStudentContextDTO>().IncludeBase<CustomDataContext, CustomDataContextDTO>();
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001B768 File Offset: 0x00019968
		public static CustomDataPerStudentContext ToDomainObject(this CustomDataPerStudentContextDTO dto)
		{
			return Mapper.Map<CustomDataPerStudentContextDTO, CustomDataPerStudentContext>(dto);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001B780 File Offset: 0x00019980
		public static CustomDataPerStudentContextDTO ToDTO(this CustomDataPerStudentContext item)
		{
			return Mapper.Map<CustomDataPerStudentContext, CustomDataPerStudentContextDTO>(item);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001B798 File Offset: 0x00019998
		public static IList<CustomDataPerStudentContext> ToDomainObject(this IList<CustomDataPerStudentContextDTO> dtos)
		{
			IList<CustomDataPerStudentContext> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<CustomDataPerStudentContext>();
			}
			return result;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001B7DC File Offset: 0x000199DC
		public static IList<CustomDataPerStudentContextDTO> ToDTO(this IList<CustomDataPerStudentContext> items)
		{
			IList<CustomDataPerStudentContextDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<CustomDataPerStudentContextDTO>();
			}
			return result;
		}
	}
}

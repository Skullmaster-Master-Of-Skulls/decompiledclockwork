using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context;
using TechnoPro.Common.Public.Entities.CustomForms.Data.Context;

namespace TechnoPro.Common.Core.Mappers.CustomForms.Data.Context
{
	// Token: 0x0200015B RID: 347
	public static class CustomDataPerSemesterContextMapper
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x0001B67E File Offset: 0x0001987E
		static CustomDataPerSemesterContextMapper()
		{
			Mapper.CreateMap<CustomDataPerSemesterContextDTO, CustomDataPerSemesterContext>().IncludeBase<CustomDataContext, CustomDataContextDTO>();
			Mapper.CreateMap<CustomDataPerSemesterContext, CustomDataPerSemesterContextDTO>().IncludeBase<CustomDataContext, CustomDataContextDTO>();
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001B698 File Offset: 0x00019898
		public static CustomDataPerSemesterContext ToDomainObject(this CustomDataPerSemesterContextDTO dto)
		{
			return Mapper.Map<CustomDataPerSemesterContextDTO, CustomDataPerSemesterContext>(dto);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001B6B0 File Offset: 0x000198B0
		public static CustomDataPerSemesterContextDTO ToDTO(this CustomDataPerSemesterContext item)
		{
			return Mapper.Map<CustomDataPerSemesterContext, CustomDataPerSemesterContextDTO>(item);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001B6C8 File Offset: 0x000198C8
		public static IList<CustomDataPerSemesterContext> ToDomainObject(this IList<CustomDataPerSemesterContextDTO> dtos)
		{
			IList<CustomDataPerSemesterContext> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<CustomDataPerSemesterContext>();
			}
			return result;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001B70C File Offset: 0x0001990C
		public static IList<CustomDataPerSemesterContextDTO> ToDTO(this IList<CustomDataPerSemesterContext> items)
		{
			IList<CustomDataPerSemesterContextDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<CustomDataPerSemesterContextDTO>();
			}
			return result;
		}
	}
}

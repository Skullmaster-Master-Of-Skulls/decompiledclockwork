using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x02000077 RID: 119
	public static class SPRequestWithSubItemsMapper
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000C18A File Offset: 0x0000A38A
		static SPRequestWithSubItemsMapper()
		{
			SPRequestMapper.CreateMap();
			SPRequestCourseMapper.CreateMap();
			SPRequestEventMapper.CreateMap();
			Mapper.CreateMap<SPRequestWithSubItems, SPRequestWithSubItemsDTO>();
			Mapper.CreateMap<SPRequestWithSubItemsDTO, SPRequestWithSubItems>();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		public static SPRequestWithSubItems ToDomainObject(this SPRequestWithSubItemsDTO dto)
		{
			return Mapper.Map<SPRequestWithSubItemsDTO, SPRequestWithSubItems>(dto);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
		public static SPRequestWithSubItemsDTO ToDTO(this SPRequestWithSubItems item)
		{
			return Mapper.Map<SPRequestWithSubItems, SPRequestWithSubItemsDTO>(item);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		public static IList<SPRequestWithSubItems> ToDomainObject(this IList<SPRequestWithSubItemsDTO> list)
		{
			IList<SPRequestWithSubItems> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPRequestWithSubItems>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000C220 File Offset: 0x0000A420
		public static IList<SPRequestWithSubItemsDTO> ToDTO(this IList<SPRequestWithSubItems> list)
		{
			IList<SPRequestWithSubItemsDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPRequestWithSubItemsDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x02000068 RID: 104
	public static class SPApplicationAvailabilityItemMapper
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x0000AEA8 File Offset: 0x000090A8
		static SPApplicationAvailabilityItemMapper()
		{
			SPApplicationMapper.CreateMap();
			Mapper.CreateMap<SPApplicationAvailabilityItem, SPApplicationAvailabilityItemDTO>();
			Mapper.CreateMap<SPApplicationAvailabilityItemDTO, SPApplicationAvailabilityItem>().ForMember((SPApplicationAvailabilityItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPApplicationAvailabilityItemDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000AF2C File Offset: 0x0000912C
		public static SPApplicationAvailabilityItem ToDomainObject(this SPApplicationAvailabilityItemDTO dto)
		{
			return Mapper.Map<SPApplicationAvailabilityItemDTO, SPApplicationAvailabilityItem>(dto);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000AF44 File Offset: 0x00009144
		public static SPApplicationAvailabilityItemDTO ToDTO(this SPApplicationAvailabilityItem item)
		{
			return Mapper.Map<SPApplicationAvailabilityItem, SPApplicationAvailabilityItemDTO>(item);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000AF5C File Offset: 0x0000915C
		public static IList<SPApplicationAvailabilityItem> ToDomainObject(this IList<SPApplicationAvailabilityItemDTO> list)
		{
			IList<SPApplicationAvailabilityItem> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPApplicationAvailabilityItem>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public static IList<SPApplicationAvailabilityItemDTO> ToDTO(this IList<SPApplicationAvailabilityItem> list)
		{
			IList<SPApplicationAvailabilityItemDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPApplicationAvailabilityItemDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

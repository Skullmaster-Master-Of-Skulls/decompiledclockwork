using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x02000069 RID: 105
	public static class SPApplicationAvailabilityTypeMapper
	{
		// Token: 0x060001AE RID: 430 RVA: 0x0000AFE4 File Offset: 0x000091E4
		static SPApplicationAvailabilityTypeMapper()
		{
			Mapper.CreateMap<SPApplicationAvailabilityType, SPApplicationAvailabilityTypeDTO>();
			Mapper.CreateMap<SPApplicationAvailabilityTypeDTO, SPApplicationAvailabilityType>().ForMember((SPApplicationAvailabilityType pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPApplicationAvailabilityTypeDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000B060 File Offset: 0x00009260
		public static SPApplicationAvailabilityType ToDomainObject(this SPApplicationAvailabilityTypeDTO dto)
		{
			return Mapper.Map<SPApplicationAvailabilityTypeDTO, SPApplicationAvailabilityType>(dto);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000B078 File Offset: 0x00009278
		public static SPApplicationAvailabilityTypeDTO ToDTO(this SPApplicationAvailabilityType item)
		{
			return Mapper.Map<SPApplicationAvailabilityType, SPApplicationAvailabilityTypeDTO>(item);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000B090 File Offset: 0x00009290
		public static IList<SPApplicationAvailabilityType> ToDomainObject(this IList<SPApplicationAvailabilityTypeDTO> list)
		{
			IList<SPApplicationAvailabilityType> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPApplicationAvailabilityType>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000B0D4 File Offset: 0x000092D4
		public static IList<SPApplicationAvailabilityTypeDTO> ToDTO(this IList<SPApplicationAvailabilityType> list)
		{
			IList<SPApplicationAvailabilityTypeDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPApplicationAvailabilityTypeDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

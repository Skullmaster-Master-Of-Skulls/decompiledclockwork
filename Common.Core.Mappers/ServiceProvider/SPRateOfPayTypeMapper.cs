using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x0200006F RID: 111
	public static class SPRateOfPayTypeMapper
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000B764 File Offset: 0x00009964
		static SPRateOfPayTypeMapper()
		{
			Mapper.CreateMap<SPRateOfPayType, SPRateOfPayTypeDTO>();
			Mapper.CreateMap<SPRateOfPayTypeDTO, SPRateOfPayType>().ForMember((SPRateOfPayType pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPRateOfPayTypeDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public static SPRateOfPayType ToDomainObject(this SPRateOfPayTypeDTO dto)
		{
			return Mapper.Map<SPRateOfPayTypeDTO, SPRateOfPayType>(dto);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000B7F8 File Offset: 0x000099F8
		public static SPRateOfPayTypeDTO ToDTO(this SPRateOfPayType item)
		{
			return Mapper.Map<SPRateOfPayType, SPRateOfPayTypeDTO>(item);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000B810 File Offset: 0x00009A10
		public static IList<SPRateOfPayType> ToDomainObject(this IList<SPRateOfPayTypeDTO> list)
		{
			IList<SPRateOfPayType> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPRateOfPayType>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000B854 File Offset: 0x00009A54
		public static IList<SPRateOfPayTypeDTO> ToDTO(this IList<SPRateOfPayType> list)
		{
			IList<SPRateOfPayTypeDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPRateOfPayTypeDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
